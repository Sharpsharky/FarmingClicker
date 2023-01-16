using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System;

namespace InfiniteValue
{
    public static class MathInfVal
    {
        // consts
        const string noArgumentsExceptionFormat_Method = "Cannot use {0} with no parameters";

        // public methods

        /// <summary>Returns the absolute value of an InfVal.</summary>
        public static InfVal Abs(in InfVal value) => InfVal.ManualFactory(BigInteger.Abs(value.digits), value.exponent);

        /// <summary>Compares two InfVal and returns true if they are similar.</summary>
        public static bool Approximately(in InfVal a, in InfVal b, int precision) =>
            (MultiplyBigIntByPowOf10(a.digits, precision - a.precision) == MultiplyBigIntByPowOf10(b.digits, precision - b.precision));

        /// <summary>Clamps an InfVal between the given minimum and maximum values.</summary>
        public static InfVal Clamp(in InfVal value, in InfVal min, in InfVal max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        /// <summary>Clamps an InfVal between 0 and 1.</summary>
        public static InfVal Clamp01(in InfVal value) => Clamp(value, 0, 1);

        /// <summary>Returns the closest power of two of an InfVal.</summary>
        public static InfVal ClosestPowerOfTwo(in InfVal value)
        {
            if (value.exponent == 0 && value.isPowerOfTwo)
                return value.ToExponent(0);
            if (value.sign < 0)
                return 0;
            if (value <= 1)
                return 1;

            InfVal previous = PreviousPowerOfTwo(value);
            InfVal next = NextPowerOfTwo(value);

            return (value - previous < next - value ? previous : next);
        }

        /// <summary>Returns the next power of two that is equal to, or greater than, the argument.</summary>
        public static InfVal NextPowerOfTwo(in InfVal value)
        {
            if (value.isPowerOfTwo)
                return value.ToExponent(0);
            if (value.sign < 0)
                return 0;

            InfVal compareVal = value.ToExponent(0);
            byte[] bytes = compareVal.digits.ToByteArray();

            for (int i = 0; i < bytes.Length - 1; i++)
                bytes[i] = 0;
            bytes[bytes.Length - 1] = 1;

            BigInteger bi = new BigInteger(bytes);
            while (bi < compareVal.digits)
                bi <<= 1;

            return InfVal.ManualFactory(bi, 0);
        }

        /// <summary>Returns the previous power of two that is equal to, or lower than, the argument.</summary>
        public static InfVal PreviousPowerOfTwo(in InfVal value)
        {
            if (value.isPowerOfTwo)
                return value.ToExponent(0);
            if (value.sign < 0)
                return 0;

            return NextPowerOfTwo(value) >> 1;
        }

        /// <summary>Loops the value t, so that it is never larger than length and never smaller than 0.</summary>
        public static InfVal Repeat(in InfVal t, in InfVal length) => ((t % length) + length) % length;

        /// <summary>PingPong returns a value that will increment and decrement between the value 0 and length.</summary>
        public static InfVal PingPong(in InfVal t, in InfVal length)
        {
            InfVal mod = Abs(t) % (length * 2);
            if (mod >= length)
                return (length - (mod % length));

            return mod % length;
        }

        /// <summary>Calculates the shortest difference between two given angles given in degrees.</summary>
        public static InfVal DeltaAngle(in InfVal current, in InfVal target) => (((((target - current) + 180) % 360) + 360) % 360) - 180;

        /// <summary>Returns the natural logarithm of a specified InfVal.</summary>
        public static double Log(in InfVal value) => BigInteger.Log(value.digits) + Math.Log(Math.Pow(10, value.exponent));

        /// <summary>Returns the logarithm of a specified InfVal in a specified base.</summary>
        public static double Log(in InfVal value, double b) => BigInteger.Log(value.digits, b) + Math.Log(Math.Pow(10, value.exponent), b);

        /// <summary>Returns the base 10 logarithm of a specified InfVal.</summary>
        public static double Log10(in InfVal value) => BigInteger.Log10(value.digits) + value.exponent;

        /// <summary>Returns the largest integer smaller than or equal to an InfVal.</summary>
        public static InfVal Floor(in InfVal value)
        {
            if (value.isInteger)
                return value;
            if (value.sign >= 0)
                return value.ToExponent(0).ToExponent(value.exponent);

            return (value.ToExponent(0) - 1).ToExponent(value.exponent);
        }

        /// <summary>Returns the smallest integer greater to or equal to an InfVal.</summary>
        public static InfVal Ceil(in InfVal value)
        {
            if (value.isInteger)
                return value;
            if (value.sign <= 0)
                return value.ToExponent(0).ToExponent(value.exponent);

            return (value.ToExponent(0) + 1).ToExponent(value.exponent);
        }

        /// <summary>Returns an InfVal rounded to the nearest integer.</summary>
        public static InfVal Round(in InfVal value)
        {
            if (value.isInteger)
                return value;

            int digitAfterDecimalPoint = (int)(BigInteger.Abs(value.ToExponent(-1).digits) % 10);
            if (digitAfterDecimalPoint >= 5)
                return (value.ToExponent(0) + value.sign).ToExponent(value.exponent);
            return value.ToExponent(0).ToExponent(value.exponent);
        }

        /// <summary>Returns the largest of two or more values.</summary>
        public static InfVal Max(in InfVal a, in InfVal b) => (a >= b ? a : b);

        /// <summary>Returns the largest of two or more values.</summary>
        public static InfVal Max(params InfVal[] values)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentException(string.Format(noArgumentsExceptionFormat_Method, "MathInfVal.Max"));

            InfVal ret = values[0];
            for (int i = 1; i < values.Length; i++)
                ret = Max(ret, values[i]);
            return ret;
        }

        /// <summary>Returns the smallest of two or more values.</summary>
        public static InfVal Min(in InfVal a, in InfVal b) => (a <= b ? a : b);

        /// <summary>Returns the smallest of two or more values.</summary>
        public static InfVal Min(params InfVal[] values)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentException(string.Format(noArgumentsExceptionFormat_Method, "MathInfVal.Min"));

            InfVal ret = values[0];
            for (int i = 1; i < values.Length; i++)
                ret = Min(ret, values[i]);
            return ret;
        }

        /// <summary>Returns the value with the highest exponent.</summary>
        public static int MaxExponent(in InfVal a, in InfVal b) => (a.exponent >= b.exponent ? a.exponent : b.exponent);

        /// <summary>Returns the value with the highest exponent.</summary>
        public static int MaxExponent(params InfVal[] values)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentException(string.Format(noArgumentsExceptionFormat_Method, "MathInfVal.MaxExponent"));

            int ret = values[0].exponent;
            for (int i = 1; i < values.Length; i++)
                ret = MinExponent(ret, values[i].exponent);
            return ret;
        }

        /// <summary>Returns the value with the lowest exponent.</summary
        public static int MinExponent(in InfVal a, in InfVal b) => (a.exponent <= b.exponent ? a.exponent : b.exponent);

        /// <summary>Returns the value with the lowest exponent.</summary
        public static int MinExponent(params InfVal[] values)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentException(string.Format(noArgumentsExceptionFormat_Method, "MathInfVal.MinExponent"));

            int ret = values[0].exponent;
            for (int i = 1; i < values.Length; i++)
                ret = MinExponent(ret, values[i].exponent);
            return ret;
        }

        /// <summary>Returns the value with the highest precision.</summary
        public static int MaxPrecision(in InfVal a, in InfVal b) => (a.precision >= b.precision ? a.precision : b.precision);

        /// <summary>Returns the value with the highest precision.</summary>
        public static int MaxPrecision(params InfVal[] values)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentException(string.Format(noArgumentsExceptionFormat_Method, "MathInfVal.MaxPrecision"));

            int ret = values[0].precision;
            for (int i = 1; i < values.Length; i++)
                ret = MinPrecision(ret, values[i].precision);
            return ret;
        }

        /// <summary>Returns the value with the lowest precision.</summary>
        public static int MinPrecision(in InfVal a, in InfVal b) => (a.precision <= b.precision ? a.precision : b.precision);

        /// <summary>Returns the value with the lowest precision.</summary>
        public static int MinPrecision(params InfVal[] values)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentException(string.Format(noArgumentsExceptionFormat_Method, "MathInfVal.MinPrecision"));

            int ret = values[0].precision;
            for (int i = 1; i < values.Length; i++)
                ret = MinPrecision(ret, values[i].precision);
            return ret;
        }

        /// <summary>Returns value raised to power.</summary>
        /// <param name="conservePrecision">Optionally define wether the result should have the same precision as the given value.</param>
        public static InfVal Pow(in InfVal value, int power, bool conservePrecision = true)
        {
            if (power == 0)
                return new InfVal(1, value.precision);
            if (value.isZero || value.isOne)
                return value;
            
            int calcPow = Math.Abs(power);
            InfVal calcVal = value.RemoveTrailingZeros();

            InfVal res = InfVal.ManualFactory(BigInteger.Pow(calcVal.digits, calcPow), calcVal.exponent * calcPow);
            if (power < 0)
                res = 1 / res;

            return (conservePrecision ? res.ToPrecision(value.precision) : res);
        }

        /// <summary>Returns the square root of value.</summary>
        /// <param name="conservePrecision">Optionally define wether the result should have the same precision as the given value.</param>
        public static InfVal Sqrt(in InfVal value, bool conservePrecision = true)
        {
            if (value.isZero || value.isOne)
                return value;
            if (value.sign < 0)
                throw new ArgumentException("Cannot use Sqrt with a negative value (The result is a complex number).", nameof(value));

            InfVal calcVal = value.RemoveTrailingZeros().ToExponent(value.exponent * 2);

            InfVal res = InfVal.ManualFactory(SqrtBigInteger(calcVal.digits), calcVal.exponent / 2);

            return (conservePrecision ? res.ToPrecision(value.precision) : res);
        }

        /// <summary>Returns the Nth root of value. (The number that raised to n will equal value).</summary>
        /// <param name="conservePrecision">Optionally define wether the result should have the same precision as the given value.</param>
        public static InfVal NthRoot(in InfVal value, int n, bool conservePrecision = true)
        {
            if (n == 0)
                throw new ArgumentException("0th root of a value is undefined.", nameof(n));
            if (value.isZero || value.isOne)
                return value;

            if (value.sign < 0 && n % 2 == 0)
                throw new ArgumentException("Result of NthRoot with a negative value and an even n is a complex number.", $"{nameof(value)}, {nameof(n)}");

            int calcN = Math.Abs(n);
            int calcExp = value.exponent;
            if ((value.exponent / (float)calcN) % 1 != 0)
                calcExp = (value.exponent / calcN) * calcN;
            InfVal calcVal = value.RemoveTrailingZeros().ToExponent(calcExp * (int)-Mathf.Sign(calcExp) * calcN);

            InfVal res = InfVal.ManualFactory(NthRootBigInteger(calcVal.digits, calcN), calcVal.exponent / calcN);
            if (n < 0)
                res = 1 / res;

            return (conservePrecision ? res.ToPrecision(value.precision) : res);
        }

        /// <summary>Calculates the integral part of a value. The returned InfVal will have the same exponent as the value argument.</summary>
        public static InfVal Truncate(in InfVal value) => (!value.isInteger ? value.ToExponent(0).ToExponent(value.exponent) : value);

        /// <summary>Calculates the decimal part of a value.</summary>
        public static InfVal DecimalPart(in InfVal value) => (value.exponent < 0 ? (value - value.ToExponent(0)) : 0);

        /// <summary>Finds the greatest common divisor of two InfVal.</summary>
        public static InfVal GreatestCommonDivisor(in InfVal a, in InfVal b)
        {
            int lowestExponent = Mathf.Min(a.exponent, b.exponent);
            return InfVal.ManualFactory(BigInteger.GreatestCommonDivisor(a.ToExponent(lowestExponent).digits, b.ToExponent(lowestExponent).digits), lowestExponent);
        }

        /// <summary>Return a random value between min [inclusive] and max [inclusive].</summary>
        public static InfVal RandomRange(in InfVal min, in InfVal max)
        {
            int lowestExponent = Mathf.Min(min.exponent, max.exponent);
            return InfVal.ManualFactory(RandomBigIntegerInRange(min.ToExponent(lowestExponent).digits, max.ToExponent(lowestExponent).digits), lowestExponent);
        }

        // private methods

        // found on https://stackoverflow.com/questions/17357760/how-can-i-generate-a-random-biginteger-within-a-certain-range/48855115
        static BigInteger RandomBigIntegerInRange(BigInteger min, BigInteger max)
        {
            if (min > max)
            {
                var buff = min;
                min = max;
                max = buff;
            }

            // offset to set min = 0
            BigInteger offset = -min;
            min = 0;
            max += offset;

            BigInteger value;
            var bytes = max.ToByteArray();

            // count how many bits of the most significant byte are 0
            // NOTE: sign bit is always 0 because max must always be positive
            byte zeroBitsMask = 0b00000000;

            var mostSignificantByte = bytes[bytes.Length - 1];

            // we try to set to 0 as many bits as there are in the most significant byte, starting from the left (most significant bits first)
            // NOTE: i starts from 7 because the sign bit is always 0
            for (var i = 7; i >= 0; i--)
            {
                // we keep iterating until we find the most significant non-0 bit
                if ((mostSignificantByte & (0b1 << i)) != 0)
                {
                    var zeroBits = 7 - i;
                    zeroBitsMask = (byte)(0b11111111 >> zeroBits);
                    break;
                }
            }

            System.Random rng = new System.Random();
            do
            {
                rng.NextBytes(bytes);

                // set most significant bits to 0 (because value > max if any of these bits is 1)
                bytes[bytes.Length - 1] &= zeroBitsMask;

                value = new BigInteger(bytes);

                // value > max 50% of the times, in which case the fastest way to keep the distribution uniform is to try again
            } while (value > max);

            return value;
        }

        // found on https://stackoverflow.com/questions/3432412/calculate-square-root-of-a-biginteger-system-numerics-biginteger
        static BigInteger SqrtBigInteger(BigInteger value)
        {
            int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(value, 2)));
            BigInteger root = BigInteger.One << (bitLength >> 1);

            while (!isSqrt(value, root))
            {
                root += value / root;
                root >>= 1;
            }
            
            return root;

            bool isSqrt(BigInteger bi, BigInteger r)
            {
                BigInteger lowerBound = r * r;
                BigInteger upperBound = (r + 1) * (r + 1);

                return (bi >= lowerBound && bi < upperBound);
            }
        }

        static BigInteger NthRootBigInteger(BigInteger value, int n)
        {
            if (n == 1)
                return value;

            BigInteger high = 1;
            while (BigInteger.Pow(high, n) < value)
                high <<= 1;
            BigInteger low = high >> 1;

            while (high - low > 1)
            {
                BigInteger mid = (low + high) / 2;
                BigInteger midToValue = BigInteger.Pow(mid, n);

                if (midToValue < value)
                    low = mid;
                else if (midToValue > value)
                    high = mid;
                else
                    return mid;
            }

            if (BigInteger.Pow(high, n) == value)
                return high;
            return low;
        }

        static BigInteger MultiplyBigIntByPowOf10(in BigInteger value, int pow)
        {
            if (pow == 0)
                return value;
            if (pow > 0)
                return value * BigInteger.Pow(10, pow);
            // (exponent < 0)
            return value / BigInteger.Pow(10, -pow);
        }
    }
}