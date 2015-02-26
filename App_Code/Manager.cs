using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using IFS.CoS.ServiceUtil;
using System.Collections;
using System.Data;
using System.IO;

/// <summary>
/// Summary description for Manager
/// </summary>
public class Manager
{
	public Manager()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public double ConvertToUnixTimestamp(DateTime date)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        TimeSpan diff = date - origin;
        return Math.Floor(diff.TotalSeconds);
    }

    public DateTime ConvertFromUnixTimestamp(double timestamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return origin.AddSeconds(timestamp);
    }

    public DateTime GetNextMonday()
    {

        DateTime dToday = DateTime.Now;
        int dayOfWeek = (int)dToday.DayOfWeek;

        if (dToday.DayOfWeek == DayOfWeek.Saturday)
        {
            dToday = dToday.AddDays(1);
            dayOfWeek = (int)dToday.DayOfWeek;
        }

        DateTime nextMon = dToday.AddDays((1 - dayOfWeek) + 7);
        return nextMon;
    }

    public DateTime GetThisMonday()
    {

        DateTime dToday = DateTime.Now;
        int dayOfWeek = (int)dToday.DayOfWeek;

        if (dToday.DayOfWeek == DayOfWeek.Saturday)
        {
            dToday = dToday.AddDays(1);
            dayOfWeek = (int)dToday.DayOfWeek;
        }

        DateTime nextMon = dToday.AddDays((1 - dayOfWeek));
        return nextMon;
    }

    public DateTime GetMondayByDate( DateTime date_)
    {

        DateTime dToday = date_;
        int dayOfWeek = (int)dToday.DayOfWeek;

        if (dToday.DayOfWeek == DayOfWeek.Saturday)
        {
            dToday = dToday.AddDays(1);
            dayOfWeek = (int)dToday.DayOfWeek;
        }

        DateTime nextMon = dToday.AddDays((1 - dayOfWeek));
        return nextMon;
    }

    public bool IsAdmin(string LogOnUser)
    {

        ArrayList arrUsernames = null;

        if (HttpContext.Current.Application["ADMIN_USERNAMES"] != null)
        {
            arrUsernames = (ArrayList)HttpContext.Current.Application["ADMIN_USERNAMES"];

            /*for(string name in arrUsernames)*/
            //EventLogUtil.Log(LogOnUser);

            /*foreach (string st in arrUsernames)
            {
                EventLogUtil.Log(st);
            }*/

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

    public bool IsSuperAdmin(string LogOnUser)
    {

        ArrayList arrUsernames = null;

        if (HttpContext.Current.Application["SUPER_ADMIN_USERNAMES"] != null)
        {
            arrUsernames = (ArrayList)HttpContext.Current.Application["SUPER_ADMIN_USERNAMES"];

            /*foreach (string st in arrUsernames)
            {
                EventLogUtil.Log(st);
            }*/

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
   
    public bool IsBlockDate()
    {

        bool val = false;

        try
        {
            //int fromDateTime = Convert.ToInt32(WebConfiguration.getConfigValue("DisableFromDateTimeUnix"));
            //int toDateTime = Convert.ToInt32(WebConfiguration.getConfigValue("DisableToDateTimeUnix"));

            int fromDateTime = Convert.ToInt32(SysParamReaderUtil.GetSysParamByName("DISABLE_FROM_DATETIME_UNIX"));
            int toDateTime = Convert.ToInt32(SysParamReaderUtil.GetSysParamByName("DISABLE_TO_DATETIME_UNIX"));

            int sysdatetimeUnix = (int)new Manager().ConvertToUnixTimestamp(DateTime.Now);

            if (sysdatetimeUnix >= fromDateTime && sysdatetimeUnix <= toDateTime)
                return true;
        }
        catch (Exception ex)
        {

            EventLogUtil.Log(ex.Message);
        }

        if ("TRUE".Equals(SysParamReaderUtil.GetSysParamByName("ENABLE_SCHEDULE_BLOCK")))
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }

             if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                string friStartTime = SysParamReaderUtil.GetSysParamByName("FRIDAY_START_TIME");
                string[] tmp = friStartTime.Split(new char[] { ':' });

                TimeSpan startTime = new TimeSpan(Convert.ToInt32(tmp[0]), Convert.ToInt32(tmp[1]), 0);
                TimeSpan endTime = new TimeSpan(23, 59, 59);
                DateTime time = DateTime.Now;


                if (endTime == startTime)
                {
                    return true;
                }
                else if (endTime < startTime)
                {
                    val = time.TimeOfDay <= endTime ||
                        time.TimeOfDay >= startTime;
                }
                else
                {
                    val = time.TimeOfDay >= startTime &&
                        time.TimeOfDay <= endTime;
                }
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                string monEndTime = SysParamReaderUtil.GetSysParamByName("MONDAY_END_TIME");
                string[] tmp = monEndTime.Split(new char[] { ':' });

                TimeSpan startTime = new TimeSpan(00, 00, 0);
                TimeSpan endTime = new TimeSpan(Convert.ToInt32(tmp[0]), Convert.ToInt32(tmp[1]), 0);
                DateTime time = DateTime.Now;

                if (endTime == startTime)
                {
                    return true;
                }
                else if (endTime < startTime)
                {
                    val = time.TimeOfDay <= endTime ||
                        time.TimeOfDay >= startTime;
                }
                else
                {
                    val = time.TimeOfDay >= startTime &&
                        time.TimeOfDay <= endTime;
                }
            }
        }

        return val;
    }

    public DataTable GetGenaratedDataTable( DateTime dtNextMon , DateTime fromDate, DateTime toDate, bool isOverView = false)
    {
        DataTable table = GetCreatedDataTable(dtNextMon);

        OracleConnection oraConn = null;

        Hashtable hashToddlersAM = new Hashtable();
        Hashtable hashEldersAM = new Hashtable();
        Hashtable hashToddlersPM = new Hashtable();
        Hashtable hashEldersPM = new Hashtable();

        int seqNo = -1;
        int kidRefNo = -1;
        string kidTagName = string.Empty;
        double age = 0;
        DateTime dateBooked = DateTime.Now;
        int unixDateBooked = 0;
        string sessionBooked = string.Empty;
        string bookingType = string.Empty;
        int statusCode = 1;

        DBUtil db = new DBUtil();

        try
        {
            DataManager dtMgr = new DataManager();

            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
            OracleDataReader dataRd = dtMgr.GetBookingData(fromDate.ToString("d"), toDate.ToString("d"), oraConn, isOverView);
            KidAttendance kidAtd = null;
            ArrayList arrList = null;
            ArrayList arrTemp = null;

            while (dataRd != null && dataRd.Read())
            {
                if (!dataRd.IsDBNull(0))
                {
                    seqNo = dataRd.GetInt32(0);
                }

                if (!dataRd.IsDBNull(1))
                {
                    kidRefNo = dataRd.GetInt32(1);
                }

                if (!dataRd.IsDBNull(2))
                {
                    kidTagName = dataRd.GetString(2);
                }

                if (!dataRd.IsDBNull(3))
                {
                    age = Convert.ToDouble(dataRd.GetValue(3));
                }

                if (!dataRd.IsDBNull(4))
                {
                    sessionBooked = dataRd.GetString(4);
                }

                if (!dataRd.IsDBNull(5))
                {
                    dateBooked = dataRd.GetDateTime(5);
                }

                if (!dataRd.IsDBNull(6))
                {
                    unixDateBooked = dataRd.GetInt32(6);
                }
                
                if ( isOverView && !dataRd.IsDBNull(8))
                {
                    bookingType = dataRd.GetString(8);
                }

                if (isOverView && !dataRd.IsDBNull(11))
                {
                    statusCode = Convert.ToInt32( dataRd.GetString(11) );
                }

                kidAtd = new KidAttendance(seqNo, Convert.ToString(kidRefNo), kidTagName, age, sessionBooked, dateBooked, unixDateBooked, bookingType, statusCode);

                if (kidAtd.IsToddler && kidAtd.UnixDateBoked > 0)
                {
                    if ("AM".Equals(kidAtd.SessionBooked))
                    {
                        if (hashToddlersAM.Contains(kidAtd.UnixDateBoked))
                        {
                            arrTemp = (ArrayList)hashToddlersAM[kidAtd.UnixDateBoked];
                            arrTemp.Add(kidAtd);
                            hashToddlersAM[kidAtd.UnixDateBoked] = arrTemp;
                            arrTemp = null;
                        }
                        else
                        {
                            arrList = new ArrayList();
                            arrList.Add(kidAtd);
                            hashToddlersAM.Add(kidAtd.UnixDateBoked, arrList);
                            arrList = null;
                        }
                    }
                    else if ("PM".Equals(kidAtd.SessionBooked))
                    {
                        if (hashToddlersPM.Contains(kidAtd.UnixDateBoked))
                        {
                            arrTemp = (ArrayList)hashToddlersPM[kidAtd.UnixDateBoked];
                            arrTemp.Add(kidAtd);
                            hashToddlersPM[kidAtd.UnixDateBoked] = arrTemp;
                            arrTemp = null;
                        }
                        else
                        {
                            arrList = new ArrayList();
                            arrList.Add(kidAtd);
                            hashToddlersPM.Add(kidAtd.UnixDateBoked, arrList);
                            arrList = null;
                        }
                    }
                }
                else
                {
                    if ("AM".Equals(kidAtd.SessionBooked))
                    {

                        if (hashEldersAM.Contains(kidAtd.UnixDateBoked))
                        {
                            arrTemp = (ArrayList)hashEldersAM[kidAtd.UnixDateBoked];
                            arrTemp.Add(kidAtd);
                            hashEldersAM[kidAtd.UnixDateBoked] = arrTemp;
                            arrTemp = null;
                        }
                        else
                        {
                            arrList = new ArrayList();
                            arrList.Add(kidAtd);
                            hashEldersAM.Add(kidAtd.UnixDateBoked, arrList);
                            arrList = null;
                        }
                    }
                    else if ("PM".Equals(kidAtd.SessionBooked))
                    {
                        if (hashEldersPM.Contains(kidAtd.UnixDateBoked))
                        {
                            arrTemp = (ArrayList)hashEldersPM[kidAtd.UnixDateBoked];
                            arrTemp.Add(kidAtd);
                            hashEldersPM[kidAtd.UnixDateBoked] = arrTemp;
                            arrTemp = null;
                        }
                        else
                        {
                            arrList = new ArrayList();
                            arrList.Add(kidAtd);
                            hashEldersPM.Add(kidAtd.UnixDateBoked, arrList);
                            arrList = null;
                        }
                    }
                }
            }

                       
            table = this.AddRowsToDataTable(table, hashToddlersAM, hashEldersAM, hashToddlersPM, hashEldersPM, dtNextMon, isOverView);

            return table;

        }
        catch (Exception ex)
        {
            EventLogUtil.Log("GetGenaratedDataTable :" + ex.Message);
        }
        finally
        {

            db.CloseConnection(oraConn);
        }

        return table;
    }

    private DataTable GetCreatedDataTable(DateTime dtMon)
    {


        int unixMon = (int)this.ConvertToUnixTimestamp(dtMon.Date);
        int unixTue = (int)this.ConvertToUnixTimestamp(dtMon.Date.AddDays(1));
        int unixWed = (int)this.ConvertToUnixTimestamp(dtMon.Date.AddDays(2));
        int unixThu = (int)this.ConvertToUnixTimestamp(dtMon.Date.AddDays(3));
        int unixFri = (int)this.ConvertToUnixTimestamp(dtMon.Date.AddDays(4));

        DataTable table = new DataTable();

        table.Columns.Add(new DataColumn("Slot", typeof(int)));
        table.Columns.Add(new DataColumn("Session", typeof(string)));

        DataColumn colMon = new DataColumn("MON", typeof(string));
        if( IsHoliday( unixMon ) )
            colMon.Caption = dtMon.ToString("ddd, d MMM") + "  (Holiday)";
        else
            colMon.Caption = dtMon.ToString("ddd, d MMM");
        table.Columns.Add(colMon);

        DataColumn colTue = new DataColumn("TUE", typeof(string));
        if (IsHoliday(unixTue))
            colTue.Caption = dtMon.AddDays(1).ToString("ddd, d MMM") + "  (Holiday)";
        else
            colTue.Caption = dtMon.AddDays(1).ToString("ddd, d MMM");
        table.Columns.Add(colTue);

        DataColumn colWed = new DataColumn("WED", typeof(string));
        if (IsHoliday(unixWed))
            colWed.Caption = dtMon.AddDays(2).ToString("ddd, d MMM") + "  (Holiday)";
        else
            colWed.Caption = dtMon.AddDays(2).ToString("ddd, d MMM");
        table.Columns.Add(colWed);

        DataColumn colThu = new DataColumn("THU", typeof(string));
        if (IsHoliday(unixThu))
            colThu.Caption = dtMon.AddDays(3).ToString("ddd, d MMM") + "  (Holiday)";
        else
            colThu.Caption = dtMon.AddDays(3).ToString("ddd, d MMM");
        table.Columns.Add(colThu);

        DataColumn colFri = new DataColumn("FRI", typeof(string));
        if (IsHoliday(unixFri))
            colFri.Caption = dtMon.AddDays(4).ToString("ddd, d MMM") + "  (Holiday)";
        else
            colFri.Caption = dtMon.AddDays(4).ToString("ddd, d MMM");
        table.Columns.Add(colFri);


        table.Columns.Add(new DataColumn("Type", typeof(string)));
        table.Columns.Add(new DataColumn("Remark", typeof(string)));
        //table.Columns.Add(new DataColumn("BookingType", typeof(string)));
        
        return table;

    }

    private DataTable AddRowsToDataTable(DataTable table, Hashtable hashToddlersAM, Hashtable hashEldersAM, Hashtable hashToddlersPM, Hashtable hashEldersPM, DateTime dtNextMon, bool isOverView)
    {

        int count = 0;
        DataTable tableTemp = this.GetCreatedTableRows(table, hashToddlersAM, "AM", "TODDLERS", dtNextMon, ref count, isOverView);

        if (count > 0)
        {
            tableTemp.Rows.Add(null, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        count = 0;
        tableTemp = this.GetCreatedTableRows(tableTemp, hashEldersAM, "AM", "ELDERS", dtNextMon, ref count, isOverView);

        if (count > 0)
        {
            tableTemp.Rows.Add(null, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        count = 0;
        tableTemp = this.GetCreatedTableRows(tableTemp, hashToddlersPM, "PM", "TODDLERS", dtNextMon, ref count, isOverView);

        if (count > 0)
        {
            tableTemp.Rows.Add(null, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        count = 0;
        tableTemp = this.GetCreatedTableRows(tableTemp, hashEldersPM, "PM", "ELDERS", dtNextMon, ref count, isOverView);

        return tableTemp;
    
    }

    private string GetTagNameByObject(KidAttendance obj, bool isOverview)
    {

        string tagName = string.Empty;

        if (obj != null)
        {
            if (isOverview)
            {
                if (obj.IsToddler && obj.Age <= 3)
                    tagName = obj.KidTagName + "%" + obj.BookingType + "#" + obj.StatusCode + "&" +  " * ";
                else
                    tagName = obj.KidTagName + "%" + obj.BookingType + "#" + obj.StatusCode + "&" + "";
            }
            else
                tagName = obj.KidTagName;
        }
        else
            tagName = string.Empty;

        return tagName;
    }

    private KidAttendance GetObjectByArrayList(ArrayList arr, int count, int i)
    {

        KidAttendance obj = null;

        if (count > i)
            obj = (KidAttendance)arr[i];
        else
            obj = null;

        return obj;
    }

    private DataTable GetCreatedTableRows(DataTable table, Hashtable hashTb, string sessionText, string TypeText,  DateTime dtNextMon, ref int count, bool isOverview)
    {

        try
        {

            int elderCount = Convert.ToInt32( SysParamReaderUtil.GetSysParamByName("ELDERS_FIXED_SLOTS") );
            int toddlerCount = Convert.ToInt32( SysParamReaderUtil.GetSysParamByName("TODDLERS_FIXED_SLOTS") );

            //Manager mgr = new Manager();

            int unixMon = (int)this.ConvertToUnixTimestamp(dtNextMon.Date);
            int unixTue = (int)this.ConvertToUnixTimestamp(dtNextMon.Date.AddDays(1));
            int unixWed = (int)this.ConvertToUnixTimestamp(dtNextMon.Date.AddDays(2));
            int unixThu = (int)this.ConvertToUnixTimestamp(dtNextMon.Date.AddDays(3));
            int unixFri = (int)this.ConvertToUnixTimestamp(dtNextMon.Date.AddDays(4));

            ArrayList arrAtdMon = null;
            ArrayList arrAtdTue = null;
            ArrayList arrAtdWed = null;
            ArrayList arrAtdThu = null;
            ArrayList arrAtdFri = null;

            int monCount = 0, tueCount = 0, wedCount = 0, thuCount = 0, friCount = 0;


            arrAtdMon = (ArrayList)hashTb[unixMon];

            arrAtdTue = (ArrayList)hashTb[unixTue];

            arrAtdWed = (ArrayList)hashTb[unixWed];

            arrAtdThu = (ArrayList)hashTb[unixThu];

            arrAtdFri = (ArrayList)hashTb[unixFri];

            if (arrAtdMon != null)
                monCount = arrAtdMon.Count;
            if (arrAtdTue != null)
                tueCount = arrAtdTue.Count;
            if (arrAtdWed != null)
                wedCount = arrAtdWed.Count;
            if (arrAtdThu != null)
                thuCount = arrAtdThu.Count;
            if (arrAtdFri != null)
                friCount = arrAtdFri.Count;


            int[] arrayMax = { monCount, tueCount, wedCount, thuCount, friCount };

            int maxValue = arrayMax.Max();

            KidAttendance valMon = null;
            KidAttendance valTue = null;
            KidAttendance valWed = null;
            KidAttendance valThu = null;
            KidAttendance valFri = null;

            string monTagName = string.Empty;
            string tueTagName = string.Empty;
            string wedTagName = string.Empty;
            string thuTagName = string.Empty;
            string friTagName = string.Empty;

            string remark = string.Empty;

            for (int j = 0; j < maxValue; j++)
            {

                valMon = GetObjectByArrayList(arrAtdMon, monCount, j);
                valTue = GetObjectByArrayList(arrAtdTue, tueCount, j);
                valWed = GetObjectByArrayList(arrAtdWed, wedCount, j);
                valThu = GetObjectByArrayList(arrAtdThu, thuCount, j);
                valFri = GetObjectByArrayList(arrAtdFri, friCount, j);


                monTagName = this.GetTagNameByObject(valMon, isOverview);
                tueTagName = this.GetTagNameByObject(valTue, isOverview);
                wedTagName = this.GetTagNameByObject(valWed, isOverview);
                thuTagName = this.GetTagNameByObject(valThu, isOverview);
                friTagName = this.GetTagNameByObject(valFri, isOverview);

                count++;

                if ("TODDLERS".Equals(TypeText) && count > toddlerCount)
                {
                    remark = "Tentative";
                }
                else if ("ELDERS".Equals(TypeText) && count > elderCount)
                {
                    remark = "Tentative";
                }
                else
                {
                    remark = string.Empty;
                }


                table.Rows.Add(count, sessionText, monTagName, tueTagName, wedTagName, thuTagName, friTagName, TypeText, remark);
            }
        }
        catch (Exception ex)
        {

            EventLogUtil.Log("GetCreatedTableRows :" + ex.Message);
        }
        return table;
    }

    public bool IsHoliday(int ndate) {

        try
        {
            HashSet<Int32> hashHolidays = (HashSet<Int32>)System.Web.HttpContext.Current.Application["HOLIDAYS"];

            if (hashHolidays != null && hashHolidays.Contains(ndate))
                return true;
            else
                return false;
        }
        catch (Exception)
        {
            return false;
        }

    }

    /*public void SendDeleteEMailToCommitee(string name_, string date_, string session_, string LogOnUser) {

        string to_ = SysParamReaderUtil.GetSysParamByName("DEL_EMAIL_SEND");
        string isEnable = SysParamReaderUtil.GetSysParamByName("DEL_EMAIL_TO_NEXT_ENABLED");

        if( "YES".Equals(isEnable) && !string.IsNullOrEmpty (to_) ){
            
            string body_ = "Following entry has been deleted : <br/> => " + name_ + "  - " + date_ + " " + session_ + " <br/><br/> Deleted By " + LogOnUser + " <br/> <br/> This is an auto genarated mail. Please do not reply."; 

            MailUtil.SendHTMLMail( null ,to_, "IFS Juniors Attendance.", body_);
        }
    }*/

   /* public void SendDeleteEMailToNextParent(string nextKidNameTag, string nextKidParentName, string nextKidParentEmail, string date_, string session_ , string LogOnUser, string delkidName)
    {

        string toComitee_ = SysParamReaderUtil.GetSysParamByName("DEL_EMAIL_SEND");

        string isEnable = SysParamReaderUtil.GetSysParamByName("DEL_EMAIL_TO_NEXT_ENABLED");

        string sEMailBody= SysParamReaderUtil.GetSysParamByName("DEL_EMAIL_TO_NEXT_BODY");
        string to_ = "ifs.juniors@ifsworld.com";

        if (!string.IsNullOrEmpty(sEMailBody))
        {            
            string body_ = sEMailBody;

            body_ = body_.Replace("%PARENT_NAME%", nextKidParentName).Replace("%KID_NAME%", nextKidNameTag).Replace("%DATE%", date_).Replace("%SESSION%", session_);

            if ("YES".Equals(isEnable))
            {
                MailUtil.SendHTMLMail(null, nextKidParentEmail, "IFS Juniors Attendance.", body_, toComitee_);
            }
            else {
                if( IsAdmin( LogOnUser) ){
                
                  to_ = new DataManager().GetEmailByLogOnUser( LogOnUser );

                  if( string.IsNullOrEmpty( to_ ) )
                      to_ = "ifs.juniors@ifsworld.com";
                }

                body_ = body_ + " <br/><br/>  <br/><br/> Following entry has been deleted : <br/> => " + delkidName + "  - " + date_ + " " + session_ + " <br/><br/> Deleted By " + LogOnUser + " <br/><br/>  This mail should be sent to " + nextKidParentEmail + "<br/> Mail Enabled : " + isEnable;
                MailUtil.SendHTMLMail(null, to_, "IFS Juniors Attendance.", body_, toComitee_);
            }           
        }
    }*/

    public void SendNewRegistrationEmailToParent(string kidParentEmail, string kidName, string LogOnUser)
    {

        string isEnable = SysParamReaderUtil.GetSysParamByName("ADD_KID_EMAIL_ENABLED");

        string toComitee_ = SysParamReaderUtil.GetSysParamByName("ADD_KID_EMAIL_CC");

        string sEMailSub = SysParamReaderUtil.GetSysParamByName("ADD_KID_EMAIL_SUB");

        string sEMailBody = SysParamReaderUtil.GetSysParamByName("ADD_KID_EMAIL_BODY");

        string to_ = "ifs.juniors@ifsworld.com";

        if (!string.IsNullOrEmpty(sEMailBody))
        {
            string body_ = sEMailBody;

            body_ = body_.Replace("%KID_NAME%", kidName);

            if ("YES".Equals(isEnable))
            {
                MailUtil.SendHTMLMail(null, kidParentEmail, sEMailSub, body_, toComitee_);
            }
            else
            {
                if (IsAdmin(LogOnUser))
                {

                    to_ = new DataManager().GetEmailByLogOnUser(LogOnUser);

                    if (string.IsNullOrEmpty(to_))
                        to_ = "ifs.juniors@ifsworld.com";
                }

                body_ = body_ + " <br/><br/>  <br/><br/> Following new kid has been Added : <br/> => " + kidName + " <br/><br/> Added By " + LogOnUser + " <br/><br/>  This mail should be sent to " + kidParentEmail + "<br/> Mail Enabled : " + isEnable;
                MailUtil.SendHTMLMail(null, to_, sEMailSub, body_, toComitee_);
            }
        }
    }

    public void WriteGridData(DataTable DT, string exportFilePath)
    {

         String line = "";
         string tmp_ = "";
         string session_ = "";
         string childGroup_ = "";
         string tmpchildGroup_ = "";
         string tmpsession_ = "";

         StreamWriter sw = null;

         string bShowAge = SysParamReaderUtil.GetSysParamByName("SHOW_AGE_AT_EXPORT");

         try
         {             
             sw = new StreamWriter(exportFilePath);

             for (int i = 0; i < DT.Columns.Count; i++)
             {
                 if (i > 1 && i < 7)
                 {
                     sw.WriteLine(DT.Columns[i].Caption);
                     sw.WriteLine("=============");
                 }

                 foreach (DataRow _row in DT.Rows)
                 {

                     if (i > 1 && i < 7)
                     {
                         tmp_ = _row[i].ToString();
                         session_ = _row[1].ToString();
                         childGroup_ = _row[7].ToString();

                         if (!string.IsNullOrEmpty(childGroup_))
                         {
                             if (!tmpchildGroup_.Equals(childGroup_))
                             {
                                 if ("AM".Equals(session_))
                                     tmpsession_ = "Morning";
                                 else
                                     tmpsession_ = "Afternoon";

                                 sw.WriteLine("\r\n");
                                 sw.WriteLine(childGroup_ + " - " + tmpsession_);

                                 sw.WriteLine("-----------------------");
                             }

                             tmpchildGroup_ = childGroup_;
                         }

                         if (!string.IsNullOrEmpty(tmp_))
                         {
                             if( "TRUE".Equals ( bShowAge ) )
                                line = _row[0].ToString() + ". " + tmp_.Substring(0, tmp_.IndexOf("%")) +   tmp_.Substring(tmp_.IndexOf("&") + 1  ) ;
                             else
                                line = _row[0].ToString() + ". " + tmp_.Substring(0, tmp_.IndexOf("%"));

                             sw.WriteLine(line);
                         }
                     }
                 }

                 if (i > 1 && i < 7)
                    sw.WriteLine("\r\n");
             }
         }
         catch (Exception ex)
         {

             throw;
         }

         finally
         {
             sw.Dispose();
         }
    }

    public string GetLogOnUser(  System.Web.UI.Page page_ )
    {

        if (HttpContext.Current.Session["LOGON_USER"] != null && !string.IsNullOrEmpty(HttpContext.Current.Session["LOGON_USER"].ToString()))
            return HttpContext.Current.Session["LOGON_USER"].ToString();

        else
        {
            string Auth_User =  page_.User.Identity.Name.ToUpper();
            string userName = string.Empty;

            if (!string.IsNullOrEmpty(Auth_User))
            {

                Auth_User = Auth_User.Replace("/", "\\");
                if (Auth_User.Contains("\\"))
                    userName = Auth_User.Split(new Char[] { '\\' })[1];
                if (userName == null)
                    userName = Auth_User;

                HttpContext.Current.Session["LOGON_USER"] = userName.ToUpper();
            }
            return userName.ToUpper();
        }        
    }
}