using Microsoft.AspNetCore.Authentication;

namespace GS.Services.Authentication.External
{
    public interface IExternalAuthenticationRegistrar
    {
        void Configure(AuthenticationBuilder builder);
    }
}
