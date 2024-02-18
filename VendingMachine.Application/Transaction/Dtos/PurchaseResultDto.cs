namespace VendingMachine.Application.Transaction.Dtos
{
    /// <summary>
    /// DTO object that contains purchase result properties.
    /// </summary>
    public class PurchaseResultDto
    {
        /// <summary>
        /// Name of the Product involved in the transaction.
        /// </summary>
        public string? ProductName { get; set; }
        /// <summary>
        /// Quantity of products purchased.
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Remaining user balance.
        /// </summary>
        public decimal BalanceRemaining { get; set; }
    }
}
