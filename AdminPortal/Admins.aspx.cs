using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using IFS.CoS.ServiceUtil;
using Oracle.DataAccess.Client;
//using System.Data;

public partial class AdminPortal_Admins : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!new Manager().IsAdmin(new Manager().GetLogOnUser(this.Page)))
        {
            Response.Redirect("~/Default.aspx");
        }

        msg1.Visible = false;
        msg2.Visible = false;
        msg3.Visible = false;
        msg4.Visible = false;

        DataManager mgr = new DataManager();

        //string sql = "select USERNAME,EMAIL from EXTERNALUSERS";

        if (!IsPostBack)
        {
            string sql = "select * from EXTERNALUSERS";
            admingrid.DataSource = mgr.getkidlist_table(sql);
            admingrid.DataBind();
        }

    }

    public string CalculateMD5Hash(string input)
    {
        // step 1, calculate MD5 hash from input
        MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);

        // step 2, convert byte array to hex string
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }

    public void adduser(string name,string password,string email)
    {

        DBUtil db = new DBUtil();
        OracleConnection oracleDbConn = null;

        string sql = "INSERT INTO EXTERNALUSERS ( username, password, EMAIL) VALUES( '"+name+"', '"+CalculateMD5Hash(password)+"', '"+email+"') ";
        try
        {
            oracleDbConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
            db.GetExecuteQuery(sql, oracleDbConn);
        }

        catch (Exception e)
        {
            EventLogUtil.Log("Adding new user to Android app : " + e.Message);
        }
        finally
        {

            db.CloseConnection(oracleDbConn);
        }

    }

    public void removeuser(string name)
    {

        DBUtil db = new DBUtil();
        OracleConnection oracleDbConn = null;

        string sql = "DELETE FROM EXTERNALUSERS WHERE username='"+name+"'";
        
        try
        {
            oracleDbConn = db.OpenConnection(ServiceUtil.DB.DefaultDB);
            db.GetExecuteQuery(sql, oracleDbConn);
        }

        catch (Exception e)
        {
            EventLogUtil.Log("removing Android app user : " + e.Message);
        }
        finally
        {
            db.CloseConnection(oracleDbConn);
        }

    }

    protected void Button_Click(object sender, EventArgs e)
    {
        DataManager mgr = new DataManager();

        if (name.Text.Equals("")){
            msg4.Visible = true;
            return;
        }
        if (Password0.Value.Equals("")) {
            msg3.Visible = true;
            return;
        }

        if (Password0.Value != Password1.Value)
        {
            msg1.Visible = true;
            return;
        }
        else {
            adduser(name.Text,Password0.Value,email.Text);
            msg2.Visible = true;
            name.Text = "";
            Password0.Value = "";
            Password1.Value = "";
            email.Text = "";
        }

        string sql = "select * from EXTERNALUSERS";
        admingrid.DataSource = mgr.getkidlist_table(sql);
        admingrid.DataBind();
    }


    protected void del_Click(object sender, EventArgs e)
    {
        DataManager dm = new DataManager();

        foreach (GridViewRow item in admingrid.Rows)
        {
            CheckBox chk = (CheckBox)item.FindControl("delbox");
            if (chk.Checked)
            {
                removeuser(item.Cells[0].Text);
            }    
        }

        string sql = "select * from EXTERNALUSERS";
        admingrid.DataSource = dm.getkidlist_table(sql);
        admingrid.DataBind();
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