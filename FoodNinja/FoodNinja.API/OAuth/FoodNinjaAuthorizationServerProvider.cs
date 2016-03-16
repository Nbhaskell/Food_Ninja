using FoodNinja.Core.Domain;
using FoodNinja.Core.Repository;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodNinja.API.OAuth
{
    public class FoodNinjaAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly Func<IAuthorizationRepository> _authorizationRepositoryFactory;
        protected IAuthorizationRepository AuthorizationRepository => _authorizationRepositoryFactory.Invoke();

        public FoodNinjaAuthorizationServerProvider(Func<IAuthorizationRepository> authorizationRepositoryFactory)
        {
            _authorizationRepositoryFactory = authorizationRepositoryFactory;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Factory.StartNew(() =>
            {
                context.Validated();
            });
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            NinjaUser user = await AuthorizationRepository.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                    "username", user.UserName
                },
                {
                    "name", user.FullName
                },
                {
                    "email", user.EmailAddress
                },
            });

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}