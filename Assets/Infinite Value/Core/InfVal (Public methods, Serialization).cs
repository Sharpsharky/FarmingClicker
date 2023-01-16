using System.Numerics;
using UnityEngine;
using System;

namespace InfiniteValue
{
    [Serializable] public partial struct InfVal : ISerializationCallbackReceiver
    {
        // public methods

        /// <summary>
        /// Return this InfVal with a modified exponent. 
        /// The default behaviour is to add zeros at the end of the digits if you decrease the exponent or trim ending digits if you increase the exponent.
        /// </summary>
        /// <param name="raw">Optionally set the exponent directly without changing the digits.</param>
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public InfVal ToExponent(int exponent, bool raw = false)
        {
            if (exponent == this.exponent)
                return this;

            InfVal ret = ManualFactory(this.digits, this.exponent);
            if (!raw)
                ret.exponent = exponent;
            else
                ret.s_exponent = exponent;
            return ret;
        }

        /// <summary>
        /// Return this InfVal with modified digits.
        /// </summary>
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public InfVal ToDigits(in BigInteger digits)
        {
            if (digits == this.digits)
                return this;

            InfVal ret = ManualFactory(this.digits, this.exponent);
            ret.digits = digits;
            return ret;
        }

        /// <summary>
        /// Return this InfVal with a modified precision by changing the exponent.
        /// This will throw an exception if precision is negative or zero.
        /// </summary>
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public InfVal ToPrecision(int precision)
        {
            if (precision <= 0)
                throw new ArgumentException(cannotBeNegOrZeroStr, nameof(precision));

            if (precision == this.precision)
                return this;

            return ToExponent(this.exponent + this.precision - precision);
        }

        /// <summary>
        /// Return this InfVal with all zeros at the end of the digits removed and the exponent modified accordingly.
        /// This wont change the InfVal value.
        /// </summary>
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public InfVal RemoveTrailingZeros()
        {
            InfVal ret = ManualFactory(digits, exponent);

            int toRemove = 0;
            while (toRemove < cacheDigitsToString.Length - 1 && cacheDigitsToString[cacheDigitsToString.Length - (1 + toRemove)] == '0')
                ++toRemove;

            ret.exponent += toRemove;
            return ret;
        }

        /// <summary>
        /// Return this InfVal with the decimal point moved left by move amount.
        /// This will change the exponent without changing the digits and therefore will change the InfVal value.
        /// </summary>
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public InfVal MovePointLeft(int move)
        {
            InfVal ret = ManualFactory(digits, exponent);
            ret.s_exponent -= move;
            return ret;
        }

        /// <summary>
        /// Return this InfVal with the decimal point moved right by move amount.
        /// This will change the exponent without changing the digits and therefore will change the InfVal value.
        /// </summary>
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public InfVal MovePointRight(int move)
        {
            InfVal ret = ManualFactory(digits, exponent);
            ret.s_exponent += move;
            return ret;
        }

        /// <summary>
        /// Extract the digits and exponent of this InfVal.
        /// Like every Deconstruct method with a similar signature, it can be used by doing:
        ///     (BigInteger d, int e) = infValVariable;
        /// </summary>
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public void Deconstruct(out BigInteger digits, out int exponent)
        {
            digits = this.digits;
            exponent = this.exponent;
        }

        // private methods
        static BigInteger MultiplyBigIntByPowOf10(in BigInteger value, int pow)
        {
            if (pow == 0)
                return value;
            if (pow > 0)
                return value * BigInteger.Pow(10, pow);
            // (exponent < 0)
            return value / BigInteger.Pow(10, -pow);
        }

        // serialization methods

        /// <summary>
        /// This is automatically called by Unity before serialization and shouldn't be used.
        /// </summary>
        public void OnBeforeSerialize()
        {
            s_digits = cacheDigitsToString;
        }

        /// <summary>
        /// This is automatically called by Unity after serialization and shouldn't be used.
        /// </summary>
        public void OnAfterDeserialize()
        {
            if (BigInteger.TryParse(cacheDigitsToString, out BigInteger res))
                digits = res;
            else
                digits = 0;
        }
    }
}
