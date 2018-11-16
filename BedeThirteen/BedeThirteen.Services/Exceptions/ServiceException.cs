namespace BedeThirteen.Services.Exceptions
{
    using System;

    [Serializable]
    public class ServiceException : Exception
    {
        public ServiceException()
        {
        }

        public ServiceException(string message)
            : base(message)
        {
        }
    }
}
