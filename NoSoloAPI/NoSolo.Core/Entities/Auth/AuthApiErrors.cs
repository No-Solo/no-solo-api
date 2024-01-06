namespace NoSolo.Core.Entities.Auth;

public static class AuthApiErrors
{
    public static readonly string INVALID_EMAIL = "INVALID_EMAIL";
    public static readonly string INVALID_PASSWORD = "INVALID_PASSWORD";
    public static readonly string INVALID_CONFIRMPASSWORD = "INVALID_CONFIRMPASSWORD";
    public static readonly string PASSWORDS_DOESNT_MATCH = "PASSWORDS_DOESNT_MATCH";

    public static readonly string API_NOT_FOUND = "PI_NOT_FOUND";
    public static readonly string API_BAD_REQUEST = "API_BAD_REQUEST";
    public static readonly string API_ERROR = "API_ERROR";
    public static readonly string SIGN_IN_PROBLEMS = "SIGN_IN_PROBLEMS";
    public static readonly string SIGN_UP_PROBLEMS = "SIGN_UP_PROBLEMS";
    public static readonly string WRONG_CREDENTIALS = "WRONG_CREDENTIALS";
    public static readonly string ACCESS_DENIED = "ACCESS_DENIED";
    public static readonly string USER_ALREADY_EXISTS = "USER_ALREADY_EXISTS";
    public static readonly string INVALID_ORGANIZATION = "INVALID_ORGANIZATION";
    public static readonly string INVALID_ORGANIZATION_TYPE = "INVALID_ORGANIZATION_TYPE";
    public static readonly string INVALID_INVITATION_CODE = "INVALID_INVITATION_CODE";
    public static readonly string INVALID_REFRESH_TOKEN = "INVALID_REFRESH_TOKEN";
    public static readonly string INVALID_TOKEN = "INVALID_TOKEN";
    public static readonly string PASSWORD_RESET_PROBLEM = "PASSWORD_RESET_PROBLEM";
}