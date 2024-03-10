namespace BoardGameBrowserAPI.Models
{
    public class QueryParameters
    {
        private int _pageSize = 25;
        public int StartIndex { get; set; }
        public int PageIndex { get; set; }
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
    }
 }
