using Biblioteka_v1.Views;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Threading.Tasks;

namespace Biblioteka_v1
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        bool LogOrSing = true;
        public MainPage( LocalDbService dbService )
        {
            InitializeComponent();
            _dbService = dbService;
            //Task.Run(async () => listView.ItemsSource = await _dbService.GetKsiaki();

        }


        //////
        /// <summary>
        /// CO MAM
        /// - wyświetlanie książek
        /// - zmiena z książki do dodaj książki i vice versa
        /// - baze danyuch
        /// - początkowe wartości
        /// - dodawaniek  kont i logowanie
        /// - wyszukiwanie
        /// - dodawanie książek
        /// - wypożycz / zwróć zalerznie od stanu
        /// - zakłądka Wyporzyczone
        /// - data wyporzczenia
        /// </summary>
        /// /// <summary>
        /// CO TRZEBA
        /// - historia wyporzyczeń (per użytkownik)(w admin panel)
        /// - filtrowanie
        /// - Kategorie
        /// </summary>


        private void LogOrSingIn_Clicked( object sender , EventArgs e )
        {
            switch(LogOrSing)
            {
                case true:
                    LogIn.IsVisible = true;
                    SingIn.IsVisible = false;
                    LogOrSing = false;
                    break;
                case false:
                    LogIn.IsVisible = false;
                    SingIn.IsVisible = true;
                    LogOrSing = true;
                    break;
            }

        }
        private async void LI_btnLogIn_Clicked( object sender , EventArgs e )
        {

           if(!string.IsNullOrEmpty(LI_mail.Text) && !string.IsNullOrEmpty(LI_pass.Text))
            {
                var currentAcc = await _dbService.LookForAccount(LI_mail.Text , LI_pass.Text);

                if(currentAcc != null)
                {
                    Globals._account = currentAcc;
                    await Shell.Current.GoToAsync(nameof(Books));
                }
                else
                {
                    await DisplayAlert("Błędne dane." , "Takie konto nie istnieje" , "OK");
                }
            }
           else
            {
                await DisplayAlert("Błędne dane." , "Mail lub hasło nie mogą być puste." , "OK");
            }
        }

        private async void SI_btnSignIn_Clicked( object sender , EventArgs e )
        {
            if(!string.IsNullOrEmpty(SI_fname.Text) && !string.IsNullOrEmpty(SI_Lname.Text))
            {
                var existingUser = await _dbService.GetAccountByMail(SI_mail.Text);
                if(existingUser != null)
                {
                    await DisplayAlert("Błąd" , "Konto z tym mailem już istnieje!" , "OK");
                    return;
                }

                try
                {
                    Globals._account = new Accounts
                    {
                        FName = SI_fname.Text ,
                        LName = SI_Lname.Text ,
                        Mail = SI_mail.Text ,
                        Password = SI_pass.Text ,
                        Admin = false
                    };
                    await _dbService.CreateAcc(Globals._account);
                    
                    await Shell.Current.GoToAsync(nameof(Books));
                }
                catch(Exception ex)
                {
                    await DisplayAlert("Błąd Bazy", "Nie udało się utowrzyć konta: " + ex.Message, "OK");
                }

            }
            else {
                await DisplayAlert("Błędne dane." , "Nalerzy podać imie i nazwisko." , "OK");
            }

        }

    }
}
