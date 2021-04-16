using LC_Common.ConfigManager;
using LC_Common.SqlFilter;
using LC_Common.SqlMapping;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class SqlBase
    {
        #region 通用增删改查(带缓存)
        /// <summary>
        /// 通用主键查询操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T Find<T>(int id) where T : BaseModel //增加约束
        {
            Type type = typeof(T);
            string sql = $"{SqlBuilder<T>.GetFindSql()}{id}";
            string connectionStr = SqlConnectionPool.GetConnectionString(SqlConnectionPool.SqlConnectionType.Read);
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    T t = (T)Activator.CreateInstance(type);
                    foreach (var prop in type.GetProperties())
                    {
                        var propName = prop.GetMappingName();//优化想法，查询时as一下，可以省下一轮
                        prop.SetValue(t, reader[propName] is DBNull ? null : reader[propName]);//可控类型  设置成null而不是数据库查询的值
                    }
                    return t;
                }
                else
                {
                    return default(T);
                }
            }
        }

        /// <summary>
        /// 通用实体新增操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int Insert<T>(T t) where T : BaseModel //增加约束
        {
            Type type = t.GetType();
            string sql = $"{SqlBuilder<T>.GetInsertSql()}";
            var paraArray = type.GetPropertiesWithoutKey().Select(p => new SqlParameter($"@{p.GetMappingName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();

            string connectionStr = SqlConnectionPool.GetConnectionString(SqlConnectionPool.SqlConnectionType.Write);
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddRange(paraArray);
                connection.Open();
                object oId = command.ExecuteScalar();
                return int.TryParse(oId?.ToString(), out int iId) ? iId : -1;
            }
        }

        /// <summary>
        /// 通用实体更新操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool Update<T>(T t) where T : BaseModel
        {
            Type type = t.GetType();
            string sql = $"{SqlBuilder<T>.GetUpdateSql()}";
            var paraArray = type.GetProperties().Select(p => new SqlParameter($"@{p.GetMappingName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();
            string connectionStr = SqlConnectionPool.GetConnectionString(SqlConnectionPool.SqlConnectionType.Write);
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddRange(paraArray);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// 通用主键删除操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            string sql = $"{SqlBuilder<T>.GetDeleteSql()}";
            string connectionStr = SqlConnectionPool.GetConnectionString(SqlConnectionPool.SqlConnectionType.Write);
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                SqlParameter parameter = new SqlParameter("@id", id);
                command.Parameters.Add(parameter);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        #endregion
    }
}
