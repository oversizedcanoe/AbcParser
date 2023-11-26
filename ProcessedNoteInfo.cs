namespace AbcParser
{
    public class ProcessedNoteInfo
    {
        /// <summary>
        /// A single note character representing the note, i.e. 'C' or 'f'.
        /// </summary>
        public char Note { get; set; }

        /// <summary>
        /// List of abc format accidental modifiers to be applied to this note, i.e. _ ^ = 
        /// </summary>
        public string AccidentalModifiers { get; set; } = string.Empty;

        /// <summary>
        /// List of abc format pitch modifiers to be applied to this note, i.e. , '
        /// </summary>
        public string PitchModifiers { get; set; } = string.Empty;

        private string _lengthModifierString { get; set; } = string.Empty;
        
        /// <summary>
        /// The length of this note compared to the abc file's 'L' header (note length as a proportion of a bar.
        /// </summary>
        public decimal LengthModifier { get; set; } = 1;

        /// <summary>
        /// Creates a ProcessedNoteInfo object.
        /// </summary>
        /// <param name="rawNote">A string containing all note info pulled from an abc file.</param>
        /// <returns></returns>
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
                    processedNoteInfo.AccidentalModifiers += c;
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
