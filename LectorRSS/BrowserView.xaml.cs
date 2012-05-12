using System;
using Backend;
using Microsoft.Phone.Controls;

namespace LectorRSS
{
    public partial class BrowserView : PhoneApplicationPage
    {
        public BrowserView()
        {
            InitializeComponent();
        }
        
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string aId;
            if (!NavigationContext.QueryString.TryGetValue("id", out aId)) return;
            var manager = new PersistenceManager();
            var selectedArticle = manager.Get(Convert.ToInt32(aId));
            if (selectedArticle != null)
            {
                wbArticle.NavigateToString(selectedArticle.Description);
            }
        }
    }
}