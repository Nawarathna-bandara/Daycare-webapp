<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Holidays.aspx.cs" Inherits="AdminPortal_Holidays" %>
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
     <asp:LinkButton ID="LinkButton6" class= "btn btn-primary" onclick="Holidays_Click" runat="server"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>&nbsp;Holidays</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton7" class= "btn btn-default" onclick="Renewsub_Click" runat="server"><span class="glyphicon glyphicon-repeat" aria-hidden="true"></span>&nbsp;Subscription</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton8" class= "btn btn-default" onclick="Sysparam_Click" runat="server"><span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>&nbsp;Parameters</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton9" class= "btn btn-default" onclick="Addkids_Click" runat="server"><span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span>&nbsp;Add Kids</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton10" class= "btn btn-default" onclick="Admins_Click" runat="server"><span class="glyphicon glyphicon-wrench" aria-hidden="true"></span>&nbsp;App</asp:LinkButton>                                             
    </div>

                <br/><br/>

                 <div class="panel panel-primary">
                 <div class="panel-heading"> Add Holiday </div>
                 <div class="panel-body">

                <div style="float:left">
                <div style="float:left">  Date : </div>
                <div style="padding-left:10px;float:left"> 
                    <asp:TextBox ID="txtDate" class="endDate" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="dateExtender" runat="server" 
                                    TargetControlID="txtDate" 
                                    CssClass="AtdCalendar"
                                    Format="MMMM d, yyyy"  />    
                </div>
                                   

                <div style="float:left;">
                    <div style="float:left;padding-left:30px;">  Description : </div>
                    <div style="padding-left:10px;padding-right:15px;float:left"> 
                         <asp:TextBox ID="txtDescription" runat="server" Width="450px" TextMode="MultiLine"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorDescription" runat="server" 
					                ControlToValidate="txtDescription" Display="None" ErrorMessage="Required Field"></asp:RequiredFieldValidator>--%>
				               <%-- <ajaxToolkit:ValidatorCalloutExtender
					                ID="ValidatorCalloutExtenderValidateDescription" runat="server" TargetControlID="RequiredFieldValidatorDescription" >
				                </ajaxToolkit:ValidatorCalloutExtender>--%>
                    </div>
                </div>

                <div style="padding-left:10px;float:left;"><asp:Button ID="btAdd" runat="server" Text="Add Holiday" OnClick="btAdd_Click" /></div>

                </div>

                     <asp:UpdatePanel runat="server" id="UpdatePanel1" updatemode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger controlid="btAdd" eventname="Click" />
                           <asp:AsyncPostBackTrigger controlid="DropDownYear" eventname="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                          <div style="margin:auto; text-align:center">                
                            <asp:Label ID="lbInfo" runat="server" ForeColor="Green" Font-Size="1.2em" Visible="False" ></asp:Label>
                            <asp:Label ID="lbMsg" runat="server" ForeColor="Red" Visible="False" Font-Size="1.2em"></asp:Label>                                                                      
                         </div>
 
                            </ContentTemplate>
                    </asp:UpdatePanel>


                     </div></div>

                <div class="panel panel-default">
                 <div class="panel-heading"> View Holidays for  

                     <asp:DropDownList ID="DropDownYear" runat="server" Width="120px" AutoPostBack="true" OnSelectedIndexChanged="DropDownYear_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select Year</asp:ListItem>
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem>2008</asp:ListItem>
                                        <asp:ListItem>2009</asp:ListItem>
                                        <asp:ListItem>2010</asp:ListItem>
                                        <asp:ListItem>2011</asp:ListItem>
                                        <asp:ListItem>2012</asp:ListItem>
                                        <asp:ListItem>2013</asp:ListItem>
                                        <asp:ListItem>2014</asp:ListItem>
                                        <asp:ListItem>2015</asp:ListItem>
                                        <asp:ListItem>2016</asp:ListItem>
                                        <asp:ListItem>2017</asp:ListItem>
                                        <asp:ListItem>2018</asp:ListItem>
                                        <asp:ListItem>2019</asp:ListItem>
                                        <asp:ListItem>2020</asp:ListItem>
                                    </asp:DropDownList> 

                 </div>
                 <div class="panel-body" style="margin-top:10px;">
        
    <asp:UpdatePanel runat="server" id="UpdatePanel" updatemode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger controlid="btAdd" eventname="Click" />
       <asp:AsyncPostBackTrigger controlid="DropDownYear" eventname="SelectedIndexChanged" />
    </Triggers>
    <ContentTemplate>

     <div class="gdView">
                                                
						<asp:GridView ID="grdViewHolidays" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" 
                    PageSize="100" AllowSorting="false"        
                    CssClass="mGrid"
                    PagerStyle-CssClass="pgr" 
                    AlternatingRowStyle-CssClass="alt"
                    emptydatatext="No data available for above period" OnRowDeleting="grdViewHolidays_RowDeleting" >
                    <columns>
                                            
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" >
                                <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                        <asp:BoundField DataField="Date_Stamp" HeaderText=" Date" SortExpression="Date_Stamp" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:MMMM d, yyyy}" ItemStyle-Width="150px"  />
                        <asp:BoundField DataField="Day_of_Week" HeaderText=" Day" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120px" ItemStyle-CssClass="itemDay" />  
                        <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-HorizontalAlign="Left" />                                                               
                        <asp:BoundField DataField="Updated_By" HeaderText=" Updated By" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />                                                                                                                                                                                             
                                                                    
                        <asp:TemplateField ItemStyle-Width="10px">
                            <ItemTemplate>   
                                <asp:ImageButton ID="btDelete" name="DeleteButton" commandName="Delete" runat="server" ImageUrl="~/Images/cross.png" ToolTip="Delete" CausesValidation="false"/>   
                                <ajaxToolkit:ConfirmButtonExtender TargetControlId="btDelete" ConfirmText="Delete this entry?" runat="server" />
                            </ItemTemplate>   
                        </asp:TemplateField>                                                                                                                                                                                                                                                                                                                  
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
                     </div></div>
</asp:Content>

