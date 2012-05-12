using System;
using System.Windows.Controls;
using Backend;
using Microsoft.Phone.Controls;

namespace LectorRSS
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void LbArticlesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedArticle = lbArticles.SelectedItem as Article;
            if (selectedArticle != null)
            {
                var uri = "/BrowserView.xaml?id=" + selectedArticle.ID;
                NavigationService.Navigate(new Uri(uri, UriKind.RelativeOrAbsolute));
            }
        }
    }
}