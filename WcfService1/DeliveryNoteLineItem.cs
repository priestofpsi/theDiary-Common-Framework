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
    
    public partial class DeliveryNoteLineItem
    {
        public long DeliveryNoteLineItemID { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
    
        public virtual Alloy Alloy { get; set; }
        public virtual Consumable Consumable { get; set; }
        public virtual DeliveryNote DeliveryNote { get; set; }
    }
}
