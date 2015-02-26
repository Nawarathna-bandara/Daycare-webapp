using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IFS.CoS.ServiceUtil;
using Oracle.DataAccess.Client;
using System.Data;
using System.Collections;

public partial class MyPortal_ManageAttendance : System.Web.UI.Page
{
    private int elderCount = 0;
    private int toddlerCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            elderCount = Convert.ToInt32(SysParamReaderUtil.GetSysParamByName("ELDERS_FIXED_SLOTS"));
            toddlerCount = Convert.ToInt32(SysParamReaderUtil.GetSysParamByName("TODDLERS_FIXED_SLOTS"));
        }
        catch (Exception)
        {

            if (!new DataManager().CheckExistDB(ServiceUtil.DB.DefaultDB))
            {
                lbInfo.Visible = true;
                lbInfo.Text = "<br /><br /> Sorry, IFS Juniors Forecast System was unable to connect to the database.<br />This may be caused by the server being busy.<br /><br />Please try again later.";
                return;
            }
            else
            {
                lbInfo.Text = "";
                lbInfo.Visible = false;
            }
        }

        if (!Page.IsPostBack) {
            
            if (!IsAdmin()) {

                drpKidName.Visible = false;
                lbKidName.Visible = false;
                lbParent1.Visible = false;
                lbParent2.Visible = false;
                btExport.Visible = false;
                adminPanel.Visible = false;
            }

            Manager mgr = new Manager();
            DateTime fromDate = mgr.GetThisMonday().Date;
            DateTime toDate = mgr.GetNextMonday().AddDays(4);

            fromDate = DateTime.Now.Date; // Only shows Data from Today.

            lbFromDate.Text = String.Format("{0:D}", fromDate);
            lbTodate.Text = String.Format("{0:D}", toDate);
            lbKidName.Text = "Kids Name";

            if (IsAdmin())
            {
                LoadKidsName();
            }

            BindGridData();

        }
    }

    private void LoadKidsName()
    {
        OracleConnection oraConn = null;

        DBUtil db = new DBUtil();

        try
        {
            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

            //OracleDataReader dataRd = new DataManager().GetKidNamesTagList(oraConn);
            OracleDataReader dataRd = new DataManager().GetKidNamesTagList(oraConn);
            int count = 0;
            while (dataRd != null && dataRd.Read())
            {

                if (!dataRd.IsDBNull(0))
                {
                    drpKidName.Items.Add(new ListItem(dataRd.GetString(0), Convert.ToString(dataRd.GetInt32(1))));
                    /**/
                    //drpKidName2.Items.Add(new ListItem(dataRd.GetString(0), Convert.ToString(dataRd.GetInt32(1))));
                    
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
        finally {

            db.CloseConnection( oraConn);
        }
    }

    private void LoadParentsName( int iKidRefNo )
    {

        OracleConnection oraConn = null;

        DBUtil db = new DBUtil();

        lbParent1.Text = "";
        lbParent2.Text = "";

        try
        {
            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

            OracleDataReader dataRd = new DataManager().GetGuardianUserNameListByKidRefNo( iKidRefNo, oraConn );

            int count = 0;

            while (dataRd != null && dataRd.Read())
            {

                if (!dataRd.IsDBNull(0))
                {
                    if( count == 0 )
                        lbParent1.Text = dataRd.GetString(0);
                    else
                        lbParent2.Text = dataRd.GetString(0);

                    count++;
                }
            }

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
    private bool IsAdmin()
    {

        ArrayList arrUsernames = null;

        if (Application["ADMIN_USERNAMES"] != null)
        {
            arrUsernames = (ArrayList)Application["ADMIN_USERNAMES"];
            if (arrUsernames != null && arrUsernames.Contains(LogOnUser))
                return true;
            else
                return false;
        }
        else
        {

            return false;
        }
    }

    private void BindGridData()
    {
        this.BindGridData(-1);
    }

    private void BindGridData( int iKidRefNo)
    {
        DataManager dtMgr = new DataManager();

        OracleConnection oraConn = null;
        DBUtil db = new DBUtil();

        try
        {
            DateTime dtNextFriday = new Manager().GetNextMonday().AddDays(4);

            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

            OracleDataReader dataRd = null;
            if (IsAdmin() && iKidRefNo > 0) {

                this.lbRefNo.Visible = true;
                this.lbRefNo.Text = "&nbsp;" + iKidRefNo; 
                dataRd = dtMgr.GetBookingData(DateTime.Now.Date.ToString("d"), dtNextFriday.ToString("d"), iKidRefNo, oraConn, true);
            }
            else if (IsAdmin())
            {
                this.lbRefNo.Visible = false;
                dataRd = dtMgr.GetBookingData(DateTime.Now.Date.ToString("d"), dtNextFriday.ToString("d"), oraConn, true);
            }
            else
            {
                this.lbRefNo.Visible = false;
                dataRd = dtMgr.GetBookingData(DateTime.Now.Date.ToString("d"), dtNextFriday.ToString("d"), this.LogOnUser, oraConn, true);
            }

            DataTable dt = new DataTable();

            dt.Load( dataRd );

            Session["DATA"] = dt;
            grdViewKidsAtd.DataSource = dt;
            grdViewKidsAtd.DataBind();
        }
        catch (Exception ex)
        {
            EventLogUtil.Log(ex.Message);
        }
        finally{
        
            db.CloseConnection( oraConn );
        }
    }


    public System.Drawing.Color BookingColor(string bookingType, string statusCode)
    {

        int iStatusCode = Convert.ToInt32( statusCode );


        if (2 ==iStatusCode)
            return System.Drawing.Color.Purple;
        else
        {
            if ("TENTATIVE".Equals(bookingType))
                return System.Drawing.Color.Red;
            else
                return System.Drawing.Color.Green;
        }
    }


    public string BookingType(string val, string slot, string kidCategory, string statusCode)
    {
        int iStatus = Convert.ToInt32(statusCode);

        if (iStatus == 2)
        {
            return "Late Booking";
        }
        else
        {
            if ("TENTATIVE".Equals(val))
            {
                int iSlot = Convert.ToInt32(slot);
                if ("ELDER".Equals(kidCategory))
                {
                    return ("Tentative (" + (iSlot - elderCount).ToString() + ")");
                }
                else
                {
                    return ("Tentative (" + (iSlot - toddlerCount).ToString() + ")");
                }
            }
            else
                return "Fixed";
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

    protected void grdViewKidsAtd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataManager dm = new DataManager();

        if (e.RowIndex >= 0)
        {
            GridViewRow row = (GridViewRow)grdViewKidsAtd.Rows[e.RowIndex];

            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lbKidRefNo = grdViewKidsAtd.Rows[e.RowIndex].FindControl("lbKidRef") as Label;
                Label lbUnixDate = grdViewKidsAtd.Rows[e.RowIndex].FindControl("lbUnixDate") as Label;
                TableCell cellNameTag = grdViewKidsAtd.Rows[e.RowIndex].Cells[3];
                TableCell cellDate = grdViewKidsAtd.Rows[e.RowIndex].Cells[4];
                TableCell cellSession =   grdViewKidsAtd.Rows[e.RowIndex].Cells[5];
                
                string nextKidNameTag = string.Empty;
                string nextKidParentName = string.Empty;
                string nextKidParentEmail = string.Empty;

                bool bVal = new DataManager().RemoveKidsBookings(lbKidRefNo.Text, Convert.ToInt32(lbUnixDate.Text), cellSession.Text, LogOnUser, ref nextKidNameTag, ref nextKidParentName, ref nextKidParentEmail);

                if (bVal)
                {
                    lbInfo.Visible = true;
                    lbInfo.ForeColor = System.Drawing.Color.Green;
                    lbInfo.Text = "Successfully Deleted! ";

                    Manager  mgr = new Manager();
                    ////mgr.SendDeleteEMailToCommitee(cellNameTag.Text, cellDate.Text, cellSession.Text, LogOnUser);
                    //////if (!string.IsNullOrEmpty(nextKidParentEmail) && !"NULL".Equals(nextKidParentEmail.ToUpper() ) )
                        //mgr.SendDeleteEMailToNextParent(nextKidNameTag, nextKidParentName, nextKidParentEmail, cellDate.Text, cellSession.Text, LogOnUser, cellNameTag.Text);
                }
                else
                {

                    lbInfo.Visible = true;
                    lbInfo.ForeColor = System.Drawing.Color.Red;
                    lbInfo.Text = "Failed to Delete! ";
                }

                if (IsAdmin()) {

                    int iKidRef = Convert.ToInt32(drpKidName.SelectedValue);

                    DataManager dtMgr = new DataManager();

                    if (iKidRef > 0)
                    {
                        BindGridData(iKidRef);
                    }
                    else {

                        BindGridData();
                    }
                }
                else
                {
                    BindGridData();
                }
            }
        }
    }

    protected void grdViewKidsAtd_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void grdViewKidsAtd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageSize = grdViewKidsAtd.PageSize;
        int newPageIndex = e.NewPageIndex;

        DataTable table = (DataTable)Session["DATA"];

        grdViewKidsAtd.PageIndex = newPageIndex;
        grdViewKidsAtd.DataSource = table;
        grdViewKidsAtd.DataBind();
    }

    protected void drpKidName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IsAdmin())
        {
            int iKidRef = Convert.ToInt32( drpKidName.SelectedValue );

            DataManager dtMgr = new DataManager();
            DateTime dtNextFriday = new Manager().GetNextMonday().AddDays(4);

            if (iKidRef > 0)
            {
                LoadParentsName(iKidRef);
                BindGridData(iKidRef);
            }
            else {

                BindGridData();
            }
        }
    }

    /*get points by kids name*/
    /*protected void drpPoints_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iKidRef = 1;// Convert.ToInt32(drpKidName2.SelectedValue);

        OracleConnection oraConn = null;
        DBUtil db = new DBUtil();


        try
        {
            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

            string sqlStatement = "select t.kid_name,t.points from KIDS_POINTS t where t.kid_name =" +
                " (select t2.name_tag from KIDS_INFO_TAB t2 where t2.ref_no =:refno )";

            OracleParameter pRefId = new OracleParameter("refno", OracleDbType.Int32);
            pRefId.Value = iKidRef;

            OracleParameter[] paramArray = { pRefId };


            OracleDataReader dataRd = new DBUtil().GetExecuteQuery(sqlStatement, oraConn, paramArray);
        
            dataRd.Read();

            //kidPoints.Text = dataRd.GetString(0);
            //kidPoints.Text = Convert.ToString( dataRd.GetInt32(1));
            //kidPoints.Visible = true;
        
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
    }*/

    protected void btExport_Click(object sender, EventArgs e)
    {
        Manager mgr = new Manager();

        fromDateExtender.SelectedDate = Convert.ToDateTime( string.IsNullOrEmpty( txtFromDate.Text) ? DateTime.Now.ToString() : txtFromDate.Text);
        DateTime fromDate = Convert.ToDateTime(fromDateExtender.SelectedDate).Date; //mgr.GetThisMonday().Date;
      
        //if (mgr.IsBlockDate()) {

        //    fromDate = mgr.GetNextMonday().Date;
        //}

        DateTime dtThisMon = mgr.GetMondayByDate( fromDate );
        DateTime toDate = dtThisMon.AddDays(4);

       

        if (!this.IsAdmin())
            fromDate = DateTime.Now.Date; // Only shows Data from Today.

        DataTable table = new Manager().GetGenaratedDataTable(dtThisMon, fromDate, toDate, true);

        try
        {
            string exportFilePath = SysParamReaderUtil.GetSysParamByName("EXPORT_FILE_PATH");
            exportFilePath = exportFilePath + @"\IFSJuniors" + DateTime.Now.ToString("d").Replace("/", "") + ".txt";

            mgr.WriteGridData(table, exportFilePath);
            lbInfo.Text = "Successfully Exported to " + exportFilePath;
            lbInfo.Visible = true;

            string exportEmaiTo = SysParamReaderUtil.GetSysParamByName("EXPORT_EMAIL_TO"); 
            string exportEmailCC = SysParamReaderUtil.GetSysParamByName("EXPORT_EMAIL_CC");

            MailUtil.SendAttachmentsMail("IFSJuniors-Report@ifsworld.com", exportEmaiTo, exportEmailCC, "IFS Juniors Attendance Overview", "IFS Juniors Attendance Overview from " + fromDate + " to " + toDate, exportFilePath);
        }
        catch (Exception ex) {

            EventLogUtil.Log(ex.Message);
            lbInfo.Text = ex.Message;
            lbInfo.Visible = true;
        }
    }
    protected void grdViewKidsAtd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow) {

        //    Label lbVal =(Label) e.Row.Cells[6].FindControl("lbGridBookigType");

        //    if (lbVal.Text == "TENTATIVE")
        //    {
        //        // e.Row.Cells[7].CssClass = "tenalt";
        //        e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
        //    }
        //    else if (lbVal.Text == "FIXED") {

        //        e.Row.Cells[7].ForeColor = System.Drawing.Color.Green;
        //    }
        
        //}
    }
}