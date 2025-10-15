using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace BAL
{
  public   class MerchanAllPaymentBal
    {
        public static List<MerchantPaymentEntity> PaymentByMerchantId(string MerchantId, string conn)
        {
            List<MerchantPaymentEntity> MerchantPaymentEntitys = new List<MerchantPaymentEntity>();
            SqlParameter[] param = new SqlParameter[2];
            SqlHelper sqlHelper = new SqlHelper();

            param[0] = new SqlParameter("@Querytype", "PaymentByMerchantId");
            param[1] = new SqlParameter("@MerchantId", MerchantId);

            using (SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "usp_Payment", param))
            {
                while (dr.Read())
                {
                    MerchantPaymentEntity tblMerchant = new MerchantPaymentEntity
                    {
                        Id = Convert.ToInt64(dr["Id"]),
                        MerchantId = dr["MerchantId"].ToString(),
                        Domain = dr["Domain"].ToString(),
                        Amount = Convert.ToDecimal(dr["Amount"]),
                        OrderId = dr["OrderId"].ToString(),
                        Currency = dr["Currency"].ToString(),
                        PaymentMethod = dr["PaymentMethod"].ToString(),
                        Status = dr["Status"].ToString()
                    };
                    MerchantPaymentEntitys.Add(tblMerchant);
                }
            }
            return MerchantPaymentEntitys;
        }


        public static MerchantPaymentEntity PaymentById(long Id, string conn)
        {
            MerchantPaymentEntity tblMerchant = null;
            SqlParameter[] param = new SqlParameter[2];
            SqlHelper sqlHelper = new SqlHelper();
            param[0] = new SqlParameter("@Querytype", "PaymentById");
            param[1] = new SqlParameter("@Id", Id);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "usp_Payment", param))
            {
                if (dr.Read())
                {
                    tblMerchant = new MerchantPaymentEntity
                    {
                        Id = Convert.ToInt64(dr["Id"]),
                        MerchantId = dr["MerchantId"].ToString(),
                        Domain = dr["Domain"].ToString(),
                        Amount = Convert.ToDecimal(dr["Amount"]),
                        OrderId = dr["OrderId"].ToString(),
                        Currency = dr["Currency"].ToString(),
                        PaymentMethod = dr["PaymentMethod"].ToString(),
                        Status = dr["Status"].ToString()
                    };
                }
            }
            return tblMerchant;
        }
    }
}
