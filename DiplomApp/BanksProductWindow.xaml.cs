using System;
using System.Collections.Generic;
using System.Windows;

namespace DiplomApp
{
    public partial class BanksProductWindow : Window
    {
        DiplomApp.DiplomDBEntities entities;
        public BanksProductWindow()
        {
            InitializeComponent();
            entities = new DiplomDBEntities();
        }
        private void ActivateAllButton()
        {
            DebitCardProductButton.IsEnabled = true;
            CreditCardProductButton.IsEnabled = true;
            CreditProductButton.IsEnabled = true;
            DepositProductButton.IsEnabled = true;
        }
        private void ShowDebtCardProduct()
        {
            entities = new DiplomDBEntities();
            productListBox.ItemsSource = BankProductAdapter.ConvertToIEnumerable(entities.DebtCardProduct);
            ActivateAllButton();
            DebitCardProductButton.IsEnabled = false;
        }
        private void ShowCreditCardProduct()
        {
            entities = new DiplomDBEntities();
            productListBox.ItemsSource = BankProductAdapter.ConvertToIEnumerable(entities.CreditCardProduct);
            ActivateAllButton();
            CreditCardProductButton.IsEnabled = false;
        }
        private void ShowCreditProduct()
        {
            entities = new DiplomDBEntities();
            productListBox.ItemsSource = BankProductAdapter.ConvertToIEnumerable(entities.CreditProduct);
            ActivateAllButton();
            CreditProductButton.IsEnabled = false;
        }
        private void ShowDepositProduct()
        {
            entities = new DiplomDBEntities();
            productListBox.ItemsSource = BankProductAdapter.ConvertToIEnumerable(entities.DepositProduct);
            ActivateAllButton();
            DepositProductButton.IsEnabled = false;
        }
        private void ShowAddProductWindow()
        {
            AddEditProductWindow addEditProductWindow = null;
            IProductAdapter product = null;
            try
            {
                if (DebitCardProductButton.IsEnabled == false)
                    product = new DepositProductAdapter();
                else if (CreditCardProductButton.IsEnabled == false)
                    product = new CreditCardProductAdapter();
                else if (DepositProductButton.IsEnabled == false)
                    product = new DepositProductAdapter();
                else if (CreditProductButton.IsEnabled == false)
                    product = new CreditProductAdapter();

                if (product is null) throw new Exception("Не выбрана страница, в которую будете добавлять продукт");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            do
            {
                using (var trans = entities.Database.BeginTransaction())
                {
                    try
                    {
                        addEditProductWindow = new AddEditProductWindow(entities, ref product);

                        if (addEditProductWindow.ShowDialog() == true)
                        {
                            if (product is DebtCardProductAdapter debtCardProductAdapter)
                                entities.DebtCardProduct.Add(debtCardProductAdapter.DebtCardProduct);
                            else if (product is CreditCardProductAdapter creditCardProductAdapter)
                                entities.CreditCardProduct.Add(creditCardProductAdapter.CreditCardProduct);
                            else if (product is DepositProductAdapter depositProductAdapter)
                                entities.DepositProduct.Add(depositProductAdapter.DepositProduct);
                            else if (product is CreditProductAdapter creditProductAdapter)
                                entities.CreditProduct.Add(creditProductAdapter.CreditProduct);

                            entities.SaveChanges();
                            trans.Commit();
                            break;
                        }
                        else
                        {
                            trans.Rollback();
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        trans.Rollback();
                    }
                }
            } while (true);
            UpdateEntityList();
        }

        private void ShowEditProductWindow()
        {
            AddEditProductWindow addEditProductWindow = null;
            IProductAdapter product = null;
            try
            {
                if (productListBox.SelectedIndex == -1) throw new Exception("Выберите продукт для редактирования");

                product = (productListBox.SelectedItem as BankProductAdapter).Product;
                if (product is null) throw new Exception("Не выбрана страница, в которую будете добавлять продукт");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            do
            {
                using (var trans = entities.Database.BeginTransaction())
                {
                    try
                    {
                        addEditProductWindow = new AddEditProductWindow(entities, ref product);
                        if (addEditProductWindow.ShowDialog() == true)
                        {
                            entities.SaveChanges();
                            trans.Commit();
                            break;
                        }
                        else
                        {
                            trans.Rollback();
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        trans.Rollback();
                    }
                }
            } while (true);
            UpdateEntityList();
        }

        private void UpdateEntityList()
        {
            entities = new DiplomDBEntities();

            if (DepositProductButton.IsEnabled == false)
                ShowDepositProduct();
            else if (CreditCardProductButton.IsEnabled == false)
                ShowCreditCardProduct();
            else if (CreditProductButton.IsEnabled == false)
                ShowCreditProduct();
            else if (DebitCardProductButton.IsEnabled == false)
                ShowDebtCardProduct();
        }

        private void ChangeIsEnableProductState_Click(object sender, RoutedEventArgs e) => ChangeIsEnableProductState();
        private void ChangeIsEnableProductState()
        {
            object item = productListBox.SelectedItem;
            if (item is BankProductAdapter bankProductAdapter)
                bankProductAdapter.IsEnable = !bankProductAdapter.IsEnable;

            entities.SaveChanges();
            UpdateEntityList();
        }

        private void BackToMenu() => Close();
        private void DebitCardPage_Click(object sender, RoutedEventArgs e) => ShowDebtCardProduct();
        private void CreditCardPage_Click(object sender, RoutedEventArgs e) => ShowCreditCardProduct();
        private void CreditPage_Click(object sender, RoutedEventArgs e) => ShowCreditProduct();
        private void DepositPage_Click(object sender, RoutedEventArgs e) => ShowDepositProduct();
        private void CloseWindow_Click(object sender, RoutedEventArgs e) => BackToMenu();

        private void AddProductButton_Click(object sender, RoutedEventArgs e) => ShowAddProductWindow();

        private void EditProductButton_Click(object sender, RoutedEventArgs e) => ShowEditProductWindow();
        class BankProductAdapter
        {
            public IProductAdapter Product { get; set; }
            public BankProductAdapter(IProductAdapter product) => Product = product;

            public string Header => Product.Title;
            public string Description => Product.Description;
            public string ClientCount => Product.ClientCount + " продано";
            public string MoneyCirculation
            {
                get
                {
                    string result = "";
                    foreach (KeyValuePair<string, decimal> item in Product.MoneyCirculation)
                    {
                        result += $"{item.Value} {item.Key}\n";
                    }
                    return result;
                }
            }
            public string ActiveStatus => IsEnable == true ? "В продаже" : "Выведен из продажи";
            public bool IsEnable
            {
                get => Product.IsEnable;
                set => Product.IsEnable = value;
            }
            public static IEnumerable<BankProductAdapter> ConvertToIEnumerable(IEnumerable<DebtCardProduct> enumerableToAdapt)
            {
                List<BankProductAdapter> result = new List<BankProductAdapter>();
                foreach (DebtCardProduct item in enumerableToAdapt)
                    result.Add(new BankProductAdapter(new DebtCardProductAdapter(item)));
                return result;
            }
            public static IEnumerable<BankProductAdapter> ConvertToIEnumerable(IEnumerable<CreditCardProduct> enumerableToAdapt)
            {
                List<BankProductAdapter> result = new List<BankProductAdapter>();
                foreach (CreditCardProduct item in enumerableToAdapt)
                    result.Add(new BankProductAdapter(new CreditCardProductAdapter(item)));
                return result;
            }
            public static IEnumerable<BankProductAdapter> ConvertToIEnumerable(IEnumerable<CreditProduct> enumerableToAdapt)
            {
                List<BankProductAdapter> result = new List<BankProductAdapter>();
                foreach (CreditProduct item in enumerableToAdapt)
                    result.Add(new BankProductAdapter(new CreditProductAdapter(item)));
                return result;
            }
            public static IEnumerable<BankProductAdapter> ConvertToIEnumerable(IEnumerable<DepositProduct> enumerableToAdapt)
            {
                List<BankProductAdapter> result = new List<BankProductAdapter>();
                foreach (DepositProduct item in enumerableToAdapt)
                    result.Add(new BankProductAdapter(new DepositProductAdapter(item)));
                return result;
            }
        }
    }
}
