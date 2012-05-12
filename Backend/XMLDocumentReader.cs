using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Backend
{
    public class XmlDocumentReader
    {
        public List<Article> ParseXml(string rssXml)
        {
            if (string.IsNullOrEmpty(rssXml)) return null;
            var document = XDocument.Parse(rssXml);
            var query = from item in document.Descendants("item")
                        select new Article
                                   {
                                       Title = item.Descendants("title").First().Value,
                                       Description = item.Descendants("description").First().Value,
                                       Link = item.Descendants("link").First().Value,
                                       PubDate = item.Descendants("pubDate").First().Value,
                                   };
            return query.ToList();
        }
    }
}
