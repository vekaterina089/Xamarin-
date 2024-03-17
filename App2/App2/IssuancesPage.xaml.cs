using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IssuancesPage : ContentPage
    {
        public IssuancesPage(List<Issuance> issuances)
        {
            InitializeComponent();
            issuancesListView.ItemsSource= issuances.ToArray();
        }
    }
}