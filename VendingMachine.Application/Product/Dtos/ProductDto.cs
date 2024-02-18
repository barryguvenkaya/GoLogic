using AutoMapper;
using VendingMachine.Application.Common.Interfaces;

namespace VendingMachine.Application.Product.Dtos
{
    /// <summary>
    /// DTO object that stores metadata about products available in the vending machine which maps from Entities.Product.
    /// </summary>
    public class ProductDto : IMapFrom<Entities.Product>
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

        /// <summary>
        /// Creates a mapping configuration from this object's relevant entity type to itself. 
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Product, ProductDto>();
        }
    }
}
