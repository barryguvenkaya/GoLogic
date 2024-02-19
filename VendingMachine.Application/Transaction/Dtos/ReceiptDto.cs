namespace VendingMachine.Application.Transaction.Dtos
{
    /// <summary>
    /// Receipt that containts transactions made by the user
    /// </summary>
    public class ReceiptDto
    {
        /// <summary>
        /// Name of the Product involved in the transaction.
        /// </summary>
        public string? ProductName { get; set; }
        /// <summary>
        /// Quantity of products purchased.
        /// </summary>
        public decimal Quantity { get; set; }
        /// <summary>
        /// Price of the Product.
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Total amount paid (PurchasedQuantity * Price)
        /// </summary>
        public decimal TotalPaid { get; set; }
        /// <summary>
        /// Date and time when the transaction converted from UTC back into user's local timezone.
        /// </summary>
        public DateTime TimestampLocal { get; set; }
    }
}
