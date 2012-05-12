using System.Collections.Generic;
using System.Linq;

namespace Backend
{
    public class PersistenceManager 
    {
        public void Add(List<Article> list)
        {
            ArticleDataContext.Current.Articles.InsertAllOnSubmit(list);
            ArticleDataContext.Current.SubmitChanges();
        }

        public List<Article> Load()
        {
            var query = from item in ArticleDataContext.Current.Articles
                        orderby item.ID descending 
                        select item;

            return query.ToList();
        }

        public Article Get(int id)
        {
            var query = from item in ArticleDataContext.Current.Articles
                        where item.ID == id
                        select item;
            return query.First();
        }

        public void DeleteAll()
        {
            ArticleDataContext.Current.Articles.DeleteAllOnSubmit(this.Load());
            ArticleDataContext.Current.SubmitChanges();
        }
    }
}
