namespace VendingMachine.Application.Entities
{
    /// <summary>
    /// Tracks the users of the vending machine. 
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// User balance.
        /// </summary>
        public decimal Balance { get; set; }
    }
}
