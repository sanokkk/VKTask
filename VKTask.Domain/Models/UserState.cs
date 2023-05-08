namespace VKTask.Domain.Models;

public class UserState
{
    public int UserStateId { get; init; }

    public string Code { get; init; }

    public string Description { get; set; }
}
