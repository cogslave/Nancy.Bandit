
namespace Nancy.Bandit.Tests
{
    using System;
    using Xunit;

    public class NameTests
    {
        [Fact]
        public void Name_WithValidConstructor_ReturnsValidValue()
        {
            const string testName = "Frank";

            Name name = new Name(testName);

            Assert.Equal(name, testName);
        }

        [Fact]
        public void Name_WithInRangeExplicitCast_ReturnsName()
        {
            const string testName = "QQQQQQQQQQQQQQQQQQQQQQQQQ";

            Name name = (Name)testName;

            Assert.Equal(name, testName);
        }

        [Fact]
        public void Name_WithOverRangeExplicitCast_Truncates()
        {
            const string testName = "PaaaaaaaaaaaaaaaaaaaaaaaaaP";
            
            Name name = (Name)testName;

            Assert.Equal(name, testName.Substring(0, 25));
        }

    }
}
