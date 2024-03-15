using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMedicinePageFlyout : ContentPage
    {
        public ListView ListView;

        public AddMedicinePageFlyout()
        {
            InitializeComponent();

            BindingContext = new AddMedicinePageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class AddMedicinePageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<AddMedicinePageFlyoutMenuItem> MenuItems { get; set; }

            public AddMedicinePageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<AddMedicinePageFlyoutMenuItem>(new[]
                {
                    new AddMedicinePageFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new AddMedicinePageFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new AddMedicinePageFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new AddMedicinePageFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new AddMedicinePageFlyoutMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}