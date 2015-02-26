﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Client;
using IFS.CoS.ServiceUtil;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Drawing;


public partial class Bookings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        msg_no_account.Visible = false;
        msg_no_connection.Visible = false;
        msg_error.Visible = false;
        msg_not_parent.Visible = false;


        if (!Page.IsPostBack)
        {

            if (Session["LOGON_AS"] == null || string.IsNullOrEmpty(Session["LOGON_AS"].ToString()))
                Session["LOGON_AS"] = Session["LOGON_USER"];

            DataManager dtMgr = new DataManager();

            if (!dtMgr.CheckExistDB(ServiceUtil.DB.DefaultDB))
            {
                msg_no_connection.Visible = true;
                return;
            }
            else
            {
                msg_no_connection.Visible = false;
            }

            DBUtil db = new DBUtil();
            OracleConnection oraConn = null;

            try
            {
                oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
                string userName = LogOnUser;
                Session["LOGON_AS"] = userName;
                drpKidName.Items.Clear();
                drpKidName.Items.Add(new ListItem("SELECT NAME", "0"));
                LoadKidsName(oraConn, userName);
            }
            catch (Exception ex)
            {
                EventLogUtil.Log(ex.Message);
            }
            Label1.Text = ""+drpKidName.SelectedValue+"";
            string command = "SELECT DATE_BOOKED,SESSION_BOOKED,ACTIVITY_USERNAME,BOOKING_STATE,ATTENDANCE FROM KIDS_ATTENDANCE_TAB WHERE KID_REF_NO ="+drpKidName.SelectedValue+" ORDER BY DATE_BOOKED DESC";
            GRID.DataSource = new DataManager().getkidlist_table(command);
            GRID.DataBind();
           
            //SELECT DATE_BOOKED,SESSION_BOOKED,ACTIVITY_USERNAME,BOOKING_STATE,ATTENDANCE FROM KIDS_ATTENDANCE_TAB WHERE KID_REF_NO = 1 ORDER BY DATE_BOOKED DESC;



        }

    }

    protected void drpKidName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpKidName.SelectedValue == "0")
        {
            
        }
        else
        {
 
            UpdatePanel1.Update();
        }
    }

    
    private void LoadKidsName(OracleConnection oraConn, string guardianUserName)
    {
        OracleDataReader dataRd = new DataManager().GetKidNamesByGuardianUserName(guardianUserName, oraConn);
        int count = 0;
        while (dataRd != null && dataRd.Read())
        {

            if (!dataRd.IsDBNull(0))
            {
                drpKidName.Items.Add(new ListItem(dataRd.GetString(0), Convert.ToString(dataRd.GetInt32(1))));
                count++;
            }
        }
        if (count > 0)
        {
            drpKidName.SelectedIndex = 1;

        }
        else
        {
            drpKidName.SelectedIndex = 0;
            
        }

        if (dataRd != null)
            dataRd.Close();
    }

    public string LogOnUser
    {
        get
        {
            return new Manager().GetLogOnUser(this.Page);
        }
    }
}