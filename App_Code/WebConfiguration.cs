/*                                                                    
*                 IFS Corporate Services 
*
*  This program is protected by copyright law and by international
*  conventions. All licensing, renting, lending or copying (including
*  for private use), and all other use of the program, which is not
*  expressively permitted by IFS Corporate Services (IFS), is a
*  violation of the rights of IFS. Such violations will be reported to the
*  appropriate authorities.
*
*  VIOLATIONS OF ANY COPYRIGHT IS PUNISHABLE BY LAW AND CAN LEAD
*  TO UP TO TWO YEARS OF IMPRISONMENT AND LIABILITY TO PAY DAMAGES.
* ----------------------------------------------------------------------------
*  File        : WebConfiguration.cs 
*  Author      : Thisara Ranasinghe
*  Created Date: 2013-09-26
* ----------------------------------------------------------------------------
*  Modified Date   Signature      History
* --------------   ---------      -------
*  2013-09-26      thirlk         Created.
*  
*/
using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// This class extacts the information form the web.conf parameters
/// </summary>
public class WebConfiguration
{

    #region Get the parameter value 
    /// <summary>  
    ///Get the parameter value according to the key. 
    /// </summary>    
    /// <param name="key">The key value that need to extract the parameter value.</param>          
    /// <returns>Returns the value coresponding to the key value.</returns>   
    public static string getConfigValue(string key)
    {
        string value = string.Empty;
        try
        {
            value = ConfigurationManager.AppSettings[key];
        }
        catch (Exception ex) { 
        
        }

        return value;
    }
    #endregion

    //public static int Time_Diff_Sec{

    //    get
    //    {
    //        return Convert.ToInt32(getConfigValue("TIME_DIFF_SEC"));
    //    }
    //}
}
