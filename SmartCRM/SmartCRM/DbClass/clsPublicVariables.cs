using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.VisualBasic;


namespace SmartCRM
{
    public class clsPublicVariables
    {
        public enum enumDbType
        {
            MsAccess = 0,
            MsSql = 1,
            MySql = 2
        };

        public enum enumFormModes
        {
            AddRecord = 0,
            EditRecord = 1,
            DeleteRecord = 2,
            DisplayForm = 3
        };

        public const string Project_Heading = "Smart CRM";
        public static string AppPath = Path.GetDirectoryName(Application.ExecutablePath).ToString();
        public static string AccessPassword="JayBhavsar@2407";
        public static string PROJECT_TITLE = "Smart CRM";
        public static string Database_Path = AppPath + "\\Database\\SmartCRM.mdb";

        ////public const string INIFileName = "SoftClinic.ini";
        ////public static string INIFilePath = AppPath + "\\" + INIFileName;
        ////public static string PatientPhotoPath = AppPath + "\\Database\\Photos\\";
        ////public static string PatientImagesPath = AppPath + "\\Database\\Images\\";
        ////public static string BalnkImgPhotoPath = AppPath + "\\Database\\Photos\\BlankImage.jpg";

        ////public static string PatientpDFPath = AppPath + "\\Database\\Pdf\\";
        ////public static string ReportPath = AppPath + "\\Report\\";

        ////public static string PatientBlankPhotoPath = AppPath + "\\blankpatient.png";

        ////public const string infoFileName = "wmud.dat";
        ////public static string InfoFilePath = AppPath + "\\" + infoFileName;

        ////public static string gEMailMessage = "This is an Automated E-Mail Sent from Jvs Group.\n\r" + "Please find the Software Error File Attached.";

        ////public static string ERROR_LOG_NAME = "err";
        ////public static string ERROR_LOG_PATH = AppPath + "\\" + ERROR_LOG_NAME + ".log";

        ////public static string DataConfigXmlFilePath = AppPath + "\\jvsgeneral.xml";

        ////// == WUMD Variables
       
        ////public static string Registered_Owner;
        ////public static string Expiry_date;
        ////public static string Product_Name;
        ////public static string License_Type;
        ////
        ////public static string Software_Type;
        ////public static string SerialNo;
        ////public static long Act_Users;
        ////public static long No_Users;
        ////public static string DateCriteriaConst;

        ////public static bool IsPharmaMod = false;
        ////public static bool IsLabMod = false;
        ////public static bool IsIndoorMod = false;
        ////public static bool IsInventoryModule = false;
        ////public static bool IsRadiologyMod = false;
        ////public static bool IsSMSMod = false;
        ////public static bool IsTreatMod = false;
        ////public static bool IsInsuranceMod = false;
        ////public static bool IsDietMod = false;
        ////public static bool IsHRModule = false;
        ////public static bool IsAccModule = false;

        ////public static enumDbType DataBaseType;

        ////public static string SqlServerName;
        ////public static string SqlUserName;
        ////public static string SqlPassword;
        ////public static string SqlDatabaseName;

        ////
        ////public static bool BranchCodeYn = false;
        ////public static string BranchCode = "";
        ////public static long BranchCodeId = 0;

        ////public static long User_Id;

        ////public static string SaveDataMsg = "Data Saved Successfully. ";
        ////public static string DeleteDataMsg = "Are you sure you want to Delete the record ? ";
        ////public static string ExitFormMsg = "Are you sure to Quit ? ";

        ////private bool _isInward;
        ////private bool _isIncpurchaseprice;

        ////public bool IsIncpurchaseprice
        ////{
        ////    get { return _isIncpurchaseprice; }
        ////    set { _isIncpurchaseprice = value; }
        ////}

        ////public bool IsInward
        ////{
        ////    get { return _isInward; }
        ////    set { _isInward = value; }
        ////}


    }
}
