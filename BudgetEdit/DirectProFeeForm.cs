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

namespace BudgetEdit
{
    public partial class DirectProFeeForm : Form
    {
        private ListBox listBox = new ListBox();
        private bool[] setting = { true, true, false, false, false, false, false, false, false };
        private DataTable indexLibarayDataTable;
        private C1.Win.C1FlexGrid.Row newRow;
        //private string sectionNumber;//记录所属章节
        public DirectProFeeForm()
        {
            InitializeComponent();
            directProFeeC1FlexGrid.Controls.Add(listBox);
            listBox.Visible = false;
            listBox.MouseDoubleClick += new MouseEventHandler(listBoxMouseEvent);
            listBox.KeyDown += new KeyEventHandler(listBoxKeyDown);

        }
        private void setDriectProFeeTitle()
        {
            string[] title = { "", "", "", "", "", "编号", "标识", "名称", "单位", "租赁", "混合", "工程量1", "工程量2", "清单", "清单", "费用明细", "费用明细", "费用明细", "费用明细", "费用明细", "预算单价", "预算合价" };
            string[] title1 = { "", " ", "", "", "", "编号", "标识", "名称", "单位", "租赁", "混合", "工程量1", "工程量2", "单价", "合价", "劳务费", "材料费", "机械费", "摊销费", "其他费", "预算单价", "预算合价" };
            directProFeeC1FlexGrid.AllowMerging = AllowMergingEnum.FixedOnly;
            directProFeeC1FlexGrid.Rows[0].AllowMerging = true;
            directProFeeC1FlexGrid.Cols[0].Width = 3 * directProFeeC1FlexGrid.Rows[0].HeightDisplay;
            directProFeeC1FlexGrid.Cols[5].Width = 6 * directProFeeC1FlexGrid.Rows[0].HeightDisplay;
            //directProFeeC1FlexGrid.Rows[1].AllowMerging = true;
            for (int i = 0; i < directProFeeC1FlexGrid.Cols.Count; i++)
            {
                directProFeeC1FlexGrid.Cols[i].AllowMerging = true;
                directProFeeC1FlexGrid[0, i] = title[i];
                directProFeeC1FlexGrid[1, i] = title1[i];
            }
            directProFeeC1FlexGrid.Cols["工程量1"].DataType = typeof(double);
            directProFeeC1FlexGrid.Cols["预算单价"].AllowEditing = false;
            directProFeeC1FlexGrid.Cols["预算合价"].AllowEditing = false;

            directProFeeC1FlexGrid.Cols["编号"].ComboList = "|...";
            directProFeeC1FlexGrid.Cols["租赁"].Visible = setting[0];
            directProFeeC1FlexGrid.Cols["混合"].Visible = setting[1];
            directProFeeC1FlexGrid.Cols["工程量2"].Visible = setting[2];
            directProFeeC1FlexGrid.Cols["劳务费"].Visible = setting[3];
            directProFeeC1FlexGrid.Cols["材料费"].Visible = setting[4];
            directProFeeC1FlexGrid.Cols["机械费"].Visible = setting[5];
            directProFeeC1FlexGrid.Cols["摊销费"].Visible = setting[6];
            directProFeeC1FlexGrid.Cols["其他费"].Visible = setting[7];
            directProFeeC1FlexGrid.Styles.Fixed.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
        }

        private void DirectProFeeForm_Shown(object sender, EventArgs e)
        {
            fillDataTable();
            ((MainForm)this.TopLevelControl).showButton();
        }
        private void fillDataTable()
        {

            string sql = "select * from index_library";
            MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
            indexLibarayDataTable = new DataTable();
            adapter.Fill(indexLibarayDataTable);
        }
        private void directProFeeC1FlexGrid_MouseClick(object sender, MouseEventArgs e)
        {
            C1FlexGrid c1Grid = (C1FlexGrid)sender;
            if (e.Button == System.Windows.Forms.MouseButtons.Right && directProFeeC1FlexGrid.Rows[c1Grid.RowSel].IsNode && directProFeeC1FlexGrid.Rows[c1Grid.RowSel].Node.Children == 0)
            {
                SettingContextMenuStrip.Show(directProFeeC1FlexGrid, e.Location);
            }
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmSetting = new SettingForm(setting);
            ////frm.TopLevel = false;
            DialogResult result = frmSetting.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                directProFeeC1FlexGrid.Cols["工程量2"].Visible = setting[0];
                directProFeeC1FlexGrid.Cols["劳务费"].Visible = setting[1];
                directProFeeC1FlexGrid.Cols["材料费"].Visible = setting[2];
                directProFeeC1FlexGrid.Cols["机械费"].Visible = setting[3];
                directProFeeC1FlexGrid.Cols["摊销费"].Visible = setting[4];
                directProFeeC1FlexGrid.Cols["其他费"].Visible = setting[5];
            }
        }

        public void displayDirectFee(string bidNumber)//显示业主清单
        {
            directProFeeC1FlexGrid.Clear(C1.Win.C1FlexGrid.ClearFlags.Content);
            directProFeeC1FlexGrid.Rows.RemoveRange(2, directProFeeC1FlexGrid.Rows.Count - 2);
            //setDriectProFeeTitle();

            string bidSql = "SELECT * FROM bid_index_inventory";
            MySqlDataAdapter bidIndexInventorydapter = SqlHelper.getSqlAdapter(bidSql);
            DataTable bidIndexInventory = new DataTable();
            bidIndexInventorydapter.Fill(bidIndexInventory);

            bidSql = "SELECT * FROM bid_material_inventory";
            MySqlDataAdapter bidMaterialInventorydapter = SqlHelper.getSqlAdapter(bidSql);
            DataTable bidMaterialInventory = new DataTable();
            bidMaterialInventorydapter.Fill(bidMaterialInventory);

            string sql = string.Format("SELECT * FROM project_inventory where 所属标段 ='{0}'", bidNumber);
            MySqlDataAdapter inventoryAdapter = SqlHelper.getSqlAdapter(sql);
            DataTable dtInventoryDatabase = new DataTable();
            inventoryAdapter.Fill(dtInventoryDatabase);
            if (dtInventoryDatabase.Rows.Count > 0)
            {
                setDriectProFeeTitle();
                foreach (DataRow drInventory in dtInventoryDatabase.Rows)
                {
                    string number = (string)drInventory["编号"];
                    int count = 0;
                    for (int i = 0; i < number.Length; i++)
                    {
                        if (number[i] == '-')
                            count++;
                    }
                    C1.Win.C1FlexGrid.Node newNode = directProFeeC1FlexGrid.Rows.AddNode(++count);//InsertNode(directProFeeC1FlexGrid.Rows.Count, ++count);
                    newNode.Row.Height = 35;
                    newNode.Row.AllowEditing = false;
                    newNode.Row["编号"] = drInventory["编号"];
                    newNode.Row["标识"] = drInventory["标识"];
                    newNode.Row["名称"] = drInventory["名称"];
                    newNode.Row["单位"] = drInventory["单位"];
                    newNode.Row["工程量1"] = drInventory["工程量"];
                    //newNode.Row["租赁"] = inventoryReader["租赁"]!=DBNull.Value ? :false;
                    //newNode.Row["混合"] = inventoryReader["混合"]!=DBNull.Value ? :false;
                    newNode.Row["劳务费"] = drInventory["劳务费"];
                    newNode.Row["材料费"] = drInventory["材料费"];
                    newNode.Row["机械费"] = drInventory["机械费"];
                    newNode.Row["摊销费"] = drInventory["摊销费"];
                    newNode.Row["其他费"] = drInventory["其他费"];
                    newNode.Row["单价"] = drInventory["单价"];
                    newNode.Row["合价"] = drInventory["合价"];
                    if ((bool)drInventory["修改标识"])
                    {
                        C1.Win.C1FlexGrid.CellStyle rs = directProFeeC1FlexGrid.Styles.Add("RowColor");
                        rs.ForeColor = Color.Red;
                        newNode.Row.Style = directProFeeC1FlexGrid.Styles["RowColor"];
                    }

                    DataRow[] drs = bidIndexInventory.Select(string.Format("所属章节='{0}'", number));
                    if (drs.Length > 0)
                    {
                        foreach (DataRow dr in drs)
                        {
                            C1.Win.C1FlexGrid.Row newRow = directProFeeC1FlexGrid.Rows.Insert(directProFeeC1FlexGrid.Rows.Count);
                            newRow["编号"] = dr["细目号"];
                            newRow["名称"] = dr["细目名称"];
                            newRow["单位"] = dr["单位"];
                            newRow["工程量1"] = dr["数量"];
                            newRow["预算单价"] = dr["预算单价"];
                            newRow["预算合价"] = dr["预算合价"];
                            newRow["标识"] = dr["工作内容"];
                            newRow["序号"] = dr["序号"];
                            newRow["清单类型"] = "I";
                        }
                    }


                    drs = bidMaterialInventory.Select(string.Format("所属章节='{0}'", number));
                    if (drs.Length > 0)
                    {
                        foreach (DataRow dr in drs)
                        {
                            C1.Win.C1FlexGrid.Row newRow = directProFeeC1FlexGrid.Rows.Insert(directProFeeC1FlexGrid.Rows.Count);
                            if (((string)dr["编号"]).Trim().Length == 6 || (((string)dr["编号"]).Trim().Length == 13 && (string)dr["材料类型"] == "周转材料"))
                            {
                                directProFeeC1FlexGrid.SetCellCheck(newRow.Index, 9, (bool)dr["租赁"] ? CheckEnum.Checked : CheckEnum.Unchecked);
                            }
                            else if (((string)dr["编号"]).Trim().Length != 1)
                            {
                                directProFeeC1FlexGrid.SetCellCheck(newRow.Index, 10, (bool)dr["混合"] ? CheckEnum.Checked : CheckEnum.Unchecked);
                            }
                            newRow["编号"] = dr["编号"];
                            newRow["名称"] = dr["名称"];
                            newRow["单位"] = dr["单位"];
                            newRow["工程量1"] = dr["数量"];
                            newRow["预算单价"] = dr["预算单价"];
                            newRow["预算合价"] = dr["预算合价"];
                            newRow["标识"] = dr["规格"];
                            newRow["序号"] = dr["序号"];
                            newRow["清单类型"] = "M";
                            newRow["材料类型"] = dr["材料类型"];
                        }
                    }
                }
            }

        }

        private void lwfbToolStripMenuItem_Click(object sender, EventArgs e)//选择劳务分包指标
        {
            ((MainForm)this.TopLevelControl).selectIndex();
        }


        public void addIndex(object obj)
        {
            if (directProFeeC1FlexGrid.Rows[directProFeeC1FlexGrid.RowSel].IsNode && directProFeeC1FlexGrid.Rows[directProFeeC1FlexGrid.RowSel].Node.Nodes.Length <= 0)
            {
                C1.Win.C1FlexGrid.Row row = (C1.Win.C1FlexGrid.Row)obj;
                string sql;
                int count = getCountnventory((string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"]);
                C1.Win.C1FlexGrid.Row newRow = directProFeeC1FlexGrid.Rows.Insert(directProFeeC1FlexGrid.RowSel + count + 1);
                newRow["编号"] = row["细目号"];
                newRow["名称"] = row["细目名称"];
                newRow["单位"] = row["单位"];
                newRow["标识"] = row["工作内容"];
                newRow["工程量1"] = row["数量"];

                sql = string.Format("SELECT * FROM bid_index_inventory");
                MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DataRow tempRow = dt.NewRow();
                tempRow["细目号"] = row["细目号"];
                tempRow["细目名称"] = row["细目名称"];
                tempRow["单位"] = row["单位"];
                tempRow["数量"] = row["数量"];
                tempRow["所属章节"] = directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"];
                tempRow["工作内容"] = row["工作内容"];
                dt.Rows.Add(tempRow);
                adapter.Update(dt);
                sql = string.Format("SELECT max(序号) as 序号 FROM bid_index_inventory where 所属章节='{0}'", directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"]);
                MySqlDataReader sdr = SqlHelper.getSqlReader(sql);
                sdr.Read();
                directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel + count + 1, "序号"] = sdr["序号"];
                directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel + count + 1, "清单类型"] = "I";
                sdr.Close();
            }


        }

        private void directProFeeC1FlexGrid_AfterEdit(object sender, RowColEventArgs e)
        {
            if (((string)directProFeeC1FlexGrid[e.Row, "清单类型"]) == "I")
            {

                if (directProFeeC1FlexGrid[e.Row, "工程量1"] != DBNull.Value
                    &&!string.IsNullOrEmpty(Convert.ToString(directProFeeC1FlexGrid[e.Row, "工程量1"]))
                    && directProFeeC1FlexGrid[e.Row, "编号"] != DBNull.Value
                    &&!string.IsNullOrEmpty(Convert.ToString(directProFeeC1FlexGrid[e.Row, "编号"])))
                {

                    string sql = string.Format("SELECT * FROM bid_index_inventory");
                    MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    DataRow[] drs = dt.Select(string.Format("序号={0}", directProFeeC1FlexGrid[e.Row, "序号"]));
                        DataRow tempRow = drs[0];
                        tempRow["数量"] = directProFeeC1FlexGrid[e.Row, "工程量1"];
                      
                    adapter.Update(dt);
                }
            }
            else
            {
                string sql = string.Format("SELECT * FROM bid_material_inventory");
                MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DataRow[] drs = dt.Select(string.Format("序号={0}", directProFeeC1FlexGrid[e.Row, "序号"]));
                DataRow tempRow = drs[0];
                if (directProFeeC1FlexGrid[e.Row, "工程量1"] != DBNull.Value
                    && !string.IsNullOrEmpty(Convert.ToString(directProFeeC1FlexGrid[e.Row, "工程量1"])))
                {
                    tempRow["数量"] = directProFeeC1FlexGrid[e.Row, "工程量1"];
                    //if (tempRow["预算单价"] != DBNull.Value && tempRow["预算合价"] != DBNull.Value && tempRow["数量"] != DBNull.Value)
                    //{
                    //    tempRow["预算合价"] = Convert.ToDouble(tempRow["预算单价"]) * Convert.ToDouble(tempRow["数量"]);
                    //}
                    
                }
                tempRow["租赁"] = directProFeeC1FlexGrid.GetCellCheck(e.Row, 9) == CheckEnum.Checked ? true : false;
                tempRow["混合"] = directProFeeC1FlexGrid.GetCellCheck(e.Row, 10) == CheckEnum.Checked ? true : false;
                adapter.Update(dt);
               
            }

        }

        private void lwfbAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = getCountnventory((string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"]);
            newRow = directProFeeC1FlexGrid.Rows.Insert(directProFeeC1FlexGrid.RowSel + count + 1);
            //sectionNumber = (string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"];
            newRow["所属章节"] = (string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"];
            newRow["清单类型"] = "I";
        }

        private void directProFeeC1FlexGrid_ChangeEdit(object sender, EventArgs e)
        {
            //Rectangle rt = directProFeeC1FlexGrid.GetCellRect(directProFeeC1FlexGrid.RowSel, directProFeeC1FlexGrid.ColSel);
            //listBox.Location = new Point(rt.Left, rt.Bottom);
            //DataRow[] drs = indexLibarayDataTable.Select(string.Format(@"细目号 like '{0}%'", directProFeeC1FlexGrid.Editor.Text));
            //listBox.Items.Clear();
            //if (drs.Length > 0)
            //{
            //    foreach (DataRow dr in drs)
            //    {
            //        listBox.Items.Add((string)dr["细目号"]);
            //    }
            //    listBox.Visible = true;
            //    listBox.SetSelected(0, true);
            //}
            //else
            //{
            //    listBox.Visible = false;
            //}
        }
        private void listBoxMouseEvent(object sender, MouseEventArgs e)
        {
            ////MessageBox.Show(listBox.SelectedItem.ToString());
            //DataRow[] drs = indexLibarayDataTable.Select(string.Format("细目号 = '{0}'", listBox.SelectedItem.ToString()));
            //newRow["编号"] = drs[0]["细目号"];
            //newRow["名称"] = drs[0]["细目名称"] != DBNull.Value ? drs[0]["细目名称"] : "";
            //newRow["单位"] = drs[0]["单位"]!=DBNull.Value ? drs[0]["单位"]: "";
            //newRow["预算单价"] = drs[0]["单价"]!=DBNull.Value ? drs[0]["单价"]: 0;
            //newRow["标识"] = drs[0]["工作内容"]!=DBNull.Value ? drs[0]["工作内容"]: "";
            //newRow["工程量"] = 0;
            //newRow["预算合价"] = 0;
            //listBox.Visible = false;
            //string sql = string.Format("SELECT * FROM bid_index_inventory");
            //MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            //DataRow tempRow = dt.NewRow();
            //tempRow["细目号"] = drs[0]["细目号"];
            //tempRow["细目名称"] = drs[0]["细目名称"]!=DBNull.Value ?drs[0]["细目名称"] : "";
            //tempRow["单位"] = drs[0]["单位"]!=DBNull.Value ? drs[0]["单位"]: "";
            //tempRow["预算单价"] = drs[0]["单价"]!=DBNull.Value ? drs[0]["单价"]: 0;
            //tempRow["所属章节"] = sectionNumber;
            //tempRow["工作内容"] = drs[0]["工作内容"]!=DBNull.Value ?drs[0]["工作内容"] : "";
            //dt.Rows.Add(tempRow);
            //adapter.Update(dt);


            //sql = string.Format("SELECT max(序号) as 序号 FROM bid_index_inventory where 所属章节='{0}'", sectionNumber);
            //MySqlDataReader sdr = SqlHelper.getSqlReader(sql);
            //sdr.Read();
            //newRow["序号"] = sdr["序号"];
            //sdr.Close();
        }

        private void listBoxKeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    DataRow[] drs = indexLibarayDataTable.Select(string.Format("细目号 = '{0}'", listBox.SelectedItem.ToString()));
            //    newRow["编号"] = drs[0]["细目号"];
            //    newRow["名称"] = drs[0]["细目名称"] != DBNull.Value ? drs[0]["细目名称"] : "";
            //    newRow["单位"] = drs[0]["单位"] != DBNull.Value ? drs[0]["单位"] : "";
            //    newRow["单价"] = drs[0]["单价"] != DBNull.Value ? drs[0]["单价"] : 0;
            //    newRow["标识"] = drs[0]["工作内容"] != DBNull.Value ? drs[0]["工作内容"] : "";
            //    newRow["工程量"] = 0;
            //    newRow["预算合价"] = 0;
            //    listBox.Visible = false;
            //    string sql = string.Format("SELECT * FROM bid_index_inventory");
            //    MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
            //    DataTable dt = new DataTable();
            //    adapter.Fill(dt);
            //    DataRow tempRow = dt.NewRow();
            //    tempRow["细目号"] = drs[0]["细目号"];
            //    tempRow["细目名称"] = drs[0]["细目名称"] != DBNull.Value ? drs[0]["细目名称"] : "";
            //    tempRow["单位"] = drs[0]["单位"] != DBNull.Value ? drs[0]["单位"] : "";
            //    tempRow["预算单价"] = drs[0]["单价"] != DBNull.Value ? drs[0]["单价"] : 0;
            //    tempRow["所属章节"] = sectionNumber;
            //    tempRow["工作内容"] = drs[0]["工作内容"] != DBNull.Value ? drs[0]["工作内容"] : "";
            //    dt.Rows.Add(tempRow);
            //    adapter.Update(dt);

            //    sql = string.Format("SELECT max(序号) as 序号 FROM bid_index_inventory where 所属章节='{0}'", sectionNumber);
            //    MySqlDataReader sdr = SqlHelper.getSqlReader(sql);
            //    sdr.Read();
            //    newRow["序号"] = sdr["序号"];
            //    sdr.Close();
            //}
        }


        public void addMaterial(object obj)
        {
            if (directProFeeC1FlexGrid.Rows[directProFeeC1FlexGrid.RowSel].IsNode && directProFeeC1FlexGrid.Rows[directProFeeC1FlexGrid.RowSel].Node.Nodes.Length <= 0)
            {
                C1.Win.C1FlexGrid.Row row = (C1.Win.C1FlexGrid.Row)obj;
                string sql;
                int count = getCountnventory((string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"]);
                C1.Win.C1FlexGrid.Row newRow = directProFeeC1FlexGrid.Rows.Insert(directProFeeC1FlexGrid.RowSel + count + 1);
                if (((string)row["编号"]).Trim().Length == 6 || (((string)row["编号"]).Trim().Length == 13 && (string)row["材料类型"] == "周转材料"))
                {
                    directProFeeC1FlexGrid.SetCellCheck(newRow.Index, 9, CheckEnum.Unchecked);
                }
                else if (((string)row["编号"]).Trim().Length != 1)
                {
                    directProFeeC1FlexGrid.SetCellCheck(newRow.Index, 10, CheckEnum.Unchecked);
                }
                newRow["材料类型"] = row["材料类型"];
                newRow["编号"] = row["编号"];
                newRow["名称"] = row["名称"];
                newRow["单位"] = row["单位"];
                newRow["单价"] = row["单价"];
                newRow["标识"] = row["规格"];
                //newRow["工程量1"] = row["数量"];
                sql = string.Format("SELECT * FROM bid_material_inventory");
                MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DataRow tempRow = dt.NewRow();
                tempRow["编号"] = row["编号"];
                tempRow["名称"] = row["名称"];
                tempRow["单位"] = row["单位"];
                //tempRow["单价"] = row["单价"] != DBNull.Value ? row["单价"] : 0;
                tempRow["所属章节"] = directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"];
                tempRow["规格"] = row["规格"];
                tempRow["租赁"] = false;
                tempRow["混合"] = false;
                //tempRow["数量"] = row["数量"];
                tempRow["材料类型"] = row["材料类型"];
                dt.Rows.Add(tempRow);
                adapter.Update(dt);
                sql = string.Format("SELECT max(序号) as 序号 FROM bid_material_inventory where 所属章节='{0}'", directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"]);
                MySqlDataReader sdr = SqlHelper.getSqlReader(sql);
                sdr.Read();
                directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel + count + 1, "序号"] = sdr["序号"];
                directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel + count + 1, "清单类型"] = "M";
                sdr.Close();
            }
        }
        private int getCountnventory(string sectionNumber)//取当前章节所有清单数
        {
            string sql = string.Format("SELECT count(序号) as 清单个数 FROM bid_material_inventory where 所属章节='{0}'", sectionNumber);
            MySqlDataReader sdr = SqlHelper.getSqlReader(sql);
            sdr.Read();
            int indexCount = Convert.ToInt16(sdr["清单个数"]);
            sdr.Close();
            sql = string.Format("SELECT count(序号) as 清单个数 FROM bid_index_inventory where 所属章节='{0}'", sectionNumber);
            sdr = SqlHelper.getSqlReader(sql);
            sdr.Read();
            int materialCount = Convert.ToInt16(sdr["清单个数"]);
            sdr.Close();
            return indexCount + materialCount;
        }

        private void directProFeeC1FlexGrid_CellButtonClick(object sender, RowColEventArgs e)
        {
            switch ((string)directProFeeC1FlexGrid[e.Row, "材料类型"])
            {
                case "人工": ((MainForm)this.TopLevelControl).selectMaterial("人才机", false);
                    break;
                case "其他": ((MainForm)this.TopLevelControl).selectMaterial("材料", false);
                    break;
                case "机械": ((MainForm)this.TopLevelControl).selectMaterial("机械", false);
                    break;
                case "周转材料": ((MainForm)this.TopLevelControl).selectMaterial("周转材料", false);
                    break;
            }
        }

        public void addAddMaterial(object obj)
        {
            if (directProFeeC1FlexGrid.Rows[directProFeeC1FlexGrid.RowSel].IsNode && directProFeeC1FlexGrid.Rows[directProFeeC1FlexGrid.RowSel].Node.Nodes.Length <= 0)
            {
                C1.Win.C1FlexGrid.Row row = (C1.Win.C1FlexGrid.Row)obj;
                string sql;
                //int count = getCountnventory((string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"]);
                C1.Win.C1FlexGrid.Row newRow = directProFeeC1FlexGrid.Rows[directProFeeC1FlexGrid.RowSel];
                if (((string)row["编号"]).Trim().Length == 6 || (((string)row["编号"]).Trim().Length == 13 && (string)row["材料类型"] == "周转材料"))
                {
                    directProFeeC1FlexGrid.SetCellCheck(newRow.Index, 9, CheckEnum.Unchecked);
                }
                else if (((string)row["编号"]).Trim().Length != 1)
                {
                    directProFeeC1FlexGrid.SetCellCheck(newRow.Index, 10, CheckEnum.Unchecked);
                }
                newRow["材料类型"] = row["材料类型"];
                newRow["编号"] = row["编号"];
                newRow["名称"] = row["名称"];
                newRow["单位"] = row["单位"];
                newRow["单价"] = row["单价"];
                newRow["标识"] = row["规格"];
                newRow["工程量1"] = row["数量"];
                sql = string.Format("SELECT * FROM bid_material_inventory");
                MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DataRow tempRow = dt.NewRow();
                tempRow["编号"] = row["编号"];
                tempRow["名称"] = row["名称"];
                tempRow["单位"] = row["单位"] ;
                tempRow["所属章节"] = directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "所属章节"];
                tempRow["规格"] = row["规格"];
                tempRow["租赁"] = false;
                tempRow["混合"] = false;
                tempRow["数量"] = row["数量"];
                tempRow["材料类型"] = row["材料类型"];
                dt.Rows.Add(tempRow);
                adapter.Update(dt);
                sql = string.Format("SELECT max(序号) as 序号 FROM bid_material_inventory where 所属章节='{0}'", directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "所属章节"]);
                MySqlDataReader sdr = SqlHelper.getSqlReader(sql);
                sdr.Read();
                directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "序号"] = sdr["序号"];
                directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "清单类型"] = "M";
                sdr.Close();
            }
        }

        public void updateDirectFee(int index,double price,double sum)//工料机汇总完成后，更新直接工程费中相应条目的预算单价和预算合价
        {
            for (int i = 1; i < directProFeeC1FlexGrid.Rows.Count; i++)
            {
                if ((string)directProFeeC1FlexGrid[i, "清单类型"] == "M" && Convert.ToInt16(directProFeeC1FlexGrid[i, "序号"]) ==  index)
                {
                    directProFeeC1FlexGrid[i, "预算单价"] = price;
                    directProFeeC1FlexGrid[i, "预算合价"] = sum;
                }
            }
        }

        private void rgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((MainForm)this.TopLevelControl).selectMaterial("人才机", true);
        }

        private void clToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((MainForm)this.TopLevelControl).selectMaterial("材料", true);
        }

        private void jxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((MainForm)this.TopLevelControl).selectMaterial("机械", true);
        }

        private void zzclToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((MainForm)this.TopLevelControl).selectMaterial("周转材料", true);
        }

        private void rgAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = getCountnventory((string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"]);
            newRow = directProFeeC1FlexGrid.Rows.Insert(directProFeeC1FlexGrid.RowSel + count + 1);
            //sectionNumber = (string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"];
            newRow["所属章节"] = (string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"];
            newRow["清单类型"] = "M";
            newRow["材料类型"] = "人工";
        }

        private void clAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = getCountnventory((string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"]);
            newRow = directProFeeC1FlexGrid.Rows.Insert(directProFeeC1FlexGrid.RowSel + count + 1);
            //sectionNumber = (string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"];
            newRow["所属章节"] = (string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"];
            newRow["清单类型"] = "M";
            newRow["材料类型"] = "其他";
        }

        private void jxAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = getCountnventory((string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"]);
            newRow = directProFeeC1FlexGrid.Rows.Insert(directProFeeC1FlexGrid.RowSel + count + 1);
            //sectionNumber = (string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"];
            newRow["所属章节"] = (string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"];
            newRow["清单类型"] = "M";
            newRow["材料类型"] = "机械";
        }

        private void zzclAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = getCountnventory((string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"]);
            newRow = directProFeeC1FlexGrid.Rows.Insert(directProFeeC1FlexGrid.RowSel + count + 1);
            //sectionNumber = (string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"];
            newRow["所属章节"] = (string)directProFeeC1FlexGrid[directProFeeC1FlexGrid.RowSel, "编号"];
            newRow["清单类型"] = "M";
            newRow["材料类型"] = "人工";
        }

        private void directProFeeC1FlexGrid_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            if (directProFeeC1FlexGrid.Cols[e.Col].DataType == typeof(double))
            {
                try
                {
                    Convert.ToDouble(directProFeeC1FlexGrid.Editor.Text);
                }
                catch
                {
                    e.Cancel = true;
                }

            }
        }
    }
}
