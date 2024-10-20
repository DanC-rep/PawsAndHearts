using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.Accounts.Domain;

public class AdminAccount
{
    public Guid Id { get; set; }
    
    public FullName FullName { get; set; }
    
    public User User { get; set; }
    
    public Guid UserId { get; set; }
}