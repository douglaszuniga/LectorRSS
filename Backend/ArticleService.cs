using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using Microsoft.Phone.Net.NetworkInformation;

namespace Backend
{
    public class ArticleService : INotifyPropertyChanged
    {
        private ObservableCollection<Article> _collection;
        public ObservableCollection<Article> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Collection"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly PersistenceManager _persistenceManager;
        private const string UriSource = "http://www.gamingcr.com/forums/rss/forums/1-gamingcr-noticias/";
        public void Retrieve()
        {
            var request = WebRequest.Create(new Uri(UriSource, UriKind.RelativeOrAbsolute));
            request.BeginGetResponse(ar =>
                                         {
                                             string stringResult = null;
                                             try
                                             {
                                                 var response = request.EndGetResponse(ar);
                                                 var reader = new StreamReader(response.GetResponseStream());
                                                 stringResult = reader.ReadToEnd();
                                                 reader.Close();
                                             }
                                             catch (Exception ex)
                                             {
                                                Console.WriteLine(ex.Message);
                                             }

                                             UpdateArticles(stringResult);
                                         }, null);
        }

        private void UpdateArticles(string stringResult)
        {
            try
            {
                var list = ParseRss(stringResult);
                //_persistenceManager.DeleteAll();
                _persistenceManager.Add(list);
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                                                                             {
                                                                                 Collection = new ObservableCollection<Article>(_persistenceManager.Load());
                                                                             });
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private List<Article> ParseRss(string stringResult)
        {
            var xmlDocumentReader = new XmlDocumentReader();
            return xmlDocumentReader.ParseXml(stringResult);
        }

        public ArticleService()
        {
            _persistenceManager = new PersistenceManager();

            Collection = new ObservableCollection<Article>(_persistenceManager.Load());

            if (IsDataRetrieveEnabled())
            {
                Retrieve();
            }
        }

        private static bool IsDataRetrieveEnabled()
        {
            return DeviceNetworkInformation.IsNetworkAvailable;
        }
    }
}
