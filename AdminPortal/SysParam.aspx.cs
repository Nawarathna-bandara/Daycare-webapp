using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IFS.CoS.ServiceUtil;
//using Oracle.DataAccess.Client;
using System.Collections;

public partial class AdminPortal_SysParam : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!new Manager().IsAdmin(new Manager().GetLogOnUser(this.Page)))
        {
            Response.Redirect("~/Default.aspx");
        }

        if (!new Manager().IsSuperAdmin( LogOnUser )) {

            Response.Redirect("/Default.aspx");
        }

        lbInfo.Visible = false;

        if (!Page.IsPostBack)
        {
            BindGridData();
        }
    }

    //private bool IsAdmin()
    //{

    //    ArrayList arrUsernames = null;

    //    if (Application["ADMIN_USERNAMES"] != null)
    //    {
    //        arrUsernames = (ArrayList)Application["ADMIN_USERNAMES"];
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

    public string LogOnUser
    {
        get
        {
            if (Session["LOGON_USER"] != null && !string.IsNullOrEmpty(Session["LOGON_USER"].ToString()))
                return Session["LOGON_USER"].ToString();

            else
            {
                string Auth_User = Page.User.Identity.Name.ToUpper();
                string userName = string.Empty;

                if (!string.IsNullOrEmpty(Auth_User))
                {

                    Auth_User = Auth_User.Replace("/", "\\");
                    if (Auth_User.Contains("\\"))
                        userName = Auth_User.Split(new Char[] { '\\' })[1];
                    if (userName == null)
                        userName = Auth_User;

                    Session["LOGON_USER"] = userName.ToUpper();
                }
                return userName.ToUpper();
            }
        }
    }

    private void BindGridData() {

        DataManager dtMgr = new DataManager();

        grdViewSysParam.DataSource = dtMgr.GetSysParam();
        grdViewSysParam.DataBind();
    }

    protected void grdViewSysParam_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdViewSysParam.EditIndex = e.NewEditIndex;
        BindGridData();
    }
    protected void grdViewSysParam_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            GridViewRow row = (GridViewRow) grdViewSysParam.Rows[e.RowIndex]; 

            if (row.RowType == DataControlRowType.DataRow)
            {

                //string s = grdViewSysParam.DataKeys[e.RowIndex].Value.ToString();
                Label lbParamName = grdViewSysParam.Rows[e.RowIndex].FindControl("lbParamName") as Label;
                TextBox txtParamValue = grdViewSysParam.Rows[e.RowIndex].FindControl("txtParamValue") as TextBox;
                TextBox txtComments = grdViewSysParam.Rows[e.RowIndex].FindControl("txtComments") as TextBox;

                grdViewSysParam.EditIndex = -1;

                new DataManager().UpdateSysParamByName(lbParamName.Text, txtParamValue.Text, txtComments.Text, LogOnUser);
                CacheUtil.RemoveCachedItem(lbParamName.Text.ToUpper());
                
                BindGridData();
            }
        }
    }
    protected void grdViewSysParam_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            GridViewRow row = (GridViewRow)grdViewSysParam.Rows[e.RowIndex];

            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lbParamName = grdViewSysParam.Rows[e.RowIndex].FindControl("lbParamName") as Label;

                new DataManager().RemoveSysParamByName(lbParamName.Text);
                CacheUtil.RemoveCachedItem(lbParamName.Text.ToUpper());

                BindGridData();
            }
        }
    }

    protected void grdViewSysParam_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        e.Cancel = true;
        grdViewSysParam.EditIndex = -1;
        BindGridData(); 
    }

    protected void grdViewSysParam_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         if( e.CommandName.Equals("Insert") ) {

             TextBox txtParamName = grdViewSysParam.FooterRow.FindControl("txtParamNameIns") as TextBox;
             TextBox txtParamValue = grdViewSysParam.FooterRow.FindControl("txtParamValueIns") as TextBox;
             TextBox txtComments = grdViewSysParam.FooterRow.FindControl("txtCommentsIns") as TextBox;

             DataManager mgr = new DataManager();

             if (!string.IsNullOrEmpty(txtParamName.Text) && !string.IsNullOrEmpty(txtParamValue.Text))
             {
                 if (!mgr.CheckExistSysParam(txtParamName.Text))
                 {
                     mgr.AddSysParam(txtParamName.Text, txtParamValue.Text, txtComments.Text, LogOnUser); 
                     BindGridData(); 
                 }
                 else {
                     lbInfo.Visible = true;
                     lbInfo.Text = "Paramter Name is already exist.";
                 }
             }
             else
             {
                 lbInfo.Visible = true;
                 lbInfo.Text = "Paramter Name and Value cannot be Empty.";
             }            
         }
    }
    protected void btRefresh_Click(object sender, EventArgs e)
    {
        try {

            HttpRuntime.UnloadAppDomain();
        }
        catch (Exception ex) {

            EventLogUtil.Log(ex.Message);
        }

    }

    protected void Attendance_Click(object sender, EventArgs e)
    {
        Response.Redirect("Attendance.aspx");
    }
    protected void Contacts_Click(object sender, EventArgs e)
    {
        Response.Redirect("Contacts.aspx");
    }
    protected void Stats_Click(object sender, EventArgs e)
    {
        Response.Redirect("Stats.aspx");
    }
    protected void Holidays_Click(object sender, EventArgs e)
    {
        Response.Redirect("Holidays.aspx");
    }
    protected void Kidsinfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Kidsinfo.aspx");
    }
    protected void Renewsub_Click(object sender, EventArgs e)
    {
        Response.Redirect("RenewSub.aspx");
    }
    protected void Sysparam_Click(object sender, EventArgs e)
    {
        Response.Redirect("SysParam.aspx");
    }
    protected void Cancellations_Click(object sender, EventArgs e)
    {
        Response.Redirect("Cancellations.aspx");
    }
    protected void Addkids_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddKids.aspx");
    }
    protected void Admins_Click(object sender, EventArgs e)
    {
        Response.Redirect("Admins.aspx");
    }
}