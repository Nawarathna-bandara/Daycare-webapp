<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="MyReport.aspx.cs" Inherits="Bookings" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
   
    </asp:Content>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent"> 





    <%-- ============================================== massegaes =========================================================== --%>
       <asp:UpdatePanel runat="server" id="UpdatePanel5" updatemode="Conditional">
                    <Triggers>
                         <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
                         
                         <%--<asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />--%>
                    </Triggers>
                    <ContentTemplate>
                        <div style="text-align:center"> 
                            <%--<asp:Label ID="lbMsg" runat="server" ForeColor="Red" Visible="False" Font-Size="1em" Text="&lt;br /&gt;&lt;br /&gt;Your account has not been synchronized with IFS Juniors Forecast system properly &lt;br /&gt;&lt;br /&gt; Please contact IFS Juniors Commitee."></asp:Label>--%>                                           
                            <%--<asp:Label ID="lbMsgDb" runat="server" ForeColor="Red" Visible="false" Font-Size="1em" Text="<br /><br /> Sorry, IFS Juniors Forecast System was unable to connect to the database.<br />This may be caused by the server being busy.<br /><br />Please try again later."></asp:Label>--%>                                           
                            
                            <div runat="server" id="msg_no_connection" class="alert alert-danger" role="alert"><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span> Sorry, IFS Juniors Forecast System was unable to connect to the database. This may be caused by the server being busy. Please try again later.</div>
                            <div runat="server" id="msg_no_account" class="alert alert-danger" role="alert"> <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span> Your account has not been synchronized with IFS Juniors Forecast system properly. Please contact IFS Juniors Commitee. </div>
                            <div runat="server" id="msg_not_parent" class="alert alert-danger" role="alert"><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span> You are not a registered parent/guardian of the IFS Juniors. Please contact IFS Juniors Committee to proceed with the registration."</div>
                            <div runat="server" id="msg_error" class="alert alert-danger" role="alert"><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>  Error occured. Please try again later !</div>

                        </div>  
                    </ContentTemplate>
                    </asp:UpdatePanel>   
    
    
    
    
     
  <%-- ============================================== tabs =========================================================== --%>

     <ul class="nav nav-tabs">
        <li role="presentation" ><a href="ManageAttendance.aspx">Manage Bookings</a></li>
        <li role="presentation"><a href="MyOverview.aspx">My Overview</a></li>
        <li role="presentation"class="active" ><a href="MyReport.aspx">My Report</a></li>
     </ul>

<%-- ================================================================================================================================ --%>


    <br/>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" updatemode="Conditional">
        <Triggers>
                <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />           
        </Triggers>
    </asp:UpdatePanel>

        <div class="panel panel-default">
          <!-- Default panel contents -->
          <div class="panel-heading"> Overview for&nbsp; 
              <asp:DropDownList ID="drpKidName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpKidName_SelectedIndexChanged" Width="130px">
                  <asp:ListItem Text="SELECT NAME" Value="0"></asp:ListItem>
              </asp:DropDownList>
              <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </div>
          <div class="panel-body" style="background-color:#f6f5f5">
     

            <asp:UpdatePanel runat="server" id="UpdatePanel2" updatemode="Conditional">
                    
                <Triggers>
                            <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
                </Triggers>


                <ContentTemplate>

                    <asp:GridView ID="GRID" runat="server" AutoGenerateColumns="False" Cssclass="mGrid" >
                   
                    <AlternatingRowStyle BackColor="#ECD7EC" />
                    <Columns>
                        <asp:BoundField DataField="DATE_BOOKED" HeaderText="&nbsp;Date &nbsp;&nbsp;" />
                        <asp:BoundField DataField="SESSION_BOOKED" HeaderText="&nbsp;Session &nbsp;&nbsp;" />
                        <asp:BoundField DataField="ACTIVITY_USERNAME" HeaderText="&nbsp;Activity User&nbsp;&nbsp;" />
                        <asp:BoundField DataField="BOOKING_STATE" HeaderText="&nbsp;Booking State&nbsp;&nbsp;" />
                        <asp:BoundField DataField="ATTENDANCE" HeaderText="&nbsp;Attendance&nbsp;&nbsp;" />

                        <asp:TemplateField HeaderText="&nbsp;Booking State&nbsp;&nbsp;">
                            <ItemTemplate>
                                <asp:Label  runat="server"  Text='<%# Eval("BOOKING_STATE").ToString().Equals ("0") ? "<span style=\"color:black;\">Pending<span>" : Eval("BOOKING_STATE").ToString().Equals ("4") ? "<span style=\"color: gray;\">Cancelled<span>" : Eval("BOOKING_STATE").ToString().Equals ("5") ? "Disqualified" : "<span style=\"color: Green;\">Fixed<span>" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="&nbsp;Attendance&nbsp;&nbsp;">
                            <ItemTemplate>
                                <asp:Label  runat="server"  Text='<%# Eval("ATTENDANCE").ToString().Equals("0") ? "<span style=\"color: red;\">Absent<span>" : "<span style=\"color: Green;\">Present<span>" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                        <HeaderStyle BackColor="#873E8D" ForeColor="White" BorderColor="#CCCCCC" Font-Bold="True" Font-Size="Small" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView> 
                        
                    </ContentTemplate> 

                </asp:UpdatePanel> 

        </div>

        </div>



<%--    <div class="auto-style2" style="border:solid 1px #808080">
        
        Kid Name :
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        Points :<asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
        <br />
        Bookings :<asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
        <br />
        Absent Count :<asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
        <br />
        Cancellations This Week :<asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
        <br />
        Bulk Cancelling Patterns :<asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
        <br />

       </div>--%>
      </ContentTemplate>
    </asp:UpdatePanel>
   
    <%--</div>--%>


    
    

</asp:Content>
