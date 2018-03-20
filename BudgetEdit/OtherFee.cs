using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Tools;
using MySql.Data.MySqlClient;

namespace BudgetEdit
{
    //其他工程费类
    public class OtherFeeItem
    {
        private string projectName;
        private string number;
        private string feeName;
        private string unit;
        private string count;
        private string unit_price;
        private string price;
        private string remark;
        private string total;           //合计

        public OtherFeeItem(string sProject, string sNumber, string sFeeName, string sUnit, string sCount, string sUnit_price, string sPrice, string sRemark, string sTotal)
        {
            projectName = sProject;
            number = sNumber;
            feeName = sFeeName;
            unit = sUnit;
            count = sCount;
            unit_price = sUnit_price;
            price = sPrice;
            remark = sRemark;
            total = sTotal;
        }

        public string ProjectName
        {
            get
            {
                return projectName;
            }
        }

        public string Number
        {
            get
            {
                return number;
            }
        }

        public string FeeName
        {
            get
            {
                return feeName;
            }
        }

        public string Unit
        {
            get
            {
                return unit;
            }
        }

        public string Count
        {
            get
            {
                return count;
            }
        }

        public string Unit_price
        {
            get
            {
                return unit_price;
            }
        }

        public string Price
        {
            get
            {
                return price;
            }
        }

        public string Remark
        {
            get
            {
                return remark;
            }
        }

        public string Total
        {
            get
            {
                return total;
            }
        }
    }

    public class OtherFee
    {
        private List<OtherFeeItem> otherfs;

        /*********************************更新dataTable递增标识列为具有父子关系的标识列******************************************************/
        public void Update(List<string> ListNumber, DataTable dt)
        {
            List<int> Level = new List<int>();
            int count = 0;
            double j = 1;
            int index = 0, pre_count = 0, row = 0, pre_row = 1;

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
            while (count <= dt.Rows.Count - 1)
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
                                if (pre_row == 1)
                                {
                                    ListNumber[row] = ListNumber[row - (step)] + "." + (step++).ToString();

                                }
                                else
                                {
                                    string temp = ListNumber[pre_row];
                                    for (int len = temp.Length - 1; len > 0; len--)
                                    {
                                        if (temp[len] == '.')
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

        public OtherFee(string proName, string bidNumber)
        {
            string sNumber, sName, sUnit, sCount, sUnitPrice, sPrice, sRemark;
            otherfs = new List<OtherFeeItem>();
            List<string> list_number = new List<string>();
            string totalSum;
            string sql = "select 标识,父亲,费用名称,单位,数量,单价,金额,备注 from other_fee where 所属标段='" + bidNumber + "' order by 标识 asc";                       //正式确认时sql语句需添加where+bid_name分支以确定对应标段
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
            adapter.Fill(dt);
            if (dt.Rows.Count <= 0)
            {
                return;
            }
            if (dt.Rows[0][6] != null && dt.Rows[0][6].ToString().Length > 0)
            {
                totalSum = Convert.ToString(dt.Rows[0][6]);
            }
            else
            {
                totalSum = "0.00";
            }
            dt.Rows[0].Delete();
            dt.AcceptChanges();
            Update(list_number, dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sNumber = list_number[i];
                if (dt.Rows[i][2] != null && dt.Rows[i][2].ToString().Length > 0)
                {
                    sName = Convert.ToString(dt.Rows[i][2]);
                }
                else
                {
                    sName = "";
                }

                if (dt.Rows[i][3] != null && dt.Rows[i][3].ToString().Length > 0)
                {
                    sUnit = Convert.ToString(dt.Rows[i][3]);
                }
                else
                {
                    sUnit = "";
                }

                if (dt.Rows[i][4] != null && dt.Rows[i][4].ToString().Length > 0)
                {
                    sCount = Convert.ToString(dt.Rows[i][4]);
                }
                else
                {
                    sCount = "";
                }

                if (dt.Rows[i][5] != null && dt.Rows[i][5].ToString().Length > 0)
                {
                    sUnitPrice = Convert.ToString(dt.Rows[i][5]);
                }
                else
                {
                    sUnitPrice = "";
                }

                if (dt.Rows[i][6] != null && dt.Rows[i][6].ToString().Length > 0)
                {
                    sPrice = Convert.ToString(dt.Rows[i][6]);
                }
                else
                {
                    sPrice = "";
                }

                if (dt.Rows[i][7] != null && dt.Rows[i][7].ToString().Length > 0)
                {
                    sRemark = Convert.ToString(dt.Rows[i][7]);
                }
                else
                {
                    sRemark = "";
                }

                otherfs.Add(new OtherFeeItem(proName, sNumber, sName, sUnit, sUnitPrice, sCount, sPrice, sRemark, totalSum));
            }
        }

        /* public OtherFee(List<string> list_number, DataTable dt)
         {
             string sNumber, sName, sUnit, sCount, sUnitPrice, sPrice, sRemark;
             otherfs = new List<OtherFeeItem>();
             for (int i = 0; i < dt.Rows.Count; i++)
             {
                 sNumber = list_number[i];
                 if (dt.Rows[i][2] != null && dt.Rows[i][2].ToString().Length > 0)
                 {
                     sName = Convert.ToString(dt.Rows[i][2]);
                 }
                 else
                 {
                     sName = "";
                 }

                 if (dt.Rows[i][3] != null && dt.Rows[i][3].ToString().Length > 0)
                 {
                     sUnit = Convert.ToString(dt.Rows[i][3]);
                 }
                 else
                 {
                     sUnit = "";
                 }

                 if (dt.Rows[i][4] != null && dt.Rows[i][4].ToString().Length > 0)
                 {
                     sCount = Convert.ToString(dt.Rows[i][4]);
                 }
                 else
                 {
                     sCount = "";
                 }

                 if (dt.Rows[i][5] != null && dt.Rows[i][5].ToString().Length > 0)
                 {
                     sUnitPrice = Convert.ToString(dt.Rows[i][5]);
                 }
                 else
                 {
                     sUnitPrice = "";
                 }

                 if (dt.Rows[i][6] != null && dt.Rows[i][6].ToString().Length > 0)
                 {
                     sPrice = Convert.ToString(dt.Rows[i][6]);
                 }
                 else
                 {
                     sPrice = "";
                 }

                 if (dt.Rows[i][7] != null && dt.Rows[i][7].ToString().Length > 0)
                 {
                     sRemark = Convert.ToString(dt.Rows[i][7]);
                 }
                 else
                 {
                     sRemark = "";
                 }

                 otherfs.Add(new OtherFeeItem(sNumber, sName, sUnit, sUnitPrice, sCount, sPrice, sRemark));
             }
         }*/

        public List<OtherFeeItem> GetItems()
        {
            return otherfs;
        }
    }
}
