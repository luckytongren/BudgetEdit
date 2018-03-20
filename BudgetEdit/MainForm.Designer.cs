namespace BudgetEdit
{
    partial class MainForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("项目根节点");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("100章");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("200章");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("300章");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("400章");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("500章");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("600章");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("700章");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("800章");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("900章");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("劳务分包费指标", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("人才机");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("材料");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("机械");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("周转材料");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("工料机库", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15});
            this.tlpAllForm = new System.Windows.Forms.TableLayoutPanel();
            this.btMaterial = new System.Windows.Forms.Button();
            this.btIndexBase = new System.Windows.Forms.Button();
            this.btProjectNavigation = new System.Windows.Forms.Button();
            this.c1SplitContainer1 = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.c1spNavigation = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.tvProjectNavigation = new System.Windows.Forms.TreeView();
            this.c1SplitterPanel2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.c1spIndexBase = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.IndexTreeView = new System.Windows.Forms.TreeView();
            this.IndexLibraryC1FlexGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.c1spMaterial = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.materialLibraryC1FlexGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.materialTreeView = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.维护BToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newBidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.projectManToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteProjecttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.BidEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bidManToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteBidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.importInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exeportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialImportContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.materialImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialSelectContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.returnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpAllForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).BeginInit();
            this.c1SplitContainer1.SuspendLayout();
            this.c1spNavigation.SuspendLayout();
            this.c1SplitterPanel2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.c1spIndexBase.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IndexLibraryC1FlexGrid)).BeginInit();
            this.c1spMaterial.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.materialLibraryC1FlexGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.importContextMenuStrip.SuspendLayout();
            this.materialImportContextMenuStrip.SuspendLayout();
            this.materialSelectContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAllForm
            // 
            this.tlpAllForm.ColumnCount = 6;
            this.tlpAllForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tlpAllForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tlpAllForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAllForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tlpAllForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tlpAllForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tlpAllForm.Controls.Add(this.btMaterial, 5, 1);
            this.tlpAllForm.Controls.Add(this.btIndexBase, 5, 0);
            this.tlpAllForm.Controls.Add(this.btProjectNavigation, 0, 0);
            this.tlpAllForm.Controls.Add(this.c1SplitContainer1, 1, 0);
            this.tlpAllForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAllForm.Location = new System.Drawing.Point(0, 34);
            this.tlpAllForm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tlpAllForm.Name = "tlpAllForm";
            this.tlpAllForm.RowCount = 3;
            this.tlpAllForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAllForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpAllForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpAllForm.Size = new System.Drawing.Size(1245, 716);
            this.tlpAllForm.TabIndex = 0;
            // 
            // btMaterial
            // 
            this.btMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btMaterial.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btMaterial.Location = new System.Drawing.Point(1203, 366);
            this.btMaterial.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.btMaterial.Name = "btMaterial";
            this.tlpAllForm.SetRowSpan(this.btMaterial, 2);
            this.btMaterial.Size = new System.Drawing.Size(37, 342);
            this.btMaterial.TabIndex = 10;
            this.btMaterial.Text = "工料机库";
            this.btMaterial.UseVisualStyleBackColor = true;
            this.btMaterial.Click += new System.EventHandler(this.btMaterial_Click);
            // 
            // btIndexBase
            // 
            this.btIndexBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btIndexBase.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btIndexBase.Location = new System.Drawing.Point(1203, 8);
            this.btIndexBase.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.btIndexBase.Name = "btIndexBase";
            this.btIndexBase.Size = new System.Drawing.Size(37, 342);
            this.btIndexBase.TabIndex = 9;
            this.btIndexBase.Text = "我的指标库";
            this.btIndexBase.UseVisualStyleBackColor = true;
            this.btIndexBase.Click += new System.EventHandler(this.btIndexBase_Click);
            // 
            // btProjectNavigation
            // 
            this.btProjectNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btProjectNavigation.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btProjectNavigation.Location = new System.Drawing.Point(5, 8);
            this.btProjectNavigation.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.btProjectNavigation.Name = "btProjectNavigation";
            this.btProjectNavigation.Size = new System.Drawing.Size(37, 342);
            this.btProjectNavigation.TabIndex = 1;
            this.btProjectNavigation.Text = "项目导航";
            this.btProjectNavigation.UseVisualStyleBackColor = true;
            this.btProjectNavigation.Click += new System.EventHandler(this.btProjectNavigation_Click);
            // 
            // c1SplitContainer1
            // 
            this.c1SplitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.c1SplitContainer1.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(150)))));
            this.tlpAllForm.SetColumnSpan(this.c1SplitContainer1, 4);
            this.c1SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SplitContainer1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.c1SplitContainer1.HeaderHeight = 0;
            this.c1SplitContainer1.LineBelowHeader = false;
            this.c1SplitContainer1.Location = new System.Drawing.Point(51, 5);
            this.c1SplitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.c1SplitContainer1.Name = "c1SplitContainer1";
            this.c1SplitContainer1.Panels.Add(this.c1spNavigation);
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel2);
            this.c1SplitContainer1.Panels.Add(this.c1spIndexBase);
            this.c1SplitContainer1.Panels.Add(this.c1spMaterial);
            this.tlpAllForm.SetRowSpan(this.c1SplitContainer1, 3);
            this.c1SplitContainer1.Size = new System.Drawing.Size(1143, 706);
            this.c1SplitContainer1.TabIndex = 0;
            // 
            // c1spNavigation
            // 
            this.c1spNavigation.Collapsible = true;
            this.c1spNavigation.Controls.Add(this.tvProjectNavigation);
            this.c1spNavigation.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.c1spNavigation.Location = new System.Drawing.Point(0, 0);
            this.c1spNavigation.Name = "c1spNavigation";
            this.c1spNavigation.Size = new System.Drawing.Size(164, 706);
            this.c1spNavigation.SizeRatio = 15D;
            this.c1spNavigation.TabIndex = 0;
            this.c1spNavigation.Text = "面板 1";
            this.c1spNavigation.Visible = false;
            this.c1spNavigation.Width = 171;
            // 
            // tvProjectNavigation
            // 
            this.tvProjectNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvProjectNavigation.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvProjectNavigation.Location = new System.Drawing.Point(0, 0);
            this.tvProjectNavigation.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.tvProjectNavigation.Name = "tvProjectNavigation";
            treeNode1.Name = "root";
            treeNode1.Text = "项目根节点";
            this.tvProjectNavigation.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tvProjectNavigation.Size = new System.Drawing.Size(164, 706);
            this.tvProjectNavigation.TabIndex = 4;
            this.tvProjectNavigation.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvProjectNavigation_AfterSelect);
            this.tvProjectNavigation.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvProjectNavigation_MouseClick);
            // 
            // c1SplitterPanel2
            // 
            this.c1SplitterPanel2.Collapsible = true;
            this.c1SplitterPanel2.Controls.Add(this.tabControl2);
            this.c1SplitterPanel2.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.c1SplitterPanel2.Location = new System.Drawing.Point(175, 0);
            this.c1SplitterPanel2.Name = "c1SplitterPanel2";
            this.c1SplitterPanel2.Size = new System.Drawing.Size(764, 706);
            this.c1SplitterPanel2.SizeRatio = 80D;
            this.c1SplitterPanel2.TabIndex = 1;
            this.c1SplitterPanel2.Text = "面板 2";
            this.c1SplitterPanel2.Width = 771;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Controls.Add(this.tabPage8);
            this.tabControl2.Controls.Add(this.tabPage9);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(764, 706);
            this.tabControl2.TabIndex = 6;
            this.tabControl2.Visible = false;
            this.tabControl2.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl2_Selected);
            this.tabControl2.VisibleChanged += new System.EventHandler(this.tabControl2_VisibleChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.tabPage3.Size = new System.Drawing.Size(756, 673);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "取费与汇总";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.tabPage4.Size = new System.Drawing.Size(756, 673);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "直接工程费";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(756, 673);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "工料机汇总";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 29);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(756, 673);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "其他工程费";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 29);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(756, 673);
            this.tabPage7.TabIndex = 4;
            this.tabPage7.Text = "现场费";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Location = new System.Drawing.Point(4, 29);
            this.tabPage8.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(756, 673);
            this.tabPage8.TabIndex = 5;
            this.tabPage8.Text = "税金及变更索赔目标";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabPage9
            // 
            this.tabPage9.Location = new System.Drawing.Point(4, 29);
            this.tabPage9.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(756, 673);
            this.tabPage9.TabIndex = 6;
            this.tabPage9.Text = "报表";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // c1spIndexBase
            // 
            this.c1spIndexBase.Controls.Add(this.tableLayoutPanel1);
            this.c1spIndexBase.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.c1spIndexBase.Location = new System.Drawing.Point(950, 0);
            this.c1spIndexBase.Name = "c1spIndexBase";
            this.c1spIndexBase.Size = new System.Drawing.Size(94, 706);
            this.c1spIndexBase.TabIndex = 2;
            this.c1spIndexBase.Text = "面板 3";
            this.c1spIndexBase.Width = 94;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.IndexTreeView, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.IndexLibraryC1FlexGrid, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(94, 706);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // IndexTreeView
            // 
            this.IndexTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IndexTreeView.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IndexTreeView.Location = new System.Drawing.Point(4, 61);
            this.IndexTreeView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.IndexTreeView.Name = "IndexTreeView";
            treeNode2.Name = "1";
            treeNode2.Text = "100章";
            treeNode3.Name = "2";
            treeNode3.Text = "200章";
            treeNode4.Name = "3";
            treeNode4.Text = "300章";
            treeNode5.Name = "4";
            treeNode5.Text = "400章";
            treeNode6.Name = "5";
            treeNode6.Text = "500章";
            treeNode7.Name = "6";
            treeNode7.Text = "600章";
            treeNode8.Name = "7";
            treeNode8.Text = "700章";
            treeNode9.Name = "8";
            treeNode9.Text = "800章";
            treeNode10.Name = "9";
            treeNode10.Text = "900章";
            treeNode11.Name = "0";
            treeNode11.Text = "劳务分包费指标";
            this.IndexTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode11});
            this.IndexTreeView.Size = new System.Drawing.Size(86, 201);
            this.IndexTreeView.TabIndex = 8;
            this.IndexTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.IndexTreeView_NodeMouseClick);
            // 
            // IndexLibraryC1FlexGrid
            // 
            this.IndexLibraryC1FlexGrid.AllowEditing = false;
            this.IndexLibraryC1FlexGrid.ColumnInfo = "6,1,0,0,0,125,Columns:1{Name:\"细目号\";}\t2{Name:\"细目名称\";}\t3{Name:\"单位\";}\t4{Name:\"工作内容\";" +
                "}\t5{Name:\"单价\";}\t";
            this.IndexLibraryC1FlexGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IndexLibraryC1FlexGrid.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.IndexLibraryC1FlexGrid.Location = new System.Drawing.Point(5, 275);
            this.IndexLibraryC1FlexGrid.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.IndexLibraryC1FlexGrid.Name = "IndexLibraryC1FlexGrid";
            this.IndexLibraryC1FlexGrid.Rows.DefaultSize = 25;
            this.IndexLibraryC1FlexGrid.Size = new System.Drawing.Size(84, 423);
            this.IndexLibraryC1FlexGrid.StyleInfo = resources.GetString("IndexLibraryC1FlexGrid.StyleInfo");
            this.IndexLibraryC1FlexGrid.TabIndex = 7;
            this.IndexLibraryC1FlexGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.IndexLibraryC1FlexGrid_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 21);
            this.label1.TabIndex = 9;
            this.label1.Text = "我的指标库";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(4, 26);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(86, 28);
            this.comboBox1.TabIndex = 10;
            // 
            // c1spMaterial
            // 
            this.c1spMaterial.Controls.Add(this.tableLayoutPanel2);
            this.c1spMaterial.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.c1spMaterial.Location = new System.Drawing.Point(1048, 0);
            this.c1spMaterial.Name = "c1spMaterial";
            this.c1spMaterial.Size = new System.Drawing.Size(95, 706);
            this.c1spMaterial.TabIndex = 3;
            this.c1spMaterial.Text = "面板 4";
            this.c1spMaterial.Width = 200;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.materialLibraryC1FlexGrid, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.comboBox2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.materialTreeView, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(95, 706);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "工料机库";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // materialLibraryC1FlexGrid
            // 
            this.materialLibraryC1FlexGrid.ColumnInfo = "10,1,0,0,0,125,Columns:1{Name:\"编号\";}\t2{Name:\"名称\";}\t3{Name:\"规格\";}\t4{Name:\"单位\";}\t5{" +
                "Name:\"单价\";}\t6{Width:0;Name:\"材料类型\";}\t";
            this.materialLibraryC1FlexGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLibraryC1FlexGrid.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.materialLibraryC1FlexGrid.Location = new System.Drawing.Point(4, 272);
            this.materialLibraryC1FlexGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.materialLibraryC1FlexGrid.Name = "materialLibraryC1FlexGrid";
            this.materialLibraryC1FlexGrid.Rows.DefaultSize = 25;
            this.materialLibraryC1FlexGrid.Size = new System.Drawing.Size(87, 429);
            this.materialLibraryC1FlexGrid.StyleInfo = resources.GetString("materialLibraryC1FlexGrid.StyleInfo");
            this.materialLibraryC1FlexGrid.TabIndex = 2;
            this.materialLibraryC1FlexGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.materialLibraryC1FlexGrid_MouseClick);
            this.materialLibraryC1FlexGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.materialLibraryC1FlexGrid_MouseDoubleClick);
            // 
            // comboBox2
            // 
            this.comboBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(4, 26);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(87, 28);
            this.comboBox2.TabIndex = 3;
            // 
            // materialTreeView
            // 
            this.materialTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialTreeView.Location = new System.Drawing.Point(4, 61);
            this.materialTreeView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.materialTreeView.Name = "materialTreeView";
            treeNode12.Name = "人才机";
            treeNode12.Text = "人才机";
            treeNode13.Name = "材料";
            treeNode13.Text = "材料";
            treeNode14.Name = "机械";
            treeNode14.Text = "机械";
            treeNode15.Name = "周转材料";
            treeNode15.Text = "周转材料";
            treeNode16.Name = "0";
            treeNode16.Text = "工料机库";
            this.materialTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode16});
            this.materialTreeView.Size = new System.Drawing.Size(87, 201);
            this.materialTreeView.TabIndex = 4;
            this.materialTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.materialTreeView_NodeMouseClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.维护BToolStripMenuItem,
            this.工具TToolStripMenuItem,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(11, 5, 0, 5);
            this.menuStrip1.Size = new System.Drawing.Size(1245, 34);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeyDisplayString = "";
            this.toolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.toolStripMenuItem1.Size = new System.Drawing.Size(66, 24);
            this.toolStripMenuItem1.Text = "文件(F)";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(67, 24);
            this.toolStripMenuItem2.Text = "编辑(E)";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(96, 24);
            this.toolStripMenuItem3.Text = "项目文件(P)";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(97, 24);
            this.toolStripMenuItem4.Text = "单价文件(U)";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(68, 24);
            this.toolStripMenuItem5.Text = "清单(B)";
            // 
            // 维护BToolStripMenuItem
            // 
            this.维护BToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.维护BToolStripMenuItem.Name = "维护BToolStripMenuItem";
            this.维护BToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.维护BToolStripMenuItem.Text = "维护(M)";
            // 
            // 工具TToolStripMenuItem
            // 
            this.工具TToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.工具TToolStripMenuItem.Name = "工具TToolStripMenuItem";
            this.工具TToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.工具TToolStripMenuItem.Text = "工具(T)";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(73, 24);
            this.toolStripMenuItem6.Text = "窗口(W)";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(70, 24);
            this.toolStripMenuItem7.Text = "帮助(H)";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.newProjectToolStripMenuItem.Text = "新建项目";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newBidToolStripMenuItem,
            this.toolStripSeparator2,
            this.projectManToolStripMenuItem,
            this.deleteProjecttoolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip2";
            this.contextMenuStrip.Size = new System.Drawing.Size(125, 76);
            // 
            // newBidToolStripMenuItem
            // 
            this.newBidToolStripMenuItem.Name = "newBidToolStripMenuItem";
            this.newBidToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.newBidToolStripMenuItem.Text = "新建标段";
            this.newBidToolStripMenuItem.Click += new System.EventHandler(this.newBidToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(121, 6);
            // 
            // projectManToolStripMenuItem
            // 
            this.projectManToolStripMenuItem.Name = "projectManToolStripMenuItem";
            this.projectManToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.projectManToolStripMenuItem.Text = "项目管理";
            this.projectManToolStripMenuItem.Visible = false;
            this.projectManToolStripMenuItem.Click += new System.EventHandler(this.projectManToolStripMenuItem_Click);
            // 
            // deleteProjecttoolStripMenuItem
            // 
            this.deleteProjecttoolStripMenuItem.Name = "deleteProjecttoolStripMenuItem";
            this.deleteProjecttoolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.deleteProjecttoolStripMenuItem.Text = "删除项目";
            this.deleteProjecttoolStripMenuItem.Click += new System.EventHandler(this.deleteProjecttoolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BidEditToolStripMenuItem,
            this.toolStripSeparator3,
            this.bidManToolStripMenuItem,
            this.deleteBidToolStripMenuItem,
            this.toolStripSeparator1,
            this.importInventoryToolStripMenuItem,
            this.exeportToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(125, 126);
            // 
            // BidEditToolStripMenuItem
            // 
            this.BidEditToolStripMenuItem.Name = "BidEditToolStripMenuItem";
            this.BidEditToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.BidEditToolStripMenuItem.Text = "预算编制";
            this.BidEditToolStripMenuItem.Click += new System.EventHandler(this.BidEditToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(121, 6);
            // 
            // bidManToolStripMenuItem
            // 
            this.bidManToolStripMenuItem.Name = "bidManToolStripMenuItem";
            this.bidManToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.bidManToolStripMenuItem.Text = "标段管理";
            this.bidManToolStripMenuItem.Visible = false;
            this.bidManToolStripMenuItem.Click += new System.EventHandler(this.bidManToolStripMenuItem_Click);
            // 
            // deleteBidToolStripMenuItem
            // 
            this.deleteBidToolStripMenuItem.Name = "deleteBidToolStripMenuItem";
            this.deleteBidToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.deleteBidToolStripMenuItem.Text = "删除标段";
            this.deleteBidToolStripMenuItem.Click += new System.EventHandler(this.deleteBidToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // importInventoryToolStripMenuItem
            // 
            this.importInventoryToolStripMenuItem.Name = "importInventoryToolStripMenuItem";
            this.importInventoryToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.importInventoryToolStripMenuItem.Text = "导入清单";
            this.importInventoryToolStripMenuItem.Click += new System.EventHandler(this.importInventoryToolStripMenuItem_Click);
            // 
            // exeportToolStripMenuItem
            // 
            this.exeportToolStripMenuItem.Name = "exeportToolStripMenuItem";
            this.exeportToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.exeportToolStripMenuItem.Text = "导出清单";
            this.exeportToolStripMenuItem.Click += new System.EventHandler(this.exeportToolStripMenuItem_Click);
            // 
            // importContextMenuStrip
            // 
            this.importContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imPortToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.importContextMenuStrip.Name = "importContextMenuStrip";
            this.importContextMenuStrip.Size = new System.Drawing.Size(137, 48);
            // 
            // imPortToolStripMenuItem
            // 
            this.imPortToolStripMenuItem.Name = "imPortToolStripMenuItem";
            this.imPortToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.imPortToolStripMenuItem.Text = "导入指标库";
            this.imPortToolStripMenuItem.Click += new System.EventHandler(this.imPortToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.exportToolStripMenuItem.Text = "导出指标库";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // materialImportContextMenuStrip
            // 
            this.materialImportContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.materialImportToolStripMenuItem,
            this.materialExportToolStripMenuItem});
            this.materialImportContextMenuStrip.Name = "materialImportContextMenuStrip";
            this.materialImportContextMenuStrip.Size = new System.Drawing.Size(149, 48);
            // 
            // materialImportToolStripMenuItem
            // 
            this.materialImportToolStripMenuItem.Name = "materialImportToolStripMenuItem";
            this.materialImportToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.materialImportToolStripMenuItem.Text = "导入工料机库";
            this.materialImportToolStripMenuItem.Click += new System.EventHandler(this.materialImportToolStripMenuItem_Click);
            // 
            // materialExportToolStripMenuItem
            // 
            this.materialExportToolStripMenuItem.Name = "materialExportToolStripMenuItem";
            this.materialExportToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.materialExportToolStripMenuItem.Text = "导出工料机库";
            // 
            // materialSelectContextMenuStrip
            // 
            this.materialSelectContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectToolStripMenuItem,
            this.returnToolStripMenuItem});
            this.materialSelectContextMenuStrip.Name = "contextMenuStrip3";
            this.materialSelectContextMenuStrip.Size = new System.Drawing.Size(113, 48);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.selectToolStripMenuItem.Text = "选择行";
            // 
            // returnToolStripMenuItem
            // 
            this.returnToolStripMenuItem.Name = "returnToolStripMenuItem";
            this.returnToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.returnToolStripMenuItem.Text = "返回";
            this.returnToolStripMenuItem.Click += new System.EventHandler(this.returnToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 750);
            this.Controls.Add(this.tlpAllForm);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "标后预算编制系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.tlpAllForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).EndInit();
            this.c1SplitContainer1.ResumeLayout(false);
            this.c1spNavigation.ResumeLayout(false);
            this.c1SplitterPanel2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.c1spIndexBase.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IndexLibraryC1FlexGrid)).EndInit();
            this.c1spMaterial.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.materialLibraryC1FlexGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.importContextMenuStrip.ResumeLayout(false);
            this.materialImportContextMenuStrip.ResumeLayout(false);
            this.materialSelectContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAllForm;
        private C1.Win.C1SplitContainer.C1SplitContainer c1SplitContainer1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1spNavigation;
        private System.Windows.Forms.TreeView tvProjectNavigation;
        private System.Windows.Forms.Button btProjectNavigation;
        private System.Windows.Forms.Button btMaterial;
        private System.Windows.Forms.Button btIndexBase;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel2;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1spIndexBase;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1spMaterial;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem 维护BToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem newBidToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem importInventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteProjecttoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteBidToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem BidEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exeportToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip importContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem imPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem bidManToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem projectManToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView IndexTreeView;
        private C1.Win.C1FlexGrid.C1FlexGrid IndexLibraryC1FlexGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1FlexGrid.C1FlexGrid materialLibraryC1FlexGrid;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TreeView materialTreeView;
        private System.Windows.Forms.ContextMenuStrip materialImportContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem materialImportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem materialExportToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip materialSelectContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem returnToolStripMenuItem;



    }
}