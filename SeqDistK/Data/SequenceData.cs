using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SeqDistK
{
    public class SequenceData
    {
        #region 私有字段

        /// <summary>
        /// key:k值   value:ktuple统计值数据
        /// </summary>
        private Dictionary<int, KtupleData> _dicKtuple = new Dictionary<int, KtupleData>();
        /// <summary>
        /// key:k值   value:markove概率值数据
        /// </summary>
        private Dictionary<int, Dictionary<int, MarkovData>> _dicMarkov = new Dictionary<int, Dictionary<int, MarkovData>>();
        /// <summary>
        /// key:k值    value:对应ktuple的统计值总和
        /// </summary>
        private Dictionary<int, int> _dicTotal = new Dictionary<int, int>();
        /// <summary>
        /// 序列的ID
        /// </summary>
        private int _id;
        /// <summary>
        /// 序列整形格式
        /// </summary>
        private List<List<int>> _lsitSequenceInt = new List<List<int>>();

        private Dictionary<int, double[]> _dicMarkovRtemp = new Dictionary<int, double[]>();

        private readonly static int[] arrDic = { 0, -1, 2, -1, -1, -1, 1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 3, -1, -1, -1, -1, -1, -1 };
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filePath">序列文件的路径</param>
        public SequenceData(string filePath, int id)
        {
            GetSeqText(filePath);
            _id = id;
        }

        internal Dictionary<int, KtupleData> Ktuple { get => _dicKtuple; set => _dicKtuple = value; }
        internal Dictionary<int, Dictionary<int, MarkovData>> Markov { get => _dicMarkov; set => _dicMarkov = value; }

        internal Dictionary<int, double[]> MarkovRtemp { get => _dicMarkovRtemp; set => _dicMarkovRtemp = value; }
        public Dictionary<int, int> DicTotal { get => _dicTotal; set => _dicTotal = value; }
        public int ID { get => _id; set => _id = value; }
        public List<List<int>> SequenceInt { get => _lsitSequenceInt; set => _lsitSequenceInt = value; }




        /// <summary>
        /// 获取序列文本
        /// </summary>
        /// <param name="filePath">序列文件的路径</param>
        private void GetSeqText(string filePath)
        {
            StringBuilder sb = new StringBuilder();
            List<string> listSeqText = new List<string>();
            //Dictionary<char, int> dic = new Dictionary<char, int>(4);
            //dic.Add('A', 0);
            //dic.Add('G', 1);
            //dic.Add('C', 2);
            //dic.Add('T', 3);
            using (StreamReader sr = new StreamReader(filePath, Encoding.ASCII))
            {
                string firstLine = sr.ReadLine();
                if (firstLine[0] == 'A' || firstLine[0] == 'G' || firstLine[0] == 'C' || firstLine[0] == 'T' || firstLine[0] == 'N')
                    sb.Append(firstLine);
                while (true)
                {
                    string tmp = sr.ReadLine();
                    if (tmp == null)
                        break;
                    sb.Append(tmp);
                }
            }
            listSeqText.AddRange(sb.ToString().Split(new char[] { 'N' }, StringSplitOptions.RemoveEmptyEntries));
            for (int i = 0; i < listSeqText.Count; i++)
            {
                SequenceInt.Add(new List<int>());
                foreach (char item in listSeqText[i])
                {
                    int key = item - 'A';
                    if (arrDic[key] != -1)
                    {
                        SequenceInt[i].Add(arrDic[key]);
                    }
                }
            }
        }

        /// <summary>
        /// 统计ktuple的个数
        /// </summary>
        /// <param name="k">ktuple的字长</param>
        public void CalKtupleCount(int k)
        {
            if (!Ktuple.ContainsKey(k))
            {
                Ktuple.Add(k, new KtupleData());
                int total = 0;
                total = Ktuple[k].CountKtuple(SequenceInt, k);
                DicTotal.Add(k, total);
            }
        }

        /// <summary>
        /// 计算Markov概率
        /// </summary>
        /// <param name="k">ktuple的字长</param>
        /// <param name="m">markov的阶数</param>
        public void CalMarkov(int k, int m)
        {
            if (k <= m || k < 2)
            {
                return;
            }
            if (!_dicMarkov.ContainsKey(k) || !_dicMarkov[k].ContainsKey(m))
            {
                MarkovData md = new MarkovData();
                md.CalMarkov(k, m, this);
                if (Markov.ContainsKey(k))
                {
                    Markov[k].Add(m, md);
                }
                else
                {
                    Markov.Add(k, new Dictionary<int, MarkovData>());
                    Markov[k].Add(m, md);
                }
            }
        }

        /// <summary>
        /// 获取ktuple的数据
        /// </summary>
        /// <param name="k">ktuple的字长</param>
        /// <returns></returns>
        public List<int> GetKtupleData(int k)
        {
            if (!_dicKtuple.ContainsKey(k))
            {
                CalKtupleCount(k);
            }
            return _dicKtuple[k].ListKtuple;
        }

        /// <summary>
        /// 获取ktuple总个数
        /// </summary>
        /// <param name="k">ktuple的字长</param>
        /// <returns></returns>
        public int GetTotal(int k)
        {
            if (!_dicTotal.ContainsKey(k))
            {
                CalKtupleCount(k);
            }
            return DicTotal[k];
        }

        /// <summary>
        /// 获取markov的数据
        /// </summary>
        /// <param name="k">ktuple的字长</param>
        /// <param name="m">markov的阶数</param>
        /// <returns></returns>
        public List<double> GetMarkovData(int k, int m)
        {
            if (!_dicMarkov.ContainsKey(k) || !_dicMarkov[k].ContainsKey(m))
            {
                CalMarkov(k, m);
            }
            return _dicMarkov[k][m].ListMarkov;
        }

        public bool ContainsMarkovKey(int k, int r)
        {
            if (_dicMarkov.ContainsKey(k) && _dicMarkov[k].ContainsKey(r))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public void Clear()
        {
            _dicKtuple = null;
            _dicMarkov = null;
            _dicTotal = null;
            _lsitSequenceInt = null;
            _dicMarkovRtemp = null;
        }
    }
}
