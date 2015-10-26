using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace MovieProphecy
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        private DatabaseLayer obj = new DatabaseLayer();
        private DataSet ds;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Load the DropDown
                //ds = obj.LoadMovieList();
                //ddlMovieList.DataSource = ds.Tables[0];
                //ddlMovieList.DataTextField = ConfigurationManager.AppSettings["ColumnName"];
                //ddlMovieList.DataValueField = ConfigurationManager.AppSettings["ColumnName"];
                //ddlMovieList.DataBind();
                //ListItem item = new ListItem("-Select a movie-", "0");
                //ddlMovieList.Items.Insert(0, item);

                //Retrieve the Upcoming movies

                try
                {
                    RottenTomato rt = new RottenTomato();
                    List<string> recentMovies = rt.FindMoviesInTheaterList();
                    List<string> upcomingMovies = rt.FindOpeningMoviesList();
                    for (int i = 0; i < 5; i++)
                    {

                        LinkButton lnk = (LinkButton)FindControl("lnkBtnUpcomingMovie" + (i + 1).ToString());
                        lnk.Text = upcomingMovies[i];
                        lnk = (LinkButton)FindControl("lnkBtnRecentMovie" + (i + 1).ToString());
                        lnk.Text = recentMovies[i];

                    }
                }
                catch (Exception ex)
                {

                }

            }
            //list of movies
            for (int i = 65; i <= 90; i++)
            {
                LinkButton link = new LinkButton();
                link.Text = "  " + ((char)i).ToString() + " ";
                link.ToolTip = "Get the list of movies starting with the letter " + link.Text;
                link.Click += new System.EventHandler(link_Click);
                pnlList.Controls.Add(link);
            }

        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut();
        }

        protected void link_Click(object sender, EventArgs e)
        {
            //ddlMovieList.SelectedIndex = 0;
            String clickedAlphabet = ((LinkButton)sender).Text.Trim().ToString();
            Response.Redirect(String.Format("MovieContent.aspx?flag={0}&Name={1}", Server.UrlEncode("1"), Server.UrlEncode(clickedAlphabet)));
        }



        protected void btnGo_Click(object sender, EventArgs e)
        {
            //if (ddlMovieList.SelectedIndex != 0)
            //{
            //    string selectedMovie = ddlMovieList.SelectedItem.Value.Trim();
            //    Response.Redirect(String.Format("MovieContent.aspx?flag={0}&Name={1}", Server.UrlEncode("2"), Server.UrlEncode(selectedMovie)));
            //}

            if (!txtMovieName.Text.Trim().Equals(""))
            {
                CallJava cj = new CallJava();
                string flag = cj.CallApi(txtMovieName.Text.Trim());
                Response.Redirect(String.Format("MovieContent.aspx?flag={0}&Name={1}", Server.UrlEncode("2"), Server.UrlEncode(txtMovieName.Text.Trim())));
            }
        }





        protected void lnkBtnMovie_Click(object sender, EventArgs e)
        {
            string movieName = ((LinkButton)sender).Text.Trim();
            CallJava cj = new CallJava();
            string flag = cj.CallApi(movieName);
            Response.Redirect(String.Format("MovieContent.aspx?flag={0}&Name={1}", Server.UrlEncode("2"), Server.UrlEncode(movieName)));
        }
    }

}