// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace WebApp.Web.Front.Components
{
    /// <summary>
    /// 查询条件实体类
    /// </summary>
    public class QueryPageOptions
    {
        /// <summary>
        /// 每页数据数量 默认 10 行
        /// </summary>
        internal const int DefaultPageItems = 10;

        /// <summary>
        /// 获得/设置 查询关键字
        /// </summary>
        public string SearchText { get; set; }
        
        /// <summary>
        /// 获得/设置 排序字段名称
        /// </summary>
        public string SortName { get; set; }
        
        /// <summary>
        /// 获得/设置 搜索条件绑定模型
        /// </summary>
        public object SearchModel { get; set; }

        /// <summary>
        /// 获得/设置 当前页码 首页为 第一页
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 获得/设置 每页条目数量
        /// </summary>
        public int PageItems { get; set; } = DefaultPageItems;

        /// <summary>
        /// 获得/设置 是否是分页查询 默认为 false
        /// </summary>
        public bool IsPage { get; set; }
    }
}
