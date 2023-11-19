#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Diagnostics.CodeAnalysis;

namespace FPLite.Types
{
    /// <summary>
    /// Represents a discriminated union with two possible cases.
    /// </summary>
    public class Union<T1, T2>
    {
        protected byte Index;
        private readonly T1 _t1;
        private readonly T2 _t2;

        protected Union()
        {
        }

        public Union([DisallowNull] T1 t1)
        {
            Index = 1;
            _t1 = t1;
        }

        public Union([DisallowNull] T2 t2)
        {
            Index = 2;
            _t2 = t2;
        }

        /// <summary>
        /// Matches the active case and invokes the appropriate delegate.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>The result of the invoked delegate.</returns>
        public TResult Match<TResult>(Func<T1, TResult> case1, Func<T2, TResult> case2) => Index switch
        {
            1 => case1(_t1),
            2 => case2(_t2),
            _ => throw new ArgumentNullException($"Possible null instantiation in class {GetType().Name}")
        };

        /// <summary>
        /// Matches the active case and invokes the appropriate action.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Match(Action<T1> case1, Action<T2> case2)
        {
            switch (Index)
            {
                case 1:
                    case1(_t1);
                    break;
                case 2:
                    case2(_t2);
                    break;
                default:
                    throw new ArgumentNullException($"Possible null instantiation in class {GetType().Name}");
            }
        }
    }

    /// <summary>
    /// Represents a discriminated union with three possible cases.
    /// </summary>
    public class Union<T1, T2, T3> : Union<T1, T2>
    {
        private readonly T3 _t3;

        protected Union()
        {
        }

        public Union([DisallowNull] T3 t3)
        {
            Index = 3;
            _t3 = t3;
        }

        /// <summary>
        /// Matches the active case and invokes the appropriate delegate.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <returns>The result of the invoked delegate.</returns>
        public TResult Match<TResult>(Func<T1, TResult> case1, Func<T2, TResult> case2,
            Func<T3, TResult> case3) => Index switch
        {
            3 => case3(_t3),
            _ => base.Match(case1, case2)
        };

        /// <summary>
        /// Matches the active case and invokes the appropriate action.
        /// </summary>
        public void Match(Action<T1> case1, Action<T2> case2, Action<T3> case3)
        {
            switch (Index)
            {
                case 3:
                    case3(_t3);
                    break;
                default:
                    base.Match(case1, case2);
                    break;
            }
        }
    }

    /// <summary>
    /// Represents a discriminated union with four possible cases.
    /// </summary>
    public class Union<T1, T2, T3, T4> : Union<T1, T2, T3>
    {
        private readonly T4 _t4;

        protected Union()
        {
        }

        public Union([DisallowNull] T4 t4)
        {
            Index = 4;
            _t4 = t4;
        }

        /// <summary>
        /// Matches the active case and invokes the appropriate delegate.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <returns>The result of the invoked delegate.</returns>
        public TResult Match<TResult>(Func<T1, TResult> case1, Func<T2, TResult> case2,
            Func<T3, TResult> case3, Func<T4, TResult> case4) => Index switch
        {
            4 => case4(_t4),
            _ => base.Match(case1, case2, case3)
        };

        /// <summary>
        /// Matches the active case and invokes the appropriate action.
        /// </summary>
        public void Match(Action<T1> case1, Action<T2> case2, Action<T3> case3, Action<T4> case4)
        {
            switch (Index)
            {
                case 4:
                    case4(_t4);
                    break;
                default:
                    base.Match(case1, case2, case3);
                    break;
            }
        }
    }

    /// <summary>
    /// Represents a discriminated union with five possible cases.
    /// </summary>
    public class Union<T1, T2, T3, T4, T5> : Union<T1, T2, T3, T4>
    {
        private readonly T5 _t5;

        protected Union()
        {
        }

        public Union([DisallowNull] T5 t5)
        {
            Index = 5;
            _t5 = t5;
        }

        /// <summary>
        /// Matches the active case and invokes the appropriate delegate.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <returns>The result of the invoked delegate.</returns>
        public TResult Match<TResult>(Func<T1, TResult> case1, Func<T2, TResult> case2,
            Func<T3, TResult> case3, Func<T4, TResult> case4, Func<T5, TResult> case5) => Index switch
        {
            5 => case5(_t5),
            _ => base.Match(case1, case2, case3, case4)
        };

        /// <summary>
        /// Matches the active case and invokes the appropriate action.
        /// </summary>
        public void Match(Action<T1> case1, Action<T2> case2, Action<T3> case3, Action<T4> case4, Action<T5> case5)
        {
            switch (Index)
            {
                case 5:
                    case5(_t5);
                    break;
                default:
                    base.Match(case1, case2, case3, case4);
                    break;
            }
        }
    }

    /// <summary>
    /// Represents a discriminated union with six possible cases.
    /// </summary>
    public class Union<T1, T2, T3, T4, T5, T6> : Union<T1, T2, T3, T4, T5>
    {
        private readonly T6 _t6;

        protected Union()
        {
        }

        public Union([DisallowNull] T6 t6)
        {
            Index = 6;
            _t6 = t6;
        }

        /// <summary>
        /// Matches the active case and invokes the appropriate delegate.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <returns>The result of the invoked delegate.</returns>
        public TResult Match<TResult>(Func<T1, TResult> case1, Func<T2, TResult> case2,
            Func<T3, TResult> case3, Func<T4, TResult> case4, Func<T5, TResult> case5, Func<T6, TResult> case6) =>
            Index switch
            {
                6 => case6(_t6),
                _ => base.Match(case1, case2, case3, case4, case5)
            };

        /// <summary>
        /// Matches the active case and invokes the appropriate action.
        /// </summary>
        public void Match(Action<T1> case1, Action<T2> case2, Action<T3> case3, Action<T4> case4, Action<T5> case5,
            Action<T6> case6)
        {
            switch (Index)
            {
                case 6:
                    case6(_t6);
                    break;
                default:
                    base.Match(case1, case2, case3, case4, case5);
                    break;
            }
        }
    }

    /// <summary>
    /// Represents a discriminated union with seven possible cases.
    /// </summary>
    public class Union<T1, T2, T3, T4, T5, T6, T7> : Union<T1, T2, T3, T4, T5, T6>
    {
        private readonly T7 _t7;

        protected Union()
        {
        }

        public Union([DisallowNull] T7 t7)
        {
            Index = 7;
            _t7 = t7;
        }

        /// <summary>
        /// Matches the active case and invokes the appropriate delegate.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <returns>The result of the invoked delegate.</returns>
        public TResult Match<TResult>(Func<T1, TResult> case1, Func<T2, TResult> case2,
            Func<T3, TResult> case3, Func<T4, TResult> case4, Func<T5, TResult> case5, Func<T6, TResult> case6,
            Func<T7, TResult> case7) =>
            Index switch
            {
                7 => case7(_t7),
                _ => base.Match(case1, case2, case3, case4, case5, case6)
            };

        /// <summary>
        /// Matches the active case and invokes the appropriate action.
        /// </summary>
        public void Match(Action<T1> case1, Action<T2> case2, Action<T3> case3, Action<T4> case4, Action<T5> case5,
            Action<T6> case6, Action<T7> case7)
        {
            switch (Index)
            {
                case 7:
                    case7(_t7);
                    break;
                default:
                    base.Match(case1, case2, case3, case4, case5, case6);
                    break;
            }
        }
    }

    /// <summary>
    /// Represents a discriminated union with eight possible cases.
    /// </summary>
    public class Union<T1, T2, T3, T4, T5, T6, T7, T8> : Union<T1, T2, T3, T4, T5, T6, T7>
    {
        private readonly T8 _t8;

        protected Union()
        {
        }

        public Union([DisallowNull] T8 t8)
        {
            Index = 8;
            _t8 = t8;
        }

        /// <summary>
        /// Matches the active case and invokes the appropriate delegate.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <returns>The result of the invoked delegate.</returns>
        public TResult Match<TResult>(Func<T1, TResult> case1, Func<T2, TResult> case2,
            Func<T3, TResult> case3, Func<T4, TResult> case4, Func<T5, TResult> case5, Func<T6, TResult> case6,
            Func<T7, TResult> case7, Func<T8, TResult> case8) =>
            Index switch
            {
                8 => case8(_t8),
                _ => base.Match(case1, case2, case3, case4, case5, case6, case7)
            };

        /// <summary>
        /// Matches the active case and invokes the appropriate action.
        /// </summary>
        public void Match(Action<T1> case1, Action<T2> case2, Action<T3> case3, Action<T4> case4, Action<T5> case5,
            Action<T6> case6, Action<T7> case7, Action<T8> case8)
        {
            switch (Index)
            {
                case 8:
                    case8(_t8);
                    break;
                default:
                    base.Match(case1, case2, case3, case4, case5, case6, case7);
                    break;
            }
        }
    }
}