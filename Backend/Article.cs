using System.Data.Linq.Mapping;

namespace Backend
{
    [Table]
    public class Article
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int  ID { get; set; }

        private string _title;

        [Column]
        public string Title
        {
            get { return _title; }
            set { _title = System.Net.HttpUtility.HtmlDecode(value); }
        }

        [Column]
        public string Link { get; set; }

        private string _description;

        [Column]
        public string Description
        {
            get { return _description; }
            set { 
                _description = value;
                //SQL server CE max is 4000
                if (_description.Length > 4000)
                {
                    _description = _description.Substring(0, 3999);
                }
            }
        }

        [Column]
        public string PubDate { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
