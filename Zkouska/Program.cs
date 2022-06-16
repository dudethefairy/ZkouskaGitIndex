// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;

namespace Zkouska // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Git Index Reader!");

            GitIndexReader reader = new GitIndexReader();
            reader.IndexPath = "zkouska-3-index";
            //reader.EnumerateEntries();

            IEnumerable<GitIndexReader.GitIndexEntry> polozky = reader.EnumerateEntries();

            FilteredTreeBuilder<int> treeBuilder = new FilteredTreeBuilder<int>();

            //FILTR NA ADRESARE POUZE src/
            treeBuilder.PathFilter = (str) =>
            {
                if (str.StartsWith("src"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };

            foreach (GitIndexReader.GitIndexEntry polozkyEntry in polozky)
            {
                //Console.WriteLine(polozkyEntry.FullPath + " | " + polozkyEntry.Size);
                treeBuilder.AddNode(polozkyEntry.FullPath, polozkyEntry.Size);
            }

            TreeObject<int> strom = treeBuilder.Build();
            Console.WriteLine(strom.ToString());


            Console.WriteLine("KONEC");

        }
    }
}