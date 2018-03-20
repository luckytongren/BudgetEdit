using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tools;
using MySql.Data.MySqlClient;
using C1.Win.C1FlexGrid;
using System.IO;

namespace BudgetEdit
{
    public partial class OtherFeeForm : Form
    {
        public string pNumber = null;
        public string bNumber = null;
        //public MySqlConnection sqlConn;
        // public MySqlCommand sqlCommand;
        public MySqlDataAdapter adapter;
        public DataTable dataTable;
        public List<DataRow> dtPaste = new List<DataRow>();   //剪切专用
        public DataTable dt;
        //public MySqlCommandBuilder builder;
        public DataRow dr;
        public DataRow global_dr;
        public int add_root_child_count = 1;
        public int add_root_brother_count = 1;
        public string[] feeName =
        {
            "其他工程费","冬雨季施工增加费","夜间施工增加费","施工进退场费","生产工具用具使用费",
            "检验试验费","检验试验费","试验、测量设备仪器折旧费","试验、测量设备仪器修理费","安全生产费",
            "文明施工费","工程定位点交清理费","工程保护费","工程保修费","工程保险费",
            "代理咨询费","二次搬运费","调遣费","监理费","租赁费",
            "临时设施费","技术服务费","环境检测费","围堰费","其他"
        };
        public string[] colName = 
        { 
            "序号", "标识", "父亲", "编号", "费用名称", "单位", "数量", "单价", "金额","备注","所属标段"
        };
        public string[] zjName =
        {
            "序号", "标识", "父亲", "编号", "仪器名称", "数量", "单价", "原值", "月折旧率", "使用时间", "折旧费", "代号", "备注","所属标段"
        };
        public string[] lsName =
        {
            "序号", "标识", "父亲", "编号", "费用项目", "单位", "数量", "单价", "合价", "代号", "备注","所属标段"
        };
        public enum MouseDirection
        {
            LEFT,
            RIGHT,
            MIDDLE
        };
        public MouseDirection mousedir;

        public enum CurrentItem
        {
            QTGCF,                  //其他工程费
            SBYQZJF,                //设施仪器折旧费
            LSSSF                   //临时设施费
        };
        public CurrentItem CurrentFee;
        public bool IsCopy = false;         //复制标识
        public bool IsPaste = false;        //剪切标识

        public OtherFeeForm()
        {
            InitializeComponent();
        }
        public void SetOtherFeeDefault(string bidNumber)
        {
            CurrentFee = CurrentItem.QTGCF;
            string sql = "select * from other_fee where 所属标段='"+bidNumber+"' order by 标识 asc";                       //正式确认时sql语句需添加where+bid_name分支以确定对应标段
            dataTable = new DataTable();
            adapter = SqlHelper.getSqlAdapter(sql);
            adapter.Fill(dataTable);
            bNumber = bidNumber;

            c1FlexGrid1.Clear();
            if (c1FlexGrid1.Rows.Count > 1)
            {
                c1FlexGrid1.Rows.RemoveRange(1, c1FlexGrid1.Rows.Count - 1);
            }
            c1FlexGrid1.AutoClipboard = true;
            c1FlexGrid1.Cols[9].AllowEditing = false;
            c1FlexGrid1.Tree.Column = 4;
            c1FlexGrid1.Cols.Count = 12;
            /************************************************从shown方法中复制过来*******************************************/
            //c1FlexGrid1.Rows.Count = 26;

            c1FlexGrid1.Styles.Normal.WordWrap = true;
            this.c1FlexGrid1.AutoSizeRows();
            c1FlexGrid1.Rows[0].AllowEditing = false;
            c1FlexGrid1.Cols[0].AllowEditing = false;
            c1FlexGrid1.Cols[0].Width = 35;
            //c1FlexGrid1.Cols[1].Width = 60;
            //c1FlexGrid1.Cols[2].Width = 60;
            // c1FlexGrid1.Cols[3].Width = 60;
            c1FlexGrid1.Cols[4].Width = 120;
            c1FlexGrid1.Cols[5].Width = 140;
            c1FlexGrid1.Cols[6].Width = 60;
            c1FlexGrid1.Cols[7].Width = 140;
            c1FlexGrid1.Cols[8].Width = 80;
            c1FlexGrid1.Cols[9].Width = 100;
            c1FlexGrid1.Cols[10].Width = 150;
            //  c1FlexGrid1.Cols[11].Width = 80;
            c1FlexGrid1.Cols[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlexGrid1.Rows[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            /************************************************从shown方法中复制过来*******************************************/

            SetColName(colName);            //设置其他工程费表的列名称

            if (dataTable.Rows.Count == 0)
            {
               // c1FlexGrid1.Rows.Add(25);
                for (int index = 0; index < 25; index++)
                {
                    switch (index)
                    {
                        case 0:
                            c1FlexGrid1.Rows.InsertNode(index+1, 0);
                            break;
                        case 6:
                        case 7:
                        case 8:
                            c1FlexGrid1.Rows.InsertNode(index + 1, 2);
                            break;
                        default:
                            c1FlexGrid1.Rows.InsertNode(index+1, 1);
                            break;
                    }
                   
                }
                for (int i = 0; i < 25; i++)
                {
                    if (i == 0 || i == 5)
                    {
                        c1FlexGrid1.SetCellImage(i+1, 4, imageList1.Images[1]);
                    }
                    else
                    {
                        c1FlexGrid1.SetCellImage(i+1, 4, imageList1.Images[0]);
                    }
                    if (i == 0 || i == 7 || i == 20)
                    {
                        if (i == 0)
                        {
                            c1FlexGrid1[i + 1, 4] = "QTGCF";
                        }
                        else if(i==7)
                        {
                            c1FlexGrid1[i + 1, 4] = "SBYQZJF";
                        }
                        else
                        {
                            c1FlexGrid1[i + 1, 4] = "LSF";
                        }
                    }
                    c1FlexGrid1[i + 1, 0] = string.Format("{0}", i + 1);
                    //c1FlexGrid1[i + 1, 1] = i + 1;
                    c1FlexGrid1[i + 1, 2] = i;
                    if (i == 0)
                    {
                        c1FlexGrid1[i + 1, 3] = -1;
                    }
                    else if (i == 6 || i == 7 || i == 8)
                    {
                        c1FlexGrid1[i + 1, 3] = 5;
                    }
                    else
                    {
                        c1FlexGrid1[i + 1, 3] = 0;
                    }
                    c1FlexGrid1[i + 1, 6] = "元";
                    c1FlexGrid1[i + 1, 5] = feeName[i];
                    c1FlexGrid1[i + 1, 8] = Convert.ToDouble("100").ToString("F2");

                    c1FlexGrid1[i + 1, 11] = bNumber;
                }
                for (int row = 1; row < 26; row++)
                {
                    dr = dataTable.NewRow();
                   // dr[colName[0]] = (int)c1FlexGrid1[row, 1];
                    dr[colName[1]] = (int)c1FlexGrid1[row, 2];
                    dr[colName[2]] = (int)c1FlexGrid1[row, 3];
                    dr[colName[3]] = (string)(c1FlexGrid1[row, 4]);
                    dr[colName[4]] = (string)c1FlexGrid1[row, 5];
                    dr[colName[5]] = (string)c1FlexGrid1[row, 6];
                    dr[colName[6]] = (string)c1FlexGrid1[row, 7];
                    dr[colName[7]] = (string)c1FlexGrid1[row, 8];
                    dr[colName[8]] = (string)c1FlexGrid1[row, 9];
                    dr[colName[9]] = (string)c1FlexGrid1[row, 10];
                    dr[colName[10]] = (string)c1FlexGrid1[row, 11];
                    dataTable.Rows.Add(dr);
                }

                HideDefaultCols();          //隐藏缺省列
                c1FlexGrid1.Rows[1].AllowEditing = false;
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
                    adapter = SqlHelper.getSqlAdapter("select * from other_fee where 所属标段='"+bidNumber+"' order by 标识 asc");
                    adapter.Fill(dataTable);
                }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    c1FlexGrid1[i + 1, 1] = dataTable.Rows[i].ItemArray[0];
                }

                dataTable.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from other_fee where 所属标段='" + bidNumber + "'  order by 标识 asc");
                adapter.Fill(dataTable);
            }
            else
            {
                //c1FlexGrid1.Rows.Add(dataTable.Rows.Count);
                //SetColName(colName);            //设置其他工程费表的列名称
 
                HideDefaultCols();          //隐藏缺省列

                InsertNode(dataTable);          //插入节点列

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    c1FlexGrid1[i + 1, 0] = string.Format("{0}", i + 1);
                    for (int col = 0; col < colName.Length; col++)
                    {
                        c1FlexGrid1[i + 1, col + 1] = dataTable.Rows[i].ItemArray[col];
                    }

                }

                /***********************************计算折旧费和临时费所有父节点金额列值*****************************************/
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (dataTable.Rows[i].ItemArray[3] != null && dataTable.Rows[i].ItemArray[3].ToString().Length>0)
                    {
                        if (String.Compare(Convert.ToString(dataTable.Rows[i].ItemArray[3]),"SBYQZJF") == 0)
                        {
                            CalParentSum(i+1, dataTable);
                        }
                        if (String.Compare(Convert.ToString(dataTable.Rows[i].ItemArray[3]),"LSF") == 0)
                        {
                            CalParentSum(i+1, dataTable);
                        }
                    }
                }
                /***********************************计算折旧费和临时费所有父节点金额列值*****************************************/
                c1FlexGrid1.Rows[1].AllowEditing = false;//根节点不能编辑
                try
                {
                    adapter.Update(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误信息！" + ex.Message);
                }
            }
        }

        private void OtherFeeForm_Load(object sender, EventArgs e)
        {
            //SetOtherFeeDefault("201501");
        }

        private void OtherFeeForm_Shown(object sender, EventArgs e)
        {
            //if (dataTable.Rows.Count == 0)
            //{
            //    if (CurrentFee == CurrentItem.QTGCF)
            //    {
            //        c1FlexGrid1.Rows.Count = 25;
            //    }
            //    else if (CurrentFee == CurrentItem.SBYQZJF)
            //    {
            //        c1FlexGrid1.Rows.Count = 2;
            //    }
            //    else
            //    {
            //        c1FlexGrid1.Rows.Count = 2;
            //    }
            //}
            //else
            //{
            //    c1FlexGrid1.Rows.Count = dataTable.Rows.Count;
            //}
            
        }

        public void SetColName(string[] Cols)
        {
            for (int j = 0; j < Cols.Length; j++)
            {
                if (j == 6)
                {
                    c1FlexGrid1[0, j + 1] = Cols[j] + "(或计算基数)";
                }
                else if (j == 7)
                {
                    c1FlexGrid1[0, j + 1] = Cols[j] + "(费率)";
                }
                else if (j == 8)
                {
                    c1FlexGrid1[0, j + 1] = Cols[j] + "(元)";
                }
                else
                {
                    c1FlexGrid1[0, j + 1] = Cols[j];
                }
            }
        }

        public void SetZjColName(string[] Cols)
        {
            for (int j = 0; j < Cols.Length; j++)
            {
                if (j == 8)
                {
                    c1FlexGrid1[0, j + 1] = Cols[j] + "%";
                }
                else if (j == 9)
                {
                    c1FlexGrid1[0, j + 1] = Cols[j] + "(月)";
                }
                else
                {
                    c1FlexGrid1[0, j + 1] = Cols[j];
                }
            }
        }

        public void SetLsColName(string[] Cols)
        {
            for (int j = 0; j < Cols.Length; j++)
            {
                if(j == 6)
                {
                    c1FlexGrid1[0, j + 1] = Cols[j] + "(或计算基数总额)";
                }
                else if(j == 7)
                {
                    c1FlexGrid1[0, j + 1] = Cols[j] + "(或摊销率)";
                }
                else if(j == 8)
                {
                    c1FlexGrid1[0, j + 1] = Cols[j] + "(或摊销金额)";
                }
                else
                {
                    c1FlexGrid1[0, j + 1] = Cols[j];
                }
            }
        }
        public void HideDefaultCols()
        {
            c1FlexGrid1.Cols[1].Width = 0;
            c1FlexGrid1.Cols[2].Width = 0;
            c1FlexGrid1.Cols[3].Width = 0;
            if (CurrentFee == CurrentItem.QTGCF)
            {
                c1FlexGrid1.Cols[11].Width = 0;
            }
            else if (CurrentFee == CurrentItem.SBYQZJF)
            {
                c1FlexGrid1.Cols[14].Width = 0;
            }
            else
            {
                c1FlexGrid1.Cols[12].Width = 0;
            }
        }

        public void InsertNode(DataTable dt)                            //更新dataTable的所有节点行
        {
            int node = -1;
            int pre_node = -1;
            int leaf_node = 2;
            int count = 0;
            List<int> nodeArray = new List<int>();
            List<int> nodeCmp = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                node = Convert.ToInt32(dt.Rows[i].ItemArray[2].ToString());
                nodeArray.Insert(i, node);
                if(node == -1)
                {
                    c1FlexGrid1.Rows.InsertNode(i + 1, 0);
                }
                else if (node == 0)
                {
                    c1FlexGrid1.Rows.InsertNode(i + 1, 1);
                    leaf_node = 2;
                }
                else
                {
                    if (node == pre_node || pre_node == -1)
                    {
                        c1FlexGrid1.Rows.InsertNode(i + 1, leaf_node);
                    }
                    else if (node > pre_node)
                    {
                        if (pre_node == 0)
                        {
                            c1FlexGrid1.Rows.InsertNode(i + 1, leaf_node);
                        }
                        else
                        {
                            c1FlexGrid1.Rows.InsertNode(i + 1, ++leaf_node);
                        }
                    }
                    else
                    {
                        //leaf_node -= (pre_node - node);
                        leaf_node = c1FlexGrid1.Rows[node + 1].Node.Level + 1;
                        c1FlexGrid1.Rows.InsertNode(i + 1, leaf_node);
                    }
                    
                }
                pre_node = node;
            }
            /*********************设置所有节点图标*****************************/
            for (int index = 0; index < nodeArray.Count; index++)
            {
                c1FlexGrid1.SetCellImage(index + 1, 4, imageList1.Images[0]);
                if (nodeArray[index] != -1)
                {
                    if (!nodeCmp.Contains(nodeArray[index]))
                    {
                        nodeCmp.Insert(count++, nodeArray[index]);
                        c1FlexGrid1.SetCellImage(nodeArray[index]+1, 4, imageList1.Images[1]);
                    }
                }
            }
            /*********************设置所有节点图标*****************************/
            nodeArray.Clear();
            nodeCmp.Clear();
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

        public void AddChildOther(DataTable dt, int CurRow)                     //增加其他工程费子项
        {
            DataRow dr = dt.NewRow();
            int old_count = dt.Rows.Count;
            Node curNode = this.c1FlexGrid1.Rows[CurRow].Node;
            int curChildren = curNode.Children;
            int cur_row_parent = Convert.ToInt32(c1FlexGrid1[CurRow, 3]);           //选中行的“父亲”列值
            if (curChildren > 0)
            {
                int NextBrotherIndex = GetNextBrotherNode(curNode);
                curChildren = NextBrotherIndex - curNode.Row.Index - 1;
            }
            this.c1FlexGrid1.Rows.InsertNode(CurRow + curChildren + 1, curNode.Level + 1);

            if (curChildren > 0)
            {
                this.c1FlexGrid1.Rows[CurRow + curChildren + 1].Node.Image = imageList1.Images[0];
            }
            else
            {
                this.c1FlexGrid1.Rows[CurRow].Node.Image = imageList1.Images[1];
                this.c1FlexGrid1.Rows[CurRow + curChildren + 1].Node.Image = imageList1.Images[0];
            }

            /***********************更新DataTable*********************************/
            if (old_count > 0)
            {
                // dr[colName[0]] = Convert.ToInt32(dt.Rows[old_count - 1].ItemArray[0].ToString()) + (add_root_child_count++);

                dr[colName[1]] = CurRow + curChildren;
                dr[colName[2]] = CurRow - 1;
                //dr[colName[3]] = "";
                //dr[colName[4]] = "";
                dr[colName[5]] = "元";
                // dr[colName[6]] = "";
                dr[colName[7]] = Convert.ToDouble("100").ToString("F2");
                //dr[colName[8]] = "";
                // dr[colName[9]] = "";
                dr[colName[10]] = bNumber;

                for (int row = CurRow + curChildren; row < old_count; row++)
                {
                    dt.Rows[row][colName[1]] = Convert.ToInt32(dt.Rows[row][colName[1]].ToString()) + 1;

                    if (Convert.ToInt32(dt.Rows[row][colName[2]].ToString()) > cur_row_parent)
                    {
                        dt.Rows[row][colName[2]] = Convert.ToInt32(dt.Rows[row][colName[2]].ToString()) + 1;
                    }
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
                MessageBox.Show("增加子项错误！" + ex.Message);
            }
            finally
            {
                dt.Clear();
                //   adapter = SqlHelper.getSqlAdapter("select * from other_fee where 序号=(select max(序号) from other_fee)");
                adapter = SqlHelper.getSqlAdapter("select * from other_fee where 所属标段='" + bNumber + "'  order by 序号 asc");
                adapter.Fill(dt);
            }

            /***********************更新c1FlexGrid*********************************/
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                c1FlexGrid1[i + 1, 0] = string.Format("{0}", i + 1);        //更新flexgrid第0列序号列
            }
            // this.c1FlexGrid1[CurRow + curChildren + 1, 1] = Convert.ToInt32(dr[colName[0]]);
            // this.c1FlexGrid1[CurRow + curChildren + 1, 2] = Convert.ToInt32(dr[colName[1]]);
            // this.c1FlexGrid1[CurRow + curChildren + 1, 3] = Convert.ToInt32(dr[colName[2]]);
            this.c1FlexGrid1[CurRow + curChildren + 1, 1] = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0]);
            this.c1FlexGrid1[CurRow + curChildren + 1, 2] = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][1]);
            this.c1FlexGrid1[CurRow + curChildren + 1, 3] = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][2]);
            this.c1FlexGrid1[CurRow + curChildren + 1, 6] = "元";
            this.c1FlexGrid1[CurRow + curChildren + 1, 8] = Convert.ToDouble("100").ToString("F2");
            this.c1FlexGrid1[CurRow + curChildren + 1, 11] = bNumber;
            for (int row = CurRow + curChildren + 2; row <= dt.Rows.Count; row++)
            {
                c1FlexGrid1[row, 2] = Convert.ToInt32(c1FlexGrid1[row, 2]) + 1;

                if (Convert.ToInt32(c1FlexGrid1[row, 3]) > cur_row_parent)
                {
                    c1FlexGrid1[row, 3] = Convert.ToInt32(c1FlexGrid1[row, 3]) + 1;
                }
            }
            /***********************更新c1FlexGrid*********************************/
            dt.Clear();
            adapter = SqlHelper.getSqlAdapter("select * from other_fee where 所属标段='" + bNumber + "'  order by 标识 asc");
            adapter.Fill(dt);
        }

        public void AddChildZj(DataTable dt, int CurRow)                    //增加设备仪器折旧费子项
        {
            DataRow dr = dt.NewRow();
            int old_count = dt.Rows.Count;
            Node curNode = this.c1FlexGrid1.Rows[CurRow].Node;
            int curChildren = curNode.Children;
            int cur_row_parent = Convert.ToInt32(c1FlexGrid1[CurRow, 3]);           //选中行的“父亲”列值
            if (curChildren > 0)
            {
                int NextBrotherIndex = GetNextBrotherNode(curNode);
                curChildren = NextBrotherIndex - curNode.Row.Index - 1;
            }
            this.c1FlexGrid1.Rows.InsertNode(CurRow + curChildren + 1, curNode.Level + 1);

            if (curChildren > 0)
            {
                this.c1FlexGrid1.Rows[CurRow + curChildren + 1].Node.Image = imageList1.Images[0];
            }
            else
            {
                this.c1FlexGrid1.Rows[CurRow].Node.Image = imageList1.Images[1];
                this.c1FlexGrid1.Rows[CurRow + curChildren + 1].Node.Image = imageList1.Images[0];
            }

            /***********************更新DataTable*********************************/
            if (old_count > 0)
            {
                dr[zjName[1]] = CurRow + curChildren;
                dr[zjName[2]] = CurRow - 1;
                dr[zjName[5]] = "0";
                dr[zjName[9]] = Convert.ToDouble("0.00").ToString("F2");
                dr[zjName[13]] = bNumber;

                for (int row = CurRow + curChildren; row < old_count; row++)
                {
                    dt.Rows[row][zjName[1]] = Convert.ToInt32(dt.Rows[row][zjName[1]].ToString()) + 1;

                    if (Convert.ToInt32(dt.Rows[row][zjName[2]].ToString()) > cur_row_parent)
                    {
                        dt.Rows[row][zjName[2]] = Convert.ToInt32(dt.Rows[row][zjName[2]].ToString()) + 1;
                    }
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
                MessageBox.Show("增加子项错误！" + ex.Message);
            }
            finally
            {
                dt.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from yqzj_fee where 所属标段='" + bNumber + "'  order by 序号 asc");
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
            this.c1FlexGrid1[CurRow + curChildren + 1, 6] = "0";
            this.c1FlexGrid1[CurRow + curChildren + 1, 10] = Convert.ToDouble("0.00").ToString("F2");
            this.c1FlexGrid1[CurRow + curChildren + 1, 14] = bNumber;
            for (int row = CurRow + curChildren + 2; row <= dt.Rows.Count; row++)
            {
                c1FlexGrid1[row, 2] = Convert.ToInt32(c1FlexGrid1[row, 2]) + 1;

                if (Convert.ToInt32(c1FlexGrid1[row, 3]) > cur_row_parent)
                {
                    c1FlexGrid1[row, 3] = Convert.ToInt32(c1FlexGrid1[row, 3]) + 1;
                }
            }
            /***********************更新c1FlexGrid*********************************/
            dt.Clear();
            adapter = SqlHelper.getSqlAdapter("select * from yqzj_fee where 所属标段='" + bNumber + "'  order by 标识 asc");
            adapter.Fill(dt);
        }

        public void AddChildLs(DataTable dt, int CurRow)                            //增加临时设施费子项
        {
            DataRow dr = dt.NewRow();
            int old_count = dt.Rows.Count;
            Node curNode = this.c1FlexGrid1.Rows[CurRow].Node;
            int curChildren = curNode.Children;
            int cur_row_parent = Convert.ToInt32(c1FlexGrid1[CurRow, 3]);           //选中行的“父亲”列值
            if (curChildren > 0)
            {
                int NextBrotherIndex = GetNextBrotherNode(curNode);
                curChildren = NextBrotherIndex - curNode.Row.Index - 1;
            }
            this.c1FlexGrid1.Rows.InsertNode(CurRow + curChildren + 1, curNode.Level + 1);

            if (curChildren > 0)
            {
                this.c1FlexGrid1.Rows[CurRow + curChildren + 1].Node.Image = imageList1.Images[0];
            }
            else
            {
                this.c1FlexGrid1.Rows[CurRow].Node.Image = imageList1.Images[1];
                this.c1FlexGrid1.Rows[CurRow + curChildren + 1].Node.Image = imageList1.Images[0];
            }

            /***********************更新DataTable*********************************/
            if (old_count > 0)
            {
                dr[lsName[1]] = CurRow + curChildren;
                dr[lsName[2]] = CurRow - 1;
                dr[lsName[11]] = bNumber;

                for (int row = CurRow + curChildren; row < old_count; row++)
                {
                    dt.Rows[row][lsName[1]] = Convert.ToInt32(dt.Rows[row][lsName[1]].ToString()) + 1;

                    if (Convert.ToInt32(dt.Rows[row][lsName[2]].ToString()) > cur_row_parent)
                    {
                        dt.Rows[row][lsName[2]] = Convert.ToInt32(dt.Rows[row][lsName[2]].ToString()) + 1;
                    }
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
                MessageBox.Show("增加子项错误！" + ex.Message);
            }
            finally
            {
                dt.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from linshi_fee where 所属标段='" + bNumber + "'  order by 序号 asc");
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
            this.c1FlexGrid1[CurRow + curChildren + 1, 12] = bNumber;
            for (int row = CurRow + curChildren + 2; row <= dt.Rows.Count; row++)
            {
                c1FlexGrid1[row, 2] = Convert.ToInt32(c1FlexGrid1[row, 2]) + 1;

                if (Convert.ToInt32(c1FlexGrid1[row, 3]) > cur_row_parent)
                {
                    c1FlexGrid1[row, 3] = Convert.ToInt32(c1FlexGrid1[row, 3]) + 1;
                }
            }
            /***********************更新c1FlexGrid*********************************/
            dt.Clear();
            adapter = SqlHelper.getSqlAdapter("select * from linshi_fee where 所属标段='" + bNumber + "'  order by 标识 asc");
            adapter.Fill(dt);
        }

        public void AddChildRow(DataTable dt, int CurRow)               //增加子项
        {
            switch (CurrentFee)
            {
                case CurrentItem.QTGCF:
                    AddChildOther(dt, CurRow);
                    break;
                case CurrentItem.SBYQZJF:
                    AddChildZj(dt, CurRow);
                    break;
                case CurrentItem.LSSSF:
                    AddChildLs(dt, CurRow);
                    break;
                default:
                    MessageBox.Show("增加子项错误！");
                    break;
            }
           
        }

        public void CtrlvRow(DataTable dt, int CurRow)                   //行复制
        {
            switch (CurrentFee)
            {
                case CurrentItem.QTGCF:
                    CtrlvOtherRow(dt, CurRow);
                    break;
                case CurrentItem.SBYQZJF:
                    CtrlvZjRow(dt, CurRow);
                    break;
                case CurrentItem.LSSSF:
                    CtrlvLsRow(dt, CurRow);
                    break;
                default:
                    MessageBox.Show("行复制错误！");
                    break;
            }
        }
        public void CopyRowTemp(DataTable dt, int CurRow)                  //复制行列表
        {
            Node curNode = c1FlexGrid1.Rows[CurRow].Node;
            int curChildren = curNode.Children;
            if (curChildren > 0)
            {
                int NextBrotherIndex = GetNextBrotherNode(curNode);
                curChildren = NextBrotherIndex - curNode.Row.Index - 1;
            }
            for (int row = CurRow - 1,index = 0; row < CurRow + curChildren; row++,index++)
            {
                DataRow dr_paste = dt.NewRow();
                for (int item = 0; item < dt.Columns.Count ; item++)
                {
                    dr_paste[item] = dt.Rows[row].ItemArray[item];
                }
                dtPaste.Insert(index, dr_paste);
            }
        }

        public void updatePasteList(int index, int children)
        {
            int father, flag, temp, old_flag ;
            father = Convert.ToInt32(c1FlexGrid1.Rows[index][2]);               //获取当前行标识，即为要插入行链表首行的父亲
            flag = Convert.ToInt32(c1FlexGrid1.Rows[index +children][2] );         //获取当前节点最后一个孩子节点的标识
            old_flag = Convert.ToInt32(dtPaste[0].ItemArray[1]);                    //获取行链表中首行的标识
            temp = flag - old_flag +1;                                              //更新行链表中行的标识跨度
            for (int i = 0; i < dtPaste.Count; i++)
            {
                dtPaste[i][1] = Convert.ToInt32(dtPaste[i][1]) + temp;
                if (i == 0)
                {
                    dtPaste[i][2] = father;
                }
                else
                {
                    dtPaste[i][2] = Convert.ToInt32(dtPaste[i][2]) + temp;
                }
            }

        }

        public void CtrlvOtherRow(DataTable dt, int CurRow)
        {
            int old_count = dt.Rows.Count;
            Node curNode = this.c1FlexGrid1.Rows[CurRow].Node;
            int curChildren = curNode.Children;
            int cur_row_parent = Convert.ToInt32(c1FlexGrid1[CurRow, 3]);           //选中行的“父亲”列值
            if (curChildren > 0)
            {
                int NextBrotherIndex = GetNextBrotherNode(curNode);
                curChildren = NextBrotherIndex - curNode.Row.Index - 1;
            }
            updatePasteList(CurRow, curChildren);               //更新剪切行链表中的标识和父亲列

            /***********************更新DataTable*********************************/
            if (old_count > 0)
            {
                for (int row = CurRow + curChildren; row < old_count; row++)
                {
                    dt.Rows[row][colName[1]] = Convert.ToInt32(dt.Rows[row][colName[1]].ToString()) + dtPaste.Count;

                    if (Convert.ToInt32(dt.Rows[row][colName[2]].ToString()) > cur_row_parent)
                    {
                        dt.Rows[row][colName[2]] = Convert.ToInt32(dt.Rows[row][colName[2]].ToString()) + dtPaste.Count;
                    }
                }
                for (int iRow = 0; iRow < dtPaste.Count; iRow++)
                {
                    DataRow dr = dt.NewRow();
                    for (int col = 1; col < colName.Length; col++)
                    {
                        dr[colName[col]] = dtPaste[iRow][colName[col]];
                    }

                    dt.Rows.Add(dr);
                }
            }
            dtPaste.Clear();
            /***********************更新DataTable*********************************/
            
            try
            {
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("增加子项错误！" + ex.Message);
            }
            finally
            {
                dt.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from other_fee where 所属标段='" + bNumber + "'  order by 标识 asc");
                adapter.Fill(dt);
            }
            c1FlexGrid1.Clear();
            if (c1FlexGrid1.Rows.Count > 1)
            {
                c1FlexGrid1.Rows.RemoveRange(1, c1FlexGrid1.Rows.Count - 1);
            }
            SetColName(colName);            //设置其他工程费表的列名称
            InsertNode(dt);          //插入节点列

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                c1FlexGrid1[i + 1, 0] = string.Format("{0}", i + 1);
                for (int col = 0; col < colName.Length; col++)
                {
                    c1FlexGrid1[i + 1, col + 1] = dt.Rows[i].ItemArray[col];
                }

            }
            CalParentSum(CurRow + 1, dt);           //新增，每次粘贴完更新所有父节点金额
            HideDefaultCols();          //隐藏缺省列
            try
            {
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("增加子项错误！" + ex.Message);
            }
        }

        public void CtrlvZjRow(DataTable dt, int CurRow)
        {
            int old_count = dt.Rows.Count;
            Node curNode = this.c1FlexGrid1.Rows[CurRow].Node;
            int curChildren = curNode.Children;
            int cur_row_parent = Convert.ToInt32(c1FlexGrid1[CurRow, 3]);           //选中行的“父亲”列值
            if (curChildren > 0)
            {
                int NextBrotherIndex = GetNextBrotherNode(curNode);
                curChildren = NextBrotherIndex - curNode.Row.Index - 1;
            }
            updatePasteList(CurRow, curChildren);               //更新剪切行链表中的标识和父亲列

            /***********************更新DataTable*********************************/
            if (old_count > 0)
            {
                for (int row = CurRow + curChildren; row < old_count; row++)
                {
                    dt.Rows[row][zjName[1]] = Convert.ToInt32(dt.Rows[row][zjName[1]].ToString()) + dtPaste.Count;

                    if (Convert.ToInt32(dt.Rows[row][zjName[2]].ToString()) > cur_row_parent)
                    {
                        dt.Rows[row][zjName[2]] = Convert.ToInt32(dt.Rows[row][zjName[2]].ToString()) + dtPaste.Count;
                    }
                }
                for (int iRow = 0; iRow < dtPaste.Count; iRow++)
                {
                    DataRow dr = dt.NewRow();
                    for (int col = 1; col < zjName.Length; col++)
                    {
                        dr[zjName[col]] = dtPaste[iRow][zjName[col]];
                    }

                    dt.Rows.Add(dr);
                }
            }
            dtPaste.Clear();
            /***********************更新DataTable*********************************/

            try
            {
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("粘贴行错误！" + ex.Message);
            }
            finally
            {
                dt.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from yqzj_fee where 所属标段='" + bNumber + "'  order by 标识 asc");
                adapter.Fill(dt);
            }
            c1FlexGrid1.Clear();
            if (c1FlexGrid1.Rows.Count > 1)
            {
                c1FlexGrid1.Rows.RemoveRange(1, c1FlexGrid1.Rows.Count - 1);
            }
            SetColName(zjName);            //设置其他工程费表的列名称
            InsertNode(dt);          //插入节点列

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                c1FlexGrid1[i + 1, 0] = string.Format("{0}", i + 1);
                for (int col = 0; col < colName.Length; col++)
                {
                    c1FlexGrid1[i + 1, col + 1] = dt.Rows[i].ItemArray[col];
                }

            }
            CalParentSum(CurRow + 1, dt);           //新增，每次粘贴完更新所有父节点金额
            HideDefaultCols();          //隐藏缺省列
            try
            {
                adapter.Update(dt);
                string UpdateSql = "update other_fee set 金额='" + dt.Rows[0].ItemArray[10] + "' where 所属标段='" + bNumber + "' and 编号='SBYQZJF'";
                SqlHelper.exeNonQuery(UpdateSql);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("粘贴行错误！" + ex.Message);
            }
        }

        public void CtrlvLsRow(DataTable dt, int CurRow)
        {
            int old_count = dt.Rows.Count;
            Node curNode = this.c1FlexGrid1.Rows[CurRow].Node;
            int curChildren = curNode.Children;
            int cur_row_parent = Convert.ToInt32(c1FlexGrid1[CurRow, 3]);           //选中行的“父亲”列值
            if (curChildren > 0)
            {
                int NextBrotherIndex = GetNextBrotherNode(curNode);
                curChildren = NextBrotherIndex - curNode.Row.Index - 1;
            }
            updatePasteList(CurRow, curChildren);               //更新剪切行链表中的标识和父亲列

            /***********************更新DataTable*********************************/
            if (old_count > 0)
            {
                for (int row = CurRow + curChildren; row < old_count; row++)
                {
                    dt.Rows[row][lsName[1]] = Convert.ToInt32(dt.Rows[row][lsName[1]].ToString()) + dtPaste.Count;

                    if (Convert.ToInt32(dt.Rows[row][lsName[2]].ToString()) > cur_row_parent)
                    {
                        dt.Rows[row][lsName[2]] = Convert.ToInt32(dt.Rows[row][lsName[2]].ToString()) + dtPaste.Count;
                    }
                }
                for (int iRow = 0; iRow < dtPaste.Count; iRow++)
                {
                    DataRow dr = dt.NewRow();
                    for (int col = 1; col < lsName.Length; col++)
                    {
                        dr[lsName[col]] = dtPaste[iRow][lsName[col]];
                    }

                    dt.Rows.Add(dr);
                }
            }
            dtPaste.Clear();
            /***********************更新DataTable*********************************/

            try
            {
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("增加子项错误！" + ex.Message);
            }
            finally
            {
                dt.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from linshi_fee where 所属标段='" + bNumber + "'  order by 标识 asc");
                adapter.Fill(dt);
            }
            c1FlexGrid1.Clear();
            if (c1FlexGrid1.Rows.Count > 1)
            {
                c1FlexGrid1.Rows.RemoveRange(1, c1FlexGrid1.Rows.Count - 1);
            }
            SetColName(lsName);            //设置其他工程费表的列名称
            InsertNode(dt);          //插入节点列

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                c1FlexGrid1[i + 1, 0] = string.Format("{0}", i + 1);
                for (int col = 0; col < lsName.Length; col++)
                {
                    c1FlexGrid1[i + 1, col + 1] = dt.Rows[i].ItemArray[col];
                }

            }
            CalParentSum(CurRow + 1, dt);           //新增，每次粘贴完更新所有父节点金额
            HideDefaultCols();          //隐藏缺省列
            try
            {
                adapter.Update(dt);
                string UpdateSql = "update other_fee set 金额='" + dt.Rows[0].ItemArray[8] + "' where 所属标段='" + bNumber + "' and 编号='LSF'";
                SqlHelper.exeNonQuery(UpdateSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("增加子项错误！" + ex.Message);
            }
        }
        public void DeleteRow(DataTable dt, int CurRow)             //删除行
        {
            switch (CurrentFee)
            {
                case CurrentItem.QTGCF:
                    DeleteOtherRow(dt, CurRow);
                    break;
                case CurrentItem.SBYQZJF:
                    DeleteZjRow(dt, CurRow);
                    break;
                case CurrentItem.LSSSF:
                    DeleteLsRow(dt, CurRow);
                    break;
                default:
                    MessageBox.Show("行删除错误！");
                    break;
            }
        }

        public void DeleteOtherRow(DataTable dt, int CurRow)
        {
            int old_count = dt.Rows.Count;
            Node curNode = this.c1FlexGrid1.Rows[CurRow].Node;
            Node parent = curNode.Parent;
            int curChildren = curNode.Children;
            int cur_row_parent = Convert.ToInt32(c1FlexGrid1[CurRow, 3]);           //选中行的“父亲”列值
            if (curChildren > 0)
            {
                int NextBrotherIndex = GetNextBrotherNode(curNode);
                curChildren = NextBrotherIndex - curNode.Row.Index - 1;
            }

            for (int row = CurRow + curChildren ; row < dt.Rows.Count; row++)
            {
                dt.Rows[row][colName[1]] = Convert.ToInt32(dt.Rows[row][colName[1]].ToString()) - (curChildren + 1);
                if (Convert.ToInt32(dt.Rows[row][colName[2]].ToString()) > cur_row_parent)
                {
                    dt.Rows[row][colName[2]] = Convert.ToInt32(dt.Rows[row][colName[2]].ToString()) - (curChildren + 1);
                }
            }
            /*******************************删除c1flexgrid/DataTable行**********************************************/
            for (int removeRow = CurRow; removeRow <= CurRow + curChildren; removeRow++)
            {
                c1FlexGrid1.Rows.Remove(CurRow);
                dt.Rows[removeRow-1].Delete();
            }
            /*******************************删除c1flexgrid/DataTable行**********************************************/
            if (parent.Children == 0)
            {
                parent.Image = imageList1.Images[0];
            }
            try
            {
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除行后更新数据库错误！" + ex.Message);
            }
            finally
            {
                //注释原因：剪切粘贴时由于dt.Clear()导致链表dtPaste同时被清空，原因不明
                /*dt.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from other_fee where 所属标段 ='" + bNumber +"' order by 标识 asc ");
                adapter.Fill(dt);*/
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
            CalParentSum(CurRow-1, dt);           //新增，每次粘贴完更新所有父节点金额
            try
            {
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除行错误！" + ex.Message);
            }
        }

        public void DeleteZjRow(DataTable dt, int CurRow)
        {
            int old_count = dt.Rows.Count;
            Node curNode = this.c1FlexGrid1.Rows[CurRow].Node;
            Node parent = curNode.Parent;
            int curChildren = curNode.Children;
            int cur_row_parent = Convert.ToInt32(c1FlexGrid1[CurRow, 3]);           //选中行的“父亲”列值
            if (curChildren > 0)
            {
                int NextBrotherIndex = GetNextBrotherNode(curNode);
                curChildren = NextBrotherIndex - curNode.Row.Index - 1;
            }

            for (int row = CurRow + curChildren; row < dt.Rows.Count; row++)
            {
                dt.Rows[row][zjName[1]] = Convert.ToInt32(dt.Rows[row][zjName[1]].ToString()) - (curChildren + 1);
                if (Convert.ToInt32(dt.Rows[row][zjName[2]].ToString()) > cur_row_parent)
                {
                    dt.Rows[row][zjName[2]] = Convert.ToInt32(dt.Rows[row][zjName[2]].ToString()) - (curChildren + 1);
                }
            }
            /*******************************删除c1flexgrid/DataTable行**********************************************/
            for (int removeRow = CurRow; removeRow <= CurRow + curChildren; removeRow++)
            {
                c1FlexGrid1.Rows.Remove(CurRow);
                dt.Rows[removeRow - 1].Delete();
            }
            /*******************************删除c1flexgrid/DataTable行**********************************************/
            if (parent.Children == 0)
            {
                parent.Image = imageList1.Images[0];
            }
            try
            {
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除行后更新数据库错误！" + ex.Message);
            }
            finally
            {
                //注释原因：剪切粘贴时由于dt.Clear()导致链表dtPaste同时被清空，原因不明
               /* dt.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from yqzj_fee where 所属标段 ='" + bNumber + "' order by 标识 asc ");
                adapter.Fill(dt);*/
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
            CalParentSum(CurRow-1, dt);           //新增，每次粘贴完更新所有父节点金额
            try
            {
                adapter.Update(dt);
                string UpdateSql = "update other_fee set 金额='" + dt.Rows[0].ItemArray[10] + "' where 所属标段='" + bNumber + "' and 编号='SBYQZJF'";
                SqlHelper.exeNonQuery(UpdateSql);

            }
            catch (Exception ex)
            {
                MessageBox.Show("删除行错误！" + ex.Message);
            }
        }

        public void DeleteLsRow(DataTable dt, int CurRow)
        {
            int old_count = dt.Rows.Count;
            Node curNode = this.c1FlexGrid1.Rows[CurRow].Node;
            Node parent = curNode.Parent;
            int curChildren = curNode.Children;
            int cur_row_parent = Convert.ToInt32(c1FlexGrid1[CurRow, 3]);           //选中行的“父亲”列值
            if (curChildren > 0)
            {
                int NextBrotherIndex = GetNextBrotherNode(curNode);
                curChildren = NextBrotherIndex - curNode.Row.Index - 1;
            }

            for (int row = CurRow + curChildren; row < dt.Rows.Count; row++)
            {
                dt.Rows[row][lsName[1]] = Convert.ToInt32(dt.Rows[row][lsName[1]].ToString()) - (curChildren + 1);
                if (Convert.ToInt32(dt.Rows[row][lsName[2]].ToString()) > cur_row_parent)
                {
                    dt.Rows[row][lsName[2]] = Convert.ToInt32(dt.Rows[row][lsName[2]].ToString()) - (curChildren + 1);
                }
            }
            /*******************************删除c1flexgrid/DataTable行**********************************************/
            for (int removeRow = CurRow; removeRow <= CurRow + curChildren; removeRow++)
            {
                c1FlexGrid1.Rows.Remove(CurRow);
                dt.Rows[removeRow - 1].Delete();
            }
            /*******************************删除c1flexgrid/DataTable行**********************************************/
            if (parent.Children == 0)
            {
                parent.Image = imageList1.Images[0];
            }
            try
            {
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除行后更新数据库错误！" + ex.Message);
            }
            finally
            {
                //注释原因：剪切粘贴时由于dt.Clear()导致链表dtPaste同时被清空，原因不明
                /*dt.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from linshi_fee where 所属标段 ='" + bNumber + "' order by 标识 asc ");
                adapter.Fill(dt);*/
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
            CalParentSum(CurRow-1, dt);           //新增，每次粘贴完更新所有父节点金额
            try
            {
                adapter.Update(dt);
                string UpdateSql = "update other_fee set 金额='" + dt.Rows[0].ItemArray[8] + "' where 所属标段='" + bNumber + "' and 编号='LSF'";
                SqlHelper.exeNonQuery(UpdateSql);

            }
            catch (Exception ex)
            {
                MessageBox.Show("删除行错误！" + ex.Message);
            }
        }

        public void AddSameLevelRow(DataTable dt, int CurRow)               //增加后项
        {
            switch (CurrentFee)
            {
                case CurrentItem.QTGCF:
                    AddSameLevelOther(dt, CurRow);
                    break;
                case CurrentItem.SBYQZJF:
                    AddSameLevelZj(dt, CurRow);
                    break;
                case CurrentItem.LSSSF:
                    AddSameLevelLs(dt, CurRow);
                    break;
                default:
                    MessageBox.Show("增加后项错误！");
                    break;
            }

        }
        public void AddSameLevelOther(DataTable dt, int CurRow)           //增加后项
        {
            DataRow dr = dt.NewRow();
            int old_count = dt.Rows.Count;
            Node curNode = this.c1FlexGrid1.Rows[CurRow].Node;
            int curChildren = curNode.Children;
            int cur_row_parent = Convert.ToInt32(c1FlexGrid1[CurRow, 3]);           //选中行的“父亲”列值
            if (curChildren > 0)
            {
                int NextBrotherIndex = GetNextBrotherNode(curNode);
                curChildren = NextBrotherIndex - curNode.Row.Index - 1;
            }

            this.c1FlexGrid1.Rows.InsertNode(CurRow + curChildren + 1, curNode.Level);

            this.c1FlexGrid1.Rows[CurRow + curChildren + 1].Node.Image = imageList1.Images[0];

            /***********************更新DataTable*********************************/
            if (old_count > 0)
            {
                dr[colName[1]] = CurRow + curChildren;
                dr[colName[2]] = Convert.ToInt32(dt.Rows[CurRow - 1].ItemArray[2].ToString());
                dr[colName[5]] = "元";
                dr[colName[7]] = Convert.ToDouble("100").ToString("F2");
                dr[colName[10]] = bNumber;
                for (int row = CurRow + curChildren; row < old_count; row++)
                {
                    dt.Rows[row][colName[1]] = Convert.ToInt32(dt.Rows[row][colName[1]].ToString()) + 1;

                    if (Convert.ToInt32(dt.Rows[row][colName[2]].ToString()) > cur_row_parent)
                    {
                        dt.Rows[row][colName[2]] = Convert.ToInt32(dt.Rows[row][colName[2]].ToString()) + 1;
                    }
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
                MessageBox.Show("增加子项错误！" + ex.Message);
            }
            finally
            {
                dt.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from other_fee where 所属标段='"+bNumber+"' order by 序号 asc");
                adapter.Fill(dt);
            }

            /***********************更新c1FlexGrid*********************************/
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                c1FlexGrid1[i + 1, 0] = string.Format("{0}", i + 1);        //更新flexgrid第0列序号列
            }
            // this.c1FlexGrid1[CurRow + curChildren + 1, 1] = Convert.ToInt32(dr[colName[0]]);
            // this.c1FlexGrid1[CurRow + curChildren + 1, 2] = Convert.ToInt32(dr[colName[1]]);
            // this.c1FlexGrid1[CurRow + curChildren + 1, 3] = Convert.ToInt32(dr[colName[2]]);
            this.c1FlexGrid1[CurRow + curChildren + 1, 1] = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0]);
            this.c1FlexGrid1[CurRow + curChildren + 1, 2] = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][1]);
            this.c1FlexGrid1[CurRow + curChildren + 1, 3] = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][2]);
            this.c1FlexGrid1[CurRow + curChildren + 1, 6] = "元";
            this.c1FlexGrid1[CurRow + curChildren + 1, 8] = Convert.ToDouble("100").ToString("F2");
            this.c1FlexGrid1[CurRow + curChildren + 1, 11] = bNumber;
            for (int row = CurRow + curChildren + 2; row <= dt.Rows.Count; row++)
            {
                c1FlexGrid1[row, 2] = Convert.ToInt32(c1FlexGrid1[row, 2]) + 1;

                if (Convert.ToInt32(c1FlexGrid1[row, 3]) > cur_row_parent)
                {
                    c1FlexGrid1[row, 3] = Convert.ToInt32(c1FlexGrid1[row, 3]) + 1;
                }
            }
            /***********************更新c1FlexGrid*********************************/
            dt.Clear();
            adapter = SqlHelper.getSqlAdapter("select * from other_fee where 所属标段='" + bNumber + "'  order by 标识 asc");
            adapter.Fill(dt);
        }

        public void AddSameLevelZj(DataTable dt, int CurRow)
        {
            DataRow dr = dt.NewRow();
            int old_count = dt.Rows.Count;
            Node curNode = this.c1FlexGrid1.Rows[CurRow].Node;
            int curChildren = curNode.Children;
            int cur_row_parent = Convert.ToInt32(c1FlexGrid1[CurRow, 3]);           //选中行的“父亲”列值
            if (curChildren > 0)
            {
                int NextBrotherIndex = GetNextBrotherNode(curNode);
                curChildren = NextBrotherIndex - curNode.Row.Index - 1;
            }

            this.c1FlexGrid1.Rows.InsertNode(CurRow + curChildren + 1, curNode.Level);

            this.c1FlexGrid1.Rows[CurRow + curChildren + 1].Node.Image = imageList1.Images[0];


            /***********************更新DataTable*********************************/
            if (old_count > 0)
            {
                dr[zjName[1]] = CurRow + curChildren;
                dr[zjName[2]] = Convert.ToInt32(dt.Rows[CurRow - 1].ItemArray[2].ToString()); ;
                dr[zjName[5]] = "0";
                dr[zjName[9]] = Convert.ToDouble("0.00").ToString("F2");
                dr[zjName[13]] = bNumber;

                for (int row = CurRow + curChildren; row < old_count; row++)
                {
                    dt.Rows[row][zjName[1]] = Convert.ToInt32(dt.Rows[row][zjName[1]].ToString()) + 1;

                    if (Convert.ToInt32(dt.Rows[row][zjName[2]].ToString()) > cur_row_parent)
                    {
                        dt.Rows[row][zjName[2]] = Convert.ToInt32(dt.Rows[row][zjName[2]].ToString()) + 1;
                    }
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
                MessageBox.Show("增加子项错误！" + ex.Message);
            }
            finally
            {
                dt.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from yqzj_fee where 所属标段='" + bNumber + "'  order by 序号 asc");
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
            this.c1FlexGrid1[CurRow + curChildren + 1, 6] = "0";
            this.c1FlexGrid1[CurRow + curChildren + 1, 10] = Convert.ToDouble("0.00").ToString("F2");
            this.c1FlexGrid1[CurRow + curChildren + 1, 14] = bNumber;
            for (int row = CurRow + curChildren + 2; row <= dt.Rows.Count; row++)
            {
                c1FlexGrid1[row, 2] = Convert.ToInt32(c1FlexGrid1[row, 2]) + 1;

                if (Convert.ToInt32(c1FlexGrid1[row, 3]) > cur_row_parent)
                {
                    c1FlexGrid1[row, 3] = Convert.ToInt32(c1FlexGrid1[row, 3]) + 1;
                }
            }
            /***********************更新c1FlexGrid*********************************/
            dt.Clear();
            adapter = SqlHelper.getSqlAdapter("select * from yqzj_fee where 所属标段='" + bNumber + "'  order by 标识 asc");
            adapter.Fill(dt);
        }

        public void AddSameLevelLs(DataTable dt, int CurRow)
        {
            DataRow dr = dt.NewRow();
            int old_count = dt.Rows.Count;
            Node curNode = this.c1FlexGrid1.Rows[CurRow].Node;
            int curChildren = curNode.Children;
            int cur_row_parent = Convert.ToInt32(c1FlexGrid1[CurRow, 3]);           //选中行的“父亲”列值
            if (curChildren > 0)
            {
                int NextBrotherIndex = GetNextBrotherNode(curNode);
                curChildren = NextBrotherIndex - curNode.Row.Index - 1;
            }

            this.c1FlexGrid1.Rows.InsertNode(CurRow + curChildren + 1, curNode.Level);

            this.c1FlexGrid1.Rows[CurRow + curChildren + 1].Node.Image = imageList1.Images[0];

            /***********************更新DataTable*********************************/
            if (old_count > 0)
            {
                dr[lsName[1]] = CurRow + curChildren;
                dr[lsName[2]] = Convert.ToInt32(dt.Rows[CurRow - 1].ItemArray[2].ToString()); ;
                dr[lsName[11]] = bNumber;

                for (int row = CurRow + curChildren; row < old_count; row++)
                {
                    dt.Rows[row][lsName[1]] = Convert.ToInt32(dt.Rows[row][lsName[1]].ToString()) + 1;

                    if (Convert.ToInt32(dt.Rows[row][lsName[2]].ToString()) > cur_row_parent)
                    {
                        dt.Rows[row][lsName[2]] = Convert.ToInt32(dt.Rows[row][lsName[2]].ToString()) + 1;
                    }
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
                MessageBox.Show("增加子项错误！" + ex.Message);
            }
            finally
            {
                dt.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from linshi_fee where 所属标段='" + bNumber + "'  order by 序号 asc");
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
            this.c1FlexGrid1[CurRow + curChildren + 1, 12] = bNumber;
            for (int row = CurRow + curChildren + 2; row <= dt.Rows.Count; row++)
            {
                c1FlexGrid1[row, 2] = Convert.ToInt32(c1FlexGrid1[row, 2]) + 1;

                if (Convert.ToInt32(c1FlexGrid1[row, 3]) > cur_row_parent)
                {
                    c1FlexGrid1[row, 3] = Convert.ToInt32(c1FlexGrid1[row, 3]) + 1;
                }
            }
            /***********************更新c1FlexGrid*********************************/
            dt.Clear();
            adapter = SqlHelper.getSqlAdapter("select * from linshi_fee where 所属标段='" + bNumber + "'  order by 标识 asc");
            adapter.Fill(dt);
        }

        /***************************计算所有非叶子节点的金额总数****************************************/
        public void CalParentSum(int row, DataTable dt)
        {
            double  sum = 0.00;
            Node father = c1FlexGrid1.Rows[row].Node.Parent;
            if (CurrentFee == CurrentItem.QTGCF || CurrentFee == CurrentItem.LSSSF)
            {
                if (father == null)
                {
                    c1FlexGrid1[1, 9] = Convert.ToDouble(sum.ToString()).ToString("F2");
                    dt.Rows[0][8] = (string)c1FlexGrid1[1, 9];
                    return;
                }
                if (father.Level == 0)
                {
                    foreach (Node child in father.Nodes)
                    {
                        if (child.Row[9] != null && child.Row[9].ToString().Length > 0)
                        {
                            sum += Convert.ToDouble(child.Row[9].ToString());
                        }
                    }
                    c1FlexGrid1[1, 9] = Convert.ToDouble(sum.ToString()).ToString("F2");
                    dt.Rows[0][8] = (string)c1FlexGrid1[1, 9];
                }
                else
                {
                    foreach (Node child in father.Nodes)
                    {
                        if (child.Row[9] != null && child.Row[9].ToString().Length > 0)
                        {
                            sum += Convert.ToDouble(child.Row[9].ToString());
                        }
                    }
                    c1FlexGrid1[father.Row.Index, 9] = Convert.ToDouble(sum.ToString()).ToString("F2");
                    dt.Rows[father.Row.Index - 1][8] = (string)c1FlexGrid1[father.Row.Index, 9];
                    CalParentSum(father.Row.Index, dt);
                }
            }
            else if (CurrentFee == CurrentItem.SBYQZJF)
            {
                if (father == null)
                {
                    c1FlexGrid1[1, 11] = Convert.ToDouble(sum.ToString()).ToString("F2");
                    dt.Rows[0][10] = (string)c1FlexGrid1[1, 11];
                    return;
                }
                if (father.Level == 0)
                {
                    foreach (Node child in father.Nodes)
                    {
                        if (child.Row[11] != null && child.Row[11].ToString().Length > 0)
                        {
                            sum += Convert.ToDouble(child.Row[11].ToString());
                        }
                    }
                    c1FlexGrid1[1, 11] = Convert.ToDouble(sum.ToString()).ToString("F2");
                    dt.Rows[0][10] = (string)c1FlexGrid1[1, 11];
                }
                else
                {
                    foreach (Node child in father.Nodes)
                    {
                        if (child.Row[11] != null && child.Row[11].ToString().Length > 0)
                        {
                            sum += Convert.ToDouble(child.Row[11].ToString());
                        }
                    }
                    c1FlexGrid1[father.Row.Index, 11] = Convert.ToDouble(sum.ToString()).ToString("F2");
                    dt.Rows[father.Row.Index - 1][10] = (string)c1FlexGrid1[father.Row.Index, 11];
                    CalParentSum(father.Row.Index, dt);
                }
            }
            else
            {
                MessageBox.Show("当前费用不存在，无法计算非叶子节点金额汇总！");
            }
        }
        /***************************计算所有非叶子节点的金额总数****************************************/

        public void Otherfee_AfterEdit(int row)
        {
            double sumA, sumB;
            bool CountIsEmpty = true;
            dataTable.Rows[row - 1][colName[0]] = (int)c1FlexGrid1[row, 1];
            dataTable.Rows[row - 1][colName[1]] = (int)c1FlexGrid1[row, 2];
            dataTable.Rows[row - 1][colName[2]] = (int)c1FlexGrid1[row, 3];

            if (c1FlexGrid1[row, 4] != null && c1FlexGrid1[row, 4].ToString().Length > 0)
            {
                dataTable.Rows[row - 1][colName[3]] = (string)c1FlexGrid1[row, 4];
            }

            if (c1FlexGrid1[row, 5] != null && c1FlexGrid1[row, 5].ToString().Length > 0)
            {
                dataTable.Rows[row - 1][colName[4]] = (string)c1FlexGrid1[row, 5];
            }

            if (c1FlexGrid1[row, 6] != null && c1FlexGrid1[row, 6].ToString().Length > 0)
            {
                dataTable.Rows[row - 1][colName[5]] = (string)c1FlexGrid1[row, 6];
            }

            if (c1FlexGrid1[row, 7] != null && c1FlexGrid1[row, 7].ToString().Length > 0)
            {
                CountIsEmpty = false;
                c1FlexGrid1[row, 7] = Convert.ToDouble(c1FlexGrid1[row, 7]).ToString();
                dataTable.Rows[row - 1][colName[6]] = c1FlexGrid1[row, 7];
            }

            if (c1FlexGrid1[row, 8] != null && c1FlexGrid1[row, 8].ToString().Length > 0)
            {
                if (!CountIsEmpty)
                {
                    sumA = Convert.ToDouble((string)c1FlexGrid1[row, 7]);
                    sumB = Convert.ToDouble((string)c1FlexGrid1[row, 8]);
                    c1FlexGrid1[row, 9] = Convert.ToDouble(sumA * sumB / 100).ToString("F2");
                }
                c1FlexGrid1[row, 8] = Convert.ToDouble(c1FlexGrid1[row, 8]).ToString("F2");
                dataTable.Rows[row - 1][colName[7]] = c1FlexGrid1[row, 8];
            }

            if (c1FlexGrid1[row, 9] != null && c1FlexGrid1[row, 9].ToString().Length > 0)
            {
                c1FlexGrid1[row, 9] = Convert.ToDouble(c1FlexGrid1[row, 9]).ToString("F2");
                dataTable.Rows[row - 1][colName[8]] = c1FlexGrid1[row, 9];
            }

            if (c1FlexGrid1[row, 10] != null && c1FlexGrid1[row, 10].ToString().Length > 0)
            {
                dataTable.Rows[row - 1][colName[9]] = (string)c1FlexGrid1[row, 10];
            }

            if (c1FlexGrid1[row, 11] != null && c1FlexGrid1[row, 11].ToString().Length > 0)
            {
                dataTable.Rows[row - 1][colName[10]] = (string)c1FlexGrid1[row, 11];
            }   

            CalParentSum(row, dataTable);

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
                adapter = SqlHelper.getSqlAdapter("select * from other_fee where 所属标段='" + bNumber + "'  order by 标识 asc");
                adapter.Fill(dataTable);
            }
        }

        public void Zjfee_AfterEdit(int row)
        {
            double sumA, sumB,sumC;
            bool CountIsEmpty = true;
            bool TotalPrice = true;
            bool ZjPercent = true;
            dataTable.Rows[row - 1][zjName[0]] = (int)c1FlexGrid1[row, 1];
            dataTable.Rows[row - 1][zjName[1]] = (int)c1FlexGrid1[row, 2];
            dataTable.Rows[row - 1][zjName[2]] = (int)c1FlexGrid1[row, 3];

            if (c1FlexGrid1[row, 4] != null && c1FlexGrid1[row, 4].ToString().Length > 0)
            {
                dataTable.Rows[row - 1][zjName[3]] = (string)c1FlexGrid1[row, 4];
            }


            if (c1FlexGrid1[row, 5] != null && c1FlexGrid1[row, 5].ToString().Length > 0)
            {
                dataTable.Rows[row - 1][zjName[4]] = (string)c1FlexGrid1[row, 5];
            }


            if (c1FlexGrid1[row, 6] != null && c1FlexGrid1[row, 6].ToString().Length > 0)
            {
                CountIsEmpty = false;
                c1FlexGrid1[row, 6] = Convert.ToDouble(c1FlexGrid1[row, 6]).ToString("F2");
                dataTable.Rows[row - 1][zjName[5]] = (string)c1FlexGrid1[row, 6];
            }


            if (c1FlexGrid1[row, 7] != null && c1FlexGrid1[row, 7].ToString().Length > 0)
            {
                if (!CountIsEmpty)
                {
                    sumA = Convert.ToDouble((string)c1FlexGrid1[row, 6]);
                    sumB = Convert.ToDouble((string)c1FlexGrid1[row, 7]);
                    c1FlexGrid1[row, 8] = Convert.ToDouble(sumA * sumB ).ToString("F2");
                }
                c1FlexGrid1[row, 7] = Convert.ToDouble(c1FlexGrid1[row, 7]).ToString("F2");
                dataTable.Rows[row - 1][zjName[6]] = c1FlexGrid1[row, 7];
            }


            if (c1FlexGrid1[row, 8] != null && c1FlexGrid1[row, 8].ToString().Length > 0)
            {
                TotalPrice = false;
                c1FlexGrid1[row, 8] = Convert.ToDouble(c1FlexGrid1[row, 8]).ToString("F2");
                dataTable.Rows[row - 1][zjName[7]] = (string)c1FlexGrid1[row, 8];
            }


            if (c1FlexGrid1[row, 9] != null && c1FlexGrid1[row, 9].ToString().Length > 0)
            {
                ZjPercent = false;
                c1FlexGrid1[row, 9] = Convert.ToDouble(c1FlexGrid1[row, 9]).ToString("F2");
                dataTable.Rows[row - 1][zjName[8]] = c1FlexGrid1[row, 9];
            }


            if (c1FlexGrid1[row, 10] != null && c1FlexGrid1[row, 10].ToString().Length > 0)
            {
                if(!TotalPrice && !ZjPercent)
                {
                    sumA = Convert.ToDouble((string)c1FlexGrid1[row, 8]);
                    sumB = Convert.ToDouble((string)c1FlexGrid1[row, 9]);
                    sumC = Convert.ToDouble((string)c1FlexGrid1[row, 10]);
                    c1FlexGrid1[row, 11] = Convert.ToDouble((sumA * sumB * sumC)/100).ToString("F2");
                }
                c1FlexGrid1[row, 10] = Convert.ToDouble(c1FlexGrid1[row, 10]).ToString("F2");
                dataTable.Rows[row - 1][zjName[9]] = (string)c1FlexGrid1[row, 10];
            }

            if (c1FlexGrid1[row, 11] != null && c1FlexGrid1[row, 11].ToString().Length > 0)
            {
                c1FlexGrid1[row, 11] = Convert.ToDouble(c1FlexGrid1[row, 11]).ToString("F2");
                dataTable.Rows[row - 1][zjName[10]] = (string)c1FlexGrid1[row, 11];
            }

            if (c1FlexGrid1[row, 12] != null && c1FlexGrid1[row, 12].ToString().Length > 0)
            {
                dataTable.Rows[row - 1][zjName[11]] = (string)c1FlexGrid1[row, 12];
            }

            if (c1FlexGrid1[row, 13] != null && c1FlexGrid1[row, 13].ToString().Length > 0)
            {
                dataTable.Rows[row - 1][zjName[12]] = (string)c1FlexGrid1[row, 13];
            }

            if (c1FlexGrid1[row, 14] != null && c1FlexGrid1[row, 14].ToString().Length > 0)
            {
                dataTable.Rows[row - 1][zjName[13]] = (string)c1FlexGrid1[row, 14];
            }
     
            CalParentSum(row, dataTable);

            try
            {
                adapter.Update(dataTable);
                string UpdateSql = "update other_fee set 金额='"+ dataTable.Rows[0].ItemArray[10]+"' where 所属标段='"+bNumber+"' and 编号='SBYQZJF'";
                SqlHelper.exeNonQuery(UpdateSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("编辑异常！" + ex.Message);
            }
            finally
            {
                dataTable.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from yqzj_fee where 所属标段='" + bNumber + "'  order by 标识 asc");
                adapter.Fill(dataTable);
            }
        }

        public void Lsfee_AfterEdit(int row)
        {
            double sumA, sumB;
            bool CountIsEmpty = true;
            dataTable.Rows[row - 1][lsName[0]] = (int)c1FlexGrid1[row, 1];
            dataTable.Rows[row - 1][lsName[1]] = (int)c1FlexGrid1[row, 2];
            dataTable.Rows[row - 1][lsName[2]] = (int)c1FlexGrid1[row, 3];

            if (c1FlexGrid1[row, 4] != null && c1FlexGrid1[row, 4].ToString().Length > 0)
            {
                dataTable.Rows[row - 1][lsName[3]] = (string)c1FlexGrid1[row, 4];
            }

            if (c1FlexGrid1[row, 5] != null && c1FlexGrid1[row, 5].ToString().Length > 0)
            {
                dataTable.Rows[row - 1][lsName[4]] = (string)c1FlexGrid1[row, 5];
            }

            if (c1FlexGrid1[row, 6] != null && c1FlexGrid1[row, 6].ToString().Length > 0)
            {
                dataTable.Rows[row - 1][lsName[5]] = (string)c1FlexGrid1[row, 6];
            }

            if (c1FlexGrid1[row, 7] != null && c1FlexGrid1[row, 7].ToString().Length > 0)
            {
                CountIsEmpty = false;
                c1FlexGrid1[row, 7] = Convert.ToDouble(c1FlexGrid1[row, 7]).ToString();
                dataTable.Rows[row - 1][lsName[6]] = c1FlexGrid1[row, 7];
            }

            if (c1FlexGrid1[row, 8] != null && c1FlexGrid1[row, 8].ToString().Length > 0)
            {
                if (!CountIsEmpty)
                {
                    sumA = Convert.ToDouble((string)c1FlexGrid1[row, 7]);
                    sumB = Convert.ToDouble((string)c1FlexGrid1[row, 8]);
                    c1FlexGrid1[row, 9] = Convert.ToDouble(sumA * sumB ).ToString("F2");
                }
                c1FlexGrid1[row, 8] = Convert.ToDouble(c1FlexGrid1[row, 8]).ToString("F2");
                dataTable.Rows[row - 1][lsName[7]] = c1FlexGrid1[row, 8];
            }

            if (c1FlexGrid1[row, 9] != null && c1FlexGrid1[row, 9].ToString().Length > 0)
            {
                c1FlexGrid1[row, 9] = Convert.ToDouble(c1FlexGrid1[row, 9]).ToString("F2");
                dataTable.Rows[row - 1][lsName[8]] = c1FlexGrid1[row, 9];
            }

            if (c1FlexGrid1[row, 10] != null && c1FlexGrid1[row, 10].ToString().Length > 0)
            {
                dataTable.Rows[row - 1][lsName[9]] = (string)c1FlexGrid1[row, 10];
            }

            if (c1FlexGrid1[row, 11] != null && c1FlexGrid1[row, 11].ToString().Length > 0)
            {
                dataTable.Rows[row - 1][lsName[10]] = (string)c1FlexGrid1[row, 11];
            }

            if (c1FlexGrid1[row, 12] != null && c1FlexGrid1[row, 12].ToString().Length > 0)
            {
                dataTable.Rows[row - 1][lsName[11]] = (string)c1FlexGrid1[row, 12];
            }

            CalParentSum(row, dataTable);

            try
            {
                adapter.Update(dataTable);
                string UpdateSql = "update other_fee set 金额='" + dataTable.Rows[0].ItemArray[8] + "' where 所属标段='" + bNumber + "' and 编号='LSF'";
                SqlHelper.exeNonQuery(UpdateSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("编辑异常！" + ex.Message);
            }
            finally
            {
                dataTable.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from linshi_fee where 所属标段='" + bNumber + "'  order by 标识 asc");
                adapter.Fill(dataTable);
            }
        }

        private void c1FlexGrid1_AfterEdit(object sender, RowColEventArgs e)
        {
            switch (CurrentFee)
            {
                case CurrentItem.QTGCF:
                    Otherfee_AfterEdit(e.Row);
                    break;
                case CurrentItem.SBYQZJF:
                    Zjfee_AfterEdit(e.Row);
                    break;
                case CurrentItem.LSSSF:
                    Lsfee_AfterEdit(e.Row);
                    break;
                default:
                    MessageBox.Show("增加后项错误！");
                    break;
            }
        }

        private void c1FlexGrid1_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            if (CurrentFee == CurrentItem.QTGCF || CurrentFee == CurrentItem.LSSSF)
            {
                if (e.Col == 7 || e.Col == 8 || e.Col == 9)
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
            else if (CurrentFee == CurrentItem.SBYQZJF)
            {
                if (e.Col == 6 || e.Col == 7 || e.Col == 8 || e.Col == 9 || e.Col == 10 || e.Col == 11)
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
            else
            {

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
            if(mousedir == MouseDirection.LEFT)
            {
                switch (e.ClickedItem.Text)
                {
                    case "增加子项":
                        AddChildRow(dataTable, c1FlexGrid1.RowSel);
                        break;
                    case "增加后项":
                        AddSameLevelRow(dataTable,c1FlexGrid1.RowSel);
                        break;
                    case "复制":
                        IsCopy = true;
                        IsPaste = false;
                        CopyRowTemp(dataTable, c1FlexGrid1.RowSel);
                        break;
                    case "剪切":
                        IsPaste = true;
                        IsCopy = false;
                        CopyRowTemp(dataTable, c1FlexGrid1.RowSel);
                        DeleteRow(dataTable, c1FlexGrid1.RowSel);
                        break;
                    case "粘贴":
                        if (IsCopy)
                        {
                            CtrlvRow(dataTable, c1FlexGrid1.RowSel);
                            IsCopy = false;
                        }
                        if (IsPaste)
                        {
                            CtrlvRow(dataTable, c1FlexGrid1.RowSel);
                            IsPaste = false;
                        }
                        break;
                    case "删除":
                        DeleteRow(dataTable, c1FlexGrid1.RowSel);
                        break;
                    case "保存模板":
                        ExportToExcel(dataTable);
                        break;
                    default:
                        break;
                }
            }
        }

        private void c1FlexGrid1_CancelAddRow(object sender, RowColEventArgs e)
        {
            MessageBox.Show("删除行！");
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Node.Text)
            {
                case "其他工程费":
                    if (CurrentFee != CurrentItem.QTGCF)
                    {
                        CurrentFee = CurrentItem.QTGCF;
                        SetOtherFeeDefault(bNumber);
                    }
                    break;
                case "试验、测量设备仪器折旧费":
                    CurrentFee = CurrentItem.SBYQZJF;
                    SetZjFeeDefault(bNumber);
                    break;
                case "临时设施费":
                    CurrentFee = CurrentItem.LSSSF;
                    SetSsFeeDefault(bNumber);
                    break;
                default:
                    break;
            }
        }

        public void ExportToExcel(DataTable dt)
        {
            string docName = "";
            string targetName = "";
            switch (CurrentFee)
            {
                case CurrentItem.QTGCF:
                    docName = @"其他工程费模板.xlsx";
                    targetName = @"其他工程费.xlsx";
                    break;
                case CurrentItem.SBYQZJF:
                    docName = @"设备仪器折旧费模板.xlsx";
                    targetName = @"设备仪器折旧费.xlsx";
                    break;
                case CurrentItem.LSSSF:
                    docName = @"临时设施费模板.xlsx";
                    targetName = @"临时设施费.xlsx";
                    break;
                default:
                    break;
            }
            if (File.Exists(Directory.GetCurrentDirectory() + @"\模板\"+docName))
            {

                if (!System.IO.Directory.Exists(Directory.GetCurrentDirectory() + @"\导出数据"))
                {
                    System.IO.Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\导出数据");

                }
                string sourcePath = Directory.GetCurrentDirectory() + @"\模板\"+ docName;
                string targetPath = Directory.GetCurrentDirectory() + @"\导出数据\" + bNumber + targetName;
                File.Copy(sourcePath, targetPath, true);
                SqlHelper.TableToExcel(dt, targetPath);
                MessageBox.Show(bNumber + targetName+ "导出成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            else
            {
                MessageBox.Show(docName + "不存在！", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SetZjFeeDefault(string bidNumber)
        {
            string sql = "select * from yqzj_fee where 所属标段='" + bidNumber + "'  order by 标识 asc";                       //正式确认时sql语句需添加where+bid_name分支以确定对应标段
            dataTable = new DataTable();
            adapter = SqlHelper.getSqlAdapter(sql);
            adapter.Fill(dataTable);
            c1FlexGrid1.Clear();
            if (c1FlexGrid1.Rows.Count > 1)
            {
                c1FlexGrid1.Rows.RemoveRange(1, c1FlexGrid1.Rows.Count - 1);
            }
            c1FlexGrid1.AutoClipboard = true;
            c1FlexGrid1.Tree.Column = 4;
            c1FlexGrid1.Cols.Count = 15;
            c1FlexGrid1.Styles.Normal.WordWrap = true;
            this.c1FlexGrid1.AutoSizeRows();
            c1FlexGrid1.Rows[0].AllowEditing = false;
            c1FlexGrid1.Cols[0].AllowEditing = false;
            c1FlexGrid1.Cols[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlexGrid1.Rows[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlexGrid1.Cols[0].Width = 35;
            c1FlexGrid1.Cols[4].Width = 120;
            c1FlexGrid1.Cols[5].Width = 200;
            c1FlexGrid1.Cols[6].Width = 100;
            c1FlexGrid1.Cols[7].Width = 120;
            c1FlexGrid1.Cols[8].Width = 120;
            c1FlexGrid1.Cols[9].Width = 120;
            c1FlexGrid1.Cols[10].Width = 150;
            c1FlexGrid1.Cols[11].Width = 100;
            c1FlexGrid1.Cols[12].Width = 100;
            c1FlexGrid1.Cols[13].Width = 100;

            SetZjColName(zjName);

            if (dataTable.Rows.Count == 0)
            {
                //c1FlexGrid1.Rows.Count = 1;
                c1FlexGrid1.Rows.InsertNode(1, 0);
                c1FlexGrid1.Rows[1].Node.Image = imageList1.Images[0];
                c1FlexGrid1[1, 0] = "1";
                c1FlexGrid1[1, 2] = 0;
                c1FlexGrid1[1, 3] = -1;
                c1FlexGrid1[1, 5] = "试验、测量设备仪器折旧费";
                c1FlexGrid1[1, 12] = "SBYQZJF";
                c1FlexGrid1[1, 14] = bidNumber;

                dr = dataTable.NewRow();
                dr[zjName[1]] = (int)c1FlexGrid1[1, 2];
                dr[zjName[2]] = (int)c1FlexGrid1[1, 3];
                dr[zjName[3]] = (string)(c1FlexGrid1[1, 4]);
                dr[zjName[4]] = (string)c1FlexGrid1[1, 5];
                dr[zjName[5]] = (string)c1FlexGrid1[1, 6];
                dr[zjName[6]] = (string)c1FlexGrid1[1, 7];
                dr[zjName[7]] = (string)c1FlexGrid1[1, 8];
                dr[zjName[8]] = (string)c1FlexGrid1[1, 9];
                dr[zjName[9]] = (string)c1FlexGrid1[1, 10];
                dr[zjName[10]] = (string)c1FlexGrid1[1, 11];
                dr[zjName[11]] = (string)c1FlexGrid1[1, 12];
                dr[zjName[12]] = (string)c1FlexGrid1[1, 13];
                dr[zjName[13]] = (string)c1FlexGrid1[1, 14];
                dataTable.Rows.Add(dr);

                HideDefaultCols();          //隐藏缺省列
                c1FlexGrid1.Rows[1].AllowEditing = false;
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
                    adapter = SqlHelper.getSqlAdapter("select * from yqzj_fee order by 标识 asc");
                    adapter.Fill(dataTable);
                }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    c1FlexGrid1[i + 1, 1] = dataTable.Rows[i].ItemArray[0];
                }

                dataTable.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from yqzj_fee where 所属标段='" + bidNumber + "'  order by 标识 asc");
                adapter.Fill(dataTable);
            }
            else
            {
               // c1FlexGrid1.Rows.Count = dataTable.Rows.Count;
                HideDefaultCols();          //隐藏缺省列
                InsertNode(dataTable);          //插入节点列

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    c1FlexGrid1[i + 1, 0] = string.Format("{0}", i + 1);
                    for (int col = 0; col < zjName.Length; col++)
                    {
                        c1FlexGrid1[i + 1, col + 1] = dataTable.Rows[i].ItemArray[col];
                    }

                }
                c1FlexGrid1.Rows[1].AllowEditing = false;
            }
        }

        public void SetSsFeeDefault(string bidNumber)
        {
            string sql = "select * from linshi_fee where 所属标段='" + bidNumber + "'  order by 标识 asc";                       //正式确认时sql语句需添加where+bid_name分支以确定对应标段
            dataTable = new DataTable();
            adapter = SqlHelper.getSqlAdapter(sql);
            adapter.Fill(dataTable);
            c1FlexGrid1.Clear();
            if (c1FlexGrid1.Rows.Count > 1)
            {
                c1FlexGrid1.Rows.RemoveRange(1, c1FlexGrid1.Rows.Count - 1);
            }
            c1FlexGrid1.AutoClipboard = true;
            c1FlexGrid1.Tree.Column = 4;
            c1FlexGrid1.Cols.Count = 13;
            c1FlexGrid1.Styles.Normal.WordWrap = true;
            this.c1FlexGrid1.AutoSizeRows();
            c1FlexGrid1.Rows[0].AllowEditing = false;
            c1FlexGrid1.Cols[0].AllowEditing = false;
            c1FlexGrid1.Cols[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlexGrid1.Rows[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlexGrid1.Cols[0].Width = 35;
            c1FlexGrid1.Cols[4].Width = 120;
            c1FlexGrid1.Cols[5].Width = 140;
            c1FlexGrid1.Cols[6].Width = 60;
            c1FlexGrid1.Cols[7].Width = 160;
            c1FlexGrid1.Cols[8].Width = 140;
            c1FlexGrid1.Cols[9].Width = 140;
            c1FlexGrid1.Cols[10].Width = 80;
            c1FlexGrid1.Cols[11].Width = 150;

            SetLsColName(lsName);

            if (dataTable.Rows.Count == 0)
            {
                //c1FlexGrid1.Rows.Count = 1;
                c1FlexGrid1.Rows.InsertNode(1, 0);
                c1FlexGrid1.Rows[1].Node.Image = imageList1.Images[0];
                c1FlexGrid1[1, 0] = "1";
                c1FlexGrid1[1, 2] = 0;
                c1FlexGrid1[1, 3] = -1;
                c1FlexGrid1[1, 5] = "临时设施费";
                c1FlexGrid1[1, 10] = "LSF";
                c1FlexGrid1[1, 12] = bidNumber;

                dr = dataTable.NewRow();
                dr[lsName[1]] = (int)c1FlexGrid1[1, 2];
                dr[lsName[2]] = (int)c1FlexGrid1[1, 3];
                dr[lsName[3]] = (string)(c1FlexGrid1[1, 4]);
                dr[lsName[4]] = (string)c1FlexGrid1[1, 5];
                dr[lsName[5]] = (string)c1FlexGrid1[1, 6];
                dr[lsName[6]] = (string)c1FlexGrid1[1, 7];
                dr[lsName[7]] = (string)c1FlexGrid1[1, 8];
                dr[lsName[8]] = (string)c1FlexGrid1[1, 9];
                dr[lsName[9]] = (string)c1FlexGrid1[1, 10];
                dr[lsName[10]] = (string)c1FlexGrid1[1, 11];
                dr[lsName[11]] = (string)c1FlexGrid1[1, 12];
                dataTable.Rows.Add(dr);

                HideDefaultCols();          //隐藏缺省列
                c1FlexGrid1.Rows[1].AllowEditing = false;
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
                    adapter = SqlHelper.getSqlAdapter("select * from linshi_fee where 所属标段='" + bidNumber + "'  order by 标识 asc");
                    adapter.Fill(dataTable);
                }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    c1FlexGrid1[i + 1, 1] = dataTable.Rows[i].ItemArray[0];
                }

                dataTable.Clear();
                adapter = SqlHelper.getSqlAdapter("select * from linshi_fee where 所属标段='" + bidNumber + "'  order by 标识 asc");
                adapter.Fill(dataTable);
            }
            else
            {
               // c1FlexGrid1.Rows.Count = dataTable.Rows.Count;
                HideDefaultCols();          //隐藏缺省列
                InsertNode(dataTable);          //插入节点列

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    c1FlexGrid1[i + 1, 0] = string.Format("{0}", i + 1);
                    for (int col = 0; col < lsName.Length; col++)
                    {
                        c1FlexGrid1[i + 1, col + 1] = dataTable.Rows[i].ItemArray[col];
                    }

                }
                c1FlexGrid1.Rows[1].AllowEditing = false;
            }
        }

        private void c1FlexGrid1_MouseClick(object sender, MouseEventArgs e)
        {
            if (c1FlexGrid1.MouseRow != -1)
            {
                if (c1FlexGrid1.Rows[c1FlexGrid1.MouseRow].Node.Level == 0)
                {
                    contextMenuStrip1.Items[0].Enabled = true;
                    contextMenuStrip1.Items[1].Enabled = false;
                    contextMenuStrip1.Items[2].Enabled = false;
                    contextMenuStrip1.Items[3].Enabled = false;
                    if (IsCopy || IsPaste)
                    {
                        contextMenuStrip1.Items[4].Enabled = true ;
                    }
                    else
                    {
                        contextMenuStrip1.Items[4].Enabled = false;
                    }
                    contextMenuStrip1.Items[5].Enabled = false;
                    contextMenuStrip1.Items[6].Enabled = true;
                }
                else
                {
                    contextMenuStrip1.Items[0].Enabled = true;
                    contextMenuStrip1.Items[1].Enabled = true;
                    contextMenuStrip1.Items[2].Enabled = true;
                    contextMenuStrip1.Items[3].Enabled = true;
                    if (IsCopy || IsPaste)
                    {
                        contextMenuStrip1.Items[4].Enabled = true ;
                    }
                    else
                    {
                        contextMenuStrip1.Items[4].Enabled = false;
                    }
                    contextMenuStrip1.Items[5].Enabled = true;
                    contextMenuStrip1.Items[6].Enabled = true;
                }
            }
        }


    }
}
