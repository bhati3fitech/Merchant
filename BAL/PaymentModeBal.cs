using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Bal
{
    public class PaymentModeBal
    {
        public static void Insert(PaymentModeEntity user, string conn)
        {
            SqlParameter[] param = new SqlParameter[4];
            SqlHelper sqlHelper = new SqlHelper();
            param[0] = new SqlParameter("@Id", user.Id);
            param[1] = new SqlParameter("@MerchantId", user.MerchantId);
            param[2] = new SqlParameter("@PaymentId", user.PaymentId);
            param[3] = new SqlParameter("@QueryType", "Insert");
            SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "usp_PaymentMode", param);
        }
        public static List<PaymentModeEntity> GetAllPayment(string conn)
        {
            List<PaymentModeEntity> merchants = new List<PaymentModeEntity>();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@QueryType", "GetAllPayment");

            using (SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "usp_PaymentMode", param))
            {
                while (dr.Read())
                {
                    PaymentModeEntity merchant = new PaymentModeEntity
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        MerchantId = Convert.ToInt32(dr["MerchantId"].ToString()),
                        PaymentId = Convert.ToInt32(dr["PaymentId"].ToString()),
                    };
                    merchants.Add(merchant);
                }
            }
            return merchants;
        }
        public static void UpdatePayment(PaymentModeEntity merchant, string conn)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MerchantId", merchant.MerchantId);
            param[1] = new SqlParameter("@PaymentId", merchant.PaymentId);
            param[2] = new SqlParameter("@QueryType", "UpdatePayment");
            SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "usp_PaymentMode", param);


        }

        public static PaymentModeEntity GetByIdPayment(int Id, string conn)
        {
            PaymentModeEntity merchant = null;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@QueryType", "GetByIdPayment");

            using (SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "usp_PaymentMode", param))
            {
                if (dr.Read())
                {
                    merchant = new PaymentModeEntity
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        PaymentId = Convert.ToInt32(dr["PaymentId"]),
                        MerchantId = Convert.ToInt32(dr["MerchantId"])
                    };
                }
            }
            return merchant;
        }


        public static List<PaymentModeEntity> GetByMerchantId(int merchantId, string conn)
        {
            List<PaymentModeEntity> paymentModeEntities = new List<PaymentModeEntity>();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MerchantId", merchantId);
            param[1] = new SqlParameter("@QueryType", "GetByMerchantId");

            using (SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "usp_PaymentMode", param))
            {
                while (dr.Read())
                {
                    PaymentModeEntity paymentModeEntity = new PaymentModeEntity();
                    paymentModeEntity.PaymentId = Convert.ToInt32(dr["PaymentId"]);
                    paymentModeEntity.MerchantId = Convert.ToInt32(dr["MerchantId"]);

                    if (dr.GetSchemaTable().Rows.Cast<DataRow>().Any(r => r["Title"].ToString() == "Title"))
                    {
                        paymentModeEntity.Title = dr["Title"].ToString();
                    }

                    paymentModeEntities.Add(paymentModeEntity);
                }
            }
            return paymentModeEntities;
        }


        public static void DeletePaymentsByMerchantId(int merchantId, string conn)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MerchantId", merchantId);
            param[1] = new SqlParameter("@QueryType", "DeletePaymentsByMerchantId");
            SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "usp_PaymentMode", param);
        }
    }
}
