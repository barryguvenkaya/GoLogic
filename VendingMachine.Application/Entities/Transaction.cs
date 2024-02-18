namespace VendingMachine.Application.Entities
{
    /// <summary>
    /// Stores details of each transaction made by users, including product purchase information, payment amount, change returned, and timestamp.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Unique identifier for each transaction.
        /// </summary>
        public int TransactionId { get; set; }
        /// <summary>
        /// Identifier of the User involved in the transaction.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Identifier of the Product involved in the transaction.
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Quantity of products purchased.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Date and time when the transaction occurred in UTC.
        /// </summary>
        public DateTime TimestampUtc { get; set; }
    }
}
