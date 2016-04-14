using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Document;

namespace CIK.WordReplacer
{
    public class RavenDbStorage : IStorage
    {
        public DocumentStore documentStore;

        public RavenDbStorage()
        {
            documentStore = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "WordsToReplace"
            };
            documentStore.Initialize();
        }

        public IEnumerable<ReplacementPair> GetRules()
        {
            var rules = new List<ReplacementPair>();
            var obj = new ReplacementPair();
            var keys = documentStore.DatabaseCommands.GetDocuments(0, 1024, metadataOnly: true)
                .Select(x => x.Key)
                .ToArray();
            foreach (var key in keys)
            {
                if (key.StartsWith("ReplacementPairs"))
                {
                    using (var session = documentStore.OpenSession())
                    {
                        var result = session.Load<ReplacementPair>(key);
                        rules.Add(result);
                    }
                }
            }
            return rules;
        }
    }
}
