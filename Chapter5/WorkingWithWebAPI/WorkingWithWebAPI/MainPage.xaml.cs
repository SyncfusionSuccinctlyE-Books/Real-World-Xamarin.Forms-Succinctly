using System;
using WorkingWithWebAPI.ViewModel;
using Xamarin.Forms;

namespace WorkingWithWebAPI
{
    public partial class MainPage : ContentPage
    {
        private BookViewModel ViewModel { get; set; }
        public MainPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<BookViewModel>(this, "BookSaved", BookSavedAction);
            MessagingCenter.Subscribe<BookViewModel>(this, "BookDeleted", BookDeletedAction);
            MessagingCenter.Subscribe<BookViewModel>(this, "ServerError", ServerErrorAction);
            ViewModel = new BookViewModel();
            BindingContext = ViewModel;
        }

        private async void BookDeletedAction(BookViewModel obj)
        {
            await DisplayAlert("Deleted", "The specified book was deleted", "OK");
        }

        private async void ServerErrorAction(BookViewModel obj)
        {
            await DisplayAlert("Error", "An error has occurred", "OK");
            LayoutRoot.BackgroundColor = Color.White;
            NewBookGrid.IsVisible = false;
        }

        private void BookSavedAction(BookViewModel obj)
        {
            LayoutRoot.BackgroundColor = Color.White;
            NewBookGrid.IsVisible = false;
        }

        private void AddBookButton_Clicked(object sender, EventArgs e)
        {
            LayoutRoot.BackgroundColor = Color.LightGray;
            NewBookGrid.IsVisible = true;
            ViewModel.NewBook = new Model.Book();
        }
    }
}
