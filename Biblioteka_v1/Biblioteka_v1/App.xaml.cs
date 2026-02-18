namespace Biblioteka_v1
{
    public partial class App : Application
    {
        public App(MainPage mainPage)
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

    }
}