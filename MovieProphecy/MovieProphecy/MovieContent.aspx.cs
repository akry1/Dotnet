using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieProphecy
{
    public partial class Content : System.Web.UI.Page
    {
        DatabaseLayer obj = new DatabaseLayer();
        DataSet ds;
        string movieName;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                movieName = Request.QueryString[1];
                lnkInfo.Visible = true;
                if (!IsPostBack)
                {
                    string flag = Request.QueryString[0];
                    ds = obj.LoadMovieList(flag, movieName);
                    if (flag == "1")
                    {
                        DisplayMovieNames(grdDisplayData);
                        lnkInfo.Visible = false;
                    }
                    else
                    {
                        if (!Context.User.Identity.IsAuthenticated)
                        {
                            DisplayMovieReveiw();
                            grdDisplayData.Visible = false;
                        }
                        else
                        {
                            Response.Redirect(String.Format("DetailedView.aspx?Name={0}", Server.UrlEncode(movieName)));
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void grdDisplayData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Movie Names Starting with " + movieName;
            }
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {
                movieName = ((LinkButton)sender).Text.ToString();

                if (!Context.User.Identity.IsAuthenticated)
                {
                    ViewState["movieName"] = movieName;
                    ds = obj.LoadMovieList("2", movieName);
                    DisplayMovieReveiw();
                    grdDisplayData.Visible = false;

                }
                else
                {
                    Response.Redirect(String.Format("DetailedView.aspx?Name={0}", Server.UrlEncode(movieName)));
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void DisplayMovieNames(GridView grid)
        {
            try
            {
                pnlMovieDetails.Visible = false;
                grid.DataSource = ds.Tables[0];
                grid.DataBind();
                grid.Visible = true;
            }
            catch (Exception e)
            {

            }

        }

        protected void DisplayMovieReveiw()
        {
            try
            {
                pnlMovieDetails.Visible = true;
                lblMovieName.Text = movieName;
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

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Write("<script>alert('Please login to view more information')</script>");
            }
            else
            {
                if (ViewState["movieName"] != null)
                {
                    movieName = ViewState["movieName"].ToString();
                    ViewState["movieName"] = null;
                }
                Response.Redirect(String.Format("DetailedView.aspx?Name={0}", Server.UrlEncode(movieName)));
            }

        }
    }
}