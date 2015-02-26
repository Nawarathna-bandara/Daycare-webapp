<%@ Page Title="Register Kids" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddKids.aspx.cs" Inherits="AdminPortal_AddKids" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <link rel="stylesheet" href="../Content/themes/jquery-ui-1.10.4.custom.css"/>
    <script src="../Scripts/jquery-ui-1.10.4.custom.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.11.1/jquery.validate.min.js"></script>

    <script src="../Scripts/site.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

    <div class="btn-group" role="group" aria-label="...">
     <asp:LinkButton ID="LinkButton1" class= "btn btn-default" onclick="Attendance_Click" runat="server"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>&nbsp;Attendance</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton2" class= "btn btn-default" onclick="Stats_Click" runat="server"><span class="glyphicon glyphicon-stats" aria-hidden="true"></span>&nbsp;Stats</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton3" class= "btn btn-default" onclick="Contacts_Click" runat="server"><span class="glyphicon glyphicon-earphone" aria-hidden="true"></span>&nbsp;Contacts</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton4" class= "btn btn-default" onclick="Cancellations_Click" runat="server"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Cancellations</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton5" class= "btn btn-default" onclick="Kidsinfo_Click" runat="server"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;Kids info</asp:LinkButton>                                                                                      
     <asp:LinkButton ID="LinkButton6" class= "btn btn-default" onclick="Holidays_Click" runat="server"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>&nbsp;Holidays</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton7" class= "btn btn-default" onclick="Renewsub_Click" runat="server"><span class="glyphicon glyphicon-repeat" aria-hidden="true"></span>&nbsp;Subscription</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton8" class= "btn btn-default" onclick="Sysparam_Click" runat="server"><span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>&nbsp;Parameters</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton9" class= "btn btn-primary" onclick="Addkids_Click" runat="server"><span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span>&nbsp;Add Kids</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton10" class= "btn btn-default" onclick="Admins_Click" runat="server"><span class="glyphicon glyphicon-wrench" aria-hidden="true"></span>&nbsp;App</asp:LinkButton>                                             
    </div>
 
 <div style="text-align:left" >
    
     <div class="topContentFrame divMsg" align="center" style="padding-top:20px; padding-bottom:20px;" >           
          <asp:Label ID="lbMessage" runat="server" Visible="false"  Font-Size="0.9em" style="margin:auto;"></asp:Label>
    </div>

     <div style="margin-top:2px;padding:10px;" class ="topContentFrame"> 

    <%--<div class ="topContentFrame" style="padding:10px;"> --%>

        <div class="panel panel-primary">
    <div class="panel-heading"> Kids Details </div>
    <div class="panel-body_main">
        
        <div class="divTable">

            <div class="divRow">               
                <div class="divCell">Reference Number</div>
                <div class="divCel2"><asp:TextBox ID="txtRefNo" runat="server" Enabled="false"></asp:TextBox></div>
            </div>

            <div class="divRow" id="divNameTag">
                <div class="divCell">Name Tag</div>
                <div class="divCel2"><asp:TextBox ID="txtKidNameTag" runat="server" class="required"></asp:TextBox></div>
            </div>
            <div class="divRow">
                <div class="divCell">Full Name</div>
                <div class="divCel2"><asp:TextBox ID="txtKidFullName" runat="server" Width="500px" TextMode="MultiLine" class="required"></asp:TextBox></div>
            </div>
            <div class="divRow">
                <div class="divCell">Date of Birth</div>
                <div class="divCel2"><asp:TextBox ID="txtKidDob" runat="server" class="required"></asp:TextBox></div>
           </div>
           <div class="divRow">
                <div class="divCell">Gender</div>
                <div class="divCel2"><asp:DropDownList ID="DropDownGender" runat="server" Width="120px" class="required" >
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem>Male</asp:ListItem>
                                        <asp:ListItem>Female</asp:ListItem>

                                     </asp:DropDownList>
                </div>
           </div>
            <div class="divRow">
                <div class="divCell">Subscription Year</div>
                <div class="divCel2">
                     <asp:DropDownList ID="DropDownYear" runat="server" Width="120px" class="required" >
                                        <asp:ListItem Value="">Select Year</asp:ListItem>
                                        <asp:ListItem>2014</asp:ListItem>
                                        <asp:ListItem>2015</asp:ListItem>
                                        <asp:ListItem>2016</asp:ListItem>
                                        <asp:ListItem>2017</asp:ListItem>
                                        <asp:ListItem>2018</asp:ListItem>
                                        <asp:ListItem>2019</asp:ListItem>
                                        <asp:ListItem>2020</asp:ListItem>
                                        <asp:ListItem>2021</asp:ListItem>
                                        <asp:ListItem>2022</asp:ListItem>
                                        <asp:ListItem>2023</asp:ListItem>
                                        <asp:ListItem>2024</asp:ListItem>
                                        <asp:ListItem>2025</asp:ListItem>
                      </asp:DropDownList> 
                </div>
           </div>

      </div>
    </div>
            </div>
    </div>
    
    <div style="margin-top:2px;padding:10px;" class ="topContentFrame"> 

       
        <div class="panel panel-primary">
    <div class="panel-heading"> Parent Details </div>
    <div class="panel-body_main">
        <div class="divTable">

           

           <div class="divRow">
                <div class="divCell"> </div>
                <div class="divCel2">New Parent <asp:RadioButton ID="rdoNewParent" runat="server" Checked="true" GroupName="ParentsType" />  &nbsp;Registered Parent <asp:RadioButton ID="rdoRegParent" runat="server" GroupName="ParentsType"/> </div>
           </div>

           

           <div class="divRow">
                <div class="divCell"> </div>
                <div class="divCel2"> </div>
           </div>

           <div class="divRow" id="IFSUserName">
                <div class="divCell"> IFS Username ( If both are at IFS, enter one )</div>
                <div class="divCel2"><asp:TextBox ID="txtIFSUserName" runat="server"  MaxLength="6"></asp:TextBox> &nbsp;  <asp:Button ID="btUserLoad" runat="server" Text="Load" class="cancel" Width="80px" /> </div>
           </div>

           <div class="divRow">
                <div class="divCell">Father's Name</div>
                <div class="divCel2"><asp:TextBox ID="txtFatherName" runat="server" Width="300px" TextMode="MultiLine" class="required"></asp:TextBox></div>
            </div>
           
           <div class="divRow">
                <div class="divCell">Is IFS User <asp:CheckBox ID="chkIsFatherIFS" runat="server" />   Username</div>
                <div class="divCel2"> <asp:TextBox ID="txtFatherIFSUser" runat="server"  MaxLength="6"></asp:TextBox></div>
           </div>

           <div class="divRow">
                <div class="divCell">IFS Email </div>
                <div class="divCel2"> <asp:TextBox ID="txtFatherEmail" runat="server"  Width="500px" TextMode="Email"></asp:TextBox></div>
           </div>

           <div class="divRow">
                <div class="divCell">Father's Contact Number</div>
                <div class="divCel2"><asp:TextBox ID="txtFatherContact" runat="server" TextMode="Phone" MaxLength="15" class="required"></asp:TextBox></div>
           </div>

           <div class="divRow">
                <div class="divCell">Mother's Name </div>
                <div class="divCel2"><asp:TextBox ID="txtMotherName" runat="server" Width="300px" TextMode="MultiLine" class="required"></asp:TextBox></div>
           </div>

           <div class="divRow">
                <div class="divCell">Is IFS User <asp:CheckBox ID="chkIsMotherIFS" runat="server" />   Username</div>
                <div class="divCel2"> <asp:TextBox ID="txtMotherIFSUser" runat="server" MaxLength="6"></asp:TextBox></div>
           </div>

          <div class="divRow">
                <div class="divCell">IFS Email </div>
                <div class="divCel2"> <asp:TextBox ID="txtMotherEmail" runat="server" Width="500px" TextMode="Email"></asp:TextBox></div>
           </div>

           <div class="divRow">
                <div class="divCell">Mother's Contact Number</div>
                <div class="divCel2"><asp:TextBox ID="txtMotherContact" runat="server" TextMode="Phone" MaxLength="15" class="required"></asp:TextBox></div>
           </div>
          
      </div>
    </div>
     </div>
    </div>
     </div>

    <div style="margin-top:2px; padding:10px;" class ="topContentFrame"> 

        <div class="panel panel-primary">
    <div class="panel-heading"> Residence </div>
    <div class="panel-body_main">

        <div class="divTable">

            <div class="divRow">
                <div class="divCell">Address</div>
                <div class="divCel2"><asp:TextBox ID="txtAddress" runat="server" Width="500px" TextMode="MultiLine" class="required"></asp:TextBox></div>
            </div>
            <div class="divRow">
                <div class="divCell">Contact Number</div>
                <div class="divCel2"><asp:TextBox ID="txtContactNumber" runat="server" TextMode="Phone"></asp:TextBox></div>
           </div>
           <div class="divRow">
                <div class="divCell">Notes</div>
                <div class="divCel2"><asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Width="500px"></asp:TextBox></div>
           </div>
        </div>
    </div>

        <div style="margin-top:2px; padding:10px;" class ="topContentFrame"> 
        <div class="divTable">
            <div class="divRow">               
                <div class="divCell"> </div>
                <div class="divCell">
                    <div>
                        <asp:Button ID="btCancel" runat="server" Text="Cancel" class="cancel" Width="80px" OnClick="btCancel_Click" />
                        <asp:Button ID="btSave" runat="server" class="required" Text="Save" Width="80px" OnClick="btSave_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
      </div>
    </div>
 

</asp:Content>

