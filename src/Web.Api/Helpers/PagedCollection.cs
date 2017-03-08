namespace Server.Api.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Server.Api.ViewModels;

    public class PagedCollection
    {
        public PagedCollectionViewModel<T> GetPagedCollection<T>(IEnumerable<T> items, int page, int pageSize)
        {
            var enumerable = items as IList<T> ?? items.ToList();
            var totalCount = enumerable.Count;

            var paged = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            return new PagedCollectionViewModel<T>
                                {
                                    Page = page,
                                    TotalCount = totalCount,
                                    TotalPages = (int)Math.Ceiling((decimal)totalCount / pageSize),
                                    Items = paged,
                                };
        }
    }
}