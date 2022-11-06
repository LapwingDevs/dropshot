using DropShot.Application.Auth.Models;
using Microsoft.AspNetCore.Identity;

namespace DropShot.Infrastructure.Identity.Extensions;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(result.Errors.Select(e => e.Description));
    }
}