using Biblioteka_v1.Views;

namespace Biblioteka_v1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Books) , typeof(Books));
            Routing.RegisterRoute(nameof(AddBooks) , typeof(AddBooks));
            Routing.RegisterRoute(nameof(Rented) , typeof(Rented));
        }
    }
}
