using System.Runtime.Serialization;

namespace EduHome.UI.Areas.Admin.Data.Exception
{
    [Serializable]
    internal class NotFoundException : IOException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }

        public NotFoundException(string? message, IOException? innerException) : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}