using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.Accounts.Domain;

public class ParticipantAccount
{
    public ParticipantAccount(FullName fullName, User user)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        User = user;
    }

    private ParticipantAccount()
    {
        
    }
    
    public Guid Id { get; set; }
    
    public FullName FullName { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }
}