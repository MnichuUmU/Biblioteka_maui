using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Biblioteka_v1.Views;

public partial class Books : ContentPage
{
    private readonly LocalDbService _dbService;
    public Books( LocalDbService dbService )
    {
        InitializeComponent();
        _dbService = dbService;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            var books = await _dbService.GetKsiakiByAvaible();
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
        Globals._book = await _dbService.LookForKsiazka(book.Tytle, book.Author);
        ((ListView)sender ).SelectedItem = null;
    }

    public async void OnBlockerTapped( object sender , EventArgs e )
    {
        popUp.IsVisible = false;
        BackgroundBlocker.IsVisible = false; 
        try
        {
            var books = await _dbService.GetKsiakiByAvaible();
            Console.WriteLine($"DEBUG: Pobrano {books.Count} ksi¹¿ek");
            LView.ItemsSource = books;
            //LView.ItemsSource = await _dbService.GetKsiaki();
        }
        catch(Exception ex)
        {
            await DisplayAlert("Error" , $"Could not load books: {ex.Message}" , "OK");
        }
    }

    private async void btn_search_Clicked( object sender , EventArgs e )
    {
        var foundResults = await _dbService.SearchKsiazki(ent_search.Text);
        LView.ItemsSource = foundResults;
    }
}