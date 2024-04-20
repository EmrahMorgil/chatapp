
using System.Numerics;

namespace Server.Application.Consts
{
    public static class ResponseMessages
    {
        public static readonly string Success = "Success";
        public static readonly string Fail = "Fail";
        public static readonly string UserAlreadyExist = "User already exist";
        public static readonly string UserNotFound = "User not found";
        public static readonly string InvalidCredentials = "Invalid credentials";
        public static readonly string NotFound = "Not found";
        public static readonly string IncorrectOldPasswordEntry = "Incorrect old password entry";
        public static readonly string NewPasswordsDoNotMatch = "New passwords do not match";
        public static readonly string NewPasswordCannotBeTheSameAsOldPassword = "New password cannot be the same as old password";
        public static readonly string AnErrorOccurredWhileLoadingTheImage = "An error occurred while loading the image";
        public static readonly string CannotBeEmpty = "Cannot be empty";
        public static readonly string ModelValidationFail = "An error occured while request model validation";
        public static readonly string UnauthorizedEntry = "Unauthorized entry";
        public static readonly string InvalidImageType = "Invalid image type";
        public static readonly string ThisEmailIsBeingUsed = "This email is being used";
        public static readonly string PleaseEnterValidEmailAddress = "Please enter a valid email address";
    }
}
