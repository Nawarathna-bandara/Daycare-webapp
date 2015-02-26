using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IFS.CoS.ServiceUtil;
using Oracle.DataAccess.Client;

public partial class AdminPortal_RenewSub : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!new Manager().IsAdmin(new Manager().GetLogOnUser(this.Page)))
        {
            Response.Redirect("~/Default.aspx");
        }

        if (!Page.IsPostBack)
        {           
            this.LoadKidsName();
            GenaratedResultsDataTable();
        }

        lbMsg.Visible = false;
        lbInfo.Visible = false;
    }

    private void LoadKidsName()
    {

        OracleConnection oraConn = null;

        DBUtil db = new DBUtil();

        try
        {
            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

            OracleDataReader dataRd = new DataManager().GetKidNamesTagList(oraConn);
            int count = 0;
            while (dataRd != null && dataRd.Read())
            {

                if (!dataRd.IsDBNull(0))
                {
                    string ss = dataRd.GetDataTypeName(1);
                    drpKidName.Items.Add(new ListItem(dataRd.GetString(0), Convert.ToString(dataRd.GetDecimal(1)))); 
                    count++;
                }
            }

            drpKidName.SelectedIndex = 0;

            if (dataRd != null)
                dataRd.Close();
        }
        catch (Exception ex)
        {
            EventLogUtil.Log(ex.Message);
        }
        finally
        {

            db.CloseConnection(oraConn);
        }
    }

    private void GenaratedResultsDataTable( int kidRefNo = -1 )
    {

        Manager mgr = new Manager();

        DataTable table = new DataManager().GetKidSubDetails( kidRefNo );

        grdViewSubscription.DataSource = table;

        grdViewSubscription.DataBind();

        lbMsg.Visible = false;
    }

    protected void drpKidNmae_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtResult = null;
        DataManager dtMgr = new DataManager();
        Manager mgr = new Manager();

        if ("0".Equals(drpKidName.SelectedValue.ToUpper()))
        {
            dtResult = dtMgr.GetKidSubDetails();
        }
        else if (!("0".Equals(drpKidName.SelectedValue.ToUpper())))
        {

            int kidRef_ = Convert.ToInt32(drpKidName.SelectedValue);

            dtResult = dtMgr.GetKidSubDetails(kidRef_);
        }

        grdViewSubscription.DataSource = dtResult;
        grdViewSubscription.DataBind();
    }

    protected void btUpdate_Click(object sender, EventArgs e)
    {
              
       int kidRef_ = Convert.ToInt32(drpKidName.SelectedValue);
       int subYear_ = Convert.ToInt32(DropDownYear.SelectedValue);

      
       if (new DataManager().UpdateKidSubYear(kidRef_, subYear_))
       {
           GenaratedResultsDataTable(kidRef_);
           lbMsg.Visible = false;
           lbInfo.Visible = true;
           lbInfo.Text = "Successfully updated " + drpKidName.SelectedItem.Text + "  to subscription year " + subYear_;
       }
       else {

           lbMsg.Visible = true;
           lbInfo.Visible = false;
           lbMsg.Text = "Failed to Update the kid " + drpKidName.SelectedItem.Text ;
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