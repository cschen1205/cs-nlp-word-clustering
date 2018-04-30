using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace WordClustering
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> word_sequence = new List<string>();
            Corpus corpus = new Corpus();
            using (StreamReader reader = new StreamReader("sample.txt"))
            {
                string[] words = reader.ReadToEnd().Split(new char[] { ' ', '?', ',', ':', '"', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in words)
                {
                    string w2 = word.Trim();
                    if (w2 == ".")
                    {
                        continue;
                    }
                    if (w2.EndsWith("."))
                    {
                        w2 = w2.Substring(0, w2.Length - 1);
                    }
                    if (!string.IsNullOrEmpty(w2) && word.Length > 1)
                    {
                        word_sequence.Add(w2);
                        corpus.Add(w2);
                    }
                }
            }

            int M = 70;
            Console.WriteLine("M: {0}", M);
            Console.WriteLine("Corpus Size: {0}", corpus.Count);
            Console.WriteLine("Document Size: {0}", word_sequence.Count);

            BrownClustering bc = new BrownClustering(M);
            bc.Cluster(corpus, word_sequence);

            Dictionary<string, List<string>> clusters = bc.GetClustersWithCodewordsOfLength(10);

            foreach (string codeword in clusters.Keys)
            {
                Console.WriteLine("In Cluster {0}", codeword);
                foreach (string word in clusters[codeword])
                {
                    Console.Write("{0}, ", word);
                }
                Console.WriteLine();
            }

            XmlDocument doc = new XmlDocument();
            XmlElement root = bc.ToXml(doc);
            doc.AppendChild(root);

            doc.Save("BrownClusteringResult.xml");

        
    }
    }
}
