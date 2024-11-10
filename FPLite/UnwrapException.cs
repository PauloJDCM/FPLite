using System;

namespace FPLite;

public abstract class UnwrapException : Exception
{
    protected UnwrapException(string message) : base(message)
    {
    }
}