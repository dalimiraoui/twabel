using Twabel.CrossCutting;

namespace Twabel.CrossCutting.Exceptions
{
    public class InvalidModelException : BusinessLogicException, IReturnErrorClient
    {
        public InvalidModelException(string message, TwabelError TwabelError) : base(message, TwabelError)
        {

        }

        public InvalidModelException(string message) : base(message, TwabelError.Twabel_ERROR)
        {

        }
    }

}
/*
this code defines a custom exception class that is specific to invalid model data in business logic operations, 
and inherits from another custom exception class (BusinessLogicException) to provide additional functionality related to error handling.

*/
