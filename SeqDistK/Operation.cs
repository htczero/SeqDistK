using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeqDistK
{
    class Operation
    {
        private List<TreeNode> _listNode = new List<TreeNode>();

        private List<int> _listK = new List<int>();

        private List<int> _listM = new List<int>();

        private List<string> _listFun = new List<string>();

        public static int _cpu = 2;

        private Action<int, string> _actShow;

        public List<TreeNode> ListNode { get => _listNode; set => _listNode = value; }
        public List<int> ListK { get => _listK; set => _listK = value; }
        public List<int> ListM { get => _listM; set => _listM = value; }
        public List<string> ListFun { get => _listFun; set => _listFun = value; }
        public Action<int, string> ActShow { get => _actShow; set => _actShow = value; }


        /// <summary>
        /// 左侧下拉框改变时，右侧的值跟着变动，以确保输入值合法
        /// </summary>
        /// <param name="cbx1">左侧下拉框</param>
        /// <param name="cbx2">右侧下拉框</param>
        public void ComboBoxSetting(ComboBox cbx1, ComboBox cbx2)
        {
            int tmp1 = int.Parse(cbx1.SelectedItem.ToString());
            int tmp2 = int.Parse(cbx1.Items[cbx1.Items.Count - 1].ToString());
            cbx2.Items.Clear();
            for (int i = tmp1; i <= tmp2; i++)
            {
                cbx2.Items.Add(i);
            }
            cbx2.SelectedIndex = 0;
        }

        /// <summary>
        /// 向树添加节点，节点的Tag记录绝对路径，通过递归遍历添加
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="path">导入路径</param>
        public void TreeViewAddNode(TreeNodeCollection nodes, params string[] path)
        {
            foreach (var item in path)
            {
                TreeNode node = new TreeNode(Path.GetFileName(item));
                node.Tag = item;
                nodes.Add(node);
                if (!File.Exists(item))
                {
                    string[] files = Directory.GetFiles(item);
                    TreeViewAddNode(node.Nodes, files);
                    string[] dirs = Directory.GetDirectories(item);
                    TreeViewAddNode(node.Nodes, dirs);
                }
            }
            return;
        }

        /// <summary>
        /// 从下拉框中获取数值
        /// </summary>
        /// <param name="cbx1">左侧下拉框</param>
        /// <param name="cbx2">右侧下拉框</param>
        /// <param name="list">用于记录数值</param>
        public void GetValueFromComboBox(ComboBox cbx1, ComboBox cbx2, List<int> list)
        {
            int tmp1 = int.Parse(cbx1.SelectedItem.ToString());
            int tmp2 = int.Parse(cbx2.SelectedItem.ToString());
            for (int i = tmp1; i <= tmp2; i++)
            {
                list.Add(i);
            }
        }

        /// <summary>
        /// 递归获取节点
        /// </summary>
        /// <param name="nodes"></param>
        private void GetListNode(TreeNodeCollection nodes, bool b)
        {
            foreach (TreeNode node in nodes)
            {
                string path = (string)node.Tag;
                if (!File.Exists(path))
                {
                    GetListNode(node.Nodes, b);
                }
                else
                {
                    if (b)
                    {
                        if ((node.NextNode != null && node.NextNode.Nodes.Count != 0) || (node.PrevNode != null && node.PrevNode.Nodes.Count != 0))
                        {
                            if (!ListNode.Contains(node.Parent))
                                ListNode.Add(node.Parent);
                        }
                    }
                    else
                    {
                        if (!ListNode.Contains(node.Parent))
                            ListNode.Add(node.Parent);
                    }
                }
            }
            return;
        }

        /// <summary>
        /// 初始化状态信息
        /// </summary>
        /// <param name="pgb">进度条</param>
        /// <param name="nodes">各组序列节点集合</param>
        /// <param name="b">是否是单行向量</param>
        private void InitalState(ProgressBar pgb, TreeNodeCollection nodes, bool b)
        {

            pgb.Value = 0;
            pgb.Maximum = 0;
            if (!b)   //多对多
            {
                foreach (TreeNode node in nodes)
                {
                    int n = node.Nodes.Count;
                    pgb.Maximum += n * (n - 1) / 2;
                }
            }
            else    //一对多
            {
                foreach (TreeNode node in nodes)
                {
                    foreach (TreeNode item in node.Nodes)
                    {
                        if (item.Nodes.Count != 0)
                        {
                            pgb.Maximum += item.Nodes.Count;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 开始计算
        /// </summary>
        /// <param name="saveDir">保存目录</param>
        /// <param name="nodes">各组节点集合</param>
        /// <param name="p">进度条</param>
        /// <param name="txt">状态文本框</param>
        /// <param name="b">是否为一对多模式</param>
        public void Star(string saveDir, TreeNodeCollection nodes, ProgressBar p, TextBox txt, bool b)
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();
            GetListNode(nodes, b);
            InitalState(p, nodes, b);

            //int i = 1;
            //分组节点
            foreach (TreeNode node in ListNode)
            {
                if (!b)
                {
                    CalOneGroup(node, saveDir, txt, p);
                }
                else
                {
                    CalOneGroup_OnwLow(node, saveDir, txt, p);
                }
            }
            sp.Stop();
            MessageBox.Show("Completed！" + (sp.ElapsedMilliseconds * 1.0 / 1000).ToString() + "s", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start(saveDir);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Inital()
        {
            _listNode.Clear();
            _listK.Clear();
            _listM.Clear();
            _listFun.Clear();
        }

        /// <summary>
        /// 计算一组数据，N To N
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="saveDir">保存目录</param>
        /// <param name="txt">显示状态的TextBox</param>
        /// <param name="pgb">进度条</param>
        private void CalOneGroup(TreeNode node, string saveDir, TextBox txt, ProgressBar pgb)
        {

            List<SequenceData> listSeqData = new List<SequenceData>();
            List<string> listSeqName = new List<string>();
            Dissimiliraty dy = new Dissimiliraty(ListFun);
            int id = 0;
            foreach (TreeNode item in node.Nodes)
            {
                string oneSeqPath = (string)item.Tag;
                listSeqData.Add(new SequenceData(oneSeqPath, id));
                id++;
                listSeqName.Add(Path.GetFileNameWithoutExtension(oneSeqPath));
            }

            int count = listSeqData.Count;
            Result result = new Result(count);

            //计算两条序列的所有k值，所有马尔科夫阶数以及所有对方法的结果
            Action<SequenceData, SequenceData> act = (SequenceData x, SequenceData y) =>
               {
                   for (int k = 0; k < ListK.Count; k++)
                   {
                       if (ListM.Count != 0)
                       {
                           for (int m = 0; m < ListM.Count; m++)
                           {
                               dy.CalDissimiliraty(x, y, result, ListK[k], ListM[m]);
                           }
                       }
                       else
                       {
                           dy.CalDissimiliraty(x, y, result, ListK[k]);
                       }
                   }
               };

            //一条和N条对比，一条和N-1条对比......一条和一条对比
            SequenceData seqX = listSeqData[0];
            PreGetData(seqX);
            for (int i = 0; i < count - 1; i++)
            {
                SequenceData seqTmp = listSeqData[i + 1];
                act(seqX, seqTmp);
                //并行计算
                Parallel.For(i + 2, count, new ParallelOptions() { MaxDegreeOfParallelism = _cpu }, (j) =>
                    {
                        //    for (int j = i + 1; j < count; j++)
                        //{
                        SequenceData seqY = listSeqData[j];
                        act(seqX, seqY);
                        // }
                    });
                listSeqData[i].Clear();   //清空后续不需要的数据
                seqX = seqTmp;     //
                ShowState(pgb, txt, count - i - 1);
            }

            //将结果写入硬盘
            saveDir += "\\" + node.FullPath;
            foreach (var fun in ListFun)
            {
                foreach (var k in ListK)
                {
                    if (ListM.Count != 0)
                    {
                        foreach (var m in ListM)
                        {
                            result.WriteResult(listSeqName, saveDir, fun, k, m);
                        }//foreach
                    }
                    else
                    {
                        result.WriteResult(listSeqName, saveDir, fun, k);
                    }
                }//foreach
            }//foreach
            listSeqData = null;
            dy = null;
            result = null;
            //GC.Collect();
        }

        /// <summary>
        /// 计算一组数据，1 To N
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="saveDir">保存目录</param>
        /// <param name="txt">显示状态的TextBox</param>
        /// <param name="pgb">进度条</param>
        private void CalOneGroup_OnwLow(TreeNode node, string saveDir, TextBox txt, ProgressBar pgb)
        {
            List<SequenceData> listSeqData = new List<SequenceData>();
            List<string> listSeqName = new List<string>();
            Dissimiliraty dy = new Dissimiliraty(ListFun);

            //将单独的那条放在0号位置，其余接着后面
            listSeqData.Add(null);
            listSeqName.Add("");
            foreach (TreeNode item in node.Nodes)
            {
                if (item.Nodes.Count == 0)
                {
                    string oneSeqPath = (string)item.Tag;
                    listSeqData[0] = new SequenceData(oneSeqPath, 0);
                    listSeqName[0] = Path.GetFileNameWithoutExtension(oneSeqPath);
                }
                else
                {
                    //其余序列插入到后面
                    int id = 0;
                    foreach (TreeNode tmp in item.Nodes)
                    {
                        string oneSeqPath = (string)tmp.Tag;
                        listSeqData.Add(new SequenceData(oneSeqPath, id));
                        id++;
                        listSeqName.Add(Path.GetFileNameWithoutExtension(oneSeqPath));
                    }
                }
            }

            int count = listSeqData.Count;
            Result result = new Result(count - 1, true);

            Action<SequenceData, SequenceData> act = (SequenceData x, SequenceData y) =>
            {
                for (int k = 0; k < ListK.Count; k++)
                {
                    if (ListM.Count != 0)
                    {
                        for (int m = 0; m < ListM.Count; m++)
                        {
                            dy.CalDissimiliraty(x, y, result, ListK[k], ListM[m]);
                        }
                    }
                    else
                    {
                        dy.CalDissimiliraty(x, y, result, ListK[k]);
                    }
                }
            };

            SequenceData seqX = listSeqData[0];
            PreGetData(seqX);

            SequenceData seqTmp = listSeqData[1];
            PreGetData(seqTmp);

            act(seqX, seqTmp);
            Parallel.For(2, count, new ParallelOptions() { MaxDegreeOfParallelism = 4 }, (j) =>
            {
                //    for (int j = i + 1; j < count; j++)
                //{
                SequenceData seqY = listSeqData[j];
                act(seqX, seqY);
                // }
            });


            ShowState(pgb, txt, count - 1);



            saveDir += "\\" + node.FullPath;
            foreach (var fun in ListFun)
            {
                foreach (var k in ListK)
                {
                    if (ListM.Count != 0)
                    {
                        foreach (var m in ListM)
                        {
                            result.WriteResult(listSeqName, saveDir, fun, k, m);
                        }//foreach
                    }
                    else
                    {
                        result.WriteResult(listSeqName, saveDir, fun, k);
                    }
                }//foreach
            }//foreach
            listSeqData = null;
            dy = null;
            result = null;
        }

        /// <summary>
        /// 预先获取临时数据，ktuple,Markov
        /// </summary>
        /// <param name="sd">序列</param>
        private void PreGetData(SequenceData sd)
        {
            foreach (var k in ListK)
            {
                sd.CalKtupleCount(k);
                foreach (var m in ListM)
                {
                    sd.CalMarkov(k, m);
                }
                if (ListFun.Contains("Hao") && k > 3)
                {
                    sd.CalMarkov(k, k - 2);
                }
            }
        }

        /// <summary>
        /// 显示进度和状态
        /// </summary>
        /// <param name="pgb"></param>
        /// <param name="txt"></param>
        /// <param name="n"></param>
        private void ShowState(ProgressBar pgb, TextBox txt, int n)
        {
            if (pgb.Value + n < pgb.Maximum)
            {
                pgb.Value += n;
            }
            else
            {
                pgb.Value = pgb.Maximum;
            }

            txt.Text += Math.Round((pgb.Value * 1.0 / pgb.Maximum) * 100, 2).ToString() + "%\r\n";
        }
    }
}
