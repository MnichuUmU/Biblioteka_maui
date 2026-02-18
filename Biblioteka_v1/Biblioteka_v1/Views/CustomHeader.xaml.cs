namespace Biblioteka_v1.Views;

public partial class CustomHeader : ContentView
{
	public CustomHeader()
	{
		InitializeComponent();
        if(Globals._account.Admin == true)
        {
            HeaderAdminPanel.IsVisible = true;
        }
        else
        {
            HeaderAdminPanel.IsVisible = false;
        }



	}

    private async void HeaderBooks_Clicked( object sender , EventArgs e )
    {
        if(Shell.Current.CurrentPage is Books) return;
        await Shell.Current.GoToAsync(nameof(Books));
    }

    private async void HeaderAddBooks_Clicked( object sender , EventArgs e )
    {
        if(Shell.Current.CurrentPage is AddBooks) return;
        await Shell.Current.GoToAsync(nameof(AddBooks));
    }

    private void HeaderAdminPanel_Clicked( object sender , EventArgs e )
    {
        ;
    }

    private async void HeaderRentedRn_Clicked( object sender , EventArgs e )
    {
        if(Shell.Current.CurrentPage is Rented) return;
        await Shell.Current.GoToAsync(nameof(Rented));
    }
}