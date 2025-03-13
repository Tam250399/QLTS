using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Reflection;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace GS.WebTool.Infrastructure
{
    public class dbContextDKTS
    {
        private static string DATA_SOURCE = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=115.146.126.37)(PORT=1522)))(CONNECT_DATA=(SID=gs19c)));User ID=QLDKTS;Password=QLDKTS";
        //private static string DATA_SOURCE = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=115.146.127.184)(PORT=1521)))(CONNECT_DATA=(SID=gs19c)));User ID=QLDKTS_09_09_2021;Password=QLDKTS_09_09_2021";
        //private static string DATA_SOURCE = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=115.146.127.184)(PORT=1521)))(CONNECT_DATA=(SID=gs19c)));User ID=QLDKTS_26_06_2021;Password=QLDKTS_26_06_2021";
        //private static string DATA_SOURCE = @"DATA SOURCE=27.72.63.249:1523/gsdb;USER ID=QLDKTS_15_03_2021;Password=QLDKTS_15_03_2021;";
        //private static string DATA_SOURCE = @"DATA SOURCE=27.72.63.249:1523/gsdb;USER ID=QLDKTS_15_03_2021;Password=QLDKTS_15_03_2021;";
        //private static string DATA_SOURCE = @"DATA SOURCE=192.168.1.33:1523/gsdb;USER ID=QLDKTS_15_03_2021;Password=QLDKTS_15_03_2021;";
        //private static string DATA_SOURCE = @"DATA SOURCE=192.168.1.33:1523/gsdb;USER ID=QLDKTS_17_02_2020;Password=QLDKTS_17_02_2020;";
        //private static string DATA_SOURCE = @"DATA SOURCE=192.168.1.33:1523/gsdb;USER ID=QLDKTS_27_03_2020;Password=QLDKTS_27_03_2020;";
        //private static string DATA_SOURCE = @"DATA SOURCE=192.168.1.33:1523/gsdb;USER ID=QLDKTS_18_08_2020;Password=QLDKTS_18_08_2020;";
        private static OracleConnection CreateConnection()
        {
            return new OracleConnection(DATA_SOURCE);
        }

        // Lấy ra từ database một giá trị
        public static string LayMotGiaTri(string Oracle)
        {
            // tạo OracleConnection đến CSDL
            OracleConnection con = CreateConnection();
            con.Open();
            OracleCommand query = new OracleCommand(Oracle, con);
            object rs = query.ExecuteScalar();
            // giải phóng tài nguyên
            con.Close();
            query.Dispose();
            // trả về kết quả
            if (rs == null)
                return "";
            else
                return rs.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Oracle"></param>
        /// <returns></returns>
        public static List<T> getTable<T>(string Oracle) where T : new()
        {
            // tạo kết nối
            OracleConnection con = CreateConnection();
            con.Open();
            DataTable tb = new DataTable();
            OracleDataAdapter adapter = new OracleDataAdapter(Oracle, con);
            adapter.Fill(tb);
            // giải phóng tài nguyên
            con.Close();
            adapter.Dispose();
            return CreateListFromTable<T>(tb);
        }

        // phương thức thêm, sửa, xóa
        public static void ExecuteQuery(string Oracle)
        {
            OracleConnection conn = CreateConnection();
            conn.Open();
            OracleCommand query = new OracleCommand(Oracle, conn);
            query.ExecuteNonQuery();
            // giải phóng tài nguyên
            conn.Close();
            query.Dispose();
        }

        public static void ExecuteProcedureINSERT_TO_TEMP_DONG_BO(decimal? pLOAI_HINH_TAI_SAN = 1, decimal? pIS_TS_TOAN_DAN = 0, int pNAM = 2020, string pNHOM_DON_VI = "03")
        {
            OracleConnection conn = CreateConnection();
            conn.Open();
            OracleParameter[] parameters = new OracleParameter[] {
                new OracleParameter("pLOAI_HINH_TAI_SAN",pLOAI_HINH_TAI_SAN),
                new OracleParameter("pIS_TS_TOAN_DAN",pIS_TS_TOAN_DAN),
                new OracleParameter("pNAM",pNAM),
                new OracleParameter("pNHOM_DON_VI",pNHOM_DON_VI)
            };
            OracleCommand query = new OracleCommand("INSERT_TO_TEMP_DONG_BO", conn);
            query.CommandType = CommandType.StoredProcedure;
            query.Parameters.AddRange(parameters);

            query.ExecuteNonQuery();
            conn.Close();
            query.Dispose();

        }

        public static List<T> CreateListFromTable<T>(DataTable tbl) where T : new()
        {
            // define return list
            List<T> lst = new List<T>();

            // go through each row
            foreach (DataRow r in tbl.Rows)
            {
                // add to the list
                lst.Add(CreateItemFromRow<T>(r));
            }

            // return the list
            return lst;
        }

        // function that creates an object from the given data row
        public static T CreateItemFromRow<T>(DataRow row) where T : new()
        {
            // create a new object
            T item = new T();

            // set the item
            SetItemFromRow(item, row);

            // return 
            return item;
        }

        public static void SetItemFromRow<T>(T item, DataRow row) where T : new()
        {
            int i = 0;
            // go through each column
            foreach (DataColumn c in row.Table.Columns)
            {
                // find the property for the column
                PropertyInfo p = item.GetType().GetProperty(c.ColumnName);

                // if exists, set the value
                if (p != null && row[c] != DBNull.Value)
                {
                    p.SetValue(item, row[c], null);
                }
            }
        }
    }
}
