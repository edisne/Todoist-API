﻿namespace Todoist_API.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        
        private string _search = "";
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }

        public string Tag { get; set; } = "";
    }
}
