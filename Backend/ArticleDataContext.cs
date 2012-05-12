using System.Data.Linq;

namespace Backend
{
    public class ArticleDataContext : DataContext
    {
        public Table<Article> Articles;
        public ArticleDataContext(string connectionString)
            : base(connectionString)
        {
            
        }

        static ArticleDataContext _dataContext;
        public static ArticleDataContext Current
        {
            get
            {
                if (_dataContext == null)
                {
                    _dataContext = new ArticleDataContext("isostore:/fayerwayerRss.sdf");
                    if (!_dataContext.DatabaseExists())
                    {
                        _dataContext.CreateDatabase();
                    }
                }
                return _dataContext;
            }
        }
    }
}
