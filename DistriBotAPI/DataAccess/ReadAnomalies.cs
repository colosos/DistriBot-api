using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace DistriBotAPI.DataAccess
{
    public class ReadAnomalies
    {
        public static List<FrontEndAnomaly> main()
        {
            string query = "SELECT Semana,ScoredLabelMean,lo95,hi95,ImporteReal,Anomalia,Diferencia FROM AnomaliasOutput";

            List<FrontEndAnomaly> ret = new List<FrontEndAnomaly>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContextFede"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    bool isPositive = dr[5].ToString().Trim() == "1";
                    string fechaCruda = dr[0].ToString();
                    DateTime date = DateTime.ParseExact(fechaCruda, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    double diff =  Double.Parse(dr[6].ToString());
                    ret.Add(new FrontEndAnomaly(isPositive, date, date, diff));
                }
            }
            return ret;
         }
    }
}