using System;
using System.Diagnostics.CodeAnalysis;
using FPLite.Option;
using FPLite.Result;
using FPLite.Either;
using FPLite.Union;

namespace FPLite;

#region InterfacesAndTypes

public abstract class UnwrapException : Exception
{
    protected UnwrapException(string message) : base(message)
    {
    }
}

#region Option

public interface IOption<T>
{
    bool IsSome { get; }

    TResult Match<TResult>(Func<T, TResult> someFunc, Func<TResult> noneFunc);
    void Match(Action<T> someAct, Action noneAct);

    IOption<TResult> Bind<TResult>(Func<T, TResult> someFunc);

    T Unwrap();
    T UnwrapOr(Func<T> func);
    IUnion<T, TOr> UnwrapOr<TOr>(Func<TOr> func);
}

#endregion

#region Result

public interface IResult<T, out TError>
{
    bool IsOk { get; }

    TResult Match<TResult>(Func<T, TResult> okFunc, Func<TError, TResult> errFunc);
    void Match(Action<T> okAct, Action<TError> errAct);

    IResult<TResult, TError> Bind<TResult>(Func<T, TResult> okFunc);

    T Unwrap();
    T UnwrapOr(Func<TError, T> func);
    IUnion<T, TOr> UnwrapOr<TOr>(Func<TError, TOr> func);
}

#endregion

#region Either

public enum EitherType : byte
{
    Neither,
    Left,
    Right,
    Both
}

public interface IEither<out TLeft, out TRight>
{
    EitherType Type { get; }

    TResult Match<TResult>(Func<TLeft, TResult> leftFunc, Func<TRight, TResult> rightFunc,
        Func<TResult> neitherFunc, Func<TLeft, TRight, TResult> bothFunc);

    IEither<TResultLeft, TResultRight> Match<TResultLeft, TResultRight>(Func<TLeft, TResultLeft> leftFunc,
        Func<TRight, TResultRight> rightFunc, Func<IEither<TResultLeft, TResultRight>> neitherFunc,
        Func<TLeft, TRight, IEither<TResultLeft, TResultRight>> bothFunc);

    void Match(Action<TLeft> leftAct, Action<TRight> rightAct, Action neitherAct, Action<TLeft, TRight> bothAct);
}

#endregion

#region Union

public enum UnionType : byte
{
    T1,
    T2,
    T3,
    T4,
    T5,
    T6,
    T7,
    T8
}

public interface IUnion<out T1, out T2>
{
    UnionType Type { get; }

    TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func);
    IUnion<T1Result, T2Result> Match<TResult, T1Result, T2Result>(Func<T1, T1Result> t1Func, Func<T2, T2Result> t2Func);
    void Match(Action<T1> t1Act, Action<T2> t2Act);
}

public interface IUnion<out T1, out T2, out T3>
{
    UnionType Type { get; }

    TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func);

    IUnion<T1Result, T2Result, T3Result> Match<TResult, T1Result, T2Result, T3Result>(Func<T1, T1Result> t1Func,
        Func<T2, T2Result> t2Func, Func<T3, T3Result> t3Func);

    void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act);
}

public interface IUnion<out T1, out T2, out T3, out T4>
{
    UnionType Type { get; }

    TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func);

    IUnion<T1Result, T2Result, T3Result, T4Result> Match<TResult, T1Result, T2Result, T3Result, T4Result>(
        Func<T1, T1Result> t1Func, Func<T2, T2Result> t2Func, Func<T3, T3Result> t3Func, Func<T4, T4Result> t4Func);

    void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act);
}

public interface IUnion<out T1, out T2, out T3, out T4, out T5>
{
    UnionType Type { get; }

    TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func);

    IUnion<T1Result, T2Result, T3Result, T4Result, T5Result> Match<T1Result, T2Result, T3Result, T4Result, T5Result>(
        Func<T1, T1Result> t1Func, Func<T2, T2Result> t2Func, Func<T3, T3Result> t3Func, Func<T4, T4Result> t4Func,
        Func<T5, T5Result> t5Func);

    void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act);
}

public interface IUnion<out T1, out T2, out T3, out T4, out T5, out T6>
{
    UnionType Type { get; }

    TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func);

    IUnion<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result> Match<T1Result, T2Result, T3Result, T4Result,
        T5Result, T6Result>(Func<T1, T1Result> t1Func, Func<T2, T2Result> t2Func, Func<T3, T3Result> t3Func,
        Func<T4, T4Result> t4Func, Func<T5, T5Result> t5Func, Func<T6, T6Result> t6Func);

    void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act);
}

public interface IUnion<out T1, out T2, out T3, out T4, out T5, out T6, out T7>
{
    UnionType Type { get; }

    TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func);

    IUnion<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result, T7Result> Match<T1Result, T2Result, T3Result,
        T4Result, T5Result, T6Result, T7Result>(Func<T1, T1Result> t1Func, Func<T2, T2Result> t2Func,
        Func<T3, T3Result> t3Func, Func<T4, T4Result> t4Func, Func<T5, T5Result> t5Func, Func<T6, T6Result> t6Func,
        Func<T7, T7Result> t7Func);

    void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act);
}

public interface IUnion<out T1, out T2, out T3, out T4, out T5, out T6, out T7, out T8>
{
    UnionType Type { get; }

    TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func,
        Func<T8, TResult> t8Func);

    IUnion<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result, T7Result, T8Result> Match<T1Result, T2Result,
        T3Result, T4Result, T5Result, T6Result, T7Result, T8Result>(Func<T1, T1Result> t1Func,
        Func<T2, T2Result> t2Func, Func<T3, T3Result> t3Func, Func<T4, T4Result> t4Func, Func<T5, T5Result> t5Func,
        Func<T6, T6Result> t6Func, Func<T7, T7Result> t7Func, Func<T8, T8Result> t8Func);

    void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act, Action<T8> t8Act);
}

#endregion

#endregion

public static class FPLite
{
    #region Option

    public static IOption<T> Some<T>([DisallowNull] T value) where T : notnull => new Some<T>(value);
    public static IOption<T> None<T>() where T : notnull => new None<T>();

    #endregion

    #region Result

    public static IResult<T, TError> Ok<T, TError>([DisallowNull] T value)
        where T : notnull where TError : notnull =>
        new Ok<T, TError>(value);

    public static IResult<T, TError> Err<T, TError>([DisallowNull] TError error)
        where T : notnull where TError : notnull =>
        new Err<T, TError>(error);

    #endregion

    #region Either

    public static IEither<TLeft, TRight> Left<TLeft, TRight>([DisallowNull] TLeft value)
        where TLeft : notnull where TRight : notnull =>
        new Left<TLeft, TRight>(value);

    public static IEither<TLeft, TRight> Right<TLeft, TRight>([DisallowNull] TRight value)
        where TLeft : notnull where TRight : notnull =>
        new Right<TLeft, TRight>(value);

    public static IEither<TLeft, TRight> Neither<TLeft, TRight>()
        where TLeft : notnull where TRight : notnull =>
        new Neither<TLeft, TRight>();

    public static IEither<TLeft, TRight> Both<TLeft, TRight>([DisallowNull] TLeft left, [DisallowNull] TRight right)
        where TLeft : notnull where TRight : notnull =>
        new Both<TLeft, TRight>(left, right);

    #endregion

    #region Union2

    public static IUnion<T1, T2> T1<T1, T2>([DisallowNull] T1 value)
        where T1 : notnull where T2 : notnull =>
        new UnionT1<T1, T2>(value);

    public static IUnion<T1, T2> T2<T1, T2>([DisallowNull] T2 value)
        where T1 : notnull where T2 : notnull =>
        new UnionT2<T1, T2>(value);

    #endregion

    #region Union3

    public static IUnion<T1, T2, T3> T1<T1, T2, T3>([DisallowNull] T1 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull =>
        new UnionT1<T1, T2, T3>(value);

    public static IUnion<T1, T2, T3> T2<T1, T2, T3>([DisallowNull] T2 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull =>
        new UnionT2<T1, T2, T3>(value);

    public static IUnion<T1, T2, T3> T3<T1, T2, T3>([DisallowNull] T3 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull =>
        new UnionT3<T1, T2, T3>(value);

    #endregion

    #region Union4

    public static IUnion<T1, T2, T3, T4> T1<T1, T2, T3, T4>([DisallowNull] T1 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull =>
        new UnionT1<T1, T2, T3, T4>(value);

    public static IUnion<T1, T2, T3, T4> T2<T1, T2, T3, T4>([DisallowNull] T2 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull =>
        new UnionT2<T1, T2, T3, T4>(value);

    public static IUnion<T1, T2, T3, T4> T3<T1, T2, T3, T4>([DisallowNull] T3 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull =>
        new UnionT3<T1, T2, T3, T4>(value);

    public static IUnion<T1, T2, T3, T4> T4<T1, T2, T3, T4>([DisallowNull] T4 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull =>
        new UnionT4<T1, T2, T3, T4>(value);

    #endregion

    #region Union5

    public static IUnion<T1, T2, T3, T4, T5> T1<T1, T2, T3, T4, T5>([DisallowNull] T1 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull =>
        new UnionT1<T1, T2, T3, T4, T5>(value);

    public static IUnion<T1, T2, T3, T4, T5> T2<T1, T2, T3, T4, T5>([DisallowNull] T2 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull =>
        new UnionT2<T1, T2, T3, T4, T5>(value);

    public static IUnion<T1, T2, T3, T4, T5> T3<T1, T2, T3, T4, T5>([DisallowNull] T3 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull =>
        new UnionT3<T1, T2, T3, T4, T5>(value);

    public static IUnion<T1, T2, T3, T4, T5> T4<T1, T2, T3, T4, T5>([DisallowNull] T4 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull =>
        new UnionT4<T1, T2, T3, T4, T5>(value);

    public static IUnion<T1, T2, T3, T4, T5> T5<T1, T2, T3, T4, T5>([DisallowNull] T5 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull =>
        new UnionT5<T1, T2, T3, T4, T5>(value);

    #endregion

    #region Union6

    public static IUnion<T1, T2, T3, T4, T5, T6> T1<T1, T2, T3, T4, T5, T6>([DisallowNull] T1 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull =>
        new UnionT1<T1, T2, T3, T4, T5, T6>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6> T2<T1, T2, T3, T4, T5, T6>([DisallowNull] T2 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull =>
        new UnionT2<T1, T2, T3, T4, T5, T6>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6> T3<T1, T2, T3, T4, T5, T6>([DisallowNull] T3 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull =>
        new UnionT3<T1, T2, T3, T4, T5, T6>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6> T4<T1, T2, T3, T4, T5, T6>([DisallowNull] T4 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull =>
        new UnionT4<T1, T2, T3, T4, T5, T6>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6> T5<T1, T2, T3, T4, T5, T6>([DisallowNull] T5 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull =>
        new UnionT5<T1, T2, T3, T4, T5, T6>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6> T6<T1, T2, T3, T4, T5, T6>([DisallowNull] T6 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull =>
        new UnionT6<T1, T2, T3, T4, T5, T6>(value);

    #endregion

    #region Union7

    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T1<T1, T2, T3, T4, T5, T6, T7>([DisallowNull] T1 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull =>
        new UnionT1<T1, T2, T3, T4, T5, T6, T7>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T2<T1, T2, T3, T4, T5, T6, T7>([DisallowNull] T2 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull =>
        new UnionT2<T1, T2, T3, T4, T5, T6, T7>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T3<T1, T2, T3, T4, T5, T6, T7>([DisallowNull] T3 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull =>
        new UnionT3<T1, T2, T3, T4, T5, T6, T7>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T4<T1, T2, T3, T4, T5, T6, T7>([DisallowNull] T4 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull =>
        new UnionT4<T1, T2, T3, T4, T5, T6, T7>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T5<T1, T2, T3, T4, T5, T6, T7>([DisallowNull] T5 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull =>
        new UnionT5<T1, T2, T3, T4, T5, T6, T7>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T6<T1, T2, T3, T4, T5, T6, T7>([DisallowNull] T6 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull =>
        new UnionT6<T1, T2, T3, T4, T5, T6, T7>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T7<T1, T2, T3, T4, T5, T6, T7>([DisallowNull] T7 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull =>
        new UnionT7<T1, T2, T3, T4, T5, T6, T7>(value);

    #endregion

    #region Union8

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T1<T1, T2, T3, T4, T5, T6, T7, T8>([DisallowNull] T1 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull =>
        new UnionT1<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T2<T1, T2, T3, T4, T5, T6, T7, T8>([DisallowNull] T2 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull =>
        new UnionT2<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T3<T1, T2, T3, T4, T5, T6, T7, T8>([DisallowNull] T3 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull =>
        new UnionT3<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T4<T1, T2, T3, T4, T5, T6, T7, T8>([DisallowNull] T4 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull =>
        new UnionT4<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T5<T1, T2, T3, T4, T5, T6, T7, T8>([DisallowNull] T5 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull =>
        new UnionT5<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T6<T1, T2, T3, T4, T5, T6, T7, T8>([DisallowNull] T6 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull =>
        new UnionT6<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T7<T1, T2, T3, T4, T5, T6, T7, T8>([DisallowNull] T7 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull =>
        new UnionT7<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T8<T1, T2, T3, T4, T5, T6, T7, T8>([DisallowNull] T8 value)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull =>
        new UnionT8<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    #endregion
}