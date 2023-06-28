using Newtonsoft.Json;
using NUnit.Framework;
using ReflectionTask;
using ReflectionTaskLibrary;
using System;
using System.IO;

namespace Tests
{
    public class CustomConverterTests
    {
        private readonly string _testDataFolder = $"{AppDomain.CurrentDomain.BaseDirectory}\\TestData\\";

        [TestCase(2, "2")]
        [TestCase(5.52, "5,52")]
        [TestCase(001.01, "1,01")]
        [TestCase("string_value", "string_value")]
        [TestCase(DayOfWeek.Friday, "Friday")]
        public void SimpleInput_Serialize_OutputIsCorrect(object value, string expected)
        {
            // Precondition
            var converter = new CustomConverter();

            // Act
            var actual = converter.Serialize(value);

            // Assert
             Assert.AreEqual(expected, actual);
        }

        [TestCase("tc1", "FullFilled")]
        [TestCase("tc2", "FullFilledWithoutNotSerializableProperties")]
        [TestCase("tc3", "ConfigWithNullValues")]
        [TestCase("tc4", "EmptyConfig")]
        public void ModelInput_Serialize_OutputIsCorrect(string testCaseName, string errorMessage)
        {
            // Precondition
            var converter = new CustomConverter();

            Config input;
            string expected;

            using (var sr = new StreamReader($"{_testDataFolder}{testCaseName}_in.json"))
            {
                input = JsonConvert.DeserializeObject<Config>(sr.ReadToEnd());
            }

            using (var sr = new StreamReader($"{_testDataFolder}{testCaseName}_out.txt"))
            {
                expected = sr.ReadToEnd();
            }

            // Act
            var actual = converter.Serialize(input);

            // Assert
            Assert.AreEqual(expected, actual, errorMessage);
        }
    }
}