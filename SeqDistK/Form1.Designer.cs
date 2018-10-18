namespace SeqDistK
{
    partial class FrmDistance
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDistance));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treLoad = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.remove = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtSate = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxCPU = new System.Windows.Forms.ComboBox();
            this.rbn = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.clbFun = new System.Windows.Forms.CheckedListBox();
            this.cbxm2 = new System.Windows.Forms.ComboBox();
            this.cbxk2 = new System.Windows.Forms.ComboBox();
            this.cbxm1 = new System.Windows.Forms.ComboBox();
            this.lblToM = new System.Windows.Forms.Label();
            this.cbxk1 = new System.Windows.Forms.ComboBox();
            this.lblM = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblK = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStar = new System.Windows.Forms.Button();
            this.pgb = new System.Windows.Forms.ProgressBar();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtSave = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(413, 681);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treLoad);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 681);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sequence";
            // 
            // treLoad
            // 
            this.treLoad.AllowDrop = true;
            this.treLoad.ContextMenuStrip = this.contextMenuStrip1;
            this.treLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treLoad.Location = new System.Drawing.Point(3, 19);
            this.treLoad.Name = "treLoad";
            this.treLoad.Size = new System.Drawing.Size(407, 659);
            this.treLoad.TabIndex = 0;
            this.treLoad.DragDrop += new System.Windows.Forms.DragEventHandler(this.myDragDrop);
            this.treLoad.DragEnter += new System.Windows.Forms.DragEventHandler(this.myDragEnter);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.remove});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(121, 26);
            // 
            // remove
            // 
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(120, 22);
            this.remove.Text = "remove";
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(413, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(851, 681);
            this.panel2.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtSate);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(447, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(404, 505);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "State";
            // 
            // txtSate
            // 
            this.txtSate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSate.Location = new System.Drawing.Point(3, 19);
            this.txtSate.Multiline = true;
            this.txtSate.Name = "txtSate";
            this.txtSate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSate.Size = new System.Drawing.Size(398, 483);
            this.txtSate.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cbxCPU);
            this.groupBox3.Controls.Add(this.rbn);
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Controls.Add(this.clbFun);
            this.groupBox3.Controls.Add(this.cbxm2);
            this.groupBox3.Controls.Add(this.cbxk2);
            this.groupBox3.Controls.Add(this.cbxm1);
            this.groupBox3.Controls.Add(this.lblToM);
            this.groupBox3.Controls.Add(this.cbxk1);
            this.groupBox3.Controls.Add(this.lblM);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.lblK);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(447, 505);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Setting";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(164, 431);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "CPU";
            // 
            // cbxCPU
            // 
            this.cbxCPU.FormattingEnabled = true;
            this.cbxCPU.Location = new System.Drawing.Point(200, 428);
            this.cbxCPU.Name = "cbxCPU";
            this.cbxCPU.Size = new System.Drawing.Size(95, 25);
            this.cbxCPU.TabIndex = 8;
            // 
            // rbn
            // 
            this.rbn.AutoSize = true;
            this.rbn.Location = new System.Drawing.Point(277, 370);
            this.rbn.Name = "rbn";
            this.rbn.Size = new System.Drawing.Size(63, 21);
            this.rbn.TabIndex = 7;
            this.rbn.Text = "1 to N";
            this.rbn.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(97, 370);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(66, 21);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "N to N";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // clbFun
            // 
            this.clbFun.CheckOnClick = true;
            this.clbFun.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clbFun.FormattingEnabled = true;
            this.clbFun.Items.AddRange(new object[] {
            "Eu",
            "Ma",
            "Ch",
            "D2",
            "Hao",
            "D2Star",
            "D2S"});
            this.clbFun.Location = new System.Drawing.Point(167, 180);
            this.clbFun.Name = "clbFun";
            this.clbFun.Size = new System.Drawing.Size(128, 172);
            this.clbFun.TabIndex = 3;
            this.clbFun.SelectedIndexChanged += new System.EventHandler(this.clbFun_SelectedIndexChanged);
            // 
            // cbxm2
            // 
            this.cbxm2.FormattingEnabled = true;
            this.cbxm2.Location = new System.Drawing.Point(260, 125);
            this.cbxm2.Name = "cbxm2";
            this.cbxm2.Size = new System.Drawing.Size(80, 25);
            this.cbxm2.TabIndex = 1;
            this.cbxm2.Visible = false;
            // 
            // cbxk2
            // 
            this.cbxk2.FormattingEnabled = true;
            this.cbxk2.Location = new System.Drawing.Point(260, 51);
            this.cbxk2.Name = "cbxk2";
            this.cbxk2.Size = new System.Drawing.Size(80, 25);
            this.cbxk2.TabIndex = 1;
            // 
            // cbxm1
            // 
            this.cbxm1.FormattingEnabled = true;
            this.cbxm1.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cbxm1.Location = new System.Drawing.Point(132, 125);
            this.cbxm1.Name = "cbxm1";
            this.cbxm1.Size = new System.Drawing.Size(80, 25);
            this.cbxm1.TabIndex = 1;
            this.cbxm1.Visible = false;
            this.cbxm1.SelectedIndexChanged += new System.EventHandler(this.cbxk1_SelectedIndexChanged);
            // 
            // lblToM
            // 
            this.lblToM.AutoSize = true;
            this.lblToM.Location = new System.Drawing.Point(227, 128);
            this.lblToM.Name = "lblToM";
            this.lblToM.Size = new System.Drawing.Size(23, 17);
            this.lblToM.TabIndex = 0;
            this.lblToM.Text = "To";
            this.lblToM.Visible = false;
            // 
            // cbxk1
            // 
            this.cbxk1.FormattingEnabled = true;
            this.cbxk1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cbxk1.Location = new System.Drawing.Point(132, 51);
            this.cbxk1.Name = "cbxk1";
            this.cbxk1.Size = new System.Drawing.Size(80, 25);
            this.cbxk1.TabIndex = 1;
            this.cbxk1.SelectedIndexChanged += new System.EventHandler(this.cbxk1_SelectedIndexChanged);
            // 
            // lblM
            // 
            this.lblM.AutoSize = true;
            this.lblM.Location = new System.Drawing.Point(94, 128);
            this.lblM.Name = "lblM";
            this.lblM.Size = new System.Drawing.Size(20, 17);
            this.lblM.TabIndex = 0;
            this.lblM.Text = "M";
            this.lblM.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "To";
            // 
            // lblK
            // 
            this.lblK.AutoSize = true;
            this.lblK.Location = new System.Drawing.Point(94, 54);
            this.lblK.Name = "lblK";
            this.lblK.Size = new System.Drawing.Size(15, 17);
            this.lblK.TabIndex = 0;
            this.lblK.Text = "k";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStar);
            this.groupBox2.Controls.Add(this.pgb);
            this.groupBox2.Controls.Add(this.btnOpen);
            this.groupBox2.Controls.Add(this.txtSave);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 505);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(851, 176);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Save";
            // 
            // btnStar
            // 
            this.btnStar.Location = new System.Drawing.Point(750, 85);
            this.btnStar.Name = "btnStar";
            this.btnStar.Size = new System.Drawing.Size(75, 23);
            this.btnStar.TabIndex = 3;
            this.btnStar.Text = "Star";
            this.btnStar.UseVisualStyleBackColor = true;
            this.btnStar.Click += new System.EventHandler(this.btnOpern_Click);
            // 
            // pgb
            // 
            this.pgb.Location = new System.Drawing.Point(25, 126);
            this.pgb.Name = "pgb";
            this.pgb.Size = new System.Drawing.Size(800, 23);
            this.pgb.TabIndex = 2;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(750, 38);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpern_Click);
            // 
            // txtSave
            // 
            this.txtSave.AllowDrop = true;
            this.txtSave.Location = new System.Drawing.Point(25, 38);
            this.txtSave.Name = "txtSave";
            this.txtSave.Size = new System.Drawing.Size(685, 23);
            this.txtSave.TabIndex = 0;
            this.txtSave.DragDrop += new System.Windows.Forms.DragEventHandler(this.myDragDrop);
            this.txtSave.DragEnter += new System.Windows.Forms.DragEventHandler(this.myDragEnter);
            // 
            // FrmDistance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDistance";
            this.Text = "SeqDistK v0.9";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treLoad;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox clbFun;
        private System.Windows.Forms.ComboBox cbxm2;
        private System.Windows.Forms.ComboBox cbxk2;
        private System.Windows.Forms.ComboBox cbxm1;
        private System.Windows.Forms.Label lblToM;
        private System.Windows.Forms.ComboBox cbxk1;
        private System.Windows.Forms.Label lblM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStar;
        private System.Windows.Forms.ProgressBar pgb;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtSave;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtSate;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem remove;
        private System.Windows.Forms.RadioButton rbn;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxCPU;
    }
}

