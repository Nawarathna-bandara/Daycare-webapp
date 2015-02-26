<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageAttendance.aspx.cs" Inherits="MyPortal_ManageAttendance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="http://ajax.aspnetcdn.com/ajax/jquery/jquery-1.9.0.min.js"></script>
    <script type="text/javascript">
            $(document).ready(function () {
                $(".gdView table tr").filter(function () {
                    return $('td', this).length && !$('table', this).length
                }).click(function () {
                    $(this).toggleClass('currRow');
                });
            });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

   <div>
       <ul class="nav nav-tabs">
        <li role="presentation" class="active"><a href="#">Manage Bookings</a></li>
        <li role="presentation" ><a href="MyOverview.aspx">My Overview</a></li>
       <li role="presentation"><a href="MyReport.aspx">My Report</a></li>
     </ul>

     <asp:UpdatePanel runat="server" id="UpdatePanelKidName" updatemode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger  controlid="drpKidName"  eventname="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>

            <div class ="topContentFrame" style="height:33px" >
 
                    <%--<div style="padding-bottom:15px;padding-left:10px;padding-top:10px; font-size:0.9em" >
                        <h2>Manage Attendance</h2>
                    </div>--%>
                   <%--<div style="width:50%;margin:auto; margin-top:-15px;height:30px">                                                        
									<asp:UpdateProgress ID="UpdateProgress1" runat="server" >
									<ProgressTemplate>
											<img src="/Images/ajax-loader.gif" />
									</ProgressTemplate>
									</asp:UpdateProgress>
					</div>--%>

                    <div class="introText" style="padding-left:10px;padding-top:10px;" >

                       <div id="divMyPortal">
                            <div style="float:left;">
                                <div style="float:left">  From : </div>
                                <div style="padding-left:10px;float:left"> 
                                    <asp:Label ID="lbFromDate" runat="server"></asp:Label>
                                </div>
                            </div>         

                            <div style="float:right;">

                                <div style="float:left;padding-left:30px;">  To : </div>
                                <div style="padding-left:10px;padding-right:15px;float:left"> 
                                      <asp:Label ID="lbTodate" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    
                        <div style="width:100%; height:35px"></div>                         
                    </div>
                             
             </div>
            <asp:Panel ID="adminPanel" runat ="server">
                <div id="div1" style="width:700px; float:left;padding-left:10px;padding-top:10px;padding-bottom:15px;" class="introText" >
                     <div style="float:left;">
                                        <asp:Label ID="lbFrom" runat="server" Text="From Date">From Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>
                                        <asp:TextBox ID="txtFromDate" class="startDate" runat="server" style="padding-left:30px;"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="fromDateExtender" runat="server" 
                                        TargetControlID="txtFromDate" 
                                        CssClass="AtdCalendar"
                                        Format="MMMM d, yyyy" />
                               

                    </div>
                   <%-- <div style="float:left; margin-left:20px;">
                                        To Date:
                                        <asp:TextBox ID="txtToDate" class="startDate" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="toDateExtender" runat="server" 
                                        TargetControlID="txtToDate" 
                                        CssClass="AtdCalendar"
                                        Format="MMMM d, yyyy" />
                               

                     </div>--%>

                    <div style="padding-left:20px;float:left;"><asp:Button ID="btExport" runat="server" Text="Export Attendance Report" OnClick="btExport_Click" /></div>
                </div>
                <div id="divAdmin" style="width:600px; float:left;padding-left:10px;padding-top:10px;" class="introText" >
			                    
                                <div style="width:100%;">
                                    <div style="float:left">  <asp:Label ID="lbKidName" runat="server"></asp:Label> </div>
                                    <div style="padding-left:20px;float:left;"> 
                                        <asp:DropDownList ID="drpKidName" runat="server" CssClass="introText" Width="130px" AutoPostBack="true" OnSelectedIndexChanged="drpKidName_SelectedIndexChanged">
                                                <asp:ListItem Text="- ALL KIDS -" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;<asp:Label ID="lbRefNo" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                </div>                     

							    <div style="width:100%;"></div> 

							    <div style="float:left; width:100%; text-align:left; padding-top:10px; padding-bottom:10px;">
								     <asp:Label ID="lbParent1" runat="server"  Text=""></asp:Label>
							    </div>
							    <div style="text-align:left; padding-bottom:20px; float:left;"> 
								     <asp:Label ID="lbParent2" runat="server" Text=""></asp:Label>
							    </div>
						
                  </div> 


                <div id="user1" style="width:600px; float:left;padding-left:10px;padding-top:10px;" class="introText" >
			                          		
                  </div>

                  
             </asp:Panel>
             <div style="text-align:center; margin:20px;"> 
                    <asp:Label ID="lbInfo" runat="server" ForeColor="Green" Font-Size="0.8em" Visible="False" ></asp:Label>
             </div>
             <div class="gdView">                                                    
						    <asp:GridView ID="grdViewKidsAtd" runat="server" AutoGenerateColumns="false"
                                            AllowPaging="true" 
                                            PageSize="100" AllowSorting="false" 
                    
                                            CssClass="mGrid"
                                            PagerStyle-CssClass="pgr" 
                                            AlternatingRowStyle-CssClass="alt" 
                                            emptydatatext="No booking data available for above period" OnRowCommand="grdViewKidsAtd_RowCommand" OnRowDeleting="grdViewKidsAtd_RowDeleting" OnPageIndexChanging="grdViewKidsAtd_PageIndexChanging" OnRowDataBound="grdViewKidsAtd_RowDataBound">

                                            <columns>
                                            
                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" >
                                                    <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                               </asp:TemplateField> 
                                            
                                               <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" Visible="false" >
                                                    <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbKidRef" Text='<%#Eval("kid_ref_no") %>'/>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" Visible="false" >
                                                    <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbUnixDate" Text='<%#Eval("unixdate_booked") %>'/>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                <asp:BoundField DataField="name_tag"  HeaderText=" Name"  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px"  />  
                                                <asp:BoundField DataField="date_booked"  HeaderText=" Booking Date" DataFormatString="{0:ddd, MMMM d, yyyy}" ItemStyle-HorizontalAlign="right" ItemStyle-Width="200px"  />   
                                                <asp:BoundField DataField="Session_booked"  HeaderText=" Session"   ItemStyle-HorizontalAlign="Right" ItemStyle-Width="60px"  />   
                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" Visible="false" >
                                                    <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbGridBookigType" Text='<%#Eval("booking_type") %>'/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                             

                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="80px" HeaderText="Booking Type" >
                                                 <ItemTemplate > 
                                                    <asp:Label ID="lbGridBookig" runat="server" Text='<%# BookingType( (string)Eval("Booking_Type"), Eval("Slot_No").ToString(), (string)Eval("category"), (string)Eval("status_code")) %>' ForeColor='<%# BookingColor((string)Eval("booking_type"), (string)Eval("status_code"))%>' ></asp:Label>
                                                 </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="activity_username" HeaderText=" Booked By"   ItemStyle-HorizontalAlign="Right" ItemStyle-Width="60px"  /> 
                                           
                                                 <asp:TemplateField  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                  <ItemTemplate>
                                                       <asp:ImageButton ID="btnDelete" name="DeleteButton" commandName="Delete" runat="server" ImageUrl="~/Images/cross.png" ToolTip="Delete" CausesValidation="false"/>   
                                                        <ajaxToolkit:ConfirmButtonExtender TargetControlId="btnDelete" ConfirmText="Delete this entry?" runat="server" />
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
 </div>
</asp:Content>

