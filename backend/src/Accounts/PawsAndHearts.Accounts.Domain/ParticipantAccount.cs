namespace PawsAndHearts.Accounts.Domain;

public class ParticipantAccount
{
    public ParticipantAccount(User user)
    {
        Id = Guid.NewGuid();
        User = user;
    }

    private ParticipantAccount()
    {
        
    }
    
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }
}