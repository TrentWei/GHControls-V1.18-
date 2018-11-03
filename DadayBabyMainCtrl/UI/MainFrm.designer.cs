namespace DadayBabyMainCtrl
{
    partial class MainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OntimInfCtl = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.SysNameCtl = new System.Windows.Forms.Label();
            this.ComNameCtl = new System.Windows.Forms.Label();
            this.timInfCtl = new System.Windows.Forms.Label();
            this.UsrInfCtl = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.btnEquFin = new System.Windows.Forms.Button();
            this.btnEquCancel = new System.Windows.Forms.Button();
            this.btnEquQuery = new System.Windows.Forms.Button();
            this.cmbEquCmdSno = new System.Windows.Forms.ComboBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.dgvCraneRgv = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.flushCmdBtn = new System.Windows.Forms.Button();
            this.cmbCmdSno = new System.Windows.Forms.ComboBox();
            this.modCmdBtn = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label107 = new System.Windows.Forms.Label();
            this.cStnNoCtl = new System.Windows.Forms.TextBox();
            this.label111 = new System.Windows.Forms.Label();
            this.cPriCtl = new System.Windows.Forms.TextBox();
            this.label112 = new System.Windows.Forms.Label();
            this.cTraceCtl = new System.Windows.Forms.TextBox();
            this.label113 = new System.Windows.Forms.Label();
            this.cmbCSts = new System.Windows.Forms.ComboBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CmdQueryBtn = new System.Windows.Forms.Button();
            this.cmbCmdMode = new System.Windows.Forms.ComboBox();
            this.CmdSnoCtl = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvCmdMst = new System.Windows.Forms.DataGridView();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.groupBox1.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCraneRgv)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCmdMst)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OntimInfCtl);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 238);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 194);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "即时信息";
            // 
            // OntimInfCtl
            // 
            this.OntimInfCtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OntimInfCtl.Location = new System.Drawing.Point(3, 17);
            this.OntimInfCtl.Name = "OntimInfCtl";
            this.OntimInfCtl.ReadOnly = true;
            this.OntimInfCtl.Size = new System.Drawing.Size(286, 174);
            this.OntimInfCtl.TabIndex = 0;
            this.OntimInfCtl.Text = "";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.groupBox1);
            this.groupBox11.Controls.Add(this.tabControl2);
            this.groupBox11.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox11.Location = new System.Drawing.Point(0, 74);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(298, 435);
            this.groupBox11.TabIndex = 6;
            this.groupBox11.TabStop = false;
            // 
            // SysNameCtl
            // 
            this.SysNameCtl.BackColor = System.Drawing.SystemColors.Control;
            this.SysNameCtl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SysNameCtl.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SysNameCtl.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.SysNameCtl.Location = new System.Drawing.Point(229, 10);
            this.SysNameCtl.Name = "SysNameCtl";
            this.SysNameCtl.Size = new System.Drawing.Size(722, 63);
            this.SysNameCtl.TabIndex = 4;
            this.SysNameCtl.Text = "SysInf";
            this.SysNameCtl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ComNameCtl
            // 
            this.ComNameCtl.BackColor = System.Drawing.SystemColors.Control;
            this.ComNameCtl.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ComNameCtl.Location = new System.Drawing.Point(997, 37);
            this.ComNameCtl.Name = "ComNameCtl";
            this.ComNameCtl.Size = new System.Drawing.Size(332, 35);
            this.ComNameCtl.TabIndex = 5;
            this.ComNameCtl.Text = "mirle";
            this.ComNameCtl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timInfCtl
            // 
            this.timInfCtl.BackColor = System.Drawing.SystemColors.Control;
            this.timInfCtl.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.timInfCtl.Location = new System.Drawing.Point(1053, 9);
            this.timInfCtl.Name = "timInfCtl";
            this.timInfCtl.Size = new System.Drawing.Size(226, 33);
            this.timInfCtl.TabIndex = 6;
            this.timInfCtl.Text = "date";
            this.timInfCtl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UsrInfCtl
            // 
            this.UsrInfCtl.BackColor = System.Drawing.SystemColors.Control;
            this.UsrInfCtl.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UsrInfCtl.Location = new System.Drawing.Point(8, 34);
            this.UsrInfCtl.Name = "UsrInfCtl";
            this.UsrInfCtl.Size = new System.Drawing.Size(168, 35);
            this.UsrInfCtl.TabIndex = 8;
            this.UsrInfCtl.Text = "欢迎您";
            this.UsrInfCtl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UsrInfCtl.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.UsrInfCtl);
            this.groupBox2.Controls.Add(this.timInfCtl);
            this.groupBox2.Controls.Add(this.ComNameCtl);
            this.groupBox2.Controls.Add(this.SysNameCtl);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1106, 74);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox15);
            this.tabPage3.Controls.Add(this.groupBox14);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(284, 195);
            this.tabPage3.TabIndex = 7;
            this.tabPage3.Text = "设备命令";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.cmbEquCmdSno);
            this.groupBox14.Controls.Add(this.btnEquQuery);
            this.groupBox14.Controls.Add(this.btnEquCancel);
            this.groupBox14.Controls.Add(this.btnEquFin);
            this.groupBox14.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox14.Location = new System.Drawing.Point(0, 0);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(284, 40);
            this.groupBox14.TabIndex = 1;
            this.groupBox14.TabStop = false;
            // 
            // btnEquFin
            // 
            this.btnEquFin.Location = new System.Drawing.Point(165, 12);
            this.btnEquFin.Name = "btnEquFin";
            this.btnEquFin.Size = new System.Drawing.Size(50, 23);
            this.btnEquFin.TabIndex = 4;
            this.btnEquFin.Tag = " ";
            this.btnEquFin.Text = "完 成";
            this.btnEquFin.UseVisualStyleBackColor = true;
            this.btnEquFin.Click += new System.EventHandler(this.btnEquFin_Click);
            // 
            // btnEquCancel
            // 
            this.btnEquCancel.Location = new System.Drawing.Point(229, 12);
            this.btnEquCancel.Name = "btnEquCancel";
            this.btnEquCancel.Size = new System.Drawing.Size(50, 23);
            this.btnEquCancel.TabIndex = 5;
            this.btnEquCancel.Tag = " ";
            this.btnEquCancel.Text = "取 消";
            this.btnEquCancel.UseVisualStyleBackColor = true;
            this.btnEquCancel.Click += new System.EventHandler(this.btnEquCancel_Click);
            // 
            // btnEquQuery
            // 
            this.btnEquQuery.Location = new System.Drawing.Point(101, 12);
            this.btnEquQuery.Name = "btnEquQuery";
            this.btnEquQuery.Size = new System.Drawing.Size(50, 23);
            this.btnEquQuery.TabIndex = 7;
            this.btnEquQuery.Text = "查 询";
            this.btnEquQuery.UseVisualStyleBackColor = true;
            this.btnEquQuery.Click += new System.EventHandler(this.btnEquQuery_Click);
            // 
            // cmbEquCmdSno
            // 
            this.cmbEquCmdSno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEquCmdSno.FormattingEnabled = true;
            this.cmbEquCmdSno.Location = new System.Drawing.Point(8, 13);
            this.cmbEquCmdSno.Name = "cmbEquCmdSno";
            this.cmbEquCmdSno.Size = new System.Drawing.Size(77, 20);
            this.cmbEquCmdSno.TabIndex = 8;
            this.cmbEquCmdSno.DropDown += new System.EventHandler(this.cmbEquCmdSno_DropDown);
            this.cmbEquCmdSno.SelectedIndexChanged += new System.EventHandler(this.cmbEquCmdSno_SelectedIndexChanged);
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.dgvCraneRgv);
            this.groupBox15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox15.Location = new System.Drawing.Point(0, 40);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(284, 155);
            this.groupBox15.TabIndex = 2;
            this.groupBox15.TabStop = false;
            // 
            // dgvCraneRgv
            // 
            this.dgvCraneRgv.AllowUserToAddRows = false;
            this.dgvCraneRgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvCraneRgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCraneRgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCraneRgv.Location = new System.Drawing.Point(3, 17);
            this.dgvCraneRgv.Name = "dgvCraneRgv";
            this.dgvCraneRgv.ReadOnly = true;
            this.dgvCraneRgv.RowHeadersVisible = false;
            this.dgvCraneRgv.RowTemplate.Height = 23;
            this.dgvCraneRgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCraneRgv.Size = new System.Drawing.Size(278, 135);
            this.dgvCraneRgv.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(284, 195);
            this.tabPage2.TabIndex = 9;
            this.tabPage2.Text = "命令维护";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.modCmdBtn);
            this.groupBox6.Controls.Add(this.cmbCmdSno);
            this.groupBox6.Controls.Add(this.flushCmdBtn);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox6.Location = new System.Drawing.Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(278, 48);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            // 
            // flushCmdBtn
            // 
            this.flushCmdBtn.Location = new System.Drawing.Point(216, 16);
            this.flushCmdBtn.Name = "flushCmdBtn";
            this.flushCmdBtn.Size = new System.Drawing.Size(55, 23);
            this.flushCmdBtn.TabIndex = 0;
            this.flushCmdBtn.Text = "刷 新";
            this.flushCmdBtn.UseVisualStyleBackColor = true;
            this.flushCmdBtn.Click += new System.EventHandler(this.flushCmdBtn_Click);
            // 
            // cmbCmdSno
            // 
            this.cmbCmdSno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCmdSno.FormattingEnabled = true;
            this.cmbCmdSno.Items.AddRange(new object[] {
            "0-待执行",
            "1-执行中"});
            this.cmbCmdSno.Location = new System.Drawing.Point(8, 17);
            this.cmbCmdSno.Name = "cmbCmdSno";
            this.cmbCmdSno.Size = new System.Drawing.Size(77, 20);
            this.cmbCmdSno.TabIndex = 2;
            this.cmbCmdSno.DropDown += new System.EventHandler(this.cmbCmdSno_DropDown);
            this.cmbCmdSno.TextChanged += new System.EventHandler(this.cmbCmdSno_TextChanged);
            // 
            // modCmdBtn
            // 
            this.modCmdBtn.Location = new System.Drawing.Point(132, 16);
            this.modCmdBtn.Name = "modCmdBtn";
            this.modCmdBtn.Size = new System.Drawing.Size(55, 23);
            this.modCmdBtn.TabIndex = 3;
            this.modCmdBtn.Text = "修 改";
            this.modCmdBtn.UseVisualStyleBackColor = true;
            this.modCmdBtn.Click += new System.EventHandler(this.modCmdBtn_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cmbCSts);
            this.groupBox5.Controls.Add(this.label113);
            this.groupBox5.Controls.Add(this.cTraceCtl);
            this.groupBox5.Controls.Add(this.label112);
            this.groupBox5.Controls.Add(this.cPriCtl);
            this.groupBox5.Controls.Add(this.label111);
            this.groupBox5.Controls.Add(this.cStnNoCtl);
            this.groupBox5.Controls.Add(this.label107);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 51);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(278, 141);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(148, 77);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(41, 12);
            this.label107.TabIndex = 2;
            this.label107.Text = "站口号";
            // 
            // cStnNoCtl
            // 
            this.cStnNoCtl.Location = new System.Drawing.Point(191, 71);
            this.cStnNoCtl.MaxLength = 5;
            this.cStnNoCtl.Name = "cStnNoCtl";
            this.cStnNoCtl.Size = new System.Drawing.Size(71, 21);
            this.cStnNoCtl.TabIndex = 3;
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(148, 32);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(41, 12);
            this.label111.TabIndex = 10;
            this.label111.Text = "优先级";
            // 
            // cPriCtl
            // 
            this.cPriCtl.Location = new System.Drawing.Point(191, 28);
            this.cPriCtl.MaxLength = 1;
            this.cPriCtl.Name = "cPriCtl";
            this.cPriCtl.Size = new System.Drawing.Size(71, 21);
            this.cPriCtl.TabIndex = 11;
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Location = new System.Drawing.Point(10, 76);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(41, 12);
            this.label112.TabIndex = 12;
            this.label112.Text = "流程码";
            // 
            // cTraceCtl
            // 
            this.cTraceCtl.Location = new System.Drawing.Point(55, 71);
            this.cTraceCtl.MaxLength = 2;
            this.cTraceCtl.Name = "cTraceCtl";
            this.cTraceCtl.Size = new System.Drawing.Size(77, 21);
            this.cTraceCtl.TabIndex = 13;
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Location = new System.Drawing.Point(10, 33);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(41, 12);
            this.label113.TabIndex = 14;
            this.label113.Text = "状  态";
            // 
            // cmbCSts
            // 
            this.cmbCSts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCSts.FormattingEnabled = true;
            this.cmbCSts.Items.AddRange(new object[] {
            "0-待执行",
            "1-执行中"});
            this.cmbCSts.Location = new System.Drawing.Point(55, 29);
            this.cmbCSts.Name = "cmbCSts";
            this.cmbCSts.Size = new System.Drawing.Size(77, 20);
            this.cmbCSts.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(284, 195);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "命令查询";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CmdSnoCtl);
            this.groupBox3.Controls.Add(this.cmbCmdMode);
            this.groupBox3.Controls.Add(this.CmdQueryBtn);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(278, 40);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // CmdQueryBtn
            // 
            this.CmdQueryBtn.Location = new System.Drawing.Point(204, 12);
            this.CmdQueryBtn.Name = "CmdQueryBtn";
            this.CmdQueryBtn.Size = new System.Drawing.Size(70, 23);
            this.CmdQueryBtn.TabIndex = 0;
            this.CmdQueryBtn.Text = "查 询";
            this.CmdQueryBtn.UseVisualStyleBackColor = true;
            this.CmdQueryBtn.Click += new System.EventHandler(this.CmdQueryBtn_Click);
            // 
            // cmbCmdMode
            // 
            this.cmbCmdMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCmdMode.FormattingEnabled = true;
            this.cmbCmdMode.Items.AddRange(new object[] {
            "0-作业模式",
            "1-入库作业",
            "2-出库作业",
            "3-盘点作业"});
            this.cmbCmdMode.Location = new System.Drawing.Point(7, 14);
            this.cmbCmdMode.Name = "cmbCmdMode";
            this.cmbCmdMode.Size = new System.Drawing.Size(88, 20);
            this.cmbCmdMode.TabIndex = 1;
            // 
            // CmdSnoCtl
            // 
            this.CmdSnoCtl.Location = new System.Drawing.Point(114, 14);
            this.CmdSnoCtl.Name = "CmdSnoCtl";
            this.CmdSnoCtl.Size = new System.Drawing.Size(80, 21);
            this.CmdSnoCtl.TabIndex = 2;
            this.CmdSnoCtl.Enter += new System.EventHandler(this.CmdSnoCtl_Enter);
            this.CmdSnoCtl.Leave += new System.EventHandler(this.CmdSnoCtl_Leave);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvCmdMst);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 43);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(278, 149);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            // 
            // dgvCmdMst
            // 
            this.dgvCmdMst.AllowUserToAddRows = false;
            this.dgvCmdMst.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvCmdMst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCmdMst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCmdMst.Location = new System.Drawing.Point(3, 17);
            this.dgvCmdMst.Name = "dgvCmdMst";
            this.dgvCmdMst.ReadOnly = true;
            this.dgvCmdMst.RowHeadersVisible = false;
            this.dgvCmdMst.RowTemplate.Height = 23;
            this.dgvCmdMst.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCmdMst.Size = new System.Drawing.Size(272, 129);
            this.dgvCmdMst.TabIndex = 0;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl2.Location = new System.Drawing.Point(3, 17);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(292, 221);
            this.tabControl2.TabIndex = 2;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1106, 509);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DadayBabyMainCtrl";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFrm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCraneRgv)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCmdMst)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox OntimInfCtl;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label SysNameCtl;
        private System.Windows.Forms.Label ComNameCtl;
        private System.Windows.Forms.Label timInfCtl;
        private System.Windows.Forms.Label UsrInfCtl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvCmdMst;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox CmdSnoCtl;
        private System.Windows.Forms.ComboBox cmbCmdMode;
        private System.Windows.Forms.Button CmdQueryBtn;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cmbCSts;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.TextBox cTraceCtl;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.TextBox cPriCtl;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.TextBox cStnNoCtl;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button modCmdBtn;
        private System.Windows.Forms.ComboBox cmbCmdSno;
        private System.Windows.Forms.Button flushCmdBtn;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.DataGridView dgvCraneRgv;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.ComboBox cmbEquCmdSno;
        private System.Windows.Forms.Button btnEquQuery;
        private System.Windows.Forms.Button btnEquCancel;
        private System.Windows.Forms.Button btnEquFin;


    }
}

