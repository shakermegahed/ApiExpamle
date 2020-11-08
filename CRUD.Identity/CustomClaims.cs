using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CRUD.Identity
{
    public static class CustomClaims
    {
        public const string ACCOUNT_ID = "id";
        public const string ACCOUNT_NAME = "name";
        public const string ACCOUNT_EMAIL = "account_email";
        public const string ACCOUNT_ROLE = "role";
        public const string SELECTED_WEBSITE = "websiteId";
        public const string WEBSITE_DOMAIN = "website_domain";
        public static string WEBSITE_FEATURES = "website_features";
    }
}
