namespace DropShot.Application.Auth.Models;

public class RegisterResponseDto
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public string Email { get; set; }
    
    public List<string> Errors { get; set; }
}