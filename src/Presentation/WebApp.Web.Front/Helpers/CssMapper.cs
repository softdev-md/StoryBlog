using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Web.Front.Helpers
{
    public class CssMapper
    {
        public string Class => AsString();

        internal string OriginalClass { get; set; }

        public string AsString()
        {
            return string.Join(" ", _map.Where(i => i.Value()).Select(i => i.Key()));
        }

        public override string ToString()
        {
            return AsString();
        }

        private readonly Dictionary<Func<string>, Func<bool>> _map = new Dictionary<Func<string>, Func<bool>>();

        public CssMapper Add(string name)
        {
            _map.Add(() => name, () => true);
            return this;
        }

        public CssMapper Get(Func<string> funcName)
        {
            _map.Add(funcName, () => true);
            return this;
        }

        public CssMapper GetIf(Func<string> funcName, Func<bool> func)
        {
            _map.Add(funcName, func);
            return this;
        }

        public CssMapper If(string name, Func<bool> func)
        {
            _map.Add(() => name, func);
            return this;
        }

        public CssMapper Clear()
        {
            _map.Clear();

            _map.Add(() => OriginalClass, () => !string.IsNullOrEmpty(OriginalClass));

            return this;
        }
    }
}
