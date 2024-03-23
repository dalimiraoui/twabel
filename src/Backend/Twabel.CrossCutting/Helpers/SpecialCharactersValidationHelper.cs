namespace Twabel.CrossCutting.Helpers
{
    public static class SpecialCharactersValidationHelper
    {
        private static readonly char[] StartingChars = { '<', '&', '=', '+', '-', '@' };

        public static bool IsDangerousString(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;

            for (var i = 0; ;)
            {

                // Look for the start of one of our patterns 
                var n = s.IndexOfAny(StartingChars, i);

                // If not found, the string is safe
                if (n < 0) return false;

                // If it's the last char, it's safe 
                if (n == s.Length - 1) return false;

                switch (s[n])
                {
                    case '<':
                        // If the < is followed by a letter or '!', it's unsafe (looks like a tag or HTML comment)
                        if (IsAtoZ(s[n + 1]) || s[n + 1] == '!' || s[n + 1] == '/' || s[n + 1] == '?') return true;
                        break;
                    case '&':
                        // If the & is followed by a #, it's unsafe (e.g. S) 
                        if (s[n + 1] == '#') return true;
                        break;
                    case '=':
                    case '+':
                    case '-':
                    case '@':
                        if (n == 0) return true;
                        break;
                }

                // Continue searching
                i = n + 1;
            }
        }

        private static bool IsAtoZ(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }
    }
}
/*

This code defines a static helper class called "SpecialCharactersValidationHelper" that provides a method "IsDangerousString" to check if a given string contains any potentially dangerous characters. The method returns a boolean value indicating whether the input string is considered dangerous or not.

The potentially dangerous characters that this code checks for include "<", "&", "=", "+", "-", and "@". The code checks for these characters in the input string and then performs additional checks depending on which character is found.

For example, if the code finds a "<" character, it checks the following character to see if it is a letter, "!", "/", or "?". If it is, then the string is considered dangerous and the method returns true.

Similarly, if the code finds an "&" character, it checks the following character to see if it is a "#". If it is, then the string is considered dangerous and the method returns true.

The code also checks for situations where one of the potentially dangerous characters appears at the beginning of the string, in which case the string is considered dangerous and the method returns true.

Overall, this code provides a basic check to identify potentially dangerous strings that could be used for security exploits, but it may not catch all possible cases and should not be relied upon as the sole means of preventing security vulnerabilities.

*/