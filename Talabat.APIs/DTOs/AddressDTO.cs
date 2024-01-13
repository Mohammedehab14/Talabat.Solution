using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.DTOs
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
    }
}
