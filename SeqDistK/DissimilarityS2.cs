using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CalDistance
{
    class DissimiliratyS2
    {
        #region 私有字段及其属性
        private List<int> listR = new List<int>();
        private List<List<int>> listKtuple = new List<List<int>>();
        private List<Dictionary<int, List<double>>> listMarkov = new List<Dictionary<int, List<double>>>();
        private int precision = 5;
        private List<int> listTotal = new List<int>();
        private List<string> listSeqname = new List<string>();
        private string saveDir;
        private int k;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dic">key为序列名字，value为指定k值的MarkovPossibility_Hao文件</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="k">指定k的值</param>
        /// <param name="precision">最后结果的精确度</param>
        public DissimiliratyS2(Dictionary<string, List<int>> dic, Dictionary<string, Dictionary<int, List<double>>> dicMarkov, Dictionary<string, int> dicTotal, string savePath, List<int> listR, int k, int precision = 5)
        {
            foreach (KeyValuePair<string, List<int>> item in dic)
            {
                listKtuple.Add(item.Value);
                listSeqname.Add(item.Key);
                listTotal.Add(dicTotal[item.Key]);
                listMarkov.Add(dicMarkov[item.Key]);
            }
            this.listR = listR;
            this.precision = precision;
            saveDir = savePath;
            this.k = k;
        }

        /// <summary>
        /// 计算距离
        /// </summary>
        private void CalDissimiliratyMatrix(List<int> listKtupleOne, List<int> listKtupleTwo, Dictionary<int, List<double>> dicPosOne, Dictionary<int, List<double>> dicPosTwo, int totalOne, int totalTwo, Dictionary<int, List<List<double>>> dicResult, int row)
        {
            double nX = totalOne;
            double nY = totalTwo;
            double n = Math.Pow(4, k);

            foreach (var j in listR)
            {
                double resultS2 = 0;
                double tmpS2 = 0;
                double tmpXS2 = 0;
                double tmpYS2 = 0;

                for (int i = 0; i < listKtupleOne.Count; i++)
                {
                    double cXi = listKtupleOne[i];
                    double cYi = listKtupleTwo[i];

                    double pXi = dicPosOne[j][i];
                    double pYi = dicPosTwo[j][i];

                    double fXi = cXi / nX;
                    double fYi = cYi / nY;

                    double cXi_sigma = cXi * pXi;
                    double cYi_sigma = cYi * pYi;

                    double tmp1 = 0;
                    double tmp2 = 0;
                    double tmp3 = 0;
                    double tmp4 = 0;

                    if (cXi_sigma == cYi_sigma)
                    {
                        tmp1 = 0;
                        tmp2 = 0;
                    }
                    else
                    {
                        tmpS2 = cXi_sigma + cYi_sigma;

                        tmp3 = 2 * cXi_sigma / tmpS2;
                        tmp1 = cXi_sigma * Math.Log(tmp3);

                        tmp4 = 2 * cYi_sigma / tmpS2;
                        tmp2 = cYi_sigma * Math.Log(tmp4);
                    }
                    //
                    if (tmp1.ToString() == "NaN" || tmp2.ToString() == "NaN")
                    {
                        ;
                    }
                    if(tmpXS2.ToString()=="NaN"|| tmpYS2.ToString() == "NaN")
                    {
                        ;
                    }
                    //
                    tmpXS2 += tmp1;
                    tmpYS2 += tmp2;
                }

                if (tmpXS2 == 0 && tmpYS2 == 0)
                    resultS2 = 0;

                else
                {
                    resultS2 = (tmpXS2 + tmpYS2) / n + 2 * Math.Log(2);
                }
                dicResult[j][row].Add(Math.Round(resultS2, precision));
            }//for
        }

        /// <summary>
        /// 获得距离矩阵
        /// </summary>
        public void GetDissimiliratyMatrix()
        {
            //int: M   List<List<double>>第几行  List<double>某一行的结果
            Dictionary<int, List<List<double>>> dicResult = new Dictionary<int, List<List<double>>>();
            foreach (var m in listR)
            {
                dicResult.Add(m, new List<List<double>>());
            }
            //并行部分
            for (int i = 0; i < listSeqname.Count - 1; i++)
            {
                foreach (List<List<double>> item in dicResult.Values)
                {
                    item.Add(new List<double>());
                }
                for (int j = i + 1; j < listSeqname.Count; j++)
                {
                    CalDissimiliratyMatrix(listKtuple[i], listKtuple[j], listMarkov[i], listMarkov[j], listTotal[i], listTotal[j], dicResult, i);
                }
            }
            WriteFile(dicResult);
        }

        private void WriteFile(Dictionary<int, List<List<double>>> dicResult)
        {
            int length = listSeqname.Count;

            string savePath = saveDir + "\\S2";
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            foreach (KeyValuePair<int, List<List<double>>> item in dicResult)
            {
                string save = savePath + ("\\S2_M" + item.Key + "_k" + k + ".txt");
                using (StreamWriter sw = new StreamWriter(save, false, Encoding.ASCII))
                {
                    //写入第一行序列名字
                    for (int j = 0; j < length; j++)
                    {
                        sw.Write("\t" + listSeqname[j]);
                    }
                    sw.Write("\r\n");
                    for (int j = 0; j < item.Value.Count; j++)
                    {
                        //写入左边序列名字
                        sw.Write(listSeqname[j]);

                        //写入0
                        int zeroCount = length - item.Value[j].Count;
                        for (int i = 0; i < zeroCount; i++)
                        {
                            sw.Write("\t0");
                        }

                        //写入结果
                        for (int i = 0; i < item.Value[j].Count; i++)
                        {
                            sw.Write("\t" + item.Value[j][i]);
                        }
                        sw.Write("\r\n");
                    }//for
                }//using
            }//foreach
        }

    }
}
