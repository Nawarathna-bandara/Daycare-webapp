using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AdminPortal_Holidays : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!new Manager().IsAdmin(new Manager().GetLogOnUser(this.Page)))
        {
            Response.Redirect("~/Default.aspx");
        }

        if (!Page.IsPostBack)
        {

            dateExtender.SelectedDate = DateTime.Today;
            txtDate.Text = DateTime.Today.ToString("d");
            GenaratedResultsDataTable();
        }
    }

   
    private void GenaratedResultsDataTable()
    {

        Manager mgr = new Manager();

        DateTime date_ = Convert.ToDateTime(txtDate.Text);
        int year_ = date_.Year;

        DateTime dateYearStart = Convert.ToDateTime("01-01-" + year_);
        DateTime dateYearEnd = Convert.ToDateTime("12-31-" + year_);
        double dyearStart = mgr.ConvertToUnixTimestamp(dateYearStart);
        double dyearEnd = mgr.ConvertToUnixTimestamp(dateYearEnd);

        DataTable table = new DataManager().GetHolidaysList(dyearStart, dyearEnd);

        //Session["DATA"] = table;
        grdViewHolidays.DataSource = table;

        grdViewHolidays.DataBind();

        lbMsg.Visible = false;

    }
    protected void DropDownYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtResult = null;
        DataManager dtMgr = new DataManager();
        Manager mgr = new Manager();

        if ("ALL".Equals(DropDownYear.SelectedValue.ToUpper()))
        {
            dtResult = dtMgr.GetHolidaysList(0, 2604355200);
        }
        else if (!("0".Equals(DropDownYear.SelectedValue.ToUpper())))
        {

            int year_ = Convert.ToInt32(DropDownYear.SelectedValue);

            DateTime dateYearStart = Convert.ToDateTime("01-01-" + year_);
            DateTime dateYearEnd = Convert.ToDateTime("12-31-" + year_);
            double dyearStart = mgr.ConvertToUnixTimestamp(dateYearStart);
            double dyearEnd = mgr.ConvertToUnixTimestamp(dateYearEnd);

            dtResult = dtMgr.GetHolidaysList(dyearStart, dyearEnd);
        }

        grdViewHolidays.DataSource = dtResult;
        //Session["RESULT"] = dtResult;
        grdViewHolidays.DataBind();
    }

    protected void btAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Manager mgr = new Manager();
            DataManager dMgr = new DataManager();
            DataTable dtResult = null;

            DateTime date_ = Convert.ToDateTime(txtDate.Text);
            int year_ = date_.Year;

            DateTime dateYearStart = Convert.ToDateTime("01-01-" + year_);
            DateTime dateYearEnd = Convert.ToDateTime("12-31-" + year_);
            double dyearStart = mgr.ConvertToUnixTimestamp(dateYearStart);
            double dyearEnd = mgr.ConvertToUnixTimestamp(dateYearEnd);

            double unixDate = mgr.ConvertToUnixTimestamp(date_);

            if (dMgr.InsertDataToHolidayTab(unixDate, txtDescription.Text, 2, LogOnUser))
            {

                lbMsg.Visible = false;
                dtResult = dMgr.GetHolidaysList(dyearStart, dyearEnd);
                grdViewHolidays.DataSource = dtResult;

                grdViewHolidays.DataBind();

                //Session["RESULT"] = dtResult;
                //Application.Clear();
                Application["HOLIDAYS"] = dMgr.GetHolidays();

                //txtDate.Text = string.Empty;
                txtDescription.Text = string.Empty;

                lbMsg.Visible = false;
                lbInfo.Visible = true;
                lbInfo.Text = txtDate.Text+" is successfully added as a Holiday"; 
            }
            else
            {
                lbInfo.Visible = false;
                lbMsg.Visible = true;
                lbMsg.Text = "Adding Operation Failed";
            }
        }
        catch (Exception ex)
        {

            lbMsg.Visible = true;
            lbInfo.Visible = false;

            if (ex.Message.Trim().ToUpper().Equals("ORA-00001: unique constraint (IFSJ.TB_HOLIDAYS_LK_PK) violated".ToUpper()))
                lbMsg.Text = "Specified date already exist in the system";
            else
                lbMsg.Text = ex.Message;
        }
    }

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
    protected void grdViewHolidays_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            DataManager dMgr = new DataManager();

            GridViewRow row = (GridViewRow)grdViewHolidays.Rows[e.RowIndex];

            if (row.RowType == DataControlRowType.DataRow)
            {
                string val = grdViewHolidays.Rows[e.RowIndex].Cells[1].Text.ToString();

                if (DeleteHoliday(val))
                {
                    GenaratedResultsDataTable();

                    //Application.Clear();
                    Application["HOLIDAYS"] = dMgr.GetHolidays();

                    lbMsg.Visible = false;
                    lbInfo.Visible = true;
                    lbInfo.Text = "Successfully Deleted"; 
                }
                else {

                    lbInfo.Visible = false;
                    lbMsg.Visible = true;
                    lbMsg.Text = "Deletion Operation Failed";
                }
            }
        }
    }

    private bool DeleteHoliday(string val)
    {

        DataManager mgr = new DataManager();
        DateTime dTime = Convert.ToDateTime(val);

        try
        {
            return mgr.DeleteDataFromHolidayTab(new Manager().ConvertToUnixTimestamp(dTime));
        }
        catch (Exception ex)
        {
            lbMsg.Text = ex.Message;
            lbMsg.Visible = true;
            return false;
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