using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using WorkingWithWebAPI.Model;
using WorkingWithWebAPI.Services;
using Xamarin.Forms;

namespace WorkingWithWebAPI.ViewModel
{
    public class BookViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get
            {
                return _books;
            }
            set
            {
                _books = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Books)));
            }
        }

        private Book _newBook;
        public Book NewBook
        {
            get { return _newBook; }
            set
            {
                _newBook = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewBook)));
            }
        }

        private Book _selectedBook;
        public Book SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedBook)));
            }
        }

        public BookViewModel()
        {
            Books = new ObservableCollection<Book>();
        }

        public Command LoadBooksCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await LoadBooksAsync();
                });
            }
        }

        public Command AddBookCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (NewBook != null)
                    {
                        await AddBookAsync();
                    }
                });
            }
        }

        public Command DeleteBookCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (SelectedBook != null)
                    {
                        // *** REMEMBER TO ASK CONFIRMATION TO THE USER BEFORE DELETING! ***
                        // *** INFORM THE USER THIS ACTION IS IRREVERSIBLE ***
                        await DeleteBookAsync();
                    }
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async Task LoadBooksAsync()
        {
            string url = "https://yourdomainname.azurewebsites.net/api/books";
            var result = await WebApiService.GetDataAsync(url);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    string resultString = 
                        await result.Content.ReadAsStringAsync();

                    var deserialized = 
                        JsonConvert.DeserializeObject<List<Book>>(resultString);

                    Books = new ObservableCollection<Book>(deserialized);
                    return;
                default:
                    MessagingCenter.
                        Send(this, "ServerError", 
                        $"{result.StatusCode} {result.ReasonPhrase}");
                    return;
            }
        }

        private async Task AddBookAsync()
        {
            if (NewBook != null)
            {
                string url = "https://yourdomainname.azurewebsites.net/api/books";
                var result = await WebApiService.WriteDataAsync(NewBook, url);

                switch (result.StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.Created:
                        string resultString = await result.Content.ReadAsStringAsync();

                        var deserialized = JsonConvert.DeserializeObject<Book>(resultString);

                        Books.Add(deserialized);
                        MessagingCenter.Send(this, "BookSaved");
                        return;
                    default:
                        MessagingCenter.
                            Send(this, "ServerError", $"{result.StatusCode} {result.ReasonPhrase}");
                        return;
                }
            }
        }

        private async Task DeleteBookAsync()
        {
            string url =
                "https://yourdomainname.azurewebsites.net/api/books";
            var result = 
                await WebApiService.DeleteDataAsync(url, SelectedBook.Id);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    string resultString = await result.Content.ReadAsStringAsync();

                    // Do anything you need with the deleted object...
                    Book deserializedBook = 
                        JsonConvert.DeserializeObject<Book>(resultString);

                    Books.Remove(SelectedBook);

                    MessagingCenter.Send(this, "BookDeleted");
                    return;
                default:
                    MessagingCenter.
                        Send(this, "ServerError", 
                        $"{result.StatusCode} {result.ReasonPhrase}");
                    return;
            }
        }
    }
}
