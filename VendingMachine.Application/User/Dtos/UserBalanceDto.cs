using AutoMapper;
using VendingMachine.Application.Common.Interfaces;

namespace VendingMachine.Application.User.Dtos
{
    /// <summary>
    /// DTO object that tracks the user balances of the vending machine. 
    /// </summary>
    public class UserBalanceDto : IMapFrom<Entities.User>
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// User balance.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Creates a mapping configuration from this object's relevant entity type to itself. 
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.User, UserBalanceDto>();
        }
    }
}
