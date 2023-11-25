using System.Text;

namespace AbcParser
{
    public class Song
    {
        public Song() 
        { 
        
        }

        public int SongNumber { get; set; }
        public string SongTitle { get; set;}
        public string? SongSubtitle { get; set;}
        public string Composer { get; set;}
        public string Meter { get; set;}
        public decimal NoteLengthProportion { get; set; }
        public string Rhythm { get; set; }
        public string Key { get; set; }
        public string Origin { get; set; }
        public int BPM { get; set; }

        public List<int> NoteValues { get; set; } = new List<int>();
        public List<int> NoteLengths { get; set; } = new List<int>();


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
