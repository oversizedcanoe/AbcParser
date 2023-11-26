namespace AbcParser
{
    public static class Constants
    {
        #region Identifiers for headers
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
        #endregion

        /// <summary>
        /// This list is used as a way to translate notes into numbers.
        /// The numeric representation of the notes can be found by taking their index in this list.
        /// For example, Constants.Notes.IndexOf("c") returns the numeric representation of middle C.
        /// </summary>
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

        /// <summary>
        /// This list is used as a way to translate numeric values of notes into frequencies.
        /// Using the NOTES list above, tones for specific notes can be found by taking the index of 
        /// the note in the NOTES list and taking the frequency found at the index in this list.
        /// This can be used for simple console testing, using an override of <see cref="Console.Beep()"/>
        /// </summary>
        public static readonly List<decimal> TONES = new List<decimal>()
        {
            32.70M,
            34.65M,
            36.71M,
            38.89M,
            41.20M,
            43.65M,
            46.25M,
            49.00M,
            51.91M,
            55.00M,
            58.27M,
            61.74M,
            65.41M,
            69.30M,
            73.42M,
            77.78M,
            82.41M,
            87.31M,
            92.50M,
            98.00M,
            103.83M,
            110.00M,
            116.54M,
            123.47M,
            130.81M,
            138.59M,
            146.83M,
            155.56M,
            164.81M,
            174.61M,
            185.00M,
            196.00M,
            207.65M,
            220.00M,
            233.08M,
            246.94M,
            261.63M, // Middle C
            277.18M,
            293.66M,
            311.13M,
            329.63M,
            349.23M,
            369.99M,
            392.00M,
            415.30M,
            440.00M,
            466.16M,
            493.88M,
            523.25M,
            554.37M,
            587.33M,
            622.25M,
            659.25M,
            698.46M,
            739.99M,
            783.99M,
            830.61M,
            880.00M,
            932.33M,
            987.77M,
            1046.50M,
            1108.73M,
            1174.66M,
            1244.51M,
            1318.51M,
            1396.91M,
            1479.98M,
            1567.98M,
            1661.22M,
            1760.00M,
            1864.66M,
            1975.53M
        };

        /// <summary>
        /// Enum which contains all possible Keys (excluding modes).
        /// 's' indicates sharp, 'b' indicates flat, and 'm' indicates minor.
        /// </summary>
        public enum KeyEnum
        {
            // Major
            C,
            Cs,
            Db,
            D,
            Ds,
            Eb,
            E,
            F,
            Fs,
            Gb,
            G,
            Gs,
            Ab,
            A,
            As,
            Bb,
            B,
            // Minor
            Cm,
            Csm,
            Dbm,
            Dm,
            Dsm,
            Ebm,
            Em,
            Fm,
            Fsm,
            Gbm,
            Gm,
            Gsm,
            Abm,
            Am,
            Asm,
            Bbm,
            Bm
        }
    }
}
