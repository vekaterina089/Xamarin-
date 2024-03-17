using System;
using Xamarin.Forms;

namespace App2
{
    public partial class Page1 : ContentPage
    {
        public int Quantity { get; set; }
        public string Reason { get; set; }

        // Создаем событие для передачи информации о списании
        public event EventHandler<Withdrawal> MedicineWithdrawn;
        public Medicine selectedMedicine;
        public Page1(Medicine selectedMedicine)
        {
            InitializeComponent();
            this.selectedMedicine = selectedMedicine;
        }
        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            if (int.TryParse(quantityEntry.Text, out int quantity))
            {
                if (quantity <= selectedMedicine.Stock)
                {
                    // Создаем объект списка списания
                    Withdrawal withdrawal = new Withdrawal
                    {
                        MedicineId = selectedMedicine.Id,
                        Quantity = quantity,
                        Reason = reasonEntry.Text
                    };

                    // Генерируем событие для передачи информации о списании обратно на главную страницу
                    MedicineWithdrawn?.Invoke(this, withdrawal);

                    // Закрываем страницу списания
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Ошибка", "Введенное количество больше, чем есть в наличии", "OK");
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите корректное количество", "OK");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            // Закрываем страницу без выполнения списания
            await Navigation.PopAsync();
        }

    }
}
