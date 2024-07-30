namespace WebApplication2.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string resourceName)
            : base($"{resourceName} not found.")
        {
        }
    }
}
