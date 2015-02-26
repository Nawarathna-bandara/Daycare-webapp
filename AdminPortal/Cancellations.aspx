<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Cancellations.aspx.cs" Inherits="AdminPortal_DeleteSummery" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="mainContent" Runat="Server">

    <div class ="topContentFrame" >
          
        <div class="btn-group" role="group" aria-label="...">
     <asp:LinkButton ID="LinkButton1" class= "btn btn-default" onclick="Attendance_Click" runat="server"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>&nbsp;Attendance</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton2" class= "btn btn-default" onclick="Stats_Click" runat="server"><span class="glyphicon glyphicon-stats" aria-hidden="true"></span>&nbsp;Stats</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton3" class= "btn btn-default" onclick="Contacts_Click" runat="server"><span class="glyphicon glyphicon-earphone" aria-hidden="true"></span>&nbsp;Contacts</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton4" class= "btn btn-primary" onclick="Cancellations_Click" runat="server"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Cancellations</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton5" class= "btn btn-default" onclick="Kidsinfo_Click" runat="server"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;Kids info</asp:LinkButton>                                                                                      
     <asp:LinkButton ID="LinkButton6" class= "btn btn-default" onclick="Holidays_Click" runat="server"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>&nbsp;Holidays</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton7" class= "btn btn-default" onclick="Renewsub_Click" runat="server"><span class="glyphicon glyphicon-repeat" aria-hidden="true"></span>&nbsp;Subscription</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton8" class= "btn btn-default" onclick="Sysparam_Click" runat="server"><span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>&nbsp;Parameters</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton9" class= "btn btn-default" onclick="Addkids_Click" runat="server"><span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span>&nbsp;Add Kids</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton10" class= "btn btn-default" onclick="Admins_Click" runat="server"><span class="glyphicon glyphicon-wrench" aria-hidden="true"></span>&nbsp;App</asp:LinkButton>                                             

    </div>

        <table style="width:100%">
            <tr>

               <td style="padding:20px; vertical-align:top;" rowspan="2">


                <div class="panel panel-default">

                 <div class="panel-heading" style="text-align:left"> <span class="glyphicon glyphicon-share" aria-hidden="true"></span>&nbsp;&nbsp; Export  </div>

                  <div class="panel-body">                  
                                                        
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Exel" />
                                                        
                  </div>
                </div>


            </td >

                <br/><br/>

                <td style="padding:20px;">  
                <div class="panel panel-primary">
                 <div class="panel-heading"> Select date range </div>
                 <div class="panel-body">

                    
            <div style="padding-bottom:25px;padding-left:10px;font-size:0.9em" >

                <div style="float:left">  From : </div>
                <div style="padding-left:10px;float:left"> 
                    <asp:TextBox ID="txtFromDate" class="startDate" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="fromDateExtender" runat="server" 
                        TargetControlID="txtFromDate" 
                        CssClass="AtdCalendar"
                        Format="MMMM d, yyyy" />
                </div>
                                   

                <div style="float:left;">
                    <div style="float:left;padding-left:30px;">  To : </div>
                    <div style="padding-left:10px;padding-right:15px;float:left"> 
                         <asp:TextBox ID="txtToDate" class="startDate" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="toDateExtender" runat="server" 
                                TargetControlID="txtToDate" 
                                CssClass="AtdCalendar"
                                Format="MMMM d, yyyy" />
                    </div>
                </div>

                <div style="float:left;">
                    <div style="padding-left:10px;float:left;"><asp:Button ID="btRefresh" runat="server" Text="Refresh" OnClick="btRefresh_Click"/></div>

                    <asp:Label ID="lbInfo" runat="server" ForeColor="Green" Font-Size="0.8em" Visible="False" ></asp:Label>
                    <asp:Label ID="lbMsg" runat="server" ForeColor="Red" Visible="False" Font-Size="0.8em"></asp:Label>                                                                      
                </div>
            </div> 
                   </div></div>          
    
                </td>
            </tr>
            <tr>
                
                <td style="padding:20px;">


                    
     <div class="gdView">
                                                
						<asp:GridView ID="grdViewKidsDeletion" runat="server" AutoGenerateColumns="false"
                                        AllowPaging="true" 
                                        PageSize="100" AllowSorting="false" 
                    
                                        CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" 
                                        AlternatingRowStyle-CssClass="alt" 
                                        emptydatatext="No deletion data available for above period" 
>
                                        <columns>
                                            
                                              <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" >
                                                    <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                               </asp:TemplateField> 

                                            <asp:BoundField DataField="name_tag"  HeaderText=" Name"  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px"  />   
                                            <asp:BoundField DataField="date_booked" HeaderText=" Date Booked"  DataFormatString="{0:ddd, MMMM d, yyyy}"    ItemStyle-HorizontalAlign="Right" ItemStyle-Width="140px"  />   
                                            <asp:BoundField DataField="session_booked"  HeaderText=" Session"   ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px"  />   
                                            <asp:BoundField DataField="category"  HeaderText=" Kid Group"   ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"  />   
                                            <asp:BoundField DataField="deleted_username" HeaderText=" Deleted By"    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"  />   
                                            <asp:BoundField DataField="deleted_timestamp"  HeaderText=" Deleted Date"    ItemStyle-HorizontalAlign="Right" ItemStyle-Width="140px"  />
                                                                                                                                                                                                                                                                                                                                                            
                                        </columns>
                                                                
                                        <pagersettings
                                            mode="NumericFirstLast"
                                            FirstPageText="First"
                                            LastPageText="Last"
                                            nextpagetext="Next" 
                                            previouspagetext="Back" 
                                            position="Bottom"
                                            pagebuttoncount="8" />
                                                                  
                                            <pagerstyle
                                            backcolor="steelblue" 
                                            forecolor="beige"
                                            horizontalalign="Center" />
                                                                  
                                            <HeaderStyle BackColor="#eeeeee" />

                            </asp:GridView>   												  												  
                    </div>





                </td>

            </tr>
        </table>


   </div>
</asp:Content>

