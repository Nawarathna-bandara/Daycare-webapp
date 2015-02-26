<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Attendance.aspx.cs" Inherits="AdminPortal_Attendance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="mainContent" Runat="Server">
    
    <div class="btn-group" role="group" aria-label="...">
     <asp:LinkButton ID="LinkButton1" class= "btn btn-primary" onclick="Attendance_Click" runat="server"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>&nbsp;Attendance</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton2" class= "btn btn-default" onclick="Stats_Click" runat="server"><span class="glyphicon glyphicon-stats" aria-hidden="true"></span>&nbsp;Stats</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton3" class= "btn btn-default" onclick="Contacts_Click" runat="server"><span class="glyphicon glyphicon-earphone" aria-hidden="true"></span>&nbsp;Contacts</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton4" class= "btn btn-default" onclick="Cancellations_Click" runat="server"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Cancellations</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton5" class= "btn btn-default" onclick="Kidsinfo_Click" runat="server"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;Kids info</asp:LinkButton>                                                                                      
     <asp:LinkButton ID="LinkButton6" class= "btn btn-default" onclick="Holidays_Click" runat="server"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>&nbsp;Holidays</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton7" class= "btn btn-default" onclick="Renewsub_Click" runat="server"><span class="glyphicon glyphicon-repeat" aria-hidden="true"></span>&nbsp;Subscription</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton8" class= "btn btn-default" onclick="Sysparam_Click" runat="server"><span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>&nbsp;Parameters</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton9" class= "btn btn-default" onclick="Addkids_Click" runat="server"><span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span>&nbsp;Add Kids</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton10" class= "btn btn-default" onclick="Admins_Click" runat="server"><span class="glyphicon glyphicon-wrench" aria-hidden="true"></span>&nbsp;App</asp:LinkButton>                                             

    </div>

    <p style="text-align:center"><br/><br/>
     Select Date : &nbsp;<asp:TextBox ID="date" runat="server"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="date_t" runat="server" 
        TargetControlID="date"
        CssClass="AtdCalendar"       
        Format="MMMM d, yyyy" />&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="Select" OnClick="Button1_Click" />

    </p>

    <table class="table" border="0">
        
        <tr><td> &nbsp;</td><td colspan="2">
           
            </td></tr>

        <tr style="page-break-inside: avoid;">
            
            <td class="auto-style2"></td>
            
            <td class="auto-style3">


                <div class="panel panel-primary">
                  <div class="panel-heading"> AM </div>
                  <div class="panel-body" style="text-align:left"> 
                        
                          
                    <asp:Label ID="Label5" runat="server" class="label label-success" Text="Present"></asp:Label>
                          
                        <br />
                      <br />
                          
                        <asp:GridView ID="AMgrid_p" runat="server" AutoGenerateColumns="False" GridLines="None" ShowHeader="False" CellSpacing="2" style="text-align:left">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />&nbsp;
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NAME_TAG" HeaderText="name" SortExpression="name" />
                        </Columns>
                       </asp:GridView>                                     

                       <br />

                       <asp:Button ID="AMbtn_p" runat="server" onclick="AMp_Click" Text="Mark Absent" />
        
                       <hr />

                       <asp:Label ID="Label7" runat="server" class="label label-danger" Text="Absent"></asp:Label>
                       <br />
                      <br />


                        <asp:GridView ID="AMgrid_a" runat="server" AutoGenerateColumns="False" GridLines="None" ShowHeader="False" CellSpacing="2" style="text-align:left">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />&nbsp;
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NAME_TAG" HeaderText="name" SortExpression="name" />
                            </Columns>
                        </asp:GridView>

                       <br />
                       <asp:Button ID="AMbtn_a" runat="server" onclick="AMa_Click" Text="Mark Present" />
       
                    </div>
                </div>

            </td>


            <td class="auto-style3">
                
             <div class="panel panel-primary">
                  <div class="panel-heading"> PM </div>
                  <div class="panel-body" style="text-align:left"> 
                        
                          
                          <asp:Label ID="Label6" runat="server" class="label label-success" Text="Present"></asp:Label>
                          
                            <br />
                      <br />
                          
                            <asp:GridView ID="PMgrid_p" runat="server" AutoGenerateColumns="False" GridLines="None" ShowHeader="False" CellSpacing="2" style="text-align:left">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />&nbsp;
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NAME_TAG" HeaderText="name" SortExpression="name" />
                            </Columns>
                        </asp:GridView>

                          <br />

                      <asp:Button ID="PMbtn_p" runat="server" onclick="PMp_Click" Text="Mark Absent"/>
        
                            <hr />

                            <asp:Label ID="Label8" runat="server" class="label label-danger" Text="Absent"></asp:Label>

                      <br />
                      <br />


                        <asp:GridView ID="PMgrid_a" runat="server" AutoGenerateColumns="False" GridLines="None" ShowHeader="False" CellSpacing="2" style="text-align:left">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />&nbsp;
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NAME_TAG" HeaderText="name" SortExpression="name" />
                            </Columns>
                        </asp:GridView>

                            <br />
                            <asp:Button ID="PMbtn_a" runat="server" onclick="PMa_Click" Text="Mark Present"/>

                  </div>
                </div>  
            
            </td>
            
            <td class="auto-style3"></td>
        </tr>
        
        

    </table>

</asp:Content>
