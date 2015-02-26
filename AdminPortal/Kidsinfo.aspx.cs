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

        string order ="t.Ref_No";

        if (sortby_age.Checked)
            order = "AGE";
        else if (sortby_name.Checked)
            order = "NAME_TAG";

        string type1 = "and s.CATEGORY = 'TODDLER' ";
        string type2 = "and s.CATEGORY = 'ELDER' ";

        

        string command = "select t.REF_NO,t.NAME_TAG,t.FULL_NAME,s.AGE,s.CATEGORY,GENDER from KIDS_INFO_TAB t,KIDS_INFO s where t.REF_NO=s.ref_no order by "+ order;
        Kidsinfogrid.DataSource = new DataManager().getkidlist_table(command);
        Kidsinfogrid.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment;filename=Kidsinfo.xls");
        Response.ContentType = "applicatio/exel";

        StringWriter sw = new StringWriter(); ;
        HtmlTextWriter htm = new HtmlTextWriter(sw);
        HtmlForm HtmlForm1 = new HtmlForm();

        Kidsinfogrid.Parent.Controls.Add(HtmlForm1);
        HtmlForm1.Attributes["runat"] = "server";
        HtmlForm1.Controls.Add(Kidsinfogrid);

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