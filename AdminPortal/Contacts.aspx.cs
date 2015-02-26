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

        if (sortby_kidid.Checked) {
            order = "NAME_TAG";
        }

        string command = "select REF_NO,NAME_TAG,FULLNAME,RELATIONSHIP,EMAIL from KIDS_INFO_TAB t,KIDS_GUARDIAN_TAB s, GUARDIAN_INFO_TAB p where t.Ref_No=s.kid_ref_no and p.Username = s.guardian_username order by "+ order;
        contactsgrid.DataSource = new DataManager().getkidlist_table(command);
        contactsgrid.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment;filename=Contacts.xls");
        Response.ContentType = "applicatio/exel";

        StringWriter sw = new StringWriter(); ;
        HtmlTextWriter htm = new HtmlTextWriter(sw);
        HtmlForm HtmlForm1 = new HtmlForm();

        contactsgrid.Parent.Controls.Add(HtmlForm1);
        HtmlForm1.Attributes["runat"] = "server";
        HtmlForm1.Controls.Add(contactsgrid);

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