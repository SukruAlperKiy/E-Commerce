using Microsoft.Data.SqlClient;
using System.Data;

namespace PresentationLayer.Models
{
    public class dal
    {
        public string HataMesaji = "";
        private readonly string _connectionString;
        public dal(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("conn");
        }

        public SqlConnection benimSqlBaglantim
        {
            get 
            {
                return new SqlConnection(_connectionString);
            }
        
        }

        public DataSet CommandExecuteReader(String sql, SqlConnection baglanti)
        {
            DataSet veriKumesi = new DataSet();
            try
            {
                SqlCommand benimEmrim = new SqlCommand(sql, baglanti);
                //eger veritabani 30sn icinde birsey dondurmezse sorguyu iptal et
                benimEmrim.CommandTimeout = 30;

                SqlDataAdapter veriBagdastiricisi = new SqlDataAdapter(benimEmrim);
                veriBagdastiricisi.Fill(veriKumesi);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                HataMesaji = sql + " -->(dal.cs HATA) " + exp.Message;
            }
            finally
            {
            }

            return veriKumesi;
        }
    }
}
