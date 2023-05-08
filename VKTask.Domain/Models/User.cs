namespace VKTask.Domain.Models;

public class User
{
    public Guid Id { get; init; }

    public string Login { get; set; }

    public string Password { get; set; }

    public DateTime CreatedDate { get; set; }

    public UserGroup UserGroup { get; set; }
    
    public int UserGroupId { get; set; }

    public UserState UserState { get; set; }
    
    public int UserStateId { get; set; }
}
