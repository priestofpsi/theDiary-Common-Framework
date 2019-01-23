//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfService1
{
    using System;
    using System.Collections.Generic;
    
    public partial class CreditNote
    {
        public CreditNote()
        {
            this.CreditNoteLineItem = new HashSet<CreditNoteLineItem>();
        }
    
        public int CreditNoteID { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string ReturnToSupplierID { get; set; }
        public Nullable<decimal> TotalQuantity { get; set; }
        public Nullable<decimal> TotalWeight { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual ICollection<CreditNoteLineItem> CreditNoteLineItem { get; set; }
    }
}
