using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoveMedicinePage : ContentPage
    {
        private List<Medicine> medicines;
        private List<Warehouses> warehouses;

        public MoveMedicinePage(List<Medicine> medicines, List<Warehouses> warehouses)
        {
            InitializeComponent();
            this.medicines = medicines;
            this.warehouses = warehouses;

            // Заполните Picker элементы списками медикаментов и складов
            foreach (var medicine in medicines)
            {
                medicinePicker.Items.Add(medicine.Name);
            }

            foreach (var warehouse in warehouses)
            {
                warehousePicker.Items.Add(warehouse.Name);
            }
        }

        private void MoveMedicineButton_Clicked(object sender, EventArgs e)
        {
            // Получите выбранный медикамент и склад
            var selectedMedicineName = medicinePicker.SelectedItem?.ToString();
            var selectedWarehouseName = warehousePicker.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedMedicineName) || string.IsNullOrEmpty(selectedWarehouseName))
            {
                DisplayAlert("Ошибка", "Пожалуйста, выберите медикамент и склад", "OK");
                return;
            }

            var mainPage = ((NavigationPage)Application.Current.MainPage).CurrentPage as MainPage;

         
            if (mainPage != null)
            {
                // Найдите медикамент и склад в списках
                var selectedMedicine = medicines.Find(m => m.Name == selectedMedicineName);
                var selectedWarehouse = warehouses.Find(w => w.Name == selectedWarehouseName);

                if (selectedMedicine != null && selectedWarehouse != null)
                {
                    // Обновите склад у медикамента
                    selectedMedicine.WarehouseId = selectedWarehouse.Id;

                    // Обновите интерфейс или список медикаментов на главной странице
                    mainPage.FilterByWarehouse(selectedWarehouse.Id);
                }
            }

            Navigation.PopAsync(); // Возвращаемся к предыдущей странице
        }
    }
}
