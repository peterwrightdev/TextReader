using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using TextReader;

namespace TextReaderTester
{
    [TestClass]
    public class FileReaderTester
    {
        [TestMethod]
        public void WhenProgramReadsFile_ThenCorrectWordCountReturned()
        {
            // Arrange
            // Act
            List<KeyValuePair<string, int>> orderdWordCount = TextReader.Program.GetOrderedWordCount("LotRAP.txt");

            // Assert
            // Verify in a vacuum that all values are ordered based on word count descending.
            int count = 1;
            foreach (KeyValuePair<string, int> orderedKeyValue in orderdWordCount)
            {
                if (count < orderdWordCount.Count)
                {
                    Assert.IsTrue(orderedKeyValue.Value >= orderdWordCount[count].Value);
                    count++;
                }
            }

            // As we have this specific file, we can verify this example explicitly.
            // Verified with notepad++ - Mark, Match whole word only.
            Assert.AreEqual("the", orderdWordCount[0].Key);
            Assert.AreEqual("of", orderdWordCount[1].Key);
            Assert.AreEqual("and", orderdWordCount[2].Key);
            Assert.AreEqual("to", orderdWordCount[3].Key);
            Assert.AreEqual("in", orderdWordCount[4].Key);
            Assert.AreEqual("a", orderdWordCount[5].Key);
            Assert.AreEqual("that", orderdWordCount[6].Key);
            Assert.AreEqual("his", orderdWordCount[7].Key);
            Assert.AreEqual("is", orderdWordCount[8].Key);
            Assert.AreEqual("as", orderdWordCount[9].Key);
            Assert.AreEqual(4225, orderdWordCount[0].Value);
            Assert.AreEqual(2278, orderdWordCount[1].Value);
            Assert.AreEqual(1387, orderdWordCount[2].Value);
            Assert.AreEqual(1130, orderdWordCount[3].Value);
            Assert.AreEqual(885, orderdWordCount[4].Value);
            Assert.AreEqual(801, orderdWordCount[5].Value);
            Assert.AreEqual(525, orderdWordCount[6].Value);
            Assert.AreEqual(461, orderdWordCount[7].Value);
            Assert.AreEqual(429, orderdWordCount[8].Value);
            Assert.AreEqual(427, orderdWordCount[9].Value);
        }

        [TestMethod]
        public void SafeDictionaryCorrectlyIncrementsValuesForGivenKey()
        {
            SafeDictionary<string> testDictionary = new SafeDictionary<string>();

            Assert.IsTrue(testDictionary.Count == 0);

            testDictionary.SafeIncrement("string");

            Assert.IsTrue(testDictionary.Count == 1);
            Assert.AreEqual(1, testDictionary["string"]);

            testDictionary.SafeIncrement("string");

            Assert.IsTrue(testDictionary.Count == 1);
            Assert.AreEqual(2, testDictionary["string"]);

            testDictionary.SafeIncrement("string2");

            Assert.IsTrue(testDictionary.Count == 2);
            Assert.AreEqual(2, testDictionary["string"]);
            Assert.AreEqual(1, testDictionary["string2"]);
        }
    }
}
