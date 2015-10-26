using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.DataVisualization.Charting.Utilities;
using System.Data.SqlClient;
using System.Web.UI .HtmlControls ;

namespace MovieProphecy
{
    public partial class DetailedView : System.Web.UI.Page
    {
        DatabaseLayer obj = new DatabaseLayer();
        DataSet ds;
        string movieName;

        protected void Page_Load(object sender, EventArgs e)
        {

            movieName = Request.QueryString[0];
            lblMovieName.Text = movieName;

            //displaydata
            if(!IsPostBack )
            {
                Panel1.Visible = true;
                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = false;
                grdDisplayData.Attributes.Add("CurrentSortField", "");
                grdDisplayData.Attributes.Add("CurrentSortOrder", "");                
                InitializeGrid();
                IniatializeHome();
            }
            InitializeChart();
        }

        public void InitializeChart()
        {

            try
            {


                ds = obj.LoadMovieList("2", movieName);
                Chart1.DataSource = ds.Tables[0];
                Chart1.Series["pieChart"].XValueMember = "polarity";
                Chart1.Series["pieChart"].YValueMembers = "count";


                Chart1.DataBind();



                Chart2.DataSource = ds.Tables[0];
                Chart2.Series["barChart"].XValueMember = "polarity";
                Chart2.Series["barChart"].YValueMembers = "count";

                //Chart3.ChartAreas["ChartArea1"].AxisX.
                Chart2.DataBind();

                colorChart(Chart1, "pieChart");
                colorChart(Chart2, "barChart");




               

                SqlDataReader dr = obj.DataForHistogram(movieName, "pos");

                Chart3.Series["Series1"].Points.DataBind(dr, "timeField", "polCount", "Label=");

                dr = obj.DataForHistogram(movieName, "neg");
                Chart3.Series["Series2"].Points.DataBind(dr, "timeField", "polCount", "Label=");

                dr = obj.DataForHistogram(movieName, "neu");
                Chart3.Series["Series3"].Points.DataBind(dr, "timeField", "polCount", "Label=");

                //dr = obj.DataForHistogram(movieName, "irr");
                //Chart3.Series["Series4"].Points.DataBind(dr, "timeField", "polCount", "Label=");
                
                colorChart(Chart3, "Series1");
                colorChart(Chart3, "Series2");
                colorChart(Chart3, "Series3");

                Chart3.Series["Series1"]["PointWidth"] = "0.3";
                Chart3.Series["Series2"]["PointWidth"] = "0.3";
                Chart3.Series["Series3"]["PointWidth"] = "0.3";
                Chart3.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            }
            catch (Exception e)
            {
 
            }
        }

        public void InitializeGrid()  
        {
            
            ds = obj.LoadMovieList("3", movieName);
            if (ds != null)
            {
                grdDisplayData.DataSource = ds;
                grdDisplayData.DataBind();
            }
        }

        public void colorChart(Chart chart1, string chartType)
        {
            try
            {
                foreach (DataPoint point in chart1.Series[chartType].Points)
                {
                    switch (point.AxisLabel.Trim())
                    {
                        case "neg":
                            point.Color = Color.OrangeRed;
                            point.LegendText = "Negative";
                            break;
                        case "pos":
                            point.Color = Color.LimeGreen;
                            point.LegendText = "Positive";
                            break;
                        //case "irr":
                        //    point.Color = Color.LightSkyBlue;
                        //    point.LegendText = "Irrelevant";
                        //    break;
                        case "neu":
                            point.Color = Color.SlateGray;
                            point.LegendText = "Neutral";
                            break;
                    }
                    if (chartType.Equals("pieChart") || chartType.Equals("barChart"))
                        point.Label = string.Format("{0:0}", point.YValues[0]);
                    else
                        point.Label = string.Format("");
                }
            }
            catch (Exception e)
            {

            }
        }

        protected void IniatializeHome()
        {
            try
            {
                lblMovieNameHome.Text = movieName;
                ds = obj.LoadMovieList("2", movieName);
                string review;
                double posPercent;
                int total = obj.CalculatePTNTRatio(ds, out review, out posPercent);

                lblReview.Text = review;
                string percent = "width:" + posPercent.ToString() + "%";
                divBar.Attributes.Add("style", percent);
                lblPercent.Text = string.Format("{0:0.00}", posPercent) + "%";
                lblRating.Text = string.Format("{0:0.0}", posPercent / 10) + "/10";
                lblNoOfReviews.Text = total.ToString();
            }
            catch (Exception e)
            {

            }
        }

        protected void grdDisplayData_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (grdDisplayData.Attributes["CurrentSortField"].Equals ( e.SortExpression))
            {
                if (grdDisplayData.Attributes["CurrentSortOrder"].Equals( " DESC"))
                {
                    ds = obj.LoadMovieList("4", movieName, e.SortExpression, " DESC");
                    grdDisplayData.DataSource = ds;
                    grdDisplayData.DataBind();
                    grdDisplayData.Attributes["CurrentSortOrder"] = "";
                    grdDisplayData.Attributes["CurrentSortField"] = "";
                }
            }
            else
            {
                ds = obj.LoadMovieList("4", movieName, e.SortExpression, " ASC");
                grdDisplayData.DataSource = ds;
                grdDisplayData.DataBind();
                grdDisplayData.Attributes["CurrentSortOrder"] = " DESC";
                grdDisplayData.Attributes["CurrentSortField"] = e.SortExpression;
            }
        }

        protected void lnktab_Click(object sender, EventArgs e)
        {
            string id= ((LinkButton)sender).ID.Trim();
            
            int name = Convert.ToInt32 (id.Substring (id.Length -1,1));

            for(int i=1;i<5;i++)
            {
                Panel pnl = (Panel)UpdatePanel1.FindControl("Panel" + i.ToString ());
                HtmlGenericControl liItem = (HtmlGenericControl)UpdatePanel1.FindControl("li" + i);
                if(i!=name)
                {
                    pnl.Visible = false;
                    liItem.Attributes.Add("class", "");
                }else
                {
                    pnl.Visible = true;
                    
                    liItem.Attributes.Add("class", "active");
                }
            }
        }
    }
}