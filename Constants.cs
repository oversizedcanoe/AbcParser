namespace AbcParser
{
    public static class Constants
    {
        public const string IDENTIFIER_COMMENT = "%";
        public const string IDENTIFIER_LYRIC = "W:";
        public const string IDENTIFIER_SONG_NUMBER = "X:";
        public const string IDENTIFIER_SONG_TITLE = "T:";
        public const string IDENTIFIER_COMPOSER = "C:";
        public const string IDENTIFIER_METER = "M:";
        public const string IDENTIFIER_NOTE_LENGTH_PROPORTION = "L:";
        public const string IDENTIFIER_RHYTHM = "R:";
        public const string IDENTIFIER_KEY = "K:";
        public const string IDENTIFIER_ORIGIN = "O:";
        public const string IDENTIFIER_BPM = "Q:";

        public static readonly List<string> NOTES = new List<string>()
        {
            "C,,", // Three octaves below middle C
            "C#,,",
            "D,,",
            "D#,,",
            "E,,",
            "F,,",
            "F#,,",
            "G,,",
            "G#,,",
            "A,,",
            "A#,,",
            "B,,",
            "C,", // Two octaves below middle C
            "C#,",
            "D,",
            "D#,",
            "E,",
            "F,",
            "F#,",
            "G,",
            "G#,",
            "A,",
            "A#,",
            "B,",
            "C", // One octave below middle C
            "C#",
            "D",
            "D#",
            "E",
            "F",
            "F#",
            "G",
            "G#",
            "A",
            "A#",
            "B",
            "c", // Middle C
            "c#",
            "d",
            "d#",
            "e",
            "f",
            "f#",
            "g",
            "g#",
            "a",
            "a#",
            "b",
            "c'", // One octave above middle C
            "c#'",
            "d'",
            "d#'",
            "e'",
            "f'",
            "f#'",
            "g'",
            "g#'",
            "a'",
            "a#'",
            "b'",
            "c''", // Two octaves above middle C
            "c#''",
            "d''",
            "d#''",
            "e''",
            "f''",
            "f#''",
            "g''",
            "g#''",
            "a''",
            "a#''",
            "b''"
        };
    }
}
