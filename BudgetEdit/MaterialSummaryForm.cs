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
using C1.Win.C1FlexGrid;

namespace BudgetEdit
{
    public partial class MaterialSummaryForm : Form
    {
        private string[] mixMaterialTitle = { "混合材料单价计算", "材料序号", "序号", "编号", "材料名称", "规格型号", "材料单位", "理论数量", "损耗数量", "实际数量", "材料单价", "材料费" };
        private string[] otherMaterialTitle = { "其他材料单价计算", "材料序号", "序号", "编号", "材料名称", "规格型号", "材料单位", "理论数量", "损耗数量", "实际数量", "材料单价", "材料费", "综合单价" };
        private string[] ownDeviceTitle = { "自有机械单价计算", "材料序号", "序号", "设备名称", "规格型号", "细目名称", "单位", "数量", "单价", "原值", "折旧年限", "月折旧率", "使用时间", "折旧费", "维修费", "安装及辅助费", "不变费小计", "燃油料费", "养路费", "驾驶人员工资", "可变费小计", "合计" };
        private string[] rentDeviceTitle = { "租赁机械单价计算", "材料序号", "序号", "租赁设备名称", "规格型号", "细目名称", "单位", "数量", "租赁时间", "租赁单价", "租金", "燃油", "安装拆卸设施费", "其他", "合计", "备注" };
        private string[] ownMaterialTitle = { "自有周转材料单价计算", "材料序号", "序号", "周转材料名称", "单位", "数量", "材料单价", "原值", "摊销年限", "月摊销率", "使用时间", "使用部位", "圬工量", "摊销额", "备注" };
        private string[] rentMaterialTitle = { "租赁周转材料单价计算", "材料序号", "序号", "周转材料名称", "单位", "数量", "租赁单价", "租赁时间", "租金合计", "使用部位", "圬工量", "摊销额", "备注" };
        MySqlDataAdapter bidMaterialInventoryAdapter;
        DataTable bidMaterialInventory;
        DataTable mixMaterialRule;
        MySqlDataAdapter mixMaterialAdapter;
        DataTable otherMaterialRule;
        MySqlDataAdapter otherMaterialAdapter;
        DataTable ownMaterialRule;
        MySqlDataAdapter ownMaterialAdapter;
        DataTable rentMaterialRule;
        MySqlDataAdapter rentMaterialAdapter;
        DataTable ownDeviceRule;
        MySqlDataAdapter ownDeviceAdapter;
        DataTable rentDeviceRule;
        MySqlDataAdapter rentDeviceAdapter;
        public MaterialSummaryForm()
        {
            InitializeComponent();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            computeGridSelect(e.Node.Name);
            computeC1SplitterPanel.Hide();
        }
        private void computeGridSelect(string materialType)
        {
            switch (materialType)
            {
                case "其他材料":
                    displayBidInventory("1");
                    break;
                case "混合材料":
                    displayBidInventory("2");
                    break;
                case "自有周转材料":
                    displayBidInventory("3");
                    break;
                case "租赁周转材料":
                    displayBidInventory("4");
                    break;
                case "自有机械":
                    displayBidInventory("5");
                    break;
                case "租赁机械":
                    displayBidInventory("6");
                    break;
                case "人工":
                    displayBidInventory("7");
                    break;
                case "所有工料机":
                    displayBidInventory("all");
                    break;
            }
        }
        private void displayComputeGridTitle(string[] title)
        {
            computC1FlexGrid.Clear(ClearFlags.All);
            computeC1SplitterPanel.Text = title[0];
            computC1FlexGrid.Cols.Count = title.Length;
            computC1FlexGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            computC1FlexGrid.Styles.Fixed.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            if (title[0] == "混合材料单价计算")
            {
                //computC1FlexGrid.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                computC1FlexGrid.Rows.RemoveRange(1, computC1FlexGrid.Rows.Count - 1);
                for (int i = 1; i < title.Length; i++)
                {
                    computC1FlexGrid[0, i] = title[i];
                    computC1FlexGrid.Cols[i].Name = title[i];
                }
                computC1FlexGrid.Cols["序号"].Width = 0;
                computC1FlexGrid.Cols["材料序号"].Width = 0;
                computC1FlexGrid.Cols["理论数量"].DataType = typeof(double);
                computC1FlexGrid.Cols["损耗数量"].DataType = typeof(double);
                computC1FlexGrid.Cols["单价"].DataType = typeof(double);
                computC1FlexGrid.Cols["实际数量"].AllowEditing = false;
                computC1FlexGrid.Cols["材料费"].AllowEditing = false;

                displayMixMaterial();
            }
            else if (title[0] == "其他材料单价计算")
            {
                computC1FlexGrid.Rows.RemoveRange(1, computC1FlexGrid.Rows.Count - 1);
                for (int i = 1; i < title.Length; i++)
                {
                    computC1FlexGrid[0, i] = title[i];
                    computC1FlexGrid.Cols[i].Name = title[i];
                }
                computC1FlexGrid.Cols["序号"].Width = 0;
                computC1FlexGrid.Cols["材料序号"].Width = 0;
                computC1FlexGrid.Cols["理论数量"].DataType = typeof(double);
                computC1FlexGrid.Cols["损耗数量"].DataType = typeof(double);
                computC1FlexGrid.Cols["材料单价"].DataType = typeof(double);
                computC1FlexGrid.Cols["实际数量"].AllowEditing = false;
                computC1FlexGrid.Cols["材料费"].AllowEditing = false;
                computC1FlexGrid.Cols["综合单价"].AllowEditing = false;
                displayOtherMaterial();
            }
            else if (title[0] == "自有机械单价计算")
            {
                computC1FlexGrid.Rows.Count = 2;
                computC1FlexGrid.Rows.RemoveRange(2, computC1FlexGrid.Rows.Count - 2);
                string[] title2 = { "自有机械单价计算", "材料序号", "序号", "设备名称", "规格型号", "细目名称", "单位", "数量", "单价", "原值", "折旧年限", "月折旧率", "使用时间", "不变费用", "不变费用", "不变费用", "不变费用", "可变费用", "可变费用", "可变费用", "可变费用", "合计" };
                computC1FlexGrid.Rows.Fixed = 2;
                computC1FlexGrid.AllowMerging = AllowMergingEnum.FixedOnly;
                computC1FlexGrid.Rows[0].AllowMerging = true;
                computC1FlexGrid.Rows[1].AllowMerging = true;
                computC1FlexGrid.Cols[0].AllowMerging = true;

                for (int i = 1; i < title2.Length; i++)
                {
                    computC1FlexGrid.Cols[i].AllowMerging = true;
                    computC1FlexGrid[0, i] = title2[i];
                    computC1FlexGrid[1, i] = title[i];
                    computC1FlexGrid.Cols[i].Name = title[i];

                }

                computC1FlexGrid.Cols["序号"].Width = 0;
                computC1FlexGrid.Cols["材料序号"].Width = 0;

                computC1FlexGrid.Cols["数量"].DataType = typeof(double);
                computC1FlexGrid.Cols["单价"].DataType = typeof(double);
                computC1FlexGrid.Cols["折旧年限"].DataType = typeof(double);
                computC1FlexGrid.Cols["月折旧率"].DataType = typeof(double);
                computC1FlexGrid.Cols["使用时间"].DataType = typeof(double);
                computC1FlexGrid.Cols["折旧费"].DataType = typeof(double);
                computC1FlexGrid.Cols["维修费"].DataType = typeof(double);
                computC1FlexGrid.Cols["安装及辅助费"].DataType = typeof(double);
                computC1FlexGrid.Cols["燃油料费"].DataType = typeof(double);
                computC1FlexGrid.Cols["养路费"].DataType = typeof(double);
                computC1FlexGrid.Cols["驾驶人员工资"].DataType = typeof(double);


                computC1FlexGrid.Cols["原值"].AllowEditing = false;
                computC1FlexGrid.Cols["月折旧率"].AllowEditing = false;
                computC1FlexGrid.Cols["折旧费"].AllowEditing = false;
                computC1FlexGrid.Cols["不变费小计"].AllowEditing = false;
                computC1FlexGrid.Cols["可变费小计"].AllowEditing = false;
                computC1FlexGrid.Cols["合计"].AllowEditing = false;
                displayOwnDevice();

            }
            else if (title[0] == "租赁机械单价计算")
            {

                computC1FlexGrid.Rows.RemoveRange(1, computC1FlexGrid.Rows.Count - 1);
                for (int i = 1; i < title.Length; i++)
                {
                    computC1FlexGrid[0, i] = title[i];
                    computC1FlexGrid.Cols[i].Name = title[i];
                }
                computC1FlexGrid.Cols["序号"].Width = 0;
                computC1FlexGrid.Cols["材料序号"].Width = 0;

                computC1FlexGrid.Cols["数量"].DataType = typeof(double);
                computC1FlexGrid.Cols["租赁时间"].DataType = typeof(double);
                computC1FlexGrid.Cols["租赁单价"].DataType = typeof(double);
                computC1FlexGrid.Cols["燃油"].DataType = typeof(double);
                computC1FlexGrid.Cols["安装拆卸设施费"].DataType = typeof(double);
                computC1FlexGrid.Cols["其他"].DataType = typeof(double);


                computC1FlexGrid.Cols["租金"].AllowEditing = false;
                computC1FlexGrid.Cols["合计"].AllowEditing = false;
                displayRentDevice();
            }
            else if (title[0] == "自有周转材料单价计算")
            {
                computC1FlexGrid.Rows.RemoveRange(1, computC1FlexGrid.Rows.Count - 1);
                for (int i = 1; i < title.Length; i++)
                {
                    computC1FlexGrid[0, i] = title[i];
                    computC1FlexGrid.Cols[i].Name = title[i];
                }
                computC1FlexGrid.Cols["序号"].Width = 0;
                computC1FlexGrid.Cols["材料序号"].Width = 0;

                computC1FlexGrid.Cols["材料单价"].DataType = typeof(double);
                computC1FlexGrid.Cols["原值"].DataType = typeof(double);
                computC1FlexGrid.Cols["摊销年限"].DataType = typeof(double);
                computC1FlexGrid.Cols["月摊销率"].DataType = typeof(double);
                computC1FlexGrid.Cols["使用时间"].DataType = typeof(double);
                computC1FlexGrid.Cols["圬工量"].DataType = typeof(double);

                computC1FlexGrid.Cols["原值"].AllowEditing = false;
                computC1FlexGrid.Cols["摊销额"].AllowEditing = false;
                displayOwnMaterail();
            }
            else if (title[0] == "租赁周转材料单价计算")
            {
                computC1FlexGrid.Rows.RemoveRange(1, computC1FlexGrid.Rows.Count - 1);
                for (int i = 1; i < title.Length; i++)
                {
                    computC1FlexGrid[0, i] = title[i];
                    computC1FlexGrid.Cols[i].Name = title[i];
                }
                computC1FlexGrid.Cols["序号"].Width = 0;
                computC1FlexGrid.Cols["材料序号"].Width = 0;

                computC1FlexGrid.Cols["数量"].DataType = typeof(double);
                computC1FlexGrid.Cols["租赁单价"].DataType = typeof(double);
                computC1FlexGrid.Cols["租赁时间"].DataType = typeof(double);
                computC1FlexGrid.Cols["圬工量"].DataType = typeof(double);

                computC1FlexGrid.Cols["租金合计"].AllowEditing = false;
                computC1FlexGrid.Cols["摊销额"].AllowEditing = false;
                displayRentMaterial();
            }

            computeC1SplitterPanel.Show();
        }
        private void displayMixMaterial()
        {
            //computC1FlexGrid.Clear(C1.Win.C1FlexGrid.ClearFlags.Content);
            string sql = string.Format("select * from mix_material_rule where 材料序号='{0}'", inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "序号"]);
            mixMaterialAdapter = SqlHelper.getSqlAdapter(sql);
            mixMaterialRule = new DataTable();
            mixMaterialAdapter.Fill(mixMaterialRule);
            deleteRowToolStripMenuItem.Enabled = false;
            addRowToolStripMenuItem.Enabled = true;
            foreach (DataRow dr in mixMaterialRule.Rows)
            {
                C1.Win.C1FlexGrid.Row newRow = computC1FlexGrid.Rows.Insert(computC1FlexGrid.Rows.Count);
                newRow.Height = 35;
                newRow["材料序号"] = dr["材料序号"];
                newRow["序号"] = dr["序号"];
                newRow["编号"] = dr["编号"];
                newRow["材料名称"] = dr["材料名称"];
                newRow["规格型号"] = dr["规格型号"];
                newRow["材料单位"] = dr["材料单位"];
                newRow["理论数量"] = dr["理论数量"];
                newRow["损耗数量"] = dr["损耗数量"];
                newRow["实际数量"] = dr["实际数量"];
                newRow["材料单价"] = dr["材料单价"];
                newRow["材料费"] = dr["材料费"];

                deleteRowToolStripMenuItem.Enabled = true;
            }
        }
        private void displayOtherMaterial()
        {
            string sql = string.Format("select * from other_material_rule where 材料序号='{0}'", inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "序号"]);
            otherMaterialAdapter = SqlHelper.getSqlAdapter(sql);
            otherMaterialRule = new DataTable();
            otherMaterialAdapter.Fill(otherMaterialRule);
            deleteRowToolStripMenuItem.Enabled = false;
            addRowToolStripMenuItem.Enabled = true;
            foreach (DataRow dr in otherMaterialRule.Rows)
            {
                C1.Win.C1FlexGrid.Row newRow = computC1FlexGrid.Rows.Insert(computC1FlexGrid.Rows.Count);
                newRow.Height = 35;
                newRow["材料序号"] = dr["材料序号"];
                newRow["序号"] = dr["序号"];
                newRow["编号"] = dr["编号"];
                newRow["材料名称"] = dr["材料名称"];
                newRow["规格型号"] = dr["规格型号"];
                newRow["材料单位"] = dr["材料单位"];
                newRow["理论数量"] = dr["理论数量"];
                newRow["损耗数量"] = dr["损耗数量"];
                newRow["实际数量"] = dr["实际数量"];
                newRow["材料单价"] = dr["材料单价"];
                newRow["材料费"] = dr["材料费"];
                newRow["综合单价"] = dr["综合单价"];

                deleteRowToolStripMenuItem.Enabled = true;
                addRowToolStripMenuItem.Enabled = false;
            }
        }

        private void displayOwnDevice()
        {
            string sql = string.Format("select * from own_device_rule where 材料序号='{0}'", inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "序号"]);
            ownDeviceAdapter = SqlHelper.getSqlAdapter(sql);
            ownDeviceRule = new DataTable();
            ownDeviceAdapter.Fill(ownDeviceRule);
            deleteRowToolStripMenuItem.Enabled = false;
            addRowToolStripMenuItem.Enabled = true;
            foreach (DataRow dr in ownDeviceRule.Rows)
            {
                C1.Win.C1FlexGrid.Row newRow = computC1FlexGrid.Rows.Insert(computC1FlexGrid.Rows.Count);
                newRow.Height = 35;
                newRow["序号"] = dr["序号"];
                newRow["材料序号"] = dr["材料序号"];
                newRow["设备名称"] = dr["设备名称"];
                newRow["规格型号"] = dr["规格型号"];
                newRow["细目名称"] = dr["项目名称"];
                newRow["单位"] = dr["单位"];
                newRow["数量"] = dr["数量"];
                newRow["单价"] = dr["单价"];
                newRow["原值"] = dr["原值"];
                newRow["折旧年限"] = dr["折旧年限"];
                newRow["月折旧率"] = dr["月折旧率"];
                newRow["使用时间"] = dr["使用时间"];
                newRow["折旧费"] = dr["折旧费"];
                newRow["维修费"] = dr["维修费"];
                newRow["安装及辅助费"] = dr["安装及辅助费"];
                newRow["不变费小计"] = dr["不变费小计"];
                newRow["燃油料费"] = dr["燃油料费"];
                newRow["养路费"] = dr["养路费"];
                newRow["驾驶人员工资"] = dr["驾驶人员工资"];
                newRow["可变费小计"] = dr["可变费小计"];
                newRow["合计"] = dr["合计"];

                deleteRowToolStripMenuItem.Enabled = true;
                addRowToolStripMenuItem.Enabled = false;
            }
        }

        private void displayRentDevice()
        {
            string sql = string.Format("select * from rent_device_rule where 材料序号='{0}'", inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "序号"]);
            rentDeviceAdapter = SqlHelper.getSqlAdapter(sql);
            rentDeviceRule = new DataTable();
            rentDeviceAdapter.Fill(rentDeviceRule);
            deleteRowToolStripMenuItem.Enabled = false;
            addRowToolStripMenuItem.Enabled = true;
            foreach (DataRow dr in rentDeviceRule.Rows)
            {
                C1.Win.C1FlexGrid.Row newRow = computC1FlexGrid.Rows.Insert(computC1FlexGrid.Rows.Count);
                newRow.Height = 35;
                newRow["序号"] = dr["序号"];
                newRow["材料序号"] = dr["材料序号"];
                newRow["租赁设备名称"] = dr["租赁设备名称"];
                newRow["规格型号"] = dr["规格型号"];
                newRow["单位"] = dr["单位"];
                newRow["数量"] = dr["数量"];
                newRow["租赁时间"] = dr["租赁时间"];
                newRow["租赁单价"] = dr["租赁单价"];
                newRow["租金"] = dr["租金"];
                newRow["燃油"] = dr["燃油"];
                newRow["安装拆卸设施费"] = dr["安装拆卸设施费"];
                newRow["其他"] = dr["其他"];
                newRow["合计"] = dr["合计"];
                newRow["备注"] = dr["备注"];

                deleteRowToolStripMenuItem.Enabled = true;
                addRowToolStripMenuItem.Enabled = false;
            }
        }

        private void displayOwnMaterail()
        {
            string sql = string.Format("select * from own_material_rule where 材料序号='{0}'", inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "序号"]);
            ownMaterialAdapter = SqlHelper.getSqlAdapter(sql);
            ownMaterialRule = new DataTable();
            ownMaterialAdapter.Fill(ownMaterialRule);
            deleteRowToolStripMenuItem.Enabled = false;
            addRowToolStripMenuItem.Enabled = true;
            foreach (DataRow dr in ownMaterialRule.Rows)
            {
                C1.Win.C1FlexGrid.Row newRow = computC1FlexGrid.Rows.Insert(computC1FlexGrid.Rows.Count);
                newRow["材料序号"] = dr["材料序号"];
                newRow["序号"] = dr["序号"];
                newRow["周转材料名称"] = dr["周转材料名称"];
                newRow["单位"] = dr["单位"];
                newRow["数量"] = dr["数量"];
                newRow["材料单价"] = dr["材料单价"];
                newRow["原值"] = dr["原值"];
                newRow["摊销年限"] = dr["摊销年限"];
                newRow["月摊销率"] = dr["月摊销率"];
                newRow["使用时间"] = dr["使用时间"];
                newRow["使用部位"] = dr["使用部位"];
                newRow["圬工量"] = dr["圬工量"];
                newRow["摊销额"] = dr["摊销额"];
                newRow["备注"] = dr["备注"];

                deleteRowToolStripMenuItem.Enabled = true;
                addRowToolStripMenuItem.Enabled = false;
            }
        }

        private void displayRentMaterial()
        {
            string sql = string.Format("select * from rent_material_rule where 材料序号='{0}'", inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "序号"]);
            rentMaterialAdapter = SqlHelper.getSqlAdapter(sql);
            rentMaterialRule = new DataTable();
            rentMaterialAdapter.Fill(rentMaterialRule);
            deleteRowToolStripMenuItem.Enabled = false;
            addRowToolStripMenuItem.Enabled = true;
            foreach (DataRow dr in rentMaterialRule.Rows)
            {
                C1.Win.C1FlexGrid.Row newRow = computC1FlexGrid.Rows.Insert(computC1FlexGrid.Rows.Count);
                newRow["材料序号"] = dr[  "材料序号"];
                newRow["序号"] = dr["序号"];
                newRow["周转材料名称"] = dr[  "周转材料名称"];
                newRow["单位"] = dr[  "单位"];
                newRow["数量"] = dr[  "数量"];
                newRow["租赁单价"] = dr[  "租赁单价"];
                newRow["租赁时间"] = dr[  "租赁时间"];
                newRow["租金合计"] = dr[  "租金合计"];
                newRow["使用部位"] = dr[  "使用部位"];
                newRow["圬工量"] = dr[  "圬工量"];
                newRow["摊销额"] = dr[  "摊销额"];
                newRow["备注"] = dr[  "备注"];

                deleteRowToolStripMenuItem.Enabled = true;
                addRowToolStripMenuItem.Enabled = false;
            }
        }

        private void MaterialSummaryForm_Shown(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }
        private void displayBidInventory(string type)
        {
            inventoryC1FlexGrid1.Rows.RemoveRange(1, inventoryC1FlexGrid1.Rows.Count - 1);
            string sql = "";
            switch (type)
            {
                case "1":
                    sql = "SELECT * FROM bid_material_inventory where 租赁=false and 混合=false and 材料类型<>'周转材料' and length(编号)=13";
                    break;
                case "2":
                    sql = "SELECT * FROM bid_material_inventory where 混合=true and length(编号)=13";
                    break;
                case "3":
                    sql = "SELECT * FROM bid_material_inventory where 租赁=false and 材料类型='周转材料' and length(编号)=13";
                    break;
                case "4":
                    sql = "SELECT * FROM bid_material_inventory where 租赁=true and 材料类型='周转材料' and length(编号)=13";
                    break;
                case "5":
                    sql = "SELECT * FROM bid_material_inventory where  租赁=false and length(编号)=6";
                    break;
                case "6":
                    sql = "SELECT * FROM bid_material_inventory where 租赁=true and length(编号)=6";
                    break;
                case "7":
                    sql = "SELECT * FROM bid_material_inventory where length(编号)=1";
                    break;
                case "all":
                    sql = "SELECT * FROM bid_material_inventory";
                    break;
            }

            bidMaterialInventoryAdapter = SqlHelper.getSqlAdapter(sql);
            bidMaterialInventory = new DataTable();
            bidMaterialInventoryAdapter.Fill(bidMaterialInventory);
            inventoryC1FlexGrid1.AllowEditing = false;
            foreach (DataRow dr in bidMaterialInventory.Rows)
            {
                C1.Win.C1FlexGrid.Row newRow = inventoryC1FlexGrid1.Rows.Insert(inventoryC1FlexGrid1.Rows.Count);
                newRow.Height = 35;
                newRow["序号"] = dr["序号"];
                newRow["编号"] = dr["编号"];
                newRow["名称"] = dr["名称"];
                newRow["单位"] = dr["单位"];
                newRow["数量"] = dr["数量"];
                newRow["租赁"] = dr["租赁"];
                newRow["混合"] = dr["混合"];
                newRow["单价"] = dr["预算单价"];
                newRow["合价"] = dr["预算合价"];
                newRow["材料类型"] = dr["材料类型"];
            }

        }

        private void inventoryC1FlexGrid1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (inventoryC1FlexGrid1.RowSel > 0)
                {
                    if (((string)inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "编号"]).Trim().Length == 13)
                    {
                        if ((string)inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "材料类型"] == "周转材料")
                        {

                            if ((bool)inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "租赁"])
                            {
                                displayComputeGridTitle(rentMaterialTitle);
                            }
                            else
                            {
                                displayComputeGridTitle(ownMaterialTitle);
                            }

                        }
                        else
                        {
                            if ((bool)inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "混合"])
                            {
                                displayComputeGridTitle(mixMaterialTitle);
                            }
                            else
                            {
                                displayComputeGridTitle(otherMaterialTitle);
                            }

                        }
                    }
                    else if (((string)inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "编号"]).Trim().Length == 6)
                    {
                        if ((bool)inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "租赁"])
                        {
                            displayComputeGridTitle(rentDeviceTitle);
                        }
                        else
                        {
                            displayComputeGridTitle(ownDeviceTitle);
                        }

                    }

                }
            }
        }

        private void computC1FlexGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                computGridContextMenuStrip.Show(computC1FlexGrid, e.Location);
            }
        }


        private void computC1FlexGrid_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (computeC1SplitterPanel.Text == "混合材料单价计算")
            {
                mixMaterialEdit(e);
            }
            else if (computeC1SplitterPanel.Text == "其他材料单价计算")
            {
                otherMaterialEdit(e);
            }
            else if (computeC1SplitterPanel.Text == "自有机械单价计算")
            {
                ownDeviceEdit(e);
            }
            else if (computeC1SplitterPanel.Text == "租赁机械单价计算")
            {
                rentDeviceEdit(e);
            }
            else if (computeC1SplitterPanel.Text == "自有周转材料单价计算")
            {
                ownMaterialEdit(e);
            }
            else if (computeC1SplitterPanel.Text == "租赁周转材料单价计算")
            {
                rentMaterialEdit(e);
            }

        }

        private void updateInventory(int orderNumber, double price)
        {
            inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "单价"] = price;
            inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "合价"] = price * Convert.ToDouble(inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "数量"] != DBNull.Value ? inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "数量"] : 0);

            DataRow[] drs = bidMaterialInventory.Select(string.Format("序号={0}", orderNumber));
            DataRow tempRow = drs[0];
            tempRow["预算单价"] = Convert.ToDouble(inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "单价"]);
            tempRow["预算合价"] = Convert.ToDouble(inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "合价"]);
            bidMaterialInventoryAdapter.Update(bidMaterialInventory);
            ((MainForm)this.TopLevelControl).frmDirectProFee.updateDirectFee(orderNumber,Convert.ToDouble(inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "单价"]),Convert.ToDouble(inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "合价"]));
        }

        private void mixMaterialEdit(C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (computC1FlexGrid[e.Row, "理论数量"] != DBNull.Value
                && computC1FlexGrid[e.Row, "理论数量"] != null
                && !Double.IsNaN(Convert.ToDouble(computC1FlexGrid[e.Row, "理论数量"]))
                && computC1FlexGrid[e.Row, "损耗数量"] != DBNull.Value
                && computC1FlexGrid[e.Row, "损耗数量"] != null
                && !Double.IsNaN(Convert.ToDouble(computC1FlexGrid[e.Row, "损耗数量"])))
            {
                computC1FlexGrid[e.Row, "实际数量"] = Convert.ToDouble(computC1FlexGrid[e.Row, "理论数量"]) + Convert.ToDouble(computC1FlexGrid[e.Row, "损耗数量"]);

                if (computC1FlexGrid[e.Row, "编号"] != DBNull.Value
                    && !string.IsNullOrEmpty(Convert.ToString(computC1FlexGrid[e.Row, "编号"]))
                && computC1FlexGrid[e.Row, "材料名称"] != DBNull.Value
                    && !string.IsNullOrEmpty(Convert.ToString(computC1FlexGrid[e.Row, "材料名称"]))
                && computC1FlexGrid[e.Row, "材料单价"] != DBNull.Value
                && computC1FlexGrid[e.Row, "材料单价"] != null
                && !Double.IsNaN(Convert.ToDouble(computC1FlexGrid[e.Row, "材料单价"])))
                {

                    computC1FlexGrid[e.Row, "材料费"] = Convert.ToDouble(computC1FlexGrid[e.Row, "实际数量"]) * Convert.ToDouble(computC1FlexGrid[e.Row, "材料单价"]);
                    if (computC1FlexGrid[e.Row, "序号"] != null)
                    {
                        DataRow[] drs = mixMaterialRule.Select(string.Format("序号={0}", computC1FlexGrid[e.Row, "序号"]));
                        DataRow tempRow = drs[0];
                        tempRow["材料序号"] = computC1FlexGrid[e.Row, "材料序号"];
                        tempRow["编号"] = computC1FlexGrid[e.Row, "编号"];
                        tempRow["材料名称"] = computC1FlexGrid[e.Row, "材料名称"];
                        tempRow["规格型号"] = computC1FlexGrid[e.Row, "规格型号"];
                        tempRow["材料单位"] = computC1FlexGrid[e.Row, "材料单位"];
                        tempRow["理论数量"] = computC1FlexGrid[e.Row, "理论数量"];
                        tempRow["损耗数量"] = computC1FlexGrid[e.Row, "损耗数量"];
                        tempRow["实际数量"] = computC1FlexGrid[e.Row, "实际数量"];
                        tempRow["材料单价"] = computC1FlexGrid[e.Row, "材料单价"];
                        tempRow["材料费"] = computC1FlexGrid[e.Row, "材料费"];

                    }
                    else
                    {
                        DataRow newRow = mixMaterialRule.NewRow();
                        newRow["材料序号"] = computC1FlexGrid[e.Row, "材料序号"];
                        newRow["编号"] = computC1FlexGrid[e.Row, "编号"];
                        newRow["材料名称"] = computC1FlexGrid[e.Row, "材料名称"];
                        newRow["规格型号"] = computC1FlexGrid[e.Row, "规格型号"];
                        newRow["材料单位"] = computC1FlexGrid[e.Row, "材料单位"];
                        newRow["理论数量"] = computC1FlexGrid[e.Row, "理论数量"];
                        newRow["损耗数量"] = computC1FlexGrid[e.Row, "损耗数量"];
                        newRow["实际数量"] = computC1FlexGrid[e.Row, "实际数量"];
                        newRow["材料单价"] = computC1FlexGrid[e.Row, "材料单价"];
                        newRow["材料费"] = computC1FlexGrid[e.Row, "材料费"];
                        mixMaterialRule.Rows.Add(newRow);

                    }
                    mixMaterialAdapter.Update(mixMaterialRule);
                    double price = 0;
                    for (int i = 1; i < computC1FlexGrid.Rows.Count; i++)
                    {
                        price += Convert.ToDouble(computC1FlexGrid[i, "材料费"]);
                    }
                    updateInventory(Convert.ToInt16(computC1FlexGrid[e.Row, "材料序号"]), price);
                    displayComputeGridTitle(mixMaterialTitle);
                }

            }

        }
        private void otherMaterialEdit(C1.Win.C1FlexGrid.RowColEventArgs e)
        {

            if (computC1FlexGrid[e.Row, "理论数量"] != null && computC1FlexGrid[e.Row, "损耗数量"] != null)
            {
                computC1FlexGrid[e.Row, "实际数量"] = Convert.ToDouble(computC1FlexGrid[e.Row, "理论数量"]) + Convert.ToDouble(computC1FlexGrid[e.Row, "损耗数量"]);
                if (computC1FlexGrid[e.Row, "编号"] != null
                    && computC1FlexGrid[e.Row, "材料名称"] != null
                    && computC1FlexGrid[e.Row, "材料单价"] != null)
                {

                    computC1FlexGrid[e.Row, "材料费"] = Convert.ToDouble(computC1FlexGrid[e.Row, "实际数量"]) * Convert.ToDouble(computC1FlexGrid[e.Row, "材料单价"]);
                    computC1FlexGrid[e.Row, "综合单价"] = Math.Round(Convert.ToDouble(computC1FlexGrid[e.Row, "材料费"]) / Convert.ToDouble(computC1FlexGrid[e.Row, "理论数量"]), 2);
                    string sql = string.Format("SELECT * FROM other_material_rule");
                    MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (computC1FlexGrid[e.Row, "序号"] != null)
                    {
                        DataRow[] drs = dt.Select(string.Format("序号={0}", computC1FlexGrid[e.Row, "序号"]));
                        DataRow tempRow = drs[0];
                        tempRow["材料序号"] = computC1FlexGrid[e.Row, "材料序号"];
                        tempRow["编号"] = computC1FlexGrid[e.Row, "编号"];
                        tempRow["材料名称"] = computC1FlexGrid[e.Row, "材料名称"];
                        tempRow["规格型号"] = computC1FlexGrid[e.Row, "规格型号"];
                        tempRow["材料单位"] = computC1FlexGrid[e.Row, "材料单位"];
                        tempRow["理论数量"] = computC1FlexGrid[e.Row, "理论数量"];
                        tempRow["损耗数量"] = computC1FlexGrid[e.Row, "损耗数量"];
                        tempRow["实际数量"] = computC1FlexGrid[e.Row, "实际数量"];
                        tempRow["材料单价"] = computC1FlexGrid[e.Row, "材料单价"];
                        tempRow["材料费"] = computC1FlexGrid[e.Row, "材料费"];
                        tempRow["综合单价"] = computC1FlexGrid[e.Row, "综合单价"];

                    }
                    else
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["材料序号"] = computC1FlexGrid[e.Row, "材料序号"];
                        newRow["编号"] = computC1FlexGrid[e.Row, "编号"];
                        newRow["材料名称"] = computC1FlexGrid[e.Row, "材料名称"];
                        newRow["规格型号"] = computC1FlexGrid[e.Row, "规格型号"];
                        newRow["材料单位"] = computC1FlexGrid[e.Row, "材料单位"];
                        newRow["理论数量"] = computC1FlexGrid[e.Row, "理论数量"];
                        newRow["损耗数量"] = computC1FlexGrid[e.Row, "损耗数量"];
                        newRow["实际数量"] = computC1FlexGrid[e.Row, "实际数量"];
                        newRow["材料单价"] = computC1FlexGrid[e.Row, "材料单价"];
                        newRow["材料费"] = computC1FlexGrid[e.Row, "材料费"];
                        newRow["综合单价"] = computC1FlexGrid[e.Row, "综合单价"];
                        dt.Rows.Add(newRow);
                    }
                    adapter.Update(dt);
                    updateInventory((int)computC1FlexGrid[e.Row, "材料序号"], (double)computC1FlexGrid[e.Row, "综合单价"]);
                    displayComputeGridTitle(otherMaterialTitle);
                }
            }

        }
        private void ownDeviceEdit(C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (computC1FlexGrid[e.Row, "数量"] != null && computC1FlexGrid[e.Row, "单价"] != null)
            {
                computC1FlexGrid[e.Row, "原值"] = Convert.ToDouble(computC1FlexGrid[e.Row, "数量"]) * Convert.ToDouble(computC1FlexGrid[e.Row, "单价"]);
            }
            if (computC1FlexGrid[e.Row, "折旧年限"] != null)
            {
                computC1FlexGrid[e.Row, "月折旧率"] = Math.Round((double)100 / (Convert.ToDouble(computC1FlexGrid[e.Row, "折旧年限"]) * 12), 2);

            }
            if (computC1FlexGrid[e.Row, "原值"] != null
                && computC1FlexGrid[e.Row, "月折旧率"] != null
                && computC1FlexGrid[e.Row, "使用时间"] != null)
            {
                computC1FlexGrid[e.Row, "折旧费"] = Math.Round(Convert.ToDouble(computC1FlexGrid[e.Row, "原值"])
                                                     * Convert.ToDouble(computC1FlexGrid[e.Row, "月折旧率"])
                                                        * Convert.ToDouble(computC1FlexGrid[e.Row, "使用时间"]), 2);
            }

            if (computC1FlexGrid[e.Row, "折旧费"] != null
               && computC1FlexGrid[e.Row, "维修费"] != null
               && computC1FlexGrid[e.Row, "安装及辅助费"] != null)
            {
                computC1FlexGrid[e.Row, "不变费小计"] = Math.Round(Convert.ToDouble(computC1FlexGrid[e.Row, "折旧费"])
                                                     + Convert.ToDouble(computC1FlexGrid[e.Row, "维修费"])
                                                        + Convert.ToDouble(computC1FlexGrid[e.Row, "安装及辅助费"]), 2);
            }

            if (computC1FlexGrid[e.Row, "燃油料费"] != null
              && computC1FlexGrid[e.Row, "养路费"] != null
              && computC1FlexGrid[e.Row, "驾驶人员工资"] != null)
            {
                computC1FlexGrid[e.Row, "可变费小计"] = Math.Round(Convert.ToDouble(computC1FlexGrid[e.Row, "燃油料费"])
                                                     + Convert.ToDouble(computC1FlexGrid[e.Row, "养路费"])
                                                        + Convert.ToDouble(computC1FlexGrid[e.Row, "驾驶人员工资"]), 2);
            }

            if (computC1FlexGrid[e.Row, "不变费小计"] != null
              && computC1FlexGrid[e.Row, "可变费小计"] != null)
            {
                computC1FlexGrid[e.Row, "合计"] = Math.Round(Convert.ToDouble(computC1FlexGrid[e.Row, "不变费小计"])
                                                     + Convert.ToDouble(computC1FlexGrid[e.Row, "可变费小计"]), 2);
            }

            if (computC1FlexGrid[e.Row, "合计"] != null)
            {
                string sql = string.Format("SELECT * FROM own_device_rule");
                MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (computC1FlexGrid[e.Row, "序号"] != null)
                {
                    DataRow[] drs = dt.Select(string.Format("序号={0}", computC1FlexGrid[e.Row, "序号"]));
                    DataRow tempRow = drs[0];
                    tempRow["材料序号"] = computC1FlexGrid[e.Row, "材料序号"];
                    tempRow["设备名称"] = computC1FlexGrid[e.Row, "设备名称"];
                    tempRow["规格型号"] = computC1FlexGrid[e.Row, "规格型号"];
                    tempRow["项目名称"] = computC1FlexGrid[e.Row, "细目名称"];
                    tempRow["单位"] = computC1FlexGrid[e.Row, "单位"];
                    tempRow["数量"] = computC1FlexGrid[e.Row, "数量"];
                    tempRow["单价"] = computC1FlexGrid[e.Row, "单价"];
                    tempRow["原值"] = computC1FlexGrid[e.Row, "原值"];
                    tempRow["折旧年限"] = computC1FlexGrid[e.Row, "折旧年限"];
                    tempRow["月折旧率"] = computC1FlexGrid[e.Row, "月折旧率"];
                    tempRow["使用时间"] = computC1FlexGrid[e.Row, "使用时间"];
                    tempRow["折旧费"] = computC1FlexGrid[e.Row, "折旧费"];
                    tempRow["维修费"] = computC1FlexGrid[e.Row, "维修费"];
                    tempRow["安装及辅助费"] = computC1FlexGrid[e.Row, "安装及辅助费"];
                    tempRow["不变费小计"] = computC1FlexGrid[e.Row, "不变费小计"];
                    tempRow["燃油料费"] = computC1FlexGrid[e.Row, "燃油料费"];
                    tempRow["养路费"] = computC1FlexGrid[e.Row, "养路费"];
                    tempRow["驾驶人员工资"] = computC1FlexGrid[e.Row, "驾驶人员工资"];
                    tempRow["可变费小计"] = computC1FlexGrid[e.Row, "可变费小计"];
                    tempRow["合计"] = computC1FlexGrid[e.Row, "合计"];

                }
                else
                {
                    DataRow newRow = dt.NewRow();
                    newRow["材料序号"] = computC1FlexGrid[e.Row, "材料序号"];
                    newRow["设备名称"] = computC1FlexGrid[e.Row, "设备名称"];
                    newRow["规格型号"] = computC1FlexGrid[e.Row, "规格型号"];
                    newRow["项目名称"] = computC1FlexGrid[e.Row, "细目名称"];
                    newRow["单位"] = computC1FlexGrid[e.Row, "单位"];
                    newRow["数量"] = computC1FlexGrid[e.Row, "数量"];
                    newRow["单价"] = computC1FlexGrid[e.Row, "单价"];
                    newRow["原值"] = computC1FlexGrid[e.Row, "原值"];
                    newRow["折旧年限"] = computC1FlexGrid[e.Row, "折旧年限"];
                    newRow["月折旧率"] = computC1FlexGrid[e.Row, "月折旧率"];
                    newRow["使用时间"] = computC1FlexGrid[e.Row, "使用时间"];
                    newRow["折旧费"] = computC1FlexGrid[e.Row, "折旧费"];
                    newRow["维修费"] = computC1FlexGrid[e.Row, "维修费"];
                    newRow["安装及辅助费"] = computC1FlexGrid[e.Row, "安装及辅助费"];
                    newRow["不变费小计"] = computC1FlexGrid[e.Row, "不变费小计"];
                    newRow["燃油料费"] = computC1FlexGrid[e.Row, "燃油料费"];
                    newRow["养路费"] = computC1FlexGrid[e.Row, "养路费"];
                    newRow["驾驶人员工资"] = computC1FlexGrid[e.Row, "驾驶人员工资"];
                    newRow["可变费小计"] = computC1FlexGrid[e.Row, "可变费小计"];
                    newRow["合计"] = computC1FlexGrid[e.Row, "合计"];
                    dt.Rows.Add(newRow);
                }
                adapter.Update(dt);
                updateInventory((int)computC1FlexGrid[e.Row, "材料序号"], Convert.ToDouble(computC1FlexGrid[e.Row, "合计"]) / Convert.ToDouble(computC1FlexGrid[e.Row, "数量"]));
                displayComputeGridTitle(ownDeviceTitle);
            }
        }
        private void rentDeviceEdit(C1.Win.C1FlexGrid.RowColEventArgs e)
        {

            if (computC1FlexGrid[e.Row, "数量"] != null && computC1FlexGrid[e.Row, "租赁时间"] != null && computC1FlexGrid[e.Row, "租赁单价"] != null)
            {
                computC1FlexGrid[e.Row, "租金"] = Convert.ToDouble(computC1FlexGrid[e.Row, "数量"])
                                                    * Convert.ToDouble(computC1FlexGrid[e.Row, "租赁时间"])
                                                    * Convert.ToDouble(computC1FlexGrid[e.Row, "租赁单价"]);
            }
            if (computC1FlexGrid[e.Row, "租金"] != null
                 && computC1FlexGrid[e.Row, "燃油"] != null
                 && computC1FlexGrid[e.Row, "安装拆卸设施费"] != null
                && computC1FlexGrid[e.Row, "其他"] != null)
            {
                computC1FlexGrid[e.Row, "合计"] = Convert.ToDouble(computC1FlexGrid[e.Row, "租金"])
                                                    + Convert.ToDouble(computC1FlexGrid[e.Row, "燃油"])
                                                    + Convert.ToDouble(computC1FlexGrid[e.Row, "安装拆卸设施费"])
                                                    + Convert.ToDouble(computC1FlexGrid[e.Row, "其他"]);
            }
            if (computC1FlexGrid[e.Row, "合计"] != null)
            {

                string sql = string.Format("SELECT * FROM rent_device_rule");
                MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (computC1FlexGrid[e.Row, "序号"] != null)
                {
                    DataRow[] drs = dt.Select(string.Format("序号={0}", computC1FlexGrid[e.Row, "序号"]));
                    DataRow tempRow = drs[0];
                    tempRow["材料序号"] = computC1FlexGrid[e.Row, "材料序号"];
                    tempRow["租赁设备名称"] = computC1FlexGrid[e.Row, "租赁设备名称"];
                    tempRow["规格型号"] = computC1FlexGrid[e.Row, "规格型号"];
                    tempRow["单位"] = computC1FlexGrid[e.Row, "单位"];
                    tempRow["数量"] = computC1FlexGrid[e.Row, "数量"];
                    tempRow["租赁时间"] = computC1FlexGrid[e.Row, "租赁时间"];
                    tempRow["租赁单价"] = computC1FlexGrid[e.Row, "租赁单价"];
                    tempRow["租金"] = computC1FlexGrid[e.Row, "租金"];
                    tempRow["燃油"] = computC1FlexGrid[e.Row, "燃油"];
                    tempRow["安装拆卸设施费"] = computC1FlexGrid[e.Row, "安装拆卸设施费"];
                    tempRow["其他"] = computC1FlexGrid[e.Row, "其他"];
                    tempRow["合计"] = computC1FlexGrid[e.Row, "合计"];
                    tempRow["备注"] = computC1FlexGrid[e.Row, "备注"];

                }
                else
                {
                    DataRow newRow = dt.NewRow();
                    newRow["材料序号"] = computC1FlexGrid[e.Row, "材料序号"];
                    newRow["租赁设备名称"] = computC1FlexGrid[e.Row, "租赁设备名称"];
                    newRow["规格型号"] = computC1FlexGrid[e.Row, "规格型号"];
                    newRow["单位"] = computC1FlexGrid[e.Row, "单位"];
                    newRow["数量"] = computC1FlexGrid[e.Row, "数量"];
                    newRow["租赁时间"] = computC1FlexGrid[e.Row, "租赁时间"];
                    newRow["租赁单价"] = computC1FlexGrid[e.Row, "租赁单价"];
                    newRow["租金"] = computC1FlexGrid[e.Row, "租金"];
                    newRow["燃油"] = computC1FlexGrid[e.Row, "燃油"];
                    newRow["安装拆卸设施费"] = computC1FlexGrid[e.Row, "安装拆卸设施费"];
                    newRow["其他"] = computC1FlexGrid[e.Row, "其他"];
                    newRow["合计"] = computC1FlexGrid[e.Row, "合计"];
                    newRow["备注"] = computC1FlexGrid[e.Row, "备注"];
                    dt.Rows.Add(newRow);
                }
                adapter.Update(dt);
                updateInventory((int)computC1FlexGrid[e.Row, "材料序号"], Convert.ToDouble(computC1FlexGrid[e.Row, "合计"]) / Convert.ToDouble(computC1FlexGrid[e.Row, "数量"]));
                displayComputeGridTitle(rentDeviceTitle);
            }
        }
        private void ownMaterialEdit(C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (computC1FlexGrid[e.Row, "数量"] != null && computC1FlexGrid[e.Row, "材料单价"] != null)
            {
                computC1FlexGrid[e.Row, "原值"] = Math.Round(Convert.ToDouble(computC1FlexGrid[e.Row, "数量"])
                                                    * Convert.ToDouble(computC1FlexGrid[e.Row, "材料单价"]),2);
            }
            if (computC1FlexGrid[e.Row, "原值"] != null
                 && computC1FlexGrid[e.Row, "月摊销率"] != null
                 && computC1FlexGrid[e.Row, "使用时间"] != null
                && computC1FlexGrid[e.Row, "圬工量"] != null)
            {
                computC1FlexGrid[e.Row, "摊销额"] = Math.Round((Convert.ToDouble(computC1FlexGrid[e.Row, "原值"])
                                                    * Convert.ToDouble(computC1FlexGrid[e.Row, "月摊销率"])
                                                    * Convert.ToDouble(computC1FlexGrid[e.Row, "使用时间"]))
                                                    / Convert.ToDouble(computC1FlexGrid[e.Row, "圬工量"]),2);
            }
            if (computC1FlexGrid[e.Row, "摊销额"] != null)
            {

                string sql = string.Format("SELECT * FROM own_material_rule");
                MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (computC1FlexGrid[e.Row, "序号"] != null)
                {
                    DataRow[] drs = dt.Select(string.Format("序号={0}", computC1FlexGrid[e.Row, "序号"]));
                    DataRow tempRow = drs[0];
                    tempRow["材料序号"] = computC1FlexGrid[e.Row, "材料序号"];
                    tempRow["周转材料名称"] = computC1FlexGrid[e.Row, "周转材料名称"];
                    tempRow["单位"] = computC1FlexGrid[e.Row, "单位"];
                    tempRow["数量"] = computC1FlexGrid[e.Row, "数量"];
                    tempRow["材料单价"] = computC1FlexGrid[e.Row, "材料单价"];
                    tempRow["原值"] = computC1FlexGrid[e.Row, "原值"];
                    tempRow["摊销年限"] = computC1FlexGrid[e.Row, "摊销年限"];
                    tempRow["月摊销率"] = computC1FlexGrid[e.Row, "月摊销率"];
                    tempRow["使用时间"] = computC1FlexGrid[e.Row, "使用时间"];
                    tempRow["使用部位"] = computC1FlexGrid[e.Row, "使用部位"];
                    tempRow["圬工量"] = computC1FlexGrid[e.Row, "圬工量"];
                    tempRow["摊销额"] = computC1FlexGrid[e.Row, "摊销额"];
                    tempRow["备注"] = computC1FlexGrid[e.Row, "备注"];

                }
                else
                {
                    DataRow newRow = dt.NewRow();
                    newRow["材料序号"] = computC1FlexGrid[e.Row, "材料序号"];
                    newRow["周转材料名称"] = computC1FlexGrid[e.Row, "周转材料名称"];
                    newRow["单位"] = computC1FlexGrid[e.Row, "单位"];
                    newRow["数量"] = computC1FlexGrid[e.Row, "数量"];
                    newRow["材料单价"] = computC1FlexGrid[e.Row, "材料单价"];
                    newRow["原值"] = computC1FlexGrid[e.Row, "原值"];
                    newRow["摊销年限"] = computC1FlexGrid[e.Row, "摊销年限"];
                    newRow["月摊销率"] = computC1FlexGrid[e.Row, "月摊销率"];
                    newRow["使用时间"] = computC1FlexGrid[e.Row, "使用时间"];
                    newRow["使用部位"] = computC1FlexGrid[e.Row, "使用部位"];
                    newRow["圬工量"] = computC1FlexGrid[e.Row, "圬工量"];
                    newRow["摊销额"] = computC1FlexGrid[e.Row, "摊销额"];
                    newRow["备注"] = computC1FlexGrid[e.Row, "备注"];
                    dt.Rows.Add(newRow);
                }
                adapter.Update(dt);
                updateInventory((int)computC1FlexGrid[e.Row, "材料序号"], Convert.ToDouble(computC1FlexGrid[e.Row, "摊销额"]));
                displayComputeGridTitle(ownMaterialTitle);
            }
        }
        private void rentMaterialEdit(C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            
            if (computC1FlexGrid[e.Row, "数量"] != null 
                && computC1FlexGrid[e.Row, "租赁单价"] != null 
                && computC1FlexGrid[e.Row, "租赁时间"] != null)
            {
                computC1FlexGrid[e.Row, "租金合计"] = Convert.ToDouble(computC1FlexGrid[e.Row, "数量"]) 
                                                        * Convert.ToDouble(computC1FlexGrid[e.Row, "租赁单价"])
                                                        *Convert.ToDouble(computC1FlexGrid[e.Row, "租赁时间"]);
            }
            if(computC1FlexGrid[e.Row, "圬工量"]!=null)
            {
                computC1FlexGrid[e.Row, "摊销额"] = Math.Round(Convert.ToDouble(computC1FlexGrid[e.Row, "租金合计"]) / Convert.ToDouble(computC1FlexGrid[e.Row, "圬工量"]),2);
            }
            if (computC1FlexGrid[e.Row, "租金合计"] != null
                && computC1FlexGrid[e.Row, "圬工量"] != null)
            {

                string sql = string.Format("SELECT * FROM rent_material_rule");
                MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                
                 if (computC1FlexGrid[e.Row, "序号"] != null)
                {
                     DataRow[] drs = dt.Select(string.Format("序号={0}", computC1FlexGrid[e.Row, "序号"]));
                    DataRow tempRow = drs[0];
                    tempRow["材料序号"] = computC1FlexGrid[e.Row, "材料序号"];
                    tempRow["周转材料名称"] = computC1FlexGrid[e.Row, "周转材料名称"];
                    tempRow["单位"] = computC1FlexGrid[e.Row, "单位"];
                    tempRow["数量"] = computC1FlexGrid[e.Row, "数量"];
                    tempRow["租赁单价"] = computC1FlexGrid[e.Row, "租赁单价"];
                    tempRow["租赁时间"] = computC1FlexGrid[e.Row, "租赁时间"];
                    tempRow["租金合计"] = computC1FlexGrid[e.Row, "租金合计"];
                    tempRow["使用部位"] = computC1FlexGrid[e.Row, "使用部位"];
                    tempRow["圬工量"] = computC1FlexGrid[e.Row, "圬工量"];
                    tempRow["摊销额"] = computC1FlexGrid[e.Row, "摊销额"];
                    tempRow["备注"] = computC1FlexGrid[e.Row, "备注"];
                }
                else
                {
                    DataRow newRow = dt.NewRow();
                    newRow["材料序号"] = computC1FlexGrid[e.Row, "材料序号"];
                    newRow["周转材料名称"] = computC1FlexGrid[e.Row, "周转材料名称"];
                    newRow["单位"] = computC1FlexGrid[e.Row, "单位"];
                    newRow["数量"] = computC1FlexGrid[e.Row, "数量"];
                    newRow["租赁单价"] = computC1FlexGrid[e.Row, "租赁单价"];
                    newRow["租赁时间"] = computC1FlexGrid[e.Row, "租赁时间"];
                    newRow["租金合计"] = computC1FlexGrid[e.Row, "租金合计"];
                    newRow["使用部位"] = computC1FlexGrid[e.Row, "使用部位"];
                    newRow["圬工量"] = computC1FlexGrid[e.Row, "圬工量"];
                    newRow["摊销额"] = computC1FlexGrid[e.Row, "摊销额"];
                    newRow["备注"] = computC1FlexGrid[e.Row, "备注"];
                    dt.Rows.Add(newRow);
                }
                adapter.Update(dt);
                updateInventory((int)computC1FlexGrid[e.Row, "材料序号"], Convert.ToDouble(computC1FlexGrid[e.Row, "摊销额"]));
                displayComputeGridTitle(rentMaterialTitle);
            }
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (computC1FlexGrid[computC1FlexGrid.RowSel, "序号"] != null)
            {
                if (computeC1SplitterPanel.Text == "混合材料单价计算")
                {
                    int materialNumber = (int)computC1FlexGrid[computC1FlexGrid.RowSel, "材料序号"];
                    DataRow[] drs = mixMaterialRule.Select(string.Format("序号={0}", (int)computC1FlexGrid[computC1FlexGrid.RowSel, "序号"]));
                    drs[0].Delete();
                    mixMaterialAdapter.Update(mixMaterialRule);
                    computC1FlexGrid.Rows.Remove(computC1FlexGrid.RowSel);
                    double price = 0;
                    for (int i = 1; i < computC1FlexGrid.Rows.Count; i++)
                    {
                        price += Convert.ToDouble(computC1FlexGrid[i, "材料费"]);
                    }

                    updateInventory(materialNumber, price);


                }
                else if (computeC1SplitterPanel.Text == "其他材料单价计算")
                {
                    DataRow[] drs = otherMaterialRule.Select(string.Format("序号={0}", (int)computC1FlexGrid[computC1FlexGrid.RowSel, "序号"]));
                    drs[0].Delete();
                    otherMaterialAdapter.Update(otherMaterialRule);

                    updateInventory((int)computC1FlexGrid[computC1FlexGrid.RowSel, "材料序号"], 0);
                    computC1FlexGrid.Rows.Remove(computC1FlexGrid.RowSel);
                }
                else if (computeC1SplitterPanel.Text == "自有机械单价计算")
                {
                    DataRow[] drs = ownDeviceRule.Select(string.Format("序号={0}", (int)computC1FlexGrid[computC1FlexGrid.RowSel, "序号"]));
                    drs[0].Delete();
                    ownDeviceAdapter.Update(ownDeviceRule);
                    updateInventory((int)computC1FlexGrid[computC1FlexGrid.RowSel, "材料序号"], 0);
                    computC1FlexGrid.Rows.Remove(computC1FlexGrid.RowSel);

                }
                else if (computeC1SplitterPanel.Text == "租赁机械单价计算")
                {
                    DataRow[] drs = rentDeviceRule.Select(string.Format("序号={0}", (int)computC1FlexGrid[computC1FlexGrid.RowSel, "序号"]));
                    drs[0].Delete();
                    rentDeviceAdapter.Update(rentDeviceRule);
                    updateInventory((int)computC1FlexGrid[computC1FlexGrid.RowSel, "材料序号"], 0);
                    computC1FlexGrid.Rows.Remove(computC1FlexGrid.RowSel);

                }
                else if (computeC1SplitterPanel.Text == "自有周转材料单价计算")
                {
                    DataRow[] drs = ownMaterialRule.Select(string.Format("序号={0}", (int)computC1FlexGrid[computC1FlexGrid.RowSel, "序号"]));
                    drs[0].Delete();
                    ownMaterialAdapter.Update(ownMaterialRule);
                    updateInventory((int)computC1FlexGrid[computC1FlexGrid.RowSel, "材料序号"], 0);
                    computC1FlexGrid.Rows.Remove(computC1FlexGrid.RowSel);

                }
                else if (computeC1SplitterPanel.Text == "租赁周转材料单价计算")
                {
                    DataRow[] drs = rentMaterialRule.Select(string.Format("序号={0}", (int)computC1FlexGrid[computC1FlexGrid.RowSel, "序号"]));
                    drs[0].Delete();
                    rentMaterialAdapter.Update(rentMaterialRule);
                    updateInventory((int)computC1FlexGrid[computC1FlexGrid.RowSel, "材料序号"], 0);
                    computC1FlexGrid.Rows.Remove(computC1FlexGrid.RowSel);
                }
            }
            else
            {
                computC1FlexGrid.Rows.Remove(computC1FlexGrid.RowSel);
            }
            if (computeC1SplitterPanel.Text == "混合材料单价计算")
            {
                if (computC1FlexGrid.Rows.Count <= 1)
                {
                    deleteRowToolStripMenuItem.Enabled = false;
                }
                addRowToolStripMenuItem.Enabled = true;
            }
            else if (computeC1SplitterPanel.Text == "自有机械单价计算")
            {
                if (computC1FlexGrid.Rows.Count <= 2)
                {
                    deleteRowToolStripMenuItem.Enabled = false;
                }
                addRowToolStripMenuItem.Enabled = true;
            }
            else
            {
                deleteRowToolStripMenuItem.Enabled = false;
                addRowToolStripMenuItem.Enabled = true;
            }

        }

        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            C1.Win.C1FlexGrid.Row newRow = computC1FlexGrid.Rows.Insert(computC1FlexGrid.Rows.Count);
            newRow["材料序号"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "序号"];
            if (computeC1SplitterPanel.Text == "混合材料单价计算")
            {
                deleteRowToolStripMenuItem.Enabled = true;
            }
            else if (computeC1SplitterPanel.Text == "其他材料单价计算")
            {
                newRow["编号"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "编号"];
                newRow["材料名称"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "名称"];
                newRow["规格型号"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "规格"];
                newRow["材料单位"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "单位"];
                addRowToolStripMenuItem.Enabled = false;
                deleteRowToolStripMenuItem.Enabled = true;
            }
            else if (computeC1SplitterPanel.Text == "自有机械单价计算")
            {
                newRow["细目名称"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "编号"];
                newRow["设备名称"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "名称"];
                newRow["规格型号"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "规格"];
                newRow["单位"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "单位"];
                addRowToolStripMenuItem.Enabled = false;
                deleteRowToolStripMenuItem.Enabled = true;

            }
            else if (computeC1SplitterPanel.Text == "租赁机械单价计算")
            {
                newRow["细目名称"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "编号"];
                newRow["租赁设备名称"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "名称"];
                newRow["规格型号"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "规格"];
                newRow["单位"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "单位"];
                addRowToolStripMenuItem.Enabled = false;
                deleteRowToolStripMenuItem.Enabled = true;
            }
            else if (computeC1SplitterPanel.Text == "自有周转材料单价计算")
            {
                newRow["周转材料名称"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "名称"];
                newRow["单位"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "单位"];
                addRowToolStripMenuItem.Enabled = false;
                deleteRowToolStripMenuItem.Enabled = true;
            }
            else if (computeC1SplitterPanel.Text == "租赁周转材料单价计算")
            {
                newRow["周转材料名称"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "名称"];
                newRow["单位"] = inventoryC1FlexGrid1[inventoryC1FlexGrid1.RowSel, "单位"];
                addRowToolStripMenuItem.Enabled = false;
                deleteRowToolStripMenuItem.Enabled = true;
            }

        }

        private void computC1FlexGrid_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
                if (computC1FlexGrid.Cols[e.Col].DataType == typeof(double))
                {
                    try
                    {
                        Convert.ToDouble(computC1FlexGrid.Editor.Text);
                    }
                    catch
                    {
                        e.Cancel = true;
                    }
                    
                }
        }

    }
}
