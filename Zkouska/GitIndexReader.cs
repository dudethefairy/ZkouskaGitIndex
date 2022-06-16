using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zkouska
{
    public class GitIndexReader
    {
        public class GitIndexEntry
        {
            public string FullPath { get; set; }
            public int Size { get; set; }

            public GitIndexEntry(string fPath, int velikost)
            {
                FullPath = fPath;
                Size = velikost;
            }
        }

        public string? IndexPath { get; set; }

        public GitIndexReader()
        {

        }

        public IEnumerable<GitIndexEntry> EnumerateEntries()
        {
            using (var stream = File.Open(IndexPath,FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.Default, false))
                {
                    List<GitIndexEntry> entries = new List<GitIndexEntry>();

                    //HEADER

                    //NEPOTREBNY
                    string a = new String(reader.ReadChars(4));
                    Console.WriteLine(a);

                    //NEPOTREBNY
                    int version = reader.ReadInt32();
                    version = System.Buffers.Binary.BinaryPrimitives.ReverseEndianness(version);
                    Console.WriteLine(version);

                    //POCET ZAZNAMU
                    int size = reader.ReadInt32();
                    size = System.Buffers.Binary.BinaryPrimitives.ReverseEndianness(size);
                    Console.WriteLine(size);

                    //CESTA MUSI BYT UKONCENA NULLEM, A MUSI BYT DELITELNA 8 !!

                    //NACTENI VSECH CEST K SOUBORUM
                    for (int j= 0; j < size; j++)
                    {
                        //INDEX ENTRY
                        //reader.ReadBytes(62);

                        reader.ReadBytes(36);

                        int fileSize = reader.ReadInt32();
                        fileSize = System.Buffers.Binary.BinaryPrimitives.ReverseEndianness(fileSize);
                        //Console.WriteLine(fileSize);

                        reader.ReadBytes(22);

                        string filePath = "";
                        for (int i = 1; ; i++)
                        {
                            char next = reader.ReadChar();
                            if (next != 0)
                            {
                                filePath += next;
                            }
                            else
                            {
                                //Console.WriteLine("delka cesty: " + i);
                                int nulBytes = ((62 + i) % 8);
                                //Console.WriteLine("celkovy pocet bytu polozky: " + (62 + i));
                                if (nulBytes != 0)
                                {
                                    //Console.WriteLine("pocet null bytu polozky: " + (8 - nulBytes));
                                    //for(int k=0; k < (8-nulBytes); k++)
                                    //{
                                    //    Console.WriteLine(reader.ReadByte());
                                    //}

                                    reader.ReadBytes((8 - nulBytes));
                                }
                                else
                                {
                                    //Console.WriteLine("pocet null bytu: " + 0);
                                }

                                break;
                            }                         
                        }

                        ////VYSLEDNY VYPIS CESTY SOUBORU A JEHO VELIKOSTI
                        //Console.WriteLine(filePath);
                        //Console.WriteLine("velikost: " + fileSize);

                        entries.Add(new GitIndexEntry(filePath, fileSize));

                        ////POZICE V SOUBORU
                        //var str = reader.BaseStream;
                        //Console.WriteLine(str.Position);
                    }

                    IEnumerable<GitIndexEntry> result = entries;
                    return result;

                }
            }
        }

    }
}
