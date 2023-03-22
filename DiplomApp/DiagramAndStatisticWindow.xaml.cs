using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
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
    public partial class DiagramAndStatisticWindow : Window
    {
        private DiplomApp.DiplomDBEntities entity;
        public DiagramAndStatisticWindow()
        {
            InitializeComponent();
            entity = new DiplomApp.DiplomDBEntities();
            HideAllLayer();
        }
        private void ShowMainLayer()
        {
            entity = new DiplomDBEntities();
            HideAllLayer();
            mainLayer.Visibility = Visibility.Visible;

            SeriesCollection seriesCollection = new SeriesCollection()
            {
                new PieSeries()
                {
                    Title = "Дебетовые карты",
                    Values = new ChartValues<ObservableValue> {new ObservableValue (entity.DebitСard.ToList().Count)},
                    DataLabels = true
                },
                new PieSeries()
                {
                    Title = "Кредитные карты",
                    Values = new ChartValues<ObservableValue> {new ObservableValue (entity.CreditCard.ToList().Count)},
                    DataLabels = true
                },
                new PieSeries()
                {
                    Title = "Кредиты",
                    Values = new ChartValues<ObservableValue> {new ObservableValue (entity.Credit.ToList().Count)},
                    DataLabels = true
                },
                new PieSeries()
                {
                    Title = "Вклады",
                    Values = new ChartValues<ObservableValue> {new ObservableValue (entity.Deposit.ToList().Count)},
                    DataLabels = true
                }
            };
            sellChart.Chart.Series = seriesCollection;
            sellChart.Chart.Update();


            seriesCollection = new SeriesCollection()
            {
                new PieSeries()
                {
                    Title = "На дебетовых картах",
                    Values = new ChartValues<ObservableValue> {new ObservableValue (Convert.ToDouble(entity.DebitСard.Where(x => x.Currency.Title == "Рубль").ToList().Sum(x => x.Balance)))},
                    DataLabels = true
                },
                new PieSeries()
                {
                    Title = "Задолженность по \nкредитным картам",
                    Values = new ChartValues<ObservableValue> {new ObservableValue (Convert.ToDouble(entity.CreditCard.Where(x => x.Currency.Title == "Рубль").ToList().Sum(x => x.CreditLimit - x.Balance > 0 ? x.CreditLimit - x.Balance : 0)))},
                    DataLabels = true
                },
                new PieSeries()
                {
                    Title = "Задолжность по кредитам",
                    Values = new ChartValues<ObservableValue> {new ObservableValue (Convert.ToDouble(entity.Credit.Where(x => x.Currency.Title == "Рубль").ToList().Sum(x => x.Debt)))},
                    DataLabels = true
                },
                new PieSeries()
                {
                    Title = "Во вкладах",
                    Values = new ChartValues<ObservableValue> {new ObservableValue (Convert.ToDouble(entity.Deposit.Where(x => x.Currency.Title == "Рубль").ToList().Sum(x => x.Deposit1)))},
                    DataLabels = true
                }
            };
            moneyChartRUB.Chart.Series = seriesCollection;
            moneyChartRUB.Chart.Update();

            seriesCollection = new SeriesCollection()
                {
                     new PieSeries()
                {
                    Title = "На дебетовых картах",
                    Values = new ChartValues<ObservableValue> {new ObservableValue (Convert.ToDouble(entity.DebitСard.Where(x => x.Currency.Title == "Евро").ToList().Sum(x => x.Balance)))},
                    DataLabels = true
                },
                new PieSeries()
                {
                    Title = "Задолженность по \nкредитным картам",
                    Values = new ChartValues<ObservableValue> {new ObservableValue (Convert.ToDouble(entity.CreditCard.Where(x => x.Currency.Title == "Евро").ToList().Sum(x => x.CreditLimit - x.Balance > 0 ? x.CreditLimit - x.Balance : 0)))},
                    DataLabels = true
                },
                new PieSeries()
                {
                    Title = "Задолжность по кредитам",
                    Values = new ChartValues<ObservableValue> {new ObservableValue (Convert.ToDouble(entity.Credit.Where(x => x.Currency.Title == "Евро").ToList().Sum(x => x.Debt)))},
                    DataLabels = true
                },
                new PieSeries()
                {
                    Title = "Во вкладах",
                    Values = new ChartValues<ObservableValue> {new ObservableValue (Convert.ToDouble(entity.Deposit.Where(x => x.Currency.Title == "Евро").ToList().Sum(x => x.Deposit1)))},
                    DataLabels = true
                }
                };
            moneyChartEUR.Chart.Series = seriesCollection;
            moneyChartEUR.Chart.Update();

            seriesCollection = new SeriesCollection()
                {
                     new PieSeries()
                {
                    Title = "На дебетовых картах",
                    Values = new ChartValues<ObservableValue> {new ObservableValue (Convert.ToDouble(entity.DebitСard.Where(x => x.Currency.Title == "Доллар").ToList().Sum(x => x.Balance)))},
                    DataLabels = true
                },
                new PieSeries()
                {
                    Title = "Задолженность по \nкредитным картам",
                    Values = new ChartValues<ObservableValue> {new ObservableValue (Convert.ToDouble(entity.CreditCard.Where(x => x.Currency.Title == "Доллар").ToList().Sum(x => x.CreditLimit - x.Balance > 0 ? x.CreditLimit - x.Balance : 0)))},
                    DataLabels = true
                },
                new PieSeries()
                {
                    Title = "Задолжность по кредитам",
                    Values = new ChartValues<ObservableValue> {new ObservableValue (Convert.ToDouble(entity.Credit.Where(x => x.Currency.Title == "Доллар").ToList().Sum(x => x.Debt)))},
                    DataLabels = true
                },
                new PieSeries()
                {
                    Title = "Во вкладах",
                    Values = new ChartValues<ObservableValue> {new ObservableValue (Convert.ToDouble(entity.Deposit.Where(x => x.Currency.Title == "Доллар").ToList().Sum(x => x.Deposit1)))},
                    DataLabels = true
                }
                };
            moneyChartUSD.Chart.Series = seriesCollection;
            moneyChartUSD.Chart.Update();
        }

        private void ShowDebtCardLayer()
        {
            entity = new DiplomDBEntities();
            HideAllLayer();
            debtCardLayer.Visibility = Visibility.Visible;

            SeriesCollection seriesCollection = new SeriesCollection();
            foreach (var a in entity.DebtCardProduct)
            {
                PieSeries pie = new PieSeries
                {
                    Title = a.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(a.DebitСard.Count) },
                    DataLabels = true
                };
                seriesCollection.Add(pie);
            }
            debtCardProductChart.Chart.Series = seriesCollection;
            debtCardProductChart.Chart.Update();

            seriesCollection = new SeriesCollection();
            foreach (var a in entity.DebtCardProduct)
            {
                PieSeries pie = new PieSeries
                {
                    Title = a.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(a.DebitСard.Where(x => x.Currency.Title == "Рубль").Sum(x => x.Balance))) },
                    DataLabels = true
                };
                seriesCollection.Add(pie);
            }
            debtCardProductChartRUB.Chart.Series = seriesCollection;
            debtCardProductChartRUB.Chart.Update();

            seriesCollection = new SeriesCollection();
            foreach (var a in entity.DebtCardProduct)
            {
                PieSeries pie = new PieSeries
                {
                    Title = a.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(a.DebitСard.Where(x => x.Currency.Title == "Евро").Sum(x => x.Balance))) },
                    DataLabels = true
                };
                seriesCollection.Add(pie);
            }
            debtCardProductChartEUR.Chart.Series = seriesCollection;
            debtCardProductChartEUR.Chart.Update();

            seriesCollection = new SeriesCollection();
            foreach (var a in entity.DebtCardProduct)
            {
                PieSeries pie = new PieSeries
                {
                    Title = a.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(a.DebitСard.Where(x => x.Currency.Title == "Доллар").Sum(x => x.Balance))) },
                    DataLabels = true
                };
                seriesCollection.Add(pie);
            }
            debtCardProductChartUSD.Chart.Series = seriesCollection;
            debtCardProductChartUSD.Chart.Update();
        }

        private void ShowCreditCardLayer()
        {
            entity = new DiplomDBEntities();
            HideAllLayer();
            creditCardLayer.Visibility = Visibility.Visible;

            SeriesCollection seriesCollection = new SeriesCollection();
            foreach (var a in entity.CreditCardProduct)
            {
                PieSeries pie = new PieSeries
                {
                    Title = a.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(a.CreditCard.Count) },
                    DataLabels = true
                };
                seriesCollection.Add(pie);
            }
            creditCardProductChart.Chart.Series = seriesCollection;
            creditCardProductChart.Chart.Update();

            seriesCollection = new SeriesCollection();
            foreach (var a in entity.CreditCardProduct)
            {
                PieSeries pie = new PieSeries
                {
                    Title = a.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(a.CreditCard.Where(x => x.Currency.Title == "Рубль")
                                                                                                                 .Sum(x => x.CreditLimit - x.Balance > 0 ? x.CreditLimit - x.Balance : 0 ))) },
                    DataLabels = true
                };
                seriesCollection.Add(pie);
            }
            creditCardProductChartRUB.Chart.Series = seriesCollection;
            creditCardProductChartRUB.Chart.Update();

            seriesCollection = new SeriesCollection();
            foreach (var a in entity.CreditCardProduct)
            {
                PieSeries pie = new PieSeries
                {
                    Title = a.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(a.CreditCard.Where(x => x.Currency.Title == "Евро")
                                                                                                                 .Sum(x => x.CreditLimit - x.Balance > 0 ? x.CreditLimit - x.Balance : 0 ))) },
                    DataLabels = true
                };
                seriesCollection.Add(pie);
            }
            creditCardProductChartEUR.Chart.Series = seriesCollection;
            creditCardProductChartEUR.Chart.Update();

            seriesCollection = new SeriesCollection();
            foreach (var a in entity.CreditCardProduct)
            {
                PieSeries pie = new PieSeries
                {
                    Title = a.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(a.CreditCard.Where(x => x.Currency.Title == "Доллар")
                                                                                                                 .Sum(x => x.CreditLimit - x.Balance > 0 ? x.CreditLimit - x.Balance : 0 ))) },
                    DataLabels = true
                };
                seriesCollection.Add(pie);
            }
            creditCardProductChartUSD.Chart.Series = seriesCollection;
            creditCardProductChartUSD.Chart.Update();
        }
        private void ShowCreditLayer()
        {
            entity = new DiplomDBEntities();
            HideAllLayer();
            creditLayer.Visibility = Visibility.Visible;

            SeriesCollection seriesCollection = new SeriesCollection();
            foreach (var a in entity.CreditProduct)
            {
                PieSeries pie = new PieSeries
                {
                    Title = a.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(a.Credit.Count) },
                    DataLabels = true
                };
                seriesCollection.Add(pie);
            }
            creditProductChart.Chart.Series = seriesCollection;
            creditProductChart.Chart.Update();

            seriesCollection = new SeriesCollection();
            foreach (var a in entity.CreditProduct)
            {
                PieSeries pie = new PieSeries
                {
                    Title = a.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(a.Credit.Where(x => x.Currency.Title == "Рубль").Sum(x => x.Debt))) },
                    DataLabels = true
                };
                seriesCollection.Add(pie);
            }
            creditProductChartRUB.Chart.Series = seriesCollection;
            creditProductChartRUB.Chart.Update();

            seriesCollection = new SeriesCollection();
            foreach (var a in entity.CreditProduct)
            {
                PieSeries pie = new PieSeries
                {
                    Title = a.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(a.Credit.Where(x => x.Currency.Title == "Евро").Sum(x => x.Debt))) },
                    DataLabels = true
                };
                seriesCollection.Add(pie);
            }
            creditProductChartEUR.Chart.Series = seriesCollection;
            creditProductChartEUR.Chart.Update();

            seriesCollection = new SeriesCollection();
            foreach (var a in entity.CreditProduct)
            {
                PieSeries pie = new PieSeries
                {
                    Title = a.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(a.Credit.Where(x => x.Currency.Title == "Доллар").Sum(x => x.Debt))) },
                    DataLabels = true
                };
                seriesCollection.Add(pie);
            }
            creditProductChartUSD.Chart.Series = seriesCollection;
            creditProductChartUSD.Chart.Update();
        }
        private void ShowDepositLayer()
        {
            entity = new DiplomDBEntities();
            HideAllLayer();
            depositLayer.Visibility = Visibility.Visible;

            SeriesCollection seriesCollection = new SeriesCollection();
            foreach (var a in entity.DepositProduct)
            {
                PieSeries pie = new PieSeries
                {
                    Title = a.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(a.Deposit.Count) },
                    DataLabels = true
                };
                seriesCollection.Add(pie);
            }
            depositProductChart.Chart.Series = seriesCollection;
            depositProductChart.Chart.Update();

            seriesCollection = new SeriesCollection();
            foreach (var a in entity.DepositProduct)
            {
                PieSeries pie = new PieSeries
                {
                    Title = a.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(a.Deposit.Where(x => x.Currency.Title == "Рубль").Sum(x => x.Deposit1))) },
                    DataLabels = true
                };
                seriesCollection.Add(pie);
            }
            depositProductChartRUB.Chart.Series = seriesCollection;
            depositProductChartRUB.Chart.Update();

            seriesCollection = new SeriesCollection();
            foreach (var a in entity.DepositProduct)
            {
                PieSeries pie = new PieSeries
                {
                    Title = a.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(a.Deposit.Where(x => x.Currency.Title == "Евро").Sum(x => x.Deposit1))) },
                    DataLabels = true
                };
                seriesCollection.Add(pie);
            }
            depositProductChartEUR.Chart.Series = seriesCollection;
            depositProductChartEUR.Chart.Update();

            seriesCollection = new SeriesCollection();
            foreach (var a in entity.DepositProduct)
            {
                PieSeries pie = new PieSeries
                {
                    Title = a.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(a.Deposit.Where(x => x.Currency.Title == "Доллар").Sum(x => x.Deposit1))) },
                    DataLabels = true
                };
                seriesCollection.Add(pie);
            }
            depositProductChartUSD.Chart.Series = seriesCollection;
            depositProductChartUSD.Chart.Update();
        }
        private void BackToMenu() => Close();
        private void HideAllLayer()
        {
            mainLayer.Visibility = Visibility.Hidden;
            debtCardLayer.Visibility = Visibility.Hidden;
            depositLayer.Visibility = Visibility.Hidden;
            creditCardLayer.Visibility = Visibility.Hidden;
            creditLayer.Visibility = Visibility.Hidden;
        }
        private void DebtCardLayerButton_Click(object sender, RoutedEventArgs e) => ShowDebtCardLayer();
        private void DepositLayerButton_Click(object sender, RoutedEventArgs e) => ShowDepositLayer();
        private void CreditCardLayerButton_Click(object sender, RoutedEventArgs e) => ShowCreditCardLayer();
        private void CreditLayerButton_Click(object sender, RoutedEventArgs e) => ShowCreditLayer();
        private void MainLayerButton_Click(object sender, RoutedEventArgs e) => ShowMainLayer();
        private void MainMenyButton_Click(object sender, RoutedEventArgs e) => BackToMenu();
    }
}
