using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Twabel.CrossCutting.Extensions
{

    public enum MaskStyle
    {
        All,
        AlphaNumericOnly,
    }

    public static class StringExtensions
    {
        public static readonly char DefaultMaskCharacter = '*';

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string TruncateLog(this string value)
        {
            return Truncate(value, 3000);
        }

        public static bool IsLengthAtLeast(this string value, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", length,
                                                        "The length must be a non-negative number.");
            }

            return value != null
                        ? value.Length >= length
                        : false;
        }

        public static string Mask(this string sourceValue, char maskChar, int numExposed, MaskStyle style)
        {
            var maskedString = sourceValue;

            if (sourceValue.IsLengthAtLeast(numExposed))
            {
                var builder = new StringBuilder(sourceValue.Length);
                int index = maskedString.Length - numExposed;

                if (style == MaskStyle.AlphaNumericOnly)
                {
                    CreateAlphaNumMask(builder, sourceValue, maskChar, index);
                }
                else
                {
                    builder.Append(maskChar, index);
                }

                builder.Append(sourceValue.Substring(index));
                maskedString = builder.ToString();
            }

            return maskedString;
        }

        public static string Mask(this string sourceValue, char maskChar, int numExposed)
        {
            return Mask(sourceValue, maskChar, numExposed, MaskStyle.All);
        }

        public static string Mask(this string sourceValue, char maskChar)
        {
            return Mask(sourceValue, maskChar, 0, MaskStyle.All);
        }

        public static string Mask(this string sourceValue, int numExposed)
        {
            return Mask(sourceValue, DefaultMaskCharacter, numExposed, MaskStyle.All);
        }

        public static string Mask(this string sourceValue)
        {
            return Mask(sourceValue, DefaultMaskCharacter, 0, MaskStyle.All);
        }

        public static string Mask(this string sourceValue, char maskChar, MaskStyle style)
        {
            return Mask(sourceValue, maskChar, 0, style);
        }

        public static string Mask(this string sourceValue, int numExposed, MaskStyle style)
        {
            return Mask(sourceValue, DefaultMaskCharacter, numExposed, style);
        }

        public static string Mask(this string sourceValue, MaskStyle style)
        {
            return Mask(sourceValue, DefaultMaskCharacter, 0, style);
        }

        public static string GetOnlyLetters(this string sourceValue)
        {
            var rgxOnlyCharacters = new Regex("[^a-zA-Z ]");
            return rgxOnlyCharacters.Replace(sourceValue, " ").Trim();
        }

        private static void CreateAlphaNumMask(StringBuilder buffer, string source, char mask, int length)
        {
            for (int i = 0; i < length; i++)
            {
                buffer.Append(char.IsLetterOrDigit(source[i])
                                ? mask
                                : source[i]);
            }
        }
    }
}
/*
This code defines a number of extension methods for strings, including methods for truncating strings, checking if strings are at least a certain length, and masking parts of strings with a specified character. It also includes a method for getting only the letters in a string.

The Truncate method takes a string and an integer maxLength and returns a truncated version of the string that is no longer than maxLength characters. If the original string is shorter than maxLength, the original string is returned.

The TruncateLog method is a convenience method that calls Truncate with a maximum length of 3000 characters, which is often used as a maximum length for log messages.

The IsLengthAtLeast method takes a string and an integer length and returns true if the length of the string is at least length. If length is negative, an ArgumentOutOfRangeException is thrown.

The Mask method has several overloads that take a string, a character to use for masking, and an integer numExposed that specifies the number of characters in the original string to leave unmasked. The MaskStyle parameter specifies whether to mask all characters or only non-alphanumeric characters.

The GetOnlyLetters method returns a new string that contains only the letters from the original string. It achieves this by creating a regular expression that matches all non-letter characters and replaces them with a space, and then trimming the result.

Overall, these extension methods are useful for manipulating strings in a variety of ways and can be used to simplify and clarify code that works with strings.

*/