using System.Text;

namespace AbcParser
{
    /// <summary>
    /// Represents all data contained in an abc file.
    /// </summary>
    public class Song
    {
        /// <summary>
        /// Song number.
        /// </summary>
        public int SongNumber { get; set; }

        /// <summary>
        /// Song title. 
        /// </summary>
        public string SongTitle { get; set;}

        /// <summary>
        /// Song secondary title.
        /// </summary>
        public string? SongSubtitle { get; set;}

        /// <summary>
        /// Composer of the song. May be 'traditional' or 'trad'.
        /// </summary>
        public string Composer { get; set;}

        /// <summary>
        /// The meter for the song, such as 4/4, 6/8, 3/4.
        /// </summary>
        public string Meter { get; set;}

        /// <summary>
        /// The note length as a proportion of a bar. For example, if 0.25, this means each note is 1/4 of a bar.
        /// </summary>
        public decimal NoteLengthProportion { get; set; }

        /// <summary>
        /// The name of the rhythm of the song.
        /// </summary>
        public string Rhythm { get; set; }

        /// <summary>
        /// The Key of the song.
        /// </summary>
        public Key Key { get; set; } = Key.FromString("C");

        /// <summary>
        /// Origin of the song.
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Beats per minute of the song.
        /// </summary>
        public int BPM { get; set; }

        /// <summary>
        /// A list of the numeric representation of the notes in this song.
        /// Note names and tones can be retrieved using the NOTES and TONES lists in the <see cref="Constants"/> class.
        /// A value of -1 indiciates a rest.
        /// </summary>
        public List<int> NoteValues { get; set; } = new List<int>();

        /// <summary>
        /// A list of the note lengths of each note in this song. This, along with BPM and NoteLengthProportion can be used to determine
        /// the note length in milliseconds.
        /// </summary>
        public List<decimal> NoteLengths { get; set; } = new List<decimal>();

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach(var property in typeof(Song).GetProperties())
            {
                stringBuilder.AppendLine($"{property.Name}: {property.GetValue(this)}");
            }

            return stringBuilder.ToString();
        }
    }
}
