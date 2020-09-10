using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.WebServices.Authorization
{
    public static class TypiconOperations
    {
        public static OperationAuthorizationRequirement Create =
          new OperationAuthorizationRequirement { Name = AuthorizationConstants.CreateTypiconName };
        public static OperationAuthorizationRequirement Edit =
          new OperationAuthorizationRequirement { Name = AuthorizationConstants.EditTypiconName };
        public static OperationAuthorizationRequirement Delete =
          new OperationAuthorizationRequirement { Name = AuthorizationConstants.DeleteTypiconName };

        public static OperationAuthorizationRequirement Default =
          new OperationAuthorizationRequirement { Name = AuthorizationConstants.DefaultTypiconName };
    }

    
}
