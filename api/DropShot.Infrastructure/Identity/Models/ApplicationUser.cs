using Microsoft.AspNetCore.Identity;

namespace DropShot.Infrastructure.Identity.Models;

public class ApplicationUser : IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; }
    
    [ProtectedPersonalData]
    public string LastName { get; set; }
}