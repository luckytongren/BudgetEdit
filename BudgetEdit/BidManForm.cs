using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Tools;
using System.Globalization;
using System.Data.OleDb;
using System.IO;

namespace BudgetEdit
{

    public partial class BidManForm : Form
    {
        private string[] colName = { "编号", "标识", "名称", "单位", "租赁", "混合", "工程量", "单价", "合价", "劳务费", "材料费", "机械费", "摊销费", "其他费" };
        private string pNumber = null;
        private DateTimePicker dtp = new DateTimePicker();  //这里实例化一个DateTimePicker控件 
        private DateTimePicker dtp1 = new DateTimePicker();
        private DateTimePicker dtp2 = new DateTimePicker();
        
        private Rectangle _Rectangle;
        private bool progreeFlag = false;
        DataTable dtInventoryDatabase;
        MySqlDataAdapter inventoryAdapter;
        bool[] setting ={false,false,false,false,false,false,false,false};
        public BidManForm()
        {
            InitializeComponent();
            dataGridView1.Controls.Add(dtp);  //把时间控件加入DataGridView  
            dataGridView1.Controls.Add(dtp1);
            dataGridView1.Controls.Add(dtp2);
            
            dtp.Format = DateTimePickerFormat.Custom;  //设置日期格式为2010-08-05  
            dtp.TextChanged += new EventHandler(dtp_TextChange); //为时间控件加入事件dtp_TextChange  
            dtp.LostFocus += new EventHandler(dtp_LostFocus);
            dtp1.Format = DateTimePickerFormat.Custom;  //设置日期格式为2010-08-05  
            dtp1.TextChanged += new EventHandler(dtp1_TextChange); //为时间控件加入事件dtp_TextChange  
            dtp1.LostFocus += new EventHandler(dtp1_LostFocus);
            dtp2.Format = DateTimePickerFormat.Custom;  //设置日期格式为2010-08-05  
            dtp2.TextChanged += new EventHandler(dtp2_TextChange); //为时间控件加入事件dtp_TextChange  
            dtp2.LostFocus += new EventHandler(dtp2_LostFocus);
            dtp.Hide();
            dtp1.Hide();
            dtp2.Hide();
        }
        private void dtp_LostFocus(object sender, EventArgs e)
        {
            dtp.Hide();
        }
        private void dtp1_LostFocus(object sender, EventArgs e)
        {
            dtp1.Hide();
        }
        private void dtp2_LostFocus(object sender, EventArgs e)
        {
            dtp2.Hide();
        }
        private void dtp_TextChange(object sender, EventArgs e)
        {
            dataGridView1[1, 6].Value = dtp.Text.ToString();  //时间控件选择时间时，就把时间赋给所在的单元格  
            dtp.Hide();
        }
        private void dtp1_TextChange(object sender, EventArgs e)
        {
            dataGridView1[1, 9].Value = dtp.Text.ToString();  //时间控件选择时间时，就把时间赋给所在的单元格  
            dtp1.Hide();
        }
        private void dtp2_TextChange(object sender, EventArgs e)
        {
            dataGridView1[1, 13].Value = dtp.Text.ToString();  //时间控件选择时间时，就把时间赋给所在的单元格  
            dtp2.Hide();
        }
        private void setInventoryTitle()
        {
            
            for (int i = 0; i < colName.Length; i++)
            {                           
                c1FlexGrid2[0, i+1] = colName[i];
            }
            c1FlexGrid2.Cols["租赁"].Visible = setting[0];
            c1FlexGrid2.Cols["混合"].Visible = setting[1];
            c1FlexGrid2.Cols["劳务费"].Visible =  setting[2];
            c1FlexGrid2.Cols["材料费"].Visible =  setting[3];
            c1FlexGrid2.Cols["机械费"].Visible =  setting[4];
            c1FlexGrid2.Cols["摊销费"].Visible =  setting[5];
            c1FlexGrid2.Cols["摊销费"].Visible =  setting[6];
            c1FlexGrid2.Cols["其他费"].Visible =  setting[7];
        }
        private void setBidTitle()
        {
            dataGridView1.Rows.Clear();
            string[] rowName = { "文件编号        （*）", "文件名称        （*）", "计价依据        （*）", "工程类别        （*）", "编制人单位     （*）", "编制人            （*）", "编制时间         （*）", "复核人单位", "复核人", "复核时间", "审批编号", "审核单位", "审核人", "审核时间" };
            dataGridView1.Rows.Add(rowName.Length);
            dataGridView1.RowHeadersWidth = 60;
            dataGridView1.Columns[1].Width = dataGridView1.Parent.Width - 205;
            for (int i = 0; i < rowName.Length; i++)
            {
                dataGridView1.Rows[i].Height = this.Parent.Height / rowName.Length-5;
                dataGridView1.Rows[i].HeaderCell.Value= string.Format("{0}", i + 1);
                dataGridView1[0, i].Value = rowName[i];
            }
            DataGridViewComboBoxCell dgCell = new DataGridViewComboBoxCell();
            dgCell.Items.Add("路线");
            dgCell.Items.Add("桥梁");
            dataGridView1.Rows[3].Cells[1] = dgCell;// "标后预算|施工预算";
            dataGridView1.Rows[0].ReadOnly = true;
        }
        public  void setBidDefault(string projectNumber)//填写表格的表头和默认值
        {
            pNumber = projectNumber;
            setBidTitle();
            string strCount =string.Format("{0}",getBidCount());
            dataGridView1[1, 0].Value = string.Format("{0}{1}", DateTime.Now.Year, strCount.Length==1 ? strCount="0"+strCount:strCount ); 
            dataGridView1[1, 2].Value = "中交二局标后预算计价包";
           
            DateTime date = DateTime.Now;
            dataGridView1[1, 6].Value = date.ToShortDateString();
            dtp.Text = date.ToShortDateString();
        }

        private void BidManForm_Shown(object sender, EventArgs e)//当窗口显示是设置表格的初始化样式
        {
            //dataGridView1.Cols[1].AllowEditing = false;
            //dataGridView1.Cols[2].AllowEditing = true;
            //dataGridView1.Rows.Count = 14;
            //dataGridView1.Cols[0].Width = 50;
            //dataGridView1.Cols[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            //dataGridView1.Cols[1].Width = 100;
            //dataGridView1.Cols[2].Width = dataGridView1.Parent.Width - 180;
        }
        private void insertProject()//把标段信息插入数据库表
        {
            string sql = "select * from bid_section";
            DataTable dataTable = new DataTable();
            MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
            adapter.Fill(dataTable);

            DataRow dr = null;
            DataRow[] drs = dataTable.Select(string.Format("文件编号='{0}'", dataGridView1[1, 0].Value));
            if (drs.Length == 0)
            {
                dr = dataTable.NewRow();
                dr["文件编号"] = (string)dataGridView1[1, 0].Value;
                dr["所属项目"] = pNumber;
            }
            else
            {
                dr = drs[0];
            }

            dr["文件名称"] = dataGridView1[1, 1].Value;
            dr["计价依据"] = dataGridView1[1, 2].Value;
            dr["工程类别"] = dataGridView1[1, 3].Value;
            dr["编制人单位"] = dataGridView1[1, 4].Value;
            dr["编制人"] = dataGridView1[1, 5].Value;
            dr["编制时间"] = dataGridView1[1, 6].Value;
            dr["复核人"] = dataGridView1[1, 7].Value;
            dr["复核人单位"] = dataGridView1[1, 8].Value;
            if (dataGridView1[1, 9].Value != null)
                dr["复核时间"] = dataGridView1[1, 9].Value;
            dr["审批编号"] = dataGridView1[1, 10].Value;
            dr["审核单位"] = dataGridView1[1, 11].Value;
            dr["审核人"] = dataGridView1[1, 12].Value;
            if (dataGridView1[1, 13].Value != null)
                dr["审核时间"] = dataGridView1[1, 13].Value;
            if (drs.Length == 0)
            {
                dataTable.Rows.Add(dr);
            }
            
            adapter.Update(dataTable);
        }
        private int getBidCount()//取标段计数
        {
            string sql = "select b_count from project_count";
            MySqlDataReader sdr = SqlHelper.getSqlReader(sql);
            sdr.Read();
            int count = (int)sdr["b_count"];
            sdr.Close();
            return count;

        }
        private void updateProjectCount()//更新标段计数
        {
            string sql = string.Format("update project_count set b_count=b_count+1");
            SqlHelper.exeNonQuery(sql);
        }

   
        public void displayBid(string bidNumber)//显示标段信息
        {
            
            string sql = string.Format("select * from bid_section where 文件编号={0}", bidNumber);
            MySqlDataReader sdr = SqlHelper.getSqlReader(sql);
            if (sdr.HasRows)
            {
                setBidTitle();
                while (sdr.Read())
                {
                    dataGridView1[1, 0].Value = sdr["文件编号"];
                    dataGridView1[1, 1].Value = sdr["文件名称"];
                    dataGridView1[1, 2].Value = sdr["计价依据"];
                    dataGridView1[1, 3].Value = sdr["工程类别"];
                    dataGridView1[1, 4].Value = sdr["编制人单位"];
                    dataGridView1[1, 5].Value = sdr["编制人"];
                    dataGridView1[1, 6].Value = ((DateTime)sdr["编制时间"]).ToShortDateString();
                    dtp.Text = ((DateTime)sdr["编制时间"]).ToShortDateString();
                    dataGridView1[1, 7].Value = sdr["复核人单位"];
                    dataGridView1[1, 8].Value = sdr["复核人"];
                    dataGridView1[1, 9].Value = sdr["复核时间"];
                    if (sdr["复核时间"] != DBNull.Value)
                    {
                        dataGridView1[1, 9].Value = ((DateTime)sdr["复核时间"]).ToShortDateString();
                        dtp1.Text = ((DateTime)sdr["复核时间"]).ToShortDateString();
                    }
                    dataGridView1[1, 10].Value = sdr["审批编号"];
                    dataGridView1[1, 11].Value = sdr["审核单位"];
                    dataGridView1[1, 12].Value = sdr["审核人"];
                    dataGridView1[1, 13].Value = sdr["审核时间"];
                    if (sdr["审核时间"] != DBNull.Value)
                    {
                        dataGridView1[1, 13].Value = ((DateTime)sdr["审核时间"]).ToShortDateString();
                        dtp2.Text = ((DateTime)sdr["审核时间"]).ToShortDateString();
                    }

                }
            }
            sdr.Close();
            
        }

        public void importInventory(string bidNumber)//导入业主清单
        {

            ImportProgressBar.Minimum = 0;
            ImportProgressBar.Value = 0;
            progreeFlag = true;
            //ImportProgressBar.Style = ProgressBarStyle.Marquee;
            
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Excel(*.xlsx)|*.xlsx|Excel(*.xls)|*.xls";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                
                DataTable dtImport = SqlHelper.ExcelToTable(openFile.FileName,"业主清单");
                ImportProgressBar.Maximum = dtImport.Rows.Count;
                ImportProgressBar.Visible = true;
                string sql = "SELECT * FROM project_inventory_temp";
                MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);

                DataTable dtDatabase = new DataTable();
                adapter.Fill(dtDatabase);

                foreach (DataRow dr in dtImport.Rows)
                {
                    
                    DataRow tempRow = dtDatabase.NewRow();
                    tempRow["编号"] = dr["编号"];
                    tempRow["标识"] = dr["标识"];
                    tempRow["名称"] = dr["名称"];
                    tempRow["单位"] = dr["单位"];
                    tempRow["工程量"] = dr["工程量"];
                    tempRow["单价"] = dr["单价"];
                    tempRow["合价"] = dr["合价"];
                    tempRow["所属标段"] = bidNumber;

                    dtDatabase.Rows.Add(tempRow);
                    ImportProgressBar.Value++;
                }
                adapter.Update(dtDatabase);
                deleteTableTemp();
                displayInventory(bidNumber);
                ImportProgressBar.Visible = false;
            }
        }
        public void displayInventory(string bidNumber)//显示业主清单
        {
            c1FlexGrid2.Clear(C1.Win.C1FlexGrid.ClearFlags.Content);
            c1FlexGrid2.Rows.RemoveRange(1, c1FlexGrid2.Rows.Count - 1);
            string sql = string.Format("SELECT * FROM project_inventory where 所属标段 ='{0}'",bidNumber);
            inventoryAdapter = SqlHelper.getSqlAdapter(sql);
            dtInventoryDatabase = new DataTable();
            inventoryAdapter.Fill(dtInventoryDatabase);
            if (progreeFlag)
            {
                ImportProgressBar.Maximum += dtInventoryDatabase.Rows.Count + 1;
            }
            else
            {
                ImportProgressBar.Value = 0;
                ImportProgressBar.Visible = true;
                ImportProgressBar.Maximum = dtInventoryDatabase.Rows.Count + 1;
            }
            if (dtInventoryDatabase.Rows.Count > 0)
            {
                setInventoryTitle();
                foreach (DataRow dr in dtInventoryDatabase.Rows)
                {
                    string number = (string)dr["编号"];
                    int count = 0;
                    for (int i = 0; i < number.Length; i++)
                    {
                        if (number[i] == '-')
                            count++;
                    }
                    //C1.Win.C1FlexGrid.Node newNode = c1FlexGrid2.Rows.InsertNode(c1FlexGrid2.Rows.Count + c1FlexGrid2.Rows.Fixed - 1, ++count);
                    C1.Win.C1FlexGrid.Node newNode = c1FlexGrid2.Rows.AddNode(++count);
                    newNode.Row.Height = 35;
                   // newNode.Row.AllowEditing = false;
                    newNode.Row["编号"] = dr["编号"];
                    newNode.Row["标识"] = dr["标识"];
                    newNode.Row["名称"] = dr["名称"];
                    newNode.Row["单位"] = dr["单位"];
                    newNode.Row["工程量"] = dr["工程量"];
                    newNode.Row["单价"] = dr["单价"];
                    newNode.Row["合价"] = dr["合价"];
                    if ((bool)dr["修改标识"])
                    {
                        C1.Win.C1FlexGrid.CellStyle rs =c1FlexGrid2.Styles.Add("RowColor");
                        rs.ForeColor = Color.Red;
                        newNode.Row.Style = c1FlexGrid2.Styles["RowColor"];
                    }

                    ImportProgressBar.Value++;

                }
            }
            ImportProgressBar.Visible = false;
            progreeFlag = false;
   
        }
        public void deleteBid(string bidNumber)
        {
            string sql = string.Format("delete from bid_section where 文件编号='{0}'", bidNumber);
            SqlHelper.exeNonQuery(sql);
        }
        public void clear()
        {
            dataGridView1.Rows.Clear();
            c1FlexGrid2.Rows.RemoveRange(1, c1FlexGrid2.Rows.Count - 1);
            c1FlexGrid2.Clear(C1.Win.C1FlexGrid.ClearFlags.Content);
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (dataGridView1[1, 1].Value != null && dataGridView1[1, 2].Value != null && dataGridView1[1, 3].Value != null && dataGridView1[1, 4].Value != null && dataGridView1[1, 5].Value != null)
            {
                insertProject();
                updateProjectCount();
                ((MainForm)this.TopLevelControl).setTreeNode();//更新主窗口的项目导航树
            }
            else
            {
                MessageBox.Show(@"请输入所有带(*)字段", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void deleteTableTemp()//删除临时表
        {
            string sql = string.Format("delete from project_inventory_temp");
            SqlHelper.exeNonQuery(sql);
        }
        public void exportInventory(string bidNumber)//导出业主清单
        {
            string sql = string.Format("select * from project_inventory where 所属标段 = '{0}'",bidNumber);
            DataTable dt = new DataTable();
            if (File.Exists(Directory.GetCurrentDirectory() + @"\模板\业主清单模板.xlsx"))
            {
                
                if (!System.IO.Directory.Exists(Directory.GetCurrentDirectory()+@"\导出数据"))
                {
                    System.IO.Directory.CreateDirectory(Directory.GetCurrentDirectory()+@"\导出数据");

                }
                string sourcePath = Directory.GetCurrentDirectory() + @"\模板\业主清单模板.xlsx";
                string targetPath = Directory.GetCurrentDirectory() + @"\导出数据\"+bidNumber+"业主清单.xlsx";
                File.Copy(sourcePath, targetPath,true);
                SqlHelper.getSqlAdapter(sql).Fill(dt);
                SqlHelper.TableToExcel(dt, targetPath);
                MessageBox.Show(bidNumber+"业主清单导出成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            else
            {
                MessageBox.Show("业主清单模板不存在！", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex == 6)
            {
                _Rectangle = dataGridView1.GetCellDisplayRectangle(1, 6, true); //得到所在单元格位置和大小  
                dtp.Size = new Size(_Rectangle.Width - 5, _Rectangle.Height); //把单元格大小赋给时间控件 
                dtp.Location = new Point(_Rectangle.X + 2, _Rectangle.Y + 8); //把单元格位置赋给时间控件  
                dtp.Visible = true;  //可以显示控件了  
                dtp.Focus();
            }
            if (e.ColumnIndex == 1 && e.RowIndex == 9)
            {
                _Rectangle = dataGridView1.GetCellDisplayRectangle(1, 9, true);
                dtp1.Size = new Size(_Rectangle.Width - 5, _Rectangle.Height); //把单元格大小赋给时间控件  
                dtp1.Location = new Point(_Rectangle.X + 2, _Rectangle.Y + 8); //把单元格位置赋给时间控件  
                dtp1.Visible = true;  //可以显示控件了  
                dtp1.Focus();
            }
            else if (e.ColumnIndex == 1 && e.RowIndex == 13)
            {

                _Rectangle = dataGridView1.GetCellDisplayRectangle(1, 13, true);
                dtp2.Size = new Size(_Rectangle.Width - 5, _Rectangle.Height); //把单元格大小赋给时间控件  
                dtp2.Location = new Point(_Rectangle.X + 2, _Rectangle.Y + 8); //把单元格位置赋给时间控件  
                dtp2.Visible = true;
                dtp2.Focus();
            }
        }

        private void BidManForm_VisibleChanged(object sender, EventArgs e)
        {
            //if (!Visible)
            //{
            //    btSave.Enabled = false;
            //    btReset.Enabled = false;
            //}
        }

   
        private void c1FlexGrid2_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //{
            //    contextMenuStrip1.Show(c1FlexGrid2,e.Location);
            //}
        }

        private void DisplaySettingToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Form frmSetting = new SettingForm(setting);
            ////frm.TopLevel = false;
            //frmSetting.Show(c1FlexGrid2);
            ////MessageBox.Show(string.Format("{0}",setting[0]));
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           if (dataGridView1[1, 1].Value != null && dataGridView1[1, 2].Value != null && dataGridView1[1, 3].Value != null && dataGridView1[1, 4].Value != null && dataGridView1[1, 5].Value != null)
            {
                insertProject();
                updateProjectCount();
                ((MainForm)this.TopLevelControl).setTreeNode();//更新主窗口的项目导航树
            }
        }

        private void c1FlexGrid2_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            DataRow[] drs = dtInventoryDatabase.Select(string.Format("编号='{0}'", (string)(c1FlexGrid2[e.Row, "编号"])));
           // DataRow dr = drs[0];
            if (!drs[0][e.Col-1].Equals(c1FlexGrid2[e.Row, e.Col]))
            {
                drs[0][e.Col-1] = c1FlexGrid2[e.Row, e.Col];
                drs[0]["修改标识"] = true;
                inventoryAdapter.Update(dtInventoryDatabase);
                displayInventory((string)drs[0]["所属标段"]);

            }
        }

     
    }
}
