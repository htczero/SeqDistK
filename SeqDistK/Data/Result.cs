using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SeqDistK
{
    class Result
    {
        /// <summary>
        /// key:Fun_k_m  value:matrix
        /// </summary>
        private Dictionary<string, double[,]> _dicResult = new Dictionary<string, double[,]>();

        /// <summary>
        /// key:Fun_k_m  value:vector
        /// </summary>
        private Dictionary<string, double[]> _dicResultVector = new Dictionary<string, double[]>();

        /// <summary>
        /// 矩阵或者向量的大小
        /// </summary>
        private readonly int _size = 0;

        /// <summary>
        /// 是否是向量形式
        /// </summary>
        private readonly bool _bVector = false;

        public Dictionary<string, double[,]> DicResult { get => _dicResult; set => _dicResult = value; }
        public Dictionary<string, double[]> DicResultVector { get => _dicResultVector; set => _dicResultVector = value; }

        /// <summary>
        /// 采用矩阵形式的构造函数
        /// </summary>
        /// <param name="seqCount">矩阵的大小，序列的数量</param>
        /// <param name="mode">模式
        /// 0：对称矩阵
        /// 1：不对称矩阵
        /// 2：向量</param>
        public Result(int seqCount, bool b = false)
        {
            _size = seqCount;
            _bVector = b;
        }



        /// <summary>
        /// 向矩阵或者向量里面添加结果
        /// </summary>
        /// <param name="key">key:Fun_k_m   对应的矩阵或者向量</param>
        /// <param name="i">序列X的编号</param>
        /// <param name="j">序列Y的编号</param>
        /// <param name="value">要添加的值</param>
        public void Add(string key, int i, int j, double value)
        {
            //向量
            if (_bVector)
            {
                AddToVector(key, j, value);
            }
            else   //矩阵
            {
                AddToMatrix(key, i, j, value);
            }
        }

        /// <summary>
        /// 添加结果到矩阵
        /// </summary>
        /// <param name="key">key:Fun_k_m   对应的矩阵</param>
        /// <param name="i">序列X的编号</param>
        /// <param name="j">序列Y的编号</param>
        /// <param name="value">要添加的值</param>
        public void AddToMatrix(string key, int i, int j, double value)
        {
            //如果key对应的矩阵不存在，则创建一个
            if (!DicResult.ContainsKey(key))
            {
                AddNewMatrix(key);
            }
            DicResult[key][i, j] = value;
            DicResult[key][j, i] = value;
        }

        /// <summary>
        /// 创建新矩阵
        /// </summary>
        /// <param name="key">矩阵对应的key索引</param>
        public void AddNewMatrix(string key)
        {
            //如果key已经存在了，则返回
            if (DicResult.ContainsKey(key))
            {
                return;
            }
            DicResult.Add(key, new double[_size, _size]);
        }

        /// <summary>
        /// 创建新的向量
        /// </summary>
        /// <param name="key">向量对应的key索引</param>
        public void AddNewVector(string key)
        {
            if (DicResultVector.ContainsKey(key))
            {
                return;
            }
            DicResultVector.Add(key, new double[_size]);
        }

        /// <summary>
        /// 添加结果到向量
        /// </summary>
        /// <param name="key">key:Fun_k_m   对应的向量</param>
        /// <param name="i">序列编号</param>
        /// <param name="value">要添加的值</param>
        public void AddToVector(string key, int i, double value)
        {
            //如果key对应的向量不存在，则创建新的向量
            if (!DicResultVector.ContainsKey(key))
            {
                AddNewVector(key);
            }
            DicResultVector[key][i] = value;
        }

        /// <summary>
        /// 将矩阵的结果写入硬盘
        /// </summary>
        /// <param name="listSeqName">序列的名字列表</param>
        /// <param name="savePath">要保存的目录</param>
        /// <param name="fun">对比的方法名称</param>
        /// <param name="k">k值</param>
        /// <param name="m">马尔科夫阶数</param>
        private void WriteResultMatrix(List<string> listSeqName, string savePath, string fun, int k, int m = -1)
        {
            string key = fun + "_k" + k;
            if (fun == "d2S" || fun == "d2star")
            {
                key += "_M" + m;
            }
            if (DicResult.ContainsKey(key))
            {
                double[,] matrix = DicResult[key];
                string path = savePath + "\\" + fun;
                Directory.CreateDirectory(path);
                path = path + "\\" + key + ".txt";
                using (StreamWriter sw = new StreamWriter(path, false, Encoding.ASCII))
                {
                    for (int i = 0; i < listSeqName.Count; i++)
                    {
                        sw.Write("\t" + listSeqName[i]);
                    }
                    sw.WriteLine();
                    for (int i = 0; i < listSeqName.Count; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(listSeqName[i]);
                        for (int j = 0; j < listSeqName.Count; j++)
                        {
                            if (_size != 0)
                            {
                                sb.Append("\t" + matrix[i, j]);
                            }
                            else
                            {
                                sb.Append("\t" + matrix[i, j]);
                            }
                        }
                        sw.WriteLine(sb.ToString());
                    }
                }//using
            }//if         
        }

        /// <summary>
        /// 将对应的向量写入硬盘
        /// </summary>
        /// <param name="savePath">保存的目录</param>
        /// <param name="fun">对比的方法名称</param>
        /// <param name="k">k值</param>
        /// <param name="m">马尔科夫阶数</param>
        private void WriteResultVector(string savePath, string fun, int k, int m = -1)
        {
            string key = fun + "_k" + k;
            if (fun == "d2S" || fun == "d2star")
            {
                key += "_M" + m;
            }
            if (DicResultVector.ContainsKey(key))
            {
                double[] list = DicResultVector[key];
                string path = savePath + "\\" + fun;
                Directory.CreateDirectory(path);
                path = path + "\\" + key + ".txt";

                using (StreamWriter sw = new StreamWriter(path, false, Encoding.ASCII))
                {
                    for (int i = 0; i < list.Length; i++)
                    {
                        sw.Write(list[i] + "\t");
                    }
                }//using
            }
        }

        /// <summary>
        /// 将结果写入硬盘
        /// </summary>
        /// <param name="listSeqName">序列名字列表</param>
        /// <param name="savePath">保存的目录</param>
        /// <param name="fun">对比方法的名称</param>
        /// <param name="k">k值</param>
        /// <param name="m">马尔科夫阶数</param>
        public void WriteResult(List<string> listSeqName, string savePath, string fun, int k, int m = -1)
        {
            if (_bVector)
            {
                WriteResultVector(savePath, fun, k, m);
            }
            else
            {
                WriteResultMatrix(listSeqName, savePath, fun, k, m);
            }
        }
    }
}
