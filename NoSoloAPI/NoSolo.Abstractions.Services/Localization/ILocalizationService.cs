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
        public static string RecordNotFound = "record-not-found";
        public static string AccessDeniedWasCreatedByAnother = "access-denied-record-was-created-by-another-user";
        public static string AccessDeniedOneSingleTeam = "access-denied-you-cannot-delete-the-one-single-team";
        public static string AccessDeniedReservedKey = "access-denied-key-reserved";
        public static string AccessDeniedLicenses = "access-denied-licences";
        public static string AccessDeniedWasSensetiveValues = "access-denied-sensetive-values";
        public static string AccessDeniedIncorrectOrganization = "access-denied-incorect-organization";
        public static string AccessDeniedIncorrectTeam = "access-denied-incorect-team";
        public static string AccessDenied = "access-denied";
        public static string IdCannotBeEmpty = "id-cannot-empty";
        public static string BadRequest = "bad-request";
        public static string InvalidOrgType = "invalid-org-type";
        public static string InvalidEmail = "invalid-email";
        public static string InvalidPassword = "invalid-password";
        public static string InvalidConfirmPassword = "invalid-confirm-password";
        public static string PasswordDoesntMatch = "password-doesnt-match";
        public static string UserAlreadyExists = "user-already-exists";
        public static string InvalidInvitationCode = "invalid-invitation-code";
        public static string InvalidOrganization = "invalid-organization";
        public static string InvalidOrganizationType = "invalid-organization-type";
        public static string SignUpProblem = "sign-up-problem";
        public static string SignInProblem = "sign-in-problem";
        public static string InvalidRefreshToken = "invalid-refreshtoken";
        public static string PasswordResetProblem = "password-reset-problem";
        public static string InvalidToken = "invalid-token";
        public static string AccessDeniedForYorAdress = "access-denied-for-your-address";
        public static string InternalServerError = "internal-server-error-has-occured";
        public static string UploadFileProblem = "upload-file-problem";
        public static string GetFileProblem = "get-file-problem";
        public static string DeleteFileProblem = "delete-file-problem";
        public static string InvalidSMTPConfiguration = "invalid-smtp-configuration";
    }