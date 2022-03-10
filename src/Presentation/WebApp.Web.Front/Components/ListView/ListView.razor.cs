// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace WebApp.Web.Front.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public sealed partial class ListView<TItem> where TItem : class, new()
    {
        private ElementReference _refLoadMoreScroll;
    }
}
