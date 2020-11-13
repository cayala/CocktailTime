using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Text.Json;
using CocktailTime.Controllers.DTO;
using CocktailTime.Json.Deserializers;

namespace CocktailTimeTests.Json.Deserializers
{
    public class CocktailDtoDeserializerTests
    {
        private const string JsonWithWrongProperty = "{\"wrong\" : \"value\"}";
        private const string JsonWithPhoneNumberProperty = "{\"phonenumber\" : \"+11111111\"}";
        private const string JsonWithTimeZoneProperty = "{\"timezone\" : \"PST\"}";
        private const string JsonWithTwoProperties = "{\"phonenumber\" : \"+11111111\", \"timezone\" : \"PST\"}";
        private const string JsonCorrectPhoneNumber = "+11111111";
        private const string JsonCorrectTimeZone = "PST";

        private JsonSerializerOptions SetupDeserializer() 
        {
            var deserializerOptions = new JsonSerializerOptions();
            deserializerOptions.Converters.Add(new CocktailDtoDeserializer());
            return deserializerOptions;
        }

        private CocktailDTO RunDeserializer(string json)
            => JsonSerializer.Deserialize<CocktailDTO>(json, SetupDeserializer());

        [Fact]
        public void ReturnsNewDTOWithNullProperties() 
        {
            //Assign
            //Act
            var res = RunDeserializer(JsonWithWrongProperty);

            //Assert
            Assert.NotNull(res);
            Assert.IsType<CocktailDTO>(res);
            Assert.Null(res.PhoneNumber);
            Assert.Null(res.TimeZone);
        }

        [Fact]
        public void ReturnsNewDTOWithPhoneNumberFilled()
        {
            //Assign
            //Act
            var res = RunDeserializer(JsonWithPhoneNumberProperty);

            //Assert
            Assert.NotNull(res);
            Assert.IsType<CocktailDTO>(res);
            Assert.NotNull(res.PhoneNumber);
            Assert.Null(res.TimeZone);
            Assert.Equal(JsonCorrectPhoneNumber, res.PhoneNumber);
        }

        [Fact]
        public void ReturnsNewDTOWithTimeZoneFilled() 
        {
            //Assign
            //Act
            var res = RunDeserializer(JsonWithTimeZoneProperty);

            //Assert
            Assert.NotNull(res);
            Assert.IsType<CocktailDTO>(res);
            Assert.Null(res.PhoneNumber);
            Assert.NotNull(res.TimeZone);
            Assert.Equal(JsonCorrectTimeZone, res.TimeZone);
        }

        [Fact]
        public void ReturnsNewDTOWithAllPropertiesFilled() 
        {
            //Assert
            //Act
            var res = RunDeserializer(JsonWithTwoProperties);

            //Assert
            Assert.NotNull(res);
            Assert.IsType<CocktailDTO>(res);
            Assert.NotNull(res.PhoneNumber);
            Assert.NotNull(res.TimeZone);
            Assert.Equal(JsonCorrectPhoneNumber, res.PhoneNumber);
            Assert.Equal(JsonCorrectTimeZone, res.TimeZone);
        }

        //Here so if the write function is changed
        //Then the dev can write appropiate tests for it and not forget
        [Fact]
        public void WriteThrowsNotImplemented() 
            => Assert.Throws<NotImplementedException>(() => {
                JsonSerializer.Serialize<CocktailDTO>(new CocktailDTO(null, null), SetupDeserializer());
            });
    }
}
