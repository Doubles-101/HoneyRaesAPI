namespace HoneyRaesAPI.Models;

public class ServiceTicketDTO
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public string Description { get; set; }
    public Boolean Emergency { get; set; }
    public DateOnly DateCompleted { get; set; }
}