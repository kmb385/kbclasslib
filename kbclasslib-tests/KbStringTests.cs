using KbClassLib;

namespace KbClassLibTests;

/// <summary>
/// Functional tests for a <see cref="KbString"/>.
/// </summary>
[TestClass]
public class KbStringTests
{
    /// <summary>
    /// Tests for successful instance creationg using the string constructor.
    /// </summary>
    [TestMethod]
    public void Constructor_String_Succeeds()
    {
        Assert.IsNotNull(new KbString(""));
        Assert.IsNotNull(new KbString(" "));
        Assert.IsNotNull(new KbString("b"));
        Assert.IsNotNull(new KbString("bengal"));
        Assert.IsNotNull(new KbString("bengal cat"));
    }

    /// <summary>
    /// Tests for exceptional conditions when the string constructor is provided null or empty values.
    /// </summary>
    [TestMethod]
    public void Constructor_String_Assertions_Throws()
    {
        string value = null;
        Assert.ThrowsException<ArgumentNullException>(() => new KbString(value));
    }

    /// <summary>
    /// Tests for successful instance creationg using the string constructor.
    /// </summary>
    [TestMethod]
    public void Constructor_Chars_Succeeds()
    {
        Assert.IsNotNull(new KbString(new char[] { }));
        Assert.IsNotNull(new KbString(new char[] {'b'}));
        Assert.IsNotNull(new KbString(new char[] { 'b', 'e', 'n', 'g', 'a', 'l' }));
        Assert.IsNotNull(new KbString(new char[] { 'b', 'e', 'n', 'g', 'a', 'l', ' ', 'c', 'a', 't' }));
    }

    /// <summary>
    /// Tests for exceptional conditions when the string constructor is provided null or empty values.
    /// </summary>
    [TestMethod]
    public void Constructor_CharArray_Assertions_Throws()
    {

        char[] chars = null;
        Assert.ThrowsException<ArgumentNullException>(() => new KbString(chars));
    }

    /// <summary>
    /// Tests for the contains method implementation where a match is found.
    /// </summary>
    [TestMethod]
    [DataRow("","")]
    [DataRow("cat","")]
    [DataRow("cat","c")]
    [DataRow("cat","ca")]
    [DataRow("cat","cat")]
    [DataRow("catch","ca")]
    [DataRow("catch","ch")]
    [DataRow("catcher","cher")]
    [DataRow("catcher","tch")]
    [DataRow("bengal cat","cat")]
    [DataRow("bengal cat","l c")]
    [DataRow("lucky bengal cat","l c")]
    public void Contains_Match_Succeeds(string source, string target)
    {
        KbString kbSource = new KbString(source);
        KbString kbTarget= new KbString(target);
        Assert.IsTrue(kbSource.Contains(kbTarget));
    }

    /// <summary>
    /// Tests for the contains method implementation where a match is not found.
    /// </summary>
    [TestMethod]
    [DataRow("","b")]
    [DataRow("cat","b")]
    [DataRow("cat","bat")]
    [DataRow("cat","ate")]
    [DataRow("cat","cater")]
    [DataRow("chat","ouch")]
    public void Contains_NoMatch_Succeeds(string source, string target)
    {
        KbString kbSource = new KbString(source);
        KbString kbTarget= new KbString(target);
        Assert.IsFalse(kbSource.Contains(kbTarget));
    }

    /// <summary>
    /// Tests for exceptional condition when target is null or empty.
    /// </summary>
    [TestMethod]
    [DataRow("value",null)]
    public void Contains_Assertions_Throws(string source, string target)
    {
        KbString kbSource = new KbString(source);
        KbString kbTarget = target == null ? null : new KbString(target);
        Assert.ThrowsException<ArgumentNullException>(() => kbSource.Contains(kbTarget));
    }

    /// <summary>
    /// Tests for the StartsWith method implementation where a match is found.
    /// </summary>
    [TestMethod]
    [DataRow("cat","")]
    [DataRow("cat","c")]
    [DataRow("cat","ca")]
    [DataRow("cat","cat")]
    [DataRow(" catch"," ca")]
    [DataRow("lucky bengal cat","lucky bengal")]
    public void StartsWith_Match_Succeeds(string source, string target)
    {
        KbString kbSource = new KbString(source);
        KbString kbTarget= new KbString(target);
        Assert.IsTrue(kbSource.StartsWith(kbTarget));
    }

    /// <summary>
    /// Tests for the StartsWith method implementation where a match is not found.
    /// </summary>
    [TestMethod]
    [DataRow("cat","b")]
    [DataRow("cat","bat")]
    [DataRow("cat","ate")]
    [DataRow("cat","cater")]
    [DataRow("chat","ouch")]
    [DataRow(" chat","ouch")]
    [DataRow("chat"," ouch")]
    public void StartsWith_NoMatch_Succeeds(string source, string target)
    {
        KbString kbSource = new KbString(source);
        KbString kbTarget= new KbString(target);
        Assert.IsFalse(kbSource.StartsWith(kbTarget));
    }

    /// <summary>
    /// Tests for exceptional condition when target is null or empty.
    /// </summary>
    [TestMethod]
    [DataRow("value",null)]
    public void StartsWith_Null_Throw(string source, string target)
    {
        KbString kbSource = new KbString(source);
        KbString kbTarget = target == null ? null : new KbString(target);
        Assert.ThrowsException<ArgumentNullException>(() => kbSource.StartsWith(kbTarget));
    }

    /// <summary>
    /// Tests for the Substring method implementation where a substring is successfully returned.
    /// </summary>
    [TestMethod]
    [DataRow("cat", 0, 1, "c")]
    [DataRow("cat", 0, 2, "ca")]
    [DataRow("cat", 0, 3, "cat")]
    [DataRow("cat", 2, 1, "t")]
    [DataRow("cat", 1, 2, "at")]
    [DataRow("catcher", 1, 6, "atcher")]
    public void Substring_Succeeds(string source, int start, int length, string result)
    {
        KbString kbSource = new KbString(source);
        KbString kbResult = new KbString(result);
        Assert.AreEqual(kbResult, kbSource.Substring(start, length));
    }

    /// <summary>
    /// Tests for exceptional condition when substring is invoked with invalid parameters.
    /// </summary>
    [TestMethod]
    [DataRow("cat",  4,  1, "c")]    // Start exceeds length
    [DataRow("cat",  0,  4, "ca")]   // Length exceeds source length 
    [DataRow("cat", -1,  3, "cat")]  // Negative start index
    [DataRow("cat",  2, -1, "t")]    // Negative length
    [DataRow("cat",  3,  2, "at")]   // Start + Length exceeds source length
    [DataRow("cat",  0,  0, "")]     // Length is 0 
    public void Substring_InvalidParameters_Throw(string source, int start, int length, string result)
    {
        KbString kbSource = new KbString(source);
        Assert.ThrowsException<ArgumentException>(() => kbSource.Substring(start, length));
    }

    /// <summary>
    /// Tests for the split method implementation where an array of tokens is successfully returned.
    /// </summary>
    [TestMethod]
    [DataRow("bat", ",")]
    [DataRow(",bat", ",")]
    [DataRow("bat,", ",")]
    [DataRow("bat,bat", ",")]
    [DataRow("bat,,bat", ",")]
    [DataRow(",bat,,bat,", ",")]
    [DataRow("bat,bat,", ",")]
    [DataRow(",bat,bat,", ",")]
    [DataRow("bat,cat,hat,rat", ",")]
    [DataRow("bat,cat,hat,rat,,", ",")]
    [DataRow("bat||cat||hat||rat", "||")]
    [DataRow("||bat||cat||hat||rat", "||")]
    [DataRow("||bat||cat||hat||rat||", "||")]
    [DataRow("||bat||||cat||||hat||||rat||", "||")]
    public void Split_Succeeds(string source, string delimiter)
    {
        KbString kbSource = new KbString(source);
        KbString kbDelimiter = new KbString(delimiter);

        KbString[] tokens = kbSource.Split(kbDelimiter);
        string[] expectedTokens = source.Split(delimiter);
        Assert.AreEqual(expectedTokens.Length, tokens.Length);

        for (int tokenIndex = 0; tokenIndex < tokens.Length; tokenIndex++)
        {
            Assert.AreEqual(expectedTokens[tokenIndex], tokens[tokenIndex].ToString());
        }
    }

    /// <summary>
    /// Tests for the split method implementation where an exceptional condition is encountered.
    /// </summary>
    [TestMethod]
    [DataRow("bat", "")]
    [DataRow("bat", null)]
    public void Split_InvalidParameters_Throw(string source, string delimiter)
    {
        KbString kbSource = new KbString(source);
        KbString kbDelimiter = new KbString(delimiter);
        Assert.ThrowsException<ArgumentException>(() => kbSource.Split(kbDelimiter));
    }

    /// <summary>
    /// Tests for the indexOf method implementation where the index is successfully returned.
    /// </summary>
    [TestMethod]
    [DataRow("bat", "", 0, 0)]
    [DataRow("bat", "", 1, 1)]
    [DataRow("bat", "b", 0, 0)]
    [DataRow("bat", "a", 0, 1)]
    [DataRow("bat", "t", 0, 2)]
    [DataRow("bat", "x", 0, -1)]
    [DataRow("battalion", "a", 0, 1)]
    [DataRow("aaaaaa", "a", 0, 0)]
    [DataRow("aaaaab", "b", 0, 5)]
    [DataRow("aaaaa", "a", 0, 0)]
    [DataRow("aaabbaaa", "aaa", 0, 0)]
    [DataRow("bbaaabbaaa", "aaa", 0, 2)]
    [DataRow("aaaaa", "a", 1, 1)]
    [DataRow("aaaaa", "a", 2, 2)]
    [DataRow("aaaaa", "a", 3, 3)]
    [DataRow("aaaaa", "a", 4, 4)]
    [DataRow("ababa", "a", 0, 0)]
    [DataRow("ababa", "a", 1, 2)]
    [DataRow("ababa", "a", 2, 2)]
    [DataRow("ababa", "a", 3, 4)]
    [DataRow("abbb", "a", 1, -1)]
    [DataRow("abbaaa", "aaa", 3, 3)]
    [DataRow("abba", "aaa", 3, -1)]
    [DataRow("bbaaabbaaa", "aaa", 3, 7)]
    public void IndexOf_Succeeds(string source, string token, int startIndex, int expectedIndex)
    {
        KbString kbSource = new KbString(source);
        KbString kbToken = new KbString(token);
        Assert.AreEqual(expectedIndex, kbSource.IndexOf(kbToken, startIndex));
    }

    /// <summary>
    /// Tests for the indexOf method implementation where an exceptional condition is encountered.
    /// </summary>
    [TestMethod]
    [DataRow("bat", null, -1)]
    public void IndexOf_Throw(string source, string token, int expectedIndex)
    {
        KbString kbSource = new KbString(source);
        KbString kbToken = token == null ? null : new KbString(token);
        Assert.ThrowsException<ArgumentNullException>(() => kbSource.IndexOf(kbToken, 0));
    }

    /// <summary>
    /// Tests for the ReplaceFirst method where a replacement occurrs successfully.
    /// </summary>
    [TestMethod]
    [DataRow("a", "a", "b", "b")]
    [DataRow("aa", "a", "b", "ba")]
    [DataRow("ab", "b", "a", "aa")]
    [DataRow("a", "a", "cc", "cc")]
    [DataRow("ab", "a", "cc", "ccb")]
    [DataRow("ab", "b", "cc", "acc")]
    [DataRow("aaabbb", "aaa", "", "bbb")]
    [DataRow("aaabbb", "aaa", "b", "bbbb")]
    [DataRow("aaabbbccc", "bbbccc", "de", "aaade")]
    [DataRow("My cat is a bengal cat.", "cat", "feline", "My feline is a bengal cat.")]
    [DataRow("My cat is a bengal cat.", ".", "!", "My cat is a bengal cat!")]
    [DataRow("My cat is a bengal cat.", "My", "Our", "Our cat is a bengal cat.")]
    [DataRow("My cat is a bengal cat.", "My cat", "My life", "My life is a bengal cat.")]
    public void ReplaceFirst_Succeeds(string source, string searchToken, string replaceToken, string expectedResult)
    {
        KbString kbSource = new KbString(source);
        KbString kbExpectedResult = new KbString(expectedResult);
        KbString kbSearchToken= new KbString(searchToken);
        KbString kbReplaceToken= new KbString(replaceToken);

        Assert.AreEqual(kbExpectedResult, kbSource.ReplaceFirst(kbSearchToken, kbReplaceToken));
    }

    /// <summary>
    /// Tests for the ReplaceFirst method implementation where an exceptional condition is encountered.
    /// </summary>
    [TestMethod]
    [DataRow("a", null, null, "")]
    public void ReplaceFirst_Throw(string source, string searchToken, string replaceToken, string expectedResult)
    {
        KbString kbSource = new KbString(source);
        KbString kbSearchToken = searchToken == null ? null : new KbString(searchToken);
        KbString kbReplaceToken = replaceToken == null ? null : new KbString(replaceToken);

        Assert.ThrowsException<ArgumentNullException>(() => kbSource.ReplaceFirst(kbSearchToken, kbReplaceToken));
    }

    /// <summary>
    /// Tests for the indexOf method implementation where the index is successfully returned.
    /// </summary>
    [TestMethod]
    [DataRow("bat", "", 2, 2)]
    [DataRow("bat", "", 1, 1)]
    [DataRow("bat", "b", 2, 0)]
    [DataRow("bat", "a", 2, 1)]
    [DataRow("bat", "t", 2, 2)]
    [DataRow("bat", "x", 2, -1)]
    [DataRow("aaaaa", "a", 0, 0)]
    [DataRow("aaaaa", "a", 1, 1)]
    [DataRow("aaaaa", "a", 2, 2)]
    [DataRow("aaaaa", "a", 3, 3)]
    [DataRow("aaaaa", "a", 4, 4)]
    [DataRow("ababa", "a", 0, 0)]
    [DataRow("ababa", "a", 3, 2)]
    [DataRow("ababa", "a", 2, 2)]
    [DataRow("ababa", "a", 3, 2)]
    [DataRow("battalion", "a", 8, 4)]
    [DataRow("aaaaab", "b", 5, 5)]
    [DataRow("aaabbaaa", "aaa", 5, 5)]
    [DataRow("bbaaabbaaa", "aaa", 6, 2)]
    [DataRow("abbb", "a", 3, 0)]
    [DataRow("bbba", "a", 3, 3)]
    [DataRow("abbaaa", "aaa", 0, -1)]
    [DataRow("abba", "aaa", 0, -1)]
    [DataRow("bbaaabbaaa", "aaa", 7, 7)]
    public void LastIndexOf_Succeeds(string source, string token, int startIndex, int expectedIndex)
    {
        KbString kbSource = new KbString(source);
        Assert.AreEqual(expectedIndex, kbSource.LastIndexOf(token, startIndex));
    }

    /// <summary>
    /// Tests for the indexOf method implementation where an exceptional condition is encountered.
    /// </summary>
    [TestMethod]
    [DataRow("bat", null, -1)]
    public void LastIndexOf_Throw(string source, string token, int expectedIndex)
    {
        KbString kbSource = new KbString(source);
        Assert.ThrowsException<ArgumentNullException>(() => kbSource.LastIndexOf(token, 0));
    }

    /// <summary>
    /// Tests for the Replace method where a replacement occurrs successfully.
    /// </summary>
    [TestMethod]
    [DataRow("a", "a", "b", "b")]
    [DataRow("aa", "a", "b", "bb")]
    [DataRow("ab", "b", "a", "aa")]
    [DataRow("a", "a", "cc", "cc")]
    [DataRow("ab", "a", "cc", "ccb")]
    [DataRow("ab", "b", "cc", "acc")]
    [DataRow("aabbcc", "bb", "dd", "aaddcc")]
    [DataRow("aaabbb", "aaa", "", "bbb")]
    [DataRow("aaabbb", "aaa", "b", "bbbb")]
    [DataRow("aaabbbccc", "bbbccc", "de", "aaade")]
    [DataRow("My cat is a bengal cat.", "cat", "feline", "My feline is a bengal feline.")]
    [DataRow("My cat is a bengal cat.", ".", "!", "My cat is a bengal cat!")]
    [DataRow("My cat is a bengal cat.", "My", "Our", "Our cat is a bengal cat.")]
    [DataRow("My cat is a bengal cat.", "My cat", "My life", "My life is a bengal cat.")]
    [DataRow("My cat is a bengal cat.", " ", "|", "My|cat|is|a|bengal|cat.")]
    [DataRow("My |cat |is |a |bengal |cat.", " |", "||", "My||cat||is||a||bengal||cat.")]
    [DataRow("My |cat |is |a |bengal |cat.", " |", " ||", "My ||cat ||is ||a ||bengal ||cat.")]
    public void Replace_Succeeds(string source, string searchToken, string replaceToken, string expectedResult)
    {
        KbString kbSource = new KbString(source);
        KbString kbSearchToken= new KbString(searchToken);
        KbString kbReplaceToken= new KbString(replaceToken);
        KbString kbExpectedResult = new KbString(expectedResult);

        Assert.AreEqual(kbExpectedResult, kbSource.Replace(kbSearchToken, kbReplaceToken));
    }

    /// <summary>
    /// Tests for the ReplaceFirst method implementation where an exceptional condition is encountered.
    /// </summary>
    [TestMethod]
    [DataRow("a", null, null, "")]
    public void Replace_Throw(string source, string searchToken, string replaceToken, string expectedResult)
    {
        KbString kbSource = new KbString(source);
        KbString kbSearchToken = searchToken == null ? null : new KbString(searchToken);
        KbString kbReplaceToken = replaceToken == null ? null : new KbString(replaceToken);

        Assert.ThrowsException<ArgumentNullException>(() => kbSource.Replace(kbSearchToken, kbReplaceToken));
    }

    /// <summary>
    /// Tests for the equals method implementation.
    /// </summary>
    [DataRow("a", null, false)]
    [DataRow("a", "a", true)]
    [DataRow("a", "b", false)]
    [DataRow("aa", "aa", true)]
    [DataRow("aa", "bb", false)]
    [DataRow("aaa", "aaa", true)]
    [DataRow("aba", "aba", true)]
    [DataRow("aba", "aca", false)]
    [DataRow("baa", "baa", true)]
    [DataRow("baa", "baa", true)]
    [DataRow("abc", "abc", true)]
    [DataRow("abc", "abb", false)]
    [DataRow("abc", "", false)]
    [TestMethod]
    public void Equals_Succeeds(string source, string comparison, bool result)
    {
        KbString kbSource = new KbString(source);

        KbString kbComparison = comparison is null ? null : new KbString(comparison);

        Assert.IsTrue(kbSource.Equals(kbComparison) == result);
    }

    /// <summary>
    /// Tests for the equals operator overload.
    /// </summary>
    [DataRow("a", null, false)]
    [DataRow("a", "a", true)]
    [DataRow("a", "b", false)]
    [DataRow("aa", "aa", true)]
    [DataRow("aa", "bb", false)]
    [DataRow("aaa", "aaa", true)]
    [DataRow("aba", "aba", true)]
    [DataRow("aba", "aca", false)]
    [DataRow("baa", "baa", true)]
    [DataRow("baa", "baa", true)]
    [DataRow("abc", "abc", true)]
    [DataRow("abc", "abb", false)]
    [DataRow("abc", "", false)]
    [TestMethod]
    public void EqualsOperator_Succeeds(string source, string comparison, bool result)
    {
        KbString kbSource = new KbString(source);

        KbString kbComparison = comparison is null ? null : new KbString(comparison);

        Assert.IsTrue((kbSource == kbComparison) == result);
    }

    /// <summary>
    /// Tests for the not equals operator overload.
    /// </summary>
    [DataRow("a", null, false)]
    [DataRow("a", "a", true)]
    [DataRow("a", "b", false)]
    [DataRow("aa", "aa", true)]
    [DataRow("aa", "bb", false)]
    [DataRow("aaa", "aaa", true)]
    [DataRow("aba", "aba", true)]
    [DataRow("aba", "aca", false)]
    [DataRow("baa", "baa", true)]
    [DataRow("baa", "baa", true)]
    [DataRow("abc", "abc", true)]
    [DataRow("abc", "abb", false)]
    [DataRow("abc", "", false)]
    [TestMethod]
    public void NotEqualsOperator_Succeeds(string source, string comparison, bool result)
    {
        KbString kbSource = new KbString(source);

        KbString kbComparison = comparison is null ? null : new KbString(comparison);

        Assert.IsTrue((kbSource != kbComparison) == !result);
    }

    /// <summary>
    /// Tests for the equals method implementation.
    /// </summary>
    [TestMethod]
    [DataRow("a", "a")]
    [DataRow("a", "b")]
    [DataRow("aa", "aa")]
    [DataRow("aa", "bb")]
    [DataRow("aaa", "aaa")]
    [DataRow("aba", "aba")]
    [DataRow("aba", "aca")]
    [DataRow("baa", "baa")]
    [DataRow("abc", "abc")]
    [DataRow("abc", "abb")]
    [DataRow("abc", "")]
    public void AdditionOperator_Succeeds(string source, string source2)
    {
        KbString kbSource = new KbString(source);
        KbString kbSource2 = new KbString(source2);

        Assert.AreEqual(new KbString(source + source2), kbSource + kbSource2);
    }

    [TestMethod]
    [DataRow("", "")]
    [DataRow("a", "a")]
    [DataRow("a", "b")]
    [DataRow("a", "b")]
    [DataRow("aa", "ab")]
    [DataRow("aa", "aab")]
    [DataRow("aab", "aa")]
    [DataRow("aab ", "aa")]
    [DataRow("aab", "aa ")]
    [DataRow("aab", "aa ")]
    public void Compare_Succeeds(string source, string comparison)
    {
        KbString kbSource = new KbString(source);
        KbString kbComparison = new KbString(comparison);

        Assert.AreEqual(
            String.Compare(source, comparison),
            KbString.Compare(kbSource, kbComparison));
    }

    /// <summary>
    /// Tests for the Compare method implementation where an exceptional condition is encountered.
    /// </summary>
    [TestMethod]
    [DataRow(null, "")]
    [DataRow("", null)]
    public void Compare_Throw(string source, string comparison)
    {
        KbString kbSource = source == null ? null : new KbString(source);
        KbString kbComparison = source == null ? null : new KbString(comparison);

        Assert.ThrowsException<ArgumentNullException>(() => KbString.Compare(kbSource, kbComparison));
    }

    [TestMethod]
    [DataRow("",   0,  "a", "a")]
    [DataRow("b",  0,  "a", "ab")]
    [DataRow("b",  0,  "aa", "aab")]
    [DataRow("a",  1,  "b", "ab")]
    [DataRow("ac", 1,  "b", "abc")]
    [DataRow("aa",  1, "bb", "abba")]
    [DataRow("ab",  2, "c", "abc")]
    [DataRow("ab",  2, "cc", "abcc")]
    public void Insert_Succeeds(string source, int position, string insertionSource, string expectedResult)
    {
        KbString kbSource = new KbString(source);
        KbString kbInsertionSource = new KbString(insertionSource);
        KbString kbExpectedResult = new KbString(expectedResult);

        Assert.AreEqual(kbExpectedResult, kbSource.Insert(position, kbInsertionSource));
    }

    [TestMethod]
    [DataRow("", 0, null, "a")]
    public void Insert_NullReference_Throw(string source, int position, string insertionSource, string expectedResult)
    {
        KbString kbSource = new KbString(source);
        KbString kbInsertionSource = insertionSource == null ? null : new KbString(insertionSource);
        KbString kbExpectedResult = new KbString(expectedResult);

        Assert.ThrowsException<ArgumentNullException>(() => kbSource.Insert(position, kbInsertionSource));
    }

    [TestMethod]
    [DataRow("", -1, "a", "a")]
    public void Insert_IndexOutofBounds_Throw(string source, int position, string insertionSource, string expectedResult)
    {
        KbString kbSource = new KbString(source);
        KbString kbInsertionSource = insertionSource == null ? null : new KbString(insertionSource);
        KbString kbExpectedResult = new KbString(expectedResult);

        Assert.ThrowsException<IndexOutOfRangeException>(() => kbSource.Insert(position, kbInsertionSource));
    }
}
