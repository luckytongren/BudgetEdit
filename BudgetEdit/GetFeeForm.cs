using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using MySql.Data.MySqlClient;
using Tools;
using System.IO;

namespace BudgetEdit
{
    public partial class GetFeeForm : Form
    {
        public string pNumber = null;
        public string bNumber = null;
        //public MySqlConnection sqlConn;
       // public MySqlCommand sqlCommand;
        public MySqlDataAdapter adapter;
        public DataTable dataTable;
        public DataTable dt;
        //public MySqlCommandBuilder builder;
        public DataRow dr;
        public enum MouseDirection
        {
            LEFT,
            RIGHT,
            MIDDLE
        };
        public MouseDirection mousedir;

        public string[] colName = { "序号", "标识","等级","费用代号", "费用项目", "费率", "金额", "计算基础", "不可竞争费", "计算说明", "所属标段" };
        public double money = 0.00;
        public GetFeeForm()
        {
            InitializeComponent();
        }

        public void HideDefaultCols()
        {
            c1FlexGrid1.Cols[1].Width = 0;                  //隐藏序号列
            c1FlexGrid1.Cols[2].Width = 0;                  //隐藏标识列
            c1FlexGrid1.Cols[3].Width = 0;                  //隐藏等级列
            c1FlexGrid1.Cols[11].Width = 0;                  //隐藏所属标段列
        }

        public void SetColName(string[] ColumnNames)
        {
            for (int j = 0; j < colName.Length; j++)
            {
                if (j != 5)
                {
                    c1FlexGrid1[0, j + 1] = colName[j];
                }
                else
                {
                    c1FlexGrid1[0, j + 1] = colName[j] + "%";
                }
            }
        }

        public void InsertNode(DataTable dt)
        {
            int level;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                level = Convert.ToInt32(dt.Rows[i].ItemArray[2]);
                c1FlexGrid1.Rows.InsertNode(i + 1, level);
            }
        }
        /********************************计算下一个兄弟节点的索引号*******************************************/
        public int GetNextBrotherNode(Node node)
        {
            if (node.NextNode != null)
            {
                return node.NextNode.Row.Index;
            }
            else
            {
                if (node.LastChild.Children == 0)
                {
                    return (node.LastChild.Row.Index + 1);
                }
                else
                {
                    return (GetNextBrotherNode(node.LastChild));
                }
            }
        }
        /********************************计算下一个兄弟节点的索引号*******************************************/

        public void setGetFeeDefault( string bidNumber)
        {
            c1FlexGrid1.ContextMenuStrip = contextMenuStrip1;
            string sql = "select * from get_fee  where 所属标段='" + bidNumber + "' order by 标识 asc";                       //正式确认时sql语句需添加where+bid_name分支以确定对应标段
            dataTable = new DataTable();
            adapter = SqlHelper.getSqlAdapter(sql);
            adapter.Fill(dataTable);
            bNumber = bidNumber;
            
            
            c1FlexGrid1.Tree.Column = 4;
            c1FlexGrid1.Cols.Count = 12;
            
            c1FlexGrid1.Rows[0].AllowEditing = false;
            c1FlexGrid1.Cols[0].AllowEditing = false;
            c1FlexGrid1.Cols[1].AllowEditing = false;
            c1FlexGrid1.Cols[2].AllowEditing = false;
            c1FlexGrid1.Cols[3].AllowEditing = false;
            c1FlexGrid1.Cols[0].Width = 35;
            c1FlexGrid1.Cols[1].Width = 50;
            c1FlexGrid1.Cols[2].Width = 50;
            c1FlexGrid1.Cols[3].Width = 50;
            c1FlexGrid1.Cols[4].Width = 80;
            c1FlexGrid1.Cols[5].Width = 170;
            c1FlexGrid1.Cols[6].Width = 80;
            c1FlexGrid1.Cols[7].Width = 150;
            c1FlexGrid1.Cols[8].Width = 300;
            c1FlexGrid1.Cols[9].Width = 80;
            c1FlexGrid1.Cols[10].Width = 240;
            c1FlexGrid1.Cols[11].Width = 80;
            c1FlexGrid1.Cols[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlexGrid1.Rows[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlexGrid1.Cols[9].DataType = typeof(bool);

            
            SetColName(colName);
            if (dataTable.Rows.Count == 0)
            {
                c1FlexGrid1.Rows.Count = 9;
                c1FlexGrid1.Rows.InsertNode(1, 0);
                c1FlexGrid1.Rows.InsertNode(2, 1);
                c1FlexGrid1.Rows.InsertNode(3, 1);
                c1FlexGrid1.Rows.InsertNode(4, 0);
                c1FlexGrid1.Rows.InsertNode(5, 1);
                c1FlexGrid1.Rows.InsertNode(6, 0);
                c1FlexGrid1.Rows.InsertNode(7, 0);
                c1FlexGrid1.Rows.InsertNode(8, 0);

                c1FlexGrid1[1, 8] = @"RGF+LWFBF";
                c1FlexGrid1[2, 8] = @"bp_bz_rgf_dj*bp_gcl";
                c1FlexGrid1[3, 8] = @"bp_zb_ysj*bp_gcl";
                c1FlexGrid1[4, 8] = @"bp_bz_clf_dj*bp_gcl-ZZCLF";
                c1FlexGrid1[5, 8] = @"bp_bz_clf_dj@ZZC*bp_gcl";
                c1FlexGrid1[6, 8] = @"bp_bz_jx_dj*bp_gcl";
                c1FlexGrid1[7, 8] = @"if($子目类型=20,bp_zm_dj*bp_gcl,0)";
                c1FlexGrid1[8, 8] = @"LWF+CLF+ZZCLF+JXF+QTF";
                
                string[] feeNumber = { "LWF", "RGF", "LWFBF", "CLF", "ZZCLF", "JXF", "QTF", "JAGCF" };
                string[] feePro = { "劳务费", "人工费", "劳务分包费", "材料费", "周转材料费", "机械费", "其他费", "建安工程费" };
                for (int i = 0; i < 8; i++)
                {

                    c1FlexGrid1.Rows[i].Height = this.Parent.Height / 27;
                    c1FlexGrid1[i + 1, 0] = string.Format("{0}", i + 1);
                   // c1FlexGrid1[i + 1, 1] = i + 1;
                    c1FlexGrid1[i + 1, 2] = i ;
                    if (i == 1 || i == 2 || i == 4)
                    {
                        c1FlexGrid1[i + 1, 3] = 1;
                    }
                    else
                    {
                        c1FlexGrid1[i + 1, 3] = 0;
                    }
                    c1FlexGrid1[i + 1, 4] = feeNumber[i];
                    c1FlexGrid1[i + 1, 5] = feePro[i];
                    c1FlexGrid1[i + 1, 6] = Convert.ToDouble("100");
                    c1FlexGrid1[i + 1, 7] = Convert.ToDouble("0.00").ToString("F2");
                    this.c1FlexGrid1.SetCellCheck(i + 1, 9, CheckEnum.Unchecked);
                    c1FlexGrid1[i + 1, 11] = bNumber ;
                }
                c1FlexGrid1.Rows[8].Height = this.Parent.Height / 27;
                
                for (int row = 1; row < 9; row++)
                {
                    dr = dataTable.NewRow();
                    dr[colName[1]] = (int)c1FlexGrid1[row, 2];
                    dr[colName[2]] = (int)c1FlexGrid1[row, 3];
                    dr[colName[3]] = (string)c1FlexGrid1[row, 4];
                    dr[colName[4]] = (string)c1FlexGrid1[row, 5];
                    dr[colName[5]] = Convert.ToDouble(c1FlexGrid1[row, 6]);
                    dr[colName[6]] = (string)c1FlexGrid1[row, 7];
                    dr[colName[7]] = (string)c1FlexGrid1[row, 8];
                    if (c1FlexGrid1.GetCellCheck(row, 9) == CheckEnum.Unchecked)
                    {

                        dr[colName[8]] = '0';
                    }
                    else
                    {
                        dr[colName[8]] = '1';
                    }
                    dr[colName[9]] = (string)c1FlexGrid1[row, 10];
                    dr[colName[10]] = (string)c1FlexGrid1[row, 11];
                    dataTable.Rows.Add(dr);
                }

               HideDefaultCols();
                try
                {
                    adapter.Update(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误信息！" + ex.Message);
                }
                finally
                {
                    dataTable.Clear();
                    adapter = SqlHelper.getSqlAdapter("select * from get_fee where 所属标段='" + bidNumber + "' order by 标识 asc");
                    adapter.Fill(dataTable);
                }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    c1FlexGrid1[i + 1, 1] = dataTable.Rows[i].ItemArray[0];
                }

                dataTable.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from get_fee where 所属标段='" + bidNumber + "'  order by 标识 asc");
                adapter.Fill(dataTable);

            }
            else
            {
                c1FlexGrid1.Rows.Count = dataTable.Rows.Count+1;
                HideDefaultCols();
                InsertNode(dataTable);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    c1FlexGrid1.Rows[i].Height = this.Parent.Height / 27;
                    c1FlexGrid1[i + 1, 0] = string.Format("{0}", i + 1);
                    for (int col = 0; col < colName.Length; col++)
                    {
                        if (col != 8)
                        {
                            c1FlexGrid1[i + 1, col + 1] = dataTable.Rows[i].ItemArray[col];
                        }
                        else
                        {
                            if (dataTable.Rows[i].ItemArray[col].ToString().Contains('0'))
                            {
                                c1FlexGrid1.SetCellCheck(i + 1, col+1, CheckEnum.Unchecked);
                            }
                            else
                            {
                                c1FlexGrid1.SetCellCheck(i + 1, col+1, CheckEnum.Checked);
                            }
                        }
                    }
                }
                c1FlexGrid1.Rows[dataTable.Rows.Count].Height = this.Parent.Height / 27;
            }
            
        }

        private void GetFeeForm_Load(object sender, EventArgs e)
        {
            //setGetFeeDefault("201501");
        }

        private void GetFeeForm_Shown(object sender, EventArgs e)
        {
            if (dataTable.Rows.Count == 0)
            {
                c1FlexGrid1.Rows.Count = 9;
            }
            else
            {
                c1FlexGrid1.Rows.Count = dataTable.Rows.Count +1;
            }
        }

        public void CalParentSum(int row, DataTable dt)
        {
            double sum = 0.00;
            Node father = c1FlexGrid1.Rows[row].Node.Parent;
            if (father == null)
            {
                if (c1FlexGrid1[row, 7] != null && c1FlexGrid1[row, 7].ToString().Length > 0)
                {
                    c1FlexGrid1[row, 7] = Convert.ToDouble(c1FlexGrid1[row, 7]).ToString("F2");
                    dt.Rows[row-1][6] = (string)c1FlexGrid1[row, 7];
                    return;
                }
                else
                {
                    c1FlexGrid1[row, 7] = sum.ToString("F2");
                    dt.Rows[row-1][6] = (string)c1FlexGrid1[row, 7];
                }
            }
            if (father.Level == 0)
            {
                foreach (Node child in father.Nodes)
                {
                    if (child.Row[7] != null && child.Row[7].ToString().Length > 0)
                    {
                        sum += Convert.ToDouble(child.Row[7].ToString());
                    }
                }
                c1FlexGrid1[father.Row.Index, 7] = Convert.ToDouble(sum.ToString()).ToString("F2");
                dt.Rows[father.Row.Index-1][6] = (string)c1FlexGrid1[father.Row.Index, 7];
            }
            else
            {
                foreach (Node child in father.Nodes)
                {
                    if (child.Row[7] != null && child.Row[7].ToString().Length > 0)
                    {
                        sum += Convert.ToDouble(child.Row[7].ToString());
                    }
                }
                c1FlexGrid1[father.Row.Index, 7] = Convert.ToDouble(sum.ToString()).ToString("F2");
                dt.Rows[father.Row.Index - 1][6] = (string)c1FlexGrid1[father.Row.Index, 7];
                CalParentSum(father.Row.Index, dt);
            }
                
        }

        public void AddChild(DataTable dt, int CurRow)                     //新增子费用项
        {
            DataRow dr = dt.NewRow();
            int old_count = dt.Rows.Count;
            Node curNode = this.c1FlexGrid1.Rows[CurRow].Node;
            int curChildren = curNode.Children;
            if (curChildren > 0)
            {
                int NextBrotherIndex = GetNextBrotherNode(curNode);
                curChildren = NextBrotherIndex - curNode.Row.Index - 1;
            }
            this.c1FlexGrid1.Rows.InsertNode(CurRow + curChildren + 1, curNode.Level + 1);
            c1FlexGrid1.Rows[CurRow + curChildren + 1].Height = this.Parent.Height / 27;
            c1FlexGrid1.Rows.Count += 1;
            /***********************更新DataTable*********************************/
            if (old_count > 0)
            {
                dr[colName[1]] = CurRow + curChildren;
                dr[colName[2]] = curNode.Level + 1;
                dr[colName[5]] = Convert.ToDouble("100");
                dr[colName[6]] = Convert.ToDouble("0.00").ToString("F2");
                dr[colName[8]] = '0';
                dr[colName[10]] = bNumber;

                for (int row = CurRow + curChildren; row < old_count; row++)
                {
                    dt.Rows[row][colName[1]] = Convert.ToInt32(dt.Rows[row][colName[1]].ToString()) + 1;
                }
                dt.Rows.Add(dr);
            }
            /***********************更新DataTable*********************************/

            try
            {
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("增加子费用项错误！" + ex.Message);
            }
            finally
            {
                dt.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from get_fee where 所属标段='" + bNumber + "'  order by 序号 asc");
                adapter.Fill(dt);
            }

            /***********************更新c1FlexGrid*********************************/
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                c1FlexGrid1[i + 1, 0] = string.Format("{0}", i + 1);        //更新flexgrid第0列序号列
            }
            this.c1FlexGrid1[CurRow + curChildren + 1, 1] = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0]);
            this.c1FlexGrid1[CurRow + curChildren + 1, 2] = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][1]);
            this.c1FlexGrid1[CurRow + curChildren + 1, 3] = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][2]);
            this.c1FlexGrid1[CurRow + curChildren + 1, 6] = Convert.ToDouble("100"); 
            this.c1FlexGrid1[CurRow + curChildren + 1, 7] = Convert.ToDouble("0.00").ToString("F2");
            if (dt.Rows[dt.Rows.Count - 1].ItemArray[8].ToString().Contains('0'))
            {
                c1FlexGrid1.SetCellCheck(CurRow + curChildren + 1, 9, CheckEnum.Unchecked);
            }
            else
            {
                c1FlexGrid1.SetCellCheck(CurRow + curChildren + 1, 9, CheckEnum.Checked);
            }
            this.c1FlexGrid1[CurRow + curChildren + 1, 11] = bNumber;
            for (int row = CurRow + curChildren + 2; row <= dt.Rows.Count; row++)
            {
                c1FlexGrid1[row, 2] = Convert.ToInt32(c1FlexGrid1[row, 2]) + 1;
            }
            /***********************更新c1FlexGrid*********************************/
            dt.Clear();
            adapter = SqlHelper.getSqlAdapter("select * from get_fee where 所属标段='" + bNumber + "'  order by 标识 asc");
            adapter.Fill(dt);
        }

        public void AddSameLevel(DataTable dt, int CurRow)           //新增同级费用项
        {
            DataRow dr = dt.NewRow();
            int old_count = dt.Rows.Count;
            Node curNode = this.c1FlexGrid1.Rows[CurRow].Node;
            int curChildren = curNode.Children;
            if (curChildren > 0)
            {
                int NextBrotherIndex = GetNextBrotherNode(curNode);
                curChildren = NextBrotherIndex - curNode.Row.Index - 1;
            }

            this.c1FlexGrid1.Rows.InsertNode(CurRow + curChildren + 1, curNode.Level);
            c1FlexGrid1.Rows.Count += 1;
            /***********************更新DataTable*********************************/
            if (old_count > 0)
            {
                dr[colName[1]] = CurRow + curChildren;
                dr[colName[2]] = curNode.Level;
                dr[colName[5]] = Convert.ToDouble("100");
                dr[colName[6]] = Convert.ToDouble("0.00").ToString("F2");
                dr[colName[8]] = '0';
                dr[colName[10]] = bNumber;

                for (int row = CurRow + curChildren; row < old_count; row++)
                {
                    dt.Rows[row][colName[1]] = Convert.ToInt32(dt.Rows[row][colName[1]].ToString()) + 1;
                }
                dt.Rows.Add(dr);
            }

            /***********************更新DataTable*********************************/

            try
            {
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("增加同级费用项错误！" + ex.Message);
            }
            finally
            {
                dt.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from get_fee where 所属标段='" + bNumber + "' order by 序号 asc");
                adapter.Fill(dt);
            }

            /***********************更新c1FlexGrid*********************************/
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                c1FlexGrid1[i + 1, 0] = string.Format("{0}", i + 1);        //更新flexgrid第0列序号列
            }
            this.c1FlexGrid1[CurRow + curChildren + 1, 1] = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0]);
            this.c1FlexGrid1[CurRow + curChildren + 1, 2] = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][1]);
            this.c1FlexGrid1[CurRow + curChildren + 1, 3] = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][2]);
            this.c1FlexGrid1[CurRow + curChildren + 1, 6] = Convert.ToDouble("100");
            this.c1FlexGrid1[CurRow + curChildren + 1, 7] = Convert.ToDouble("0.00").ToString("F2");
            if (dt.Rows[dt.Rows.Count - 1].ItemArray[8].ToString().Contains('0'))
            {
                c1FlexGrid1.SetCellCheck(CurRow + curChildren + 1, 9, CheckEnum.Unchecked);
            }
            else
            {
                c1FlexGrid1.SetCellCheck(CurRow + curChildren + 1, 9, CheckEnum.Checked);
            }
            this.c1FlexGrid1[CurRow + curChildren + 1, 11] = bNumber;
            for (int row = CurRow + curChildren + 2; row <= dt.Rows.Count; row++)
            {
                c1FlexGrid1[row, 2] = Convert.ToInt32(c1FlexGrid1[row, 2]) + 1;
            }
            /***********************更新c1FlexGrid*********************************/
            dt.Clear();
            adapter = SqlHelper.getSqlAdapter("select * from get_fee where 所属标段='" + bNumber + "'  order by 标识 asc");
            adapter.Fill(dt);
        }

        public void DeleteRow(DataTable dt, int CurRow)
        {
            int old_count = dt.Rows.Count;
            Node curNode = this.c1FlexGrid1.Rows[CurRow].Node;
            Node parent = curNode.Parent;
            int curChildren = curNode.Children;
            if (curChildren > 0)
            {
                int NextBrotherIndex = GetNextBrotherNode(curNode);
                curChildren = NextBrotherIndex - curNode.Row.Index - 1;
            }

            for (int row = CurRow + curChildren; row < dt.Rows.Count; row++)
            {
                dt.Rows[row][colName[1]] = Convert.ToInt32(dt.Rows[row][colName[1]].ToString()) - (curChildren + 1);
            }
            /*******************************删除c1flexgrid/DataTable行**********************************************/
            for (int removeRow = CurRow; removeRow <= CurRow + curChildren; removeRow++)
            {
                c1FlexGrid1.Rows.Remove(CurRow);
                dt.Rows[removeRow - 1].Delete();
            }
            /*******************************删除c1flexgrid/DataTable行**********************************************/
            try
            {
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除行后更新数据库错误！" + ex.Message);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                c1FlexGrid1[i + 1, 0] = string.Format("{0}", i + 1);
            }

            for (int row = CurRow; row <= dt.Rows.Count; row++)
            {
                c1FlexGrid1[row, 1] = Convert.ToInt32(dt.Rows[row - 1].ItemArray[0]);
                c1FlexGrid1[row, 2] = Convert.ToInt32(dt.Rows[row - 1].ItemArray[1]);
                c1FlexGrid1[row, 3] = Convert.ToInt32(dt.Rows[row - 1].ItemArray[2]);
            }
            CalParentSum(CurRow-1, dt);           //每次删除完更新所有父节点金额
            try
            {
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除行错误！" + ex.Message);
            }
        }

        public void ExportToExcel(DataTable dt)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + @"\模板\取费汇总模板.xlsx"))
            {

                if (!System.IO.Directory.Exists(Directory.GetCurrentDirectory() + @"\导出数据"))
                {
                    System.IO.Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\导出数据");

                }
                string sourcePath = Directory.GetCurrentDirectory() + @"\模板\取费汇总模板.xlsx";
                string targetPath = Directory.GetCurrentDirectory() + @"\导出数据\" + bNumber + "取费汇总.xlsx";
                File.Copy(sourcePath, targetPath, true);
                SqlHelper.TableToExcel(dt, targetPath);
                MessageBox.Show(bNumber + "取费汇总导出成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            else
            {
                MessageBox.Show("取费汇总模板不存在！", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void c1FlexGrid1_AfterEdit(object sender, RowColEventArgs e)
        {
            dataTable.Rows[e.Row - 1][colName[0]] = (int)c1FlexGrid1[e.Row, 1];
            dataTable.Rows[e.Row - 1][colName[1]] = (int)c1FlexGrid1[e.Row, 2];
            dataTable.Rows[e.Row - 1][colName[2]] = (int)c1FlexGrid1[e.Row, 3];

            if (c1FlexGrid1[e.Row, 4] != null && c1FlexGrid1[e.Row, 4].ToString().Length > 0)
            {
                dataTable.Rows[e.Row - 1][colName[3]] = (string)c1FlexGrid1[e.Row, 4];
            }

            if (c1FlexGrid1[e.Row, 5] != null && c1FlexGrid1[e.Row, 5].ToString().Length > 0)
            {
                dataTable.Rows[e.Row - 1][colName[4]] = (string)c1FlexGrid1[e.Row, 5];
            }

            if (c1FlexGrid1[e.Row, 6] != null && c1FlexGrid1[e.Row, 6].ToString().Length > 0)
            {
                dataTable.Rows[e.Row - 1][colName[5]] = Convert.ToDouble(c1FlexGrid1[e.Row, 6]);
            }

            if (c1FlexGrid1[e.Row, 7] != null && c1FlexGrid1[e.Row, 7].ToString().Length > 0)
            {
                c1FlexGrid1[e.Row, 7] = Convert.ToDouble(c1FlexGrid1[e.Row, 7]).ToString("F2");
                dataTable.Rows[e.Row - 1][colName[6]] = c1FlexGrid1[e.Row, 7];
            }

            if (c1FlexGrid1[e.Row, 8] != null && c1FlexGrid1[e.Row, 8].ToString().Length > 0)
            {
                dataTable.Rows[e.Row - 1][colName[7]] = (string)c1FlexGrid1[e.Row, 8];
            }

            if (c1FlexGrid1.GetCellCheck(e.Row, 9) == CheckEnum.Unchecked)
            {
                dataTable.Rows[e.Row - 1][colName[8]] = '0';
            }
            else
            {
                dataTable.Rows[e.Row - 1][colName[8]] = '1';
            }

            if (c1FlexGrid1[e.Row, 10] != null && c1FlexGrid1[e.Row, 10].ToString().Length > 0)
            {
                dataTable.Rows[e.Row - 1][colName[9]] = (string)c1FlexGrid1[e.Row, 10];
            }

            if (c1FlexGrid1[e.Row, 11] != null && c1FlexGrid1[e.Row, 11].ToString().Length > 0)
            {
                dataTable.Rows[e.Row - 1][colName[10]] = (string)c1FlexGrid1[e.Row, 11];
            }

            CalParentSum(e.Row, dataTable);
            try
            {
                adapter.Update(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("编辑异常！" + ex.Message);
            }
            finally
            {
                dataTable.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from get_fee where 所属标段='"+bNumber +"' order by 标识 asc");
                adapter.Fill(dataTable);
            }
        }

        private void contextMenuStrip1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mousedir = MouseDirection.LEFT;
            }
            else if (e.Button == MouseButtons.Right)
            {
                mousedir = MouseDirection.RIGHT;
            }
            else
            {
                mousedir = MouseDirection.MIDDLE;
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (mousedir == MouseDirection.LEFT)
            {
                switch (e.ClickedItem.Text)
                {
                    case "新增同级费用项":
                        AddSameLevel(dataTable, c1FlexGrid1.RowSel);
                        break;
                    case "新增子费用项":
                        AddChild(dataTable, c1FlexGrid1.RowSel);
                        break;
                    case "删除":
                        DeleteRow(dataTable, c1FlexGrid1.RowSel);
                        break;
                    case "导出":
                        ExportToExcel(dataTable);
                        break;
                    default:
                        break;
                }
            }
        }

        private void c1FlexGrid1_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            if (e.Col == 7 || e.Col == 6)
            {
                //如果输入不在0-9、PERIOD、DELETE或BACKSPACE之间 
                if (!((e.KeyChar == 48) || (e.KeyChar == 49) || (e.KeyChar == 50) || (e.KeyChar == 51) ||
                    (e.KeyChar == 52) || (e.KeyChar == 53) || (e.KeyChar == 54) || (e.KeyChar == 55) ||
                    (e.KeyChar == 56) || (e.KeyChar == 57) || (e.KeyChar == 46) || (e.KeyChar == 127) || (e.KeyChar == 8))
                    )
                {
                    //禁止输入非法的按键值到控件上 e.Handled = true;
                    e.Handled = true;
                }
            }
        }
    }
   
}
