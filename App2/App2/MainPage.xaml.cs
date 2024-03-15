using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        public List<Medicine> medicines = new List<Medicine>();
        public List<Warehouses> warehouses = new List<Warehouses>();
        public List<Delivery> deliveries = new List<Delivery>();
        public MainPage()
        {
            InitializeComponent();

            // Добавляем склады
            warehouses.Add(new Warehouses() { Id = 1, Name = "Склад 1" });
            warehouses.Add(new Warehouses() { Id = 2, Name = "Склад 2" });
            warehouses.Add(new Warehouses() { Id = 3, Name = "Склад 3" });

            PopulateWarehousePicker();

            // Добавляем медикаменты
            medicines.Add(new Medicine()
            {
                Id = 2,
                Name = "Вольтарен 25мг/мл 3мл 5 шт. раствор для внутримышечного введения",
                Tradename = "Вольтарен",
                Manufacturer = "Новартис Фарма АГ",
                Image = "https://imgs.asna.ru/iblock/2d7/2d71cac199086932e4b68e6ae633eca8/100082.jpg",
                Price = 79,
                Stock = 41,
                WarehouseId = 2
            });

            medicines.Add(new Medicine()
            {
                Id = 3,
                Name = "Кальцекс 500мг 10 шт. таблетки татхимфарм",
                Tradename = "Кальцекс",
                Manufacturer = "Татхимфармпрепараты АО",
                Image = "https://imgs.asna.ru/iblock/177/177882ef988b42be05abd45dbb7d5fba/816f88b93afa5c096afbeec679ffd4c0.jpg",
                Price = 42,
                Stock = 69,
                WarehouseId = 3
            });

            medicineListView.ItemsSource = medicines;

        }
        private async void ReceiveDeliveryButton_Clicked(object sender, EventArgs e)
        {
            AddMedicinePage addMedicinePage = new AddMedicinePage();
            addMedicinePage.MedicineAdded += OnMedicineAdded; // Добавляем обработчик события
            await Navigation.PushAsync(addMedicinePage); // Используем тот же экземпляр AddMedicinePage
        }



        private void OnMedicineAdded(object sender, Medicine newMedicine)
        {
            // Добавление нового медикамента в список
            medicines.Add(newMedicine);

            // Обновление ListView
            medicineListView.ItemsSource = null; // Очистка источника данных ListView
            medicineListView.ItemsSource = medicines; // Обновление источника данных
        }


        private void PopulateWarehousePicker()
        {
            List<string> warehouseNames = new List<string>();

            // Добавить "Все склады" в начало списка
            warehouseNames.Add("Все склады");

            // Добавить имена складов
            foreach (var warehouse in warehouses)
            {
                warehouseNames.Add(warehouse.Name);
            }

            warehousePicker.ItemsSource = warehouseNames;
        }

        // Обработчик события изменения выбора склада
        private void WarehousePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedWarehouseName = warehousePicker.SelectedItem.ToString();
            if (selectedWarehouseName == "Все склады")
            {
                // Если выбрана опция "Все склады", показываем все медикаменты
                medicineListView.ItemsSource = medicines;
            }
            else
            {
                Warehouses selectedWarehouse = warehouses.FirstOrDefault(w => w.Name == selectedWarehouseName);
                if (selectedWarehouse != null)
                {
                    FilterByWarehouse(selectedWarehouse.Id);
                }
                else
                {
                    DisplayAlert("Ошибка", "Выбранный склад не существует", "OK");
                }
            }
        }

        // Пример фильтрации по складу
        public void FilterByWarehouse(int warehouseId)
        {
            var filteredMedicines = medicines.Where(m => m.WarehouseId == warehouseId).ToList();
            medicineListView.ItemsSource = filteredMedicines;
        }

        // Пример отображения информации о количестве препарата на всех складах
        public void DisplayStockInformation(int medicineId)
        {
            var medicine = medicines.FirstOrDefault(m => m.Id == medicineId);
            if (medicine != null)
            {
                DisplayAlert("Наличие на складах", $"Склад {medicine.WarehouseId}: {medicine.Stock} шт.", "OK");
            }
        }
      
    }

    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tradename { get; set; }
        public string Manufacturer { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int WarehouseId { get; set; }


        public DateTime ExpiryDate { get; set; }
       
    }

    public class Warehouses
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Delivery
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

}
