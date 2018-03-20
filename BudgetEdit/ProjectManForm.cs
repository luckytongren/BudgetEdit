using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using System.Data.SqlClient;
using Tools;
using System.Globalization;
using MySql.Data.MySqlClient;
namespace BudgetEdit
{
    public partial class ProjectManForm : Form
    {
        DateTimePicker dtp = new DateTimePicker();  //这里实例化一个DateTimePicker控件 
        DateTimePicker dtp1 = new DateTimePicker();
        DateTimePicker dtp2 = new DateTimePicker();
        Rectangle _Rectangle;
        public ProjectManForm()
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

        /*************时间控件选择时间时****************/
        private void dtp_TextChange(object sender, EventArgs e)
        {
            dataGridView1[1, 5].Value = dtp.Text.ToString();  //时间控件选择时间时，就把时间赋给所在的单元格  
            dtp.Hide();
        }
        private void dtp1_TextChange(object sender, EventArgs e)
        {
            dataGridView1[1, 8].Value = dtp.Text.ToString();  //时间控件选择时间时，就把时间赋给所在的单元格  
            dtp1.Hide();
        }
        private void dtp2_TextChange(object sender, EventArgs e)
        {
            dataGridView1[1, 12].Value = dtp.Text.ToString();  //时间控件选择时间时，就把时间赋给所在的单元格  
            dtp2.Hide();
        }

        /***********当列的宽度变化时，时间控件先隐藏起来，不然单元格变大时间控件无法跟着变大哦***********/
        //private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        //{
        //    dtp.Visible = false;

        //}

        private void setProjectTitle()//设置表格的样式个表头
        {
            dataGridView1.Rows.Clear();
            string[] rowName = { "项目编号        （*）", "项目名称        （*）", "编制阶段        （*）", "编制人单位     （*）", "编制人            （*）", "编制时间", "复核人单位", "复核人", "复核时间", "审批编号", "审核单位", "审核人", "审核时间" };
            dataGridView1.Rows.Add(rowName.Length);
            dataGridView1.Columns[1].Width = dataGridView1.Parent.Width - 220;
            for (int i = 0; i < rowName.Length; i++)
            {

                dataGridView1.Rows[i].Cells[0].Value = rowName[i];
                dataGridView1.Rows[i].Height = this.Parent.Height / rowName.Length - 5;
                dataGridView1.Rows[i].HeaderCell.Value = string.Format("{0}", i + 1);
            }
            DataGridViewComboBoxCell dgCell = new DataGridViewComboBoxCell();
            dgCell.Items.Add("标后预算");
            dgCell.Items.Add("施工预算");
            dataGridView1.Rows[2].Cells[1] = dgCell;// "标后预算|施工预算";


            //_Rectangle = dataGridView1.GetCellDisplayRectangle(1, 5, true);
            ////ComboBox cb = new ComboBox();
            ////cb.Items.Add("标后预算");
            ////cb.Items.Add("施工预算");
            //comboBox1.Size = new Size(_Rectangle.Width, _Rectangle.Height+20);
            //comboBox1.Location = new Point(_Rectangle.X, _Rectangle.Y);
            ////cb.Show();

            dataGridView1.Rows[0].ReadOnly = true;
            c1SplitterPanel1.Show();
            c1SplitterPanel2.Show();
            c1SplitterPanel3.Show();
            label1.Show();

        }

        public void setProjectDefault()//设置字段默认值
        {

            setProjectTitle();
            setDescriptio();
            string temp = string.Format("{0}", getProjectCount());
            if (temp.Length == 1)
                dataGridView1.Rows[0].Cells[1].Value = "00" + temp;
            else if (temp.Length == 2)
                dataGridView1.Rows[0].Cells[1].Value = "0" + temp;
            else
                dataGridView1.Rows[0].Cells[1].Value = "0" + temp;


            DateTime date = DateTime.Now;
            dataGridView1[1, 5].Value = date.ToShortDateString();
            dtp.Text = date.ToShortDateString();


        }

        private int getProjectCount()//获得项目计数
        {
            string sql = "select p_count from project_count";
            MySqlDataReader sdr = SqlHelper.getSqlReader(sql);
            sdr.Read();
            int count = (int)sdr["p_count"];
            return count;

        }

        private void insertProject()/*把项目信息插入数据库表*/
        {
            string sql = "select * from project_message";
            DataTable dataTable = new DataTable();
            MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
            adapter.Fill(dataTable);

            DataRow dr = null;
            DataRow[] drs = dataTable.Select(string.Format("项目编号='{0}'", (string)dataGridView1[1, 0].Value));
            if (drs.Length == 0)
            {
                dr = dataTable.NewRow();
                dr["项目编号"] = (string)dataGridView1[1, 0].Value;
            }
            else
            {
                dr = drs[0];
            }

            dr["项目名称"] = dataGridView1[1, 1].Value;
            dr["编制阶段"] = dataGridView1[1, 2].Value;
            dr["编制人单位"] = dataGridView1[1, 3].Value;
            dr["编制人"] = dataGridView1[1, 4].Value;
            dr["编制时间"] = dataGridView1[1, 5].Value;
            dr["复核人单位"] = dataGridView1[1, 6].Value;
            dr["复核人"] = dataGridView1[1, 7].Value;
            if (dataGridView1[1, 8].Value != null)
                dr["复核时间"] = dataGridView1[1, 8].Value;
            dr["审批编号"] = dataGridView1[1, 9].Value;
            dr["审核单位"] = dataGridView1[1, 10].Value;
            dr["审核人"] = dataGridView1[1, 11].Value;
            if (dataGridView1[1, 12].Value != null)
                dr["审核时间"] = dataGridView1[1, 12].Value;
            dr["编制说明"] = richTextBox1.Text;
            dr["审核_工程概况"] = richTextBox2.Text;
            dr["审核_依据"] = richTextBox3.Text;
            dr["审核_问题"] = richTextBox4.Text;
            dr["审核_其他"] = richTextBox5.Text;
            
            if (drs.Length == 0)
            {
                dataTable.Rows.Add(dr);
            }
            adapter.Update(dataTable);
        }

        private void updateProjectCount()//更新项目计数
        {
            string sql = string.Format("update project_count set p_count=p_count+1");
            SqlHelper.exeNonQuery(sql);
        }

        private void ProjectManForm_Shown(object sender, EventArgs e)//窗口显示是初始化表格样式
        {
        }
        public void setDescriptio()
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
            richTextBox5.Clear();
        }
        public void displayProject(string projectNumber)//在项目导航选中项目，显示项目信息
        {
            setProjectTitle();
            setDescriptio();
            string sql = string.Format("select * from project_message where 项目编号={0}", projectNumber);
            MySqlDataReader sdr = SqlHelper.getSqlReader(sql);

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {

                    dataGridView1[1, 0].Value = sdr["项目编号"];
                    dataGridView1[1, 1].Value = sdr["项目名称"];
                    dataGridView1[1, 2].Value = sdr["编制阶段"];
                    dataGridView1[1, 3].Value = sdr["编制人单位"];
                    dataGridView1[1, 4].Value = sdr["编制人"];
                    dataGridView1[1, 5].Value = ((DateTime)sdr["编制时间"]).ToShortDateString();
                    dtp.Text = ((DateTime)sdr["编制时间"]).ToShortDateString();
                    dataGridView1[1, 6].Value = sdr["复核人单位"];
                    dataGridView1[1, 7].Value = sdr["复核人"];
                    if (sdr["复核时间"] != DBNull.Value)
                    {
                        dataGridView1[1, 8].Value = ((DateTime)sdr["复核时间"]).ToShortDateString();
                        dtp1.Text = ((DateTime)sdr["复核时间"]).ToShortDateString();
                    }
                    dataGridView1[1, 9].Value = sdr["审批编号"];
                    dataGridView1[1, 10].Value = sdr["审核单位"];
                    dataGridView1[1, 11].Value = sdr["审核人"];
                    if (sdr["审核时间"] != DBNull.Value)
                    {
                        dataGridView1[1, 12].Value = ((DateTime)sdr["审核时间"]).ToShortDateString();
                        dtp2.Text = ((DateTime)sdr["审核时间"]).ToShortDateString();
                    }
                    if (sdr["编制说明"] != DBNull.Value)
                    {
                        richTextBox1.Text = (string)sdr["编制说明"];
                    }
                    if (sdr["审核_工程概况"] != DBNull.Value)
                    {
                        richTextBox2.Text = (string)sdr["审核_工程概况"];
                    }
                    if (sdr["审核_依据"] != DBNull.Value)
                    {
                        richTextBox3.Text = (string)sdr["审核_依据"];
                    }
                    if (sdr["审核_问题"] != DBNull.Value)
                    {
                        richTextBox4.Text = (string)sdr["审核_问题"];
                    }
                    if (sdr["审核_其他"] != DBNull.Value)
                    {
                        richTextBox5.Text = (string)sdr["审核_其他"];
                    }

                }
            }
            sdr.Close();
        }
        public void deleteProject(string projectNumber)
        {
            string sql = string.Format("delete from project_message where 项目编号='{0}'", projectNumber);
            SqlHelper.exeNonQuery(sql);
        }
        public void clear()
        {
            dataGridView1.Rows.Clear();
            dtp.Visible = false;
            dtp1.Visible = false;
            dtp2.Visible = false;
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
            richTextBox5.Clear();
            c1SplitterPanel1.Hide();
            c1SplitterPanel2.Hide();
            c1SplitterPanel3.Hide();
            label1.Hide();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 1 && e.RowIndex == 5)
            {
                _Rectangle = dataGridView1.GetCellDisplayRectangle(1, 5, true); //得到所在单元格位置和大小  
                
                dtp.Size = new Size(_Rectangle.Width - 5, _Rectangle.Height); //把单元格大小赋给时间控件 
                dtp.Location = new Point(_Rectangle.X + 2, _Rectangle.Y + 8); //把单元格位置赋给时间控件  
                dtp.Visible = true;  //可以显示控件了  
                dtp.Focus();
            }
            if (e.ColumnIndex == 1 && e.RowIndex == 8)
            {
                _Rectangle = dataGridView1.GetCellDisplayRectangle(1, 8, true);
                dtp1.Size = new Size(_Rectangle.Width - 5, _Rectangle.Height); //把单元格大小赋给时间控件  
                dtp1.Location = new Point(_Rectangle.X + 2, _Rectangle.Y + 8); //把单元格位置赋给时间控件  
                dtp1.Visible = true;  //可以显示控件了  
                dtp1.Focus();
            }
            else if (e.ColumnIndex == 1 && e.RowIndex == 12)
            {

                _Rectangle = dataGridView1.GetCellDisplayRectangle(1, 12, true);
                dtp2.Size = new Size(_Rectangle.Width - 5, _Rectangle.Height); //把单元格大小赋给时间控件  
                dtp2.Location = new Point(_Rectangle.X + 2, _Rectangle.Y + 8); //把单元格位置赋给时间控件  
                dtp2.Visible = true;
                dtp2.Focus();
            }

        }

        private void ProjectManForm_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1[1, 1].Value != null && dataGridView1[1, 2].Value != null && dataGridView1[1, 3].Value != null && dataGridView1[1, 4].Value != null)
            {
                insertProject();
                updateProjectCount();
                ((MainForm)this.TopLevelControl).setTreeNode();//更新主窗口的项目导航树
            }
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length>0 && dataGridView1[1, 1].Value != null && dataGridView1[1, 2].Value != null && dataGridView1[1, 3].Value != null && dataGridView1[1, 4].Value != null)
            {
                insertProject();
                updateProjectCount();
                ((MainForm)this.TopLevelControl).setTreeNode();//更新主窗口的项目导航树
            }
        }

        private void richTextBox2_Leave(object sender, EventArgs e)
        {
            if (richTextBox2.Text.Length > 0 && dataGridView1[1, 1].Value != null && dataGridView1[1, 2].Value != null && dataGridView1[1, 3].Value != null && dataGridView1[1, 4].Value != null)
            {
                insertProject();
                updateProjectCount();
                ((MainForm)this.TopLevelControl).setTreeNode();//更新主窗口的项目导航树
            }
        }

        private void richTextBox3_Leave(object sender, EventArgs e)
        {
            if (richTextBox3.Text.Length > 0 && dataGridView1[1, 1].Value != null && dataGridView1[1, 2].Value != null && dataGridView1[1, 3].Value != null && dataGridView1[1, 4].Value != null)
            {
                insertProject();
                updateProjectCount();
                ((MainForm)this.TopLevelControl).setTreeNode();//更新主窗口的项目导航树
            }
        }

        private void richTextBox4_Leave(object sender, EventArgs e)
        {
            if (richTextBox4.Text.Length > 0 && dataGridView1[1, 1].Value != null && dataGridView1[1, 2].Value != null && dataGridView1[1, 3].Value != null && dataGridView1[1, 4].Value != null)
            {
                insertProject();
                updateProjectCount();
                ((MainForm)this.TopLevelControl).setTreeNode();//更新主窗口的项目导航树
            }
        }

        private void richTextBox5_Leave(object sender, EventArgs e)
        {
            if (richTextBox5.Text.Length > 0 && dataGridView1[1, 1].Value != null && dataGridView1[1, 2].Value != null && dataGridView1[1, 3].Value != null && dataGridView1[1, 4].Value != null)
            {
                insertProject();
                updateProjectCount();
                ((MainForm)this.TopLevelControl).setTreeNode();//更新主窗口的项目导航树
            }
        }




    }
}
