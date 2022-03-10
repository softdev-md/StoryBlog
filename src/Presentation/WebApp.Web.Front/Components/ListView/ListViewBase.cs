// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebApp.Web.Front.Components;
using WebApp.Web.Front.Helpers;

namespace WebApp.Web.Front.Components
{
    public class ListGridType
    {
        public string Gutter { get; set; }
        public string Column { get; set; }
        public string Xs { get; set; }
        public string Sm { get; set; }
        public string Md { get; set; }
        public string Lg { get; set; }
        public string Xl { get; set; }
        public string Xxl { get; set; }
    }

    public class ListViewBase<TItem> : BootstrapComponentBase where TItem : class, new()
    {
        protected virtual string? ClassName => CssBuilder.Default("listview")
            .AddClass("listview-grid", () => Grid != null)
            .AddClass("listview-vertical", () => Grid == null)
            .AddClass("listview-split", () => Split)
            .AddClass("listview-bordered", () => Bordered)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        [Parameter]
        public RenderFragment? Header { get; set; }

        [Parameter]
        public RenderFragment<TItem> ChildContent { get; set; }
        
        [Parameter]
        public RenderFragment? Footer { get; set; }
        
        [Parameter]
        public IEnumerable<TItem>? Items { get; set; }
        
        [Parameter] 
        public ListGridType Grid { get; set; }

        [Parameter]
        public bool Bordered { get; set; }

        [Parameter] 
        public bool Split { get; set; }

        [Parameter]
        public bool Pageable { get; set; }

        [Parameter]
        public IEnumerable<int>? PageItemsSource { get; set; }

        [Parameter]
        public Func<TItem, object>? GroupName { get; set; }

        [Parameter] 
        public string NoResult { get; set; }

        [Parameter] 
        public bool Loading { get; set; } = false;

        [Parameter] 
        public RenderFragment LoadMore { get; set; }
        
        [Parameter] 
        public RenderFragment LoadMoreScroll { get; set; }

        [Parameter]
        public Func<QueryPageOptions, Task<QueryData<TItem>>>? OnQueryAsync { get; set; }

        [Parameter]
        public EventCallback<TItem> OnItemClick { get; set; }

        protected long TotalCount { get; set; }

        protected int PageIndex { get; set; } = 1;

        protected int PageItems { get; set; } = QueryPageOptions.DefaultPageItems;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            // 初始化每页显示数量
            if (Pageable)
            {
                PageItems = PageItemsSource?.FirstOrDefault() ?? QueryPageOptions.DefaultPageItems;

                if (Items != null) throw new InvalidOperationException($"Please set {nameof(OnQueryAsync)} instead set {nameof(Items)} property when {nameof(Pageable)} be set True.");
            }

            // 如果未设置 Items 数据源 自动执行查询方法
            if (Items == null)
            {
                await QueryData();
                Items ??= Array.Empty<TItem>();
            }
        }

        public Task ReloadDataAsync()
        {
            PageIndex = 1;

            return QueryAsync();
        }
        
        //private async Task ReloadAndInvokeChangeAsync()
        //{
        //    var queryModel = this.Reload();
        //    if (OnChange.HasDelegate)
        //    {
        //        await OnChange.InvokeAsync(queryModel);
        //    }

        //    await Task.CompletedTask;
        //}

        //private ListQueryModel<TItem> Reload()
        //{
        //    var queryModel = new ListQueryModel<TItem>(PageIndex, PageSize);
            
        //    if (ServerSide)
        //    {
        //        _showItems = _dataSource;
        //    }
        //    else
        //    {
        //        if (_dataSource != null)
        //        {
        //            var query = DataSource.AsQueryable();

        //            query = query.Skip((PageIndex - 1) * PageSize).Take(PageSize);
        //            queryModel.SetQueryableLambda(query);
                    
        //            _showItems = query;
        //        }
        //    }

        //    StateHasChanged();

        //    return queryModel;
        //}

        protected async Task OnPageClick(int pageIndex, int pageItems)
        {
            if (pageIndex != PageIndex)
            {
                PageIndex = pageIndex;
                PageItems = pageItems;
                await QueryAsync();
            }
        }

        protected async Task OnPageItemsChanged(int pageItems)
        {
            PageIndex = 1;
            PageItems = pageItems;
            await QueryAsync();
        }

        public async Task QueryAsync()
        {
            await QueryData();
            StateHasChanged();
        }

        protected async Task QueryData()
        {
            QueryData<TItem>? queryData = null;
            if (OnQueryAsync != null)
            {
                queryData = await OnQueryAsync(new QueryPageOptions()
                {
                    PageIndex = PageIndex,
                    PageItems = PageItems,
                });
            }
            if (queryData != null)
            {
                Items = queryData.Items;
                TotalCount = queryData.TotalCount;
            }
        }
        protected void OnClick(TItem item)
        {
            if (OnItemClick.HasDelegate) 
                OnItemClick.InvokeAsync(item);
        }
    }
}
