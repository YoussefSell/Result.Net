namespace Result.Net
{
    /// <summary>
    /// a list of common result codes
    /// </summary>
    public static class ResultCode
    {
        /// <summary>
        /// this code represent a fake error for testing purposes.
        /// </summary>
        public const string FakeError = "fake_error";

        /// <summary>
        /// this code represent a successful result operation.
        /// </summary>
        public const string OperationSucceeded = "operation_succeeded";

        /// <summary>
        /// this code represent a failure result operation.
        /// </summary>
        public const string OperationFailed = "operation_failed";

        /// <summary>
        /// this code represent a failure result operation caused by an exception.
        /// </summary>
        public const string OperationFailedException = "operation_failed_exception";

        /// <summary>
        /// the operation has failed due to an internal error.
        /// </summary>
        public const string InternalException = "internal_exception";

        /// <summary>
        /// the user is authenticated to perform the action.
        /// </summary>
        public const string Unauthorized = "unauthorized";
        
        /// <summary>
        /// the user doesn't have the permission to perform the action.
        /// </summary>
        public const string Forbidden = "forbidden";

        /// <summary>
        /// the requested resource cannot be found.
        /// </summary>
        public const string NotFound = "not_found";

        /// <summary>
        /// the user has entered invalid login credentials.
        /// </summary>
        public const string InvalidLoginCredentials = "invalid_login_credentials";

        /// <summary>
        /// the given resource already exist.
        /// </summary>
        public const string ResourceAlreadyExist = "resource_already_exist";

        /// <summary>
        /// this code means that resource has been processed, and no need for re processing
        /// </summary>
        public const string ResourceAlreadyProcessed = "resource_already_processed";

        /// <summary>
        /// the validation of the resource has failed.
        /// </summary>
        public const string ValidationFailed = "validation_failed";

        /// <summary>
        /// this code means that we failed to save the data to the database.
        /// </summary>
        public const string DataPersistenceFailed = "data_persistence_failed";

        /// <summary>
        /// this code means that resource has been Canceled, cannot be processed.
        /// </summary>
        public const string ResourceCanceled = "resource_canceled";

        /// <summary>
        /// this code means that the authentication failed.
        /// </summary>
        public const string AuthenticationFailed = "authentication_failed";

        /// <summary>
        /// this code indicate that we couldn't reach the service, the connection has failed.
        /// </summary>
        public const string ServiceConnectionFailed = "service_connection_failed";

        /// <summary>
        /// this code indicate that the password value is required.
        /// </summary>
        public const string PasswordRequired = "password_required";

        /// <summary>
        /// this code indicate that the email value is required.
        /// </summary>
        public const string EmailRequired = "email_required";

        /// <summary>
        /// this code indicate that the given email value is not valid.
        /// </summary>
        public const string InvalidEmail = "invalid_email";

        /// <summary>
        /// the given operation type is not supported.
        /// </summary>
        public const string UnsupportedActionType = "un_supported_action_type";

        /// <summary>
        /// this code means that we couldn't find a user with the given email.
        /// </summary>
        public const string UserWithEmailNotExist = "user_with_email_not_exist";

        /// <summary>
        /// this code means that you have supplier an invalid password for your login.
        /// </summary>
        public const string InvalidUserPassword = "invalid_user_password";

        /// <summary>
        /// this code means that the user account has been blocked.
        /// </summary>
        public const string UserAccountBlocked = "user_account_blocked";

        /// <summary>
        /// this code means that the user account has been Deactivated.
        /// </summary>
        public const string UserAccountDeactivated = "user_account_deactivated";

        /// <summary>
        /// the property value is not valid.
        /// </summary>
        public const string InvalidPropertyValue = "invalid_property_value";
    }
}
