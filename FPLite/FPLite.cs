using System;
using System.Diagnostics.CodeAnalysis;
using FPLite.Option;
using FPLite.Result;
using FPLite.Either;
using FPLite.Union;

namespace FPLite;

public static class FPLite
{
    #region Option

    public static IOption<T> Some<T>([DisallowNull] T value) => new Some<T>(value);
    public static IOption<T> None<T>() => new None<T>();

    #endregion

    #region Result

    public static IResult<T, TError> Ok<T, TError>([DisallowNull] T value) => new Ok<T, TError>(value);

    public static IResult<T, TError> Err<T, TError>([DisallowNull] TError error) => new Err<T, TError>(error);

    #endregion

    #region Either

    public static IEither<TLeft, TRight> Left<TLeft, TRight>([DisallowNull] TLeft value) =>
        new Left<TLeft, TRight>(value);

    public static IEither<TLeft, TRight> Right<TLeft, TRight>([DisallowNull] TRight value) =>
        new Right<TLeft, TRight>(value);

    public static IEither<TLeft, TRight> Neither<TLeft, TRight>() => new Neither<TLeft, TRight>();

    public static IEither<TLeft, TRight> Both<TLeft, TRight>([DisallowNull] TLeft left, [DisallowNull] TRight right) =>
        new Both<TLeft, TRight>(left, right);

    #endregion

    #region Union2

    public static IUnion<T1, T2> T1<T1, T2>([DisallowNull] T1 value) => new UnionT1<T1, T2>(value);
    public static IUnion<T1, T2> T2<T1, T2>([DisallowNull] T2 value) => new UnionT2<T1, T2>(value);

    #endregion

    #region Union3

    public static IUnion<T1, T2, T3> T1<T1, T2, T3>([DisallowNull] T1 value) => new UnionT1<T1, T2, T3>(value);
    public static IUnion<T1, T2, T3> T2<T1, T2, T3>([DisallowNull] T2 value) => new UnionT2<T1, T2, T3>(value);
    public static IUnion<T1, T2, T3> T3<T1, T2, T3>([DisallowNull] T3 value) => new UnionT3<T1, T2, T3>(value);

    #endregion

    #region Union4

    public static IUnion<T1, T2, T3, T4> T1<T1, T2, T3, T4>([DisallowNull] T1 value) =>
        new UnionT1<T1, T2, T3, T4>(value);

    public static IUnion<T1, T2, T3, T4> T2<T1, T2, T3, T4>([DisallowNull] T2 value) =>
        new UnionT2<T1, T2, T3, T4>(value);

    public static IUnion<T1, T2, T3, T4> T3<T1, T2, T3, T4>([DisallowNull] T3 value) =>
        new UnionT3<T1, T2, T3, T4>(value);

    public static IUnion<T1, T2, T3, T4> T4<T1, T2, T3, T4>([DisallowNull] T4 value) =>
        new UnionT4<T1, T2, T3, T4>(value);

    #endregion

    #region Union5

    public static IUnion<T1, T2, T3, T4, T5> T1<T1, T2, T3, T4, T5>([DisallowNull] T1 value) =>
        new UnionT1<T1, T2, T3, T4, T5>(value);

    public static IUnion<T1, T2, T3, T4, T5> T2<T1, T2, T3, T4, T5>([DisallowNull] T2 value) =>
        new UnionT2<T1, T2, T3, T4, T5>(value);

    public static IUnion<T1, T2, T3, T4, T5> T3<T1, T2, T3, T4, T5>([DisallowNull] T3 value) =>
        new UnionT3<T1, T2, T3, T4, T5>(value);

    public static IUnion<T1, T2, T3, T4, T5> T4<T1, T2, T3, T4, T5>([DisallowNull] T4 value) =>
        new UnionT4<T1, T2, T3, T4, T5>(value);

    public static IUnion<T1, T2, T3, T4, T5> T5<T1, T2, T3, T4, T5>([DisallowNull] T5 value) =>
        new UnionT5<T1, T2, T3, T4, T5>(value);

    #endregion

    #region Union6

    public static IUnion<T1, T2, T3, T4, T5, T6> T1<T1, T2, T3, T4, T5, T6>([DisallowNull] T1 value) =>
        new UnionT1<T1, T2, T3, T4, T5, T6>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6> T2<T1, T2, T3, T4, T5, T6>([DisallowNull] T2 value) =>
        new UnionT2<T1, T2, T3, T4, T5, T6>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6> T3<T1, T2, T3, T4, T5, T6>([DisallowNull] T3 value) =>
        new UnionT3<T1, T2, T3, T4, T5, T6>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6> T4<T1, T2, T3, T4, T5, T6>([DisallowNull] T4 value) =>
        new UnionT4<T1, T2, T3, T4, T5, T6>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6> T5<T1, T2, T3, T4, T5, T6>([DisallowNull] T5 value) =>
        new UnionT5<T1, T2, T3, T4, T5, T6>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6> T6<T1, T2, T3, T4, T5, T6>([DisallowNull] T6 value) =>
        new UnionT6<T1, T2, T3, T4, T5, T6>(value);

    #endregion

    #region Union7

    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T1<T1, T2, T3, T4, T5, T6, T7>([DisallowNull] T1 value) =>
        new UnionT1<T1, T2, T3, T4, T5, T6, T7>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T2<T1, T2, T3, T4, T5, T6, T7>([DisallowNull] T2 value) =>
        new UnionT2<T1, T2, T3, T4, T5, T6, T7>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T3<T1, T2, T3, T4, T5, T6, T7>([DisallowNull] T3 value) =>
        new UnionT3<T1, T2, T3, T4, T5, T6, T7>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T4<T1, T2, T3, T4, T5, T6, T7>([DisallowNull] T4 value) =>
        new UnionT4<T1, T2, T3, T4, T5, T6, T7>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T5<T1, T2, T3, T4, T5, T6, T7>([DisallowNull] T5 value) =>
        new UnionT5<T1, T2, T3, T4, T5, T6, T7>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T6<T1, T2, T3, T4, T5, T6, T7>([DisallowNull] T6 value) =>
        new UnionT6<T1, T2, T3, T4, T5, T6, T7>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T7<T1, T2, T3, T4, T5, T6, T7>([DisallowNull] T7 value) =>
        new UnionT7<T1, T2, T3, T4, T5, T6, T7>(value);

    #endregion

    #region Union8

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T1<T1, T2, T3, T4, T5, T6, T7, T8>([DisallowNull] T1 value) =>
        new UnionT1<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T2<T1, T2, T3, T4, T5, T6, T7, T8>([DisallowNull] T2 value) =>
        new UnionT2<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T3<T1, T2, T3, T4, T5, T6, T7, T8>([DisallowNull] T3 value) =>
        new UnionT3<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T4<T1, T2, T3, T4, T5, T6, T7, T8>([DisallowNull] T4 value) =>
        new UnionT4<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T5<T1, T2, T3, T4, T5, T6, T7, T8>([DisallowNull] T5 value) =>
        new UnionT5<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T6<T1, T2, T3, T4, T5, T6, T7, T8>([DisallowNull] T6 value) =>
        new UnionT6<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T7<T1, T2, T3, T4, T5, T6, T7, T8>([DisallowNull] T7 value) =>
        new UnionT7<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T8<T1, T2, T3, T4, T5, T6, T7, T8>([DisallowNull] T8 value) =>
        new UnionT8<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    #endregion
}