using Microsoft.AspNetCore.Identity;

namespace NoteKeeper.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}
