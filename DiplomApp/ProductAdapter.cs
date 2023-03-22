using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomApp
{
    public interface IProductAdapter
    {
        object Product { get; }
        string Title { get; set; }
        string Description { get; set; }
        bool IsEnable  { get; set; }
        CreditType CreditType { get; set; }
        int NoDebtDay { get; set; }
        decimal DebtRate { get; set; }
        string ProductType { get; }
        Dictionary<string, decimal> MoneyCirculation { get; }
        int ClientCount { get; }
    }
    class DebtCardProductAdapter : IProductAdapter
    {
        public DebtCardProduct DebtCardProduct { get; set; }
        object IProductAdapter.Product { get => DebtCardProduct; }

        public DebtCardProductAdapter() { DebtCardProduct = new DebtCardProduct(); }
        public DebtCardProductAdapter(DebtCardProduct debtCardProduct) => DebtCardProduct = debtCardProduct;

        public string Title
        {
            get => DebtCardProduct.Title;
            set => DebtCardProduct.Title = value;
        }
        public string Description 
        { 
            get => DebtCardProduct.Description;
            set => DebtCardProduct.Description = value; 
        }
        public bool IsEnable 
        { 
            get => DebtCardProduct.IsEnable; 
            set => DebtCardProduct.IsEnable = value; 
        }
        public string ProductType => "Дебетовая карта";

        public Dictionary<string, decimal> MoneyCirculation
        {
            get
            {
                var currencyGroups = DebtCardProduct.DebitСard.GroupBy(x => x.IdCurrency);
                Dictionary<string, decimal> result = new Dictionary<string, decimal>();
                foreach (var currencyGroup in currencyGroups)
                    result.Add(currencyGroup.First().Currency.CurrencySymbol, currencyGroup.Sum(x=> x.Balance));
                return result;
            }
        }
        public int ClientCount => DebtCardProduct.DebitСard.Where(x=> x.IsActive == true).Count();
        public CreditType CreditType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int NoDebtDay { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal DebtRate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
    class CreditCardProductAdapter : IProductAdapter
    {
        public CreditCardProduct CreditCardProduct { get; set; }
        object IProductAdapter.Product { get => CreditCardProduct; }

        public CreditCardProductAdapter() { CreditCardProduct = new CreditCardProduct(); }
        public CreditCardProductAdapter(CreditCardProduct creditCardProduct) => CreditCardProduct = creditCardProduct;

        public string Title
        {
            get => CreditCardProduct.Title;
            set => CreditCardProduct.Title = value;
        }
        public string Description
        {
            get => CreditCardProduct.Description;
            set => CreditCardProduct.Description = value;
        }
        public bool IsEnable
        {
            get => CreditCardProduct.IsEnable;
            set => CreditCardProduct.IsEnable = value;
        }
        public int NoDebtDay 
        { 
            get => CreditCardProduct.NoDebtDay.Value;
            set => CreditCardProduct.NoDebtDay = value; 
        }
        public decimal DebtRate 
        { 
            get => CreditCardProduct.MinimalDebtRate; 
            set => CreditCardProduct.MinimalDebtRate = value; 
        }

        public string ProductType => "Кредитная карта";

        public Dictionary<string, decimal> MoneyCirculation
        {
            get
            {
                var currencyGroups = CreditCardProduct.CreditCard.GroupBy(x => x.IdCurrency);
                Dictionary<string, decimal> result = new Dictionary<string, decimal>();
                foreach (var currencyGroup in currencyGroups)
                    result.Add(currencyGroup.First().Currency.CurrencySymbol, currencyGroup.Sum(x => x.CreditLimit - x.Balance));
                return result;
            }
        }
        public int ClientCount => CreditCardProduct.CreditCard.Where(x => x.IsActive == true).Count();
        public CreditType CreditType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
    class CreditProductAdapter : IProductAdapter
    {
        public CreditProduct CreditProduct { get; set; }
        object IProductAdapter.Product { get => CreditProduct; }

        public CreditProductAdapter() { CreditProduct = new CreditProduct(); }
        public CreditProductAdapter(CreditProduct creditProduct) => CreditProduct = creditProduct;

        public string Title
        {
            get => CreditProduct.Title;
            set => CreditProduct.Title = value;
        }
        public string Description
        {
            get => CreditProduct.Description;
            set => CreditProduct.Description = value;
        }
        public bool IsEnable
        {
            get => CreditProduct.IsEnable;
            set => CreditProduct.IsEnable = value;
        }
        public CreditType CreditType 
        { 
            get => CreditProduct.CreditType; 
            set => CreditProduct.CreditType = value; 
        }

        public decimal DebtRate 
        { 
            get => CreditProduct.MinimalDebtRate; 
            set => CreditProduct.MinimalDebtRate = value; 
        }

        public string ProductType => "Кредит";

        public Dictionary<string, decimal> MoneyCirculation
        {
            get
            {
                var currencyGroups = CreditProduct.Credit.GroupBy(x => x.IdCurrency);
                Dictionary<string, decimal> result = new Dictionary<string, decimal>();
                foreach (var currencyGroup in currencyGroups)
                    result.Add(currencyGroup.First().Currency.CurrencySymbol, currencyGroup.Sum(x => x.Debt));
                return result;
            }
        }
        public int ClientCount => CreditProduct.Credit.Where(x => x.IsActive == true).Count();
        public int NoDebtDay { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
    class DepositProductAdapter : IProductAdapter
    {
        public DepositProduct DepositProduct { get; set; }
        object IProductAdapter.Product { get => DepositProduct; }

        public DepositProductAdapter() { DepositProduct = new DepositProduct();}
        public DepositProductAdapter(DepositProduct debtCardProduct) => DepositProduct = debtCardProduct;

        public string Title
        {
            get => DepositProduct.Title;
            set => DepositProduct.Title = value;
        }
        public string Description
        {
            get => DepositProduct.Description;
            set => DepositProduct.Description = value;
        }
        public bool IsEnable
        {
            get => DepositProduct.IsEnable;
            set => DepositProduct.IsEnable = value;
        }
        public decimal DebtRate 
        { 
            get => DepositProduct.DebtRate; 
            set => DepositProduct.DebtRate = value; 
        }
        public string ProductType => "Вклад";
        public Dictionary<string, decimal> MoneyCirculation
        {
            get
            {
                var currencyGroups = DepositProduct.Deposit.GroupBy(x => x.IdCurrency);
                Dictionary<string, decimal> result = new Dictionary<string, decimal>();
                foreach (var currencyGroup in currencyGroups)
                    result.Add(currencyGroup.First().Currency.CurrencySymbol, currencyGroup.Sum(x => x.Deposit1));
                return result;
            }
        }
        public int ClientCount => DepositProduct.Deposit.Where(x => x.IsActive == true).Count();
        public CreditType CreditType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int NoDebtDay { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
