using System;
using System.Collections.Generic;
using System.Linq;

namespace SeqDistK
{
    class KtupleData
    {
        #region 私有字段

        /// <summary>
        /// ktuple统计值
        /// </summary>
        private List<int> _listKtuple = new List<int>();
        /// <summary>
        /// 所有ktuple的统计值之和
        /// </summary>


        #endregion

        #region 属性
        public List<int> ListKtuple { get => _listKtuple; set => _listKtuple = value; }

        #endregion

        #region 方法


        /// <summary>
        /// 计算kTuple统计值
        /// </summary>
        /// <param name="listSequenceInt">序列</param>
        /// <param name="k">k值</param>
        /// <returns></returns>
        public int CountKtuple(List<List<int>> listSequenceInt, int k)
        {
            try
            {
                //统计
                int total = 0;
                int count = 1 << (k << 1);  //4的k次方
                int[] arrKtuple = new int[count];
                for (int i = 0; i < listSequenceInt.Count; i++)
                {
                    if (listSequenceInt[i].Count >= k)
                    {
                        total += (listSequenceInt[i].Count - k + 1);
                        GetKtupleCount(k, listSequenceInt[i], arrKtuple);
                    }
                }
                _listKtuple = arrKtuple.ToList();
                return total;
            }
            catch
            {
                throw new Exception("CountKtuple has an error!");
            }
        }


        /// <summary>
        /// 计算kTuple统计值
        /// </summary>
        /// <param name="k">k值</param>
        /// <param name="seqInt">序列的一部分</param>
        /// <param name="arrKtuple">统计结果数组</param>
        private void GetKtupleCount(int k, List<int> seqInt, int[] arrKtuple)
        {
            int length = seqInt.Count - k + 1;

            int key = 0;

            for (int i = 0; i < k; i++)
            {
                key = (key << 2) + seqInt[i];
            }
            arrKtuple[key]++;
            int flag = (1 << ((k - 1) << 1)) - 1;    //4^(k-1)-1
            for (int i = 1; i < length; i++)
            {
                key = ((key & flag) << 2) + seqInt[i + k - 1];   //向右滑动窗口一位
                arrKtuple[key]++;
            }
        }
        #endregion
    }
}
