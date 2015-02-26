<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Stats.aspx.cs" Inherits="AdminPortal_Overview" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="mainContent" Runat="Server">
   
    
     <div class ="topContentFrame" >
    

    <div class="btn-group" role="group" aria-label="...">
     <asp:LinkButton ID="LinkButton1" class= "btn btn-default" onclick="Attendance_Click" runat="server"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>&nbsp;Attendance</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton2" class= "btn btn-primary" onclick="Stats_Click" runat="server"><span class="glyphicon glyphicon-stats" aria-hidden="true"></span>&nbsp;Stats</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton3" class= "btn btn-default" onclick="Contacts_Click" runat="server"><span class="glyphicon glyphicon-earphone" aria-hidden="true"></span>&nbsp;Contacts</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton4" class= "btn btn-default" onclick="Cancellations_Click" runat="server"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Cancellations</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton5" class= "btn btn-default" onclick="Kidsinfo_Click" runat="server"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;Kids info</asp:LinkButton>                                                                                      
     <asp:LinkButton ID="LinkButton6" class= "btn btn-default" onclick="Holidays_Click" runat="server"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>&nbsp;Holidays</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton7" class= "btn btn-default" onclick="Renewsub_Click" runat="server"><span class="glyphicon glyphicon-repeat" aria-hidden="true"></span>&nbsp;Subscription</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton8" class= "btn btn-default" onclick="Sysparam_Click" runat="server"><span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>&nbsp;Parameters</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton9" class= "btn btn-default" onclick="Addkids_Click" runat="server"><span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span>&nbsp;Add Kids</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton10" class= "btn btn-default" onclick="Admins_Click" runat="server"><span class="glyphicon glyphicon-wrench" aria-hidden="true"></span>&nbsp;App</asp:LinkButton>                                             
    </div>
    </div>
    <table>

       <tr>

            <td style="padding:20px; vertical-align:top; width:auto" rowspan="2">

           

                <div class="panel panel-default">

                 <div class="panel-heading" style="text-align:left"> <span class="glyphicon glyphicon-align-left" aria-hidden="true"></span>&nbsp;&nbsp;Sort By </div>

                  <div class="panel-body" style="text-align:left">                  
                    
                    <asp:RadioButton ID="sortby_points" GroupName="sortby" runat="server" AutoPostBack="true">
                    </asp:RadioButton>&nbsp;Points<br/>
                    
                    <asp:RadioButton ID="sortby_refno" GroupName="sortby" runat="server" AutoPostBack="true">
                    </asp:RadioButton>&nbsp;Ref No <br/>
                     
                    <asp:RadioButton ID="sortby_name" GroupName="sortby" runat="server" AutoPostBack="true">
                    </asp:RadioButton>&nbsp;Name<br/> 

                      <asp:RadioButton ID="sortby_bookings" GroupName="sortby" runat="server" AutoPostBack="true">
                    </asp:RadioButton>&nbsp;Bookings<br/> 

                      <asp:RadioButton ID="sortby_fixes" GroupName="sortby" runat="server" AutoPostBack="true">
                    </asp:RadioButton>&nbsp;Fixes<br/> 

                       <asp:RadioButton ID="sortby_present" GroupName="sortby" runat="server" AutoPostBack="true">
                    </asp:RadioButton>&nbsp;Present<br/> 

                      <asp:RadioButton ID="sortby_absent" GroupName="sortby" runat="server" AutoPostBack="true">
                    </asp:RadioButton>&nbsp;Absent<br/> 

                      <asp:RadioButton ID="sortby_cancelled" GroupName="sortby" runat="server" AutoPostBack="true">
                    </asp:RadioButton>&nbsp;Cancelled<br/> 

                      <asp:RadioButton ID="sortby_bulk" GroupName="sortby" runat="server" AutoPostBack="true">
                    </asp:RadioButton>&nbsp;Bulk Cancels<br/> 

                  </div>
                </div>

                 <div class="panel panel-default">

                 <div class="panel-heading" style="text-align:left"> <span class="glyphicon glyphicon-check" aria-hidden="true"></span>&nbsp; Select &nbsp; 

                 </div>

                  <div class="panel-body">                  
                                                        
                      <asp:CheckBox ID="toddler_chk" runat="server" AutoPostBack="true" ValidationGroup="a"/> Toddlers <br/>
                      <asp:CheckBox ID="elder_chk" runat="server" AutoPostBack="true" ValidationGroup="a"/> Elders

                  </div>
                </div>

                <div class="panel panel-default">

                 <div class="panel-heading" style="text-align:left"> <span class="glyphicon glyphicon-sort-by-attributes-alt" aria-hidden="true"></span>&nbsp; Order &nbsp; 

                 </div>

                  <div class="panel-body">                  
                                                        
                      <asp:CheckBox ID="Order" runat="server" AutoPostBack="true" ValidationGroup="a"/> Descending <br/>
                      
                  </div>
                </div>


                <div class="panel panel-default">

                 <div class="panel-heading" style="text-align:left"> <span class="glyphicon glyphicon-share" aria-hidden="true"></span>&nbsp;&nbsp; Export </div>

                  <div class="panel-body">                  
                                                        
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text=" To Exel" />
                                                        
                      <br />
                      <br />

                  </div>
                </div>

            </td >       
            <td style="width:800px">

               <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

                <Triggers>
                    <asp:AsyncPostBackTrigger controlid="sortby_points" eventname="CheckedChanged" />   
                    <asp:AsyncPostBackTrigger controlid="sortby_refno" eventname="CheckedChanged" />  
                    <asp:AsyncPostBackTrigger controlid="sortby_name" eventname="CheckedChanged" />
                    <asp:AsyncPostBackTrigger controlid="sortby_bookings" eventname="CheckedChanged" /> 
                   <asp:AsyncPostBackTrigger controlid="sortby_cancelled" eventname="CheckedChanged" />
                    <asp:AsyncPostBackTrigger controlid="sortby_absent" eventname="CheckedChanged" />  
                    <asp:AsyncPostBackTrigger controlid="sortby_bulk" eventname="CheckedChanged" /> 
                    <asp:AsyncPostBackTrigger controlid="toddler_chk" eventname="CheckedChanged"/>
                    <asp:AsyncPostBackTrigger controlid="elder_chk" eventname="CheckedChanged" />
                    <asp:AsyncPostBackTrigger controlid="Order" eventname="CheckedChanged" />
                  </Triggers>        
                   
                                    
                <ContentTemplate>

               <table style="width:auto">

                   <tr>
                       
                       <td style="padding:20px; vertical-align:top;">
                                <asp:GridView ID="statsgrid" runat="server" AutoGenerateColumns="False" Cssclass="mGrid">
                                    <AlternatingRowStyle BackColor="#F2E6F2" />
                                    <Columns>
                                        <asp:BoundField DataField="REF_NO" HeaderText="&nbsp; No &nbsp;" />
                                        <asp:BoundField DataField="NAME_TAG" HeaderText="Kid &nbsp;" />
                                        <asp:BoundField DataField="POINTS" HeaderText="Points &nbsp;" />
                                        <asp:BoundField DataField="BOOKINGS" HeaderText="&nbsp; Bookings &nbsp;" />
                                        <asp:BoundField DataField="FIXED_BOOKINGS" HeaderText="Fixed &nbsp;" />
                                        <asp:BoundField DataField="PRESENT" HeaderText="Present&nbsp;" />
                                        <asp:BoundField DataField="ABSENT" HeaderText="Absent &nbsp;" />
                                        <asp:BoundField DataField="CANCELLED" HeaderText="&nbsp; Cancelled &nbsp;" />
                                        <asp:BoundField DataField="CANCELLED_THISWEEK" HeaderText="This week cancels&nbsp;" />
                                        <asp:BoundField DataField="BULK_CANCELLATIONS" HeaderText="Bulk cancels &nbsp;" />
                                    </Columns>
                                    <HeaderStyle BackColor="#990099" Font-Size="Small" ForeColor="White" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView>                                              
                         </td>              
                  

                   </tr>

              </table>
            
              </ContentTemplate>
            </asp:UpdatePanel>
  
                           
            </td>
            
            
        </tr>   

        </table>

&nbsp;


</asp:Content>

