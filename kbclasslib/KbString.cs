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
        if(characters == null || characters.Length == 0)
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

        if(target.Length > this.characters.Length)
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

                if(targetIndex == target.Length - 1)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
