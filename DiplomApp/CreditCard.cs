//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiplomApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class CreditCard
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public int IdPaymentSystem { get; set; }
        public int IdCreditCardProduct { get; set; }
        public decimal Balance { get; set; }
        public string Number { get; set; }
        public string OwnerName { get; set; }
        public System.DateTime ValidTHRU { get; set; }
        public bool IsActive { get; set; }
        public decimal LoanRate { get; set; }
        public Nullable<int> IdCurrency { get; set; }
        public decimal CreditLimit { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual CreditCardProduct CreditCardProduct { get; set; }
        public virtual PaymentSystem PaymentSystem { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
