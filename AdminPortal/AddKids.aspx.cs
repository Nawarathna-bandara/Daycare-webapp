using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPortal_AddKids : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!new Manager().IsAdmin(new Manager().GetLogOnUser(this.Page)))
        {
            Response.Redirect("~/Default.aspx");
        }

        if (!Page.IsPostBack) {

            rdoNewParent.Checked = true;
        }

    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        KidsInfo kidsInfo = new KidsInfo();
        kidsInfo.NameTag = txtKidNameTag.Text;
        kidsInfo.FullName = txtKidFullName.Text;
        kidsInfo.DateOfBirth = Convert.ToDateTime( txtKidDob.Text);
        kidsInfo.Gender = DropDownGender.SelectedValue;
        kidsInfo.SubYear = DropDownYear.SelectedValue;
        kidsInfo.FatherName = txtFatherName.Text;
        kidsInfo.FatherUserName = txtFatherIFSUser.Text;
        kidsInfo.FatherEmail = txtFatherEmail.Text;
        kidsInfo.FatherContactNo = txtFatherContact.Text;
        kidsInfo.MotherName = txtMotherName.Text;
        kidsInfo.MotherUserName = txtMotherIFSUser.Text;
        kidsInfo.MotherEmail = txtMotherEmail.Text;
        kidsInfo.MotherContactNo = txtMotherContact.Text;
        kidsInfo.Residence = txtAddress.Text;
        kidsInfo.ResContactNo = txtContactNumber.Text;
        kidsInfo.Notes = txtNote.Text;

        this.ClearForm(Page.Form.Controls);
        
        DataManager dtMgr = new DataManager();
        try
        {
            int refNo = dtMgr.AddKidsInfo(kidsInfo);
            lbMessage.Visible = true;

            if (refNo > 0)
            {

                this.txtRefNo.Text = refNo.ToString();
                lbMessage.Text = "Sucessfully Saved the Data. Please Refer the Reference Number.";
                lbMessage.ForeColor = System.Drawing.Color.Green;
                this.ClearForm(Page.Form.Controls);
                
                Manager mgr = new Manager();
                /**/
                string fatherEmail = string.IsNullOrEmpty(kidsInfo.FatherEmail) ? string.Empty : (kidsInfo.FatherEmail );
                string toEmail = string.Empty;

                if (!string.IsNullOrEmpty(kidsInfo.FatherEmail))
                    toEmail = kidsInfo.FatherEmail + (string.IsNullOrEmpty(kidsInfo.MotherEmail) ? string.Empty : (";" + kidsInfo.MotherEmail));
                else
                    toEmail = kidsInfo.MotherEmail;
                try
                {
                    mgr.SendNewRegistrationEmailToParent(toEmail, kidsInfo.NameTag, mgr.GetLogOnUser(this.Page));
                    HttpRuntime.UnloadAppDomain();
                }
                catch (Exception ex)
                { }
            }
            else
            {
                lbMessage.Text = "Failed to Saved the Data. Please try again later.";
                lbMessage.ForeColor = System.Drawing.Color.Red;
            }

        }
        catch (Exception ex)
        {
            
             lbMessage.Text = "Failed to Saved the Data to Database. </br> Exception : " + ex.Message;
             lbMessage.ForeColor = System.Drawing.Color.Red;
        }

    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/AdminPortal/");
    }

    private void ClearForm(ControlCollection controls) {

        foreach (Control c in controls)
        {
            if (c.GetType() == typeof(System.Web.UI.WebControls.TextBox))
            {
                System.Web.UI.WebControls.TextBox t = (System.Web.UI.WebControls.TextBox)c;
                if( t.ID != txtRefNo.ID )
                    t.Text = String.Empty;
            }
            else if (c.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
            {
                System.Web.UI.WebControls.DropDownList t = (System.Web.UI.WebControls.DropDownList)c;
                t.ClearSelection();
            }
            else if (c.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
            {
                System.Web.UI.WebControls.CheckBox t = (System.Web.UI.WebControls.CheckBox)c;
                t.Checked = false;
            }

            if (c.Controls.Count > 0) ClearForm(c.Controls);
        }
    }

    [WebMethod]
    public static string GetParentUseNames()
    {
        DataManager dtMgr = new DataManager();
        return dtMgr.GetParentUseNameString();
    }

    [WebMethod]
    public static string GetParentDetails( string userName)
    {
        DataManager dtMgr = new DataManager();
        return dtMgr.GetParentDetailString( userName);
    }

    [WebMethod]
    public static bool CheckExistKidNameTag(string nameTag)
    {
        bool isExist = false;
        DataManager dtMgr = new DataManager();
        try
        {
            isExist = dtMgr.CheckExistKidNameTag(nameTag);
        }
        catch (Exception)
        {            
             throw new Exception("Internal Server Error. Please try again later.");;
        }
        if (isExist)
        {
            throw new Exception("The Name Tag is already in used. Please use different Name.");
        }

        return isExist;
    }
    protected void btUserLoad_Click(object sender, EventArgs e)
    {

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