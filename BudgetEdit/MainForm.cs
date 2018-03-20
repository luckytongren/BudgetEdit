using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Tools;
using System.Data.OleDb;
using C1.Win.C1FlexGrid;


namespace BudgetEdit
{
    public partial class MainForm : Form
    {
        GetFeeForm frmGetFee = null;
        public DirectProFeeForm frmDirectProFee = null;
        LocalFeeForm frmLocalFee = null;
        MaterialSummaryForm frmMaterialSummary = null;
        OtherFeeForm frmOtherFeeForm = null;
        ReportForm frmReport = null;
        TaxForm frmTax = null;
        ProjectManForm frmProjectMan = null;
        BidManForm frmBidMan = null;
        WelcomeForm frmWelcome = null;
        bool navigationCtrl = true;
        bool indexBaseCtrl = false;
        bool materialCtrl = false;
        TreeNode node = null;
        bool displayFlag = true;//true--显示标段信息，false--显示预算编制
        bool addOrSelectFlag = true; //true-选择，false--添加
        public MainForm()
        {
            InitializeComponent();
            c1spNavigation.Visible = true;
            c1spMaterial.Visible = false;
            c1spIndexBase.Visible = false;
        }

        private void btProjectNavigation_Click(object sender, EventArgs e)
        {
            navigationCtrl = navigationCtrl ? false : true;
            c1spNavigation.Visible = navigationCtrl;
            tlpAllForm.Refresh();
        }

        private void btIndexBase_Click(object sender, EventArgs e)
        {
            indexBaseCtrl = indexBaseCtrl ? false : true;
            c1spIndexBase.Visible = indexBaseCtrl;
            c1spMaterial.Visible = false;
            IndexTreeView.ExpandAll();
            tlpAllForm.Refresh();
        }

        private void btMaterial_Click(object sender, EventArgs e)
        {
            materialCtrl = materialCtrl ? false : true;
            c1spMaterial.Visible = materialCtrl;
            c1spIndexBase.Visible = false;
            materialTreeView.ExpandAll();
            tlpAllForm.Refresh();
        }


        private void setLayout()
        {
            tabControl2.SelectedIndex = 1;
            frmGetFee = new GetFeeForm(); ;
            frmDirectProFee = new DirectProFeeForm();
            frmLocalFee = new LocalFeeForm();
            frmMaterialSummary = new MaterialSummaryForm();
            frmOtherFeeForm = new OtherFeeForm();
            frmReport = new ReportForm();
            frmTax = new TaxForm();
            frmProjectMan = new ProjectManForm();
            frmBidMan = new BidManForm();
            frmWelcome = new WelcomeForm();

            frmGetFee.TopLevel = false;
            frmDirectProFee.TopLevel = false;
            frmLocalFee.TopLevel = false;
            frmMaterialSummary.TopLevel = false;
            frmOtherFeeForm.TopLevel = false;
            frmReport.TopLevel = false;
            frmTax.TopLevel = false;
            frmBidMan.TopLevel = false;
            frmProjectMan.TopLevel = false;
            frmWelcome.TopLevel = false;

            frmDirectProFee.Dock = DockStyle.Fill;
            frmGetFee.Dock = DockStyle.Fill;
            frmLocalFee.Dock = DockStyle.Fill;
            frmMaterialSummary.Dock = DockStyle.Fill;
            frmOtherFeeForm.Dock = DockStyle.Fill;
            frmReport.Dock = DockStyle.Fill;
            frmTax.Dock = DockStyle.Fill;
            frmProjectMan.Dock = DockStyle.Fill;
            frmBidMan.Dock = DockStyle.Fill;
            frmWelcome.Dock = DockStyle.Fill;

            tabControl2.TabPages[0].Controls.Add(frmGetFee);
            tabControl2.TabPages[1].Controls.Add(frmDirectProFee);
            tabControl2.TabPages[2].Controls.Add(frmMaterialSummary);
            tabControl2.TabPages[3].Controls.Add(frmOtherFeeForm);
            tabControl2.TabPages[4].Controls.Add(frmLocalFee);
            tabControl2.TabPages[5].Controls.Add(frmTax);
            tabControl2.TabPages[6].Controls.Add(frmReport);
            tabControl2.Hide();
            btIndexBase.Hide();
            btMaterial.Hide();
            c1SplitterPanel2.Controls.Add(frmProjectMan);
            c1SplitterPanel2.Controls.Add(frmBidMan);
            c1SplitterPanel2.Controls.Add(frmWelcome);

            frmGetFee.Show();
            frmDirectProFee.Show();
            frmMaterialSummary.Show();
            frmOtherFeeForm.Show();
            frmLocalFee.Show();
            frmTax.Show();
            frmReport.Show();
            frmProjectMan.Show();
            frmBidMan.Hide();
            frmProjectMan.Hide();
            frmWelcome.Show();


        }

        private void tvProjectNavigation_MouseClick(object sender, MouseEventArgs e)//在项目导航点击右键
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (tvProjectNavigation.SelectedNode.Level == 0 && displayFlag)//.Equals(tvProjectNavigation.Nodes["root"]))
                {
                    contextMenuStrip1.Show(tvProjectNavigation, e.Location);
                }
                else if (tvProjectNavigation.SelectedNode.Level == 1)
                {
                    contextMenuStrip.Show(tvProjectNavigation, e.Location);
                    node = tvProjectNavigation.SelectedNode;//用node把选中节点的项目编号传给标段窗口
                }
                else if (tvProjectNavigation.SelectedNode.Level == 2)
                {
                    contextMenuStrip2.Show(tvProjectNavigation, e.Location);
                }

            }

        }


        public void setTreeNode()
        {
            tvProjectNavigation.Nodes["root"].Nodes.Clear();
            string sql = "select 项目编号,项目名称 from project_message";
            MySqlDataReader sdr = SqlHelper.getSqlReader(sql);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    tvProjectNavigation.Nodes["root"].Nodes.Add((string)sdr["项目名称"]).Name = (string)sdr["项目编号"];
                }
            }

            sdr.Close();
            sql = "select a.项目编号,a.项目名称,b.文件编号,b.文件名称 from project_message a,bid_section b where a.项目编号=b.所属项目";
            
            sdr = SqlHelper.getSqlReader(sql);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    TreeNode[] nodes = tvProjectNavigation.Nodes["root"].Nodes.Find((string)sdr["项目编号"], false);

                    nodes[0].Nodes.Add((string)sdr["文件名称"]).Name = (string)sdr["文件编号"];


                }
            }
            sdr.Close();
            tvProjectNavigation.ExpandAll();
        }


        private void MainForm_Shown(object sender, EventArgs e)
        {
        }

        private void tvProjectNavigation_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                frmProjectMan.clear();
                frmBidMan.clear();
                frmWelcome.Show();
            }
            else if (e.Node.Level == 1)
            {
                if (displayFlag)
                {
                    frmWelcome.Hide();
                    frmProjectMan.Show();
                    frmBidMan.Hide();
                    frmProjectMan.displayProject(tvProjectNavigation.SelectedNode.Name);
                }

            }
            else if (e.Node.Level == 2)
            {
                if (displayFlag)
                {
                    frmWelcome.Hide();
                    frmBidMan.Show();
                    frmProjectMan.Hide();
                    frmBidMan.displayBid(tvProjectNavigation.SelectedNode.Name);
                    frmBidMan.displayInventory(tvProjectNavigation.SelectedNode.Name);
                }
                else
                {
                    tabControl2.Show();
                    btIndexBase.Show();
                    btMaterial.Show();
                    frmProjectMan.Hide();
                    frmBidMan.Hide();
                }

            }

        }


        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmWelcome.Hide();
            frmProjectMan.Show();
            frmBidMan.Hide();
            frmProjectMan.setProjectDefault();
        }

        private void newBidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProjectMan.Hide();
            frmBidMan.Show();
            frmBidMan.clear();
            frmBidMan.setBidDefault(node.Name);

        }


        private void importInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBidMan.importInventory(tvProjectNavigation.SelectedNode.Name);
        }

        private void deleteProjecttoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProjectMan.deleteProject(tvProjectNavigation.SelectedNode.Name);
            setTreeNode();
            tvProjectNavigation.ExpandAll();
            frmProjectMan.clear();
        }

        private void deleteBidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBidMan.deleteBid(tvProjectNavigation.SelectedNode.Name);
            setTreeNode();
            tvProjectNavigation.ExpandAll();
            frmBidMan.clear();
        }

        private void BidEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmProjectMan.Hide();
            frmBidMan.Hide();
            displayFlag = false;
            tvProjectNavigation.SelectedNode = tvProjectNavigation.SelectedNode;
            //调用各窗口的接口函数
            frmDirectProFee.displayDirectFee(tvProjectNavigation.SelectedNode.Name);
            frmGetFee.setGetFeeDefault(tvProjectNavigation.SelectedNode.Name);
            frmOtherFeeForm.SetOtherFeeDefault(tvProjectNavigation.SelectedNode.Name);
            tabControl2.Show();


            projectManToolStripMenuItem.Visible = true;
            bidManToolStripMenuItem.Visible = true;
            newBidToolStripMenuItem.Visible = false;
            deleteProjecttoolStripMenuItem.Visible = false;
            BidEditToolStripMenuItem.Visible = false;
            deleteBidToolStripMenuItem.Visible = false;
            importInventoryToolStripMenuItem.Visible = false;
            exeportToolStripMenuItem.Visible = false;
            
        }

        private void exeportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBidMan.exportInventory(tvProjectNavigation.SelectedNode.Name);
        }


        private void imPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importIndexLibrary();

        }
        private void importIndexLibrary()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Excel(*.xlsx)|*.xlsx|Excel(*.xls)|*.xls";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                DataTable dtExcel = SqlHelper.ExcelToTable(openFile.FileName);
                MySqlDataAdapter adapter = SqlHelper.getSqlAdapter("select * from index_library_temp");
                DataTable dtDatabase = new DataTable();
                adapter.Fill(dtDatabase);
                foreach (DataRow dr in dtExcel.Rows)
                {
                    DataRow tempRow = dtDatabase.NewRow();
                    tempRow.ItemArray = dr.ItemArray;
                    dtDatabase.Rows.Add(tempRow);

                }
                adapter.Update(dtDatabase);
                SqlHelper.exeNonQuery("delete from index_library_temp");
                displayIndexLibrary("0");
            }
        }
        private void displayIndexLibrary(string number)
        {
            IndexLibraryC1FlexGrid.Clear(C1.Win.C1FlexGrid.ClearFlags.Content);
            string[] strTitle = { "细目号", "细目名称", "单位", "工作内容", "单价" };
            MySqlDataReader dataReader;
            for (int i = 0; i < strTitle.Length; i++)
            {
                IndexLibraryC1FlexGrid[0, i + IndexLibraryC1FlexGrid.Rows.Fixed] = strTitle[i];
            }
            if (number == "0")
            {
                dataReader = SqlHelper.getSqlReader("select * from index_library");
            }
            else
            {
                string sql = string.Format(@"select * from index_library where 细目号 like '{0}%'", number);
                dataReader = SqlHelper.getSqlReader(sql);
            }
            if (dataReader.HasRows)
            {
                int i = 1;
                while (dataReader.Read())
                {
                    IndexLibraryC1FlexGrid[i, 0] = string.Format("{0}", i);
                    IndexLibraryC1FlexGrid[i, "细目号"] = dataReader["细目号"];
                    IndexLibraryC1FlexGrid[i, "细目名称"] = dataReader["细目名称"];
                    IndexLibraryC1FlexGrid[i, "单位"] = dataReader["单位"];
                    IndexLibraryC1FlexGrid[i, "工作内容"] = dataReader["工作内容"];
                    IndexLibraryC1FlexGrid[i, "单价"] = dataReader["单价"];
                    i++;
                }
            }
            dataReader.Close();
        }

        private void importMaterialLibrary()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Excel(*.xlsx)|*.xlsx|Excel(*.xls)|*.xls";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                DataTable dtExcel = SqlHelper.ExcelToTable(openFile.FileName);
                MySqlDataAdapter adapter = SqlHelper.getSqlAdapter("select * from material_library_temp");
                DataTable dtDatabase = new DataTable();
                adapter.Fill(dtDatabase);
                foreach (DataRow dr in dtExcel.Rows)
                {
                    DataRow tempRow = dtDatabase.NewRow();
                    tempRow.ItemArray = dr.ItemArray;
                    dtDatabase.Rows.Add(tempRow);

                }
                adapter.Update(dtDatabase);
                SqlHelper.exeNonQuery("delete from material_library_temp");
                displayMaterialLibrary("0");
            }
        }
        private void displayMaterialLibrary(string number)
        {
            materialLibraryC1FlexGrid.Clear(C1.Win.C1FlexGrid.ClearFlags.Content);
            string[] strTitle = { "编号", "名称", "规格", "单位", "单价","材料类型"};
            MySqlDataReader dataReader;
            string sql = "";
            for (int i = 0; i < strTitle.Length; i++)
            {
                materialLibraryC1FlexGrid[0, i + materialLibraryC1FlexGrid.Rows.Fixed] = strTitle[i];
            }
            switch (number)
            {
                case "0": sql = "select * from material_library order by length(编号),编号";
                    break;
                case "人才机": sql = "select * from material_library where 编号 like '_' order by length(编号),编号";
                    break;
                case "材料": sql = "select * from material_library where 编号 like '_____________' and (材料类型<>'周转材料' or  isnull(材料类型)) order by length(编号),编号";
                    break;
                case "机械": sql = "select * from material_library where 编号 like '______' order by length(编号),编号";
                    break;
                case "周转材料": sql = "select * from material_library where 编号 like '_____________' and 材料类型='周转材料' order by length(编号),编号";
                    break;
            }
            dataReader = SqlHelper.getSqlReader(sql);
            if (dataReader.HasRows)
            {
                int i = 1;
                while (dataReader.Read())
                {
                    materialLibraryC1FlexGrid.Rows.Add(1);
                    materialLibraryC1FlexGrid[i, 0] = string.Format("{0}", i);
                    materialLibraryC1FlexGrid[i, "编号"] = dataReader["编号"];
                    materialLibraryC1FlexGrid[i, "名称"] = dataReader["名称"];
                    materialLibraryC1FlexGrid[i, "规格"] = dataReader["规格"];
                    materialLibraryC1FlexGrid[i, "单位"] = dataReader["单位"];
                    materialLibraryC1FlexGrid[i, "单价"] = dataReader["单价"];
                    materialLibraryC1FlexGrid[i, "材料类型"] = dataReader["材料类型"];
                    i++;
                }
            }
            dataReader.Close();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bidManToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBidMan.Show();
            frmProjectMan.Hide();
            tabControl2.Hide();
            btIndexBase.Hide();
            btMaterial.Hide();
            c1spIndexBase.Visible = false;
            c1spMaterial.Visible = false;
            displayFlag = true;
            projectManToolStripMenuItem.Visible = false;
            bidManToolStripMenuItem.Visible = false;
            newBidToolStripMenuItem.Visible = true;
            deleteProjecttoolStripMenuItem.Visible = true;
            BidEditToolStripMenuItem.Visible = true;
            deleteBidToolStripMenuItem.Visible = true;
            importInventoryToolStripMenuItem.Visible = true;
            exeportToolStripMenuItem.Visible = true;
        }

        private void projectManToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProjectMan.Show();
            frmBidMan.Hide();
            tabControl2.Hide();
            btIndexBase.Hide();
            btMaterial.Hide();
            c1spIndexBase.Visible = false;
            c1spMaterial.Visible = false;
            displayFlag = true;
            projectManToolStripMenuItem.Visible = false;
            bidManToolStripMenuItem.Visible = false;
            newBidToolStripMenuItem.Visible = true;
            deleteProjecttoolStripMenuItem.Visible = true;
            BidEditToolStripMenuItem.Visible = true;
            deleteBidToolStripMenuItem.Visible = true;
            importInventoryToolStripMenuItem.Visible = true;
            exeportToolStripMenuItem.Visible = true;
        }


        private void materialTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.Node.Level == 0)
            {
                materialImportContextMenuStrip.Show(materialTreeView, e.Location);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                displayMaterialLibrary(e.Node.Name);
            }
        }

        private void materialImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importMaterialLibrary();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            setLayout();
            setTreeNode();
            tvProjectNavigation.ExpandAll();
            displayIndexLibrary("0");
            displayMaterialLibrary("0");
        }


        private void IndexTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.Node.Level == 0)
            {
                importContextMenuStrip.Show(IndexTreeView, e.Location);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                displayIndexLibrary(e.Node.Name);
            }
        }
        public void selectIndex()
        {
            c1spIndexBase.Show();
            c1spMaterial.Hide();
            IndexTreeView.ExpandAll();
            lockMouse();
            displayIndexLibrary("0");
        }
        private void lockMouse()
        {
            
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = materialLibraryC1FlexGrid.PointToScreen(new Point(0,0));
            Cursor.Clip = new Rectangle(Cursor.Position , materialLibraryC1FlexGrid.Size);

        }
        private void unlockMouse()
        {
            Screen[] screens = Screen.AllScreens;
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Clip = screens[0].Bounds;
        }
        private void IndexLibraryC1FlexGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            c1spIndexBase.Hide();
            C1FlexGrid c1Grid = (C1FlexGrid)sender;
            frmDirectProFee.addIndex(c1Grid.Rows[c1Grid.RowSel]);
            unlockMouse();
        }
        public void selectMaterial(string index,bool addOrSelectFlag)
        {
            c1spMaterial.Show();
            c1spIndexBase.Hide();
            materialTreeView.ExpandAll();
            displayMaterialLibrary(index);
            lockMouse();
            this.addOrSelectFlag = addOrSelectFlag;
        }

        private void materialLibraryC1FlexGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            c1spMaterial.Hide();
            C1FlexGrid c1Grid = (C1FlexGrid)sender;
            if (addOrSelectFlag)
            {
                frmDirectProFee.addMaterial(c1Grid.Rows[c1Grid.RowSel]);
            }
            else
            {
                frmDirectProFee.addAddMaterial(c1Grid.Rows[c1Grid.RowSel]);

            }
            unlockMouse();
        }
        public void hideButton()
        {
            btIndexBase.Hide();
            btMaterial.Hide();
        }
        public void showButton()
        {
            btIndexBase.Show();
            btMaterial.Show();
        }


        private void tabControl2_Selected(object sender, TabControlEventArgs e)
        {
            if(e.TabPageIndex == 1)
            {
                showButton();
            }
            else
            {
                hideButton();
            }
        }

        private void tabControl2_VisibleChanged(object sender, EventArgs e)
        {
            if (tabControl2.Visible)
            {
                showButton();
            }
            else
            {
                hideButton();
            }
        }

        private void returnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            unlockMouse();
            c1spMaterial.Hide();
        }

        private void materialLibraryC1FlexGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                materialSelectContextMenuStrip.Show(materialLibraryC1FlexGrid,e.Location);
            }
        }
    }
}
