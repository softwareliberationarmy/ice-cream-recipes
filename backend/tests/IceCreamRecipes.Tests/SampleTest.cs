using Xunit;

namespace IceCreamRecipes.Tests;
public class SampleTest
{
    [Fact]
    public void Test_Passes()
    {
        // Arrange
        var expected = true;

        // Act
        var actual = true;

        // Assert
        Assert.Equal(expected, actual);
    }
}
