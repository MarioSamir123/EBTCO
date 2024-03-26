using EBTCO.Core.Contract.Identity;
using Google.Apis.Auth;

namespace ToursYard.RDS.Identity
{
    public class GoogleAuthService : IGoogleAuthService
    {
        public async Task<GoogleTokenPayload?> GetUserDataFromGoogleToken(string token)
        {
            var validator = new GoogleJsonWebSignature.ValidationSettings { };

            var payload = await GoogleJsonWebSignature.ValidateAsync(token, validator);

            return (payload == null) ? default : new GoogleTokenPayload(payload.Email, payload.EmailVerified, payload.Name);
        }
    }
}
