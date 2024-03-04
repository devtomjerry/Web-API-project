using API.Data;
using System.ComponentModel.DataAnnotations;

public class Student
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string MobileNumber { get; set; }

    [Required]
    public string SchoolName { get; set; }

    [Required]
    public string Email { get; set; }

}
