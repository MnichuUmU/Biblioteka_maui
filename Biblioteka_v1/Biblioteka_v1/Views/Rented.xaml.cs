namespace Biblioteka_v1.Views;

public partial class Rented : ContentPage
{
    private readonly LocalDbService _dbService;
    public Rented( LocalDbService dbService )
    {
        InitializeComponent();
        _dbService = dbService;
    }

   
 protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            var books = await _dbService.GetKsiakiByUnavaible();
            Console.WriteLine($"DEBUG: Pobrano {books.Count} ksi¹¿ek");
            LView.ItemsSource = books;
            //LView.ItemsSource = await _dbService.GetKsiaki();
        }
        catch(Exception ex)
        {
            await DisplayAlert("Error" , $"Could not load books: {ex.Message}" , "OK");
        }
    }

    private async void LView_ItemTapped( object sender , ItemTappedEventArgs e )
    {
        var book = (Ksiazka)e.Item;

        BackgroundBlocker.IsVisible = true;
        popUp.IsVisible = true;
        popUp.UpdatePageData(book);
        Globals._book = await _dbService.LookForKsiazka(book.Tytle , book.Author);
        ( (ListView)sender ).SelectedItem = null;
    }

    private async void OnBlockerTapped( object sender , EventArgs e )
    {
        popUp.IsVisible = false;
        BackgroundBlocker.IsVisible = false;
        try
        {
            var books = await _dbService.GetKsiakiByUnavaible();
            Console.WriteLine($"DEBUG: Pobrano {books.Count} ksi¹¿ek");
            LView.ItemsSource = books;
            //LView.ItemsSource = await _dbService.GetKsiaki();
        }
        catch(Exception ex)
        {
            await DisplayAlert("Error" , $"Could not load books: {ex.Message}" , "OK");
        }
    }
}