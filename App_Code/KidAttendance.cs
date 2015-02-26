using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for KidsAttendance
/// </summary>
/// 
public class KidAttendance
{
    private int seqNo;
    private string kidRefNo;
    private string kidTagName;
    private double age;
    private DateTime dateBooked;
    private int unixDateBoked;
    private string sessionBooked;
    private int statusCode;
    private string activityUserName;
    private string bookingType;

    public KidAttendance(string kidRefNo_, string sessionBooked_, DateTime dateBooked_, int statusCode_, string activityUserName_)
	{
        this.kidRefNo = kidRefNo_;
        this.sessionBooked = sessionBooked_;
        this.dateBooked = dateBooked_;
        this.statusCode = statusCode_;
        this.activityUserName = activityUserName_;
	}

    public KidAttendance(int seqNo_, string kidRefNo_, string kidTagName_, double age_, string sessionBooked_, DateTime dateBooked_, int unixDateBoked_ , string bookingType_, int statusCode_)
    {
        this.seqNo = seqNo_;
        this.kidRefNo = kidRefNo_;
        this.kidTagName = kidTagName_;
        this.age = age_;
        this.sessionBooked = sessionBooked_;
        this.dateBooked = dateBooked_;
        this.unixDateBoked = unixDateBoked_;
        this.bookingType = bookingType_;
        this.statusCode = statusCode_;
    }

    public int SeqNo
    {
        get { return seqNo; }       
    }

    public string KidRefNo
    {
        get { return kidRefNo; }       
    }

    public string KidTagName
    {
        get { return kidTagName; }
    }

    public double Age
    {
        get { return age; }
    }

    public DateTime DateBooked
    {
        get { return Convert.ToDateTime( dateBooked.ToString("d") ); }
    }

    public int UnixDateBoked
    {
        get { return unixDateBoked; }

    }

    public string SessionBooked
    {
        get { return sessionBooked; }
    }

    public int StatusCode
    {
        get { return statusCode; }
    }

    public string ActivityUserName
    {
        get { return activityUserName; }
    }

    public bool IsToddler {

        get
        {
            int toddlerMaxAge = Convert.ToInt32( IFS.CoS.ServiceUtil.SysParamReaderUtil.GetSysParamByName("TODDLER_AGE_LIMIT") );
            if (age <= toddlerMaxAge)
                return true;
            else
                return false;
        }
    }

    public string BookingType
    {
        get { return bookingType; }
    }

}