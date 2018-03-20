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
using Microsoft.Reporting.WinForms;

namespace BudgetEdit
{
    public partial class ReportForm : Form
    {
        public DataTable dataTable;
        public MySqlDataAdapter adapter;
        public string bNumber;
        public string proName;
        public List<string> ListSort = new List<string>();
        
        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            
            //OtherFeeReport("延安高速公路", "201501");
            //ZjFeeReport("延安高速公路", "201501");
           // LsFeeReport("延安高速公路", "201501");
            EditShowReport("001");
        }
        /***************************************************编制说明*******************************************************/
        public void EditShowReport(string projectName)
        {
            EditShow edit_show;
            edit_show = new EditShow(projectName);
            reportViewer1.LocalReport.ReportEmbeddedResource = "BudgetEdit.EditShowReport.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("EditShowDs", edit_show.GetItems()));
            this.reportViewer1.RefreshReport();
        }
        /***************************************************编制说明*******************************************************/

        /*************************************************临时设施费****************************************************************/
        public void LsFeeReport(string project, string bidNumber)
        {
            LsFee ls_fee;
            ls_fee = new LsFee(project, bidNumber);
            reportViewer1.LocalReport.ReportEmbeddedResource = "BudgetEdit.LsFeeReport.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("LsFeeDs", ls_fee.GetItems()));
            this.reportViewer1.RefreshReport();
        }
        /*************************************************临时设施费****************************************************************/

        /*************************************************设备仪器折旧费****************************************************************/
        public void ZjFeeReport(string project, string bidNumber)
        {
            ZjFee yqzj_fee;
            yqzj_fee = new ZjFee(project, bidNumber);
            reportViewer1.LocalReport.ReportEmbeddedResource = "BudgetEdit.ZjFeeReport.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("ZjFeeDs", yqzj_fee.GetItems()));
            this.reportViewer1.RefreshReport();
        }
        /*************************************************设备仪器折旧费****************************************************************/

        /*************************************************其他工程费****************************************************************/
        public void OtherFeeReport(string project, string bidNumber)
        {
            OtherFee other_fee;
            other_fee = new OtherFee(project, bidNumber);
            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportEmbeddedResource = "BudgetEdit.OtherFeeReport.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("OtherFeeDs", other_fee.GetItems()));
            this.reportViewer1.RefreshReport();
        }
        /*************************************************其他工程费****************************************************************/

        /*********************************更新dataTable递增标识列为具有父子关系的标识列******************************************************/
        public void Update(List<string> ListNumber, DataTable dt)
        {
           List<int> Level = new List<int>();
           int count = 0;
           double j = 1;
           int index = 0, pre_count = 0,row =0,pre_row = 1; 
           
           int step = 1;  

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListNumber.Add(Convert.ToString(dt.Rows[i].ItemArray[0]));
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i].ItemArray[1]) == 0)
                {
                    ListNumber[i] = (step++).ToString();            //一级节点
                    Level.Add(i);
                }
            }
            step = 1;
            count = Level.Count;
            while (count <= dt.Rows.Count-1)
            {
                for (int i = pre_count; i < count; i++)
                {
                    pre_row = 1;
                    step = 1;
                    for (row = 0; row < dt.Rows.Count; row++)
                    {
                        if (Convert.ToInt32(dt.Rows[row].ItemArray[1]) == (Convert.ToInt32(dt.Rows[Level[i]].ItemArray[0])))
                        {
                            if (index == 0)
                            {
                                if (pre_row == 1)
                                {
                                    ListNumber[row] = Convert.ToString(Convert.ToDouble(ListNumber[row - 1]) + j / (Math.Pow(10, index + 1)));          //二级节点
                                }
                                else
                                {
                                    ListNumber[row] = Convert.ToString(Convert.ToDouble(ListNumber[pre_row]) + j / (Math.Pow(10, index + 1)));          //二级节点
                                }
                                pre_row = row;

                            }
                            else                                                          //三级节点及以上
                            {                
                                if(pre_row ==1)
                                {
                                    ListNumber[row] = ListNumber[row - (step)] + "." + (step++).ToString();    
                                   
                                }
                                else
                                {
                                    string temp = ListNumber[pre_row];
                                    for (int len = temp.Length - 1; len > 0; len--)
                                    {
                                        if(temp[len] == '.')
                                        {
                                            temp = temp.Remove(len + 1);
                                            break;
                                        }
                                    }
                                    ListNumber[row] = temp + (step++).ToString();    
                                }
                                pre_row = row;
                            }

                            Level.Add(row);
                        }
                    }
                    
                }
                index++;
                pre_count = count;
                count = Level.Count;      
            }
            pre_count = 0;
        }
        /*********************************更新dataTable递增标识列为具有父子关系的标识列******************************************************/
    }
}
