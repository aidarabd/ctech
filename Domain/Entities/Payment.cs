namespace Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime OperationDate { get; set; }
    public int OperationTypeId { get; set; }

    public User User { get; set; }
}