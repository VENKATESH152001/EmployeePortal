namespace EmployeePortal.web.Models
{
    public class AddEmployeeViewModel
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}
