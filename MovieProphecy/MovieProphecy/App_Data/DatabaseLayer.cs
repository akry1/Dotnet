using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.ComponentModel;
using System.Collections;
using System.Data.SqlClient;

namespace MovieProphecy
{
    public class DatabaseLayer
    {
        private SqlConnection con;
        private SqlDataAdapter da;
        private DataSet ds;

        public DatabaseLayer()
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["MPDB"].ToString());
            }
            catch (Exception e)
            {

            }
        }

        public DataSet LoadMovieList(params String[] str)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                string query;
                string columnName = ConfigurationManager.AppSettings["ColumnName"];
                string tableName = ConfigurationManager.AppSettings["TableName"];
                if (str.Count() == 0)
                    da = new SqlDataAdapter("SELECT DISTINCT " + columnName + " FROM " + tableName, con);
                else if (str[0] == "1")
                {
                    query = "SELECT DISTINCT " + columnName + " FROM " + tableName + " WHERE " + columnName + " LIKE '[" + str[1] + str[1].ToLower() + "]%'";
                    da = new SqlDataAdapter(query, con);
                }
                else if (str[0] == "2")
                {
                    query = "SELECT polarity, count(polarity) as count FROM " + tableName + " WHERE " + columnName + "='" + str[1] + "' GROUP BY polarity";

                    //this is just for testing, replace it with above
                    //query = "SELECT polarity, count(polarity) as count FROM "+ tableName  +" WHERE " + columnName + " LIKE '[" + str[1].Substring(0,1) + str[1].Substring(0,1).ToLower() +"]%' GROUP BY polarity";
                    da = new SqlDataAdapter(query, con);
                }

                else if (str[0] == "3")
                    da = new SqlDataAdapter("SELECT TOP(100) tweet_text,polarity FROM " + tableName + " WHERE " + columnName + "='" + str[1] + "' ORDER BY polarity", con);
                else if (str[0] == "4")
                    da = new SqlDataAdapter("SELECT TOP(100) tweet_text,polarity FROM " + tableName + " WHERE " + columnName + "='" + str[1] + "' ORDER BY " + str[2].Trim() + str[3], con);
                ds = new DataSet();
                da.Fill(ds);

            }
            catch (Exception e)
            {
                //Handle exeption
                return null;
            }
            return ds;
        }


        public SqlDataReader DataForHistogram(string movieName,string pos)
        {

            //string query = "select DATEPART(mm, tweet_time)as MyMonth, DATEPART(yy, tweet_time)as MyYear,polarity, count(polarity) as count" +
            //    " from " + ConfigurationManager.AppSettings["TableName"] + " where polarity ='" + pos + "' group by DATEPART(mm, tweet_time), DATEPART(yy, tweet_time), polarity order by MyYear";
            
            try
            {
                SqlCommand cmd;
                if (pos == "pos")
                {
                    cmd = new SqlCommand("DataForHistogram", con);
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("MovieName", SqlDbType.VarChar);
                    cmd.Parameters["MovieName"].Value = movieName;

                    int rowsEffected = cmd.ExecuteNonQuery();
                }

                String query = "SELECT * FROM mytemp WHERE polarity='"+pos+"'";
                cmd = new SqlCommand(query,con);// WHERE polarity='" + pos + "'", con);
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                return dr;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int CalculatePTNTRatio(DataSet ds, out string review,out double posPercent)
        {
            int neg = 0, pos = 0, neu = 0, irr = 0; 
            try
            {
                DataTable dt = ds.Tables[0];                               
                foreach (DataRow dr in dt.Rows)
                {
                    string typeOfReview = dr[0].ToString().Trim();
                    switch (typeOfReview)
                    {
                        case "pos":
                            Int32.TryParse(dr[1].ToString(), out pos);
                            break;
                        case "neg":
                            Int32.TryParse(dr[1].ToString(), out neg);
                            break;
                        case "neu":
                            Int32.TryParse(dr[1].ToString(), out neu);
                            break;
                        default:
                            Int32.TryParse(dr[1].ToString(), out irr);
                            break;
                    }
                }

                double ptnt;
                if (neg != 0)
                    ptnt = (double)pos / neg;
                else
                    ptnt = 0;

                if (ptnt >= 3)
                    review = "Hit";
                else if (ptnt < 1.5 && ptnt > 0)
                    review = "Flop";
                else if (ptnt == 0)
                    review = "NA";
                else
                    review = "Average";

                if (pos + neg != 0)
                    posPercent = 100 * pos / (double)(pos + neg);
                else posPercent = 0;
                return pos + neg + neu + irr;
            }
            catch (Exception e)
            {
                review = "";
                posPercent = 0;
                return 0;
            }
        }
    }



}