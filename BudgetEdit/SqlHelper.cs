using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Tools
{
    class SqlHelper
    {


        public static DataTable ExcelToTable(string fileName,string SheetName="Sheet1")//Excel文件导入到DataTable
        {
            //默认sheet
            //DataTable dt = null;
            //OpenFileDialog openFile = new OpenFileDialog();
            //openFile.Filter = "Excel(*.xlsx)|*.xlsx|Excel(*.xls)|*.xls";
            //openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //openFile.Multiselect = false;
            //if (openFile.ShowDialog() == DialogResult.OK)
            //{
          
                //链接
             
                OleDbConnection conn = new OleDbConnection(getExcelConn(fileName));
                DataTable dt = new DataTable();
                try
                {
                    conn.Open();
                    string strExcel = "";
                    OleDbDataAdapter myCommand = null;

                    strExcel = "select * from [" + SheetName + "$]";
                    myCommand = new OleDbDataAdapter(strExcel, conn);
                   
                    //查询数据
                    myCommand.Fill(dt);
                }
                finally
                {
                    conn.Close();
                }
                //
                
            //}

            return dt;
        }

        static  private string getExcelConn(string fileName)
        {
            string fileType = System.IO.Path.GetExtension(fileName);
            string excelConnection = string.Format("Provider=Microsoft.ACE.OLEDB.{0}.0;" +
                            "Extended Properties=\"Excel {1}.0;HDR={2};IMEX=0;\";" +
                            "data source={3};",
                            (fileType == ".xls" ? 4 : 12), (fileType == ".xlsx" ? 8 : 12), ("yes"), fileName);
            //string excelConnection = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + fileName + ";" + "Extended Properties=Excel 8.0;";  
            
            return excelConnection;
        }

        static private string getMySqlConn(string dbName)
        {

            MySqlConnection con = new MySqlConnection();//实例化链接
            MySqlConnectionStringBuilder sqlBuilder = new MySqlConnectionStringBuilder();
            sqlBuilder.Server = "127.0.0.1";
            sqlBuilder.UserID = "root";
            sqlBuilder.Password = "591314";
            sqlBuilder.Database = dbName;
            sqlBuilder.CharacterSet = "utf8";
            return sqlBuilder.ConnectionString;
        }
     
        public static void TableToExcel(DataTable dt, string fileName,string sheetName="Sheet1")//DataTable导出到Excel
        {
                OleDbConnection myConn = new OleDbConnection(getExcelConn(fileName));

                string sql = "select * from [" + sheetName + "$]";
                try
                {
                    myConn.Open();
                    OleDbDataAdapter myCommand = new OleDbDataAdapter(sql, myConn);

                    System.Data.OleDb.OleDbCommandBuilder builder = new OleDbCommandBuilder(myCommand);

                    //QuotePrefix和QuoteSuffix主要是对builder生成InsertComment命令时使用。   
                    //获取insert语句中保留字符（起始位置）  
                    builder.QuotePrefix = "[";

                    //获取insert语句中保留字符（结束位置）   
                    builder.QuoteSuffix = "]";

                    DataTable newdt = new DataTable();
                    //获得表结构
                    newdt = dt.Clone();
                    newdt.TableName = sheetName;
                    myCommand.Fill(newdt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow temp = newdt.NewRow();
                        temp.ItemArray = dr.ItemArray;
                        newdt.Rows.Add(temp);
                    }            
            
                    //插入数据
                    myCommand.Update(newdt);
                    
                }
                finally
                {
                    myConn.Close();
                }
            //}
        }
        static public MySqlDataReader getSqlReader(string sql)
        {
            MySqlConnection sqlConn = new MySqlConnection(getMySqlConn("BudgetEdit"));
            sqlConn.Open();
            MySqlCommand MySqlCommand = new MySqlCommand(sql, sqlConn);
            MySqlDataReader sdr = MySqlCommand.ExecuteReader();
            return sdr;
        }
        static public MySqlDataAdapter getSqlAdapter(string sql)
        {
            MySqlConnection sqlConn = new MySqlConnection(getMySqlConn("BudgetEdit"));
            //string sql = "select * from project_message";
            MySqlCommand sqlCommand = new MySqlCommand(sql, sqlConn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            sqlConn.Close();
            return adapter;
        }
        static public void exeNonQuery(string sql)
        {
            MySqlConnection sqlConn = new MySqlConnection(SqlHelper.getMySqlConn("BudgetEdit"));
            sqlConn.Open();
            //string sql = string.Format("delete from project_message where 项目编号='{0}'", projectNumber);
            MySqlCommand sqlCommand = new MySqlCommand(sql, sqlConn);
            sqlCommand.ExecuteNonQuery();
            sqlConn.Close();
        }



    }
}
