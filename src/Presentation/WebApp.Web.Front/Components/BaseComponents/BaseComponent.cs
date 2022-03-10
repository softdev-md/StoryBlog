using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebApp.Web.Front.Components
{
    /// <summary>
    /// Base class for BaseComponent
    /// </summary>
    public abstract class BaseComponent : ComponentBase, IDisposable
    {
        private readonly Queue<Func<Task>> _afterRenderCallQueue = new Queue<Func<Task>>();

        protected void CallAfterRender(Func<Task> action)
        {
            _afterRenderCallQueue.Enqueue(action);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await OnFirstAfterRenderAsync();
            }

            if (_afterRenderCallQueue.Count > 0)
            {
                var actions = _afterRenderCallQueue.ToArray();
                _afterRenderCallQueue.Clear();

                foreach (var action in actions)
                {
                    if (IsDisposed)
                    {
                        return;
                    }

                    await action();
                }
            }
        }

        protected virtual Task OnFirstAfterRenderAsync()
        {
            return Task.CompletedTask;
        }

        protected BaseComponent()
        {
        }

        protected void InvokeStateHasChanged()
        {
            InvokeAsync(() =>
            {
                if (!IsDisposed)
                {
                    StateHasChanged();
                }
            });
        }

        protected Task InvokeStateHasChangedAsync()
        {
            return InvokeAsync(() =>
             {
                 if (!IsDisposed)
                 {
                     StateHasChanged();
                 }
             });
        }

        [Inject]
        protected IJSRuntime Js { get; set; }

        protected async Task<T> JsInvokeAsync<T>(string code, params object[] args)
        {
            try
            {
                return await Js.InvokeAsync<T>(code, args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected async Task JsInvokeAsync(string code, params object[] args)
        {
            try
            {
                await Js.InvokeVoidAsync(code, args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected bool IsDisposed { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;

            IsDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseComponent()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }
    }
}