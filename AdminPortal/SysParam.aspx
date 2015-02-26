<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SysParam.aspx.cs" Inherits="AdminPortal_SysParam" EnableViewState="true" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

       <div class ="topContentFrame" >

     <div class="btn-group" role="group" aria-label="...">
     <asp:LinkButton ID="LinkButton1" class= "btn btn-default" onclick="Attendance_Click" runat="server"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>&nbsp;Attendance</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton2" class= "btn btn-default" onclick="Stats_Click" runat="server"><span class="glyphicon glyphicon-stats" aria-hidden="true"></span>&nbsp;Stats</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton3" class= "btn btn-default" onclick="Contacts_Click" runat="server"><span class="glyphicon glyphicon-earphone" aria-hidden="true"></span>&nbsp;Contacts</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton4" class= "btn btn-default" onclick="Cancellations_Click" runat="server"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Cancellations</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton5" class= "btn btn-default" onclick="Kidsinfo_Click" runat="server"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;Kids info</asp:LinkButton>                                                                                      
     <asp:LinkButton ID="LinkButton6" class= "btn btn-default" onclick="Holidays_Click" runat="server"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>&nbsp;Holidays</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton7" class= "btn btn-default" onclick="Renewsub_Click" runat="server"><span class="glyphicon glyphicon-repeat" aria-hidden="true"></span>&nbsp;Subscription</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton8" class= "btn btn-primary" onclick="Sysparam_Click" runat="server"><span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>&nbsp;Parameters</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton9" class= "btn btn-default" onclick="Addkids_Click" runat="server"><span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span>&nbsp;Add Kids</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton10" class= "btn btn-default" onclick="Admins_Click" runat="server"><span class="glyphicon glyphicon-wrench" aria-hidden="true"></span>&nbsp;App</asp:LinkButton>                                             
    </div>
            <div style="float:right;padding:10px">
                <asp:Button ID="btRefresh" runat="server" Text="Cache Refresh" OnClick="btRefresh_Click" />
            </div>              
     </div>
     <div style="text-align:center"> 
            <asp:Label ID="lbInfo" runat="server" ForeColor="Red" Font-Size="0.8em" Visible="False" ></asp:Label>
     </div>
     <div class="gdView">                                                       
						<asp:GridView ID="grdViewSysParam" runat="server" AutoGenerateColumns="false" ShowFooter="true" 
                                    AllowPaging="true" 
                                    PageSize="100" AllowSorting="false"                    
                                    CssClass="mGrid"
                                    PagerStyle-CssClass="pgr" 
                                    AlternatingRowStyle-CssClass="alt" emptydatatext="No data available!"
                                    OnRowCancelingEdit="grdViewSysParam_RowCancelingEdit" OnRowDeleting="grdViewSysParam_RowDeleting" OnRowEditing="grdViewSysParam_RowEditing" OnRowUpdating="grdViewSysParam_RowUpdating" OnRowCommand="grdViewSysParam_RowCommand" >
                                    <columns>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" >
                                            <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>  

                                       <asp:TemplateField HeaderText="Pram Name" ItemStyle-HorizontalAlign="Left">
                                             <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbParamName" Text='<%#Eval("param") %>'/>
                                             </ItemTemplate>  
                                             <FooterTemplate>
                                                <asp:TextBox ID="txtParamNameIns" runat="server" Width="200px"></asp:TextBox>                                          
                                             </FooterTemplate>                                           
                                        </asp:TemplateField> 

                                        <asp:TemplateField HeaderText="Pram Value" ItemStyle-HorizontalAlign="Left">
                                             <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbPramValue" Text='<%#Eval("param_value") %>'/>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                  <asp:TextBox runat="server" ID="txtParamValue" Width="350px" Height="200px" TextMode="MultiLine" Text='<%#Eval("param_value") %>' />
                                                  <asp:RequiredFieldValidator runat="server" ID="rfdParamValue" ControlToValidate="txtParamValue" EnableClientScript="true" ErrorMessage="*" Display="Dynamic" CssClass="ErrorText" />
                                             </EditItemTemplate>
                                              <FooterTemplate>
                                                <asp:TextBox ID="txtParamValueIns" Width="350px" runat="server" TextMode="MultiLine"></asp:TextBox>                                          
                                             </FooterTemplate> 
                                        </asp:TemplateField>  

                                        <asp:TemplateField HeaderText="Comments" ItemStyle-HorizontalAlign="Left">
                                             <ItemTemplate>
                                                  <asp:Label runat="server" ID="lbComments" Text='<%#Eval("comments") %>'/>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                  <asp:TextBox runat="server" ID="txtComments" TextMode="MultiLine" Width="300px" Height="200px" Text='<%#Eval("comments") %>' />                                                 
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                <asp:TextBox ID="txtCommentsIns" runat="server" TextMode="MultiLine" Width="300px"></asp:TextBox>                                          
                                             </FooterTemplate> 
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField>
                                              <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" Text="Edit" runat="server" CommandName="Edit" />
                                              </ItemTemplate>
                                              <EditItemTemplate>
                                                    <asp:LinkButton ID="btnUpdate" Text="Update" runat="server" CommandName="Update" />
                                                    <asp:LinkButton ID="btnCancel" Text="Cancel" runat="server" CommandName="Cancel" />
                                              </EditItemTemplate>                                              
                                        </asp:TemplateField>  
                                         
                                        <asp:TemplateField>
                                              <ItemTemplate>
                                                   <asp:ImageButton ID="btnDelete" name="DeleteButton" commandName="Delete" runat="server" ImageUrl="~/Images/cross.png" ToolTip="Delete" CausesValidation="false"/>   
                                                    <ajaxToolkit:ConfirmButtonExtender TargetControlId="btnDelete" ConfirmText="Delete this entry?" runat="server" />
                                              </ItemTemplate>
                                                                                                                                                                                                                                       
                                         <FooterTemplate>
                                            <asp:LinkButton ID="lbtnInsert" runat="server" CommandName="Insert">Insert</asp:LinkButton>
                                         </FooterTemplate> 

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

</asp:Content>

