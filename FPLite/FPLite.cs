using FPLite.Option;
using FPLite.Result;
using FPLite.Either;
using FPLite.Union;

namespace FPLite;

public static class FPLite
{
    #region Option

    public static IOption<T> Some<T>(T value) => new Some<T>(value);
    public static IOption<T> None<T>() => new None<T>();

    #endregion

    #region Result

    public static IResult<T, TError> Ok<T, TError>(T value) => new Ok<T, TError>(value);
    public static IResult<T, TError> Err<T, TError>(TError error) => new Err<T, TError>(error);

    #endregion

    #region Either

    public static IEither<T, TError> Left<T, TError>(T value) => new Left<T, TError>(value);
    public static IEither<T, TError> Right<T, TError>(TError error) => new Right<T, TError>(error);
    public static IEither<T, TError> Both<T, TError>(T value, TError error) => new Both<T, TError>(value, error);
    public static IEither<T, TError> Neither<T, TError>() => new Neither<T, TError>();

    #endregion

    #region Union2

    public static IUnion<T1, T2> T1<T1, T2>(T1 value) => new UnionT1<T1, T2>(value);
    public static IUnion<T1, T2> T2<T1, T2>(T2 value) => new UnionT2<T1, T2>(value);

    #endregion

    #region Union3

    public static IUnion<T1, T2, T3> T1<T1, T2, T3>(T1 value) => new UnionT1<T1, T2, T3>(value);
    public static IUnion<T1, T2, T3> T2<T1, T2, T3>(T2 value) => new UnionT2<T1, T2, T3>(value);
    public static IUnion<T1, T2, T3> T3<T1, T2, T3>(T3 value) => new UnionT3<T1, T2, T3>(value);

    #endregion

    #region Union4

    public static IUnion<T1, T2, T3, T4> T1<T1, T2, T3, T4>(T1 value) => new UnionT1<T1, T2, T3, T4>(value);
    public static IUnion<T1, T2, T3, T4> T2<T1, T2, T3, T4>(T2 value) => new UnionT2<T1, T2, T3, T4>(value);
    public static IUnion<T1, T2, T3, T4> T3<T1, T2, T3, T4>(T3 value) => new UnionT3<T1, T2, T3, T4>(value);
    public static IUnion<T1, T2, T3, T4> T4<T1, T2, T3, T4>(T4 value) => new UnionT4<T1, T2, T3, T4>(value);

    #endregion

    #region Union5

    public static IUnion<T1, T2, T3, T4, T5> T1<T1, T2, T3, T4, T5>(T1 value) =>
        new UnionT1<T1, T2, T3, T4, T5>(value);

    public static IUnion<T1, T2, T3, T4, T5> T2<T1, T2, T3, T4, T5>(T2 value) =>
        new UnionT2<T1, T2, T3, T4, T5>(value);

    public static IUnion<T1, T2, T3, T4, T5> T3<T1, T2, T3, T4, T5>(T3 value) =>
        new UnionT3<T1, T2, T3, T4, T5>(value);

    public static IUnion<T1, T2, T3, T4, T5> T4<T1, T2, T3, T4, T5>(T4 value) =>
        new UnionT4<T1, T2, T3, T4, T5>(value);

    public static IUnion<T1, T2, T3, T4, T5> T5<T1, T2, T3, T4, T5>(T5 value) =>
        new UnionT5<T1, T2, T3, T4, T5>(value);

    #endregion

    #region Union6

    public static IUnion<T1, T2, T3, T4, T5, T6> T1<T1, T2, T3, T4, T5, T6>(T1 value) =>
        new UnionT1<T1, T2, T3, T4, T5, T6>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6> T2<T1, T2, T3, T4, T5, T6>(T2 value) =>
        new UnionT2<T1, T2, T3, T4, T5, T6>(value);
    
    public static IUnion<T1, T2, T3, T4, T5, T6> T3<T1, T2, T3, T4, T5, T6>(T3 value) =>
        new UnionT3<T1, T2, T3, T4, T5, T6>(value);
    
    public static IUnion<T1, T2, T3, T4, T5, T6> T4<T1, T2, T3, T4, T5, T6>(T4 value) =>
        new UnionT4<T1, T2, T3, T4, T5, T6>(value);
    
    public static IUnion<T1, T2, T3, T4, T5, T6> T5<T1, T2, T3, T4, T5, T6>(T5 value) =>
        new UnionT5<T1, T2, T3, T4, T5, T6>(value);
    
    public static IUnion<T1, T2, T3, T4, T5, T6> T6<T1, T2, T3, T4, T5, T6>(T6 value) =>
        new UnionT6<T1, T2, T3, T4, T5, T6>(value);

    #endregion

    #region Union7

    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T1<T1, T2, T3, T4, T5, T6, T7>(T1 value) =>
        new UnionT1<T1, T2, T3, T4, T5, T6, T7>(value);
    
    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T2<T1, T2, T3, T4, T5, T6, T7>(T2 value) =>
        new UnionT2<T1, T2, T3, T4, T5, T6, T7>(value);
    
    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T3<T1, T2, T3, T4, T5, T6, T7>(T3 value) =>
        new UnionT3<T1, T2, T3, T4, T5, T6, T7>(value);
    
    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T4<T1, T2, T3, T4, T5, T6, T7>(T4 value) =>
        new UnionT4<T1, T2, T3, T4, T5, T6, T7>(value);
    
    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T5<T1, T2, T3, T4, T5, T6, T7>(T5 value) =>
        new UnionT5<T1, T2, T3, T4, T5, T6, T7>(value);
    
    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T6<T1, T2, T3, T4, T5, T6, T7>(T6 value) =>
        new UnionT6<T1, T2, T3, T4, T5, T6, T7>(value);

    public static IUnion<T1, T2, T3, T4, T5, T6, T7> T7<T1, T2, T3, T4, T5, T6, T7>(T7 value) =>
        new UnionT7<T1, T2, T3, T4, T5, T6, T7>(value);

    #endregion

    #region Union8

    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T1<T1, T2, T3, T4, T5, T6, T7, T8>(T1 value) =>
        new UnionT1<T1, T2, T3, T4, T5, T6, T7, T8>(value);
    
    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T2<T1, T2, T3, T4, T5, T6, T7, T8>(T2 value) =>
        new UnionT2<T1, T2, T3, T4, T5, T6, T7, T8>(value);
    
    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T3<T1, T2, T3, T4, T5, T6, T7, T8>(T3 value) =>
        new UnionT3<T1, T2, T3, T4, T5, T6, T7, T8>(value);
    
    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T4<T1, T2, T3, T4, T5, T6, T7, T8>(T4 value) =>
        new UnionT4<T1, T2, T3, T4, T5, T6, T7, T8>(value);
    
    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T5<T1, T2, T3, T4, T5, T6, T7, T8>(T5 value) =>
        new UnionT5<T1, T2, T3, T4, T5, T6, T7, T8>(value);
    
    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T6<T1, T2, T3, T4, T5, T6, T7, T8>(T6 value) =>
        new UnionT6<T1, T2, T3, T4, T5, T6, T7, T8>(value);
    
    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T7<T1, T2, T3, T4, T5, T6, T7, T8>(T7 value) =>
        new UnionT7<T1, T2, T3, T4, T5, T6, T7, T8>(value);
    
    public static IUnion<T1, T2, T3, T4, T5, T6, T7, T8> T8<T1, T2, T3, T4, T5, T6, T7, T8>(T8 value) =>
        new UnionT8<T1, T2, T3, T4, T5, T6, T7, T8>(value);

    #endregion
}