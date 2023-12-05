namespace KbClassLib;

/// <summary>
/// A limited string implementation created by Kevin Bowersox.
///
/// Supported Methods/Operators: Addition, Comparison, Compare Contains, Equals, Indexer, IndexOf, LastIndexOf, Replace, ReplaceFirst, Split, StartsWith
/// 
/// Popular methods currently not supported: Clone, CompareTo, Concat, EndsWith, Format, HashCode, Insert, IsNullOrXXX, Join, Pad, Remove, ToUpper/Lower, Trim.
///
/// </summary>
public class KbString
{
    /// <summary>
    /// Index of not found constant for readability.
    /// </summary>
    private const int IndexNotFound = -1;
 
    /// <summary>
    /// Compare greater than constant for readability.
    /// </summary>
    private const int GreaterThan = 1;

    /// <summary>
    /// Compare less than constant for readability.
    /// </summary>
    private const int LessThan = -1;

    /// <summary>
    /// Compare equal constant for readability.
    /// </summary>
    private const int Equal = 0;

    /// <summary>
    /// First Character Index constant for readability.
    /// </summary>
    private const int FirstCharIndex = 0;

    /// <summary>
    /// An <see cref="Array"/> representing the sequence of <see cref="char"/> in the <see cref="KbString"/>.
    /// </summary>
    private readonly char[] characters;

    /// <summary>
    /// Empty string constant for readability and ease of use.
    /// </summary>
    public static readonly KbString Empty = new KbString("");

    /// <summary>
    /// Constructor for <see cref="KbString"/> that accepts a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">A <see cref="string"/> value to create the <see cref="KbString"/> from.</param>
    /// <exception cref="ArgumentException">Thrown when value is null.</exception>
    public KbString(string value)
    {
        NullGuard(nameof(value), value);

        this.characters = value.ToCharArray();
    }

    /// <summary>
    /// Constructor for <see cref="KbString"/> that accepts a <see cref="char[]"/> value.
    /// </summary>
    /// <param name="characters">A <see cref="char[]"/> value to create the <see cref="KbString"/> from.</param>
    /// <exception cref="ArgumentException">Thrown when characters is null.
    public KbString(char[] characters)
    {
        NullGuard(nameof(characters), characters);

        this.characters = characters;
    }

    /// <summary>
    /// Return the length of the <see cref="KbString"/>
    /// </summary>
    public int Length => this.characters.Length;

    /// <summary>
    /// Compares the two provided <see cref="KbString"/> instances for order.
    /// </summary>
    /// <param name="object1">The first instance of <see cref="KbString"/> to compare.</param>
    /// <param name="object2">The second instance of <see cref="KbString"/> to compare.</param>
    /// <returns>An integer representing if the first argument is less than (-1), equalt to (0), or great than (1) the
    /// second argument.</returns>
    public static int Compare(KbString object1, KbString object2)
    {
        NullGuard(nameof(object1), object1);
        NullGuard(nameof(object2), object2);

        // Short circuit for same value.
        if(object1 == object2)
        {
            return Equal;
        }

        int object1Index = 0;
        int object2Index = 0;

        // Seek the first diff between the values or exhaustion of a value;
        while (
            object1Index < object1.Length &&
            object2Index < object2.Length &&
            object1[object1Index] == object2[object2Index])
        {
            object1Index++;
            object2Index++;
        }

        // Diff found prior to exhaustion. Potentially at last position of both values, but not exhausted.
        if(object1Index <= object1.Length - 1 && object2Index <= object2.Length - 1)
        {
            if (object1[object1Index] == object2[object2Index])
            {
                return Equal;
            }

            // CompareTo contract does not guarantee 1, 0 or -1.  Only greater than 0, less than 0. Standardizing for dev ex.
            return object1[object1Index].CompareTo(object2[object2Index]) > 0 ? 1 : -1;
        }

        // One of the values exhausted, meaning all characters matched until this point.  Subtract indexes to position shortest first.
        return object1.Length - object2.Length > 0 ? GreaterThan : LessThan;
    }

    /// <summary>
    /// Returns true if the provided target <see cref="Kbstring"/> is found within this <see cref="KbString"/>.
    /// </summary>
    /// <param name="target">A target <see cref="KbString"/> to search for.</param>
    /// <returns>Returns <see cref="bool"/> true when this <see cref="KbString"/> contains the provided target.</returns>
    public bool Contains(KbString target)
    {
        return this.IndexOf(target, 0) != IndexNotFound;
    }

    /// <summary>
    /// Returns true when the provided <see cref="KbString"/> is present at the begining of this <see cref="KbString"/>.
    /// </summary>
    /// <param name="target">The target <see cref="KbString"/> to find a match for.</param>
    /// <returns>A <see cref="bool"/> that is true when a match is found; Otherwise false.</returns>
    public bool StartsWith(KbString target)
    {
        return this.IndexOf(target, 0) == 0;
    }

    /// <summary>
    /// Returns a <see cref="KbString"/> representing a subsequence of this <see cref="KbString"/> starting at the provided
    /// index with the specified length.
    /// Implementation has linear time complexity O(n)
    /// </summary>
    /// <param name="start">An <see cref="int"/> representing a zero-based index to begin the subsequence.</param>
    /// <param name="length">An <see cref="int"/> representing the length of the subsequence.</param>
    /// <returns>A <see cref="KbString"/> subsequence starting at the provided index of the provided length.</returns>
    public KbString Substring(int start, int length)
    {
        // TODO: Add bounds guard here and tests.
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

        return new KbString(result);
    }

    /// <summary>
    /// Returns an <see cref="Array"/> of <see cref="KbString"/>s that appear around or between the delimeter.
    /// </summary>
    /// <param name="delimiter">A delimiter to split this <see cref="KbString"/> around.</param>
    /// <returns>An <see cref="Array"/> of <see cref="KbString"/>.</returns>
    public KbString[] Split(KbString delimiter)
    {
        if (delimiter == null || delimiter == Empty)
        {
            throw new ArgumentException(nameof(delimiter));
        }

        List<KbString> tokens = new();

        int sourceIndex = 0;
        int sourceLength = this.characters.Length;
        int tokenizedSourceOffset = 0;

        // Only tokenize from tokens 0...N-1.
        while (sourceIndex <= sourceLength - delimiter.Length)
        {
            // Substring Invocation is linear time, effectively making this a nested loop, so overall its quadratic.  TODO: Research if Perf/Refactor is possible
            if (delimiter != this.Substring(sourceIndex, delimiter.Length))
            {
                sourceIndex++;
                continue;
            }

            // Compute length of token in the split.
            int tokenLength = sourceIndex - tokenizedSourceOffset;

            tokens.Add(tokenLength == 0
                ? Empty // No characters prior to the last delimiter, add empty string.
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
            ? Empty // No characters prior to the last delimiter add empty string.
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
    /// <param name="startIndex">A zero based index to begin the operation from.</param>
    /// <returns>An <see cref="int"/> representing the zero-based index where the state of the provided token
    /// was first found within this <see cref="KbString"/>.  If the token is not present within this <see cref="KbString"/>
    /// the value -1 is returned</returns>
    public int IndexOf(KbString token, int startIndex)
    {
        NullGuard(nameof(token), token);

        if (token == Empty)
        {
            // Set theory denotes the empty set as a member of the set.  Established convention in major languages.
            return startIndex;
        }

        if (this.characters.Length < token.Length)
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

                if (tokenIndex == token.Length - 1)
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
    /// Time Complexity: Linear time O(n).
    /// </summary>
    /// <param name="searchToken">A <see cref="KbString"/> to replace.</param>
    /// <param name="replaceToken">A <see cref="KbString"/> to substitute for the searchToken.</param>
    /// <returns>A <see cref="KbString"/> with the first instance of the provided search token replaced
    /// by the provided replaceToken.
    /// </returns>
    public KbString ReplaceFirst(KbString searchToken, KbString replaceToken)
    {
        NullGuard(nameof(searchToken), searchToken);
        NullGuard(nameof(replaceToken), replaceToken);

        // Attempt to find first occurrence.
        int firstMatch = this.IndexOf(searchToken, 0);

        // Short circuit when no search token is present in string.
        if (firstMatch == IndexNotFound)
        {
            return new KbString(this.characters);
        }

        /*
         * Compute the length of the result.
         *
         * "123456".ReplaceFirst("1","12"); = (6 + 2) - 1 == 7 == "1223456".Length
        */
        int resultLength = (this.characters.Length + replaceToken.Length) - searchToken.Length;
        char[] resultCharacters = new char[resultLength];

        // Cursor to track position in the result set under construction.
        int resultIndex = 0;

        // Cursor to track position in the source string as its copied over to result.
        int sourceIndex = 0;

        // Copy source characters prior to the start of replacement.
        while (resultIndex < firstMatch)
        {
            resultCharacters[sourceIndex++] = this.characters[resultIndex++];
        }

        // Cursor to track position of replacement string as its copied over to result.
        int replaceIndex = 0;

        // Copy characters to be replaced. Account for empty string.
        while (replaceToken.Length > 0 && replaceIndex < replaceToken.Length)
        {
            resultCharacters[resultIndex++] = replaceToken[replaceIndex++];
        }

        // Skip copy of characters in the source that should be replaced.
        sourceIndex += searchToken.Length;

        // Copy source characters after the replacement if they exist.
        while (resultIndex < resultCharacters.Length)
        {
            resultCharacters[resultIndex++] = this.characters[sourceIndex++];
        }

        return new KbString(resultCharacters);
    }

    /// <summary>
    /// Returns a new <see cref="String"/> with all occurrences of the provided search token replaced by the provided
    /// replace token.
    /// </summary>
    /// <param name="searchToken">A <see cref="KbString"/> to replace.</param>
    /// <param name="replaceToken">A <see cref="KbString"/> to substitute for the searchToken.</param>
    /// <returns>A <see cref="KbString"/> with all instances of the provided search token replaced
    /// by the provided replaceToken.
    public KbString Replace(KbString searchToken, KbString replaceToken)
    {
        NullGuard(nameof(searchToken), searchToken);
        NullGuard(nameof(replaceToken), replaceToken);

        KbString result = Empty;

        // Cursor to track the last position copied from source to result.
        int copyIndex = 0;

        for (int matchIndex = this.IndexOf(searchToken, FirstCharIndex); // Start with the (potential) first occurrence
            matchIndex != IndexNotFound; // Iterate while there is a match to replace
            matchIndex = this.IndexOf(searchToken, matchIndex + searchToken.Length)) // Search for next match on each occurrence.
        {
            // Length of characters prior to the match.
            int prefixLength = matchIndex - copyIndex;

            if(prefixLength > 0)
            {
                result += this.Substring(copyIndex, prefixLength);
            }

            result += replaceToken;

            // Position of match + skip the search token.
            copyIndex = matchIndex + searchToken.Length;
        }

        int sourceOffset = copyIndex + searchToken.Length - 1;

        // There may be remaining characters after the last match.
        if(copyIndex < this.characters.Length)
        {
            result += this.Substring(copyIndex, this.characters.Length - copyIndex);
        }

        return result;
    }

    /// <summary>
    /// Returns an <see cref="int"/> representing the zero-based index where of the provided token
    /// was first found within this <see cref="KbString"/>.
    /// 
    /// An empty <see cref="string"/> is 
    /// </summary>
    /// <param name="token">A <see cref="string"/> token to locate.</param>
    /// <param name="startIndex">A zero based index to begin the operation from.</param>
    /// <returns>An <see cref="int"/> representing the zero-based index where the state of the provided token
    /// was first found within this <see cref="KbString"/>.  If the token is not present within this <see cref="KbString"/>
    /// the value -1 is returned</returns>
    public int LastIndexOf(string token, int startIndex)
    {
        NullGuard(nameof(token), token);

        // TODO: Add Bounds Guard and tests
        if(startIndex < 0 || startIndex > this.characters.Length - 1)
        {
            throw new ArgumentException(nameof(startIndex));
        }

        if (token == string.Empty)
        {
            // Set theory denotes the empty set as a member of the set.  Established convention in major languages.
            return startIndex;
        }

        if (this.characters.Length < token.Length)
        {
            return IndexNotFound;
        }

        if(startIndex < token.Length - 1)
        {
            return -1;
        }

        // Match cannot be found before (end of string -  searchToken length).
        int lastPotentialMatchIndex = Math.Min(startIndex, this.characters.Length - token.Length);

        for (int sourceIndex = lastPotentialMatchIndex; sourceIndex >= 0; sourceIndex--)
        {
            if (this.characters[sourceIndex] != token[FirstCharIndex])
            {
                continue;
            }

            int tokenIndex = FirstCharIndex;

            while (tokenIndex < token.Length)
            {
                if (this.characters[sourceIndex + tokenIndex] != token[tokenIndex++])
                {
                    break;
                }

                if(tokenIndex == token.Length)
                {
                    return sourceIndex;
                }
            }
        }

        return IndexNotFound;
    }

    /// <summary>
    /// Inserts the provided <see cref="KbString"/> value into this <see cref="KbString"/> at the provided start index.
    /// </summary>
    /// <param name="startIndex">The index at which the insertion should begin.</param>
    /// <param name="value">The value to be inserted.</param>
    /// <returns>A new <see cref="KbString"/> with the provided <see cref="KbString"/> value inserted at the specified index.</returns>
    public KbString Insert(int startIndex, KbString value)
    {
        NullGuard(nameof(value), value);
        BoundsGuard(nameof(startIndex), startIndex);

        char[] result = new char[this.characters.Length + value.Length];
        int exclusiveEndIndex = startIndex + value.Length;

        int sourceIndex = 0;
        int valueIndex = 0;

        // Iterate once per character in the result
        for (int insertionIndex = 0; insertionIndex < result.Length; insertionIndex++)
        {
            // If current index is within the bounds of insertion space, insert new chars.
            if (startIndex <= insertionIndex && insertionIndex < exclusiveEndIndex)
            {
                result[insertionIndex] = value[valueIndex++];
                continue;
            }
            else
            {
                // Otherwise insert from the source.
                result[insertionIndex] = this.characters[sourceIndex++];
            }
        }

        return new KbString(result);
    }

    public char this[int index]
    {
        get
        {
            if(index < 0 || index >= this.characters.Length)
            {
                throw new IndexOutOfRangeException();
            }

            return this.characters[index];
        }

        set
        {
            if(index < 0 || index >= this.characters.Length)
            {
                throw new IndexOutOfRangeException();
            }

            this.characters[index] = value;
        }
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if(obj == null)
        {
            return false;
        }

        if(this == null)
        {
            return false;
        }

        if(obj is not KbString)
        {
            return false;
        }

        KbString objKbString = obj as KbString;

        if(objKbString.characters.Length != this.characters.Length)
        {
            return false;
        }

        for(int x = 0; x < this.characters.Length; x++)
        {
            if (objKbString[x] != this.characters[x])
            {
                return false;
            }
        }

        return true; 
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        // TODO: Implementation
        return base.GetHashCode();
    }

    /// <inheritdoc/>
    public override string? ToString()
    {
        return new string(this.characters);
    }

    #region operator overloads

    public static bool operator ==(KbString a, KbString b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(KbString a, KbString b)
    {
        return !a.Equals(b);
    }

    public static KbString operator +(KbString a, KbString b)
    {
        NullGuard(nameof(a), a);
        NullGuard(nameof(b), b);

        char[] newChars = new char[a.Length + b.Length];

        int ac = 0;

        while(ac < a.Length)
        {
            newChars[ac] = a[ac++];
        }

        int bc = 0;

        while(bc < b.Length)
        {
            newChars[ac++] = b[bc++];
        }

        return new KbString(newChars);
    }

    #endregion

    private static void NullGuard(string parameterName, object parameterValue)
    {
        if (parameterValue == null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }

    private void BoundsGuard(string parameterName, int value, int max = Int32.MaxValue)
    {
        if(0 > value && value > max)
        {
            throw new IndexOutOfRangeException($"{parameterName} is out of range.");
        }
    }
}
