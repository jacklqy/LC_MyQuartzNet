using LC_Common.ConfigManager;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace Dal
{
    public class SqlHelper : SqlBase
    {
        #region 批量新增
        public static void BatchInsertTest()
        {
            List<tb_log> logs = new List<tb_log>();
            for (int i = 0; i < 100000; i++)
            {
                tb_log lgAdd = new tb_log()
                {
                    info = $"批量新增测试{i}",
                    methodname = "TestJob1111",
                    mqpath = "This is TestJob1111",
                    mqpathid = 1,
                    createtime = DateTime.Now,
                    updatetime = DateTime.Now
                };
                logs.Add(lgAdd);
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            BatchInsert(logs);
            stopwatch.Stop();
            Console.WriteLine("耗时：" + stopwatch.ElapsedMilliseconds);
        }
        public static void BatchInsert<T>(List<T> dataList, int batchSize = 0)
        {
            DataTable dataTable = ConvertToDataTable(dataList);
            BatchInsert(dataTable, typeof(T).Name, batchSize);
        }

        public static void BatchInsert(DataTable dataTable, string destinationTableName, int batchSize = 0)
        {
            string connectionStr = SqlConnectionPool.GetConnectionString(SqlConnectionPool.SqlConnectionType.Write);
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    //SqlBulkCopy 要求 DataTable 的列必须和表列顺序一致，并且不能多也不能少，所以实体类的字段顺序要和数据库表字段顺序和类型一致...
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        bulkCopy.BatchSize = batchSize;
                        bulkCopy.DestinationTableName = destinationTableName;
                        try
                        {
                            bulkCopy.WriteToServer(dataTable);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            transaction.Rollback();
                        }
                    }
                }
            }
        }
        #endregion

        #region sql获取Table数据
        /// <summary>
        /// 执行sql语句或存储过程返回一个表
        /// sql=select * from student where sex=@a 
        /// sql2=select * from grade where score >@x and stuid>@n
        /// </summary>
        /// <param name="sql">sql语句或过程名</param>
        /// <param name="isProcedure">是否是过程</param>
        /// <param name="pms">执行所要的参数</param>
        /// <returns></returns>
        public static DataTable GetTable(string sql, bool isProcedure, params SqlParameter[] pms)
        {
            string connectionStr = SqlConnectionPool.GetConnectionString(SqlConnectionPool.SqlConnectionType.Read);
            using (SqlConnection conn = new SqlConnection(connectionStr)) //using创建的对象，自动释放资源
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (isProcedure == true)
                    cmd.CommandType = CommandType.StoredProcedure;
                if (pms != null)
                    cmd.Parameters.AddRange(pms);
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds, "aa");
                DataTable dt = ds.Tables["aa"];
                return dt;
            }
        }
        /// <summary>
        /// 执行sql语句返回表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="pms">sql语句所需要的参数</param>
        /// <returns></returns>
        public static DataTable GetTable(string sql, params SqlParameter[] pms)
        {
            return GetTable(sql, false, pms);
        }
        #endregion

        #region sql增删改操作
        /// <summary>
        /// 执行sql命名返回一个影响行数,针对于增删改操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="isProcedure">是否是过程</param>
        /// <param name="pms">sql语句所需要的参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, bool isProcedure, params SqlParameter[] pms)
        {
            string connectionStr = SqlConnectionPool.GetConnectionString(SqlConnectionPool.SqlConnectionType.Write);
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (isProcedure == true)
                    cmd.CommandType = CommandType.StoredProcedure;
                if (pms != null)
                    cmd.Parameters.AddRange(pms);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 执行sql命名返回一个影响行数,针对于增删改操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="pms">sql语句所需要的参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] pms)
        {
            return ExecuteNonQuery(sql, false, pms);
        }
        #endregion

        #region sql利用DataReader一行一行的读数据
        /// <summary>
        /// 执行命令得到一个SqlDataReader对象,你可以用这个对象的Reader对象读取数据----使用后需要Close()释放
        /// </summary>
        /// <param name="sql">sql语句或过程名</param>
        /// <param name="isProcedure">是否是过程</param>
        /// <param name="pms">执行所要的参数</param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql, bool isProcedure, params SqlParameter[] pms)
        {
            string connectionStr = SqlConnectionPool.GetConnectionString(SqlConnectionPool.SqlConnectionType.Read);
            SqlConnection conn = new SqlConnection(connectionStr);
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (isProcedure == true)
                cmd.CommandType = CommandType.StoredProcedure;
            if (pms != null)
                cmd.Parameters.AddRange(pms);
            conn.Open();
            //***CommandBehavior.CloseConnection 在sdr执行命令完毕后的行为是自动关闭连接,也就意味着手动调用sdr.Close()方法时，conn也close()***
            SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //bool b = sdr.Read();
            //conn.Close();
            return sdr;
        }


        /// <summary>
        /// 执行命令得到一个SqlDataReader对象,你可以用这个对象的Reader对象读取数据----使用后需要Close()释放
        /// </summary>
        /// <param name="sql">sql语句或过程名</param>
        /// <param name="pms">执行所要的参数</param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql, params SqlParameter[] pms)
        {
            return GetReader(sql, false, pms);
        }

        #endregion

        #region private
        /// <summary>
        /// 由于 SqlBulkCopy 要求 DataTable 的列必须和表列顺序一致，并且不能多也不能少，所以实体类的字段顺序要和数据库表字段顺序和类型一致...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        } 
        #endregion
    }
}
