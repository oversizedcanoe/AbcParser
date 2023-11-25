namespace AbcParser
{
    public static class ExtensionMethods
    {
        public static bool IsNote(this char c)
        {
            return "abcdefg".Contains(c.ToString().ToLower());
        }

        public static bool IsLengthModifier(this char c)
        {
            return c == '/' || int.TryParse(c.ToString(), out int _);
        }

        public static bool IsAccidental(this char c)
        {
            return c == '^' || c =='_' || c == '=';
        }

        public static bool IsPitchModifier(this char c)
        {
            return c == '\'' || c == ',';
        }

        public static bool IsRest(this char c)
        {
            return "z".Contains(c.ToString().ToLower());
        }
    }
}
