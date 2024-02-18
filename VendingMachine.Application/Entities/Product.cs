namespace VendingMachine.Application.Entities
{
    /// <summary>
    /// Stores metadata about products available in the vending machine.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Unique identifier for each product.
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Name of the Product.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Price of the Product.
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Stock quantity of the Product
        /// </summary>
        public int Stock { get; set; }
    }
}
