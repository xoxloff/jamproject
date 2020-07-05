using System.Numerics;
using System.Linq;
using System;
using UnityEngine;
public struct ShortBigInteger
{
    public static readonly char[] Alphabet = "ABCDEFGHJK".ToCharArray();
    public static readonly int DefaultMaxValue = 1000;

    private BigInteger _value;
    private int _maxValue;
    private string _suffix;
    private float _shortValue;

    public string FullValue => ToString();
    public BigInteger Value
    {
        get => _value;
        set
        {
            this._value = value;
            GetNumber(this._value);
        }
    }
    public int MaxValue
    {
        get => _maxValue;
        set
        {

            _maxValue = value == 0 ? DefaultMaxValue : value;
            GetNumber(this._value);
        }
    }

    public ShortBigInteger(int maxValue, BigInteger value) : this()
    {
        MaxValue = maxValue;
        Value = value;
        GetNumber(value);
    }
    public ShortBigInteger(BigInteger value) : this(DefaultMaxValue, value)
    {
    }

    private float GetNumber(BigInteger num)
    {
        if (BigInteger.Abs(num) < MaxValue)
        {
            _suffix = "";
            _shortValue = (int)num;
            return _shortValue;
        }

        var suffixesCount = 1;
        BigInteger suffixesValue = MaxValue;
        while (BigInteger.Abs(num) / suffixesValue >= MaxValue)
        {
            suffixesValue *= MaxValue;
            suffixesCount++;
        }

        _suffix = GetSuffixLetter(suffixesCount - 1);
        _shortValue = (float)Math.Exp(BigInteger.Log(BigInteger.Abs(num)) - BigInteger.Log(suffixesValue)) * num.Sign;
        return _shortValue;
    }
    public static string GetSuffixLetter(int suffixNumber)
    {
        var length = Alphabet.Length;
        var d = suffixNumber / length;
        var p = suffixNumber % length;
        if (d < 1)
        {
            return Alphabet[p].ToString();
        }
        if (d > length)
        {
            return GetSuffixLetter(d - 1) + Alphabet[p];
        }
        return $"{Alphabet[d - 1]}{Alphabet[p]}";
    }
    public static int GetSuffixNumber(string suffix)
    {
        var letters = suffix.Trim().Reverse().ToArray();
        var number = 0;
        for (var i = 0; i < Alphabet.Length; i++)
        {
            for (var j = 0; j < letters.Length; j++)
            {
                if (Alphabet[i] == letters[j])
                {
                    number += j == 0 ? i : (int)Math.Pow(Alphabet.Length, j) * (i + 1);
                }
            }
        }
        return number;
    }
    public bool Equals(ShortBigInteger other)
    {
        if (MaxValue != other.MaxValue)
            throw new ArgumentException("ShortBigInteger has different max value");
        return Value.Equals(other.Value);
    }
    public static ShortBigInteger operator ++(ShortBigInteger a) => new ShortBigInteger(a.MaxValue, a.Value + 1);
    public static ShortBigInteger operator --(ShortBigInteger a) => new ShortBigInteger(a.MaxValue, a.Value - 1);
    public static ShortBigInteger operator +(ShortBigInteger a) => a;
    public static ShortBigInteger operator -(ShortBigInteger a) => new ShortBigInteger(a.MaxValue, -a.Value);
    public static ShortBigInteger operator +(ShortBigInteger a, ShortBigInteger b)
    {
        if (a.MaxValue != b.MaxValue)
            throw new ArgumentException("ShortBigInteger has different max value");
        return new ShortBigInteger(a.MaxValue, a.Value + b.Value);
    }
    public static ShortBigInteger operator -(ShortBigInteger a, ShortBigInteger b)
    {
        if (a.MaxValue != b.MaxValue)
            throw new ArgumentException("ShortBigInteger has different max value");
        return new ShortBigInteger(a.MaxValue, a + (-b)); ;
    }
    public static ShortBigInteger operator *(ShortBigInteger a, ShortBigInteger b)
    {
        if (a.MaxValue != b.MaxValue)
            throw new ArgumentException("ShortBigInteger has different max value");
        return new ShortBigInteger(a.MaxValue, a.Value * b.Value);
    }
    public static ShortBigInteger operator /(ShortBigInteger a, ShortBigInteger b)
    {
        if (a.MaxValue != b.MaxValue)
            throw new ArgumentException("ShortBigInteger has different max value");
        if (b.Value == 0)
        {
            throw new DivideByZeroException();
        }
        return new ShortBigInteger(a.MaxValue, a.Value / b.Value);
    }
    public static bool operator ==(ShortBigInteger a, ShortBigInteger b)
    {
        if (a.MaxValue != b.MaxValue)
            throw new ArgumentException("ShortBigInteger has different max value");
        return a.Value == b.Value;
    }
    public static bool operator !=(ShortBigInteger a, ShortBigInteger b)
    {
        if (a.MaxValue != b.MaxValue)
            throw new ArgumentException("ShortBigInteger has different max value");
        return a.Value != b.Value;
    }
    public static bool operator <(ShortBigInteger a, ShortBigInteger b)
    {
        if (a.MaxValue != b.MaxValue)
            throw new ArgumentException("ShortBigInteger has different max value");
        return a.Value < b.Value;
    }
    public static bool operator >(ShortBigInteger a, ShortBigInteger b)
    {
        if (a.MaxValue != b.MaxValue)
            throw new ArgumentException("ShortBigInteger has different max value");
        return a.Value > b.Value;
    }
    public static bool operator <=(ShortBigInteger a, ShortBigInteger b)
    {
        if (a.MaxValue != b.MaxValue)
            throw new ArgumentException("ShortBigInteger has different max value");
        return a.Value <= b.Value;
    }
    public static bool operator >=(ShortBigInteger a, ShortBigInteger b)
    {
        if (a.MaxValue != b.MaxValue)
            throw new ArgumentException("ShortBigInteger has different max value");
        return a.Value >= b.Value;
    }
    public static implicit operator BigInteger(ShortBigInteger v) => v.Value;
    public static explicit operator string(ShortBigInteger v) => v.ToString();
    public static implicit operator ShortBigInteger(BigInteger v) => new ShortBigInteger(v);
    public static implicit operator ShortBigInteger(short v) => new ShortBigInteger(v);
    public static implicit operator ShortBigInteger(int v) => new ShortBigInteger(v);
    public static implicit operator ShortBigInteger(long v) => new ShortBigInteger(v);
    public static implicit operator ShortBigInteger(string v)
    {
        var line = v.Trim().Split(' ');
        if (line.Length == 1)
        {
            BigInteger.TryParse(line[0], out var result);
            return result;
        }
        float.TryParse(line[0], out var valueResult);
        return (BigInteger)(valueResult * DefaultMaxValue) * (BigInteger.Pow(DefaultMaxValue, GetSuffixNumber(line[1]) + 1) / DefaultMaxValue);
    }
    public override int GetHashCode() => Value.GetHashCode();
    public override bool Equals(object obj) => obj is ShortBigInteger other && Equals(other);
    public override string ToString()
    {
        if (BigInteger.Abs(Value) < MaxValue)
        {
            return $"{_shortValue}{(string.IsNullOrWhiteSpace(_suffix) ? "" : " ")}{_suffix}";
        }
        return $"{_shortValue:F}{(string.IsNullOrWhiteSpace(_suffix) ? "" : " ")}{_suffix}";
    }
}
