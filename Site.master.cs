using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Drawing;

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;*/
using IFS.CoS.ServiceUtil;



public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;

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

    void master_Page_PreLoad(object sender, EventArgs e)
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
        DateTime today = DateTime.Now;
        today_lable.Text = today.ToLongDateString();
        ArrayList arrUsernames = (ArrayList)Application["GUARDIAN_USERNAMES"];
        
        //EventLogUtil.Log(LogOnUser + LogOnUser);

        if (new Manager().IsSuperAdmin(LogOnUser))
        {
            btAdminPortal.Visible = true;
            EventLogUtil.Log(LogOnUser +"superadmin");
        }
        else
        {
            btAdminPortal.Visible = false;
        }
        
        if (!new Manager().IsAdmin(LogOnUser) && (arrUsernames != null && !arrUsernames.Contains(LogOnUser)))
        {
            btConditions.Visible = false;
            btMyPortal.Visible = false;
        }



        try
        {
            if (Request.Url.ToString().ToUpper().Contains("MYPORTAL"))
            {
                btMyPortal.BackColor = Color.FromArgb(135, 62, 141);
                btMyPortal.ForeColor = Color.White;
            }
            else if (Request.Url.ToString().ToUpper().Contains("ADMINPORTAL"))
            {
                btAdminPortal.BackColor = Color.FromArgb(135, 62, 141);
                btAdminPortal.ForeColor = Color.White;
            }
            else if (Request.Url.ToString().ToUpper().Contains("CONDITIONS"))
            {
                btConditions.BackColor = Color.FromArgb(135, 62, 141);
                btConditions.ForeColor = Color.White;
            }
            else{
                btHome.BackColor = Color.FromArgb(135, 62, 141);
                btHome.ForeColor = Color.White;
            }
        }
        catch (Exception ex)
        {
           
        }
    }


    public string LogOnUser
    {
        get
        {

            if (Session["LOGON_USER"] != null && !string.IsNullOrEmpty(Session["LOGON_USER"].ToString()))
                return Session["LOGON_USER"].ToString().ToUpper();

            else
                return Page.User.Identity.Name.ToUpper();
        }
    }

    //private bool IsSuperAdmin()
    //{

    //    ArrayList arrUsernames = null;

    //    if (Application["SUPER_ADMIN_USERNAMES"] != null)
    //    {
    //        arrUsernames = (ArrayList)Application["SUPER_ADMIN_USERNAMES"];
    //        if (arrUsernames != null && arrUsernames.Contains(LogOnUser))
    //            return true;
    //        else
    //            return false;
    //    }
    //    else
    //    {

    //        return false;
    //    }
    //}

    protected void btOverview_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Overview/Overview.aspx");
    }
    protected void btMyPortal_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MyPortal/ManageAttendance.aspx");
    }
    protected void btAdminPortal_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPortal/Attendance.aspx");
    }
    protected void btHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void btConditions_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Conditions.aspx");
        /*not yet defined*/
    }
}