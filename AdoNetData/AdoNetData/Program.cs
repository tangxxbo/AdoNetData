using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetData
{
    class Program
    {
        private static string connSql = ConfigurationManager.ConnectionStrings["connSql"].ConnectionString;
        //存储过程从配置文件中获取
        private static string procedureSql = ConfigurationManager.ConnectionStrings["addTeacherByTran"].ConnectionString;
        static void Main(string[] args)
        {
            DataBaseStudy();

        }

        private static void DataBaseStudy() {
            /* 事务的开启调用
             * 程序如何来调用事务，事务放在存储过程中
                Begin tran
                Begin try
                
            

                Commit tran
                end try



                begin catch
                RollBack tran
                end catch
             */
            using (SqlConnection conn = new SqlConnection(connSql)) {
                SqlCommand scmd = new SqlCommand(procedureSql, conn);
                //声明scmd对象的Sql是一个存储过程
                scmd.CommandType = CommandType.StoredProcedure;
                scmd.Parameters.Clear();
                SqlParameter[] para1 = {
                      new SqlParameter("@class","大2班"),
                      new SqlParameter("@code","1000005"),
                      new SqlParameter("@name","杜康"),
                      new SqlParameter("@age",25),
                      new SqlParameter("@address","山东"),
                      new SqlParameter("@tsex",int.Parse("0".ToString())),
                      new SqlParameter("@interesting","SAP"),
                      new SqlParameter("@TeacherId","10000000"),
                      new SqlParameter("@reValue",SqlDbType.Int,10)
                };
                para1[8].Direction = ParameterDirection.ReturnValue;
                scmd.Parameters.AddRange(para1);
                conn.Open();
                scmd.ExecuteNonQuery();
                int reValu = int.Parse(para1[8].Value.ToString());
                if (reValu == 1)
                {
                    Console.WriteLine("添加成功");
                }
                else {
                    Console.WriteLine("添加失败");
                }
                Console.ReadKey();  
            }
            
        }
    }
}
