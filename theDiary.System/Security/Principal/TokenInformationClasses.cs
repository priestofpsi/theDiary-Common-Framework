using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security.Principal
{
    /// <summary>
    /// Defines the values that specify the type of information being assigned to or retrieved from an access token.
    /// </summary>
    public enum TokenInformationClasses
    {
        TokenUser = 1,

        TokenGroups,

        TokenPrivileges,

        TokenOwner,

        TokenPrimaryGroup,

        TokenDefaultDacl,

        TokenSource,

        TokenType,

        TokenImpersonationLevel,

        TokenStatistics,

        TokenRestrictedSids,

        TokenSessionId,

        TokenGroupsAndPrivileges,

        TokenSessionReference,

        TokenSandBoxInert,

        TokenAuditPolicy,

        TokenOrigin,

        TokenElevationType,

        TokenLinkedToken,

        TokenElevation,

        TokenHasRestrictions,

        TokenAccessInformation,

        TokenVirtualizationAllowed,

        TokenVirtualizationEnabled,

        TokenIntegrityLevel,

        TokenUIAccess,

        TokenMandatoryPolicy,

        TokenLogonSid,

        MaxTokenInfoClass
    }
}
