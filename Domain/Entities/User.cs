namespace Domain.Entities;

public class User
{
     public int Id { get; set; }
     public string Username { get; set; }
     public string PasswordHash { get; set; }
     public decimal Balance { get; set; } = 8m;
     public short LoginAttempts { get; set; }
     public DateTime? LockoutEnd { get; set; }
}