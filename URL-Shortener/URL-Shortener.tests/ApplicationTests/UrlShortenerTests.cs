using Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URL_Shortener.tests.ApplicationTests;

public class UrlShortenerTests
{
    private readonly UrlShortenerHelper _urlHelper;

    public UrlShortenerTests()
    {
        _urlHelper = new UrlShortenerHelper();
    }

    [Fact]
    public void GenerateSurl_GenerateSurlGivenLength_ShouldReturnCorrectLength()
    {
        int length = 6;

        var generatedString = _urlHelper.GenerateSurl(length);

        Assert.Equal(length, generatedString.Length);
    }

    [Fact]
    public void GenerateSurl_GenerateSurlGivenCharacters_ShouldReturnCorrectChars()
    {
        int length = 6;
        string validCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";

        var generatedString = _urlHelper.GenerateSurl(length);

        foreach (char c in generatedString)
        {
            Assert.Contains(c, validCharacters);
        }
    }
}
