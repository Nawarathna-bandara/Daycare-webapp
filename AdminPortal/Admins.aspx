<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Admins.aspx.cs" Inherits="AdminPortal_Admins" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%--<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">--%>
<%--</asp:Content>--%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="mainContent" Runat="Server">
   
    <div class="btn-group" role="group" aria-label="...">
     <asp:LinkButton ID="LinkButton1" class= "btn btn-default" onclick="Attendance_Click" runat="server"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>&nbsp;Attendance</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton2" class= "btn btn-default" onclick="Stats_Click" runat="server"><span class="glyphicon glyphicon-stats" aria-hidden="true"></span>&nbsp;Stats</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton3" class= "btn btn-default" onclick="Contacts_Click" runat="server"><span class="glyphicon glyphicon-earphone" aria-hidden="true"></span>&nbsp;Contacts</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton4" class= "btn btn-default" onclick="Cancellations_Click" runat="server"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Cancellations</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton5" class= "btn btn-default" onclick="Kidsinfo_Click" runat="server"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;Kids</asp:LinkButton>                                                                                      
     <asp:LinkButton ID="LinkButton6" class= "btn btn-default" onclick="Holidays_Click" runat="server"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>&nbsp;Holidays</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton7" class= "btn btn-default" onclick="Renewsub_Click" runat="server"><span class="glyphicon glyphicon-repeat" aria-hidden="true"></span>&nbsp;Subscription</asp:LinkButton>                                                                                      
     <asp:LinkButton ID="LinkButton8" class= "btn btn-default" onclick="Sysparam_Click" runat="server"><span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>&nbsp;Parameters</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton9" class= "btn btn-default" onclick="Addkids_Click" runat="server"><span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span>&nbsp;Add Kids</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton10" class= "btn btn-primary" onclick="Admins_Click" runat="server"><span class="glyphicon glyphicon-wrench" aria-hidden="true"></span>&nbsp;App</asp:LinkButton>                                             

    </div>

    <table style="width:100%;padding:10px;margin-top:30px">
 
        <tr>
            <td style="padding:10px;vertical-align:top;width:60%;" rowspan="2" class="auto-style2">

                <div class="panel panel-primary">
                  <div class="panel-heading"> Android App Users </div>
                  <div class="panel-body">

                <asp:UpdatePanel ID="panel" runat="server" UpdateMode="Conditional">

                <Triggers>
                    <asp:AsyncPostBackTrigger controlid="Button" eventname="Click" /> 
                    <asp:AsyncPostBackTrigger controlid="Button1" eventname="Click" />                   
                </Triggers>  
                                         
                <ContentTemplate>
                <asp:GridView ID="admingrid" runat="server" CssClass="mGrid" AutoGenerateColumns="False">

                    <Columns>
                        <asp:BoundField DataField="USERNAME" HeaderText="Username" SortExpression="name" />
                        <asp:BoundField DataField="EMAIL" HeaderText="E mail" SortExpression="name" />

                         <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Checkbox ID="delbox" HeaderText="Remove" runat="server"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                </asp:GridView>      
                
                </ContentTemplate>
                </asp:UpdatePanel>

                                                                                            
                <asp:Button ID="Button1" runat="server" Text="Remove Users" Autopostback="true" OnClick="del_Click" />                       
                                     
                   </div>
                </div> 
            </td>

            <td style="padding:10px;text-align:right">
                <div class="panel panel-primary">
                     <div class="panel-heading"> Add new user </div>
                  <div class="panel-body">
                                                                          
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">

                        <Triggers>
                            <asp:AsyncPostBackTrigger controlid="Button" eventname="Click" /> 
                        </Triggers>  
                                         
                        <ContentTemplate>

                            Name &nbsp;<asp:TextBox ID="name" runat="server"></asp:TextBox><br/><br/>
                            Password &nbsp;<input id="Password0" runat="server" type="password" /><br/><br/>
                            Re-enter Parrword &nbsp;<input id="Password1" runat="server" type="password" /><br/><br/>
                            E mail &nbsp;<asp:TextBox ID="email" runat="server"></asp:TextBox>
                            <br />
                            <br/> 

                            <div runat="server" id="msg1" class="alert alert-danger" role="alert" ><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true" ></span> Passwords don't match </div>
                            <div runat="server" id="msg3" class="alert alert-danger" role="alert" ><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true" ></span> Password cannot be empty </div>
                            <div runat="server" id="msg4" class="alert alert-danger" role="alert" ><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true" ></span> Enter Username </div>             
                            <div runat="server" id="msg2" class="alert alert-success" role="alert" ><span class="glyphicon glyphicon-ok" aria-hidden="true" ></span> User successfully added </div>     
                        </ContentTemplate>

                    </asp:UpdatePanel> 
                                                                            
                <asp:Button ID="Button" runat="server" Text="ADD" OnClick="Button_Click" Autopostback="true"/>                       
                                     
                    <br/>
                </div></div>
            </td>
          </tr>
            <tr>
                <td>

                    &nbsp;</td>
            </tr> 
    </table>



</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">

    </style>
</asp:Content>
