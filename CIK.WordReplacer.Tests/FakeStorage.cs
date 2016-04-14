using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIK.WordReplacer.Tests
{
    public class FakeStorage : IStorage
    {
        public IEnumerable<ReplacementPair> GetRules()
        {
            var rules = new List<ReplacementPair>();
            var ruleOne = new ReplacementPair();
            ruleOne.illegalWord = "Företaget";
            ruleOne.validWord = "Forefront";
            rules.Add(ruleOne);
            var ruleTwo = new ReplacementPair();
            ruleTwo.illegalWord = "applikationerna";
            ruleTwo.validWord = "systemen";
            rules.Add(ruleTwo);
            var ruleThree = new ReplacementPair();
            ruleThree.illegalWord = "annorlunda";
            ruleThree.validWord = "tvärtom";
            rules.Add(ruleThree);
            var ruleFour = new ReplacementPair();
            ruleFour.illegalWord = "bra";
            ruleFour.validWord = "flexibla";
            rules.Add(ruleFour);
            var ruleFive = new ReplacementPair();
            ruleFive.illegalWord = "offshore";
            ruleFive.validWord = "Frontshore";
            rules.Add(ruleFive);
            var ruleSix = new ReplacementPair();
            ruleSix.illegalWord = "China";
            ruleSix.validWord = "Kina";
            rules.Add(ruleSix);
            return rules;
        }
    }
}
