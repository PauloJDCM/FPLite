using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using FPLite.Union;

namespace FPLite.Option;

public sealed class OptionUnwrapException<T> : UnwrapException
{
    private const string ErrorMessage = "Called Option<{0}>.Unwrap() on None!";

    public OptionUnwrapException() : base(string.Format(ErrorMessage, typeof(T)))
    {
    }
}

public enum OptionType : byte
{
    NotSet,
    None,
    Some
}

public readonly record struct Option<T>(T? Value = default, OptionType Type = OptionType.NotSet)
    where T : notnull
{
    [Pure]
    public static Option<T> Some([DisallowNull] T value) => new(value, OptionType.Some);

    [Pure]
    public static Option<T> None() => new(Type: OptionType.None);

    [Pure]
    public TResult Match<TResult>(Func<T, TResult> someFunc, Func<TResult> noneFunc) => Type switch
    {
        OptionType.Some => someFunc(Value!),
        OptionType.None => noneFunc(),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };

    public void Match(Action<T> someAct, Action noneAct)
    {
        switch (Type)
        {
            case OptionType.Some:
                someAct(Value!);
                break;
            case OptionType.None:
                noneAct();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Type), Type,
                    $"{GetType()} does not support {Type.ToString()}!");
        }
    }

    [Pure]
    public Option<TResult> Bind<TResult>(Func<T, TResult> someFunc)
        where TResult : notnull =>
        Type switch
        {
            OptionType.Some => new(someFunc(Value!), OptionType.Some),
            OptionType.None => new(Type: OptionType.None),
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
                $"{GetType()} does not support {Type.ToString()}!")
        };

    [Pure]
    public T Unwrap() => Type switch
    {
        OptionType.Some => Value!,
        OptionType.None => throw new OptionUnwrapException<T>(),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };

    [Pure]
    public T UnwrapOr(Func<T> func) => Type switch
    {
        OptionType.Some => Value!,
        OptionType.None => func(),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };

    [Pure]
    public Union<T, TOr> UnwrapOr<TOr>(Func<TOr> func)
        where TOr : notnull =>
        Type switch
        {
            OptionType.Some => new(V1: Value!, Type: UnionType.T1),
            OptionType.None => new(V2: func(), Type: UnionType.T2),
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
                $"{GetType()} does not support {Type.ToString()}!")
        };
}