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

            loadKidsDetails();
        }

    }

    protected void drpKidName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpKidName.SelectedValue == "0")
        {
            
        }
        else
        {
            loadKidsDetails();
            UpdatePanel1.Update();
        }
    }

    private void loadKidsDetails() {

        String kidText = drpKidName.SelectedItem.Text;
         
        DataManager dtMgr = new DataManager();
        OracleConnection oraConn = null;
        DBUtil db = new DBUtil();

       try
        {
            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

            OracleDataReader dataRd = dtMgr.getkid_data(oraConn, kidText);

            dataRd.Read();

            if (!dataRd.IsDBNull(0))
            {
                //Label4.Text = Convert.ToString(dataRd.GetInt32(1));
                points.Text = Convert.ToString(dataRd.GetInt32(0));
                //points.Text = "55";
 
            }
          
            if (!dataRd.IsDBNull(1))
            {
                //Label6.Text = Convert.ToString(dataRd.GetInt32(2));
                bookings1.Text = Convert.ToString(dataRd.GetInt32(1));
            }
            if (!dataRd.IsDBNull(2))
            {
                //Label6.Text = Convert.ToString(dataRd.GetInt32(2));
                fixed1.Text = Convert.ToString(dataRd.GetInt32(2));
            }
            if (!dataRd.IsDBNull(3))
            {
                //Label8.Text = Convert.ToString(dataRd.GetInt32(3));
                present.Text = Convert.ToString(dataRd.GetInt32(3));
            }
            if (!dataRd.IsDBNull(4))
            {
                //Label10.Text = Convert.ToString(dataRd.GetInt32(4));
                absent.Text = Convert.ToString(dataRd.GetInt32(4));
            }
            if (!dataRd.IsDBNull(5))
            {
                //Label10.Text = Convert.ToString(dataRd.GetInt32(4));
                cancelled.Text = Convert.ToString(dataRd.GetInt32(5));
            }
            if (!dataRd.IsDBNull(6))
            {
                //Label12.Text = Convert.ToString(dataRd.GetInt32(5));
                bulkcancels.Text = Convert.ToString(dataRd.GetInt32(6));
            }
        }
        catch (Exception ex)
        {
            EventLogUtil.Log("Default.aspx:PageLoad : " + ex.Message);
        }
        finally
        {

            db.CloseConnection(oraConn);
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