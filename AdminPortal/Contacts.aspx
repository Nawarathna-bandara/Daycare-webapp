<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Contacts.aspx.cs" Inherits="AdminPortal_Overview" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="mainContent" Runat="Server">
   

    <div class="btn-group" role="group" aria-label="...">
     <asp:LinkButton ID="LinkButton1" class= "btn btn-default" onclick="Attendance_Click" runat="server"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>&nbsp;Attendance</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton2" class= "btn btn-default" onclick="Stats_Click" runat="server"><span class="glyphicon glyphicon-stats" aria-hidden="true"></span>&nbsp;Stats</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton3" class= "btn btn-primary" onclick="Contacts_Click" runat="server"><span class="glyphicon glyphicon-earphone" aria-hidden="true"></span>&nbsp;Contacts</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton4" class= "btn btn-default" onclick="Cancellations_Click" runat="server"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Cancellations</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton5" class= "btn btn-default" onclick="Kidsinfo_Click" runat="server"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;Kids info</asp:LinkButton>                                                                                      
     <asp:LinkButton ID="LinkButton6" class= "btn btn-default" onclick="Holidays_Click" runat="server"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>&nbsp;Holidays</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton7" class= "btn btn-default" onclick="Renewsub_Click" runat="server"><span class="glyphicon glyphicon-repeat" aria-hidden="true"></span>&nbsp;Subscription</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton8" class= "btn btn-default" onclick="Sysparam_Click" runat="server"><span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>&nbsp;Parameters</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton9" class= "btn btn-default" onclick="Addkids_Click" runat="server"><span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span>&nbsp;Add Kids</asp:LinkButton>                                           
     <asp:LinkButton ID="LinkButton10" class= "btn btn-default" onclick="Admins_Click" runat="server"><span class="glyphicon glyphicon-wrench" aria-hidden="true"></span>&nbsp;App</asp:LinkButton>                                             
    </div>

    <table>

       <tr>

            <td style="padding:20px; vertical-align:top;" rowspan="2">

                

                <div class="panel panel-default">

                 <div class="panel-heading" style="text-align:left"> <span class="glyphicon glyphicon-align-left" aria-hidden="true"></span>&nbsp;&nbsp;Sort By </div>

                  <div class="panel-body">                  
                    
                    <asp:RadioButton ID="sortby_no" GroupName="sortby" runat="server" AutoPostBack="true">
                    </asp:RadioButton>&nbsp;Ref No<br/>
                    
                    <asp:RadioButton ID="sortby_kidid" GroupName="sortby" runat="server" AutoPostBack="true">
                    </asp:RadioButton>&nbsp; Kidname <br/>
                     
                  </div>
                </div>


                <div class="panel panel-default">

                 <div class="panel-heading" style="text-align:left"> <span class="glyphicon glyphicon-share" aria-hidden="true"></span>&nbsp;&nbsp; Export  </div>

                  <div class="panel-body">                  
                                                        
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Exel" />
                                                        
                  </div>
                </div>


            </td >

           </tr>
        <tr> 
            <td colspan="2">

               <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

                <Triggers>
                    <asp:AsyncPostBackTrigger controlid="sortby_no" eventname="CheckedChanged" />   
                   <asp:AsyncPostBackTrigger controlid="sortby_kidid" eventname="CheckedChanged" />   

                </Triggers>  
                                         
                <ContentTemplate>

               <table>

                   <tr>
                       
                       <td style="padding:20px; vertical-align:top;">
                         
                                <asp:GridView ID="contactsgrid" runat="server" AutoGenerateColumns="False" Cssclass="mGrid" >
                                    <AlternatingRowStyle BackColor="#ECD7EC" />
                                    <Columns>
                                        <asp:BoundField DataField="REF_NO" HeaderText="&nbsp;No &nbsp;&nbsp;" />
                                        <asp:BoundField DataField="NAME_TAG" HeaderText="Kid &nbsp;&nbsp;" />
                                        <%--<asp:BoundField DataField="GUARDIAN_USERNAME" HeaderText=" Guardian nametag &nbsp;&nbsp;" />--%>
                                        <asp:BoundField DataField="FULLNAME" HeaderText="Guardian name &nbsp;&nbsp;" />
                                        <asp:BoundField DataField="RELATIONSHIP" HeaderText="Relationship &nbsp;&nbsp;" />
                                        <asp:BoundField DataField="EMAIL" HeaderText="E-mail address &nbsp;&nbsp;" />

                                    </Columns>
                                    <HeaderStyle BackColor="#873E8D" ForeColor="White" BorderColor="#CCCCCC" Font-Bold="True" Font-Size="Small" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
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

