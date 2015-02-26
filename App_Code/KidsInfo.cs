using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for KidsInfo
/// </summary>
public class KidsInfo
{
    private DateTime DoB;

	public KidsInfo()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    public int RefNo{ get; set;}
    public string NameTag{ get; set;}
    public string FullName{ get; set;}
   
    public DateTime DateOfBirth
    {
        get
        {
            return Convert.ToDateTime(this.DoB.ToString("d"));
        }
        set
        {
            this.DoB = value;
        }
    }
    public string Gender{ get; set;}
    public string StatusCode{ get; set;}
    public string SubYear{ get; set;}
    public string ActivityTimestamp{ get; set;}
    public string FatherName{ get; set;}
    public string FatherUserName { get; set; }
    public string FatherEmail { get; set; }
    public string FatherContactNo{ get; set;}
    public string MotherName{ get; set;}
    public string MotherUserName { get; set; }
    public string MotherEmail { get; set; }
    public string MotherContactNo{ get; set;}
    public string Residence{ get; set;}
    public string ResContactNo{ get; set;}
    public string Notes{ get; set;}

}