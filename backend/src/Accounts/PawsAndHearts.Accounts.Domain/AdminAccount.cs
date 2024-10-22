namespace PawsAndHearts.Accounts.Domain;

public class AdminAccount
{
    private AdminAccount()
    {
        
    }
    
    public AdminAccount(User user)
    {
        Id = Guid.NewGuid();
        User = user;
    }
    
    public Guid Id { get; set; }
    
    public User User { get; set; }
    
    public Guid UserId { get; set; }
}