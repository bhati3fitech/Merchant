using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bal
{
    public class PaymentModeMasterBal
    {
        public static PaymentModeMasterEntity Insert(PaymentModeMasterEntity user, string conn)
        {
            PaymentModeMasterEntity userDetail = new PaymentModeMasterEntity();

            SqlParameter[] param = new SqlParameter[4];
            SqlHelper sqlHelper = new SqlHelper();

            param[0] = new SqlParameter("@Id", user.Id);
            param[1] = new SqlParameter("@Description", user.Description);
            param[2] = new SqlParameter("@Title", user.Title);
            param[3] = new SqlParameter("@QueryType", "Insert");

            using (SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "usp_PaymentModeMaster", param))
            {
                if (dr.Read())
                {
                    userDetail.Id = Convert.ToInt32(dr["Id"]);
                }
            }

            return userDetail;
        }
        public static List<PaymentModeMasterEntity> GetAllPayment(string conn)
        {
            List<PaymentModeMasterEntity> merchants = new List<PaymentModeMasterEntity>();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@QueryType", "GetAllPayment");

            using (SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "usp_PaymentModeMaster", param))
            {
                while (dr.Read())
                {
                    PaymentModeMasterEntity merchant = new PaymentModeMasterEntity
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Description = dr["Description"].ToString(),
                        Title = dr["Title"].ToString(),
                    };
                    merchants.Add(merchant);
                }
            }

            return merchants;
        }
        public static void Delete(int Id, string conn)
        {
            SqlParameter[] param = new SqlParameter[2];
            SqlHelper sqlHelper = new SqlHelper();
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@QueryType", "DELETE");
            SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "usp_PaymentModeMaster", param);
        }

        public static PaymentModeMasterEntity GetByPaymentId(int Id, string conn)
        {
            PaymentModeMasterEntity merchant = null;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@QueryType", "GetByPaymentId");

            using (SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "usp_PaymentModeMaster", param))
            {
                if (dr.Read())
                {
                    merchant = new PaymentModeMasterEntity
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Description = dr["Description"].ToString(),
                        Title = dr["Title"].ToString(),
                    };
                }
            }
            return merchant;
        }

    }
}
