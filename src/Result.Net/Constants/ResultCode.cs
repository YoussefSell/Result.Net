namespace ResultNet
{
    /// <summary>
    /// A list of common result codes.
    /// </summary>
    public static class ResultCode
    {
        /// <summary>
        /// This code represents a fake error for testing purposes.
        /// </summary>
        public const string FakeError = "fake_error";

        /// <summary>
        /// This code represents a successful result operation.
        /// </summary>
        public const string OperationSucceeded = "operation_succeeded";

        /// <summary>
        /// This code represents a failure result operation.
        /// </summary>
        public const string OperationFailed = "operation_failed";

        /// <summary>
        /// This code represents a failure result operation caused by an exception.
        /// </summary>
        public const string OperationFailedException = "operation_failed_exception";

        /// <summary>
        /// This code indicates that the operation has failed due to an internal error.
        /// </summary>
        public const string InternalException = "internal_exception";

        /// <summary>
        /// This code indicates that the user is not authenticated to perform the action.
        /// </summary>
        public const string Unauthorized = "unauthorized";
        
        /// <summary>
        /// This code indicates that the user doesn't have the permission to perform the action.
        /// </summary>
        public const string Forbidden = "forbidden";

        /// <summary>
        /// This code indicates that the requested resource cannot be found.
        /// </summary>
        public const string NotFound = "not_found";

        /// <summary>
        /// This code indicates that the user has entered invalid login credentials.
        /// </summary>
        public const string InvalidLoginCredentials = "invalid_login_credentials";

        /// <summary>
        /// This code indicates that the given resource already exists.
        /// </summary>
        public const string ResourceAlreadyExist = "resource_already_exist";

        /// <summary>
        /// This code indicates that the resource has been processed, and there is no need for reprocessing.
        /// </summary>
        public const string ResourceAlreadyProcessed = "resource_already_processed";

        /// <summary>
        /// This code indicates that the validation of the resource has failed.
        /// </summary>
        public const string ValidationFailed = "validation_failed";

        /// <summary>
        /// This code indicates that the system failed to save the data to the database.
        /// </summary>
        public const string DataPersistenceFailed = "data_persistence_failed";

        /// <summary>
        /// This code indicates that the resource has been canceled and cannot be processed.
        /// </summary>
        public const string ResourceCanceled = "resource_canceled";

        /// <summary>
        /// This code indicates that the authentication failed.
        /// </summary>
        public const string AuthenticationFailed = "authentication_failed";

        /// <summary>
        /// This code indicates that the system couldn't reach the service and the connection has failed.
        /// </summary>
        public const string ServiceConnectionFailed = "service_connection_failed";

        /// <summary>
        /// This code indicates that the password value is required.
        /// </summary>
        public const string PasswordRequired = "password_required";

        /// <summary>
        /// This code indicates that the email value is required.
        /// </summary>
        public const string EmailRequired = "email_required";

        /// <summary>
        /// This code indicates that the given email value is not valid.
        /// </summary>
        public const string InvalidEmail = "invalid_email";

        /// <summary>
        /// This code indicates that the given operation type is not supported.
        /// </summary>
        public const string UnsupportedActionType = "un_supported_action_type";

        /// <summary>
        /// This code indicates that the system couldn't find a user with the given email.
        /// </summary>
        public const string UserWithEmailNotExist = "user_with_email_not_exist";

        /// <summary>
        /// This code indicates that the user has supplied an invalid password for login.
        /// </summary>
        public const string InvalidUserPassword = "invalid_user_password";

        /// <summary>
        /// This code indicates that the user account has been blocked.
        /// </summary>
        public const string UserAccountBlocked = "user_account_blocked";

        /// <summary>
        /// This code indicates that the user account has been deactivated.
        /// </summary>
        public const string UserAccountDeactivated = "user_account_deactivated";

        /// <summary>
        /// This code indicates that the property value is not valid.
        /// </summary>
        public const string InvalidPropertyValue = "invalid_property_value";
    }
}
