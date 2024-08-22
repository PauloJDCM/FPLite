using System.Diagnostics.CodeAnalysis;
using FPLite.Option;
using FPLite.Result;
using FPLite.Either;
using FPLite.Union;

namespace FPLite;

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