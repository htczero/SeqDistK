using System;
using System.IO;
using System.Windows.Forms;

namespace SeqDistK
{
    public partial class FrmDistance : Form
    {
        public FrmDistance()
        {
            InitializeComponent();
        }

        Operation op;

        private void Form1_Load(object sender, EventArgs e)
        {
            cbxk1.Tag = cbxk2;
            cbxm1.Tag = cbxm2;
            op = new Operation();
            int max = Environment.ProcessorCount;
            for (int i = 0; i < max; i++)
            {
                cbxCPU.Items.Add(i + 1);
            }
            cbxCPU.SelectedIndex = max / 2 - 1;
        }

        private void cbxk1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbx1 = (ComboBox)sender;
            ComboBox cbx2 = (ComboBox)cbx1.Tag;
            op.ComboBoxSetting(cbx1, cbx2);
        }

        private void myDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link; //重要代码：表明是链接类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }

        private void myDragDrop(object sender, DragEventArgs e)
        {
            Array arr = ((Array)e.Data.GetData(DataFormats.FileDrop));
            string[] arrPath = new string[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arrPath[i] = arr.GetValue(i).ToString();
            }
            if (sender is TreeView)
            {
                TreeView tre = (TreeView)sender;
                op.TreeViewAddNode(treLoad.Nodes, arrPath);
            }
            else
            {
                TextBox tbx = (TextBox)sender;

                if (arrPath.Length != 1)
                {
                    MessageBox.Show("Only one path can be tragged in", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (tbx.Name == "txtSave")
                {
                    if (!File.Exists(arrPath[0]))
                        tbx.Text = arrPath[0];
                    else
                    {
                        MessageBox.Show("Please input the directory", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                //else
                //{
                //    if (File.Exists(arrPath[0]))
                //        tbx.Text = arrPath[0];
                //    else
                //    {
                //        MessageBox.Show("请输入序列文件(⊙o⊙)哦", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    }//else
                //}//else
            }//else
        }
        private bool CheckSetting()
        {
            if (treLoad.Nodes.Count == 0)
            {
                MessageBox.Show("Please input sequence files", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (cbxk1.SelectedItem == null)
            {
                MessageBox.Show("The value of k can not be none", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (clbFun.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one ways to calculate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (lblM.Visible == true)
            {
                if (cbxm1.SelectedItem == null)
                {
                    MessageBox.Show("The value of M can not be none", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            if (txtSave.Text == "")
            {
                MessageBox.Show("Saveing path can not be none", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            GetParameter();
            return true;
        }

        private void btnOpern_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Name == "btnOpen")
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.ShowDialog();
                if (fbd.SelectedPath != null)
                {
                    txtSave.Text = fbd.SelectedPath;
                }
            }
            else
            {
                if (!CheckSetting())
                {
                    op.Inital();
                    return;
                }
                //op.ActShow = State;
                Operation._cpu = cbxCPU.SelectedIndex + 1;
                op.Star(txtSave.Text, treLoad.Nodes, pgb, txtSate, rbn.Checked);
                op = new Operation();
            }
        }

        private void GetParameter()
        {
            foreach (var item in clbFun.CheckedItems)
            {
                op.ListFun.Add(item.ToString());
            }
            op.GetValueFromComboBox(cbxk1, cbxk2, op.ListK);
            if (lblM.Visible == true)
                op.GetValueFromComboBox(cbxm1, cbxm2, op.ListM);
        }

        private void remove_Click(object sender, EventArgs e)
        {
            treLoad.SelectedNode.Remove();
        }

        private void clbFun_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in clbFun.CheckedItems)
            {
                string str = item.ToString();
                if (str == "D2S" || str == "D2Star")
                {
                    lblM.Visible = true;
                    cbxm1.Visible = cbxm2.Visible = lblToM.Visible = true;
                }
                else
                {
                    lblM.Visible = false;
                    cbxm1.Visible = cbxm2.Visible = lblToM.Visible = false;
                }
            }
        }

        private void State(int n, string text)
        {
            Action act = () =>
            {
                pgb.Value += n;
            };
            pgb.BeginInvoke(act);
            Action act2 = () =>
            {
                txtSate.Text += text;
            };
            txtSate.BeginInvoke(act2);
        }
    }
}
