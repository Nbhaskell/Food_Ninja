using FoodNinja.Core.Repository;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;


namespace FoodNinja.API.Infrastructure
{
    public class FoodNinjaAuthorizationServerProvider
    {
        private Func<IAuthorizationRepository> _authRepository;
        private IAuthorizationRepository AuthRepository
        {
            get
            {
                return _authRepository.Invoke();
            }
        }

        public FoodNinjaAuthorizationServerProvider(Func<IAuthorizationRepository> authRepositoryFactory)
        {
            _authRepository = authRepositoryFactory;
        }

        public async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });


            var user = await AuthRepository.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The username or password is incorrect");
                return;
            }
            else
            {
                var token = new ClaimsIdentity(context.Options.AuthenticationType);
                token.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                token.AddClaim(new Claim("role", "user"));
                context.Validated(token);
            }
        }
    }
}