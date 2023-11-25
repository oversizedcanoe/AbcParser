using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AbcParser
{
    public class ProcessedNoteInfo
    {
        public char Note { get; set; }
        public string Accidentals { get; set; } = string.Empty;
        public string PitchModifiers { get; set; } = string.Empty;

        private string _lengthModifierString { get; set; } = string.Empty;
        public decimal LengthModifier { get; set; } = 1;

        public static ProcessedNoteInfo FromRawNote(string rawNote)
        {
            ProcessedNoteInfo processedNoteInfo = new ProcessedNoteInfo();

            foreach (char c in rawNote)
            {
                if (c.IsNote())
                {
                    processedNoteInfo.Note = c;
                    continue;
                }

                if (c.IsRest())
                {
                    processedNoteInfo.Note = c;
                    continue;
                }

                if (c.IsAccidental())
                {
                    processedNoteInfo.Accidentals += c;
                    continue;
                }

                if (c.IsPitchModifier())
                {
                    processedNoteInfo.PitchModifiers += c;
                    continue;
                }

                if (c.IsLengthModifier())
                {
                    if (processedNoteInfo.Note == default)
                    {
                        // We haven't set the note yet, this is prefixed with a length modifier.
                        processedNoteInfo.LengthModifier = int.Parse(c.ToString());
                    }
                    else
                    {
                        // We have set the note, this is suffixed with a length modifier.
                        processedNoteInfo._lengthModifierString += c;
                    }
                 
                    continue;
                }
            }

            if (processedNoteInfo.LengthModifier == 1 && string.IsNullOrEmpty(processedNoteInfo._lengthModifierString) == false)
            {
                // TODO!!!  G/ is short hand for G/2, G//2 is short hand for G/4 
                // It will look like either '3', '3/2', or '/4'.
                string[] lengthModifierSegments = processedNoteInfo._lengthModifierString.Split('/');

                if(lengthModifierSegments.Count() == 1) // i.e. "3"
                {
                    processedNoteInfo.LengthModifier = decimal.Parse(lengthModifierSegments[0]);
                }
                else if(lengthModifierSegments.Count() == 2) // i.e. "", "4"
                {
                    processedNoteInfo.LengthModifier = Decimal.Divide(1, int.Parse(lengthModifierSegments[1]));
                }
                else if (lengthModifierSegments.Count() == 3) // i.e. "3", "", "2"
                {
                    processedNoteInfo.LengthModifier = Decimal.Divide(int.Parse(lengthModifierSegments[0]), int.Parse(lengthModifierSegments[2]));
                }
            }

            return processedNoteInfo;
        }
    }
}
