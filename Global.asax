<%@ Application Language="C#" %>
<%@ Import Namespace="IFSJunirosAttendanceForeCast" %>
<%@ Import Namespace="Oracle.DataAccess.Client" %>
<%@ Import Namespace="IFS.CoS.ServiceUtil" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        AuthConfig.RegisterOpenAuth();
        
        DataManager dtMgr = new DataManager();

        OracleConnection oraConn = null;
        DBUtil db = new DBUtil();
        try
        {

            oraConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
            OracleDataReader dataRd = dtMgr.GetGuardianUserNameList(oraConn);
            ArrayList arrUserNames = new ArrayList();
            ArrayList arrAdminUsers = new ArrayList();
            ArrayList arrSuperAdminUsers = new ArrayList();

            while (dataRd != null && dataRd.Read())
            {

                if (!dataRd.IsDBNull(0))
                {
                    arrUserNames.Add(dataRd.GetString(0));
                }
            }

            Application["GUARDIAN_USERNAMES"] = arrUserNames;

            if (dataRd != null)
                dataRd.Close();

            dataRd = dtMgr.GetAdminNameList(oraConn);

            string tmpUser = string.Empty;
            int tmpIsAdmin = 0;
            
            while (dataRd != null && dataRd.Read())
            {
                if (!dataRd.IsDBNull(0))
                {
                    tmpUser = dataRd.GetString(0);

                    if (!dataRd.IsDBNull(1))
                    {
                        tmpIsAdmin = Convert.ToInt32( dataRd.GetValue(1) );
                        //EventLogUtil.Log(LogOnUser);
                    }
                    else {

                        tmpIsAdmin = 0;
                    }
                    
                    if (tmpIsAdmin > 0)
                    {
                        arrAdminUsers.Add(tmpUser);
                        arrSuperAdminUsers.Add(tmpUser);
                        EventLogUtil.Log(tmpUser + " admin user- super");
                    }
                    else
                        arrAdminUsers.Add(tmpUser);
                }
            }

            Application["ADMIN_USERNAMES"] = arrAdminUsers;
            Application["SUPER_ADMIN_USERNAMES"] = arrSuperAdminUsers;

            Application["HOLIDAYS"] = dtMgr.GetHolidays();
        }
        catch (Exception ex) {

            EventLogUtil.Log("Application Start : " + ex.Message); 
        }
        finally {
            
            db.CloseConnection(oraConn);
        }
        
    }
    
    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
    }

    void Session_Start(object sender, EventArgs e) {

        string Auth_User = Request.ServerVariables["LOGON_USER"];
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

       //Session["LOGON_USER"] = "THIRLK";
    }
    

</script>
