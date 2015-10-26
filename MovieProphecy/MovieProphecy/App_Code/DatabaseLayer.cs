using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        }

        public DataSet LoadMovieList()
        {
            try
            {
                da = new SqlDataAdapter("SELECT Email FROM AspNetUsers", con);
                ds = new DataSet();
                da.Fill(ds);

            }
            catch (Exception e)
            {
                //Handle exeption
            }
            return ds;
        }
    }

}