namespace BudgetEdit
{
    partial class MaterialSummaryForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("人工");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("其他材料");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("混合材料");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("自有周转材料");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("租赁周转材料");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("材料", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("自有机械");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("租赁机械");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("机械", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("所有工料机", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode6,
            treeNode9});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialSummaryForm));
            this.c1SplitContainer1 = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.c1SplitterPanel1 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.c1SplitterPanel2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.c1SplitContainer2 = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.c1SplitterPanel3 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.inventoryC1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.computeC1SplitterPanel = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.computC1FlexGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.computGridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).BeginInit();
            this.c1SplitContainer1.SuspendLayout();
            this.c1SplitterPanel1.SuspendLayout();
            this.c1SplitterPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer2)).BeginInit();
            this.c1SplitContainer2.SuspendLayout();
            this.c1SplitterPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryC1FlexGrid1)).BeginInit();
            this.computeC1SplitterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.computC1FlexGrid)).BeginInit();
            this.computGridContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1SplitContainer1
            // 
            this.c1SplitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.c1SplitContainer1.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(150)))));
            this.c1SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SplitContainer1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.c1SplitContainer1.HeaderHeight = 0;
            this.c1SplitContainer1.LineBelowHeader = false;
            this.c1SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.c1SplitContainer1.Name = "c1SplitContainer1";
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel1);
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel2);
            this.c1SplitContainer1.Size = new System.Drawing.Size(844, 742);
            this.c1SplitContainer1.SplitterWidth = 1;
            this.c1SplitContainer1.TabIndex = 0;
            // 
            // c1SplitterPanel1
            // 
            this.c1SplitterPanel1.Controls.Add(this.treeView1);
            this.c1SplitterPanel1.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.c1SplitterPanel1.Location = new System.Drawing.Point(0, 0);
            this.c1SplitterPanel1.Name = "c1SplitterPanel1";
            this.c1SplitterPanel1.Size = new System.Drawing.Size(192, 742);
            this.c1SplitterPanel1.SizeRatio = 22.776D;
            this.c1SplitterPanel1.TabIndex = 0;
            this.c1SplitterPanel1.Text = "面板 1";
            this.c1SplitterPanel1.Width = 192;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "人工";
            treeNode1.Text = "人工";
            treeNode2.Name = "其他材料";
            treeNode2.Text = "其他材料";
            treeNode3.Name = "混合材料";
            treeNode3.Text = "混合材料";
            treeNode4.Name = "自有周转材料";
            treeNode4.Text = "自有周转材料";
            treeNode5.Name = "租赁周转材料";
            treeNode5.Text = "租赁周转材料";
            treeNode6.Name = "材料";
            treeNode6.Text = "材料";
            treeNode7.Name = "自有机械";
            treeNode7.Text = "自有机械";
            treeNode8.Name = "租赁机械";
            treeNode8.Text = "租赁机械";
            treeNode9.Name = "机械";
            treeNode9.Text = "机械";
            treeNode10.Name = "所有工料机";
            treeNode10.Text = "所有工料机";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode10});
            this.treeView1.Size = new System.Drawing.Size(192, 742);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // c1SplitterPanel2
            // 
            this.c1SplitterPanel2.Controls.Add(this.c1SplitContainer2);
            this.c1SplitterPanel2.Height = 100;
            this.c1SplitterPanel2.Location = new System.Drawing.Point(193, 0);
            this.c1SplitterPanel2.Name = "c1SplitterPanel2";
            this.c1SplitterPanel2.Size = new System.Drawing.Size(651, 742);
            this.c1SplitterPanel2.TabIndex = 1;
            this.c1SplitterPanel2.Text = "面板 2";
            // 
            // c1SplitContainer2
            // 
            this.c1SplitContainer2.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(150)))));
            this.c1SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SplitContainer2.HeaderHeight = 20;
            this.c1SplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.c1SplitContainer2.Name = "c1SplitContainer2";
            this.c1SplitContainer2.Panels.Add(this.c1SplitterPanel3);
            this.c1SplitContainer2.Panels.Add(this.computeC1SplitterPanel);
            this.c1SplitContainer2.Size = new System.Drawing.Size(651, 742);
            this.c1SplitContainer2.SplitterWidth = 2;
            this.c1SplitContainer2.TabIndex = 0;
            // 
            // c1SplitterPanel3
            // 
            this.c1SplitterPanel3.Collapsible = true;
            this.c1SplitterPanel3.Controls.Add(this.inventoryC1FlexGrid1);
            this.c1SplitterPanel3.Height = 585;
            this.c1SplitterPanel3.Location = new System.Drawing.Point(0, 0);
            this.c1SplitterPanel3.Name = "c1SplitterPanel3";
            this.c1SplitterPanel3.Size = new System.Drawing.Size(651, 578);
            this.c1SplitterPanel3.SizeRatio = 79.054D;
            this.c1SplitterPanel3.TabIndex = 0;
            // 
            // inventoryC1FlexGrid1
            // 
            this.inventoryC1FlexGrid1.ColumnInfo = resources.GetString("inventoryC1FlexGrid1.ColumnInfo");
            this.inventoryC1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inventoryC1FlexGrid1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.inventoryC1FlexGrid1.Location = new System.Drawing.Point(0, 0);
            this.inventoryC1FlexGrid1.Name = "inventoryC1FlexGrid1";
            this.inventoryC1FlexGrid1.Rows.Count = 1;
            this.inventoryC1FlexGrid1.Rows.DefaultSize = 25;
            this.inventoryC1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.inventoryC1FlexGrid1.Size = new System.Drawing.Size(651, 578);
            this.inventoryC1FlexGrid1.StyleInfo = resources.GetString("inventoryC1FlexGrid1.StyleInfo");
            this.inventoryC1FlexGrid1.TabIndex = 0;
            this.inventoryC1FlexGrid1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.inventoryC1FlexGrid1_MouseClick);
            // 
            // computeC1SplitterPanel
            // 
            this.computeC1SplitterPanel.Collapsible = true;
            this.computeC1SplitterPanel.Controls.Add(this.computC1FlexGrid);
            this.computeC1SplitterPanel.Height = 100;
            this.computeC1SplitterPanel.Location = new System.Drawing.Point(0, 607);
            this.computeC1SplitterPanel.Name = "computeC1SplitterPanel";
            this.computeC1SplitterPanel.Size = new System.Drawing.Size(651, 135);
            this.computeC1SplitterPanel.TabIndex = 1;
            this.computeC1SplitterPanel.Text = "面板 2";
            this.computeC1SplitterPanel.Visible = false;
            // 
            // computC1FlexGrid
            // 
            this.computC1FlexGrid.ColumnInfo = "10,1,0,0,0,125,Columns:";
            this.computC1FlexGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.computC1FlexGrid.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.computC1FlexGrid.Location = new System.Drawing.Point(0, 0);
            this.computC1FlexGrid.Name = "computC1FlexGrid";
            this.computC1FlexGrid.Rows.Count = 10;
            this.computC1FlexGrid.Rows.DefaultSize = 25;
            this.computC1FlexGrid.Rows.Fixed = 2;
            this.computC1FlexGrid.Size = new System.Drawing.Size(651, 135);
            this.computC1FlexGrid.StyleInfo = resources.GetString("computC1FlexGrid.StyleInfo");
            this.computC1FlexGrid.TabIndex = 0;
            this.computC1FlexGrid.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.computC1FlexGrid_AfterEdit);
            this.computC1FlexGrid.ValidateEdit += new C1.Win.C1FlexGrid.ValidateEditEventHandler(this.computC1FlexGrid_ValidateEdit);
            this.computC1FlexGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.computC1FlexGrid_MouseClick);
            // 
            // computGridContextMenuStrip
            // 
            this.computGridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRowToolStripMenuItem,
            this.deleteRowToolStripMenuItem});
            this.computGridContextMenuStrip.Name = "computGridContextMenuStrip";
            this.computGridContextMenuStrip.Size = new System.Drawing.Size(113, 48);
            // 
            // addRowToolStripMenuItem
            // 
            this.addRowToolStripMenuItem.Name = "addRowToolStripMenuItem";
            this.addRowToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.addRowToolStripMenuItem.Text = "增加行";
            this.addRowToolStripMenuItem.Click += new System.EventHandler(this.addRowToolStripMenuItem_Click);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.deleteRowToolStripMenuItem.Text = "删除行";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // MaterialSummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 742);
            this.Controls.Add(this.c1SplitContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MaterialSummaryForm";
            this.Text = "MaterialSummaryForm";
            this.Shown += new System.EventHandler(this.MaterialSummaryForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).EndInit();
            this.c1SplitContainer1.ResumeLayout(false);
            this.c1SplitterPanel1.ResumeLayout(false);
            this.c1SplitterPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer2)).EndInit();
            this.c1SplitContainer2.ResumeLayout(false);
            this.c1SplitterPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inventoryC1FlexGrid1)).EndInit();
            this.computeC1SplitterPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.computC1FlexGrid)).EndInit();
            this.computGridContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1SplitContainer.C1SplitContainer c1SplitContainer1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel1;
        private System.Windows.Forms.TreeView treeView1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel2;
        private C1.Win.C1SplitContainer.C1SplitContainer c1SplitContainer2;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel3;
        private C1.Win.C1FlexGrid.C1FlexGrid inventoryC1FlexGrid1;
        private C1.Win.C1SplitContainer.C1SplitterPanel computeC1SplitterPanel;
        private C1.Win.C1FlexGrid.C1FlexGrid computC1FlexGrid;
        private System.Windows.Forms.ContextMenuStrip computGridContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;

    }
}