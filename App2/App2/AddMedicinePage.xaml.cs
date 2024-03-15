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
    public partial class AddMedicinePage : ContentPage
    {
        public event EventHandler<Medicine> MedicineAdded;

        public AddMedicinePage()
        {
            InitializeComponent();
           
        }
     private void SaveMedicine()
{
   

    // Проверяем, что введенная дата не превышает текущую дату
    if (Convert.ToDateTime(expiryDateEntry.Text) >= DateTime.Now)
    {
        // Создание нового медикамента на основе данных из полей ввода
        Medicine newMedicine = new Medicine
        {
            Id = GenerateUniqueId(), // Генерация уникального ID (реализация зависит от ваших требований)
            Name = nameEntry.Text,
            Tradename = tradenameEntry.Text,
            Manufacturer = manufacturerEntry.Text,
            Image = imageEntry.Text,
            Price = Convert.ToDecimal(priceEntry.Text),
            Stock = Convert.ToInt32(stockEntry.Text),
            WarehouseId = Convert.ToInt32(warehouseIdEntry.Text),
            // Установка срока годности
            ExpiryDate = Convert.ToDateTime(expiryDateEntry.Text)
        };

        // Вызов события сохранения медикамента
        MedicineAdded?.Invoke(this, newMedicine);
    }
    else
    {
        // Выводим сообщение об ошибке
        DisplayAlert("Ошибка", "Дата истечения срока годности не может быть позже текущей даты", "OK");
    }
}

        

        // Метод для генерации уникального ID (реализация зависит от ваших требований)
        private int GenerateUniqueId()
        {
           Random random = new Random();
            return random.Next();
        }
    
    private void Button_Clicked(object sender, EventArgs e)
        {
        SaveMedicine();
        Navigation.PopAsync();
    }
    }
}