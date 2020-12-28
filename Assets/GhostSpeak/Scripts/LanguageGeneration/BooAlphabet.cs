using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents an alphabet in ghost language and provides translation services between Latin and boo letters
/// </summary>
public class BooAlphabet
{
    /// <summary>
    /// Gets the available boo letters
    /// </summary>
    private static readonly string[] AVAILABLE_BOOS =
    {
        "Boo",
        "Bho",
        "Bwo",
        "Buu",
        "Buo",
        "Ooh",
        "Ooo",
        "Ouo",
        "Oee",
        "Huu",
        "Hou",
        "Hoo",
        "Hue",
        "Uuu",
        "Uhu",
        "Uhh",
        "Uee",
        "Uuo",
        "Who",
        "Woo",
        "Wuu",
        "Whu",
        "Eee",
        "Eeo",
        "Eeu",
        "Euu"
    };

    /// <summary>
    /// Gets the mappings for this boo alphabet configuration
    /// </summary>
    public BooToLetterMapping[] Mappings { get; }

    /// <summary>
    /// Creates a new boo alphabet
    /// </summary>
    /// <param name="rng"></param>
    public BooAlphabet()
    {
        IEnumerable<char> letters = Enumerable.Range((int)'A', (int)'Z')
            .Select(i => (char)i);

        IEnumerable<string> boos = AVAILABLE_BOOS
            .OrderBy(i => UnityEngine.Random.value);

        Mappings = letters.Zip(
            second: boos,
            resultSelector: (letter, boo) => new BooToLetterMapping(letter, boo)
        ).ToArray();
    }

    /// <summary>
    /// Gets the letter associated with the provided boo letter
    /// </summary>
    /// <param name="boo"></param>
    /// <returns></returns>
    public char BooToLetter(string boo)
    {
        return Mappings.Single(i => i.BooLetter.ToUpper() == boo.ToUpper()).LatinLetter;
    }

    /// <summary>
    /// Gets the letter associated with the provided letter
    /// </summary>
    /// <param name="letter"></param>
    /// <returns></returns>
    public string LetterToBoo(char letter)
    {
        if (letter == ' ')
            return "   ";

        return Mappings.SingleOrDefault(i => char.ToUpper(i.LatinLetter) == char.ToUpper(letter))?.BooLetter ?? "???";
    }

    /// <summary>
    /// Converts the provided Latin string into a boo string
    /// </summary>
    /// <param name="latinString"></param>
    /// <returns></returns>
    public string ToBooString(string latinString)
    {
        return string.Join(" ", latinString.Select(LetterToBoo));
    }

    /// <summary>
    /// Converts the provided boo string into a Latin string
    /// </summary>
    /// <param name="latinString"></param>
    /// <returns></returns>
    public string ToLatinString(string booString)
    {
        booString = booString.ToUpper().Trim();
        return string.Concat(booString.Split().Select(BooToLetter));
    }

    /// <summary>
    /// Represents a mapping between a Latin and a boo letter
    /// </summary>
    public class BooToLetterMapping
    {
        /// <summary>
        /// Gets the Latin letter of the mapping
        /// </summary>
        public char LatinLetter { get; }

        /// <summary>
        /// Gets the boo letter of the mapping
        /// </summary>
        public string BooLetter { get; }

        /// <summary>
        /// Creates a new mapping
        /// </summary>
        /// <param name="letter"></param>
        /// <param name="boo"></param>
        public BooToLetterMapping(char letter, string boo)
        {
            LatinLetter = letter;
            BooLetter = boo;
        }
    }
}
