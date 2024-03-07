using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserListDto
    {
        public int Id { get; set; }
       
        public string Email { get; set; }

        public string Address { get; set; }

        public string MobileNumber { get; set; }

    }
}
