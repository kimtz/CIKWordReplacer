using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Raven.Client;
using Raven.Client.Document;

namespace CIK.WordReplacer.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var storage = new RavenDbStorage();
            var wordReplacer = new WordReplacer(storage);
            // Let the user define which words to replace with what fron the console
            // Save these in a file, IllegalWord AcceptedWord
            // Read the file with words to replace
            System.Console.WriteLine("What would you like to do?");
            System.Console.WriteLine("To add a new illegal word, press 1.");
            System.Console.WriteLine("To replace words in a text, press 2.");
            System.Console.WriteLine("To delete a illegal word and it's replacement, press 3.");
          // System.Console.WriteLine("To change a illegal word and/or it's replacement, press 4.");
            var action = System.Console.ReadLine();

            switch (action)
            {
                case "1":
                    System.Console.WriteLine("Which word would you like to replace? Please type it below.");
                    var illegalWordToSave = System.Console.ReadLine();
                    System.Console.WriteLine("What word would you like to replce it with? Please type it below.");
                    var validWordToSave = System.Console.ReadLine();

                    var replacementPairToSave = new ReplacementPair();
                    replacementPairToSave.illegalWord = illegalWordToSave;
                    replacementPairToSave.validWord = validWordToSave;

                    using (var session = storage.documentStore.OpenSession())
                    {
                        session.Store(replacementPairToSave);
                        session.SaveChanges();
                    }
                    break;
                case "2":
                    System.Console.WriteLine("Please add the path to the file containing the text:");
                    var textPath = System.Console.ReadLine();
                    var rules = storage.GetRules();
                    var result = wordReplacer.ReplaceWordsInText(textPath, rules);
                    System.Console.WriteLine("Please copy the result below.");
                    System.Console.WriteLine(result);
                    System.Console.ReadKey();
                    break;
                case "3":
                    System.Console.WriteLine("Which illegal word would you like to delete? Please type the id below.");
                    System.Console.WriteLine("Please note that the deletion of an illegal word also deletes it's replacement.");
                    var illegalWordToDelete = System.Console.ReadLine();
                    using (var session = storage.documentStore.OpenSession())
                    {
                        var replacementPairToDelete = session.Load<ReplacementPair>(illegalWordToDelete);
                        session.Delete(replacementPairToDelete);
                        session.SaveChanges();
                    }
                    break;
            }
            
        }

    }
}

//System.Console.WriteLine("Which illegal word would you like to delete? Please type it below.");
//System.Console.WriteLine("Please note that the deletion of an illegal word also deletes it's replacement.");
//var illegalWordToDelete = System.Console.ReadLine();
//using (var session = wordReplacer.documentStore.OpenSession())
//{
//   var replacementPairToDelete =
//        session.Query<ReplacementPair>().Where(x => x.illegalWord == illegalWordToDelete);
//    var rp = session.Load<ReplacementPair>(replacementPairToDelete.First());
//    session.Delete(replacementPairToDelete);
//    session.SaveChanges();
//}