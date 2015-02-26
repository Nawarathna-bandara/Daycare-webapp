using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class AdminPortal_Attendance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!new Manager().IsAdmin(new Manager().GetLogOnUser(this.Page)))
        {
            Response.Redirect("~/Default.aspx");
        }

        DataManager mgr = new DataManager();

        if (!IsPostBack)
        {
            date.Text = DateTime.Today.ToString("d");

            AMgrid_p.DataSource = mgr.getkidlist_table("select k.NAME_TAG from KIDS_INFO_TAB k, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED = TO_DATE('" + date.Text + "', 'MM/DD/YYYY') and t.ATTENDANCE = 1 and t.BOOKING_STATE = 1 and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED ='AM' order by k.NAME_TAG");
            AMgrid_p.DataBind();
            AMgrid_a.DataSource = mgr.getkidlist_table("select k.NAME_TAG from KIDS_INFO_TAB k, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED = TO_DATE('" + date.Text + "', 'MM/DD/YYYY') and t.ATTENDANCE = 0 and t.BOOKING_STATE = 1 and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED ='AM' order by k.NAME_TAG");
            AMgrid_a.DataBind();

            PMgrid_p.DataSource = mgr.getkidlist_table("select k.NAME_TAG from KIDS_INFO_TAB k, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED = TO_DATE('" + date.Text + "', 'MM/DD/YYYY') and t.ATTENDANCE = 1 and t.BOOKING_STATE = 1 and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED = 'PM' order by k.NAME_TAG");
            PMgrid_p.DataBind();
            PMgrid_a.DataSource = mgr.getkidlist_table("select k.NAME_TAG from KIDS_INFO_TAB k, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED = TO_DATE('" + date.Text + "', 'MM/DD/YYYY') and t.ATTENDANCE = 0 and t.BOOKING_STATE = 1 and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED = 'PM' order by k.NAME_TAG");
            PMgrid_a.DataBind();
        }

                 
    }

    protected void AMp_Click(object sender, EventArgs e)
    {
        DataManager dm = new DataManager();

        foreach (GridViewRow item in AMgrid_p.Rows)
        {
            CheckBox chk = (CheckBox)item.FindControl("CheckBox1");         
            if (chk.Checked)
            {
                dm.markAttendence(DateTime.Parse(date.Text), "AM", item.Cells[1].Text,0);
                dm.addcount_name(item.Cells[1].Text, "ABSENT");
                dm.reducecount_name(item.Cells[1].Text, "PRESENT");
            }
        }

        string AMcmd_p = "select k.NAME_TAG from KIDS_INFO_TAB k, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED = TO_DATE('" + date.Text + "', 'MM/DD/YYYY') and t.ATTENDANCE = 1 and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED ='AM' order by k.NAME_TAG";
        AMgrid_p.DataSource = dm.getkidlist_table(AMcmd_p);
        AMgrid_p.DataBind();

        string AMcmd_a = "select k.NAME_TAG from KIDS_INFO_TAB k, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED =  TO_DATE('" + date.Text + "', 'MM/DD/YYYY') and t.ATTENDANCE = 0 and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED ='AM' order by k.NAME_TAG";
        AMgrid_a.DataSource = dm.getkidlist_table(AMcmd_a);
        AMgrid_a.DataBind();

    }

    protected void AMa_Click(object sender, EventArgs e)
    {
        DataManager dm = new DataManager();

        foreach (GridViewRow item in AMgrid_a.Rows)
        {
            CheckBox chk = (CheckBox)item.FindControl("CheckBox1");
            if (chk.Checked)
            {
                dm.markAttendence(DateTime.Parse(date.Text), "AM", item.Cells[1].Text, 1);
                dm.addcount_name(item.Cells[1].Text, "PRESENT");
                dm.reducecount_name(item.Cells[1].Text, "ABSENT");
            }

        }

        string AMcmd_a = "select k.NAME_TAG from KIDS_INFO_TAB k, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED =  TO_DATE('" + date.Text + "', 'MM/DD/YYYY') and t.ATTENDANCE = 0 and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED ='AM' order by k.NAME_TAG";
        AMgrid_a.DataSource = dm.getkidlist_table(AMcmd_a);
        AMgrid_a.DataBind();

        string AMcmd_p = "select k.NAME_TAG from KIDS_INFO_TAB k, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED =  TO_DATE('" + date.Text + "', 'MM/DD/YYYY') and t.ATTENDANCE = 1 and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED ='AM' order by k.NAME_TAG";
        AMgrid_p.DataSource = dm.getkidlist_table(AMcmd_p);
        AMgrid_p.DataBind();

    }


    protected void PMp_Click(object sender, EventArgs e)
    {
        DataManager dm = new DataManager();

        foreach (GridViewRow item in PMgrid_p.Rows)
        {

            CheckBox chk = (CheckBox)item.FindControl("CheckBox1");

            if (chk.Checked)
            {
                dm.markAttendence(DateTime.Parse(date.Text), "PM", item.Cells[1].Text, 0);
                dm.addcount_name(item.Cells[1].Text, "ABSENT");
                dm.reducecount_name(item.Cells[1].Text, "PRESENT");
            }
        }

        string PMcmd_p = "select k.NAME_TAG from KIDS_INFO_TAB k, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED =  TO_DATE('" + date.Text + "', 'MM/DD/YYYY') and t.ATTENDANCE = 1 and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED ='PM' order by k.NAME_TAG";
        PMgrid_p.DataSource = dm.getkidlist_table(PMcmd_p);
        PMgrid_p.DataBind();

        string PMcmd_a = "select k.NAME_TAG from KIDS_INFO_TAB k, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED =  TO_DATE('" + date.Text + "', 'MM/DD/YYYY') and t.ATTENDANCE = 0 and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED ='PM' order by k.NAME_TAG";
        PMgrid_a.DataSource = dm.getkidlist_table(PMcmd_a);
        PMgrid_a.DataBind();

    }

   
    protected void PMa_Click(object sender, EventArgs e)
    {
        DataManager dm = new DataManager();

        foreach (GridViewRow item in PMgrid_a.Rows)
        {

            CheckBox chk = (CheckBox)item.FindControl("CheckBox1");

            if (chk.Checked)
            {
                dm.markAttendence(DateTime.Parse(date.Text), "PM", item.Cells[1].Text, 1);
                dm.addcount_name(item.Cells[1].Text, "PRESENT");
                dm.reducecount_name(item.Cells[1].Text, "ABSENT");
            }

        }

        string PMcmd_a = "select k.NAME_TAG from KIDS_INFO_TAB k, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED =  TO_DATE('" + date.Text + "', 'MM/DD/YYYY') and t.ATTENDANCE = 0 and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED ='PM' order by k.NAME_TAG";
        PMgrid_a.DataSource = dm.getkidlist_table(PMcmd_a);
        PMgrid_a.DataBind();

        string PMcmd_p = "select k.NAME_TAG from KIDS_INFO_TAB k, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED =  TO_DATE('" + date.Text + "', 'MM/DD/YYYY') and t.ATTENDANCE = 1 and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED ='PM' order by k.NAME_TAG";
        PMgrid_p.DataSource = dm.getkidlist_table(PMcmd_p);
        PMgrid_p.DataBind();

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

    protected void Button1_Click(object sender, EventArgs e)
    {
        String date1 = date.Text;
        DataManager dm = new DataManager();

        string PMcmd_a = "select k.NAME_TAG from KIDS_INFO_TAB k, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED =TO_DATE('"+date1+"', 'MM/DD/YYYY') and t.ATTENDANCE = 0 and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED ='PM' order by k.NAME_TAG";
        PMgrid_a.DataSource = dm.getkidlist_table(PMcmd_a);
        PMgrid_a.DataBind();

        string PMcmd_p = "select k.NAME_TAG from KIDS_INFO_TAB k, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED =TO_DATE('" + date1 + "', 'MM/DD/YYYY') and t.ATTENDANCE = 1 and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED ='PM' order by k.NAME_TAG";
        PMgrid_p.DataSource = dm.getkidlist_table(PMcmd_p);
        PMgrid_p.DataBind();

        string AMcmd_a = "select k.NAME_TAG from KIDS_INFO_TAB k, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED =TO_DATE('" + date1 + "', 'MM/DD/YYYY') and t.ATTENDANCE = 0 and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED ='AM' order by k.NAME_TAG";
        AMgrid_a.DataSource = dm.getkidlist_table(AMcmd_a);
        AMgrid_a.DataBind();

        string AMcmd_p = "select k.NAME_TAG from KIDS_INFO_TAB k, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED =TO_DATE('" + date1 + "', 'MM/DD/YYYY') and t.ATTENDANCE = 1 and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED ='AM' order by k.NAME_TAG";
        AMgrid_p.DataSource = dm.getkidlist_table(AMcmd_p);
        AMgrid_p.DataBind();
    }
}