namespace Todo.Api.Models;

public class AuthUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Roles { get; set; }
}
