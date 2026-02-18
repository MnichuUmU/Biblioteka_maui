using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;

namespace Biblioteka_v1.Views;

public partial class AddBooks : ContentPage
{
    private readonly LocalDbService _dbService;
    public AddBooks( LocalDbService dbService )
    {
        InitializeComponent();
        _dbService = dbService;
    }

    private async void btn_addBook_Clicked( object sender , EventArgs e )
    {
        string Tag = "";
        if(rb_adventure.IsChecked)
        {
            Tag = rb_adventure.Value.ToString();
        }
        else if(rb_krymial.IsChecked)
        {
            Tag = rb_krymial.Value.ToString();
        }
        else if(rb_fantasy.IsChecked)
        {
            Tag = rb_fantasy.Value.ToString();
        }


        Ksiazka newKsiazka = new Ksiazka
        {
            Tytle = Add_tytle.Text ,
            Author = Add_author.Text ,
            Tags = Tag,
            Date = Add_date.Text ,
            Stan = "tak"
        };

        await _dbService.CreateKsiazka(newKsiazka);
        await DisplayAlert("Sukces" , "Ksi¹¿ka dodana!" , "OK");
        await Shell.Current.GoToAsync(nameof(Books));
    } 
}
