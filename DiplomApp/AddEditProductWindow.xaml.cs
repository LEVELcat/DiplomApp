using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DiplomApp
{
    public partial class AddEditProductWindow : Window
    {
        private AddEditProductAdapter addEditProductAdapter;
        bool isAdd;
        DiplomDBEntities entities;
        public AddEditProductWindow(DiplomDBEntities entities, ref IProductAdapter product)
        {
            InitializeComponent();
            if (product == null) DialogResult = false;
            this.entities = entities;
            addEditProductAdapter = new AddEditProductAdapter(product);
            if (addEditProductAdapter.Title == null)
            {
                isAdd = true;
                headerLabel.Content = "Добавление продукта:";
                addEditButton.Content = "Добавить";
            }
            else
            {
                isAdd = false;
                headerLabel.Content = "Изменение продукта:";
                addEditButton.Content = "Изменить";
            }
            headerLabel.Content += $" {addEditProductAdapter.ProductType.ToLower()}";
            if (product is DebtCardProductAdapter)
            {
                creditTypeLayer.Width = 0;
                debtRateLayer.Width = 0;
                NoDebtRateLayer.Width = 0;
            }
            else if (product is CreditCardProductAdapter)
            {
                creditTypeLayer.Width = 0;
            }
            else if (product is CreditProductAdapter)
            {
                NoDebtRateLayer.Width = 0;
            }
            else if (product is DepositProductAdapter)
            {
                NoDebtRateLayer.Width = 0;
                creditTypeLayer.Width = 0;
            }
            if (creditTypeLayer.Width != 0)
            {
                foreach(var type in entities.CreditType)
                    creditTypeBox.Items.Add(type);
            }
            if (isAdd == false)
            {
                titleBox.Text = addEditProductAdapter.Title;
                descriptionBox.Text = addEditProductAdapter.Description;
                isActiveBox.IsChecked = addEditProductAdapter.IsActive;
                if (creditTypeLayer.Width != 0)
                    creditTypeBox.SelectedItem = addEditProductAdapter.CreditType;
                if (NoDebtRateLayer.Width != 0)
                    NoDebtDayBox.Text = addEditProductAdapter.NoDebtDay;
                if (debtRateLayer.Width != 0)
                    debtRateBox.Text = addEditProductAdapter.DebtRate;
            }
        }
        private void Save()
        {
            try
            {
                addEditProductAdapter.Title = titleBox.Text;
                addEditProductAdapter.Description = descriptionBox.Text;
                addEditProductAdapter.IsActive = isActiveBox.IsChecked.Value;
                if (debtRateLayer.Width != 0)
                    if (decimal.TryParse(debtRateBox.Text, out decimal debtRate))
                        addEditProductAdapter.DebtRateNum = debtRate;
                    else throw new Exception("Данные в поле 'Ставка' не является числом");

                if (NoDebtRateLayer.Width != 0)
                    if (int.TryParse(NoDebtDayBox.Text, out int noDebtDay))
                        addEditProductAdapter.NoDebtDayNum = noDebtDay;
                    else throw new Exception("Данные в поле 'Беспроцентных дней' не является целым числом");

                if (creditTypeLayer.Width != 0)
                    if (creditTypeBox.SelectedItem is CreditType creditType)
                        addEditProductAdapter.CreditType = creditType;
                    else throw new Exception("Выбранный тип не являеться типом кредита\n" +
                                             "(поздравляю, вы нарвались на баг)");

                var errors = entities.GetValidationErrors();
                if (errors.ToList().Count == 0) 
                {
                    this.DialogResult = true;
                    return;
                }

                string message = "";
                foreach(var error in errors)
                    foreach (var exception in error.ValidationErrors)
                        message += exception.ErrorMessage + "\n";

                MessageBox.Show(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void Cancel() => DialogResult = false;  
        private void CancelButton_Click(object sender, RoutedEventArgs e) => Cancel();

        private void addEditButton_Click(object sender, RoutedEventArgs e) => Save();
        class AddEditProductAdapter
        {
            public IProductAdapter Product { get; set; }
            public AddEditProductAdapter(IProductAdapter product) => Product = product;

            public string Title
            {
                get { return Product.Title; }
                set { Product.Title = value; }
            }
            public string Description
            {
                get { return Product.Description; }
                set { Product.Description = value; }
            }
            public bool IsActive
            {
                get { return Product.IsEnable; }
                set { Product.IsEnable = value; }
            }
            public string DebtRate => Product.DebtRate.ToString();
            public decimal DebtRateNum
            {
                set => Product.DebtRate = value;
            }
            public string NoDebtDay => Product.NoDebtDay.ToString();
            public int NoDebtDayNum
            {
                set => Product.NoDebtDay = value;
            }
            public CreditType CreditType
            {
                get => Product.CreditType;
                set => Product.CreditType = value;
            }
            public string ProductType => Product.ProductType.ToString();
        }
    }
}
