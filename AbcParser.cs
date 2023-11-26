namespace AbcParser
{
    /// <summary>
    /// The purpose of this class is to receive an .abc file as input and return it in a easy to use format in the form of a C# class.
    /// </summary>
    public class AbcParser
    {
        /// <summary>
        /// Parses the file at <paramref name="filePath"/> into a Song object.
        /// </summary>
        /// <param name="filePath">The file path of the abc file to parse.</param>
        /// <returns>A Song object, which includes two lists containing the notes and lengths of each note.</returns>
        /// <exception cref="Exception">Thrown if there are any issues while parsing the contents of <paramref name="filePath"/>.</exception>
        public static Song ParseFromFile(string filePath)
        {
            if (File.Exists(filePath) == false)
            {
                throw new Exception("File path does not exist: " + filePath);
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
                    // Note that the only purpose of "Key" is to identify accidentals, the actual key is not used.
                    // For example, C major and it's various modes will result in C major being returned.
                    string key = line.Replace(Constants.IDENTIFIER_KEY, string.Empty).Trim();
                    song.Key = Key.FromString(key);

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

            return song;
        }

        /// <summary>
        /// Processes a line of music data in the abc file, adding data to <paramref name="song"/>.
        /// </summary>
        /// <param name="song">The song the data is being parsed into.</param>
        /// <param name="line">The current line of music data.</param>
        /// <exception cref="Exception">Thrown if there are any issues while parsing the music data.</exception>
        private static void ProcessMusicNotes(Song song, string line)
        {
            int lineLength = line.Length;
            int lastProcessedIndex = 0;

            // Track our current set of characters being parsed. This gets reset once a valid note/rest is found/processed.
            string currentNote = string.Empty;

            while (lastProcessedIndex < lineLength)
            {
                char c = line[lastProcessedIndex];

                // If char is space, process the currentNote.
                if (c == ' ')
                {
                    ProcessNote(song, currentNote);
                    lastProcessedIndex++;
                    currentNote = string.Empty;
                    continue;
                }

                // If it's a comment skip the remainder of the line, and if it's a back slash, the line is done
                if (c == '%' || c == '\\')
                {
                    break;
                }

                if (c.IsNote())
                {
                    if (currentNote != string.Empty)
                    {
                        // If we already have a currentNote being built, and we run into a new currentNote, we should process the current currentNote first.
                        ProcessNote(song, currentNote);
                        currentNote = string.Empty;
                    }

                    currentNote += c;
                    lastProcessedIndex++;
                    continue;
                }

                if (c.IsLengthModifier() || c.IsPitchModifier())
                {
                    currentNote += c;
                    lastProcessedIndex++;
                    continue;
                }

                if (c == '"')
                {
                    // This is a chord. Find the next index of ", and resume parsing after that character.
                    int lastIndexOfChord = line.IndexOf('"', lastProcessedIndex + 1);
                    lastProcessedIndex = lastIndexOfChord + 1;
                    continue;
                }

                // Indicates a new bar. We don't care about bars, but it does signify that the note is done and we can process it.
                if (c == '|')
                {
                    ProcessNote(song, currentNote);
                    lastProcessedIndex++;
                    currentNote = string.Empty;
                    continue;
                }

                if (c.IsRest())
                {
                    // TODO not sure about this one, does it work perfectly?
                    currentNote += c;
                    lastProcessedIndex++;
                    continue;
                }

                throw new Exception($"Unknown character encountered: {c}");
            }
        }

        /// <summary>
        /// Processes a string of text reresenting a note, including length and pitch modifiers.
        /// </summary>
        /// <param name="song">The song being processed.</param>
        /// <param name="rawNote">The raw text being parsed. For example, 'e', 'G3/2', '^a2', etc.</param>
        /// <exception cref="NotImplementedException">This library does not yet handle accidentals in the music data.</exception>
        /// <exception cref="Exception">Thrown if there are any issues while parsing the raw note.</exception>
        private static void ProcessNote(Song song, string rawNote)
        {
            if (string.IsNullOrWhiteSpace(rawNote))
            {
                return;
            }

            // This will return pitch info: letter note, pitch modifiers, accidentals.
            ProcessedNoteInfo processedNoteInfo = ProcessedNoteInfo.FromRawNote(rawNote);

            if (processedNoteInfo.Note.IsRest())
            {
                song.NoteValues.Add(-1);
                song.NoteLengths.Add(processedNoteInfo.LengthModifier);
                return;
            }

            // Take the raw note given.
            // Determine it's actual note given the songs key (i.e. if key is G, and note is F, bump to F#
            // Apply Accidentals.
            // Add on any pitch modifiers (the Note itselves capitalization also matters).
            // Find the resulting string index in Constants.NOTES.

            Key key = song.Key;

            // Find any accidentals in this key for the found note: i.e. if the note is 'C', do any accidentals start with 'C'.
            string matchingAccidental = key.Accidentals.FirstOrDefault(a => char.ToUpper(a[0]) == char.ToUpper(processedNoteInfo.Note)) ?? string.Empty;

            string parsedNote = string.IsNullOrEmpty(matchingAccidental) ? processedNoteInfo.Note.ToString() : matchingAccidental;

            if (string.IsNullOrEmpty(processedNoteInfo.AccidentalModifiers) == false)
            {
                throw new NotImplementedException(); // TODO
            }

            parsedNote += processedNoteInfo.PitchModifiers;

            int noteValue = Constants.NOTES.IndexOf(parsedNote);

            if (noteValue == -1)
            {
                throw new Exception($"Unable to determine note value for: {parsedNote}");
            }

            song.NoteValues.Add(noteValue);
            song.NoteLengths.Add(processedNoteInfo.LengthModifier);
        }

        private static int NoteToNumber(string note)
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