namespace Codebridge.Business.Validation
{
    public class CodebridgeException : Exception
    {
        public CodebridgeException()
        {
        }

        public CodebridgeException(string message)
            : base(message)
        {
        }

        public CodebridgeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
