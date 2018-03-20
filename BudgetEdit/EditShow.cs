using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using MySql.Data.MySqlClient;
using System.Data;

namespace BudgetEdit
{
    //编制说明类
    public class EditShowItem
    {
        private string edit;

        public EditShowItem(string sEdit)
        {
            edit = sEdit;
        }

        public string Edit
        {
            get
            {
                return edit;
            }
        }
    }

    public class EditShow
    {
        private List<EditShowItem> editfs;

        public EditShow(string proName)
        {
            editfs = new List<EditShowItem>();
            string EditInfo;
            string sql = "select 编制说明 from project_message where 项目编号='" + proName + "'";
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = SqlHelper.getSqlAdapter(sql);
            adapter.Fill(dt);
            if (dt.Rows.Count <= 0)
            {
                return;
            }
            if (dt.Rows[0][0] != null && dt.Rows[0][0].ToString().Length > 0)
            {
                EditInfo = Convert.ToString(dt.Rows[0][0]);
            }
            else
            {
                EditInfo = "";
            }
            editfs.Add(new EditShowItem(EditInfo));
        }

        public List<EditShowItem> GetItems()
        {
            return editfs;
        }
    }
}