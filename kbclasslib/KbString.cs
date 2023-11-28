namespace KbClassLib;

/// <summary>
/// An implementation of a string created by Kevin Bowersox.
/// 
/// "I am not concerned that you have fallen, I am concerned that you arise." - Abraham Lincoln
/// 
/// </summary>
public class KbString
{
    private readonly char[] characters;

    /// <summary>
    /// Constructor for <see cref="KbString"/> that accepts a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">A <see cref="string"/> value to create the <see cref="KbString"/> from.</param>
    /// <exception cref="ArgumentException">Thrown when value is null or empty.</exception>
    public KbString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(nameof(value));
        }

        this.characters = value.ToCharArray();
    }

    /// <summary>
    /// Constructor for <see cref="KbString"/> that accepts a <see cref="char[]"/> value.
    /// </summary>
    /// <param name="characters">A <see cref="char[]"/> value to create the <see cref="KbString"/> from.</param>
    /// <exception cref="ArgumentException">Thrown when characters is null or empty.
    public KbString(char[] characters)
    {
        if (characters == null || characters.Length == 0)
        {
            throw new ArgumentException(nameof(characters));
        }

        this.characters = characters;
    }

    /// <summary>
    /// Returns true if the provided target <see cref="string"/> is found within this <see cref="string"/>.
    /// </summary>
    /// <param name="target">A target <see cref="string"/> to search for.</param>
    /// <returns>Returns <see cref="bool"/> true when this <see cref="string"/> contains the provided target.</returns>
    public bool Contains(string target)
    {
        if (string.IsNullOrWhiteSpace(target))
        {
            throw new ArgumentException(nameof(target));
        }

        if (target.Length > this.characters.Length)
        {
            return false;
        }

        int firstCharacter = 0;

        for (int sourceOffset = 0; sourceOffset < this.characters.Length; sourceOffset++)
        {
            if (this.characters[sourceOffset] != target[firstCharacter])
            {
                continue;
            }

            for (int targetIndex = firstCharacter; targetIndex < target.Length; targetIndex++)
            {
                int matchIndex = sourceOffset + targetIndex;
                if (matchIndex >= this.characters.Length || this.characters[matchIndex] != target[targetIndex])
                {
                    break;
                }

                if (targetIndex == target.Length - 1)
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Returns true when the provided <see cref="string"/> is present at the begining of this <see cref="KbString"/>.
    /// </summary>
    /// <param name="target">The target <see cref="String"/> to find a match for.</param>
    /// <returns>A <see cref="bool"/> that is true when a match is found; Otherwise false.</returns>
    public bool StartsWith(string target)
    {
        if (string.IsNullOrWhiteSpace(target))
        {
            throw new ArgumentException(nameof(target));
        }

        if (this.characters.Length < target.Length)
        {
            return false;
        }

        bool result = true;

        for (int targetIndex = 0; targetIndex < target.Length; targetIndex++)
        {
            if (this.characters[targetIndex] != target[targetIndex])
            {
                result = false;
                break;
            }
        }

        return result;
    }

    /// <summary>
    /// Returns a <see cref="string"/> representing a subsequence of this <see cref="KbString"/> starting at the provided
    /// index with the specified length.
    /// </summary>
    /// <param name="start">An <see cref="int"/> representing a zero-based index to begin the subsequence.</param>
    /// <param name="length">An <see cref="int"/> representing the length of the subsequence.</param>
    /// <returns>A <see cref="string"/> subsequence starting at the provided index of the provided length.</returns>
    public string Substring(int start, int length)
    {
        if (start < 0 || this.characters.Length < start)
        {
            throw new ArgumentException(nameof(start));
        }

        if (length <= 0 || this.characters.Length < length)
        {
            throw new ArgumentException(nameof(length));
        }

        int last = start + length;

        if (this.characters.Length < last)
        {
            throw new ArgumentException(nameof(length));
        }

        int copyIndex = 0;
        char[] result = new char[length];

        for (int sourceIndex = start; sourceIndex < last; sourceIndex++)
        {
            result[copyIndex++] = this.characters[sourceIndex];
        }

        return new String(result);
    }

    /// <summary>
    /// Returns an <see cref="Array"/> of <see cref="string"/>s that appear around or between the delimeter.
    /// </summary>
    /// <param name="delimiter">A delimiter to split this <see cref="KbString"/> around.</param>
    /// <returns>An <see cref="Array"/> of <see cref="string"/>.</returns>
    public string[] Split(string delimiter)
    {
        if (string.IsNullOrWhiteSpace(delimiter))
        {
            throw new ArgumentException(nameof(delimiter));
        }

        List<string> tokens = new();

        int sourceIndex = 0;
        int sourceLength = this.characters.Length;
        int tokenizedSourceOffset = 0;

        // Only tokenize from tokens 0...N-1.
        while (sourceIndex <= sourceLength - delimiter.Length)
        {
            if(delimiter != this.Substring(sourceIndex, delimiter.Length))
            {
                sourceIndex++;
                continue;
            }

            // Compute length of token in the split.
            int tokenLength = sourceIndex - tokenizedSourceOffset;

            tokens.Add(tokenLength == 0
                ? string.Empty // No characters prior to the last delimiter, add empty string.
                : this.Substring(tokenizedSourceOffset, tokenLength)); // Copy all characters prior to the delimiter.

            // Advance sourceIndex past last delimiter match using non-zero based Length property.
            sourceIndex += delimiter.Length;
            // Position the index for the next copy. 
            tokenizedSourceOffset = sourceIndex;
        }

        // Handle final token, which is either a delimiter or a token.

        // Compute length of final token.
        int untokenizedSourceLength = sourceLength - tokenizedSourceOffset;

        tokens.Add(untokenizedSourceLength == 0
            ? string.Empty // No characters prior to the last delimiter add empty string.
            : this.Substring(tokenizedSourceOffset, untokenizedSourceLength)); // Copy all characters prior to the delimiter.

        return tokens.ToArray();
    }
}
