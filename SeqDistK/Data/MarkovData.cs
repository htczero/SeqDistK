using System;
using System.Collections.Generic;

namespace SeqDistK
{
    class MarkovData
    {
        private List<double> _listMarkov = new List<double>();
        private Dictionary<int, double[]> _dicRtemp;

        public List<double> ListMarkov { get => _listMarkov; set => _listMarkov = value; }
        public Dictionary<int, double[]> DicRtemp { get => _dicRtemp; set => _dicRtemp = value; }





        /// <summary>
        /// 10进制转4进制
        /// </summary>
        /// <param name="temp">10进制数</param>
        /// <param name="k">4进制的位数</param>
        /// <returns>返回4进制数链表</returns>
        //private void Get4Bits(int temp, int k, int[] arr)
        //{
        //    int b = 3;
        //    for (int i = 0; i < k - 1; i++)
        //    {
        //        arr[k - 1 - i] = temp & b;
        //        temp = temp >> 2;
        //    }
        //    arr[0] = temp;
        //}

        /// <summary>
        /// 4进制数截取一部分后转10进制
        /// </summary>
        /// <param name="list">4进制数</param>
        /// <param name="r">截取位置</param>
        /// <param name="length">截取长度</param>
        /// <returns></returns>
        //private int Get10Bits(int[] arr, int r, int length)
        //{
        //    int result = 0;
        //    for (int i = 0; i < length; i++)
        //    {
        //        //int tmp = 1;  
        //        result += (arr[i + r] << ((length - i - 1) << 1));   //(arr[i + r]*4的（length - i - 1）次方

        //    }
        //    return result;
        //}

        /// <summary>
        /// 计算马尔科夫（非0阶）
        /// </summary>
        /// <param name="k">k值</param>
        /// <param name="r">马尔科夫阶数</param>
        /// <param name="total">序列的归一化长度</param>
        /// <param name="list1">kTuple统计数据表1</param>
        /// <param name="list2">kTuple统计数据表2</param>
        /// <returns></returns>
        private List<double> CalMarkovPossibility(int k, int r, int total, List<int> list1, List<int> list2)
        {
            int count = 1 << (k << 1);   //4的k次方
            double[] arrPos = new double[count];

            double[] arr ;

            if (DicRtemp.ContainsKey(r))
            {
                arr = DicRtemp[r];
            }
            else
            {
                arr = new double[list2.Count];
                for (int i = 0; i < list2.Count; i += 4)
                {
                    double tmp = list2[i] + list2[i + 1] + list2[i + 2] + list2[i + 3];
                    for (int j = 0; j < 4; j++)
                    {
                        if (tmp != 0)
                        {
                            arr[i + j] = list2[i + j] / tmp;
                        }
                    }
                }
                DicRtemp.Add(r, arr);
            }
    

            int end = k - r;
            int rCount = 1 << (r << 1);     //4的r次方
            int b = rCount - 1;

            for (int i = 0; i < rCount; i++)
            {
                double res = list1[i] * 1.0 / total;
                RecursionCal(i, res, arr, i, 0, end, arrPos, b);
            }

            return new List<double>(arrPos);
        }

        private List<double> CalMarkovPossibility(int k, int r, SequenceData sd)
        {
            List<double> preMarkov = sd.GetMarkovData(k - 1, r);
            int b = (1 << (r << 1)) - 1;  //4的r次方-1
            double[] arr = sd.MarkovRtemp[r];
            int count = 1 << (k << 1);   //4的k次方
            double[] arrPos = new double[count];

            int tmp = 0;
            int key = 0;
            int rKey = 0;
            for (int i = 0; i < preMarkov.Count; i++)
            {
                tmp = (b & i) << 2;
                key = i << 2;
                for (int j = 0; j < 4; j++)
                {
                    rKey = tmp + j;
                    arrPos[key + j] = preMarkov[i] * arr[rKey];
                }
            }
            return new List<double>(arrPos);
        }

        /// <summary>
        /// 递归求解Markov概率
        /// </summary>
        /// <param name="rKey">（r-1）个符号</param>
        /// <param name="res"></param>
        /// <param name="arr">对应rKey的概率</param>
        /// <param name="key">累计转移的整体key</param>
        /// <param name="count">当前递归深度</param>
        /// <param name="end">递归深度次数（k-r）</param>
        /// <param name="arrMarkov">用来保存Markov概率结果</param>
        /// <param name="b">用来清rKey的最高位</param>
        private void RecursionCal(int rKey, double res, double[] arr, int key, int count, int end, double[] arrMarkov, int b)
        {
            count++;
            if (end == count)
            {
                key = key << 2;
                rKey = rKey << 2;
                arrMarkov[key] = res * arr[rKey];
                for (int i = 1; i < 4; i++)
                {
                    arrMarkov[key + i] = res * arr[rKey + i];
                }
                return;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    int newRkey = (rKey << 2) + i;
                    int newKey = (key << 2) + i;
                    double newRes = res * arr[newRkey];
                    newRkey = newRkey & b;
                    RecursionCal(newRkey, newRes, arr, newKey, count, end, arrMarkov, b);
                }
                return;
            }
        }


        /// <summary>
        /// 计算0阶马尔科夫
        /// </summary>
        /// <param name="k">k值</param>
        /// <param name="total">序列归一化长度</param>
        /// <param name="list1">kTuple统计数据表</param>
        /// <returns></returns>
        private List<double> CalMarkovPossibility(int k, int total, List<int> list1)
        {
            int count = 1 << (k << 1);   //4的k次方
            List<double> listPos = new List<double>(count);
            double[] arrMarkov = new double[count];
            double[] arr = new double[4];
            for (int i = 0; i < 4; i++)
            {
                arr[i] = list1[i] * 1.0 / total;
            }
            RecursionCal(k, 1, 0, 0, arrMarkov, arr);
            return new List<double>(arrMarkov);
        }

        /// <summary>
        /// 计算非零阶马尔科夫
        /// </summary>
        /// <param name="end"></param>
        /// <param name="res"></param>
        /// <param name="count"></param>
        /// <param name="key"></param>
        /// <param name="arrMarkov"></param>
        /// <param name="arr"></param>
        private void RecursionCal(int end, double res, int count, int key, double[] arrMarkov, double[] arr)
        {
            count++;
            if (end == count)
            {
                key = key << 2;
                arrMarkov[key] = res * arr[0];
                for (int i = 1; i < 4; i++)
                {
                    arrMarkov[key + i] = res * arr[i];
                }
                return;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    int newKey = (key << 2) + i;
                    double newRes = res * arr[i];
                    RecursionCal(end, newRes, count, newKey, arrMarkov, arr);
                }
                return;
            }
        }


        /// <summary>
        /// 计算MArkov
        /// </summary>
        /// <param name="k">k值</param>
        /// <param name="m">马尔科夫阶数</param>
        /// <param name="sd">序列</param>
        public void CalMarkov(int k, int m, SequenceData sd)
        {
            //if (k <= m || k < 2)
            //{
            //    return;
            //}
            try
            {
                if (m == 0)  //0阶马尔科夫
                {
                    int total = sd.GetTotal(1);
                    List<int> list1 = sd.GetKtupleData(1);
                    ListMarkov = CalMarkovPossibility(k, total, list1);
                }
                else
                {
                    if (sd.ContainsMarkovKey(k - 1, m))
                    {
                        ListMarkov = CalMarkovPossibility(k, m, sd);
                    }
                    else
                    {
                        int total = sd.GetTotal(m);
                        List<int> list1 = sd.GetKtupleData(m);
                        List<int> list2 = sd.GetKtupleData(m + 1);
                        DicRtemp = sd.MarkovRtemp;
                        ListMarkov = CalMarkovPossibility(k, m, total, list1, list2);
                    }
                }
            }
            catch
            {
                throw new Exception("GetMarkov has an error!");
            }
        }
    }
}
