
namespace Server.Application.Consts
{
    public static class ResponseMessages
    {
        public static readonly string Success = "Success";
        public static readonly string Fail = "Fail";
        public static readonly string UserAlreadyExist = "User Already Exist";
        public static readonly string UserNotFound = "User Not Found";
        public static readonly string InvalidCredentials = "Invalid Credentials";
        public static readonly string NotFound = "Not Found";
        public static readonly string IncorrectOldPasswordEntry = "Incorrect Old Password Entry";
        public static readonly string NewPasswordsDoNotMatch = "New Passwords Do Not Match";
        public static readonly string NewPasswordCannotBeTheSameAsOldPassword = "New Password Cannot Be The Same As Old Password";
        public static readonly string AnErrorOccurredWhileLoadingTheImage = "An Error Occurred While Loading The Image";
        public static readonly string CannotBeEmpty = "Cannot Be Empty";
        public static readonly string ModelValidationFail = "An Error Occured While Request Model Validation";
    }
}
