namespace VendingMachine.Application.User.Dtos
{
    /// <summary>
    /// DTO object representing the refund of the user's entire balance. 
    /// </summary>
    public class UserBalanceRefundDto
    {
        /// <summary>
        /// The change amount that is equal to the user's previous balance. 
        /// </summary>
        public decimal ChangeAmount { get; set; }
    }
}
