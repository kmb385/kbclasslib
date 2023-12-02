﻿namespace KbClassLib;

/// <summary>
/// An implementation of a string created by Kevin Bowersox.
/// 
/// "I am not concerned that you have fallen, I am concerned that you arise." - Abraham Lincoln
/// 
/// </summary>
public class KbString
{
    /// <summary>
    /// Index of not found constant for readability.
    /// </summary>
    private const int IndexNotFound = -1;

    /// <summary>
    /// First Character Index constant for readability.
    /// </summary>
    private const int FirstCharIndex = 0;

    /// <summary>
    /// An <see cref="Array"/> representing the sequence of <see cref="char"/> in the <see cref="KbString"/>.
    /// </summary>
    private readonly char[] characters;

    /// <summary>
    /// Constructor for <see cref="KbString"/> that accepts a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">A <see cref="string"/> value to create the <see cref="KbString"/> from.</param>
    /// <exception cref="ArgumentException">Thrown when value is null.</exception>
    public KbString(string value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        this.characters = value.ToCharArray();
    }

    /// <summary>
    /// Constructor for <see cref="KbString"/> that accepts a <see cref="char[]"/> value.
    /// </summary>
    /// <param name="characters">A <see cref="char[]"/> value to create the <see cref="KbString"/> from.</param>
    /// <exception cref="ArgumentException">Thrown when characters is null.
    public KbString(char[] characters)
    {
        if (characters == null)
        {
            throw new ArgumentNullException(nameof(characters));
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
        return this.IndexOf(target, 0) != IndexNotFound;
    }

    /// <summary>
    /// Returns true when the provided <see cref="string"/> is present at the begining of this <see cref="KbString"/>.
    /// </summary>
    /// <param name="target">The target <see cref="String"/> to find a match for.</param>
    /// <returns>A <see cref="bool"/> that is true when a match is found; Otherwise false.</returns>
    public bool StartsWith(string target)
    {
        return this.IndexOf(target, 0) == 0;
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

    /// <summary>
    /// Returns an <see cref="int"/> representing the zero-based index where the start of the provided token
    /// was first found within this <see cref="KbString"/>.
    /// 
    /// An empty <see cref="string"/> is 
    /// </summary>
    /// <param name="token">A <see cref="string"/> token to locate.</param>
    /// <returns>An <see cref="int"/> representing the zero-based index where the state of the provided token
    /// was first found within this <see cref="KbString"/>.  If the token is not present within this <see cref="KbString"/>
    /// the value -1 is returned</returns>
    public int IndexOf(string token, int startIndex)
    {
        if (token == null)
        {
            throw new ArgumentNullException(nameof(token));
        }

        if(token == string.Empty)
        {
            // Set theory denotes the empty set as a member of the set.  Established convention in major languages.
            return startIndex;
        }

        if(this.characters.Length < token.Length)
        {
            return IndexNotFound;
        }

        int firstCharacter = 0;
        int sourceLength = this.characters.Length;

        for (int sourceOffset = startIndex; sourceOffset < sourceLength; sourceOffset++)
        {
            if (this.characters[sourceOffset] != token[firstCharacter])
            {
                continue;
            }

            for (int tokenIndex = firstCharacter; tokenIndex < token.Length; tokenIndex++)
            {
                int matchIndex = sourceOffset + tokenIndex;

                if (matchIndex >= sourceLength || this.characters[matchIndex] != token[tokenIndex])
                {
                    break;
                }

                if(tokenIndex == token.Length - 1)
                {
                    return sourceOffset;
                }
            }
        }

        return IndexNotFound;
    }

    /// <summary>
    /// Returns a new <see cref="KbString"/> with the first instance of the provided search token replaced
    /// by the provided replaceToken.
    /// 
    /// Time Complexity: Constant time O(n).
    /// </summary>
    /// <param name="searchToken">A <see cref="string"/> to replace.</param>
    /// <param name="replaceToken">A <see cref="string"/> to substitute for the searchToken.</param>
    /// <returns>A <see cref="string"/> with the first instance of the provided search token replaced
    /// by the provided replaceToken.
    /// </returns>
    public string ReplaceFirst(string searchToken, string replaceToken)
    {
        if(searchToken == null)
        {
            throw new ArgumentNullException(nameof(searchToken));
        }

        if(replaceToken == null)
        {
            throw new ArgumentNullException(nameof(replaceToken));
        }

        // Attempt to find first occurrence.
        int firstMatch = this.IndexOf(searchToken, 0);

        // Short circuit when no search token is present in string.
        if(firstMatch == IndexNotFound)
        {
            return new string(this.characters);
        }

        /*
         * Compute the length of the result.
         *
         * "123456".ReplaceFirst("1","12"); = (6 + 2) - 1 = 7 = "1223456".Length
        */
        int resultLength = (this.characters.Length + replaceToken.Length) - searchToken.Length;
        char[] resultCharacters = new char[resultLength];

        // Cursor to track position in the result set under construction.
        int resultIndex = 0;

        // Cursor to track position in the source string as its copied over to result.
        int sourceIndex = 0;

        // Copy source characters prior to the start of replacement.
        while(resultIndex < firstMatch)
        {
            resultCharacters[sourceIndex++] = this.characters[resultIndex++];
        }

        // Cursor to track position of replacement string as its copied over to result.
        int replaceIndex = 0;

        // Copy characters to be replaced. Account for empty string.
        while(replaceToken.Length > 0 && replaceIndex < replaceToken.Length)
        {
            resultCharacters[resultIndex++] = replaceToken[replaceIndex++];
        }

        // Skip copy of characters in the source that should be replaced.
        sourceIndex += searchToken.Length;

        // Copy source characters after the replacement if they exist.
        while(resultIndex < resultCharacters.Length)
        {
            resultCharacters[resultIndex++] = this.characters[sourceIndex++];
        }

        return new string(resultCharacters);
    }

    /// <summary>
    /// Returns a new <see cref="String"/> with all occurrences of the provided search token replaced by the provided
    /// replace token.
    /// </summary>
    /// <param name="searchToken">A <see cref="string"/> to replace.</param>
    /// <param name="replaceToken">A <see cref="string"/> to substitute for the searchToken.</param>
    /// <returns>A <see cref="string"/> with all instances of the provided search token replaced
    /// by the provided replaceToken.
    public string Replace(string searchToken, string replaceToken)
    {
        this.NullGuard(searchToken, nameof(searchToken));
        this.NullGuard(replaceToken, nameof(replaceToken));

        /*
         * "My cat is a bengal cat.".Replace("cat", "feline") => "My feline is a bengal feline." 
         * 
         *  Solution 1: "Replace Next Strategy"
         *  1. Loop while search string is not found in string.
         *  2. When found, replace the search string with the target string.
         *  3. Eventually all occurrences are replaced or loop ends.
         *  4. Note:  Similar to a Find/Replace capability in a text editor, where you can hit "Replace Next (not exact analogy)".
         *  5. Note:  Implemenation has poor time commplexity.  Outer loop is O(n) * O(n) IndexOf/ReplaceFirst invocations = Quadratic Time (N^2)
         *  
         *  Solution 2:
         *  1. Use a list to store the result.
         *  2. Split on the provided search string.
         *  3. Join with the replace token. 
         * 
         * Solution 3:
         * 1. First pass to find the occurrences and their indexes.
         * 2. Drop out if no occurrences. Otherwise proceed. 
         * 3. Calculate the length of the new string (similar to replace first)
         * 4. Loop through source before each occurrence, track position with cursor (sourceCursor)
         * 5. Loop through each occurrence, calculate the start/end, update (replaceCursor)
         * 6. Update sourceCursor to account for copied characters.
         * 7. Loop through remaning source (if necessary), and append to results. 
        */

        // Implementation: Solution 1 Replace Next

        // Copy this string to seed result and maintain immutability.
        KbString result = new KbString(this.characters);

        for(int matchIndex = result.IndexOf(searchToken, FirstCharIndex); // Start with the (potential) first occurrence
            matchIndex != IndexNotFound; // Iterate while there is a match to replace
            matchIndex = result.IndexOf(searchToken, matchIndex + replaceToken.Length)) // Search for next match on each occurrence.
        {
            // TODO: Replace string type return values in a refactor.
            result = new KbString(result.ReplaceFirst(searchToken, replaceToken));
        }

        return new String(result.characters);
    }

    private void NullGuard(string parameterName, string parameterValue)
    {
        // TODO: Replace explicit guard logic with helper in a refactor. 
        if(parameterValue == null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }
}
