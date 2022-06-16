using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zkouska
{
    public class FilteredTreeBuilder<T>
    {
        public Func<string, bool> PathFilter { get; set; }

        private List<string> paths;
        private List<T> datas;
        
        public FilteredTreeBuilder()
        {
            paths = new List<string>();
            datas = new List<T>();
        }

        public void AddNode(string path, T data)
        {
            this.paths.Add(path);
            //Console.WriteLine(path);
            this.datas.Add(data);
            //Console.WriteLine(data);
        }

        public TreeObject<T> Build()
        {
            TreeFolder<T> koren = new TreeFolder<T>();
            for(int i = 0; i < paths.Count; i++)
            {
                if (PathFilter(paths[i]))
                {
                    TreeFolder<T> aktualni = koren;

                    //ROZDELENI NA SLOZKY
                    string[] tokens = paths[i].Split('/');

                    //PROCHAZENI SLOZEK VE STROMU
                    for (int j = 0; j < tokens.Length-1; j++)
                    {
                        bool existuje = false;
                        foreach(TreeObject<T> podslozka in aktualni.Entries)
                        {
                            if(podslozka is TreeFolder<T>)
                            {
                                if(podslozka.Name == tokens[j])
                                {
                                    existuje = true;
                                    aktualni = podslozka as TreeFolder<T>;
                                    //SLOZKA JIZ EXISTUJE + PRECHOD DO JEJIHO ADRESARE
                                    break;
                                }
                            }
                        }
                        //SLOŽKA JEŠTĚ NEEXISTUJE JE NUTNÉ JI VYTVOŘIT + PRECHOD DO JEJIHO ADRESARE
                        if (!existuje)
                        {
                            TreeFolder<T> novaSlozka = new TreeFolder<T>();
                            novaSlozka.Name = tokens[j];
                            novaSlozka.delkaCesty = j;
                            aktualni.Entries.Add(novaSlozka);
                            aktualni = novaSlozka;
                        }
                    }

                    TreeFile<T> file = new TreeFile<T>();
                    file.Name = tokens[tokens.Length - 1];
                    file.Data = datas[i];
                    file.delkaCesty = tokens.Length - 1;
                    aktualni.Entries.Add(file);
                }
            }
            return koren;
        }
    }
}
