using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Utils.Paging;
using WebApp.Api.Domain.Common;

namespace WebApp.Api.Persistence.Extensions
{
    public static class AsyncIQueryableExtensions
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="getOnlyTotalCount">A value in indicating whether you want to load only total number of records. Set to "true" if you don't want to load data from database</param>
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, 
            int pageIndex, int pageSize, bool getOnlyTotalCount = false) where T : EntityBase
        {
            if (source == null)
                return new PagedList<T>(new List<T>(), pageIndex, pageSize);

            //min allowed page size is 1
            pageSize = Math.Max(pageSize, 1);

            var count = await source.CountAsync();

            var data = new List<T>();

            if (!getOnlyTotalCount)
                data.AddRange(await source.Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking().ToListAsync());

            return new PagedList<T>(data, pageIndex, pageSize, count);
        }
    }
}
