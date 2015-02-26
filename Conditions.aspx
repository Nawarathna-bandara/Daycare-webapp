<%@ Page Title="IFS Juniors Attendance Forecast" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Conditions.aspx.cs" Inherits="_Default" EnableEventValidation="false"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
   
    <style type="text/css">
        .auto-style1 {
            padding: 5px 15px; /*width: 182px;*/
	        /*width: 100%;*/
            height: 25px;
            vertical-align: top; /*	background-color: #EBEBEB; */
            background-color: #f5f5f5; /*	background-color: #e9e3cd;*/ /*border-top: 1px solid #f3f3f3;
	        border-left: 1px solid #f3f3f3;
	        border-right: 1px solid #f3f3f3;*/
            border-bottom: 1px solid #f5f5f5;
            font-family: arial, Verdana, Helvetica, sans-serif;
            font-size: 0.9m;
            text-align: left;
        }
    p.MsoNormal
	{margin-bottom:.0001pt;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
	    margin-left: 0in;
        margin-right: 0in;
        margin-top: 0in;
    }
p.MsoListParagraph
	{margin-top:0in;
	margin-right:0in;
	margin-bottom:0in;
	margin-left:.5in;
	margin-bottom:.0001pt;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
	}
    </style>
   
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   
    <script type="text/javascript" language="javascript">

        //var requestManager = Sys.WebForms.PageRequestManager.getInstance();
        //requestManager.add_initializeRequest(CancelPostbackForSubsequentSubmitClicks);

        //function CancelPostbackForSubsequentSubmitClicks(sender, args) {
        //    if (requestManager.get_isInAsyncPostBack() & args.get_postBackElement().id == 'mainContent_btnAdd') {
        //        args.set_cancel(true);
        //        alert('Wait, A previous request is still in progress');
        //    }
        //}

    </script>

     

     <table class="contentArea" cellspacing="0" cellpadding="10" width="100%">
            <tr>
                
                <td class="auto-style1">
                       
                     &nbsp;</td>
            </tr>
                                   
            <tr>
                <td class="userNavPP">
                    <div style="text-align:center"> 
                            <asp:Label ID="LbText" runat="server" ForeColor="Red" Font-Size="0.9em" Visible="False" ></asp:Label>                            
                     </div> 
                    <asp:UpdatePanel runat="server" id="UpdatePanel1" updatemode="Conditional">
                    
                    <ContentTemplate>
                        <div style="text-align:center"> 
                            <asp:Label ID="lbInfo" runat="server" ForeColor="Green" Font-Size="0.8em" Visible="False" ></asp:Label>
                            <p class="MsoNormal">
                                <b><u><span>Conditions for Setting Priority<p></p>
                                </span></u></b>
                            </p>
                            <p class="MsoNormal">
                                <b><u><span>
                                <p>
                                &nbsp;</p>
                                </span></u></b>
                            </p>
                            <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
                                <![if !supportLists]><b><span>1.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></b><![endif]><b><span>Priority for smaller children<p></p>
                                </span></b>
                            </p>
                            <p class="MsoNormal" style="margin-left:.5in">
                                <span>This condition should be based on 2 categories, Toddlers and Elders.
                                <p>
                                </p>
                                </span>
                            </p>
                            <p class="MsoNormal" style="margin-left:.5in">
                                <span>Toddlers are from 2 years to 3 years. If there are more slots available in toddler’s category, elders will be taken into the vacant toddler category and be given priority for youngers from elder category.
                                <p>
                                </p>
                                </span>
                            </p>
                            <p class="MsoNormal" style="margin-left:.5in">
                                <span>
                                <p>
                                &nbsp;</p>
                                </span>
                            </p>
                            <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
                                <![if !supportLists]><b><span>2.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></b><![endif]><b><span>Priority for kids who do not come regularly
                                <p>
                                </p>
                                </span></b>
                            </p>
                            <p class="MsoNormal" style="margin-left:.5in">
                                <span>
                                <p>
                                &nbsp;</p>
                                </span>
                            </p>
                            <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
                                <![if !supportLists]><b><span>3.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></b><![endif]><b><span>Usage / Cancellation&nbsp;
                                <p>
                                </p>
                                </span></b>
                            </p>
                            <p class="MsoListParagraph">
                                <b><span>
                                <p>
                                &nbsp;</p>
                                </span></b>
                            </p>
                            <p class="MsoListParagraph">
                                <b><span>There are 2 types of cancellations as explained below.<p></p>
                                </span></b>
                            </p>
                            <p class="MsoListParagraph">
                                <b><span>
                                <p>
                                &nbsp;</p>
                                </span></b>
                            </p>
                            <p class="MsoListParagraph" style="margin-left:.75in;text-indent:-.25in;
mso-list:l1 level1 lfo2">
                                <![if !supportLists]><b><span>A.&nbsp;&nbsp;&nbsp;&nbsp; </span></b><![endif]><b><span>Absence without cancelling the reservation<p></p>
                                </span></b>
                            </p>
                            <p class="MsoListParagraph" style="margin-left:.75in">
                                <span>If the booking is not cancelled before 4.30 PM the earlier day, it will be considered for penalty.<p></p>
                                </span>
                            </p>
                            <p class="MsoNormal" style="margin-left:.5in">
                                <span>We may give 3 free chances for a calendar year and negative marks will be applied if they do not cancel more than 3 times.<p></p>
                                </span>
                            </p>
                            <p class="MsoNormal" style="margin-left:.5in">
                                <span>
                                <p>
                                &nbsp;</p>
                                </span>
                            </p>
                            <p class="MsoNormal" style="margin-left:.5in">
                                <span>1<sup>st</sup> negative mark will not have much weight; the negative marks will get bigger according to the number of times.
                                <p>
                                </p>
                                </span>
                            </p>
                            <p class="MsoNormal" style="margin-left:.5in">
                                <span>Eg: The negative marks for 5<sup>th</sup> time will be bigger than 4<sup>th </sup>time.<p></p>
                                </span>
                            </p>
                            <p class="MsoNormal" style="margin-left:.5in">
                                <span>
                                <p>
                                &nbsp;</p>
                                </span>
                            </p>
                            <p class="MsoNormal" style="margin-left:.5in">
                                <span>If a tentative booking gets a fixed slot within the last 24 hours, and if that parent does not cancel if the kid is not coming next day, he or she will not be given any negative marks as it is last minute solution from us, hence parent may have found another option already.<p></p>
                                </span>
                            </p>
                            <p class="MsoNormal">
                                <b><span>
                                <p>
                                &nbsp;</p>
                                </span></b>
                            </p>
                            <p class="MsoListParagraph" style="margin-left:.75in;text-indent:-.25in;
mso-list:l1 level1 lfo2">
                                <![if !supportLists]><b><span>B.&nbsp;&nbsp;&nbsp;&nbsp; </span></b><![endif]><b><span>Making reservations without a plan<p></p>
                                </span></b>
                            </p>
                            <p class="MsoNormal" style="margin-left:.5in">
                                <span>Even if the parent is cancelling the reservation, if he/she cancels quite often, that means that they do not plan ahead.<p></p>
                                </span>
                            </p>
                            <p class="MsoNormal" style="margin-left:.5in">
                                <span>To avoid such behavior, negative marks will be added if the parent is cancelling a booking done for more than two days in a week, and if this pattern happens more than 6 times.<p></p>
                                </span>
                            </p>
                            <p class="MsoNormal">
                                <span>
                                <p>
                                &nbsp;</p>
                                </span>
                            </p>
                            <p class="MsoNormal">
                                <b><i><span>All above conditions will be reset when a new calendar year begins.
                                <p>
                                </p>
                                </span></i></b>
                            </p>
                        </div>  
                    </ContentTemplate>
                    </asp:UpdatePanel>    
                    
                                                          
                </td>
            </tr>
        </table>

</asp:Content>