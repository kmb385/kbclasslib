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
        Assert.IsTrue(kbSource.Contains(target));
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
        Assert.IsFalse(kbSource.Contains(target));
    }

    /// <summary>
    /// Tests for exceptional condition when target is null or empty.
    /// </summary>
    [TestMethod]
    [DataRow("value",null)]
    public void Contains_Assertions_Throws(string source, string target)
    {
        KbString kbSource = new KbString(source);
        Assert.ThrowsException<ArgumentNullException>(() => kbSource.Contains(target));
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
        Assert.IsTrue(kbSource.StartsWith(target));
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
        Assert.IsFalse(kbSource.StartsWith(target));
    }

    /// <summary>
    /// Tests for exceptional condition when target is null or empty.
    /// </summary>
    [TestMethod]
    [DataRow("value",null)]
    public void StartsWith_Null_Throw(string source, string target)
    {
        KbString kbSource = new KbString(source);
        Assert.ThrowsException<ArgumentNullException>(() => kbSource.StartsWith(target));
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
        Assert.AreEqual(result, kbSource.Substring(start, length));
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
        string[] tokens = kbSource.Split(delimiter);
        string[] expectedTokens = source.Split(delimiter);
        Assert.AreEqual(expectedTokens.Length, tokens.Length);

        for (int tokenIndex = 0; tokenIndex < tokens.Length; tokenIndex++)
        {
            Assert.AreEqual(expectedTokens[tokenIndex], tokens[tokenIndex]);
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
        Assert.ThrowsException<ArgumentException>(() => kbSource.Split(delimiter));
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
        Assert.AreEqual(expectedIndex, kbSource.IndexOf(token, startIndex));
    }

    /// <summary>
    /// Tests for the indexOf method implementation where an exceptional condition is encountered.
    /// </summary>
    [TestMethod]
    [DataRow("bat", null, -1)]
    public void IndexOf_Throw(string source, string token, int expectedIndex)
    {
        KbString kbSource = new KbString(source);
        Assert.ThrowsException<ArgumentNullException>(() => kbSource.IndexOf(token, 0));
    }

}
