using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CalDistance
{
    class Dissimiliraty
    {
        protected List<double> listKtupleOne = new List<double>();
        protected List<double> listKtupleTwo = new List<double>();
        protected double totalOne;
        protected double totalTwo;
        protected List<string> listSeqname = new List<string>();
        protected List<string> listPath = new List<string>();
        private string saveDir;
        protected int precision;
        protected int current;
        protected string str;

        public string SaveDir
        {
            get
            {
                return saveDir;
            }

            set
            {
                saveDir = value;
            }
        }

        public Dissimiliraty(string str)
        {
            if (str != "")
            {
                this.str = "\\" + str;
            }
        }

        protected void WriteFile(string funName, List<string> list, string Mes, int k)
        {
            string savePath = saveDir + "\\" + funName + str;
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            string path = savePath + "\\Dissimiliraty_" + Mes + "_k" + k + ".txt";
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.ASCII))
            {
                int n = listSeqname.Count - list.Count;
                string str = str = listSeqname[0] + "\t";
                int length = listSeqname.Count;
                Action<int> act = (int num) =>
                {
                    string titleLine = "\t";
                    for (int i = num; i < listSeqname.Count; i++)
                    {
                        titleLine += listSeqname[i] + "\t";
                    }
                    sw.WriteLine(titleLine);
                };

                if (current == 0)
                {
                    act(0);
                }
                else if (current == -1)
                {
                    act(1);
                    n = 0;
                    list.Remove("0");
                    length--;
                }
                else
                {
                    str = listSeqname[current] + "\t";
                }
                for (int i = 0; i < length; i++)
                {
                    if (i < n)
                    {
                        str += "0\t";
                    }
                    else
                    {
                        str += list[i - n] + "\t";
                    }
                }
                sw.WriteLine(str);
                list.Clear();
            }//using
            savePath = null;
        }


        public virtual void GetDissimiliratyMatrix(int k)
        {

        }


        public virtual void GetDissimiliratyOneToN(int k)
        {

        }
    }
}
