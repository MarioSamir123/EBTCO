namespace EBTCO.Core.Contract.Identity
{
    public record GoogleTokenPayload(string Email, bool EmailVerified, string username);
}
