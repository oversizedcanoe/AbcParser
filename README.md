# AbcParser
This library is designed to parse an abc file into an easy to use C# object.

> :warning: This library is still under construction! It should work on most basic songs, but is far from feature complete. See the Todo section at the bottom. 

[This file was used as a guide for the abc file standards](https://abcnotation.com/wiki/abc:standard:v2.1), as well as [this file](https://thecelticroom.org/abc-music-notation/abc-notation-read-and-write.html).


## Usage
To parse an abc file into a `Song` object, simply call `ParseFromFile(...)`, passing in the file path of the abc file you wish to parse.

```csharp
using AbcParser;

Song song = AbcParser.AbcParser.ParseFromFile("./JingleBells.abc");
```

The `Song` class contains useful information parsed from the file, including title, BPM, Key, Meter, etc. However, the pièce(s) de résistance are the `NoteValues` and `NoteLengths` lists. These contain numeric representation of the notes in the song, making it easily readable by other programs.

The `NoteValues` list contain each notes index from [a list of notes](https://github.com/oversizedcanoe/AbcParser/blob/main/Constants.cs#L24), where [0] represents the C three octaves below middle C, and [71] represents the B two octaves about middle C. The value of the string at this index is the abc formatted note, i.e. `C,,` for [0] and `b''` for [71]. Note that a value of -1 in this list indicates a rest.

The `NoteLengths` list contain each notes length as a proportion of `song.NoteLengthProportion`. More info on how to use this below, but for example, a value of 1 indicates a length of 100% of normal note length, a value of 0.5 indicates a length of 50% of normal note length.


## Putting it all together
Below is a very simple example which will perform a `Console.Beep()` for each note in the song, at the correct hertz, for the correct time.
Note that `Console.Beep()` is only supported on windows.

```csharp
  Song song = AbcParser.AbcParser.ParseFromFile("./JingleBells.abc");

  // Determine the length of each note in milliseconds.
  int bpm = song.BPM > 0 ? song.BPM : 100;
  decimal beatsPerSecond = decimal.Divide(bpm, 60);
  decimal secondsPerBeat = 1 / beatsPerSecond;
  decimal notesPerBeat = 1 / song.NoteLengthProportion;
  decimal secondsPerNote = secondsPerBeat / notesPerBeat;

  for (int i = 0; i < song.NoteValues.Count; i++)
  {
      int note = song.NoteValues[i];
      decimal noteLengthMultiplier = song.NoteLengths[i];
      int noteLengthMS = (int)(secondsPerNote * noteLengthMultiplier * 1000);

      if (note == -1)
      {
          // Rest
          Task.Delay(noteLengthMS).Wait();
      }
      else
      {
          Console.Beep((int)Constants.TONES[note], noteLengthMS);
      }
  }
```

Note: Your computer may not be capable of `Console.Beep()`-ing at the actual tone of some songs. For basic songs that don't stretch much of the note range, adding a multiple of 12 (one full octave) to the note index used for the `TONES` list (i.e. `Console.Beep((int)Constants.TONES[note + 24], noteLengthMS);` will bump the frequency up to be audible. 

Also note: BPM is not a required field (for some reason...) in abc files. Because of this, your library should check if the resulting `song.BPM` is 0, and specify a default BPM if this is the case.

## Todo
* Square bracket parsing
* Better error handling
* Better file validation
* Add a `ParseFromLines(IEnumerable<string> lines)` method
* Several properties of `Song` could be made into Enums or made into their own class. For example, perhaps `Meter` could be an Enum?
* Solution structure should be edited. I made this library quickly for use elsewhere, and I didn't really take time to organize things nicely. Certain things could be moved around or split into different files.
* The determination of what Key a song is in will always return the major key. This should be fixed. The string representation of the key should be saved to the `Key` class as well (instead of using a `KeyEnum`).
* Verify rests work properly
* If a note has an accidental applied to it, it will not apply the accidental and change the note. This _does_ work when it's an accidental as part of the key -- i.e. the library knows an F in the key of G Major is actually F# -- however if an F# is in a C Major song, the library will read this as F.
* Short hand length modifiers are not working. I.e. "G/" is shorthand for "G/2", and "G//2" is short hand for "G/4". This will not work yet.
* Probably much more :) 
