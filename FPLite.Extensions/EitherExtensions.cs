using System;
using System.Threading;
using System.Threading.Tasks;
using FPLite.Either;

namespace FPLite.Extensions;

public static class EitherExtensions
{
    /// <summary>
    /// Tries to execute a function and returns an <see cref="Either{TLeft, TRight}"/> with the result.
    /// </summary>
    /// <returns>
    /// Returns with type <see cref="EitherType.Neither"/> if no exception is thrown.
    /// <br/>
    /// Returns with type <see cref="EitherType.Left"/> if an exception of type <typeparamref name="TException"/> is thrown.
    /// <br/>
    /// Returns with type <see cref="EitherType.Right"/> if another exception is thrown.
    /// </returns>
    public static Either<TException, Exception> TryEither<TException>(Action action)
        where TException : Exception
    {
        try
        {
            action();
            return Either<TException, Exception>.Neither();
        }
        catch (TException e)
        {
            return Either<TException, Exception>.Left(e);
        }
        catch (Exception e)
        {
            return Either<TException, Exception>.Right(e);
        }
    }

    /// <summary>
    /// Tries to execute an async function and returns an <see cref="Either{TLeft, TRight}"/> with the result.
    /// </summary>
    /// <returns>
    /// Returns with type <see cref="EitherType.Neither"/> if no exception is thrown.
    /// <br/>
    /// Returns with type <see cref="EitherType.Left"/> if an exception of type <typeparamref name="TException"/> is thrown.
    /// <br/>
    /// Returns with type <see cref="EitherType.Right"/> if another exception is thrown.
    /// </returns>
    public static async ValueTask<Either<TException, Exception>> TryEitherAsync<TException>(
        Func<CancellationToken, ValueTask> func, CancellationToken ct = default)
        where TException : Exception
    {
        try
        {
            await func(ct);
            return Either<TException, Exception>.Neither();
        }
        catch (TException e)
        {
            return Either<TException, Exception>.Left(e);
        }
        catch (Exception e)
        {
            return Either<TException, Exception>.Right(e);
        }
    }
}