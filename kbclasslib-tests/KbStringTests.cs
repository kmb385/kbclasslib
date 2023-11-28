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
        Assert.IsNotNull(new KbString("b"));
        Assert.IsNotNull(new KbString("bengal"));
        Assert.IsNotNull(new KbString("bengal cat"));
    }

    /// <summary>
    /// Tests for exceptional conditions when the string constructor is provided null or empty values.
    /// </summary>
    [TestMethod]
    public void Constructor_String_Empty_Throws()
    {
        string value = null;
        Assert.ThrowsException<ArgumentException>(() => new KbString(value));

        Assert.ThrowsException<ArgumentException>(() => new KbString(String.Empty));
    }

    /// <summary>
    /// Tests for successful instance creationg using the string constructor.
    /// </summary>
    [TestMethod]
    public void Constructor_Chars_Succeeds()
    {
        Assert.IsNotNull(new KbString(new char[] {'b'}));
        Assert.IsNotNull(new KbString(new char[] { 'b', 'e', 'n', 'g', 'a', 'l' }));
        Assert.IsNotNull(new KbString(new char[] { 'b', 'e', 'n', 'g', 'a', 'l', ' ', 'c', 'a', 't' }));
    }

    /// <summary>
    /// Tests for exceptional conditions when the string constructor is provided null or empty values.
    /// </summary>
    [TestMethod]
    public void Constructor_Chars_Empty_Throws()
    {
        string value = null;
        Assert.ThrowsException<ArgumentException>(() => new KbString(new char[] {}));

        Assert.ThrowsException<ArgumentException>(() => new KbString(String.Empty));

        char[] chars = null;
        Assert.ThrowsException<ArgumentException>(() => new KbString(value));
        Assert.ThrowsException<ArgumentException>(() => new KbString(new char[] {}));
    }

    /// <summary>
    /// Tests for the contains method implementation where a match is found.
    /// </summary>
    [TestMethod]
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
        KbString kbString = new KbString(source);
        Assert.IsTrue(kbString.Contains(target));
    }

    /// <summary>
    /// Tests for the contains method implementation where a match is not found.
    /// </summary>
    [TestMethod]
    [DataRow("cat","b")]
    [DataRow("cat","bat")]
    [DataRow("cat","ate")]
    [DataRow("cat","cater")]
    [DataRow("chat","ouch")]
    public void Contains_NoMatch_Succeeds(string source, string target)
    {
        KbString kbString = new KbString(source);
        Assert.IsFalse(kbString.Contains(target));
    }

    /// <summary>
    /// Tests for exceptional condition when target is null or empty.
    /// </summary>
    [TestMethod]
    [DataRow("value","")]
    [DataRow("value",null)]
    public void Contains_EmptyAssertions_Throw(string source, string target)
    {
        KbString kbString = new KbString(source);
        Assert.ThrowsException<ArgumentException>(() => kbString.Contains(target));
    }

    /// <summary>
    /// Tests for the StartsWith method implementation where a match is found.
    /// </summary>
    [TestMethod]
    [DataRow("cat","c")]
    [DataRow("cat","ca")]
    [DataRow("cat","cat")]
    [DataRow(" catch"," ca")]
    [DataRow("lucky bengal cat","lucky bengal")]
    public void StartsWith_Match_Succeeds(string source, string target)
    {
        KbString kbString = new KbString(source);
        Assert.IsTrue(kbString.StartsWith(target));
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
        KbString kbString = new KbString(source);
        Assert.IsFalse(kbString.StartsWith(target));
    }

    /// <summary>
    /// Tests for exceptional condition when target is null or empty.
    /// </summary>
    [TestMethod]
    [DataRow("value","")]
    [DataRow("value",null)]
    public void StartsWith_EmptyAssertions_Throw(string source, string target)
    {
        KbString kbString = new KbString(source);
        Assert.ThrowsException<ArgumentException>(() => kbString.StartsWith(target));
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
        KbString kbString = new KbString(source);
        Assert.AreEqual(result, kbString.Substring(start, length));
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
        KbString kbString = new KbString(source);
        Assert.ThrowsException<ArgumentException>(() => kbString.Substring(start, length));
    }

    /// <summary>
    /// Tests for the index of method implementation where a substring is successfully returned.
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
        KbString kbString = new KbString(source);
        string[] tokens = kbString.Split(delimiter);
        string[] expectedTokens = source.Split(delimiter);
        Assert.AreEqual(expectedTokens.Length, tokens.Length);

        for (int tokenIndex = 0; tokenIndex < tokens.Length; tokenIndex++)
        {
            Assert.AreEqual(expectedTokens[tokenIndex], tokens[tokenIndex]);
        }
    }

    /// <summary>
    /// Tests for the index of method implementation where a substring is successfully returned.
    /// </summary>
    [TestMethod]
    [DataRow("bat", "")]
    [DataRow("bat", null)]
    public void Split_InvalidParameters_Throw(string source, string delimiter)
    {
        KbString kbString = new KbString(source);
        Assert.ThrowsException<ArgumentException>(() => kbString.Split(delimiter));
    }
}
