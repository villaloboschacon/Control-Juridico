using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using Microsoft.Owin.Security;
using SistemaControl.App_Start;
using BackEnd.BLL;

namespace SistemaControl.Models
{
    public class AdAuthenticationService
    {
        private IUsuarioBLL usuarioBLL;
        public class AuthenticationResult
        {
            public AuthenticationResult(string errorMessage = null)
            {
                ErrorMessage = errorMessage;
            }

            public String ErrorMessage { get; private set; }
            public Boolean IsSuccess => String.IsNullOrEmpty(ErrorMessage);
        }

        private readonly IAuthenticationManager authenticationManager;

        public AdAuthenticationService(IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }


        /// <summary>
        /// Check if username and password matches existing account in AD. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        ///
        public UserPrincipal getUserPrincipal(string username, string password)
        {
            ContextType authenticationType = ContextType.Machine;
            PrincipalContext principalContext = new PrincipalContext(authenticationType);
            bool isAuthenticated = false;
            UserPrincipal userPrincipal = null;
            isAuthenticated = principalContext.ValidateCredentials(username, password, ContextOptions.Negotiate);
            try
            {
                //isAuthenticated = principalContext.ValidateCredentials(username, password, ContextOptions.Negotiate);
                if (isAuthenticated)
                {
                    userPrincipal = UserPrincipal.FindByIdentity(principalContext, username);
                    return userPrincipal;
                }
            }
            catch (Exception)
            {
                isAuthenticated = false;
                userPrincipal = null;
                return null;
            }
            return null;
        }
        public AuthenticationResult SignIn(String username, String password)
        {
#if DEBUG
            // authenticates against your local machine - for development time
            //ContextType authenticationType = ContextType.Machine;
#else
            // authenticates against your Domain AD
            ContextType authenticationType = ContextType.Domain;
#endif
            //Se usa Context.Domain cuando se utiliza el active
            //ContextType authenticationType = ContextType.Domain;
            //PrincipalContext principalContext = new PrincipalContext(authenticationType, "192.168.52.129", username, password);
            //Se usa Context.Machine cuando se utiliza la cuenta de la PC.
            //Se quitan los comentarios de abajo para poder usarlos y se comentan los dos de arriba.
            ContextType authenticationType = ContextType.Machine;
            PrincipalContext principalContext = new PrincipalContext(authenticationType);
            bool isAuthenticated = false;
            UserPrincipal userPrincipal = null;
            isAuthenticated = principalContext.ValidateCredentials(username, password, ContextOptions.Negotiate);
            try
            {
                //isAuthenticated = principalContext.ValidateCredentials(username, password, ContextOptions.Negotiate);
                if (isAuthenticated)
                {
                    userPrincipal = UserPrincipal.FindByIdentity(principalContext, username);
                }
            }
            catch (Exception)
            {
                isAuthenticated = false;
                userPrincipal = null;
            }
            if (!isAuthenticated || userPrincipal == null)
            {
                return new AuthenticationResult("Username or Password is not correct");
            }

            if (userPrincipal.IsAccountLockedOut())
            {
                // here can be a security related discussion weather it is worth 
                // revealing this information
                return new AuthenticationResult("Your account is locked.");
            }

            if (userPrincipal.Enabled.HasValue && userPrincipal.Enabled.Value == false)
            {
                // here can be a security related discussion weather it is worth 
                // revealing this information
                return new AuthenticationResult("Your account is disabled");
            }

            var identity = CreateIdentity(userPrincipal);

            authenticationManager.SignOut(MyAuthentication.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);


            return new AuthenticationResult();
        }
        public string GetUsername(String username, String password)
        {
#if DEBUG
            // authenticates against your local machine - for development time
            //ContextType authenticationType = ContextType.Machine;
#else
            // authenticates against your Domain AD
            ContextType authenticationType = ContextType.Domain;
#endif
            //Se usa Context.Domain cuando se utiliza el active
            //ContextType authenticationType = ContextType.Domain;
            //PrincipalContext principalContext = new PrincipalContext(authenticationType, "192.168.52.129", username, password);
            //Se usa Context.Machine cuando se utiliza la cuenta de la PC.
            //Se quitan los comentarios de abajo para poder usarlos y se comentan los dos de arriba.
            ContextType authenticationType = ContextType.Machine;
            PrincipalContext principalContext = new PrincipalContext(authenticationType);
            bool isAuthenticated = false;
            UserPrincipal userPrincipal = null;
            isAuthenticated = principalContext.ValidateCredentials(username, password, ContextOptions.Negotiate);
            try
            {
                //isAuthenticated = principalContext.ValidateCredentials(username, password, ContextOptions.Negotiate);
                if (isAuthenticated)
                {
                    userPrincipal = UserPrincipal.FindByIdentity(principalContext, username);
                }
            }
            catch (Exception)
            {
                isAuthenticated = false;
                userPrincipal = null;
            }
            var identity = CreateIdentity(userPrincipal);
            return identity.Name;
        }

        private ClaimsIdentity CreateIdentity(UserPrincipal userPrincipal)
        {
            var identity = new ClaimsIdentity(MyAuthentication.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "Active Directory"));
            //identity.AddClaim(new Claim(ClaimTypes.Name, userPrincipal.DisplayName));
            try
            {
                usuarioBLL = new UsuarioBLLImpl();
                usuarioBLL.getUsuario(userPrincipal.Name);
                string role = usuarioBLL.gerRolForUser(userPrincipal.Name).ToString();
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            catch (Exception)
            {

            }

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userPrincipal.SamAccountName));
            if (!String.IsNullOrEmpty(userPrincipal.EmailAddress))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, userPrincipal.EmailAddress));
            }

            // add your own claims if you need to add more information stored on the cookie

            return identity;
        }
    }
}