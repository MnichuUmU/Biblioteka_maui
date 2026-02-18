using System.Threading.Tasks;

namespace Biblioteka_v1.Views;
public partial class BookPopUp : ContentView
{
    private readonly LocalDbService _dbService;
    public BookPopUp( )
	{

		InitializeComponent();
        _dbService = Application.Current.Handler.MauiContext.Services.GetService<LocalDbService>();
    }

	public void UpdatePageData(Ksiazka book)
	{
        btn_rent.IsVisible = false;
        btn_return.IsVisible = false;
        if(book.Stan == "tak")
		{
			btn_rent.IsVisible = true;
		}
		else
		{
            btn_return.IsVisible = true;
        }

        lbl_tytle.Text = book.Tytle;
        lbl_autor.Text = book.Author;
        lbl_releaseDate.Text = book.Date;
        lbl_tags.Text = book.Tags;
        lbl_stan.Text = book.Stan;

    }

    private async void btn_rent_Clicked( object sender , EventArgs e )
    {
        Globals._book.Stan = "nie";
        await _dbService.UpdateKsiazka(Globals._book, Globals._account.Id);
        UpdatePageData(Globals._book);
    }

    private async void btn_return_Clicked( object sender , EventArgs e )
    {
        Globals._book.Stan = "tak";
        await _dbService.UpdateKsiazka(Globals._book , Globals._account.Id);
        UpdatePageData(Globals._book);

    }
}