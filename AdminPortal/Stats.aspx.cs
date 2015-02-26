using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class AdminPortal_Overview : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!new Manager().IsAdmin(new Manager().GetLogOnUser(this.Page)))
        {
            Response.Redirect("~/Default.aspx");
        }

        if (!IsPostBack) {
            toddler_chk.Checked = true;
            elder_chk.Checked = true;
        }

        string sortby = "p.points";
        string orderby = "";

        if (sortby_refno.Checked)
            sortby = "k.REF_NO";
        else if (sortby_name.Checked)
            sortby = "k.NAME_TAG";
        else if (sortby_present.Checked)
            sortby = "t.PRESENT";
        else if (sortby_fixes.Checked)
            sortby = "t.FIXED_BOOKINGS";
        else if (sortby_bookings.Checked)
            sortby = "t.BOOKINGS";
        else if (sortby_cancelled.Checked)
            sortby = "t.CANCELLED";
        else if (sortby_absent.Checked)
            sortby = "t.ABSENT";
        else if (sortby_bulk.Checked)
            sortby = "t.BULK_CANCELLATIONS";
        
        string tods =" ";
        string elds = " ";

        if (toddler_chk.Checked)
        {
            tods = "and k.category ='TODDLER'";

            if (elder_chk.Checked)
                tods = "";

        }
        else
        {
            if (elder_chk.Checked)
                tods = "and k.category ='ELDER'";
            else
                tods = "and k.category ='NONE'";
        }

        if (Order.Checked) {
            orderby = "DESC";
        }

        string toddlers = "select k.REF_NO, k.NAME_TAG, p.POINTS, t.BOOKINGS, t.FIXED_BOOKINGS, t.PRESENT,t.ABSENT,t.CANCELLED,t.CANCELLED_THISWEEK,t.BULK_CANCELLATIONS from KIDS_INFO k, points p,KIDS_STATS_TAB t where p.kid_name = k.name_tag and t.REF_NO=p.REF_NO "+tods+" "+elds+" and k.ref_no=p.REF_NO order by " + sortby+" "+orderby;

        statsgrid.DataSource = new DataManager().getkidlist_table(toddlers);
        statsgrid.DataBind();

        //eldersgrid.DataSource = new DataManager().getkidlist_table(elders);
        //eldersgrid.DataBind();


    }


    protected void Button1_Click(object sender, EventArgs e)
    {

        Response.ClearContent();
        Response.AddHeader("content-disposition","attachment;filename=Stats.xls");
        Response.ContentType = "applicatio/exel";

        StringWriter sw = new StringWriter(); ;
        HtmlTextWriter htm = new HtmlTextWriter(sw);
        HtmlForm HtmlForm1 = new HtmlForm();

        statsgrid.Parent.Controls.Add(HtmlForm1);
        HtmlForm1.Attributes["runat"] = "server";
        HtmlForm1.Controls.Add(statsgrid);
        //HtmlForm1.Controls.Add(eldersgrid);

        HtmlForm1.RenderControl(htm);
        Response.Write(sw.ToString());
        Response.End();
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