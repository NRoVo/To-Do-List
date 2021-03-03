using System.Collections.Generic;

namespace ToDoList.Core.Models
{
    public class PagedResult<T>
    {
        public List<T> Items { get; }
        public int Count { get; }

        public PagedResult(List<T> items, int count)
        {
            Items = items;
            Count = count;
        }
    }
}