using System.Numerics;
using System.Linq;
using System;
using UnityEngine;
public struct ShortBigInteger
{
    public static readonly char[] Alphabet = "ABCDEFGHJK".ToCharArray();
    public static readonly int MaxValue = 1000;

    private BigInteger _value;
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

    public ShortBigInteger(BigInteger value) : this()
    {
        Value = value;
        GetNumber(value);
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
        _shortValue = Division(num, suffixesValue);
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
    public static float Division(ShortBigInteger a, ShortBigInteger b)
    {
        return (float)Math.Exp(BigInteger.Log(BigInteger.Abs(a.Value)) - BigInteger.Log(BigInteger.Abs(b.Value))) * a.Value.Sign * b.Value.Sign;
    }
    public bool Equals(ShortBigInteger other) => Value.Equals(other.Value);
    public static ShortBigInteger operator ++(ShortBigInteger a) => new ShortBigInteger(a.Value + 1);
    public static ShortBigInteger operator --(ShortBigInteger a) => new ShortBigInteger(a.Value - 1);
    public static ShortBigInteger operator +(ShortBigInteger a) => a;
    public static ShortBigInteger operator -(ShortBigInteger a) => new ShortBigInteger(-a.Value);
    public static ShortBigInteger operator +(ShortBigInteger a, ShortBigInteger b) => new ShortBigInteger(a.Value + b.Value);

    public static ShortBigInteger operator -(ShortBigInteger a, ShortBigInteger b) => new ShortBigInteger(a + (-b));

    public static ShortBigInteger operator *(ShortBigInteger a, ShortBigInteger b) => new ShortBigInteger(a.Value * b.Value);

    public static ShortBigInteger operator /(ShortBigInteger a, ShortBigInteger b)
    {
        if (b.Value == 0)
        {
            throw new DivideByZeroException();
        }
        return new ShortBigInteger(a.Value / b.Value);
    }
    public static bool operator ==(ShortBigInteger a, ShortBigInteger b) => a.Value == b.Value;

    public static bool operator !=(ShortBigInteger a, ShortBigInteger b) => a.Value != b.Value;

    public static bool operator <(ShortBigInteger a, ShortBigInteger b) => a.Value < b.Value;

    public static bool operator >(ShortBigInteger a, ShortBigInteger b) => a.Value > b.Value;

    public static bool operator <=(ShortBigInteger a, ShortBigInteger b) => a.Value <= b.Value;

    public static bool operator >=(ShortBigInteger a, ShortBigInteger b) => a.Value >= b.Value;
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
        return (BigInteger)(valueResult * MaxValue) * (BigInteger.Pow(MaxValue, GetSuffixNumber(line[1]) + 1) / MaxValue);
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
