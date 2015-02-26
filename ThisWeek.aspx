<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="ThisWeek.aspx.cs" Inherits="Bookings" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
   
   
</asp:Content>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent"> 

     <asp:UpdatePanel runat="server" id="UpdatePanel5" updatemode="Conditional">
                    <Triggers>
                         <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
                         <asp:AsyncPostBackTrigger controlid="drpGuardian" eventname="SelectedIndexChanged" />
                         <asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <div style="text-align:center">

                            <div runat="server" id="msg_no_connection" class="alert alert-danger" role="alert"><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span> Sorry, IFS Juniors Forecast System was unable to connect to the database. This may be caused by the server being busy. Please try again later.</div>
                            <div runat="server" id="msg_no_account" class="alert alert-danger" role="alert"> <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span> Your account has not been synchronized with IFS Juniors Forecast system properly. Please contact IFS Juniors Commitee. </div>
                            <div runat="server" id="msg_not_parent" class="alert alert-danger" role="alert"><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span> You are not a registered parent/guardian of the IFS Juniors. Please contact IFS Juniors Committee to proceed with the registration."</div>
                            <div runat="server" id="msg_error" class="alert alert-danger" role="alert"><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>  Error occured. Please try again later !</div>
                            


                        </div>  
                    </ContentTemplate>
                    </asp:UpdatePanel>   
    
     <table style="background:#eeecec; border:1px #828282 solid;" width="100%" > 
          
                <tr>
                <td> </td>
                <td >                                    
                    <br/> 
                                 
                    <div style="padding-bottom:25px;padding-left:10px">
                        <div style="float:left;">
                            <div style="float:left;padding-left:30px;">  <span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp; Sign in as </div>
                            <div style="padding-left:10px;padding-right:5px;float:left"> 
                                <asp:DropDownList ID="drpGuardian" runat="server" CssClass="introText" Width="130px" OnSelectedIndexChanged="drpGuardian_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="SELECT NAME" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <asp:UpdatePanel runat="server" id="UpdatePanelKidName" updatemode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger controlid="drpGuardian" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />
                            </Triggers>
                            <ContentTemplate>
                            
                            <div style="padding-left:10px;float:right"> 
                                <asp:DropDownList ID="drpKidName" runat="server" Width="130px" OnSelectedIndexChanged="drpKidName_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="SELECT NAME" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div style="float:right"> Kid Name </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                        </div> 
                    <br/>
                    </td>
                <td> &nbsp;&nbsp; &nbsp;</td>
                </tr> 

        </table>

    <br/>

     <ul class="nav nav-tabs">
        <li role="presentation" style="background-color:#f0dff8"><a href="Default.aspx"><asp:Literal runat="server" id="Nextweektab" EnableViewState="false" /></a></li>
        <li role="presentation" class="active"><a href="ThisWeek.aspx"><asp:Literal runat="server" id="Thisweektab" EnableViewState="false" /></a></li>
     </ul>


    <div class="panel panel-primary" aria-disabled="true">
    <div class="panel-heading"> Select Your Bookings </div>
    <div class="panel-body_main">   
      
    <asp:UpdatePanel runat="server" id="UpdatePanelbookings" updatemode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger controlid="drpGuardian" eventname="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />
        </Triggers>

        <ContentTemplate>  

            <table id="booking_table" class="table" runat="server" border="0">

            <tr>
                <td></td>
                <td colspan="9">

                    <div class="alert alert-info" role="alert"> &nbsp;&nbsp;<span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>&nbsp;&nbsp;<asp:Literal runat="server" id="msg_home" EnableViewState="false" /></div>

                </td>
                <td></td>
            </tr>
            <tr>

            <td></td>
            <td>

                <div class="panel panel-primary" id ="mon_panel" runat="server">
                  <div class="panel-heading"><asp:Label ID="lbMon" runat="server" ></asp:Label></div>
                  <div class="panel-body"> 
                      
                      <div style="float:left">
                      <asp:UpdatePanel runat="server" id="UpdatePanel6" updatemode="Conditional">
                        <Triggers>
                                <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="drpGuardian" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:CheckBox ID="chkMon" runat="server" AutoPostBack="true" OnCheckedChanged="chkMon_CheckedChanged" Enabled="false" />
                        </ContentTemplate>
                      </asp:UpdatePanel>
                      </div>

                      <div>
                            <asp:UpdatePanel runat="server" id="UpdatePanel4" updatemode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger controlid="chkMon" eventname="CheckedChanged" />                             
                                <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="drpGuardian" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />
                            </Triggers>
                                <ContentTemplate>
                                    <div style="float:left; padding:10px; width:140px" >
                                        <asp:DropDownList ID="drpMon" runat="server" CssClass="introText" Enabled="False">
                                        <asp:ListItem>SELECT</asp:ListItem>
                                        <asp:ListItem>AM</asp:ListItem>
                                        <asp:ListItem>PM</asp:ListItem>
                                        <asp:ListItem>Full Day</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                       </div>

                  </div>

                </div>


            </td>
            <td></td>
            <td>

                 <div class="panel panel-primary" id ="tue_panel" runat="server">
                  <div class="panel-heading"><asp:Label ID="lbTue" runat="server" ></asp:Label></div>
                  <div class="panel-body"> 
                      

                      <asp:UpdatePanel runat="server" id="UpdatePanel7" updatemode="Conditional">
                        <Triggers>
                                <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="drpGuardian" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:CheckBox ID="chkTue" runat="server" AutoPostBack="true" OnCheckedChanged="chkTue_CheckedChanged" Enabled="false" />
                        </ContentTemplate>
                      </asp:UpdatePanel>

                      <div>
                            <asp:UpdatePanel runat="server" id="UpdatePanel3" updatemode="Conditional">
                            <Triggers>
     
                                <asp:AsyncPostBackTrigger controlid="chkTue" eventname="CheckedChanged" />                                
                                <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="drpGuardian" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />
                            </Triggers>
                                <ContentTemplate>
                                    <div style="float:left; padding:10px; width:140px" >
                                        <asp:DropDownList ID="drpTue" runat="server" CssClass="introText" Enabled="False">
                                        <asp:ListItem>SELECT</asp:ListItem>
                                        <asp:ListItem>AM</asp:ListItem>
                                        <asp:ListItem>PM</asp:ListItem>
                                        <asp:ListItem>Full Day</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                       </div>

                  </div>

                </div>

            </td>
            <td></td>
            <td>

                 <div class="panel panel-primary" id ="wed_panel" runat="server">
                  <div class="panel-heading"><asp:Label ID="lbWed" runat="server" ></asp:Label></div>
                  <div class="panel-body"> 
                      
                      <asp:UpdatePanel runat="server" id="UpdatePanel8" updatemode="Conditional">
                        <Triggers>
                                <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="drpGuardian" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />
                        </Triggers>
                        <ContentTemplate>
                             <asp:CheckBox ID="chkWed" runat="server" AutoPostBack="true" OnCheckedChanged="chkWed_CheckedChanged" Enabled="false" />
                        </ContentTemplate>
                      </asp:UpdatePanel>

                      <div>
                            <asp:UpdatePanel runat="server" id="UpdatePanel2" updatemode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger controlid="chkWed" eventname="CheckedChanged" />
                                <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="drpGuardian" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />
                            </Triggers>
                                <ContentTemplate>
                                    <div style="float:left; padding:10px; width:140px" >
                                        <asp:DropDownList ID="drpWed" runat="server" CssClass="introText" Enabled="False">
                                        <asp:ListItem>SELECT</asp:ListItem>
                                        <asp:ListItem>AM</asp:ListItem>
                                        <asp:ListItem>PM</asp:ListItem>
                                        <asp:ListItem>Full Day</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                       </div>

                  </div>

                </div>

            </td>
            <td></td>
            <td>

                 <div class="panel panel-primary" runat="server" id="thu_panel">
                  <div class="panel-heading" ><asp:Label ID="lbThu" runat="server" ></asp:Label></div>
                  <div class="panel-body"> 
                      
                    <asp:UpdatePanel runat="server" id="UpdatePanel9" updatemode="Conditional">
                        <Triggers>
                                <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="drpGuardian" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />
                        </Triggers>
                        <ContentTemplate>
                             <asp:CheckBox ID="chkThu" runat="server" AutoPostBack="true" OnCheckedChanged="chkThu_CheckedChanged" Enabled="false" />
                        </ContentTemplate>
                      </asp:UpdatePanel>

                      <div>
                            <asp:UpdatePanel runat="server" id="UpdatePanel1" updatemode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger controlid="chkThu" eventname="CheckedChanged" />
                                <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="drpGuardian" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />
                            </Triggers>
                                <ContentTemplate>
                                    <div style="float:left; padding:10px; width:140px" >
                                        <asp:DropDownList ID="drpThu" runat="server" CssClass="introText" Enabled="False">
                                        <asp:ListItem>SELECT</asp:ListItem>
                                        <asp:ListItem>AM</asp:ListItem>
                                        <asp:ListItem>PM</asp:ListItem>
                                        <asp:ListItem>Full Day</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                       </div>

                  </div>

                </div>

            </td>
            <td></td>
            <td>

                 <div class="panel panel-primary" id ="fri_panel" runat="server">
                  <div class="panel-heading"><asp:Label ID="lbFri" runat="server" ></asp:Label></div>
                  <div class="panel-body"> 
                      
                      <asp:UpdatePanel runat="server" id="UpdatePanel10" updatemode="Conditional">
                        <Triggers>
                                <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="drpGuardian" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />
                        </Triggers>
                        <ContentTemplate>
                             <asp:CheckBox ID="chkFri" runat="server" AutoPostBack="true" OnCheckedChanged="chkFri_CheckedChanged" Enabled="false" />
                        </ContentTemplate>
                      </asp:UpdatePanel>

                      <div>
                            <asp:UpdatePanel runat="server" id="UpdatePanelChkBox" updatemode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger controlid="chkFri" eventname="CheckedChanged" />
                                <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="drpGuardian" eventname="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />
                            </Triggers>
                                <ContentTemplate>
                                    <div style="float:left; padding:10px; width:140px" >
                                        <asp:DropDownList ID="drpFri" runat="server" CssClass="introText" Enabled="False">
                                        <asp:ListItem>SELECT</asp:ListItem>
                                        <asp:ListItem>AM</asp:ListItem>
                                        <asp:ListItem>PM</asp:ListItem>
                                        <asp:ListItem>Full Day</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                       </div>

                  </div>

                </div>

            </td>
            <td></td>
               
        </tr>
        <tr>
            <td></td>
            <td>
                     <asp:UpdatePanel runat="server" id="UpdatePanel11" updatemode="Conditional">
                                <Triggers>
                                        <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger controlid="drpGuardian" eventname="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />
                                </Triggers>
                                 <ContentTemplate>                            
                                    
                                    <asp:Button ID="btnAdd" runat="server" Text="Add My Bookings" OnClick="btnAdd_Click" OnClientClick="return IsChecked()" />&nbsp;                                   
                                 </ContentTemplate>
                                </asp:UpdatePanel>

            </td>
            <td colspan="9">

                <asp:UpdatePanel runat="server" id="addbuttonupdate" updatemode="Conditional">
                                <Triggers>
                                        <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger controlid="drpGuardian" eventname="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />
                                </Triggers>
                                 <ContentTemplate>                            
                                    <div id="msg_selectnames" runat="server" class="alert alert-warning" role="alert"> <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span> &nbsp;Select Your name and Kid name to proceed with boookings </div>
                                    
                                    <div id="msg_booking_success" runat="server" class="alert alert-success" role="alert"> <asp:Literal runat="server" id="success_msg" EnableViewState="false" /> </div>
                                     
                                    <div id="msg_booking_exists" runat="server" class="alert alert-danger" role="alert"> <asp:Literal runat="server" id="already_exist_msg" EnableViewState="false" /> </div>
                                    
                                 </ContentTemplate>
                                </asp:UpdatePanel>
                 </td>
        </tr>
        </table>

        </ContentTemplate>
        </asp:UpdatePanel>

        </div></div>


    <asp:UpdatePanel runat="server" id="tableupdate" updatemode="Conditional">
        <Triggers>           
            <asp:AsyncPostBackTrigger controlid="btnAdd" eventname="Click" />
        </Triggers>

            

        <ContentTemplate> 
                      
        <div class="panel panel-primary">
            <div class="panel-heading"> Morning session </div>
            <div class="panel-body_main">

        <table class="table" border="0" id="morning_table">
        <tr>
            <td>

            </td>
            <td>

                <div class="panel panel-primary">
                  <div class="panel-heading"> <asp:Label ID="Label1m" runat="server" ></asp:Label> <br/><span class="badge"><asp:Literal runat="server" id="aa_m" EnableViewState="false" /></span> </div>
                  <div class="panel-body" style="text-align:left"> 
                      
                      <span class="label label-primary">Toddlers</span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="aat_m" EnableViewState="false" /> </span>
                      <p>       
                      <asp:GridView ID="Mondaytgrid_m" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p><hr>
                      <span class="label label-primary">Elders</span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="aae_m" EnableViewState="false" /> </span>
                      <p>
                      <asp:GridView ID="Mondayegrid_m" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p>
                  </div>
                </div>


            </td>
            <td>
            </td>

            <td>


                <div class="panel panel-primary">
                  <div class="panel-heading"><asp:Label ID="Label2m" runat="server" ></asp:Label> <br/><span class="badge"><asp:Literal runat="server" id="bb_m" EnableViewState="false" /></span></div>
                  <div class="panel-body" style="text-align:left"> 
                      
                      <span class="label label-primary">Toddlers</span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="bbt_m" EnableViewState="false" /> </span>
                      <p>       
                      <asp:GridView ID="Tuesdaytgrid_m" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p><hr>
                      <span class="label label-primary">Elders</span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="bbe_m" EnableViewState="false" /> </span>
                      <p>
                      <asp:GridView ID="Tuesdayegrid_m" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p>
                  </div>
                </div>

            </td>

            <td>

            </td>

            <td>

                <div class="panel panel-primary">
                  <div class="panel-heading"><asp:Label ID="Label3m" runat="server" ></asp:Label><br/> <span class="badge"><asp:Literal runat="server" id="cc_m" EnableViewState="false" /></span></div>
                  <div class="panel-body" style="text-align:left"> 
                      
                      <span class="label label-primary"> Toddlers </span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="cct_m" EnableViewState="false" /> </span>
                      <p>       
                      <asp:GridView ID="Wednesdaytgrid_m" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p><hr>
                      <span class="label label-primary">Elders </span> &nbsp;<span class="badge"><asp:Literal runat="server" id="cce_m" EnableViewState="false" /></span>
                      <p>
                      <asp:GridView ID="Wednesdayegrid_m" runat="server" ShowHeader="False" GridLines="None"  style="text-align:left">
                      </asp:GridView>
                      </p>
                  </div>
                </div>

            </td>

            <td>
       
                &nbsp;</td>

            <td>
                <div class="panel panel-primary">
                  <div class="panel-heading"><asp:Label ID="Label4m" runat="server" ></asp:Label><br/><span class="badge"><asp:Literal runat="server" id="dd_m" EnableViewState="false" /></span></div>
                  <div class="panel-body" style="text-align:left"> 
                      
                      <span class="label label-primary">Toddlers</span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="ddt_m" EnableViewState="false" /> </span>
                      <p>       
                      <asp:GridView ID="Thursdaytgrid_m" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p><hr>
                      <span class="label label-primary">Elders</span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="dde_m" EnableViewState="false" /> </span>
                      <p>
                      <asp:GridView ID="Thursdayegrid_m" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p>
                  </div>
                </div>

            </td>

            <td>

            </td>

            <td>
               <div class="panel panel-primary">
                  <div class="panel-heading"><asp:Label ID="Label5m" runat="server" ></asp:Label><br/> <span class="badge"><asp:Literal runat="server" id="ee_m" EnableViewState="false" /></span></div>
                  <div class="panel-body" style="text-align:left"> 
                      
                      <span class="label label-primary">Toddlers</span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="eet_m" EnableViewState="false" /> </span>
                      <p>       
                      <asp:GridView ID="Fridaytgrid_m" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p>
                      <hr>
                      <span class="label label-primary">Elders</span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="eee_m" EnableViewState="false" /> </span>
                      <p>
                      <asp:GridView ID="Fridayegrid_m" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p>
                  </div>
                </div>

            </td>
            <td>

            </td>
        </tr>
        <tr>
            <td colspan="11" style="text-align:center">
               <span style="color:darkgreen"> Fixed </span>,  <span style="color:black"> Safe </span>, <span style="color:darkorange"> Tentative </span>, <span style="color:darkred"> Disqualified </span> 
            </td></tr>
        
    </table>

    </div>
    </div>
    
    <div class="panel panel-primary">
    <div class="panel-heading"> Evening session </div>
    <div class="panel-body_main">

    <table class="table" border="0">
        
        <tr>
            <td>

            </td>
            <td>

                <div class="panel panel-primary">
                  <div class="panel-heading"> <asp:Label ID="Label1e" runat="server" ></asp:Label><br/> <span class="badge"><asp:Literal runat="server" id="aa_e" EnableViewState="false" /></span> </div>
                  <div class="panel-body" style="text-align:left"> 
                      
                      <span class="label label-primary">Toddlers</span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="aat_e" EnableViewState="false" /> </span>
                      <p>       
                      <asp:GridView ID="mondaytgrid_e" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p><hr>
                      <span class="label label-primary">Elders</span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="aae_e" EnableViewState="false" /> </span>
                      <p>
                      <asp:GridView ID="mondayegrid_e" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p>
                  </div>
                </div>


            </td>
            <td>
            </td>

            <td>


                <div class="panel panel-primary">
                  <div class="panel-heading"><asp:Label ID="Label2e" runat="server" ></asp:Label> <br/><span class="badge"><asp:Literal runat="server" id="bb_e" EnableViewState="false" /></span></div>
                  <div class="panel-body" style="text-align:left"> 
                      
                      <span class="label label-primary">Toddlers</span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="bbt_e" EnableViewState="false" /> </span>
                      <p>       
                      <asp:GridView ID="tuesdaytgrid_e" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p><hr>
                      <span class="label label-primary">Elders</span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="bbe_e" EnableViewState="false" /> </span>
                      <p>
                      <asp:GridView ID="tuesdayegrid_e" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p>
                  </div>
                </div>

            </td>

            <td>

            </td>

            <td>

                <div class="panel panel-primary">
                  <div class="panel-heading"><asp:Label ID="Label3e" runat="server" ></asp:Label><br/> <span class="badge"><asp:Literal runat="server" id="cc_e" EnableViewState="false" /></span></div>
                  <div class="panel-body" style="text-align:left"> 
                      
                      <span class="label label-primary"> Toddlers </span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="cct_e" EnableViewState="false" /> </span>
                      <p>       
                      <asp:GridView ID="wednesdaytgrid_e" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p><hr>
                      <span class="label label-primary">Elders </span> &nbsp;<span class="badge"><asp:Literal runat="server" id="cce_e" EnableViewState="false" /></span>
                      <p>
                      <asp:GridView ID="wednesdayegrid_e" runat="server" ShowHeader="False" GridLines="None" style="text-align:left" >
                      </asp:GridView>
                      </p>
                  </div>
                </div>

            </td>

            <td>
       
                &nbsp;</td>

            <td>
                <div class="panel panel-primary">
                  <div class="panel-heading"><asp:Label ID="Label4e" runat="server" ></asp:Label><br/> <span class="badge"><asp:Literal runat="server" id="dd_e" EnableViewState="false" /></span></div>
                  <div class="panel-body" style="text-align:left"> 
                      
                      <span class="label label-primary">Toddlers</span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="ddt_e" EnableViewState="false" /> </span>
                      <p>       
                      <asp:GridView ID="thursdaytgrid_e" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p><hr>
                      <span class="label label-primary">Elders</span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="dde_e" EnableViewState="false" /> </span>
                      <p>
                      <asp:GridView ID="thursdayegrid_e" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p>
                  </div>
                </div>

            </td>

            <td>

            </td>

            <td>
               <div class="panel panel-primary">
                  <div class="panel-heading"><asp:Label ID="Label5e" runat="server" ></asp:Label><br/><span class="badge"><asp:Literal runat="server" id="ee_e" EnableViewState="false" /></span></div>
                  <div class="panel-body" style="text-align:left"> 
                      
                      <span class="label label-primary">Toddlers</span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="eet_e" EnableViewState="false" /> </span>
                      <p>       
                      <asp:GridView ID="fridaytgrid_e" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p>
                      <hr>
                      <span class="label label-primary">Elders</span> &nbsp;<span class="badge"> <asp:Literal runat="server" id="eee_e" EnableViewState="false" /> </span>
                      <p>
                      <asp:GridView ID="fridayegrid_e" runat="server" ShowHeader="False" GridLines="None" style="text-align:left">
                      </asp:GridView>
                      </p>
                  </div>
                </div>

            </td>
            <td>

            </td>
        </tr>
        <tr>
            <td colspan="11" style="text-align:center">
               <span style="color:darkgreen"> Fixed </span>,  <span style="color:black"> Safe </span>, <span style="color:darkorange"> Tentative </span>, <span style="color:darkred"> Disqualified </span> 
            </td></tr>
        
    </table>

    </div>
    </div>

        </ContentTemplate>
    </asp:UpdatePanel>



    

</asp:Content>
