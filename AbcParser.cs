namespace AbcParser
{
    public class AbcParser
    {
        public static void ParseFromFile(string filePath)
        {
            if (File.Exists(filePath) == false)
            {
                throw new InvalidDataException("File path does not exist: " + filePath);
            }

            Song song = new Song();

            string[] lines = File.ReadAllLines(filePath);

            bool doneHeaders = false;
            int index = 0;

            foreach (var line in lines)
            {
                index++;

                if (doneHeaders)
                {
                    ProcessMusicNotes(song, line);

                    // No need to check headers again, just continue
                    continue;
                }

                if (line.StartsWith(Constants.IDENTIFIER_COMMENT) || line.StartsWith(Constants.IDENTIFIER_LYRIC))
                {
                    continue;
                }

                if (line.StartsWith(Constants.IDENTIFIER_SONG_NUMBER))
                {
                    int.TryParse(line.Replace(Constants.IDENTIFIER_SONG_NUMBER, string.Empty), out int songNumber);
                    song.SongNumber = songNumber;
                }
                else if (line.StartsWith(Constants.IDENTIFIER_SONG_TITLE))
                {
                    song.SongTitle = line.Replace(Constants.IDENTIFIER_SONG_TITLE, string.Empty).Trim();
                }
                else if (line.StartsWith(Constants.IDENTIFIER_COMPOSER))
                {
                    song.Composer = line.Replace(Constants.IDENTIFIER_COMPOSER, string.Empty).Trim();
                }
                else if (line.StartsWith(Constants.IDENTIFIER_METER))
                {
                    string meter = line.Replace(Constants.IDENTIFIER_METER, string.Empty).Trim();

                    if (meter == "C")
                    {
                        song.Meter = "4/4";
                    }
                    else if (meter == "C|")
                    {
                        song.Meter = "2/2";
                    }
                    else
                    {
                        song.Meter = meter;
                    }
                }
                else if (line.StartsWith(Constants.IDENTIFIER_NOTE_LENGTH_PROPORTION))
                {
                    string fraction = line.Replace(Constants.IDENTIFIER_NOTE_LENGTH_PROPORTION, string.Empty).Trim();
                    int numerator = int.Parse(fraction.Split('/')[0]);
                    int denominator = int.Parse(fraction.Split('/')[1]);
                    song.NoteLengthProportion = decimal.Divide(numerator, denominator);
                }
                else if (line.StartsWith(Constants.IDENTIFIER_RHYTHM))
                {
                    song.Rhythm = line.Replace(Constants.IDENTIFIER_RHYTHM, string.Empty).Trim();
                }
                else if (line.StartsWith(Constants.IDENTIFIER_KEY))
                {
                    song.Key = line.Replace(Constants.IDENTIFIER_KEY, string.Empty).Trim();

                    // Key is always the last header
                    doneHeaders = true;
                }
                else if (line.StartsWith(Constants.IDENTIFIER_ORIGIN))
                {
                    song.Origin = line.Replace(Constants.IDENTIFIER_ORIGIN, string.Empty).Trim();
                }
                else if (line.StartsWith(Constants.IDENTIFIER_BPM))
                {
                    int.TryParse(line.Replace(Constants.IDENTIFIER_BPM, string.Empty), out int bpm);
                    song.BPM = bpm;
                }
                else
                {
                    throw new Exception($"Unable to read line {index}: {line}");
                }
            }
        }

        public static void ProcessMusicNotes(Song song, string line)
        {
            int lineLength = line.Length;
            int lastProcessedIndex = 0;

            string currentNote = string.Empty;
            
            while (lastProcessedIndex< lineLength)
            {
                // All options for what each character can be:
                // ABCDEFG abcdefg , '  --> pitch identifier
                // ^ _ =  --> accidentals
                // A number or slash --> modifies the note length
                // % --> comment
                // " --> indicates there is a chord, end of chord is next "
                // | --> new bar
                // z Z  --> Rest (uses same length modifier as notes)
                // / --> means the line is done
            }

            // Use song.NoteLengthProportion to determine what a standard note is. i.e. if NoteLengthProportion is 1/8, then G just means it's 1/8.
            // G/2 means half, or a 1/16 note. 2G means double, or a 1/4 note.
            // G/ is short hand for G/2, G//2 is short hand for G/4 

            Console.WriteLine("Processing music line: ");
            Console.WriteLine(line);
        }

        public static int NoteToNumber(string note)
        {
            bool noteIsSharp = note.StartsWith('^');
            bool noteIsFlat = note.StartsWith('_');
            bool noteIsNatural = note.StartsWith('=');

            int index = Constants.NOTES.IndexOf(note);

            if (index == -1)
            {
                throw new Exception("Invalid note passed");
            }

            return index;
        }
    }
}