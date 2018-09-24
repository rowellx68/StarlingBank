using System;
using StarlingBank.Utilities;
using StarlingBank.Validators;
using Xunit;

namespace StarlingBank.UnitTests.Validators
{
    public class WebhookPayloadValidatorTests
    {
        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(EmptyStringException))]
        [InlineData(" ", typeof(WhitespaceException))]
        public void Validate_NullEmptyWhitespaceSignature_ThrowsException(string value, Type exception)
        {
            // Act/Assert
            Assert.Throws(exception, () => WebhookPayloadValidator.Validate(value, "secret", "{}"));
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(EmptyStringException))]
        [InlineData(" ", typeof(WhitespaceException))]
        public void Validate_NullEmptyWhitespaceSecret_ThrowsException(string value, Type exception)
        {
            // Act/Assert
            Assert.Throws(exception, () => WebhookPayloadValidator.Validate("signature", value, "{}"));
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(EmptyStringException))]
        [InlineData(" ", typeof(WhitespaceException))]
        public void Validate_NullEmptyWhitespaceJson_ThrowsException(string value, Type exception)
        {
            // Act/Assert
            Assert.Throws(exception, () => WebhookPayloadValidator.Validate("signature", "secret", value));
        }
    }
}