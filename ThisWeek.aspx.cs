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
        Manager dm = new Manager();

        msg_no_account.Visible = false;
        msg_no_connection.Visible = false;
        msg_error.Visible = false;
        msg_not_parent.Visible = false;
        msg_selectnames.Visible = true;
        msg_booking_success.Visible = false;
        msg_booking_exists.Visible = false;

        msg_home.Text = SysParamReaderUtil.GetSysParamByName("NOTIFICATION_HOME");

        if (!Page.IsPostBack)
        {        
            
            drpMon.Visible = false;
            drpTue.Visible = false;
            drpWed.Visible = false;
            drpThu.Visible = false;
            drpFri.Visible = false;


            DateTime dtThisMon = new Manager().GetThisMonday();
            DateTime dtNextMon = new Manager().GetNextMonday();

            lbMon.Text = dtThisMon.ToString("m") + "<br/>Monday";
            lbTue.Text = dtThisMon.AddDays(1).ToString("m") + "<br/>Tuesday";
            lbWed.Text = dtThisMon.AddDays(2).ToString("m") + "<br/>Wednesday";
            lbThu.Text = dtThisMon.AddDays(3).ToString("m") + "<br/>Thursday";
            lbFri.Text = dtThisMon.AddDays(4).ToString("m") + "<br/>Friday";

            Label1m.Text = lbMon.Text;
            Label2m.Text = lbTue.Text;
            Label3m.Text = lbWed.Text;
            Label4m.Text = lbThu.Text;
            Label5m.Text = lbFri.Text;

            Label1e.Text = lbMon.Text;
            Label2e.Text = lbTue.Text;
            Label3e.Text = lbWed.Text;
            Label4e.Text = lbThu.Text;
            Label5e.Text = lbFri.Text;

            //Thisweektab.Text = " This week ( " + dtThisMon.ToString("MMM dd") + " to " + dtThisMon.AddDays(4).ToString("MMM dd") + " )";
            //Nextweektab.Text = " Next week ( " + dtNextMon.ToString("MMM dd") + " to " + dtNextMon.AddDays(4).ToString("MMM dd") + " )";

            Thisweektab.Text = " <span class=\"glyphicon glyphicon-calendar\" aria-hidden=\"true\"></span><span style=\"font-size:18px\"> This week </span><br/><span style=\"font-size:12px\">" + dtThisMon.ToString("MMM dd") + " to " + dtThisMon.AddDays(4).ToString("MMM dd") + "</span> ";
            Nextweektab.Text = " <span class=\"glyphicon glyphicon-calendar\" aria-hidden=\"true\"></span><span style=\"font-size:18px;color:#873e8d\"> Next week </span><br/><span style=\"font-size:12px;color:#873e8d\">" + dtNextMon.ToString("MMM dd") + " to " + dtNextMon.AddDays(4).ToString("MMM dd") + " </span>";


            EnableChecK();

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


            OracleConnection oraConn = null;
            DBUtil db = new DBUtil();

            GenaratedResultsDataTable();

            try
            {
                oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

                LoadGuardiansUserNames(oraConn);
                drpGuardian.SelectedValue = LogOnUser;
                LoadKidsName(oraConn, LogOnUser);
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

    }

    private void GenaratedResultsDataTable()
    {
        int ElderCount = 0, ToddlerCount = 0,ElderfixCount = 0, ToddlerfixCount = 0, Total = 0;

        DataManager mng = new DataManager();

        Manager mngr = new Manager();

        DateTime thismonday = mngr.GetThisMonday();
        DateTime thistuesday = thismonday.AddDays(1);
        DateTime thiswednesday = thismonday.AddDays(2);
        DateTime thisthursday = thismonday.AddDays(3);
        DateTime thisfriday = thismonday.AddDays(4);


        Mondaytgrid_m.DataSource = mng.getkidlist_table(thismonday, "AM", "TODDLER");
        Mondaytgrid_m.DataBind();

        Mondayegrid_m.DataSource = mng.getkidlist_table(thismonday, "AM", "ELDER");
        Mondayegrid_m.DataBind();


        Tuesdaytgrid_m.DataSource = mng.getkidlist_table(thistuesday, "AM", "TODDLER");
        Tuesdaytgrid_m.DataBind();

        Tuesdayegrid_m.DataSource = mng.getkidlist_table(thistuesday, "AM", "ELDER");
        Tuesdayegrid_m.DataBind();


        Wednesdaytgrid_m.DataSource = mng.getkidlist_table(thiswednesday, "AM", "TODDLER");
        Wednesdaytgrid_m.DataBind();

        Wednesdayegrid_m.DataSource = mng.getkidlist_table(thiswednesday, "AM", "ELDER");
        Wednesdayegrid_m.DataBind();


        Thursdaytgrid_m.DataSource = mng.getkidlist_table(thisthursday, "AM", "TODDLER");
        Thursdaytgrid_m.DataBind();

        Thursdayegrid_m.DataSource = mng.getkidlist_table(thisthursday, "AM", "ELDER");
        Thursdayegrid_m.DataBind();


        Fridaytgrid_m.DataSource = mng.getkidlist_table(thisfriday, "AM", "TODDLER");
        Fridaytgrid_m.DataBind();

        Fridayegrid_m.DataSource = mng.getkidlist_table(thisfriday, "AM", "ELDER");
        Fridayegrid_m.DataBind();


        /*evening sessoin*/
        mondaytgrid_e.DataSource = mng.getkidlist_table(thismonday, "PM", "TODDLER");
        mondaytgrid_e.DataBind();

        tuesdaytgrid_e.DataSource = mng.getkidlist_table(thistuesday, "PM", "TODDLER");
        tuesdaytgrid_e.DataBind();

        wednesdaytgrid_e.DataSource = mng.getkidlist_table(thiswednesday, "PM", "TODDLER");
        wednesdaytgrid_e.DataBind();

        thursdaytgrid_e.DataSource = mng.getkidlist_table(thisthursday, "PM", "TODDLER");
        thursdaytgrid_e.DataBind();

        fridaytgrid_e.DataSource = mng.getkidlist_table(thisfriday, "PM", "TODDLER");
        fridaytgrid_e.DataBind();

        mondayegrid_e.DataSource = mng.getkidlist_table(thismonday, "PM", "ELDER");
        mondayegrid_e.DataBind();

        tuesdayegrid_e.DataSource = mng.getkidlist_table(thistuesday, "PM", "ELDER");
        tuesdayegrid_e.DataBind();

        wednesdayegrid_e.DataSource = mng.getkidlist_table(thiswednesday, "PM", "ELDER");
        wednesdayegrid_e.DataBind();

        thursdayegrid_e.DataSource = mng.getkidlist_table(thisthursday, "PM", "ELDER");
        thursdayegrid_e.DataBind();

        fridayegrid_e.DataSource = mng.getkidlist_table(thisfriday, "PM", "ELDER");
        fridayegrid_e.DataBind();


        /*morning counts*/

        ToddlerCount = mng.getkidlist_count(thismonday, "AM", "TODDLER");
        ElderCount = mng.getkidlist_count(thismonday, "AM", "ELDER");
        Total = ToddlerCount + ElderCount;
        ToddlerfixCount = mng.getkidlist_fixed_count(thismonday, "AM", "TODDLER");
        ElderfixCount = mng.getkidlist_fixed_count(thismonday, "AM", "ELDER");

        aa_m.Text = Convert.ToString(Total);
        aat_m.Text = Convert.ToString(ToddlerCount);
        aae_m.Text = Convert.ToString(ElderCount);

        setbookingcolors(Mondaytgrid_m,ElderCount, ToddlerCount,ElderfixCount,ToddlerfixCount, 0);
        setbookingcolors(Mondayegrid_m, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 1);


        ToddlerCount = mng.getkidlist_count(thistuesday, "AM", "TODDLER");
        ElderCount = mng.getkidlist_count(thistuesday, "AM", "ELDER");
        Total = ToddlerCount + ElderCount;

        ToddlerfixCount = mng.getkidlist_fixed_count(thistuesday, "AM", "TODDLER");
        ElderfixCount = mng.getkidlist_fixed_count(thistuesday, "AM", "ELDER");

        bb_m.Text = Convert.ToString(Total);
        bbt_m.Text = Convert.ToString(ToddlerCount);
        bbe_m.Text = Convert.ToString(ElderCount);

        setbookingcolors(Tuesdaytgrid_m, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 0);
        setbookingcolors(Tuesdayegrid_m, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 1);



        ToddlerCount = mng.getkidlist_count(thiswednesday, "AM", "TODDLER");
        ElderCount = mng.getkidlist_count(thiswednesday, "AM", "ELDER");
        Total = ToddlerCount + ElderCount;

        ToddlerfixCount = mng.getkidlist_fixed_count(thiswednesday, "AM", "TODDLER");
        ElderfixCount = mng.getkidlist_fixed_count(thiswednesday, "AM", "ELDER");

        cc_m.Text = Convert.ToString(Total);
        cct_m.Text = Convert.ToString(ToddlerCount);
        cce_m.Text = Convert.ToString(ElderCount);

        setbookingcolors(Wednesdaytgrid_m, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 0);
        setbookingcolors(Wednesdayegrid_m, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 1);




        ToddlerCount = mng.getkidlist_count(thisthursday, "AM", "TODDLER");
        ElderCount = mng.getkidlist_count(thisthursday, "AM", "ELDER");
        Total = ToddlerCount + ElderCount;

        ToddlerfixCount = mng.getkidlist_fixed_count(thisthursday, "AM", "TODDLER");
        ElderfixCount = mng.getkidlist_fixed_count(thisthursday, "AM", "ELDER");

        dd_m.Text = Convert.ToString(Total);
        ddt_m.Text = Convert.ToString(ToddlerCount);
        dde_m.Text = Convert.ToString(ElderCount);

        setbookingcolors(Thursdaytgrid_m, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 0);
        setbookingcolors(Thursdayegrid_m, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 1);



        ToddlerCount = mng.getkidlist_count(thisfriday, "AM", "TODDLER");
        ElderCount = mng.getkidlist_count(thisfriday, "AM", "ELDER");
        Total = ToddlerCount + ElderCount;

        ToddlerfixCount = mng.getkidlist_fixed_count(thisfriday, "AM", "TODDLER");
        ElderfixCount = mng.getkidlist_fixed_count(thisfriday, "AM", "ELDER");

        ee_m.Text = Convert.ToString(Total);
        eet_m.Text = Convert.ToString(ToddlerCount);
        eee_m.Text = Convert.ToString(ElderCount);

        setbookingcolors(Fridaytgrid_m, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 0);
        setbookingcolors(Fridayegrid_m, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 1);



        /*evening counts*/
        ToddlerCount = mng.getkidlist_count(thismonday, "PM", "TODDLER");
        ElderCount = mng.getkidlist_count(thismonday, "PM", "ELDER");
        Total = ToddlerCount + ElderCount;

        ToddlerfixCount = mng.getkidlist_fixed_count(thismonday, "PM", "TODDLER");
        ElderfixCount = mng.getkidlist_fixed_count(thismonday, "PM", "ELDER");

        aa_e.Text = Convert.ToString(Total);
        aat_e.Text = Convert.ToString(ToddlerCount);
        aae_e.Text = Convert.ToString(ElderCount);

        setbookingcolors(mondaytgrid_e, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 0);
        setbookingcolors(mondayegrid_e, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 1);



        ToddlerCount = mng.getkidlist_count(thistuesday, "PM", "TODDLER");
        ElderCount = mng.getkidlist_count(thistuesday, "PM", "ELDER");
        Total = ToddlerCount + ElderCount;

        ToddlerfixCount = mng.getkidlist_fixed_count(thistuesday, "PM", "TODDLER");
        ElderfixCount = mng.getkidlist_fixed_count(thistuesday, "PM", "ELDER");

        bb_e.Text = Convert.ToString(Total);
        bbt_e.Text = Convert.ToString(ToddlerCount);
        bbe_e.Text = Convert.ToString(ElderCount);

        setbookingcolors(tuesdaytgrid_e, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 0);
        setbookingcolors(tuesdayegrid_e, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 1);


        ToddlerCount = mng.getkidlist_count(thiswednesday, "PM", "TODDLER");
        ElderCount = mng.getkidlist_count(thiswednesday, "PM", "ELDER");
        Total = ToddlerCount + ElderCount;

        ToddlerfixCount = mng.getkidlist_fixed_count(thiswednesday, "PM", "TODDLER");
        ElderfixCount = mng.getkidlist_fixed_count(thiswednesday, "PM", "ELDER");

        cc_e.Text = Convert.ToString(Total);
        cct_e.Text = Convert.ToString(ToddlerCount);
        cce_e.Text = Convert.ToString(ElderCount);

        setbookingcolors(wednesdaytgrid_e, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 0);
        setbookingcolors(wednesdayegrid_e, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 1);



        ToddlerCount = mng.getkidlist_count(thisthursday, "PM", "TODDLER");
        ElderCount = mng.getkidlist_count(thisthursday, "PM", "ELDER");
        Total = ToddlerCount + ElderCount;

        ToddlerfixCount = mng.getkidlist_fixed_count(thisthursday, "PM", "TODDLER");
        ElderfixCount = mng.getkidlist_fixed_count(thisthursday, "PM", "ELDER");

        dd_e.Text = Convert.ToString(Total);
        ddt_e.Text = Convert.ToString(ToddlerCount);
        dde_e.Text = Convert.ToString(ElderCount);

        setbookingcolors(thursdaytgrid_e, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 0);
        setbookingcolors(thursdayegrid_e, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount, 1);



        ToddlerCount = mng.getkidlist_count(thisfriday, "PM", "TODDLER");
        ElderCount = mng.getkidlist_count(thisfriday, "PM", "ELDER");
        Total = ToddlerCount + ElderCount;

        ToddlerfixCount = mng.getkidlist_fixed_count(thisfriday, "PM", "TODDLER");
        ElderfixCount = mng.getkidlist_fixed_count(thisfriday, "PM", "ELDER");

        ee_e.Text = Convert.ToString(Total);
        eet_e.Text = Convert.ToString(ToddlerCount);
        eee_e.Text = Convert.ToString(ElderCount);

        setbookingcolors(fridaytgrid_e, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount,0);
        setbookingcolors(fridayegrid_e, ElderCount, ToddlerCount, ElderfixCount, ToddlerfixCount,1);

    }

    /* Set booking colours to kid list */
    public void setbookingcolors(GridView grid, int eldercount, int toddlercount, int elderfixcount, int toddlerfixcount,int iselder)
    {

        int toddler_limit = Convert.ToInt32(SysParamReaderUtil.GetSysParamByName("TODDLERS_FIXED_SLOTS"));
        int elder_limit = Convert.ToInt32(SysParamReaderUtil.GetSysParamByName("ELDERS_FIXED_SLOTS"));
        int tentative_limit = Convert.ToInt32(SysParamReaderUtil.GetSysParamByName("TENTATIVE_SLOTS"));
        int i;

        DataManager mng=new DataManager();
        
        if (iselder == 1)
        {
            int tods;
            if (toddlercount > toddler_limit)
            {
                tods = toddler_limit;
            }
            else
            {
                tods = toddlercount;
            }

            for (i = 0; i < eldercount; i++)
            {
                
                if (i < elderfixcount)
                {
                    grid.Rows[i].ForeColor = Color.DarkGreen;
                }
                else if (i < elder_limit - tentative_limit)
                {
                    grid.Rows[i].ForeColor = Color.Black;
                }
                else if (i < elder_limit + toddler_limit - tods)
                {
                    grid.Rows[i].ForeColor = Color.DarkOrange;
                }
                else
                {
                    grid.Rows[i].ForeColor = Color.DarkRed;
                }

            }
        }

        else
        {
            for (i = 0; i < toddlercount; i++)
            {

                if (i < toddlerfixcount)
                {
                    grid.Rows[i].ForeColor = Color.Green;
                }
                else if (i < toddler_limit - tentative_limit)
                {
                    grid.Rows[i].ForeColor = Color.Black;
                }
                else if (i < toddler_limit)
                {
                    grid.Rows[i].ForeColor = Color.DarkOrange;
                }
                else
                {
                    grid.Rows[i].ForeColor = Color.DarkRed;
                }

            }

        }

       

    }

    protected void chkMon_CheckedChanged(object sender, EventArgs e)
    {
        if (chkMon.Checked)
        {
            this.drpMon.Enabled = true;
            this.drpMon.Visible = true;
        }
        else
        {
            this.drpMon.Visible = false;
            this.drpMon.Enabled = false;
        }
    }

    protected void chkTue_CheckedChanged(object sender, EventArgs e)
    {
        if (chkTue.Checked)
        {
            this.drpTue.Enabled = true;
            this.drpTue.Visible = true;
        }
        else
        {
            this.drpTue.Visible = false;
            this.drpTue.Enabled = false;
        }
    }

    protected void chkWed_CheckedChanged(object sender, EventArgs e)
    {
        if (chkWed.Checked)
        {
            this.drpWed.Enabled = true;
            this.drpWed.Visible = true;
        }
        else
        {
            this.drpWed.Visible = false;
            this.drpWed.Enabled = false;
        }
    }

    protected void chkThu_CheckedChanged(object sender, EventArgs e)
    {
        if (chkThu.Checked)
        {
            this.drpThu.Enabled = true;
            this.drpThu.Visible = true;
        }
        else
        {
            this.drpThu.Visible = false;
            this.drpThu.Enabled = false;
        }
    }

    protected void chkFri_CheckedChanged(object sender, EventArgs e)
    {
        if (chkFri.Checked)
        {
            this.drpFri.Enabled = true;
            this.drpFri.Visible = true;
        }
        else
        {
            this.drpFri.Visible = false;
            this.drpFri.Enabled = false;
        }

    }

    protected void drpKidName_SelectedIndexChanged(object sender, EventArgs e)
    {

        msg_no_connection.Visible = false;
        //lbInfo.Visible = false;

        if (drpKidName.SelectedValue == "0")
        {
            DisableCheckDrop();
            ClearCheckDrop();
            btnAdd.Enabled = false;
           
            msg_selectnames.Visible = true;
        }
        else
        {
            EnableChecK();
            ClearCheckDrop();
            btnAdd.Enabled = true;
            msg_selectnames.Visible = false;
        }
    }

    protected void drpGuardian_SelectedIndexChanged(object sender, EventArgs e)
    {
        DBUtil db = new DBUtil();
        OracleConnection oraConn = null;

        try
        {
            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
            string userName = drpGuardian.SelectedValue.ToString();
            Session["LOGON_AS"] = userName;
            drpKidName.Items.Clear();
            drpKidName.Items.Add(new ListItem("SELECT NAME", "0"));
            LoadKidsName(oraConn, userName);
        }
        catch (Exception ex)
        {
            EventLogUtil.Log(ex.Message);
        }
    }

    private void DisableCheckDrop()
    {


        chkMon.Enabled = false;
        drpMon.Enabled = false;

        chkTue.Enabled = false;
        drpTue.Enabled = false;

        chkWed.Enabled = false;
        drpWed.Enabled = false;

        chkThu.Enabled = false;
        drpThu.Enabled = false;

        chkFri.Enabled = false;
        drpFri.Enabled = false;

    }

    private void ClearCheckDrop()
    {

        chkMon.Checked = false;
        drpMon.SelectedIndex = 0;
        drpMon.Enabled = false;
        drpMon.Visible = false;

        chkTue.Checked = false;
        drpTue.SelectedIndex = 0;
        drpTue.Enabled = false;
        drpTue.Visible = false;

        chkWed.Checked = false;
        drpWed.SelectedIndex = 0;
        drpWed.Enabled = false;
        drpWed.Visible = false;

        chkThu.Checked = false;
        drpThu.SelectedIndex = 0;
        drpThu.Enabled = false;
        drpThu.Visible = false;

        chkFri.Checked = false;
        drpFri.SelectedIndex = 0;
        drpFri.Enabled = false;
        drpFri.Visible = false;
    }

    private void LoadKidsName(OracleConnection oraConn, string guardianUserName)
    {
        btnAdd.Enabled = false;
        

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
            btnAdd.Enabled = true;
            
            EnableChecK();
            ClearCheckDrop();
            //f.Visible = true;
            msg_selectnames.Visible = false;
        }
        else
        {
            drpKidName.SelectedIndex = 0;
            btnAdd.Enabled = false;
           
            DisableCheckDrop();
            ClearCheckDrop();
            //booking_table.Visible = false;
            msg_selectnames.Visible = true;
        }

        if (dataRd != null)
            dataRd.Close();
    }

    private void EnableChecK()
    {

        Manager mgr = new Manager();

        DateTime dtNextMon = new Manager().GetThisMonday();

        int unixMon = (int)mgr.ConvertToUnixTimestamp(dtNextMon.Date);
        int unixTue = (int)mgr.ConvertToUnixTimestamp(dtNextMon.Date.AddDays(1));
        int unixWed = (int)mgr.ConvertToUnixTimestamp(dtNextMon.Date.AddDays(2));
        int unixThu = (int)mgr.ConvertToUnixTimestamp(dtNextMon.Date.AddDays(3));
        int unixFri = (int)mgr.ConvertToUnixTimestamp(dtNextMon.Date.AddDays(4));

        int unixToday = (int)mgr.ConvertToUnixTimestamp(DateTime.Now); 

        if (mgr.IsHoliday(unixMon))
        {
            drpMon.Visible = false;
            mon_panel.Attributes["class"] = "panel panel-default";
            chkMon.Text = "&nbsp;&nbsp;<span class=\"label label-warning\"> Holiday </span>";
        }
        else
        {
            if (unixToday < unixMon)
            {
                chkMon.Enabled = true;
                chkMon.Text = "&nbsp;&nbsp;<span class=\"label label-primary\"> Select to Book </span>";
            }
            else
                chkMon.Enabled = false;
        }

        if (mgr.IsHoliday(unixTue))
        {
            drpTue.Visible = false;
            tue_panel.Attributes["class"] = "panel panel-default";
            chkTue.Text = "&nbsp;&nbsp;<span class=\"label label-warning\"> Holiday </span>";
        }
        else
        {
            if (unixToday < unixTue)
            {
                chkTue.Enabled = true;
                chkTue.Text = "&nbsp;&nbsp;<span class=\"label label-primary\"> Select to Book </span>";
            }
            else
                chkTue.Enabled = false;
        }

        if (mgr.IsHoliday(unixWed))
        {
            drpWed.Visible = false;
            wed_panel.Attributes["class"] = "panel panel-default";
            chkWed.Text = "&nbsp;&nbsp;<span class=\"label label-warning\"> Holiday </span>";
        }
        else
        {   if (unixToday < unixWed)
            {
            chkWed.Enabled = true;
            chkWed.Text = "&nbsp;&nbsp;<span class=\"label label-primary\"> Select to Book </span>";
            }
            else
                chkWed.Enabled = false;
        }

        if (mgr.IsHoliday(unixThu))
        {
            
            drpThu.Visible = false;
            thu_panel.Attributes["class"] = "panel panel-default";
            chkThu.Text = "&nbsp;&nbsp;<span class=\"label label-warning\"> Holiday </span>";
        }
        else
        {   if (unixToday < unixThu)
            {
            chkThu.Enabled = true;
            chkThu.Text = "&nbsp;&nbsp;<span class= \"label label-primary\"> Select to Book </span>";
            }
            else
                chkThu.Enabled = false;
        }

        if (mgr.IsHoliday(unixFri))
        {
            drpFri.Visible = false;
            fri_panel.Attributes["class"] = "panel panel-default";
            chkFri.Text = "&nbsp;&nbsp;<span class=\"label label-warning\"> Holiday </span>";
        }
        else
        {   if (unixToday < unixFri)
            {
            chkFri.Enabled = true;
            chkFri.Text = "&nbsp;&nbsp;<span class=\"label label-primary\"> Select to Book </span>";
            }
            else
            chkFri.Enabled = false;
        }

        //chkMon.Enabled = false;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataManager dtMgr = new DataManager();
        DBUtil db = new DBUtil();
        OracleConnection oraConn = null;
        string message = string.Empty;
        string messageSuccess = string.Empty;
        OracleTransaction atdTrans = null;

        try
        {
            ArrayList arrDates = GetCheckedDateList();
            oraConn = new DBUtil().OpenConnection(ServiceUtil.DB.DefaultDB);

            atdTrans = oraConn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

            foreach (KidAttendance val in arrDates)
            {

                if (!dtMgr.CheckExistForecastBooking(Convert.ToInt32(val.KidRefNo), val.DateBooked, val.SessionBooked, oraConn))
                {
                    dtMgr.AddAttendanceForecastDB(Convert.ToInt32(val.KidRefNo), val.DateBooked, val.SessionBooked, val.ActivityUserName, oraConn, atdTrans);
                    messageSuccess += "<br/> &nbsp;&nbsp;<span class=\"glyphicon glyphicon-ok\" aria-hidden=\"true\"></span>&nbsp;&nbsp;" + val.DateBooked.ToString("m") + "&nbsp;&nbsp;" + val.SessionBooked + ".";
                   
                    // increase the booking count
                    dtMgr.addcount_refno(val.KidRefNo, "BOOKINGS");
                    dtMgr.addcount_refno(val.KidRefNo, "ABSENT");
                }
                else
                {
                    message += "</br> &nbsp;&nbsp;<span class=\"glyphicon glyphicon-remove\" aria-hidden=\"true\"></span>&nbsp;&nbsp;" + val.DateBooked.ToString("m") + "&nbsp;&nbsp;" + val.SessionBooked + ".";
                }
            }

            atdTrans.Commit();

            if (!string.IsNullOrEmpty(message))
            {
                msg_selectnames.Visible = false;
                already_exist_msg.Text = " <span style=\"font-weight:bold\">The booking is already exist for following days</span> " + message;
                msg_booking_exists.Visible = true;

            }
            else
                msg_booking_exists.Visible = false;

            if (!string.IsNullOrEmpty(messageSuccess))
            {
                this.ClearCheckDrop();
                msg_selectnames.Visible = false;
                success_msg.Text = " <span style=\"font-weight:bold\"> The booking is successfully saved for following days</span>" + messageSuccess;
                msg_booking_success.Visible = true;
                
            }
            else
                msg_booking_success.Visible = false;


            GenaratedResultsDataTable();
            //Response.Redirect("~/Default.aspx");

        }
        catch (Exception ex)
        {
            atdTrans.Rollback();
            EventLogUtil.Log("Exception Add Forecast Data : " + ex.Message);
            db.CloseConnection(oraConn);

            //lbMsg.Text = "Error Occured. Please try again." + ex.Message;
            msg_error.Visible = true;
            //throw;
        }
    }

    private ArrayList GetCheckedDateList()
    {

        string refNo = drpKidName.SelectedValue;
        ArrayList arrDates = new ArrayList();

        DateTime dtNextMon = new Manager().GetThisMonday();

        LoadDataToArray(refNo, chkMon, drpMon, arrDates, Convert.ToDateTime(dtNextMon.ToString("d")));
        LoadDataToArray(refNo, chkTue, drpTue, arrDates, Convert.ToDateTime(dtNextMon.AddDays(1).ToString("d")));
        LoadDataToArray(refNo, chkWed, drpWed, arrDates, Convert.ToDateTime(dtNextMon.AddDays(2).ToString("d")));
        LoadDataToArray(refNo, chkThu, drpThu, arrDates, Convert.ToDateTime(dtNextMon.AddDays(3).ToString("d")));
        LoadDataToArray(refNo, chkFri, drpFri, arrDates, Convert.ToDateTime(dtNextMon.AddDays(4).ToString("d")));

        return arrDates;
    }

    private ArrayList LoadDataToArray(string refNo, CheckBox chkBox, DropDownList drpDown, ArrayList arrDates, DateTime date_)
    {
        if (chkBox.Checked)
        {
            string sVal = drpDown.SelectedValue;

            if ("FULL DAY".Equals(sVal.ToUpper()))
            {
                arrDates.Add(new KidAttendance(refNo, "AM", date_, 1, LogOnUser));
                arrDates.Add(new KidAttendance(refNo, "PM", date_, 1, LogOnUser));
            }
            else if ("AM".Equals(sVal.ToUpper()))
            {
                arrDates.Add(new KidAttendance(refNo, "AM", date_, 1, LogOnUser));
            }

            else if ("PM".Equals(sVal.ToUpper()))
            {
                arrDates.Add(new KidAttendance(refNo, "PM", date_, 1, LogOnUser));
            }
        }

        return arrDates;
    }

    public string LogOnUser
    {
        get
        {
            return new Manager().GetLogOnUser(this.Page);
        }
        //get
        //{
        //    if (Session["LOGON_USER"] != null && !string.IsNullOrEmpty(Session["LOGON_USER"].ToString()))
        //        return Session["LOGON_USER"].ToString();

        //    else
        //    {
        //        string Auth_User = Page.User.Identity.Name.ToUpper();
        //        string userName = string.Empty;

        //        if (!string.IsNullOrEmpty(Auth_User))
        //        {

        //            Auth_User = Auth_User.Replace("/", "\\");
        //            if (Auth_User.Contains("\\"))
        //                userName = Auth_User.Split(new Char[] { '\\' })[1];
        //            if (userName == null)
        //                userName = Auth_User;

        //            Session["LOGON_USER"] = userName.ToUpper();
        //        }
        //        return userName.ToUpper();
        //    }
        //}
    }

    private void LoadGuardiansUserNames(OracleConnection oraConn)
    {

        ArrayList arrUsernames = null;

        if (Application["GUARDIAN_USERNAMES"] != null)
        {
            arrUsernames = (ArrayList)Application["GUARDIAN_USERNAMES"];

            if (arrUsernames != null && arrUsernames.Count > 0)
            {
                foreach (string val in arrUsernames)
                {
                    drpGuardian.Items.Add(new ListItem(val));
                }
            }
            else
            {
                OracleDataReader dataRd = new DataManager().GetGuardianUserNameList(oraConn);

                while (dataRd != null && dataRd.Read())
                {

                    if (!dataRd.IsDBNull(0))
                    {
                        drpGuardian.Items.Add(new ListItem(dataRd.GetString(0)));
                        arrUsernames.Add(dataRd.GetString(0));
                    }
                }

                if (dataRd != null)
                    dataRd.Close();
            }
        }

        if (!new Manager().IsAdmin(LogOnUser) && (arrUsernames != null && !arrUsernames.Contains(LogOnUser)))
        {
            btnAdd.Enabled = false;
            
            //btnDelete.Enabled = false;
            drpGuardian.Enabled = false;
            drpKidName.Enabled = false;

            //lbMsg.Text = "You are not a registered parent/guardian of the IFS Juniors. Please contact IFS Juniors Committee to proceed with the registration.";
            msg_not_parent.Visible = true;
            //grdViewKidsAtd.Visible = false;
        }
    }
}