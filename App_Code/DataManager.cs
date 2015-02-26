using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IFS.CoS.ServiceUtil;
using Oracle.DataAccess.Client;
using System.Data;
/// <summary>
/// Summary description for DataManager
/// </summary>
public class DataManager
{
    #region Private Constants
    private const char FIELD_SEPARATOR = (char)31;
    private const char RECORD_SEPARATOR = (char)30;
    #endregion

    public DataManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    /*get booking data to the order*/
    public OracleDataReader getkid_data(OracleConnection oConn, string kidName)
    {
        //string sqlStatement =
        //"select * from KIDS_STATS_TAB t where upper(t.kid_name) = upper(:pname)";

        string sqlStatement =
        "select t2.points, t1.bookings, t1.fixed_bookings,t1.present, t1.absent, t1.cancelled, t1.bulk_cancellations " +
        "from KIDS_STATS_TAB t1, POINTS t2 " +
        "where t1.kid_name= t2.KID_NAME and t1.kid_name =:pname ";

        OracleParameter pkid = new OracleParameter("pname", OracleDbType.Varchar2);
        pkid.Value = kidName;

        OracleParameter[] paramArray = { pkid};

        return new DBUtil().GetExecuteQuery(sqlStatement, oConn, paramArray);
    }

    /* get kid list for given date */ 
    public OracleDataReader getkidlist_data(String fromDate, OracleConnection oConn, string session, string typeText)
    {
        string sqlStatement = "select k.NAME_TAG from KIDS_INFO k, POINTS p, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED = TO_DATE(:pdate, 'MM/DD/YYYY') and upper(p.kid_name) = upper (k.NAME_TAG) and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED = :psession and k.CATEGORY = :ptype order by t.BOOKING_STATE desc,p.POINTS ,k.NAME_TAG";

        OracleParameter pFromDate = new OracleParameter("pdate", OracleDbType.Varchar2);
        pFromDate.Value = fromDate;

        OracleParameter pSession = new OracleParameter("psession", OracleDbType.Varchar2);
        pSession.Value = session;

        OracleParameter pType = new OracleParameter("ptype", OracleDbType.Varchar2);
        pType.Value = typeText;

        OracleParameter[] paramArray = { pFromDate, pSession, pType };

        return new DBUtil().GetExecuteQuery(sqlStatement, oConn, paramArray);
    }

    /*get booking data count to the order*/
    /*to be changed kidtype-changed*/
    public OracleDataReader getkidlist_data_count(String fromDate, OracleConnection oConn, string session, string typeText)
    {
        string sqlStatement = "select count(k.NAME_TAG) from KIDS_INFO k, POINTS p, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED = TO_DATE(:pdate, 'MM/DD/YYYY') and upper(p.kid_name) = upper (k.NAME_TAG) and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED = :psession and k.CATEGORY = :ptype order by p.POINTS ,k.NAME_TAG";

        OracleParameter pFromDate = new OracleParameter("pdate", OracleDbType.Varchar2);
        pFromDate.Value = fromDate;

        OracleParameter pSession = new OracleParameter("psession", OracleDbType.Varchar2);
        pSession.Value = session;

        OracleParameter pType = new OracleParameter("ptype", OracleDbType.Varchar2);
        pType.Value = typeText;

        OracleParameter[] paramArray = { pFromDate, pSession, pType };

        return new DBUtil().GetExecuteQuery(sqlStatement, oConn, paramArray);
    }

    public OracleDataReader getkidlist_fixeddata_count(String fromDate, OracleConnection oConn, string session, string typeText)
    {
        string sqlStatement = "select count(k.NAME_TAG) from KIDS_INFO k, POINTS p, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED = TO_DATE(:pdate, 'MM/DD/YYYY') and upper(p.kid_name) = upper (k.NAME_TAG) and k.REF_NO=t.Kid_Ref_No and t.SESSION_BOOKED = :psession and k.CATEGORY = :ptype and (t.BOOKING_STATE=1 or t.BOOKING_STATE=2) order by p.POINTS ,k.NAME_TAG";  

        OracleParameter pFromDate = new OracleParameter("pdate", OracleDbType.Varchar2);
        pFromDate.Value = fromDate;

        OracleParameter pSession = new OracleParameter("psession", OracleDbType.Varchar2);
        pSession.Value = session;

        OracleParameter pType = new OracleParameter("ptype", OracleDbType.Varchar2);
        pType.Value = typeText;

        OracleParameter[] paramArray = { pFromDate, pSession, pType };

        return new DBUtil().GetExecuteQuery(sqlStatement, oConn, paramArray);
    }

    /*===================== get table for the perticular date ==========================================*/
    public DataTable getkidlist_table(DateTime initialdate, string session, string typeText)
    {
        DataManager dtMgr = new DataManager();

        OracleConnection oraConn = null;
        DBUtil db = new DBUtil();

        DataTable table = new DataTable();

        try
        {
            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

            OracleDataReader dataRd = getkidlist_data(initialdate.ToString("d"), oraConn, session, typeText);

            table.Load(dataRd); 
            
            return table;

        }
        catch (Exception ex)
        {
            EventLogUtil.Log("Default.aspx:PageLoad : " + ex.Message);
        }
        finally
        {

            db.CloseConnection(oraConn);
        }

        return table;
    }


    /*mark the attendence*/
    public void markAttendence(DateTime initialdate, string session, string kidText,int ispresent)
    {
        DataManager dtMgr = new DataManager();

        OracleConnection oraConn = null;
        DBUtil db = new DBUtil();

        try
        {
            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

            markAttendence_data(initialdate.ToString("d"), oraConn, session, kidText, ispresent);
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

    /*get booking state*/
    public OracleDataReader getTentitiveToFixedBookingCount(String fromDate, OracleConnection oConn, string date, string sessoin)
    {
        string sqlStatement =
        /*"UPDATE kids_attendance_tab t1 SET t1.attendance =:att WHERE t1.kid_ref_no =(select t2.ref_no from kids_info t2 where t2.name_tag=:pkid ) and t1.session_booked = :psession and t1.date_booked = TO_DATE(:pdate, 'MM/DD/YYYY')";*/

        "select count(*) from KIDS_ATTENDANCE_TAB t1 " +
        "where t1.date_booked = TO_DATE(:pdate, 'MM/DD/YYYY') and t1.session_booked = :psession " +
        "and t1.status_code = 2  "; 

        OracleParameter pdate = new OracleParameter("pdate", OracleDbType.Varchar2);
        pdate.Value = date;

        OracleParameter psession = new OracleParameter("psession", OracleDbType.Varchar2);
        psession.Value = sessoin;

        /*OracleParameter pkid = new OracleParameter("pkid", OracleDbType.Varchar2);
        pkid.Value = kid;*/

        OracleParameter[] paramArray = { pdate, psession, };

        return new DBUtil().GetExecuteQuery(sqlStatement, oConn, paramArray);
    }
    
    
    /*mark attendence data*/
    public OracleDataReader markAttendence_data(String fromDate, OracleConnection oConn, string session, string kidText,int attendance)
    {
        string sqlStatement =
        "UPDATE kids_attendance_tab t1 SET t1.attendance =:att WHERE t1.kid_ref_no =(select t2.ref_no from kids_info t2 where t2.name_tag=:pkid ) and t1.session_booked = :psession and t1.date_booked = TO_DATE(:pdate, 'MM/DD/YYYY')";

        OracleParameter att = new OracleParameter("att", OracleDbType.Varchar2);
        att.Value = attendance.ToString();

        OracleParameter pFromDate = new OracleParameter("pkid", OracleDbType.Varchar2);
        pFromDate.Value = kidText;

        OracleParameter pSession = new OracleParameter("psession", OracleDbType.Varchar2);
        pSession.Value = session;

        OracleParameter pType = new OracleParameter("pdate", OracleDbType.Varchar2);
        pType.Value = fromDate;



        OracleParameter[] paramArray = { att, pFromDate, pSession, pType };

        return new DBUtil().GetExecuteQuery(sqlStatement, oConn, paramArray);
    }


    /*get a list count*/
    public int getkidlist_count(DateTime initialdate, string session, string typeText)
    {
        DataManager dtMgr = new DataManager();

        OracleConnection oraConn = null;
        DBUtil db = new DBUtil();

        try
        {
            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

            OracleDataReader dataRd = getkidlist_data_count(initialdate.ToString("d"), oraConn, session, typeText);

            if (dataRd != null && dataRd.Read())
            {
                if (!dataRd.IsDBNull(0))
                {
                    return dataRd.GetInt32(0);
                }
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
        return 0;
    }

    public int getkidlist_fixed_count(DateTime initialdate, string session, string typeText)
    {
        DataManager dtMgr = new DataManager();

        OracleConnection oraConn = null;
        DBUtil db = new DBUtil();

        try
        {
            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

            OracleDataReader dataRd = getkidlist_fixeddata_count(initialdate.ToString("d"), oraConn, session, typeText);

            if (dataRd != null && dataRd.Read())
            {
                if (!dataRd.IsDBNull(0))
                {
                    return dataRd.GetInt32(0);
                }
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
        return 0;
    }

    /*get a list count*/
   /* public int getT2F_count(DateTime initialdate, string session, string typeText)
    {
        DataManager dtMgr = new DataManager();

        OracleConnection oraConn = null;
        DBUtil db = new DBUtil();

        try
        {
            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

            OracleDataReader dataRd = dtMgr.getTentitiveToFixedBookingCount(initialdate.ToString("d"), oraConn, session, typeText);

            if (dataRd != null && dataRd.Read())
            {
                if (!dataRd.IsDBNull(0))
                {
                    return dataRd.GetInt32(0);
                }
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
        return 0;
    }*/

    /// <summary>
    /// Check whether the DB connection is successfullr.
    /// </summary>
    ///  <param name="oraConn">DB Connection to verify.</param>
    /// <returns>
    ///   <c>true</c> if [Db Connection Exist]; otherwise, <c>false</c>.
    /// </returns>
    public bool CheckExistDB(ServiceUtil.DB db_)
    {
        DBUtil db = new DBUtil();
        OracleConnection oracleDbConn = null;

        bool isExist = false;

        try
        {
            oracleDbConn = db.OpenConnection(db_);
            isExist = true;
        }
        catch (Exception e)
        {
            EventLogUtil.Log("CheckExistDB : " + e.Message);
        }

        finally
        {
            db.CloseConnection(oracleDbConn);
        }

        return isExist;
    }

    /// <summary>
    /// Check whether the active user account exist in DB for the specified user.
    /// </summary>
    ///  <param name="sUsername">Username.</param>
    /// <returns>
    ///   <c>true</c> if [is multiple cases exists] [the specified case ID]; otherwise, <c>false</c>.
    /// </returns>
   /* public bool CheckExistGardian(string sUsername, int iStatusCode)
    {
        DBUtil db = new DBUtil();
        OracleConnection oracleDbConn = null;

        string sql = "SELECT fullname, FROM guardian_info_tab WHERE status_code = :statusCode AND UPPER( username ) = UPPER(:username)";
        bool isExist = false;

        try
        {
            OracleParameter pStatusCode = new OracleParameter("statusCode", OracleDbType.Int32);
            pStatusCode.Value = iStatusCode;

            OracleParameter pUsername = new OracleParameter("username", OracleDbType.Varchar2);
            pUsername.Value = sUsername;

            OracleParameter[] paramArray = { pStatusCode, pUsername };

            oracleDbConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

            OracleDataReader dataRd = db.GetExecuteQuery(sql, oracleDbConn, paramArray);

            if (dataRd.HasRows)
            {
                isExist = true;
            }
        }

        catch (Exception e)
        {
            EventLogUtil.Log("CheckExiseGardian : " + e.Message);
        }

        finally
        {
            db.CloseConnection(oracleDbConn);
        }

        return isExist;
    }*/

    /// <summary>
    /// Check whether the active user account exist in DB for the specified user.
    /// </summary>
    ///  <param name="sUsername">Username.</param>
    /// <returns>
    ///   <c>true</c> if [is multiple cases exists] [the specified case ID]; otherwise, <c>false</c>.
    /// </returns>
    public bool CheckExistForecastBooking(int ikidRefNo, DateTime dateBooked, string sessionBooked, OracleConnection oracleDbConn)
    {
        DBUtil db = new DBUtil();
       
        string sql = "SELECT 1 FROM kids_attendance_tab WHERE kid_ref_no = :refno AND date_booked = :datebooked AND session_booked = :sessionbooked";
        bool isExist = false;

        try
        {
            OracleParameter pSrefNo = new OracleParameter("refno", OracleDbType.Int32);
            pSrefNo.Value = ikidRefNo;

            OracleParameter pDtBooked = new OracleParameter("datebooked", OracleDbType.Date);
            pDtBooked.Value = Convert.ToDateTime(dateBooked.ToString("d"));

            OracleParameter pSessionBooked = new OracleParameter("sessionbooked", OracleDbType.Varchar2);
            pSessionBooked.Value = sessionBooked;

            OracleParameter[] paramArray = { pSrefNo, pDtBooked, pSessionBooked };

             OracleDataReader dataRd = db.GetExecuteQuery(sql, oracleDbConn, paramArray);

            if (dataRd.HasRows)
            {
                isExist = true;
            }
        }

        catch (Exception e)
        {
            EventLogUtil.Log("CheckExiseGardian : " + e.Message);
        }

        
        return isExist;
    }


    public bool AddAttendanceForecastDB(int iKidRefNo, DateTime dateBooked, string sSession, string sActUserName, OracleConnection oracleDbConn, OracleTransaction atdTrans, int iStatusCode = 1)
    {

        DBUtil db = new DBUtil();
        
        string sql = "INSERT INTO kids_attendance_tab( seq_no, kid_ref_no, date_booked, unixdate_booked, session_booked, activity_username, activity_timestamp, status_code) VALUES( ATTENDANCE_SEQ.NEXTVAL, :refno, :datebooked, :unixdatebooked, :sessionbooked, :activityusername, SYSDATE, :statuscode ) ";

        int val = -1;

        try
        {
            int unixDateBooked = (int)new Manager().ConvertToUnixTimestamp(dateBooked.Date);

            OracleParameter pKidRefNo = new OracleParameter("refno", OracleDbType.Int32);
            pKidRefNo.Value = iKidRefNo;

            OracleParameter pDtBooked = new OracleParameter("datebooked", OracleDbType.Date);
            pDtBooked.Value = dateBooked.Date; //Convert.ToDateTime( dateBooked.ToString("d") );

            OracleParameter pDtUnixBooked = new OracleParameter("unixdatebooked", OracleDbType.Int32);
            pDtUnixBooked.Value = unixDateBooked;

            OracleParameter pSession = new OracleParameter("sessionbooked", OracleDbType.Varchar2);
            pSession.Value = sSession;

            OracleParameter pActUserName = new OracleParameter("activityusername", OracleDbType.Varchar2);
            pActUserName.Value = sActUserName;

            OracleParameter pStatusCode = new OracleParameter("statuscode", OracleDbType.Int32);
            pStatusCode.Value = iStatusCode;

            OracleParameter[] paramArray = { pKidRefNo, pDtBooked, pDtUnixBooked, pSession, pActUserName, pStatusCode };

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oracleDbConn;
            cmd.Transaction = atdTrans;
            cmd.CommandText = sql;
            val = db.InvokeCommand(cmd, paramArray);
            cmd.Parameters.Clear();
            cmd.Dispose();
        }

        catch (Exception e)
        {
            EventLogUtil.Log("AddAttendanceForecastDB : " + e.Message);
            throw;
        }

        if (val > 0)
            return true;
        else
            return false;
    }

    public OracleDataReader GetKidNamesByGuardianUserName(string sUserName, OracleConnection oConn)
    {
        string minAge = SysParamReaderUtil.GetSysParamByName("MIN_AGE_LIMIT");
        string maxAge = SysParamReaderUtil.GetSysParamByName("MAX_AGE_LIMIT");

        string sqlStatement = "SELECT t.name_tag, t.ref_no FROM KIDS_GUARDIAN_INFO t WHERE UPPER(username) = :username" +
                              " AND t.sub_year >= extract(year from sysdate) AND t.age >= " + minAge + " AND t.age <= " + maxAge +
                              " UNION SELECT k.name_tag, k.ref_no FROM KIDS_GUARDIAN_INFO k WHERE k.kids_status = 4 AND  UPPER(username) = :username" +
                              " order by name_tag";

        OracleParameter pUserName;

        pUserName = new OracleParameter("username", OracleDbType.Varchar2);
        pUserName.Value = sUserName.ToUpper();

        OracleParameter[] paramArray = { pUserName };

        return new DBUtil().GetExecuteQuery(sqlStatement, oConn, paramArray);

    }

    public OracleDataReader GetKidNamesTagList(OracleConnection oConn)
    {
        string minAge = SysParamReaderUtil.GetSysParamByName("MIN_AGE_LIMIT");
        string maxAge = SysParamReaderUtil.GetSysParamByName("MAX_AGE_LIMIT");

        string sqlStatement = "SELECT t.name_tag, t.ref_no FROM KIDS_INFO t" +
                              " WHERE t.age >= " + minAge + " AND t.age <= " + maxAge +
                              " UNION SELECT k.name_tag, k.ref_no FROM KIDS_GUARDIAN_INFO k WHERE k.kids_status = 4" +
                              " order by name_tag ";

        return new DBUtil().GetExecuteQuery(sqlStatement, oConn);
    }

    public OracleDataReader GetGuardianUserNameList(OracleConnection oConn)
    {
        //string sqlStatement = "SELECT DISTINCT(UPPER(t.username)) as username FROM KIDS_GUARDIAN_INFO t ORDER BY username";
        string sqlStatement = "select upper(t.username) from GUARDIAN_INFO_TAB t order by t.username asc";

        return new DBUtil().GetExecuteQuery(sqlStatement, oConn);
    }

    public OracleDataReader GetGuardianUserNameListByKidRefNo(int iKidRefNo, OracleConnection oConn)
    {
        string sqlStatement = "SELECT  t.fullname FROM KIDS_GUARDIAN_INFO t where  t.ref_no = :refno";

        OracleParameter pKidRefNo = new OracleParameter("refno", OracleDbType.Int32);
        pKidRefNo.Value = iKidRefNo;

        OracleParameter[] paramArray = { pKidRefNo };

        return new DBUtil().GetExecuteQuery(sqlStatement, oConn, paramArray);
    }

    public OracleDataReader GetAdminNameList(OracleConnection oConn)
    {
        string sqlStatement = "SELECT DISTINCT(UPPER(t.username)) as username, t.is_admin  FROM ADMIN_TAB t ORDER BY username";

        return new DBUtil().GetExecuteQuery(sqlStatement, oConn);
    }

    private string KidsAttendanceOverviewSQL
    {

        get
        {

            return "select t.seq_no, t.kid_ref_no, t.name_tag, t.age, t.session_booked, t.date_booked, t.unixdate_booked, t.activity_username, t.booking_type, t.slot_no, t.category, t.status_code from KIDS_ATTENDANCE_OVERVIEW t ";
        }

    }

    private string KidsAttendanceBookingSQL
    {
        get
        {

            return "select t.seq_no, t.kid_ref_no, t.name_tag, t.age, t.session_booked, t.date_booked, t.unixdate_booked, t.activity_username from KIDS_ATTENDANCE_BOOKING t ";
        }

    }

    public OracleDataReader GetBookingData(String fromDate, String toDate, OracleConnection oConn, bool isOverview)
    {

        string KidsAttendanceSQL = KidsAttendanceBookingSQL;

        if (isOverview)
        {

            KidsAttendanceSQL = KidsAttendanceOverviewSQL;
        }

        string sqlStatement = KidsAttendanceSQL +
        " where t.date_booked between TO_DATE(:fromdate, 'MM/DD/YYYY')" +
        " AND TO_DATE(:todate, 'MM/DD/YYYY') ORDER BY t.date_booked, t.session_booked, t.seq_no";
        OracleParameter pFromDate = new OracleParameter("fromdate", OracleDbType.Varchar2);
        pFromDate.Value = fromDate;

        OracleParameter pToDate = new OracleParameter("todate", OracleDbType.Varchar2);
        pToDate.Value = toDate;

        OracleParameter[] paramArray = { pFromDate, pToDate };

        return new DBUtil().GetExecuteQuery(sqlStatement, oConn, paramArray);
    }


    public OracleDataReader GetBookingData(String fromDate, String toDate, string guardianUsername, OracleConnection oConn, bool isOverview)
    {
        /*string KidsAttendanceSQL = KidsAttendanceBookingSQL;

        if (isOverview)
        {
            KidsAttendanceSQL = KidsAttendanceOverviewSQL;
        }
        string sqlStatement = KidsAttendanceSQL; 

        OracleParameter pFromDate = new OracleParameter("fromdate", OracleDbType.Varchar2);
        pFromDate.Value = fromDate;

        OracleParameter pToDate = new OracleParameter("todate", OracleDbType.Varchar2);
        pToDate.Value = toDate;

        OracleParameter pUserName = new OracleParameter("username", OracleDbType.Varchar2);
        pUserName.Value = guardianUsername;

        OracleParameter[] paramArray = { pFromDate, pToDate, pUserName };*/



        string KidsAttendanceSQL = KidsAttendanceBookingSQL;

        if (isOverview)
        {
            KidsAttendanceSQL = KidsAttendanceOverviewSQL;
        }
        string sqlStatement = KidsAttendanceSQL + // "select t.seq_no, t.kid_ref_no, t.name_tag, t.age, t.session_booked, t.date_booked, t.unixdate_booked, t.activity_username from KIDS_ATTENDANCE t " +
                              " where t.date_booked between TO_DATE(:fromdate, 'MM/DD/YYYY')" +
                              " AND TO_DATE(:todate, 'MM/DD/YYYY') AND t.kid_ref_no IN ( select ref_no from KIDS_GUARDIAN_INFO g where upper( g.USERNAME ) = UPPER( :username ) ) " +
                              " ORDER BY t.date_booked, t.session_booked, t.seq_no";

        OracleParameter pFromDate = new OracleParameter("fromdate", OracleDbType.Varchar2);
        pFromDate.Value = fromDate;

        OracleParameter pToDate = new OracleParameter("todate", OracleDbType.Varchar2);
        pToDate.Value = toDate;

        OracleParameter pUserName = new OracleParameter("username", OracleDbType.Varchar2);
        pUserName.Value = guardianUsername;

        OracleParameter[] paramArray = { pFromDate, pToDate, pUserName };

        return new DBUtil().GetExecuteQuery(sqlStatement, oConn, paramArray);
    }

    public OracleDataReader GetBookingData(String fromDate, String toDate, int iKidRefNo, OracleConnection oConn, bool isOverview)
    {
        string KidsAttendanceSQL = KidsAttendanceBookingSQL;

        if (isOverview)
        {
            KidsAttendanceSQL = KidsAttendanceOverviewSQL;
        }
        string sqlStatement = KidsAttendanceSQL + // "select t.seq_no, t.kid_ref_no, t.name_tag, t.age, t.session_booked, t.date_booked, t.unixdate_booked, t.activity_username from KIDS_ATTENDANCE t " +
                              " where t.date_booked between TO_DATE(:fromdate, 'MM/DD/YYYY')" +
                              " AND TO_DATE(:todate, 'MM/DD/YYYY') AND t.kid_ref_no = :refno " +
                              " ORDER BY t.date_booked, t.session_booked, t.seq_no";

        OracleParameter pFromDate = new OracleParameter("fromdate", OracleDbType.Varchar2);
        pFromDate.Value = fromDate;

        OracleParameter pToDate = new OracleParameter("todate", OracleDbType.Varchar2);
        pToDate.Value = toDate;

        OracleParameter pKidRefNo = new OracleParameter("refno", OracleDbType.Int32);
        pKidRefNo.Value = iKidRefNo;


        OracleParameter[] paramArray = { pFromDate, pToDate, pKidRefNo };

        return new DBUtil().GetExecuteQuery(sqlStatement, oConn, paramArray);
    }

    public DataTable GetSysParam()
    {

        string sqlStatement = "select param, param_value, comments from system_param_tab order by param";

        DBUtil db = new DBUtil(ServiceUtil.DB.DefaultDB);

        DataTable dTable = new DataTable();
        OracleDataReader dataRd = null;
        string paramName = string.Empty;
        string paramValue = string.Empty;
        try
        {
            dataRd = db.GetExecuteQuery(sqlStatement);

            dTable.Load(dataRd);
        }
        catch (Exception ex)
        {
            EventLogUtil.Log("GetSysParam : " + ex.Message);
        }
        finally
        {

            db.Close();
        }
        return dTable;
    }

    public DataTable GetDeletionKidList(String fromDate, String toDate)
    {

        string sqlStatement = "SELECT t.name_tag, t.session_booked, t.category, t.date_booked, t.unixdate_booked, t.deleted_username, t.deleted_timestamp FROM DEL_KIDS_ATTENDANCE t" +
                              " where t.date_booked between TO_DATE(:fromdate, 'MM/DD/YYYY')" +
                              " AND TO_DATE(:todate, 'MM/DD/YYYY')";

        DBUtil db = new DBUtil(ServiceUtil.DB.DefaultDB);

        DataTable dTable = new DataTable();
        OracleDataReader dataRd = null;

        OracleParameter pFromDate = new OracleParameter("fromdate", OracleDbType.Varchar2);
        pFromDate.Value = fromDate;

        OracleParameter pToDate = new OracleParameter("todate", OracleDbType.Varchar2);
        pToDate.Value = toDate;

        OracleParameter[] paramArray = { pFromDate, pToDate };
        try
        {
            dataRd = db.GetExecuteQuery(sqlStatement, paramArray);

            dTable.Load(dataRd);
        }
        catch (Exception ex)
        {
            EventLogUtil.Log("GetDeletionKidList : " + ex.Message);
        }
        finally
        {

            db.Close();
        }
        return dTable;
    }


    /*public string GetBookingType(string unixdate_booked_, string session_booked_, string kid_slot_no_, string kid_category_)
    {

        string sql = "Ifsjuniors_Util_API.Get_Booking_Type";

        DBUtil db = new DBUtil();

        OracleConnection oraConn = null;
        string bookingType = string.Empty;

        try
        {
            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

            OracleParameter pReturnVal = new OracleParameter("returnVal", OracleDbType.Varchar2);
            pReturnVal.Direction = ParameterDirection.ReturnValue;
            pReturnVal.Size = 10;

            OracleParameter pUnixDate = new OracleParameter("unixdate_booked_", OracleDbType.Varchar2);
            pUnixDate.Value = unixdate_booked_;

            OracleParameter pSessionBooked = new OracleParameter("session_booked_", OracleDbType.Varchar2);
            pSessionBooked.Value = session_booked_;

            OracleParameter pKidSlotNo = new OracleParameter("kid_slot_no_", OracleDbType.Int32);
            pKidSlotNo.Value = kid_slot_no_;

            OracleParameter pKidCategory = new OracleParameter("kid_category_", OracleDbType.Int32);
            pKidCategory.Value = kid_category_;

            OracleParameter[] paramArray = { pReturnVal, pUnixDate, pSessionBooked, pKidSlotNo, pKidCategory };

            db.GetExecuteStoredProcedure(sql, paramArray, oraConn);

            bookingType = pReturnVal.Value.ToString();
        }
        catch (Exception ex)
        {
            EventLogUtil.Log("GetBookingType : " + ex.Message);
        }
        finally
        {
            db.CloseConnection(oraConn);
        }
        return bookingType;
    }*/

    public string GetEmailByLogOnUser(string LogOnUser_)
    {
        string sql = "Ifsjuniors_Util_API.Get_Email_By_Username";

        DBUtil db = new DBUtil();

        OracleConnection oraConn = null;
        string email_ = string.Empty;

        try
        {
            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

            OracleParameter pReturnVal = new OracleParameter("returnVal", OracleDbType.Varchar2);
            pReturnVal.Direction = ParameterDirection.ReturnValue;
            pReturnVal.Size = 250;
            pReturnVal.DbType = DbType.String;

            OracleParameter pUserName = new OracleParameter("username_", OracleDbType.Varchar2);
            pUserName.Value = LogOnUser_;
            pUserName.Direction = ParameterDirection.Input;

            OracleParameter[] paramArray = { pReturnVal, pUserName };

            db.GetExecuteStoredProcedure(sql, paramArray, oraConn);

            email_ = pReturnVal.Value.ToString();
        }
        catch (Exception ex)
        {
            EventLogUtil.Log("GetEmailByLogOnUser : " + ex.Message);
        }
        finally
        {
            db.CloseConnection(oraConn);
        }
        return email_;
    }

    public bool CheckExistSysParam(string paramName)
    {

        string sqlStatement = "select param, param_value, comments from system_param_tab where param = :param";

        DBUtil db = new DBUtil(ServiceUtil.DB.DefaultDB);

        OracleDataReader dataRd = null;

        try
        {
            OracleParameter pParamName = new OracleParameter("param", OracleDbType.Varchar2);
            pParamName.Value = paramName.ToUpper();

            OracleParameter[] paramArray = { pParamName };

            dataRd = db.GetExecuteQuery(sqlStatement, paramArray);

            if (dataRd.HasRows)
                return true;
            else
                return false;

        }
        catch (Exception ex)
        {
            EventLogUtil.Log("CheckExistSysParam : " + ex.Message);
        }
        finally
        {
            db.Close();
        }
        return false;
    }

    public bool AddSysParam(string paramName, string paramValue, string comments, string logOnUser)
    {

        DBUtil db = new DBUtil();
        OracleConnection oracleDbConn = null;

        string sql = "INSERT INTO system_param_tab( param, param_value, comments, activity_username, activity_timestamp) VALUES( :param, :param_value, :comments, :activityusername, sysdate ) ";

        int val = -1;

        try
        {
            OracleParameter pParamName = new OracleParameter("param", OracleDbType.Varchar2);
            pParamName.Value = paramName.ToUpper();

            OracleParameter pParamValue = new OracleParameter("param_value", OracleDbType.Varchar2);
            pParamValue.Value = paramValue;

            OracleParameter pComments = new OracleParameter("comments", OracleDbType.Varchar2);
            pComments.Value = comments;

            OracleParameter pActivityUsername = new OracleParameter("activityusername", OracleDbType.Varchar2);
            pActivityUsername.Value = logOnUser;


            OracleParameter[] paramArray = { pParamName, pParamValue, pComments, pActivityUsername };

            oracleDbConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
            val = db.InvokeCommand(oracleDbConn, sql, paramArray);
        }

        catch (Exception e)
        {
            EventLogUtil.Log("AddSysParam : " + e.Message);
        }
        finally
        {

            db.CloseConnection(oracleDbConn);
        }

        if (val > 0)
            return true;
        else
            return false;
    }

    public bool UpdateSysParamByName(string paramName, string paramValue, string comments, string logOnUser)
    {

        DBUtil db = new DBUtil();
        OracleConnection oracleDbConn = null;

        string sql = "UPDATE system_param_tab SET param_value = :param_value , comments = :comments, update_username = :updateusername, update_timestamp = sysdate WHERE param = :param";

        int val = -1;

        try
        {

            OracleParameter pParamValue = new OracleParameter("param_value", OracleDbType.Varchar2);
            pParamValue.Value = paramValue;

            OracleParameter pComments = new OracleParameter("comments", OracleDbType.Varchar2);
            pComments.Value = comments;

            OracleParameter pParamName = new OracleParameter("param", OracleDbType.Varchar2);
            pParamName.Value = paramName.ToUpper();

            OracleParameter pUpdateUsername = new OracleParameter("updateusername", OracleDbType.Varchar2);
            pUpdateUsername.Value = logOnUser;

            OracleParameter[] paramArray = { pParamValue, pComments, pUpdateUsername, pParamName };

            oracleDbConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
            val = db.InvokeCommand(oracleDbConn, sql, paramArray);
        }

        catch (Exception e)
        {
            EventLogUtil.Log("UpdateSysParamByName : " + e.Message);
        }
        finally
        {

            db.CloseConnection(oracleDbConn);
        }

        if (val > 0)
            return true;
        else
            return false;
    }

    public bool RemoveSysParamByName(string paramName)
    {

        DBUtil db = new DBUtil();
        OracleConnection oracleDbConn = null;

        string sql = "DELETE FROM system_param_tab WHERE param = :param";

        int val = -1;

        try
        {
            OracleParameter pParamName = new OracleParameter("param", OracleDbType.Varchar2);
            pParamName.Value = paramName.ToUpper();


            OracleParameter[] paramArray = { pParamName };

            oracleDbConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
            val = db.InvokeCommand(oracleDbConn, sql, paramArray);
        }

        catch (Exception e)
        {
            EventLogUtil.Log("RemoveSysParamByName : " + e.Message);
        }
        finally
        {

            db.CloseConnection(oracleDbConn);
        }

        if (val > 0)
            return true;
        else
            return false;
    }

    public bool RemoveKidsBookings(string kidRefNo, int unixDateBooked, string session, string logOnUser, ref string nextKidNameTag, ref string nextKidParentName, ref string nextKidParentEmail)
    {

        DBUtil db = new DBUtil();
        OracleConnection oracleDbConn = null;

        string sql = "Ifsjuniors_Util_API.CANCEL_ATTENDENCE";

        //int val = -1;

        try
        {
            OracleParameter pKidRefNo = new OracleParameter("kid_ref_no_", OracleDbType.Varchar2);
            pKidRefNo.Value = kidRefNo;

            OracleParameter pUnixDateBooked = new OracleParameter("unixdate_booked_", OracleDbType.Int32);
            pUnixDateBooked.Value = unixDateBooked;

            OracleParameter pSession = new OracleParameter("session_", OracleDbType.Varchar2);
            pSession.Value = session;

            OracleParameter pLogOnUser = new OracleParameter("LogOn_User_", OracleDbType.Varchar2);
            pLogOnUser.Value = logOnUser;

            OracleParameter pNextKidNameTag = new OracleParameter("next_kid_name_tag_", OracleDbType.Varchar2);
            pNextKidNameTag.Direction = ParameterDirection.Output;
            pNextKidNameTag.Size = 50;

            OracleParameter pNextKidParentName = new OracleParameter("next_kid_parent_name_", OracleDbType.Varchar2);
            pNextKidParentName.Direction = ParameterDirection.Output;
            pNextKidParentName.Size = 200;

            OracleParameter pNextKidParentEmail = new OracleParameter("next_kid_parent_email_", OracleDbType.Varchar2);
            pNextKidParentEmail.Direction = ParameterDirection.Output;
            pNextKidParentEmail.Size = 200;

            OracleParameter[] paramArray = { pKidRefNo, pUnixDateBooked, pSession, pLogOnUser, pNextKidNameTag, pNextKidParentName, pNextKidParentEmail };

            oracleDbConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
            db.GetExecuteStoredProcedure(sql, paramArray, oracleDbConn);

            Object val_ = null;

            val_ = pNextKidNameTag.Value;
            if (val_ != null)
                nextKidNameTag = val_.ToString();

            val_ = pNextKidParentName.Value;
            if (val_ != null)
                nextKidParentName = val_.ToString();

            val_ = pNextKidParentEmail.Value;
            if (val_ != null)
                nextKidParentEmail = val_.ToString();

            return true;
        }

        catch (Exception e)
        {
            EventLogUtil.Log("RemoveKidsBookings : " + e.Message);
        }
        finally
        {

            db.CloseConnection(oracleDbConn);
        }

        return false;
    }

    public DataTable GetKidSubDetails(int refNo = -1)
    {
        DataTable tblResults = new DataTable();

        string sqlStatement = "select name_tag, ROUND(age, 2) as age, sub_year from KIDS_INFO t ";

        string where_ = " where t.ref_no = :ref_no order by name_tag";

        string order_ = " order by name_tag";

        DBUtil db = new DBUtil(ServiceUtil.DB.DefaultDB);

        OracleDataReader dataRd = null;

        OracleParameter pRefNo = new OracleParameter("ref_no", OracleDbType.Int32);
        pRefNo.Value = refNo;

        OracleParameter[] paramArray = { pRefNo };

        try
        {
            if (refNo > 0)
            {

                dataRd = db.GetExecuteQuery(sqlStatement + where_, paramArray);
            }
            else
            {
                dataRd = db.GetExecuteQuery(sqlStatement + order_);
            }
            if (dataRd != null)
                tblResults.Load(dataRd);
        }
        catch (Exception ex)
        {
            EventLogUtil.Log("GetKidSubDetails : " + ex.Message);
        }
        finally
        {

            db.Close();
        }
        return tblResults;
    }

    public bool UpdateKidSubYear(int kidRefNo, int subYear)
    {

        DBUtil db = new DBUtil();
        OracleConnection oracleDbConn = null;

        string sql = "UPDATE KIDS_INFO_TAB SET sub_year = :sub_year WHERE ref_no = :ref_no";

        int val = -1;

        try
        {

            OracleParameter psubYear = new OracleParameter("sub_year", OracleDbType.Int32);
            psubYear.Value = subYear;

            OracleParameter pRefNo = new OracleParameter("ref_no", OracleDbType.Int32);
            pRefNo.Value = kidRefNo;

            OracleParameter[] paramArray = { psubYear, pRefNo };

            oracleDbConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
            val = db.InvokeCommand(oracleDbConn, sql, paramArray);
        }

        catch (Exception e)
        {
            EventLogUtil.Log("UpdateKidSubYear : " + e.Message);
        }
        finally
        {

            db.CloseConnection(oracleDbConn);
        }

        if (val > 0)
            return true;
        else
            return false;
    }

    public HashSet<int> GetHolidays()
    {

        int yesterDay = (int)new Manager().ConvertToUnixTimestamp(DateTime.Today.AddDays(-1));
        string sqlStatement = "select ndate from holidays_lk_tab where ndate >= " + yesterDay + " order by ndate";

        DBUtil db = new DBUtil(ServiceUtil.DB.DefaultDB);


        HashSet<Int32> hashDays = new HashSet<Int32>();

        OracleDataReader dataRd = null;
        string paramName = string.Empty;
        string paramValue = string.Empty;
        try
        {
            dataRd = db.GetExecuteQuery(sqlStatement);

            while (dataRd != null && dataRd.Read())
            {

                if (!dataRd.IsDBNull(0))
                {

                    hashDays.Add(dataRd.GetInt32(0));
                }

            }
        }
        catch (Exception ex)
        {
            EventLogUtil.Log("GetHolidays : " + ex.Message);
        }
        finally
        {

            db.Close();
        }
        return hashDays;
    }
    public DataTable GetHolidaysList(double dyearStart, double dyearEnd)
    {
        Manager mgr = new Manager();

        double dateStamp = 0;
        string sDescription = null;
        string sUpdatedBy = null;
        DateTime date_ = DateTime.Now;
        DayOfWeek sDay;

        DataTable table = new DataTable();
        table.Columns.Add("Date_Stamp", typeof(DateTime));
        table.Columns.Add("Day_of_Week", typeof(string));
        table.Columns.Add("Description", typeof(string));
        table.Columns.Add("Updated_By", typeof(string));

        string sqlStatement = "select h.ndate, h.description, h.update_by from holidays_lk_tab h WHERE ndate BETWEEN :startD AND :endD order by h.ndate";

        OracleDataReader dReader = null;

        OracleParameter pFromDate = new OracleParameter("startD", OracleDbType.Double);
        pFromDate.Value = dyearStart;

        OracleParameter pToDate = new OracleParameter("endD", OracleDbType.Double);
        pToDate.Value = dyearEnd;

        OracleParameter[] paramArray = { pFromDate, pToDate };

        DBUtil db = new DBUtil(ServiceUtil.DB.DefaultDB);

        try
        {
            dReader = db.GetExecuteQuery(sqlStatement, paramArray);

            while (dReader != null && dReader.Read())
            {
                if (!dReader.IsDBNull(0))
                    dateStamp = dReader.GetInt32(0);
                else
                    dateStamp = 0;

                if (!dReader.IsDBNull(1))
                    sDescription = dReader.GetString(1);
                else
                    sDescription = string.Empty;

                if (!dReader.IsDBNull(2))
                    sUpdatedBy = dReader.GetString(2);
                else
                    sUpdatedBy = string.Empty;


                date_ = mgr.ConvertFromUnixTimestamp(dateStamp);
                sDay = date_.DayOfWeek;

                table.Rows.Add(date_, sDay, sDescription, sUpdatedBy);
            }

            table.Load(dReader);
        }
        catch (Exception ex)
        {
            EventLogUtil.Log("GetHolidaysList : " + ex.Message);
        }
        finally
        {

            db.Close();
        }
        return table;
    }

    public bool InsertDataToHolidayTab(double nDate, string sDescription, int status, string sUpdatedBy)
    {

        DBUtil db = new DBUtil();
        OracleConnection oraConn = null;

        string sql = "INSERT INTO holidays_lk_tab (ndate, description, status, update_by, last_update ) " +
                                                    "VALUES ( :ndate, :description, :status, :update_by, sysdate )";

        int val = -1;

        try
        {
            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);


            OracleParameter pNdate = new OracleParameter("ndate", OracleDbType.Int32);
            pNdate.Value = nDate;

            OracleParameter pDescription = new OracleParameter("description", OracleDbType.Varchar2);
            pDescription.Value = sDescription;

            OracleParameter pStatus = new OracleParameter("status", OracleDbType.Int32);
            pStatus.Value = status;

            OracleParameter pActUserName = new OracleParameter("update_by", OracleDbType.Varchar2);
            pActUserName.Value = sUpdatedBy;


            OracleParameter[] paramArray = { pNdate, pDescription, pStatus, pActUserName };

            val = db.InvokeCommand(oraConn, sql, paramArray);

        }

        catch (Exception e)
        {
            EventLogUtil.Log("InsertDataToHolidayTab : " + e.Message);
            throw;
        }
        finally
        {

            db.CloseConnection(oraConn);
        }

        if (val > 0)
            return true;
        else
            return false;

    }

    public bool DeleteDataFromHolidayTab(double nDate)
    {
        DBUtil db = new DBUtil();
        OracleConnection oraConn = null;
        int val = 0;
        try
        {
            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);


            string sql = "DELETE FROM holidays_lk_tab WHERE NDATE = :ndate";

            OracleParameter pNdate = new OracleParameter("ndate", OracleDbType.Int32);
            pNdate.Value = nDate;

            OracleParameter[] paramArray = { pNdate };

            val = db.InvokeCommand(oraConn, sql, paramArray);

            if (val > 0)
                return true;
        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally
        {
            db.CloseConnection(oraConn);
        }

        return false;
    }

    /*public bool InsertKidsDetails()
    {

        DBUtil db = new DBUtil();
        OracleConnection oraConn = null;

        string sql = "INSERT INTO holidays_lk_tab (ndate, description, status, update_by, last_update ) " +
                                                    "VALUES ( :ndate, :description, :status, :update_by, sysdate )";

        int val = -1;

        try
        {
            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

           }

        catch (Exception e)
        {
            EventLogUtil.Log("InsertDataToHolidayTab : " + e.Message);
            throw;
        }
        finally
        {

            db.CloseConnection(oraConn);
        }

        if (val > 0)
            return true;
        else
            return false;

    }*/

    public int AddKidsInfo(KidsInfo kidsInfo)
    {

        DBUtil db = new DBUtil();
        OracleConnection oracleDbConn = null;

        string sql = "Ifsjuniors_Util_API.Add_Kids_Info";

        try
        {
            OracleParameter pNameTag = new OracleParameter("name_tag_", OracleDbType.Varchar2);
            pNameTag.Value = kidsInfo.NameTag;

            OracleParameter pFullName = new OracleParameter("full_name_", OracleDbType.Varchar2);
            pFullName.Value = kidsInfo.FullName;

            OracleParameter pDoB = new OracleParameter("date_of_birth_", OracleDbType.Date);
            pDoB.Value = kidsInfo.DateOfBirth;

            OracleParameter pGender = new OracleParameter("gender_", OracleDbType.Varchar2);
            pGender.Value = kidsInfo.Gender;

            OracleParameter pSubYear = new OracleParameter("sub_year_", OracleDbType.Int32);
            pSubYear.Value = kidsInfo.SubYear;

            OracleParameter pFatherName = new OracleParameter("father_name_", OracleDbType.Varchar2);
            pFatherName.Value = kidsInfo.FatherName;

            OracleParameter pFatherUserName = new OracleParameter("father_user_name_", OracleDbType.Varchar2);
            pFatherUserName.Value = string.IsNullOrEmpty(kidsInfo.FatherUserName) ? null : kidsInfo.FatherUserName;

            OracleParameter pFatherEmail = new OracleParameter("father_email_", OracleDbType.Varchar2);
            pFatherEmail.Value = kidsInfo.FatherEmail;

            OracleParameter pFatherContactNo = new OracleParameter("father_contact_no_", OracleDbType.Varchar2);
            pFatherContactNo.Value = kidsInfo.FatherContactNo;

            OracleParameter pMotherName = new OracleParameter("mother_name_", OracleDbType.Varchar2);
            pMotherName.Value = kidsInfo.MotherName;

            OracleParameter pMotherUserName = new OracleParameter("mother_user_name_", OracleDbType.Varchar2);
            pMotherUserName.Value = string.IsNullOrEmpty(kidsInfo.MotherUserName) ? null : kidsInfo.MotherUserName;

            OracleParameter pMotherEmail = new OracleParameter("mother_email_", OracleDbType.Varchar2);
            pMotherEmail.Value = kidsInfo.MotherEmail;

            OracleParameter pMotherContactNo = new OracleParameter("mother_contact_no_", OracleDbType.Varchar2);
            pMotherContactNo.Value = kidsInfo.MotherContactNo;

            OracleParameter pResidence = new OracleParameter("residence_", OracleDbType.Varchar2);
            pResidence.Value = kidsInfo.Residence;

            OracleParameter pResContactNo = new OracleParameter("res_contact_no_", OracleDbType.Varchar2);
            pResContactNo.Value = kidsInfo.ResContactNo;

            OracleParameter pNotes = new OracleParameter("notes_", OracleDbType.Varchar2);
            pNotes.Value = kidsInfo.Notes;

            OracleParameter pKidRefNo = new OracleParameter("ref_no_", OracleDbType.Int32);
            pKidRefNo.Direction = ParameterDirection.Output;
            //pKidRefNo.Size = 200;

            OracleParameter[] paramArray = { pNameTag, pFullName, pDoB, pGender, pSubYear, pFatherName, pFatherUserName, pFatherEmail, pFatherContactNo, pMotherName, pMotherUserName, pMotherEmail, pMotherContactNo, pResidence, pResContactNo, pNotes, pKidRefNo };

            EventLogUtil.Log("AddKidsInfo : " + " pNameTag : " + kidsInfo.NameTag + " pFullName :" + kidsInfo.FullName + " pDoB : " + kidsInfo.DateOfBirth + " pGender : " + kidsInfo.Gender + " pSubYear : " + kidsInfo.SubYear + " pFatherName : " + kidsInfo.FatherName + " pFatherUserName : " + kidsInfo.FatherUserName + " pFatherEmail : " + kidsInfo.FatherEmail + " pFatherContactNo : " + kidsInfo.FatherContactNo + " pKidRefNo : " + pKidRefNo);
            oracleDbConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
            db.GetExecuteStoredProcedure(sql, paramArray, oracleDbConn);


            Object val_ = null;

            val_ = pKidRefNo.Value;
            if (val_ != null)
                kidsInfo.RefNo = int.Parse(val_.ToString());

            return kidsInfo.RefNo;
        }

        catch (Exception e)
        {
            EventLogUtil.Log("AddKidsInfo : " + e.Message);
            EventLogUtil.Log(e.StackTrace);
            /*throw e;*/
            return -2;
        }
        finally
        {
            db.CloseConnection(oracleDbConn);
            //return -3;
        }

       // return -1;
    }

    public string GetParentUseNameString()
    {

        DBUtil db = new DBUtil();

        string sqlStatement = "select distinct(username) from GUARDIAN_INFO_TAB t";
        string userName = string.Empty;

        OracleConnection oConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);

        try
        {
            OracleDataReader dataRd = db.GetExecuteQuery(sqlStatement, oConn);

            while (dataRd != null && dataRd.Read())
            {

                if (!dataRd.IsDBNull(0))
                {
                    if (!string.IsNullOrEmpty(userName))
                        userName = userName + FIELD_SEPARATOR + dataRd.GetString(0).Trim();
                    else
                        userName = dataRd.GetString(0);
                }
            }

        }
        catch (Exception ex)
        {
            EventLogUtil.Log(typeof(DataManager).FullName + "GetParentUseNameString : " + ex.Message);

        }
        finally
        {


            db.CloseConnection(oConn);
        }
        return userName;
    }

    public string GetParentDetailString(string userName)
    {

        DBUtil db = new DBUtil();

        string sqlStatement = "SELECT t.father_name, t.father_contact_no, t.mother_name, t.mother_contact_no, t.residence, t.res_contact_no, t.relationship, t.email, t.username FROM KIDS_GUARDIAN_INFO t WHERE " +
                              "t.ref_no IN ( SELECT k.ref_no FROM KIDS_GUARDIAN_INFO k WHERE UPPER(k.username) = UPPER(:username) )";
        string detailsString = string.Empty;

        OracleParameter pUserName = new OracleParameter("username", OracleDbType.Varchar2);
        pUserName.Value = userName;

        OracleParameter[] paramArray = { pUserName };

        OracleConnection oConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
        try
        {
            OracleDataReader dataRd = db.GetExecuteQuery(sqlStatement, oConn, paramArray);

            while (dataRd != null && dataRd.Read())
            {

                for (int i = 0; i < dataRd.FieldCount; i++)
                {
                    if (!dataRd.IsDBNull(i))
                    {
                        if (!string.IsNullOrEmpty(detailsString))
                            detailsString = detailsString + FIELD_SEPARATOR + dataRd.GetName(i) + RECORD_SEPARATOR + dataRd.GetValue(i).ToString().Trim();
                        else
                            detailsString = dataRd.GetName(i) + RECORD_SEPARATOR + dataRd.GetValue(i).ToString().Trim();
                    }
                    else
                    {

                        if (!string.IsNullOrEmpty(detailsString))
                            detailsString = detailsString + FIELD_SEPARATOR + dataRd.GetName(i) + RECORD_SEPARATOR + null;
                        else
                            detailsString = dataRd.GetName(i) + RECORD_SEPARATOR + null;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            EventLogUtil.Log(typeof(DataManager).FullName + "GetParentDetailString : " + ex.Message);

        }
        finally
        {


            db.CloseConnection(oConn);
        }
        return detailsString;
    }

    public bool CheckExistKidNameTag(string nameTag)
    {

        string sqlStatement = "select 1 from kids_info k where UPPER(name_tag) = :param";

        DBUtil db = new DBUtil(ServiceUtil.DB.DefaultDB);

        OracleDataReader dataRd = null;

        try
        {
            OracleParameter pParamName = new OracleParameter("param", OracleDbType.Varchar2);
            pParamName.Value = nameTag.ToUpper();

            OracleParameter[] paramArray = { pParamName };

            dataRd = db.GetExecuteQuery(sqlStatement, paramArray);

            if (dataRd.HasRows)
                return true;
            else
                return false;

        }
        catch (Exception ex)
        {
            EventLogUtil.Log("CheckExistKidNameTag : " + ex.Message);
        }
        finally
        {
            db.Close();
        }
        return false;
    }

    /*===================================================================================*/

    public DataTable getkidlist_table(string sqlcommand)
    {
        DBUtil db = new DBUtil();
        OracleConnection oracleDbConn = null;
        DataTable dataTable = new DataTable();
        OracleCommand cmd = new OracleCommand();

        try
        {
            oracleDbConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
            cmd = oracleDbConn.CreateCommand();
            cmd.CommandText = sqlcommand;
            OracleDataReader dr = cmd.ExecuteReader();
            dataTable.Load(dr);
            return dataTable;
        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            db.CloseConnection(oracleDbConn);
        }
    }

    /*to be changed kidtype*/
    /*public string getkidlist_command(string date, string session, string type)
    {
        string command = "select p.KID_NAME from KIDS_INFO_TAB k, GUARDIAN_POINTS p, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED = (select to_char(sysdate-21) from dual) and t.ACTIVITY_USERNAME=upper(p.GUARDIAN_NAME) and k.REF_NO=t.Kid_Ref_No and SESSION_BOOKED = 'PM' order by t.BOOKING_STATE,k.KID_TYPE DESC,p.POINTS DESC,p.KID_NAME";
        //string command = "select p.KID_NAME from KIDS_INFO_TAB k, GUARDIAN_POINTS p, KIDS_ATTENDANCE_TAB t where  t.DATE_BOOKED = (select to_char(sysdate-21) from dual) and t.ACTIVITY_USERNAME=upper(p.GUARDIAN_NAME) and k.REF_NO=t.Kid_Ref_No and SESSION_BOOKED = 'PM' order by t.BOOKING_STATE,k.KID_TYPE DESC,p.POINTS DESC,p.KID_NAME";

        return command;
    }*/

    /*get kids name by session*/
    /*public OracleDataReader GetKidNamesTagListToSession(OracleConnection oConn)
    {
        string minAge = SysParamReaderUtil.GetSysParamByName("MIN_AGE_LIMIT");
        string maxAge = SysParamReaderUtil.GetSysParamByName("MAX_AGE_LIMIT");

        string sqlStatement = "SELECT t.name_tag, t.ref_no FROM KIDS_INFO t" +
                              " WHERE t.age >= " + minAge + " AND t.age <= " + maxAge
                              + "and t.ref_no in(select t1.kid_ref_no from KIDS_ATTENDANCE_TAB t1 where t1.date_booked = TO_DATE('11/10/2014', 'MM/DD/YYYY')) order by name_tag";
        OracleParameter pDate;

        pDate = new OracleParameter("pdate", OracleDbType.Date);
        pDate.Value = "11/19/2014";

        OracleParameter[] paramArray = { pDate };

        return new DBUtil().GetExecuteQuery(sqlStatement, oConn, paramArray);
    }*/

    /************************************ NEW METHODS ******************************/

    /*this method will add points, per attend*/
    //public void points_per_attend(string kidText)
    //{
    //    int points = Convert.ToInt32(SysParamReaderUtil.GetSysParamByName("BOOKING_PENALTY"));

    //    reduce_kids_points(kidText, points);
    //}

    /* function to set specific point value to a guardian*/
    //public void reduce_kids_points(string kidText, int value)
    //{
    //    DBUtil db = new DBUtil();
    //    OracleConnection oracleDbConn = null;
    //    int val = 0;

    //    try
    //    {
    //        oracleDbConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
    //        string sql = "Update KIDS_POINTS t1 set POINTS= POINTS + :pvalue where upper(t1.kid_name) = upper(:pkid)";

    //        OracleParameter pkid = new OracleParameter("pkid", OracleDbType.Varchar2);
    //        pkid.Value = kidText;

    //        OracleParameter pvalue = new OracleParameter("pvalue", OracleDbType.Varchar2);
    //        pvalue.Value = value;

    //        OracleParameter[] paramArray = { pvalue, pkid };

    //        val = db.InvokeCommand(oracleDbConn, sql, paramArray);
    //    }

    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        db.CloseConnection(oracleDbConn);
    //    }

    //}

    /*================================= functions to increase and decrease counts in stat table ===========================================*/

    public void addcount_refno(string refno, string column)
    {
        DBUtil db = new DBUtil();
        OracleConnection oracleDbConn = null;

        try
        {
            string sql = "Update KIDS_STATS_TAB t set " + column + "=" + column + "+1 where t.REF_NO=" + refno;
            oracleDbConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
            db.InvokeCommand(oracleDbConn, sql);
        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            db.CloseConnection(oracleDbConn);
        }

    }

    public void reducecount_name(string name, string column)
    {
        DBUtil db = new DBUtil();
        OracleConnection oracleDbConn = null;

        try
        {
            string sql = "Update KIDS_STATS_TAB t set " + column + "=" + column + "-1 where t.KID_NAME='" + name+"'";
            oracleDbConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
            db.InvokeCommand(oracleDbConn, sql);
        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            db.CloseConnection(oracleDbConn);
        }

    }

    public void addcount_name(string name, string column)
    {
        DBUtil db = new DBUtil();
        OracleConnection oracleDbConn = null;
        
        try
        {
            oracleDbConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
            string sqlStatement = "Update KIDS_STATS_TAB t set " + column + "=" + column + "+1 where KID_NAME='" + name + "'";
            db.GetExecuteQuery(sqlStatement, oracleDbConn);
        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            db.CloseConnection(oracleDbConn);
        }

    }
}