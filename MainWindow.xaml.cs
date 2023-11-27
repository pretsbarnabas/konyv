using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace konyv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Book> books;
        public MainWindow()
        {
            InitializeComponent();
            ReadDatabase();
            AddLanguageSections();
        }

        private void AddLanguageSections()
        {
            var filteredlist = new List<string>();
            foreach (var item in books)
            {
                if (!filteredlist.Contains(item.Language))
                {
                    filteredlist.Add(item.Language);
                }
            }
            cbox_select.ItemsSource = filteredlist;
        }

        private void ReadDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection("books.db"))
            {
                connection.CreateTable<Book>();
                books = connection.Table<Book>().ToList();
                Debug.WriteLine(books[0].Title);
            }
        }

        private void cbox_select_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sp_listedBooks.Children.Clear();
            string language = (sender as ComboBox).SelectedItem as string;
            var filteredList = new List<Book>();
            foreach (var item in books)
            {
                if(item.Language==language)
                {
                    filteredList.Add(item);
                }
            }
            foreach (var item in filteredList)
            {
                Listelement listelement = new Listelement();
                listelement.author.Content = item.Author;
                listelement.title.Content = item.Title;
                listelement.AddHandler(Listelement.MouseDownEvent, new RoutedEventHandler(ListedBookClick));
                sp_listedBooks.Children.Add(listelement);
            }
        }
        private void ListedBookClick(object sender, RoutedEventArgs e)
        {
            string title = (sender as Listelement).title.Content.ToString();
            Book selectedBook = new();
            foreach (var item in books)
            {
                if(item.Title == title)
                {
                    selectedBook = item;
                    break;
                }
            }
            bookinfo.info_author.Content = selectedBook.Author;
            bookinfo.info_title.Content = selectedBook.Title;
            bookinfo.info_country.Content = selectedBook.Country;
            bookinfo.info_realyear.Content = selectedBook.RealYears;
            bookinfo.info_language.Content = selectedBook.Language;
            if (selectedBook.WikipediaLink!= "")
            {
                bookinfo.info_link.Visibility = Visibility.Visible;
                bookinfo.HL_wiki.NavigateUri = new Uri(selectedBook.WikipediaLink);
            }
            else
            {
                bookinfo.info_link.Visibility = Visibility.Hidden;
            }
            book_image.Source = new BitmapImage(new Uri(@$"/images/{selectedBook.ImageName}",UriKind.Relative));
        }


    }
}
