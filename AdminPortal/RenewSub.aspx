<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RenewSub.aspx.cs" Inherits="AdminPortal_RenewSub" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="mainContent" Runat="Server">
    <div class ="topContentFrame" >

        <div class="btn-group" role="group" aria-label="...">
     <asp:LinkButton ID="LinkButton1" class= "btn btn-default" onclick="Attendance_Click" runat="server"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>&nbsp;Attendance</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton2" class= "btn btn-default" onclick="Stats_Click" runat="server"><span class="glyphicon glyphicon-stats" aria-hidden="true"></span>&nbsp;Stats</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton3" class= "btn btn-default" onclick="Contacts_Click" runat="server"><span class="glyphicon glyphicon-earphone" aria-hidden="true"></span>&nbsp;Contacts</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton4" class= "btn btn-default" onclick="Cancellations_Click" runat="server"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Cancellations</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton5" class= "btn btn-default" onclick="Kidsinfo_Click" runat="server"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;Kids info</asp:LinkButton>                                                                                      
     <asp:LinkButton ID="LinkButton6" class= "btn btn-default" onclick="Holidays_Click" runat="server"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>&nbsp;Holidays</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton7" class= "btn btn-primary" onclick="Renewsub_Click" runat="server"><span class="glyphicon glyphicon-repeat" aria-hidden="true"></span>&nbsp;Subscription</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton8" class= "btn btn-default" onclick="Sysparam_Click" runat="server"><span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>&nbsp;Parameters</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton9" class= "btn btn-default" onclick="Addkids_Click" runat="server"><span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span>&nbsp;Add Kids</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton10" class= "btn btn-default" onclick="Admins_Click" runat="server"><span class="glyphicon glyphicon-wrench" aria-hidden="true"></span>&nbsp;App</asp:LinkButton>                                             
    </div>
          
        <br/><br/>
        <div class="panel panel-primary">
                 <div class="panel-heading"> Update Subscription </div>
                 <div class="panel-body">

            <div style="padding-bottom:10px;padding-left:10px;font-size:1em;"  >

                 <div>
                    <div style="float:left;">
                         <div style="float:left;padding-left:0px;">  Kid Name: </div>
                         <div style="padding-left:10px;padding-right:15px;float:left"> 
                             <asp:DropDownList ID="drpKidName" runat="server" CssClass="introText" Width="130px" AutoPostBack="true"  OnSelectedIndexChanged="drpKidNmae_SelectedIndexChanged">
                                        <asp:ListItem Text="- ALL KIDS -" Value="0"></asp:ListItem>
                             </asp:DropDownList>
                         </div>
                    </div>
                 </div>
                <div>
                    <div style="float:left;padding-left:30px;">
                        <div style="float:left;padding-left:0px;">  Subscription Year: </div>
                        <div style="padding-left:10px;padding-right:15px;float:left"> 
                             <asp:DropDownList ID="DropDownYear" runat="server" Width="120px" >
                                        <asp:ListItem Value="0">Select Year</asp:ListItem>
                                        <asp:ListItem>2013</asp:ListItem>
                                        <asp:ListItem>2014</asp:ListItem>
                                        <asp:ListItem>2015</asp:ListItem>
                                        <asp:ListItem>2016</asp:ListItem>
                                        <asp:ListItem>2017</asp:ListItem>
                                        <asp:ListItem>2018</asp:ListItem>
                                        <asp:ListItem>2019</asp:ListItem>
                                        <asp:ListItem>2020</asp:ListItem>
                                        <asp:ListItem>2022</asp:ListItem>
                                        <asp:ListItem>2023</asp:ListItem>
                                        <asp:ListItem>2024</asp:ListItem>
                                        <asp:ListItem>2025</asp:ListItem>
                             </asp:DropDownList> 
                        </div>
                    </div>
               </div>
               <div style="float:left">              
                    <div style="padding-right:10px;float:right;">
                        <asp:Button ID="btUpdate" runat="server" Text="Update Subscription" OnClick="btUpdate_Click"  />
                    </div>
                </div>
            </div> 
                             
    </div>
    </div></div>
    <asp:UpdatePanel runat="server" id="UpdatePanel" updatemode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger controlid="btUpdate" eventname="Click" />
       <asp:AsyncPostBackTrigger controlid="drpKidName" eventname="SelectedIndexChanged" />
    </Triggers>
    <ContentTemplate>
      <div style="margin:auto; padding:10px; text-align:center">                
        <asp:Label ID="lbInfo" runat="server" ForeColor="Green" Font-Size="0.8em" Visible="False" ></asp:Label>
        <asp:Label ID="lbMsg" runat="server" ForeColor="Red" Visible="False" Font-Size="0.8em"></asp:Label>                                                                      
     </div>
     <div class="gdView">
                                                
						<asp:GridView ID="grdViewSubscription" runat="server" AutoGenerateColumns="false"
                                        AllowPaging="true" 
                                        PageSize="150" AllowSorting="false" 
                    
                                        CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" 
                                        AlternatingRowStyle-CssClass="alt" 
                                        emptydatatext="No data available for above period"
>
                                        <columns>
                                            
                                              <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" >
                                                    <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                               </asp:TemplateField> 
                                            <asp:BoundField DataField="name_tag" HeaderText=" Kid Name" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="150px"  />
                                            <asp:BoundField DataField="age" HeaderText=" Age" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80px" />  
                                            <asp:BoundField DataField="sub_year" HeaderText="Subscription Year" ItemStyle-HorizontalAlign="Center"/>                                                               
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

