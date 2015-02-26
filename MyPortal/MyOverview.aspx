<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="MyOverview.aspx.cs" Inherits="Bookings" %>

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
        <li role="presentation"class="active" ><a href="MyOverview.aspx">My Overview</a></li>
        <li role="presentation"><a href="MyReport.aspx">My Report</a></li>
     </ul>

<%-- ================================================================================================================================ --%>


    <br/>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" updatemode="Conditional">
    <Triggers>
            <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />           
    </Triggers>
    <ContentTemplate>

        <div class="panel panel-default">
          <!-- Default panel contents -->
          <div class="panel-heading"> Overview for&nbsp; 
              <asp:DropDownList ID="drpKidName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpKidName_SelectedIndexChanged" Width="130px">
                  <asp:ListItem Text="SELECT NAME" Value="0"></asp:ListItem>
              </asp:DropDownList>
            </div>
          <div class="panel-body" style="background-color:#f6f5f5">
     
          <!-- List group -->
          <ul class="list-group" style="padding-left:100px;padding-right:100px">

            <li class="list-group-item">
                <span class="badge" style="font-size:25px">
                    <asp:Literal runat="server" id="points" EnableViewState="false" />
                </span> <h4>Points</h4>
            </li>

            <li class="list-group-item">
                <span class="badge" style="text-align:center;font-size:20px">
                    <asp:Literal runat="server" id="bookings1" EnableViewState="false" />

                </span> Bookings 
            </li>

            <li class="list-group-item">
                <span class="badge" style="text-align:center;font-size:20px">
                    <asp:Literal runat="server" id="fixed1" EnableViewState="false" />
                </span> Fixed
            </li>

            <li class="list-group-item">
                <span class="badge" style="text-align:center;font-size:20px">
                    <asp:Literal runat="server" id="present" EnableViewState="false" />
                </span> Present
            </li>

             <li class="list-group-item">
                <span class="badge" style="text-align:center;font-size:20px">
                    <asp:Literal runat="server" id="absent" EnableViewState="false" />
                </span> Absent
            </li>

             <li class="list-group-item">
                <span class="badge" style="text-align:center;font-size:20px">
                    <asp:Literal runat="server" id="cancelled" EnableViewState="false" />
                </span> Cancelled
            </li>

            <li class="list-group-item">
                <span class="badge" style="text-align:center;font-size:20px">
                    <asp:Literal runat="server" id="bulkcancels" EnableViewState="false" />
                </span> Bulk Cancels
            </li>
            
        </ul>

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
