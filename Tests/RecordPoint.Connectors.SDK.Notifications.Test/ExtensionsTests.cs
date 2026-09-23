#nullable enable
using RecordPoint.Connectors.SDK.Notifications.Handlers;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace RecordPoint.Connectors.SDK.Notifications.Test;

public class ExtensionsTests
{
    private sealed class Sample
    {
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }
    }

    private static object ToElement(string json) => JsonDocument.Parse(json).RootElement;

    [Fact]
    public void ContextToObject_DeserializesObject()
    {
        var element = ToElement("{\"Name\":\"abc\",\"Value\":42}");

        var result = element.ContextToObject<Sample>();

        Assert.NotNull(result);
        Assert.Equal("abc", result!.Name);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void ContextToObject_Throws_WhenNotAnObject()
    {
        var element = ToElement("[1,2,3]");

        var ex = Assert.Throws<ArgumentException>(() => element.ContextToObject<Sample>());
        Assert.Contains("not an object", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ContextToList_DeserializesArray()
    {
        var element = ToElement("[{\"Name\":\"a\",\"Value\":1},{\"Name\":\"b\",\"Value\":2}]");

        var result = element.ContextToList<Sample>();

        Assert.Equal(2, result.Count);
        Assert.Equal("a", result[0].Name);
        Assert.Equal("b", result[1].Name);
    }

    [Fact]
    public void ContextToList_SkipsNullElements()
    {
        var element = ToElement("[{\"Name\":\"a\",\"Value\":1},null]");

        var result = element.ContextToList<Sample>();

        Assert.Single(result);
        Assert.Equal("a", result[0].Name);
    }

    [Fact]
    public void ContextToList_Throws_WhenNotAnArray()
    {
        var element = ToElement("{\"Name\":\"a\"}");

        var ex = Assert.Throws<ArgumentException>(() => element.ContextToList<Sample>());
        Assert.Contains("not an array", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ContextToList_ReturnsEmpty_ForEmptyArray()
    {
        var element = ToElement("[]");

        var result = element.ContextToList<Sample>();

        Assert.Empty(result);
    }
}
