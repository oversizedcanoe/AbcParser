namespace AbcParser
{
    public class Key
    {
        public enum KeyEnum
        {
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

        public Key(KeyEnum keyEnum)
        {
            switch (keyEnum)
            {
                case KeyEnum.C:
                case KeyEnum.Am:
                    this.Accidentals = new List<string>();
                    break;
                case KeyEnum.Cs:
                case KeyEnum.Db:
                case KeyEnum.Bbm:
                case KeyEnum.Asm:
                    this.Accidentals = new List<string>() { "F#", "C#", "G", "D#", "A#", "E#", "B#" };
                    break;
                case KeyEnum.D:
                case KeyEnum.Bm:
                    this.Accidentals = new List<string>() { "F#", "C#" };
                    break;
                case KeyEnum.Eb:
                case KeyEnum.Ds:
                case KeyEnum.Cm:
                    this.Accidentals = new List<string>() { "Bb", "Eb", "Ab" };
                    break;

                case KeyEnum.E:
                case KeyEnum.Csm:
                case KeyEnum.Dbm:
                    this.Accidentals = new List<string>() { "F#", "C#", "G", "D#" };
                    break;
                case KeyEnum.F:
                case KeyEnum.Dm:
                    this.Accidentals = new List<string>() { "Bb" };
                    break;
                case KeyEnum.Fs:
                case KeyEnum.Gb:
                case KeyEnum.Ebm:
                case KeyEnum.Dsm:
                    this.Accidentals = new List<string>() { "F#", "C#", "G", "D#", "A#", "E#" };
                    break;

                case KeyEnum.G:
                case KeyEnum.Em:
                    this.Accidentals = new List<string>() { "F#" };
                    break;
                case KeyEnum.Ab:
                case KeyEnum.Gs:
                case KeyEnum.Fm:
                    this.Accidentals = new List<string>() { "Bb", "Eb", "Ab", "Db" };
                    break;
                case KeyEnum.A:
                case KeyEnum.Gbm:
                case KeyEnum.Fsm:
                    this.Accidentals = new List<string>() { "F#", "C#", "G" };
                    break;
                case KeyEnum.Bb:
                case KeyEnum.As:
                case KeyEnum.Gm:
                    this.Accidentals = new List<string>() { "Bb", "Eb" };
                    break;
                case KeyEnum.B:
                case KeyEnum.Abm:
                case KeyEnum.Gsm:
                    this.Accidentals = new List<string>() { "F#", "C#", "G", "D#", "A#" };
                    break;
                default:
                    throw new Exception($"Unknown KeyEnum value passed: {keyEnum}");
            }
        }

        public static Key FromString(string key)
        {
            // There is 100% a more efficient way to do this programmatically...
            switch (key.ToLower())
            {
                case "c#" or "a#m" or "g#mix" or "d#dor" or "e#phr" or "f#lyd" or "b#loc":
                    return new Key(KeyEnum.Cs);
                case "f#" or "d#m" or "c#mix" or "g#dor" or "a#phr" or "blyd" or "e#loc":
                    return new Key(KeyEnum.Fs);
                case "b" or "g#m" or "f#mix" or "c#dor" or "d#phr" or "elyd" or "a#loc":
                    return new Key(KeyEnum.B);
                case "e" or "c#m" or "bmix" or "f#dor" or "g#phr" or "alyd" or "d#loc":
                    return new Key(KeyEnum.E);
                case "a" or "f#m" or "emix" or "bdor" or "c#phr" or "dlyd" or "g#loc":
                    return new Key(KeyEnum.A);
                case "d" or "bm" or "amix" or "edor" or "f#phr" or "glyd" or "c#loc":
                    return new Key(KeyEnum.D);
                case "g" or "em" or "dmix" or "ador" or "bphr" or "clyd" or "f#loc":
                    return new Key(KeyEnum.G);
                case "c" or "am" or "gmix" or "ddor" or "ephr" or "flyd" or "bloc":
                    return new Key(KeyEnum.C);
                case "f" or "dm" or "cmix" or "gdor" or "aphr" or "bblyd eloc":
                    return new Key(KeyEnum.F);
                case "bb" or "gm" or "fmix" or "cdor" or "dphr" or "eblyd aloc":
                    return new Key(KeyEnum.Bb);
                case "eb" or "cm" or "bbmix" or "fdor" or "gphr" or "ablyd dloc":
                    return new Key(KeyEnum.Eb);
                case "ab" or "fm" or "ebmix" or "bbdor" or "cphr" or "dblyd" or "gloc":
                     return new Key(KeyEnum.Ab);
                case "db" or "bbm" or "abmix" or "ebdor" or "fphr" or "gblyd" or "cloc":
                    return new Key(KeyEnum.Db);
                case "gb" or "ebm" or "dbmix" or "abdor" or "bbphr" or "cblyd" or "floc":
                    return new Key(KeyEnum.Gb);
                default:
                    throw new Exception($"Unable to parse key from: {key}");
            }
        }

        public List<string> Accidentals { get; set; }
    }
}
