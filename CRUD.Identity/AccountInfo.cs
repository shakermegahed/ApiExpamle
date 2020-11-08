using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace CRUD.Identity
{
    public class AccountInfo : IAccountInfo
    {
        private readonly List<Claim> _claims;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountInfo(IHttpContextAccessor httpContextAccessor)
        {
            _claims = httpContextAccessor.HttpContext.User.Claims.ToList();
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid Id => new Guid(GetClaimValue(CustomClaims.ACCOUNT_ID));

        public string Email => GetClaimValue(CustomClaims.ACCOUNT_EMAIL);

        public string Name => GetClaimValue(CustomClaims.ACCOUNT_NAME);
        public string Role => GetClaimValue(ClaimTypes.Role);
        public Guid WebsiteId => new Guid(GetClaimValue(CustomClaims.SELECTED_WEBSITE));


        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool IsInRole(string role)
        {
            return _httpContextAccessor.HttpContext.User.IsInRole(role);
        }

        private string GetClaimValue(string claimName)
        {
            if (_claims != null && _claims.Count > 0)
            {
                var claim = _claims.Find(c => c.Type == claimName);
                if (claim != null)
                { return claim.Value; }
            }
            return string.Empty;
        }
    }
}
