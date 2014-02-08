
namespace Nancy.Bandit.Tests
{
    using System;
    using Xunit;

    public class DescriptiveTextTests
    {
        [Fact]
        public void DescriptiveText_WithValidConstructor_ReturnsValidValue()
        {
            const string data = "This is a description.";

            DescriptiveText text = new DescriptiveText(data);

            Assert.Equal(text, data);
        }

        [Fact]
        public void DescriptiveText_WithInRangeExplicitCast_ReturnsName()
        {
            string data = "".PadLeft(256, 'a');

            DescriptiveText text = (DescriptiveText)data;

            Assert.Equal(text, data);
        }

        [Fact]
        public void DescriptiveText_WithOverRangeExplicitCast_Truncates()
        {
            string data = "".PadLeft(500, 'a');

            DescriptiveText text = (DescriptiveText)data;

            Assert.Equal(text, data.Substring(0, 256));
        }
    }
}
