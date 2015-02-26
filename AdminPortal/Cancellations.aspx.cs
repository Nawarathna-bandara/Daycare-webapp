using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class AdminPortal_DeleteSummery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!new Manager().IsAdmin(new Manager().GetLogOnUser(this.Page)))
        {
            Response.Redirect("~/Default.aspx");
        }

        if (!Page.IsPostBack)
        {

            fromDateExtender.SelectedDate = DateTime.Today;
            toDateExtender.SelectedDate = DateTime.Today;
            txtFromDate.Text = DateTime.Today.ToString("d");
            txtToDate.Text = DateTime.Today.ToString("d");

            GenaratedResultsDataTable();
        }
    }


    private void GenaratedResultsDataTable()
    {

        Manager mgr = new Manager();
        //DateTime fromDate = Convert.ToDateTime( txtFromDate.Text).Date;
        //DateTime toDate = Convert.ToDateTime(txtToDate.Text).Date;

        if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
        {
            string fromDate = txtFromDate.Text;
            string toDate = txtToDate.Text;

            DataTable table = new DataManager().GetDeletionKidList(fromDate, toDate);

            Session["DATA"] = table;
            grdViewKidsDeletion.DataSource = table;

            grdViewKidsDeletion.DataBind();

            lbMsg.Visible = false;

            fromDateExtender.SelectedDate = Convert.ToDateTime( txtFromDate.Text );
            toDateExtender.SelectedDate = Convert.ToDateTime( txtToDate.Text );

        }
        else {

            lbMsg.Text = "Valid date should be selected!";
            lbMsg.Visible = true;
        }

    }
    protected void btRefresh_Click(object sender, EventArgs e)
    {
        GenaratedResultsDataTable();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment;filename=Cancellations.xls");
        Response.ContentType = "applicatio/exel";

        StringWriter sw = new StringWriter(); ;
        HtmlTextWriter htm = new HtmlTextWriter(sw);
        HtmlForm HtmlForm1 = new HtmlForm();

        grdViewKidsDeletion.Parent.Controls.Add(HtmlForm1);
        HtmlForm1.Attributes["runat"] = "server";
        HtmlForm1.Controls.Add(grdViewKidsDeletion);

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