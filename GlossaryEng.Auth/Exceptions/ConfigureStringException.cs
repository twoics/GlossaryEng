namespace GlossaryEng.Auth.Exceptions;

public class ConfigureStringException : Exception
{
    public ConfigureStringException()
    {
    }

    public ConfigureStringException(string message) : base(message)
    {
    }

    public ConfigureStringException(string message, Exception inner) : base(message, inner)
    {
    }
}