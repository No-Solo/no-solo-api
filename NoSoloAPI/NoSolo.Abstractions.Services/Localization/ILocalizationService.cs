namespace NoSolo.Abstractions.Services.Localization;

 public interface ILocalizationService
    {

        public List<string> Locales { get; }

        public string CurrentLocale { get; }

        public string Get(string key, string? locale = null);

        public string Get(string key, string orgId, string? locale = null);

        public string GetSystem(string key, string? locale = null);

        public List<string> GetRange(List<string> keys, string? locale = null);
    }

    public static class LocalizationReservedKeys
    {
        public static readonly string RecordNotFound = "record-not-found";
        public static readonly string AccessDeniedWasCreatedByAnother = "access-denied-record-was-created-by-another-userEntity";
        public static readonly string AccessDeniedOneSingleTeam = "access-denied-you-cannot-delete-the-one-single-team";
        public static readonly string AccessDeniedReservedKey = "access-denied-key-reserved";
        public static readonly string AccessDeniedLicenses = "access-denied-licences";
        public static readonly string AccessDeniedWasSensetiveValues = "access-denied-sensetive-values";
        public static readonly string AccessDeniedIncorrectOrganization = "access-denied-incorect-organizationEntity";
        public static readonly string AccessDeniedIncorrectTeam = "access-denied-incorect-team";
        public static readonly string AccessDenied = "access-denied";
        public static readonly string IdCannotBeEmpty = "id-cannot-empty";
        public static readonly string BadRequest = "bad-request";
        public static readonly string InvalidOrgType = "invalid-org-type";
        public static readonly string InvalidEmail = "invalid-email";
        public static readonly string InvalidPassword = "invalid-password";
        public static readonly string InvalidConfirmPassword = "invalid-confirm-password";
        public static readonly string PasswordDoesntMatch = "password-doesnt-match";
        public static readonly string UserAlreadyExists = "userEntity-already-exists";
        public static readonly string InvalidInvitationCode = "invalid-invitation-code";
        public static readonly string InvalidOrganization = "invalid-organizationEntity";
        public static readonly string InvalidOrganizationType = "invalid-organizationEntity-type";
        public static readonly string SignUpProblem = "sign-up-problem";
        public static readonly string SignInProblem = "sign-in-problem";
        public static readonly string InvalidRefreshToken = "invalid-refreshtoken";
        public static readonly string PasswordResetProblem = "password-reset-problem";
        public static readonly string InvalidToken = "invalid-token";
        public static readonly string AccessDeniedForYorAdress = "access-denied-for-your-address";
        public static readonly string InternalServerError = "internal-server-error-has-occured";
        public static readonly string UploadFileProblem = "upload-file-problem";
        public static readonly string GetFileProblem = "get-file-problem";
        public static readonly string DeleteFileProblem = "delete-file-problem";
        public static readonly string InvalidSMTPConfiguration = "invalid-smtp-configuration";
    }