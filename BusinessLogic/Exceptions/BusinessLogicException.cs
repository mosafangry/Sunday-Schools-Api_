using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions
{
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException(string message) : base(message) { }
        public BusinessLogicException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class UserNotFoundException : BusinessLogicException
    {
        public UserNotFoundException(int userId) : base($"User with ID {userId} not found") { }
        public UserNotFoundException(string email) : base($"User with email {email} not found") { }
    }

    public class InvalidCredentialsException : BusinessLogicException
    {
        public InvalidCredentialsException() : base("Invalid email or password") { }
    }

    public class DuplicateUserException : BusinessLogicException
    {
        public DuplicateUserException(string email) : base($"User with email {email} already exists") { }
    }

    public class PermissionDeniedException : BusinessLogicException
    {
        public PermissionDeniedException(string permission) : base($"Access denied: {permission} permission required") { }
    }

    public class SubscriptionExpiredException : BusinessLogicException
    {
        public SubscriptionExpiredException() : base("User subscription has expired") { }
    }
}
