/// Uncomment or Comment the next line to enable or disable the IConvertible interface implementation.
/// This is useful if you want to use an InfVal with the Convert class.

//#define DoConvertible

#if DoConvertible
using System;
using System.Numerics;

namespace InfiniteValue
{
    public partial struct InfVal : IConvertible
    {
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public TypeCode GetTypeCode() => TypeCode.Object;

#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public byte ToByte(IFormatProvider provider) => (byte)this;
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public sbyte ToSByte(IFormatProvider provider) => (sbyte)this;
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public short ToInt16(IFormatProvider provider) => (short)this;
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public ushort ToUInt16(IFormatProvider provider) => (ushort)this;
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public int ToInt32(IFormatProvider provider) => (int)this;
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public uint ToUInt32(IFormatProvider provider) => (uint)this;
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public long ToInt64(IFormatProvider provider) => (long)this;
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public ulong ToUInt64(IFormatProvider provider) => (ulong)this;

#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public float ToSingle(IFormatProvider provider) => (float)this;
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public double ToDouble(IFormatProvider provider) => (double)this;
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public decimal ToDecimal(IFormatProvider provider) => (decimal)this;

#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public bool ToBoolean(IFormatProvider provider) => !isZero;
#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public char ToChar(IFormatProvider provider) => (char)this;

#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public DateTime ToDateTime(IFormatProvider provider) => throw new InvalidCastException();

#if UNITY_2020_2_OR_NEWER
        readonly
#endif
        public object ToType(Type conversionType, IFormatProvider provider)
        {
            if (conversionType == typeof(InfVal)) return this;
            if (conversionType == typeof(BigInteger)) return (BigInteger)this;
            if (conversionType == typeof(string)) return ToString(provider);

            if (conversionType == typeof(byte)) return ToByte(provider);
            if (conversionType == typeof(sbyte)) return ToSByte(provider);
            if (conversionType == typeof(short)) return ToInt16(provider);
            if (conversionType == typeof(ushort)) return ToUInt16(provider);
            if (conversionType == typeof(int)) return ToInt32(provider);
            if (conversionType == typeof(uint)) return ToUInt32(provider);
            if (conversionType == typeof(long)) return ToInt64(provider);
            if (conversionType == typeof(ulong)) return ToUInt64(provider);

            if (conversionType == typeof(float)) return ToSingle(provider);
            if (conversionType == typeof(double)) return ToDouble(provider);
            if (conversionType == typeof(decimal)) return ToDecimal(provider);

            if (conversionType == typeof(bool)) return ToBoolean(provider);
            if (conversionType == typeof(char)) return ToChar(provider);

            throw new InvalidCastException();
        }
    }
}
#endif