using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace BAL
{
    public class MerchantBAL
    {
        public static MerchantEntity MerchantRegister(MerchantEntity merchantentity, string conn)
        {
            MerchantEntity merchant = new MerchantEntity();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@MerchantId", merchantentity.MerchantId);
            param[1] = new SqlParameter("@Name", merchantentity.Name);
            param[2] = new SqlParameter("@Email", merchantentity.Email);
            param[3] = new SqlParameter("@PhoneNumber", merchantentity.PhoneNo);
            param[4] = new SqlParameter("@Domain", merchantentity.Domain);
            param[5] = new SqlParameter("@Commission", merchantentity.Commission);
            param[6] = new SqlParameter("@NatureBusiness", merchantentity.NatureBusiness);
            param[7] = new SqlParameter("@Tax", merchantentity.Tax);
            param[8] = new SqlParameter("@PrivateKey", merchantentity.PrivateKey);
            param[9] = new SqlParameter("@Password", merchantentity.Password);
            param[10] = new SqlParameter("@QueryType", "Insert");
            using (SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "usp_Merchant", param))
            {
                while (dr.Read())
                {
                    merchant.Id = Convert.ToInt32(dr["Id"]);
                    merchant.Email = dr["Email"].ToString();
                    merchant.Name = dr["Name"].ToString();
                }
            }
            return merchant;

        }
        public static MerchantEntity GetByMerchantId(int Id, string conn)
        {
            MerchantEntity merchant = null;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@QueryType", "GetByMerchantId");

            using (SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "usp_Merchant", param))
            {
                while (dr.Read())
                {
                    merchant = new MerchantEntity
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        MerchantId = dr["MerchantId"].ToString(),
                        Name = dr["Name"].ToString(),
                        Email = dr["Email"].ToString(),
                        PhoneNo = dr["PhoneNumber"].ToString(),
                        Domain = dr["Domain"].ToString(),
                        Commission = Convert.ToDecimal(dr["Commission"]),
                        NatureBusiness = dr["NatureBusiness"].ToString(),
                        Tax = Convert.ToDecimal(dr["Tax"]),
                        PrivateKey = dr["PrivateKey"].ToString(),
                        Password = dr["Password"].ToString()
                    };
                }
            }
            return merchant;
        }
        public static MerchantEntity UpdateMerchant(MerchantEntity merchant, string conn)
        {
            MerchantEntity updatedMerchant = null;

            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@Id", merchant.Id);
            param[1] = new SqlParameter("@MerchantId", merchant.MerchantId);
            param[2] = new SqlParameter("@Name", merchant.Name);
            param[3] = new SqlParameter("@Email", merchant.Email);
            param[4] = new SqlParameter("@PhoneNumber", merchant.PhoneNo);
            param[5] = new SqlParameter("@Domain", merchant.Domain);
            param[6] = new SqlParameter("@Commission", merchant.Commission);
            param[7] = new SqlParameter("@NatureBusiness", merchant.NatureBusiness);
            param[8] = new SqlParameter("@Tax", merchant.Tax);
            param[9] = new SqlParameter("@Password", merchant.Password);
            param[10] = new SqlParameter("@QueryType", "UPDATE");

            try
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "usp_Merchant", param))
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            updatedMerchant = new MerchantEntity
                            {

                            };
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("SQL Error: " + sqlEx.Message);
                throw;
            }

            return updatedMerchant;
        }
        public static MerchantEntity MerchantLogin(string Email, string Password, string conn)
        {
            MerchantEntity merchant = new MerchantEntity();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Email", Email);
            param[1] = new SqlParameter("@Password", Password);
            param[2] = new SqlParameter("@QueryType", "Login");
            using (SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "usp_Merchant", param))
            {
                while (dr.Read())
                {
                    merchant.Id = Convert.ToInt32(dr["Id"]);
                    merchant.Name = dr["Name"].ToString();
                    merchant.MerchantId = dr["MerchantId"].ToString();
                }
            }
            return merchant;
        }


    }
}
