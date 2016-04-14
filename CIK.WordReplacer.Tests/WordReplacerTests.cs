using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CIK.WordReplacer;

namespace CIK.WordReplacer.Tests
{
    [TestFixture]
    class WordReplacerTests
    {
        public WordReplacer wordReplacer;
        public IStorage storage;

        [SetUp]
        public void SetUp()
        {
            storage = new FakeStorage();
            wordReplacer = new WordReplacer(storage);

        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void Should_replace_illegal_word_with_valid_word()
        {
            var illegalWord = "Företaget";
            var validWord = "Forefront";
            var text = "Företaget är coolt";
            var resultText = wordReplacer.Replace(illegalWord, validWord, text);
            Assert.AreEqual("Forefront är coolt", resultText);
        }

        [Test]
        public void Should_replace_illegal_word_with_valid_word_even_if_its_part_of_word()
        {
            var illegalWord = "offshore";
            var validWord = "Frontshore";
            var text = "offshoreutveckling";
            var resultText = wordReplacer.Replace(illegalWord, validWord, text);
            Assert.AreEqual("Frontshoreutveckling", resultText);
        }

        [Test]
        public void Should_not_replace_illegal_word_with_valid_word_when_it_does_not_exist_in_text()
        {
            var illegalWord = "Företaget";
            var validWord = "Forefront";
            var text = "Alla är coola";
            var resultText = wordReplacer.Replace(illegalWord, validWord, text);
            Assert.AreEqual("Alla är coola", resultText);
        }

        [Test]
        public void Should_replace_illegal_word_in_text_with_valid_word()
        {
            var rules = storage.GetRules();
            var rule = new List<ReplacementPair>();
            rule.Add(rules.First());
            var textPath = "C:/Users/kim.tengbom/Documents/Storage/testTextWordReplacer.txt";
            var resultText = wordReplacer.ReplaceWordsInText(textPath, rule);
            Assert.AreEqual("Forefront är coolt. Dom här applikationerna är grymma.", resultText);
        }

        [Test]
        public void Should_replace_illegal_words_in_text_with_valid_words()
        {
            var rules = storage.GetRules();
            var textPath = "C:/Users/kim.tengbom/Documents/Storage/testTextWordReplacer.txt";
            var bla = rules.Take(2);
            var resultText = wordReplacer.ReplaceWordsInText(textPath, rules);
            Assert.AreEqual("Forefront är coolt. Dom här systemen är grymma.", resultText);
        }
    }
}
