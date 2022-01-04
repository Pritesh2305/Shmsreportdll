using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace Shmsreportdll
{
    public class clsGeneral : clsPublicVariables
    {

        private Int64 _para1;

        public Int64 Para1
        {
            get { return _para1; }
            set { _para1 = value; }
        }

        private DateTime _fromdate;

        public DateTime Fromdate
        {
            get { return _fromdate; }
            set { _fromdate = value; }
        }

        private DateTime _todate;

        public DateTime Todate
        {
            get { return _todate; }
            set { _todate = value; }
        }

        private void GetConnectionDetails()
        {
            try
            {
                ServerName1 = System.Configuration.ConfigurationManager.AppSettings["SERVER"];
                DatabaseName1 = System.Configuration.ConfigurationManager.AppSettings["DATABASE"];
                UserName1 = System.Configuration.ConfigurationManager.AppSettings["DBUSERID"];
                Password1 = System.Configuration.ConfigurationManager.AppSettings["DBPASSWORD"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in GetConnectionDetails())");
            }
        }

        private void GetSettingDetails()
        {
            DataTable dtsetting = new DataTable();
            clsMsSqlDbFunction mysql = new clsMsSqlDbFunction();
            try
            {
                dtsetting = mysql.FillDataTable("SELECT * FROM SHMSSETTING", "SHMSSETTING");

                RptTitle1 = "";
                RptTitle2 = "";
                RptTitle3 = "";
                RptTitle4 = "";
                RptTitle5 = "";
                RptTitle6 = "";
                RptTitle7 = "";
                RptTitle8 = "";

                RptPrintername = "";
                RptExtraBillPrintername = "";
                RptBillPrintername = "";

                RptFooter1 = "";
                RptFooter2 = "";
                RptFooter3 = "";
                RptFooter4 = "";
                RptFooter5 = "";
                RptFooter6 = "";
                RptFooter7 = "";
                RptFooter8 = "";

                CbHeader1 = "";
                CbHeader2 = "";
                CbHeader3 = "";
                CbFooter1 = "";
                CbFooter2 = "";
                CbFooter3 = "";

                CrHeader1 = "";
                CrHeader2 = "";
                CrHeader3 = "";
                CrFooter1 = "";
                CrFooter2 = "";
                CrFooter3 = "";
                BillTitle = "";


                if (dtsetting.Rows.Count > 0)
                {
                    foreach (DataRow row in dtsetting.Rows)
                    {
                        RptTitle1 = (row["GENERALTITLE1"]) + "".Trim();
                        RptTitle2 = (row["GENERALTITLE2"]) + "".Trim();
                        RptTitle3 = (row["GENERALTITLE3"]) + "".Trim();
                        RptTitle4 = (row["GENERALTITLE4"]) + "".Trim();
                        RptTitle5 = (row["GENERALTITLE5"]) + "".Trim();
                        RptTitle6 = (row["GENERALTITLE6"]) + "".Trim();
                        RptTitle7 = (row["GENERALTITLE7"]) + "".Trim();
                        RptTitle8 = (row["GENERALTITLE8"]) + "".Trim();

                        RptFooter1 = (row["GENERALFOOTER1"]) + "".Trim();
                        RptFooter2 = (row["GENERALFOOTER2"]) + "".Trim();
                        RptFooter3 = (row["GENERALFOOTER3"]) + "".Trim();
                        RptFooter4 = (row["GENERALFOOTER4"]) + "".Trim();
                        RptFooter5 = (row["GENERALFOOTER5"]) + "".Trim();
                        RptFooter6 = (row["GENERALFOOTER6"]) + "".Trim();
                        RptFooter7 = (row["GENERALFOOTER7"]) + "".Trim();
                        RptFooter8 = (row["GENERALFOOTER8"]) + "".Trim();

                        //RptPrintername = (row["REPORTPRINTER"]) + "".Trim();

                        CbHeader1 = (row["CBHEADER1"]) + "".Trim();
                        CbHeader2 = (row["CBHEADER2"]) + "".Trim();
                        CbHeader3 = (row["CBHEADER3"]) + "".Trim();
                        CbFooter1 = (row["CBFOOTER1"]) + "".Trim();
                        CbFooter2 = (row["CBFOOTER2"]) + "".Trim();
                        CbFooter3 = (row["CBFOOTER3"]) + "".Trim();

                        CrHeader1 = (row["CRHEADER1"]) + "".Trim();
                        CrHeader2 = (row["CRHEADER2"]) + "".Trim();
                        CrHeader3 = (row["CRHEADER3"]) + "".Trim();
                        CrFooter1 = (row["CRFOOTER1"]) + "".Trim();
                        CrFooter2 = (row["CRFOOTER2"]) + "".Trim();
                        CrFooter3 = (row["CRFOOTER3"]) + "".Trim();
                        BillTitle = (row["BILLTITLE"]) + "".Trim();

                        /// Load Setting
                        string priterpath = clsPublicVariables.AppPath + "\\printersetting.txt";
                        string printername1 = "";
                        string[] printernm;
                        if (System.IO.File.Exists(priterpath))
                        {
                            TextReader tr = new StreamReader(priterpath);
                            // read a line of text
                            printername1 = tr.ReadLine().ToString();
                            printernm = printername1.Split('|');

                            RptBillPrintername = printernm[0] + "".Trim();
                            RptExtraBillPrintername = printernm[1] + "".Trim();
                            RptPrintername = printernm[2] + "".Trim();
                            RptCouponPrintername = printernm[3] + "".Trim();
                            RptRefundPrintername = printernm[4] + "".Trim();
                            // }
                            tr.Close();
                        }
                        else
                        {
                            PrinterSettings settings = new PrinterSettings();
                            RptPrintername = settings.PrinterName;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in GetSettingDetails())");
            }
        }

        public double Null2Dbl(object Numbr1)
        {
            Double Amt1;
            try
            {

                if (DBNull.Value == Numbr1)
                {
                    Amt1 = 0;
                }

                else if ((string.IsNullOrEmpty(Numbr1.ToString()) || (Numbr1.ToString().Trim() == "")))
                {
                    Amt1 = 0;
                }
                else
                {
                    Amt1 = (double)Numbr1;
                }

                return (double)Amt1;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public long Null2lng(object Numbr1)
        {
            long Amt1;
            try
            {

                if (DBNull.Value == Numbr1)
                {
                    Amt1 = 0;
                }

                else if ((string.IsNullOrEmpty(Numbr1.ToString()) || (Numbr1.ToString().Trim() == "")))
                {
                    Amt1 = 0;
                }
                else
                {
                    Amt1 = Convert.ToInt64(Numbr1);
                }

                return Convert.ToInt64(Amt1);
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public DateTime Null2Date(object Dt1)
        {
            DateTime tDt1;
            //DateTime eDt1;
            bool IsEmpDateYn;
            try
            {
                if (DBNull.Value == Dt1)
                {
                    IsEmpDateYn = true;
                }
                else if (Dt1.ToString() == "1/1/1753")
                {
                    IsEmpDateYn = true;
                }
                else if ((Dt1.ToString().Trim() == ""))
                {
                    IsEmpDateYn = true;
                }
                else
                {
                    IsEmpDateYn = false;
                }
                if ((IsEmpDateYn == true))
                {
                    tDt1 = DateTime.MinValue;
                }
                else
                {
                    tDt1 = ((DateTime)(Dt1));
                }

                return tDt1;
            }
            catch (Exception)
            {

                return (DateTime)Dt1;
            }
        }

        public string Null2Str(object Str1)
        {
            string tStr1;
            try
            {
                if (DBNull.Value == (Str1))
                {
                    tStr1 = " ";
                }
                else if (string.IsNullOrEmpty(Str1.ToString()))
                {
                    tStr1 = " ";
                }
                else
                {
                    tStr1 = Str1.ToString();
                }
                return tStr1;
            }
            catch (Exception)
            {
                return Str1.ToString();
            }
        }

        public void OpenForm(string FormName_1, string ModuleId_1)
        {
            frmImage frmimg = new frmImage();

            try
            {
                this.GetConnectionDetails();

                this.GetSettingDetails();

                clsPublicVariables.enumSHMSReports FormEnum = (clsPublicVariables.enumSHMSReports)Enum.Parse(typeof(clsPublicVariables.enumSHMSReports), FormName_1, true);

                switch (FormEnum)
                {
                    case enumSHMSReports.SHMS_ROOMCHECKOUTBILL:
                        frmReport frm2 = new frmReport();
                        frm2.Instance.ReportName = FormEnum;
                        frm2.Instance.ModuleId = ModuleId_1;
                        frm2.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_PERFOMAINVOICE:
                        frmReport frm3 = new frmReport();
                        frm3.Instance.ReportName = FormEnum;
                        frm3.Instance.ModuleId = ModuleId_1;
                        frm3.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_CASHRECEIPT:
                        frmReport frm4 = new frmReport();
                        frm4.Instance.ReportName = FormEnum;
                        frm4.Instance.ModuleId = ModuleId_1;
                        frm4.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_EXTRASERBILL:
                        frmReport frm5 = new frmReport();
                        frm5.Instance.ReportName = FormEnum;
                        frm5.Instance.ModuleId = ModuleId_1;
                        frm5.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_REGISTRATIONCARD:
                        frmReport frm6 = new frmReport();
                        frm6.Instance.ReportName = FormEnum;
                        frm6.Instance.ModuleId = ModuleId_1;
                        frm6.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_BREAKFASTCOUPONS:
                        frmReport frm7 = new frmReport();
                        frm7.Instance.ReportName = FormEnum;
                        frm7.Instance.ModuleId = ModuleId_1;
                        frm7.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_LUNCHCOUPONS:
                        frmReport frm8 = new frmReport();
                        frm8.Instance.ReportName = FormEnum;
                        frm8.Instance.ModuleId = ModuleId_1;
                        frm8.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_DINNERCOUPONS:
                        frmReport frm9 = new frmReport();
                        frm9.Instance.ReportName = FormEnum;
                        frm9.Instance.ModuleId = ModuleId_1;
                        frm9.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISC:
                        frmReport frm10 = new frmReport();
                        frm10.Instance.ReportName = FormEnum;
                        frm10.Instance.ModuleId = ModuleId_1;
                        frm10.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_PERFOMAINVOICEWODISC:
                        frmReport frm11 = new frmReport();
                        frm11.Instance.ReportName = FormEnum;
                        frm11.Instance.ModuleId = ModuleId_1;
                        frm11.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_ROOMCHECKOUTBILLUSD:
                        frmReport frm12 = new frmReport();
                        frm12.Instance.ReportName = FormEnum;
                        frm12.Instance.ModuleId = ModuleId_1;
                        frm12.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISCUSD:
                        frmReport frm13 = new frmReport();
                        frm13.Instance.ReportName = FormEnum;
                        frm13.Instance.ModuleId = ModuleId_1;
                        frm13.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_PERFOMAINVOICEUSD:
                        frmReport frm14 = new frmReport();
                        frm14.Instance.ReportName = FormEnum;
                        frm14.Instance.ModuleId = ModuleId_1;
                        frm14.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_PERFOMAINVOICEWODISCUSD:
                        frmReport frm15 = new frmReport();
                        frm15.Instance.ReportName = FormEnum;
                        frm15.Instance.ModuleId = ModuleId_1;
                        frm15.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_DATEWISELUXURYTAXREG:
                        frmReport frm16 = new frmReport();
                        frm16.Instance.ReportName = FormEnum;
                        frm16.Instance.ModuleId = ModuleId_1;
                        frm16.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_DATEWISEVATTAXREG:
                        frmReport frm17 = new frmReport();
                        frm17.Instance.ReportName = FormEnum;
                        frm17.Instance.ModuleId = ModuleId_1;
                        frm17.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_DATEWISEEXTTAX1REG:
                        frmReport frm18 = new frmReport();
                        frm18.Instance.ReportName = FormEnum;
                        frm18.Instance.ModuleId = ModuleId_1;
                        frm18.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_CHECKOUTBILLREG:
                        frmReport frm19 = new frmReport();
                        frm19.Instance.ReportName = FormEnum;
                        frm19.Instance.ModuleId = ModuleId_1;
                        frm19.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHSM_TARIFF_BILL:
                        frmReport frm20 = new frmReport();
                        frm20.Instance.ReportName = FormEnum;
                        frm20.Instance.ModuleId = ModuleId_1;
                        frm20.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_WOTARIFF_BILL:
                        frmReport frm21 = new frmReport();
                        frm21.Instance.ReportName = FormEnum;
                        frm21.Instance.ModuleId = ModuleId_1;
                        frm21.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_WOTARIFF_DIFFBILL:
                        frmReport frm21a = new frmReport();
                        frm21a.Instance.ReportName = FormEnum;
                        frm21a.Instance.ModuleId = ModuleId_1;
                        frm21a.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_ENTRYTICKET:
                        frmReport frm22 = new frmReport();
                        frm22.Instance.ReportName = FormEnum;
                        frm22.Instance.ModuleId = ModuleId_1;
                        frm22.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_ENTRYTICKETDETAILCOLLECTIONRPT:
                        frmReport frm23 = new frmReport();
                        frm23.Instance.ReportName = FormEnum;
                        frm23.Instance.ModuleId = ModuleId_1;
                        frm23.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_ENTRYTICKETSUMMARYCOLLECTIONRPT:
                        frmReport frm24 = new frmReport();
                        frm24.Instance.ReportName = FormEnum;
                        frm24.Instance.ModuleId = ModuleId_1;
                        frm24.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_COUPONBILLDETAILCOLLECTIONRPT:
                        frmReport frm25 = new frmReport();
                        frm25.Instance.ReportName = FormEnum;
                        frm25.Instance.ModuleId = ModuleId_1;
                        frm25.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_COUPONBILLSUMMARYCOLLECTIONRPT:
                        frmReport frm26 = new frmReport();
                        frm26.Instance.ReportName = FormEnum;
                        frm26.Instance.ModuleId = ModuleId_1;
                        frm26.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_COUPONREFUNDDETAILCOLLECTIONRPT:
                        frmReport frm27 = new frmReport();
                        frm27.Instance.ReportName = FormEnum;
                        frm27.Instance.ModuleId = ModuleId_1;
                        frm27.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_COUPONBILLITEMWISEDETAILCOLLECTIONRPT:
                        frmReport frm27a = new frmReport();
                        frm27a.Instance.ReportName = FormEnum;
                        frm27a.Instance.ModuleId = ModuleId_1;
                        frm27a.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_COUPONREFUNDSUMMARYCOLLECTIONRPT:
                        frmReport frm28 = new frmReport();
                        frm28.Instance.ReportName = FormEnum;
                        frm28.Instance.ModuleId = ModuleId_1;
                        frm28.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_COUPONBILL:
                        frmReport frm29 = new frmReport();
                        frm29.Instance.ReportName = FormEnum;
                        frm29.Instance.ModuleId = ModuleId_1;
                        frm29.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_COUPONREFUND:
                        frmReport frm30 = new frmReport();
                        frm30.Instance.ReportName = FormEnum;
                        frm30.Instance.ModuleId = ModuleId_1;
                        frm30.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_COUPONBILLREGDATEWISE:
                        frmReport frm31 = new frmReport();
                        frm31.Instance.ReportName = FormEnum;
                        frm31.Instance.ModuleId = ModuleId_1;
                        frm31.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_POLICEINQUIRYREPORT:
                        frmReport frm32 = new frmReport();
                        frm32.Instance.ReportName = FormEnum;
                        frm32.Instance.ModuleId = ModuleId_1;
                        frm32.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_SEGMENTWISESUMMARY:
                        frmReport frm33 = new frmReport();
                        frm33.Instance.ReportName = FormEnum;
                        frm33.Instance.ModuleId = ModuleId_1;
                        frm33.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_ROOMBOOKING:
                        frmReport frm34 = new frmReport();
                        frm34.Instance.ReportName = FormEnum;
                        frm34.Instance.ModuleId = ModuleId_1;
                        frm34.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_RECIPAYDTL:
                        frmReport frm35 = new frmReport();
                        frm35.Instance.ReportName = FormEnum;
                        frm35.Instance.ModuleId = ModuleId_1;
                        frm35.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_LUXURYTAXDETAILSREG:
                        frmReport frm36 = new frmReport();
                        frm36.Instance.ReportName = FormEnum;
                        frm36.Instance.ModuleId = ModuleId_1;
                        frm36.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_COLLECTIONREPORT:
                        frmReport frm37 = new frmReport();
                        frm37.Instance.ReportName = FormEnum;
                        frm37.Instance.ModuleId = ModuleId_1;
                        frm37.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_CHECKOUTBILLSUMMARYRPT:
                        frmReport frm38 = new frmReport();
                        frm38.Instance.ReportName = FormEnum;
                        frm38.Instance.ModuleId = ModuleId_1;
                        frm38.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_CASHIERREPORT:
                        frmReport frm39 = new frmReport();
                        frm39.Instance.ReportName = FormEnum;
                        frm39.Instance.ModuleId = ModuleId_1;
                        frm39.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_COUPONBILLDEPOSITSUMMARYRPT:
                        frmReport frm40 = new frmReport();
                        frm40.Instance.ReportName = FormEnum;
                        frm40.Instance.ModuleId = ModuleId_1;
                        frm40.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_CHECKOUTBILLREGDTL:
                        frmReport frm41 = new frmReport();
                        frm41.Instance.ReportName = FormEnum;
                        frm41.Instance.ModuleId = ModuleId_1;
                        frm41.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_CHECKOUTBILLWITHGSTINFO:
                        frmReport frm42 = new frmReport();
                        frm42.Instance.ReportName = FormEnum;
                        frm42.Instance.ModuleId = ModuleId_1;
                        frm42.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_EXTRABILLDETAILREGISTER:
                        frmReport frm43 = new frmReport();
                        frm43.Instance.ReportName = FormEnum;
                        frm43.Instance.ModuleId = ModuleId_1;
                        frm43.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_MONTHLYINFORMATION:
                        frmReport frm44 = new frmReport();
                        frm44.Instance.ReportName = FormEnum;
                        frm44.Instance.ModuleId = ModuleId_1;
                        frm44.Instance.ShowDialog();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in OpenForm())", clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void Open_NoParameter_Reportform(string FormName_1)
        {

            DateTime fdate1 = new DateTime(1999, 01, 01);
            DateTime tdate1 = new DateTime(3000, 01, 01);

            try
            {
                this.GetConnectionDetails();
                this.GetSettingDetails();

                clsPublicVariables.enumSHMSReports FormEnum = (clsPublicVariables.enumSHMSReports)Enum.Parse(typeof(clsPublicVariables.enumSHMSReports), FormName_1, true);


                switch (FormEnum)
                {
                    case enumSHMSReports.SHMS_OCCUPANCYREGISTER:
                        frmReport frmrpt1 = new frmReport();
                        frmrpt1.Instance.ReportName = FormEnum;
                        frmrpt1.Instance.ModuleId = "";
                        frmrpt1.Instance.Fromdate = Convert.ToDateTime(fdate1);
                        frmrpt1.Instance.Todate = Convert.ToDateTime(tdate1);
                        frmrpt1.Instance.ShowDialog();
                        break;

                    default:
                        break;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in Open_NoParameter_Reportform())", clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public void PrintSHMSReport_From_To_Date_Para1_Parameter(string FormName_1, DateTime Fromdate1, DateTime Todate1, Int64 Para1)
        {
            try
            {
                this.GetConnectionDetails();
                this.GetSettingDetails();

                this.Fromdate = Fromdate1;
                this.Todate = Todate1;
                this.Para1 = Para1;

                clsPublicVariables.enumSHMSReports FormEnum = (clsPublicVariables.enumSHMSReports)Enum.Parse(typeof(clsPublicVariables.enumSHMSReports), FormName_1, true);

                switch (FormEnum)
                {
                    case enumSHMSReports.SHMS_CHARGEWISEEXTRASERVICE:
                        frmReport frmrpt1 = new frmReport();
                        frmrpt1.Instance.ReportName = FormEnum;
                        frmrpt1.Instance.ModuleId = "";
                        frmrpt1.Instance.Fromdate = Fromdate;
                        frmrpt1.Instance.Todate = Todate;
                        frmrpt1.Instance.Para1 = Para1;
                        frmrpt1.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_ROOMWISEEXTRASERVICE:
                        frmReport frmrpt2 = new frmReport();
                        frmrpt2.Instance.ReportName = FormEnum;
                        frmrpt2.Instance.ModuleId = "";
                        frmrpt2.Instance.Fromdate = Fromdate;
                        frmrpt2.Instance.Todate = Todate;
                        frmrpt2.Instance.Para1 = Para1;
                        frmrpt2.Instance.ShowDialog();
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in PrintSHMSReport_From_To_Date_Para1_Parameter())", clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void OpenFrom_To_Date_Parameter_Reportform(string FormName_1, DateTime Fromdate1, DateTime Todate1)
        {
            frmImage frmimg = new frmImage();
            try
            {
                this.GetConnectionDetails();
                this.GetSettingDetails();

                this.Fromdate = Fromdate1;
                this.Todate = Todate1;


                //clsPublicVariables.enumRMSForms FormEnum = (clsPublicVariables.enumRMSForms)Enum.Parse(typeof(clsPublicVariables.enumRMSForms), FormName_1, true);
                clsPublicVariables.enumSHMSReports FormEnum = (clsPublicVariables.enumSHMSReports)Enum.Parse(typeof(clsPublicVariables.enumSHMSReports), FormName_1, true);

                switch (FormEnum)
                {
                    case enumSHMSReports.SHMS_CHECKINREGISTER:
                        frmReport frmrpt1 = new frmReport();
                        frmrpt1.Instance.Icon = frmimg.Icon;
                        frmrpt1.Instance.ReportName = FormEnum;
                        frmrpt1.Instance.ModuleId = "";
                        frmrpt1.Instance.Fromdate = Fromdate;
                        frmrpt1.Instance.Todate = Todate;
                        frmrpt1.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_CHECKINOUTREGISTER:
                        frmReport frmrpt2 = new frmReport();
                        frmrpt2.Instance.Icon = frmimg.Icon;
                        frmrpt2.Instance.ReportName = FormEnum;
                        frmrpt2.Instance.ModuleId = "";
                        frmrpt2.Instance.Fromdate = Fromdate;
                        frmrpt2.Instance.Todate = Todate;
                        frmrpt2.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_EXTRABILLREGISTER:
                        frmReport frmrpt3 = new frmReport();
                        frmrpt3.Instance.Icon = frmimg.Icon;
                        frmrpt3.Instance.ReportName = FormEnum;
                        frmrpt3.Instance.ModuleId = "";
                        frmrpt3.Instance.Fromdate = Fromdate;
                        frmrpt3.Instance.Todate = Todate;
                        frmrpt3.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_CASHRECEIPTREGISTER:
                        frmReport frmrpt4 = new frmReport();
                        frmrpt4.Instance.Icon = frmimg.Icon;
                        frmrpt4.Instance.ReportName = FormEnum;
                        frmrpt4.Instance.ModuleId = "";
                        frmrpt4.Instance.Fromdate = Fromdate;
                        frmrpt4.Instance.Todate = Todate;
                        frmrpt4.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_CHECKOUTREGISTER:
                        frmReport frmrpt5 = new frmReport();
                        frmrpt5.Instance.Icon = frmimg.Icon;
                        frmrpt5.Instance.ReportName = FormEnum;
                        frmrpt5.Instance.ModuleId = "";
                        frmrpt5.Instance.Fromdate = Fromdate;
                        frmrpt5.Instance.Todate = Todate;
                        frmrpt5.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_SALESREGISTER:
                        frmReport frmrpt6 = new frmReport();
                        frmrpt6.Instance.Icon = frmimg.Icon;
                        frmrpt6.Instance.ReportName = FormEnum;
                        frmrpt6.Instance.ModuleId = "";
                        frmrpt6.Instance.Fromdate = Fromdate;
                        frmrpt6.Instance.Todate = Todate;
                        frmrpt6.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_EXTRABILLREGCHARGEWISE:
                        frmReport frmrpt7 = new frmReport();
                        frmrpt7.Instance.Icon = frmimg.Icon;
                        frmrpt7.Instance.ReportName = FormEnum;
                        frmrpt7.Instance.ModuleId = "";
                        frmrpt7.Instance.Fromdate = Fromdate;
                        frmrpt7.Instance.Todate = Todate;
                        frmrpt7.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_ROOMTARIFFWISEREG:
                        frmReport frmrpt8 = new frmReport();
                        frmrpt8.Instance.Icon = frmimg.Icon;
                        frmrpt8.Instance.ReportName = FormEnum;
                        frmrpt8.Instance.ModuleId = "";
                        frmrpt8.Instance.Fromdate = Fromdate;
                        frmrpt8.Instance.Todate = Todate;
                        frmrpt8.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_DATEWISELUXURYTAXREG:
                        frmReport frmrpt9 = new frmReport();
                        frmrpt9.Instance.Icon = frmimg.Icon;
                        frmrpt9.Instance.ReportName = FormEnum;
                        frmrpt9.Instance.ModuleId = "";
                        frmrpt9.Instance.Fromdate = Fromdate;
                        frmrpt9.Instance.Todate = Todate;
                        frmrpt9.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_DATEWISEVATTAXREG:
                        frmReport frmrpt10 = new frmReport();
                        frmrpt10.Instance.Icon = frmimg.Icon;
                        frmrpt10.Instance.ReportName = FormEnum;
                        frmrpt10.Instance.ModuleId = "";
                        frmrpt10.Instance.Fromdate = Fromdate;
                        frmrpt10.Instance.Todate = Todate;
                        frmrpt10.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_DATEWISEEXTTAX1REG:
                        frmReport frmrpt11 = new frmReport();
                        frmrpt11.Instance.Icon = frmimg.Icon;
                        frmrpt11.Instance.ReportName = FormEnum;
                        frmrpt11.Instance.ModuleId = "";
                        frmrpt11.Instance.Fromdate = Fromdate;
                        frmrpt11.Instance.Todate = Todate;
                        frmrpt11.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_CHECKOUTBILLREG:
                        frmReport frmrpt12 = new frmReport();
                        frmrpt12.Instance.Icon = frmimg.Icon;
                        frmrpt12.Instance.ReportName = FormEnum;
                        frmrpt12.Instance.ModuleId = "";
                        frmrpt12.Instance.Fromdate = Fromdate;
                        frmrpt12.Instance.Todate = Todate;
                        frmrpt12.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_ENTRYTICKETDETAILCOLLECTIONRPT:
                        frmReport frmrpt13 = new frmReport();
                        frmrpt13.Instance.Icon = frmimg.Icon;
                        frmrpt13.Instance.ReportName = FormEnum;
                        frmrpt13.Instance.ModuleId = "";
                        frmrpt13.Instance.Fromdate = Fromdate;
                        frmrpt13.Instance.Todate = Todate;
                        frmrpt13.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_ENTRYTICKETSUMMARYCOLLECTIONRPT:
                        frmReport frmrpt14 = new frmReport();
                        frmrpt14.Instance.Icon = frmimg.Icon;
                        frmrpt14.Instance.ReportName = FormEnum;
                        frmrpt14.Instance.ModuleId = "";
                        frmrpt14.Instance.Fromdate = Fromdate;
                        frmrpt14.Instance.Todate = Todate;
                        frmrpt14.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_COUPONBILLDETAILCOLLECTIONRPT:
                        frmReport frmrpt15 = new frmReport();
                        frmrpt15.Instance.Icon = frmimg.Icon;
                        frmrpt15.Instance.ReportName = FormEnum;
                        frmrpt15.Instance.ModuleId = "";
                        frmrpt15.Instance.Fromdate = Fromdate;
                        frmrpt15.Instance.Todate = Todate;
                        frmrpt15.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_COUPONBILLSUMMARYCOLLECTIONRPT:
                        frmReport frmrpt16 = new frmReport();
                        frmrpt16.Instance.Icon = frmimg.Icon;
                        frmrpt16.Instance.ReportName = FormEnum;
                        frmrpt16.Instance.ModuleId = "";
                        frmrpt16.Instance.Fromdate = Fromdate;
                        frmrpt16.Instance.Todate = Todate;
                        frmrpt16.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_COUPONREFUNDDETAILCOLLECTIONRPT:
                        frmReport frmrpt17 = new frmReport();
                        frmrpt17.Instance.Icon = frmimg.Icon;
                        frmrpt17.Instance.ReportName = FormEnum;
                        frmrpt17.Instance.ModuleId = "";
                        frmrpt17.Instance.Fromdate = Fromdate;
                        frmrpt17.Instance.Todate = Todate;
                        frmrpt17.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_COUPONBILLITEMWISEDETAILCOLLECTIONRPT:
                        frmReport frmrpt17a = new frmReport();
                        frmrpt17a.Instance.Icon = frmimg.Icon;
                        frmrpt17a.Instance.ReportName = FormEnum;
                        frmrpt17a.Instance.ModuleId = "";
                        frmrpt17a.Instance.Fromdate = Fromdate;
                        frmrpt17a.Instance.Todate = Todate;
                        frmrpt17a.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_COUPONREFUNDSUMMARYCOLLECTIONRPT:
                        frmReport frmrpt18 = new frmReport();
                        frmrpt18.Instance.Icon = frmimg.Icon;
                        frmrpt18.Instance.ReportName = FormEnum;
                        frmrpt18.Instance.ModuleId = "";
                        frmrpt18.Instance.Fromdate = Fromdate;
                        frmrpt18.Instance.Todate = Todate;
                        frmrpt18.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_COUPONBILLREGDATEWISE:
                        frmReport frmrpt19 = new frmReport();
                        frmrpt19.Instance.Icon = frmimg.Icon;
                        frmrpt19.Instance.ReportName = FormEnum;
                        frmrpt19.Instance.ModuleId = "";
                        frmrpt19.Instance.Fromdate = Fromdate;
                        frmrpt19.Instance.Todate = Todate;
                        frmrpt19.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_POLICEINQUIRYREPORT:
                        frmReport frmrpt20 = new frmReport();
                        frmrpt20.Instance.Icon = frmimg.Icon;
                        frmrpt20.Instance.ReportName = FormEnum;
                        frmrpt20.Instance.ModuleId = "";
                        frmrpt20.Instance.Fromdate = Fromdate;
                        frmrpt20.Instance.Todate = Todate;
                        frmrpt20.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_SEGMENTWISESUMMARY:
                        frmReport frmrpt21 = new frmReport();
                        frmrpt21.Instance.Icon = frmimg.Icon;
                        frmrpt21.Instance.ReportName = FormEnum;
                        frmrpt21.Instance.ModuleId = "";
                        frmrpt21.Instance.Fromdate = Fromdate;
                        frmrpt21.Instance.Todate = Todate;
                        frmrpt21.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_LUXURYTAXDETAILSREG:
                        frmReport frmrpt22 = new frmReport();
                        frmrpt22.Instance.Icon = frmimg.Icon;
                        frmrpt22.Instance.ReportName = FormEnum;
                        frmrpt22.Instance.ModuleId = "";
                        frmrpt22.Instance.Fromdate = Fromdate;
                        frmrpt22.Instance.Todate = Todate;
                        frmrpt22.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_COLLECTIONREPORT:
                        frmReport frmrpt23 = new frmReport();
                        frmrpt23.Instance.Icon = frmimg.Icon;
                        frmrpt23.Instance.ReportName = FormEnum;
                        frmrpt23.Instance.ModuleId = "";
                        frmrpt23.Instance.Fromdate = Fromdate;
                        frmrpt23.Instance.Todate = Todate;
                        frmrpt23.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_CHECKOUTBILLSUMMARYRPT:
                        frmReport frmrpt24 = new frmReport();
                        frmrpt24.Instance.Icon = frmimg.Icon;
                        frmrpt24.Instance.ReportName = FormEnum;
                        frmrpt24.Instance.ModuleId = "";
                        frmrpt24.Instance.Fromdate = Fromdate;
                        frmrpt24.Instance.Todate = Todate;
                        frmrpt24.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_CASHIERREPORT:
                        frmReport frmrpt25 = new frmReport();
                        frmrpt25.Instance.Icon = frmimg.Icon;
                        frmrpt25.Instance.ReportName = FormEnum;
                        frmrpt25.Instance.ModuleId = "";
                        frmrpt25.Instance.Fromdate = Fromdate;
                        frmrpt25.Instance.Todate = Todate;
                        frmrpt25.Instance.ShowDialog();
                        break;

                    case enumSHMSReports.SHMS_COUPONBILLDEPOSITSUMMARYRPT:
                        frmReport frmrpt26 = new frmReport();
                        frmrpt26.Instance.Icon = frmimg.Icon;
                        frmrpt26.Instance.ReportName = FormEnum;
                        frmrpt26.Instance.ModuleId = "";
                        frmrpt26.Instance.Fromdate = Fromdate;
                        frmrpt26.Instance.Todate = Todate;
                        frmrpt26.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_CHECKOUTBILLREGDTL:
                        frmReport frmrpt27 = new frmReport();
                        frmrpt27.Instance.Icon = frmimg.Icon;
                        frmrpt27.Instance.ReportName = FormEnum;
                        frmrpt27.Instance.ModuleId = "";
                        frmrpt27.Instance.Fromdate = Fromdate;
                        frmrpt27.Instance.Todate = Todate;
                        frmrpt27.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_CHECKOUTBILLWITHGSTINFO:
                        frmReport frmrpt28 = new frmReport();
                        frmrpt28.Instance.Icon = frmimg.Icon;
                        frmrpt28.Instance.ReportName = FormEnum;
                        frmrpt28.Instance.ModuleId = "";
                        frmrpt28.Instance.Fromdate = Fromdate;
                        frmrpt28.Instance.Todate = Todate;
                        frmrpt28.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_EXTRABILLDETAILREGISTER:
                        frmReport frmrpt29 = new frmReport();
                        frmrpt29.Instance.Icon = frmimg.Icon;
                        frmrpt29.Instance.ReportName = FormEnum;
                        frmrpt29.Instance.ModuleId = "";
                        frmrpt29.Instance.Fromdate = Fromdate;
                        frmrpt29.Instance.Todate = Todate;
                        frmrpt29.Instance.ShowDialog();
                        break;
                    case enumSHMSReports.SHMS_MONTHLYINFORMATION:
                        frmReport frmrpt30 = new frmReport();
                        frmrpt30.Instance.Icon = frmimg.Icon;
                        frmrpt30.Instance.ReportName = FormEnum;
                        frmrpt30.Instance.ModuleId = "";
                        frmrpt30.Instance.Fromdate = Fromdate;
                        frmrpt30.Instance.Todate = Todate;
                        frmrpt30.Instance.ShowDialog();
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in OpenFrom_To_Date_Parameter_Reportform())", clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public void DirectPrintRMSReport(string FormName_1, string ModuleId_1)
        {
            ReportDocument reportdocument = new ReportDocument();
            frmImage frmimg = new frmImage();
            frmReport frmRpt = new frmReport();
            string ReportFilePath;
            string formulastr = "";

            try
            {
                ReportFilePath = "";
                // clsPublicVariables.enumRMSForms FormEnum = (clsPublicVariables.enumRMSForms)Enum.Parse(typeof(clsPublicVariables.enumRMSForms), FormName_1, true);
                clsPublicVariables.enumSHMSReports FormEnum = (clsPublicVariables.enumSHMSReports)Enum.Parse(typeof(clsPublicVariables.enumSHMSReports), FormName_1, true);

                this.GetConnectionDetails();
                this.GetSettingDetails();

                switch (FormEnum)
                {
                    //case clsPublicVariables.enumRMSForms.RMS_KOT:
                    //    ReportFilePath = clsPublicVariables.ReportPath + "\\Kot.rpt";
                    //    break;

                    //case clsPublicVariables.enumRMSForms.RMS_KOTB:
                    //    ReportFilePath = clsPublicVariables.ReportPath + "\\Kotb.rpt";
                    //    break;

                    //case clsPublicVariables.enumRMSForms.RMS_BILL:
                    //    ReportFilePath = clsPublicVariables.ReportPath + "\\Bill.rpt";
                    //    break;

                    //case clsPublicVariables.enumRMSForms.RMS_BILLB:
                    //    ReportFilePath = clsPublicVariables.ReportPath + "\\Billb.rpt";
                    //    break;

                    //case clsPublicVariables.enumRMSForms.POS_BILL:
                    //    ReportFilePath = clsPublicVariables.ReportPath + "\\PosBill.rpt";
                    //    break;


                    default:
                        break;
                }

                reportdocument.PrintOptions.PrinterName = RptPrintername;

                reportdocument.Load(ReportFilePath, OpenReportMethod.OpenReportByDefault);

                reportdocument.SetDatabaseLogon(clsPublicVariables.UserName1, clsPublicVariables.Password1, clsPublicVariables.ServerName1, clsPublicVariables.DatabaseName1);

                this.AssignParameterToReport(reportdocument);
                //this.AssignMarginToReport(reportdocument);

                formulastr = "";
                // Assign Formula String 
                switch (FormEnum)
                {
                    //case clsPublicVariables.enumRMSForms.RMS_BILL:
                    //    formulastr = " {BILL.RId} = [" + ModuleId_1 + "] and {BILLDTL.DELFLG}=False";
                    //    //{BILLDTL.DELFLG}=False
                    //    break;

                    //case clsPublicVariables.enumRMSForms.RMS_BILLB:
                    //    formulastr = " {BILLb.RId} = [" + ModuleId_1 + "] and {BILLBDTL.DELFLG}=False";
                    //    break;

                    //case clsPublicVariables.enumRMSForms.RMS_KOT:
                    //    formulastr = " {KOT.RID} = [" + ModuleId_1 + "] and {KOTDTL.DELFLG}=False";
                    //    break;

                    //case clsPublicVariables.enumRMSForms.RMS_KOTB:
                    //    formulastr = " {KOTb.RID} = [" + ModuleId_1 + "]and {KOTBDTL.DELFLG}=False";
                    //    break;

                    //case clsPublicVariables.enumRMSForms.POS_BILL:
                    //    formulastr = " {BILL.RId} = [" + ModuleId_1 + "] and {BILLDTL.DELFLG}=False";
                    //    break;


                    default:
                        break;
                }

                Database crDatabase;
                Tables crTables;
                TableLogOnInfo crTableLogOnInfo;
                ConnectionInfo crConnectionInfo;

                crConnectionInfo = new ConnectionInfo();
                crConnectionInfo.ServerName = clsPublicVariables.ServerName1;
                crConnectionInfo.DatabaseName = clsPublicVariables.DatabaseName1;
                crConnectionInfo.UserID = clsPublicVariables.UserName1;
                crConnectionInfo.Password = clsPublicVariables.Password1;

                crDatabase = reportdocument.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                }

                //crystalReportViewer1.ReportSource = reportdocument;

                reportdocument.RecordSelectionFormula = formulastr;
                reportdocument.PrintToPrinter(1, false, 0, 0);

                Cursor.Current = Cursors.Default;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured in DirectPrintRMS() " + ex.Message.ToString());
            }

        }

        public void LoadReport(bool IsDefaultRpt, enumSHMSReports RptName_1, string ModuleId_1, string RptFilePath_1, bool directprint_1, DateTime fromdate, DateTime todate, Int64 para1)
        {
            frmImage frmimg = new frmImage();
            frmReport frmRpt = new frmReport();
            ReportDocument reportdocument = new ReportDocument();
            clsMsSqlDbFunction clsmssql = new clsMsSqlDbFunction();

            string ReportFilePath;
            string formulastr = "";

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ReportFilePath = "";

                this.Fromdate = fromdate;
                this.Todate = todate;
                this.Para1 = para1;

                this.GetConnectionDetails();
                this.GetSettingDetails();

                switch (RptName_1)
                {
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKINREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkinregister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKINOUTREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkinoutregister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Perfomainvoice.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CASHRECEIPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\cashreceipt.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRABILLREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\extrabillregister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CASHRECEIPTREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Cashreceiptregister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRASERBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Extrabill.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutregister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_OCCUPANCYREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\OccupancyRegister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_SALESREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SalesRegister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_REGISTRATIONCARD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RegistrationCard.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRABILLREGCHARGEWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ExtraBillRegChargeWise.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHARGEWISEEXTRASERVICE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ChargeWiseExtraServiceReport.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMWISEEXTRASERVICE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RoomWiseExtraServiceReport.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMTARIFFWISEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RoomTariffWiseReg.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_BREAKFASTCOUPONS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BreakfastCoupons.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_LUNCHCOUPONS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\LunchCoupons.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_DINNERCOUPONS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DinnerCoupons.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISC:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_WoDisc.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEWODISC:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Perfomainvoice_WoDisc.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLUSD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbillusd.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISCUSD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_WoDisc_USD.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEUSD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PerfomainvoiceUSD.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEWODISCUSD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Perfomainvoice_WoDisc_USD.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_POLICEINQUIRYSAMPLE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PoliceInquirysample.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_DATEWISELUXURYTAXREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateLuxuryTaxRegister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_DATEWISEVATTAXREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseVATRegister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_DATEWISEEXTTAX1REG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseExttax1Register.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutBillRegister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHSM_TARIFF_BILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_Tariff.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_WOTARIFF_BILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_Tariff_Wo.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_WOTARIFF_DIFFBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_Tariff_Wo_Diff.rpt";
                        RptPrintername = RptBillPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKET:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicket.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKETDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKETSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketSummaryCollection.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillSummary.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLITEMWISEDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillItemWiseDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUNDDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefundBillDetailed.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUNDSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefundBillSummary.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBill.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUND:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefundBill.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLREGDATEWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillRegisterDateWise.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_POLICEINQUIRYREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PoliceInquiryReport.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_SEGMENTWISESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SegmentWiseSummary.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMBOOKING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RoomBookingLetter.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_RECIPAYDTL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutreceipt.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_LUXURYTAXDETAILSREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\LuxuaryTaxDetailsReg.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COLLECTIONREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CollectionReport.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLSUMMARYRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutBillDetailsReg.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CASHIERREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CashierReport.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONDEPOSITBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponDepositBill.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLDEPOSITSUMMARYRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillDepositSummary.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLREGDTL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutBillDtlReg.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLWITHGSTINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutBillwithGstInfo.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRABILLDETAILREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ExtraServiceBillDetail.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_MONTHLYINFORMATION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\MonthlyInformation.rpt";
                        break;

                    default:
                        break;
                }
                //}

                reportdocument.PrintOptions.PrinterName = frmRpt.Instance.cmbPrinter.Text;
                reportdocument.Load(ReportFilePath, OpenReportMethod.OpenReportByDefault);

                reportdocument.SetDatabaseLogon(clsPublicVariables.UserName1, clsPublicVariables.Password1, clsPublicVariables.ServerName1, clsPublicVariables.DatabaseName1);

                //Assign Parameter Value to Report
                this.AssignParameterToReport(reportdocument);

                //Set Margin To Reports
                //this.AssignMarginToReport(reportdocument);

                // Assign Formula String 
                switch (RptName_1)
                {
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILL:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICE:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICEDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CASHRECEIPT:
                        formulastr = " {CASHRECEIPT.RID} = [" + ModuleId_1 + "]";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRASERBILL:
                        formulastr = " {EXTRABILL.RID} = [" + ModuleId_1 + "] and {EXTRABILLDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_REGISTRATIONCARD:
                        formulastr = " {ROOMCHECKIN.RID} = [" + ModuleId_1 + "] and {ROOMCHECKIN.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_BREAKFASTCOUPONS:
                        formulastr = " {ROOMCHECKIN.RID} = [" + ModuleId_1 + "] and {ROOMCHECKIN.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_LUNCHCOUPONS:
                        formulastr = " {ROOMCHECKIN.RID} = [" + ModuleId_1 + "] and {ROOMCHECKIN.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_DINNERCOUPONS:
                        formulastr = " {ROOMCHECKIN.RID} = [" + ModuleId_1 + "] and {ROOMCHECKIN.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISC:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEWODISC:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICEDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLUSD:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISCUSD:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEUSD:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICEDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEWODISCUSD:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICEDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHSM_TARIFF_BILL:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False and {ROOMCHECKOUTDTL.SERRID}=0";
                        //{BILLDTL.DELFLG}=False                        
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_WOTARIFF_BILL:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False and {ROOMCHECKOUTDTL.SERRID}>0";
                        //{BILLDTL.DELFLG}=False                        
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_WOTARIFF_DIFFBILL:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False and {ROOMCHECKOUTDTL.SERRID}>0";
                        //{BILLDTL.DELFLG}=False                        
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKET:
                        formulastr = " {ENTRYTICKET.RID} = [" + ModuleId_1 + "] and {ENTRYTICKET.DELFLG}=False ";
                        //PaperOrientation.Landscape;
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILL:
                        formulastr = " {COUPONBILL.RID} = [" + ModuleId_1 + "]";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUND:
                        formulastr = " {REFUNDBILL.RID} = [" + ModuleId_1 + "]";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMBOOKING:
                        formulastr = " {ROOMBOOKING.RID} = [" + ModuleId_1 + "] and {ROOMBOOKING.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_RECIPAYDTL:
                        formulastr = " {RECIPAYDTL.RID} = [" + ModuleId_1 + "] and {RECIPAYDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONDEPOSITBILL:
                        formulastr = " {COUPONBILL.RID} = [" + ModuleId_1 + "]";
                        //{BILLDTL.DELFLG}=False
                        break;
                    default:
                        break;
                }

                Database crDatabase;
                Tables crTables;
                TableLogOnInfo crTableLogOnInfo;
                ConnectionInfo crConnectionInfo;

                crConnectionInfo = new ConnectionInfo();
                crConnectionInfo.ServerName = clsPublicVariables.ServerName1;
                crConnectionInfo.DatabaseName = clsPublicVariables.DatabaseName1;
                crConnectionInfo.UserID = clsPublicVariables.UserName1;
                crConnectionInfo.Password = clsPublicVariables.Password1;

                crDatabase = reportdocument.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                }

                //crystalReportViewer1.ReportSource = reportdocument;

                if (directprint_1 == true)
                {
                    reportdocument.RecordSelectionFormula = formulastr;
                    reportdocument.PrintToPrinter(1, false, 0, 0);
                }
                else
                {
                    //Display Crystal Report Viewer
                    frmRptDisplay frm2 = new frmRptDisplay();
                    frm2.Instance.Icon = frmimg.Icon;
                    frm2.Instance.ReportDocument = reportdocument;
                    frm2.Instance.FormulaStr = formulastr;
                    frm2.Instance.ShowDialog();
                }


                // UPDATE BILL PRINT COUNTER
                //if (RptName_1 == clsPublicVariables.enumRMSForms.RMS_BILL)
                //{
                //    string str1;
                //    DataTable dtbill = new DataTable();
                //    Int64 cntprint1;

                //    cntprint1 = 0;
                //    str1 = "select CNTPRINT from bill where rid = " + ModuleId_1;
                //    dtbill = clsmssql.FillDataTable(str1, "bill");

                //    if (dtbill.Rows.Count > 0)
                //    {
                //        foreach (DataRow row in dtbill.Rows)
                //        {
                //            Int64.TryParse(row["CNTPRINT"] + "", out cntprint1);
                //            cntprint1 = cntprint1 + 1;
                //        }

                //        str1 = "UPDATE BILL SET CNTPRINT = " + cntprint1 + " WHERE RID = " + ModuleId_1;
                //        clsmssql.ExecuteMsSqlCommand(str1);
                //    }
                //}

                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured in LoadReport " + ex.Message.ToString(), clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool ExportReportToPdf(bool IsDefaultRpt, string RptName_1, string ModuleId_1, string RptFilePath_1, DateTime fromdate, DateTime todate)
        {
            frmImage frmimg = new frmImage();
            frmReport frmRpt = new frmReport();
            ReportDocument reportdocument = new ReportDocument();

            this.Fromdate = fromdate;
            this.Todate = todate;

            clsMsSqlDbFunction objclsdb = new clsMsSqlDbFunction();
            string filename;
            string ReportFilePath;
            string formulastr = "";

            string filenamestr_1 = "";
            //string namestr_1 = "";
            //string sqlstr;
            //DataTable Dt1;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                this.GetConnectionDetails();
                this.GetSettingDetails();

                //clsPublicVariables.enumRMSForms RptEnum = (clsPublicVariables.enumRMSForms)Enum.Parse(typeof(clsPublicVariables.enumRMSForms), RptName_1, true);
                clsPublicVariables.enumSHMSReports RptEnum = (clsPublicVariables.enumSHMSReports)Enum.Parse(typeof(clsPublicVariables.enumSHMSReports), RptName_1, true);

                filename = "";
                ReportFilePath = "";
                filenamestr_1 = "";

                this.Fromdate = fromdate;
                this.Todate = todate;

                //if (RptFilePath_1.Trim() != "")
                //{
                //    ReportFilePath = RptFilePath_1;
                //}
                //else
                //{
                switch (RptEnum)
                {
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKINOUTREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckinoutRegister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKINREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckinRegister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Perfomainvoice.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CASHRECEIPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\cashreceipt.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRABILLREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\extrabillregister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CASHRECEIPTREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Cashreceiptregister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRASERBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Extrabill.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutRegister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_OCCUPANCYREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\OccupancyRegister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_SALESREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SalesRegister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_REGISTRATIONCARD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RegistrationCard.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRABILLREGCHARGEWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ExtraBillRegChargeWise.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHARGEWISEEXTRASERVICE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ChargeWiseExtraServiceReport.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMWISEEXTRASERVICE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RoomWiseExtraServiceReport.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMTARIFFWISEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RoomTariffWiseReg.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_BREAKFASTCOUPONS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BreakfastCoupons.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_LUNCHCOUPONS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\LunchCoupons.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_DINNERCOUPONS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DinnerCoupons.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISC:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_WoDisc.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEWODISC:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Perfomainvoice_WoDisc.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLUSD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbillusd.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISCUSD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_WoDisc_USD.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEUSD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PerfomainvoiceUSD.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEWODISCUSD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Perfomainvoice_WoDisc_USD.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_POLICEINQUIRYSAMPLE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PoliceInquirysample.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_DATEWISELUXURYTAXREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateLuxuryTaxRegister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_DATEWISEVATTAXREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseVATRegister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_DATEWISEEXTTAX1REG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseExttax1Register.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutBillRegister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHSM_TARIFF_BILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_Tariff.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_WOTARIFF_BILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_Tariff_Wo.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_WOTARIFF_DIFFBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_Tariff_Wo_Diff.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKET:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicket.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKETDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKETSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketSummaryCollection.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillSummary.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUNDDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefundBillDetailed.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLITEMWISEDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillItemWiseDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUNDSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefundBillSummary.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBill.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUND:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefundBill.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLREGDATEWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillRegisterDateWise.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_POLICEINQUIRYREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PoliceInquiryReport.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_SEGMENTWISESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SegmentWiseSummary.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMBOOKING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RoomBookingLetter.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_RECIPAYDTL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutreceipt.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_LUXURYTAXDETAILSREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\LuxuaryTaxDetailsReg.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COLLECTIONREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CollectionReport.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLSUMMARYRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutBillDetailsReg.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CASHIERREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CashierReport.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLDEPOSITSUMMARYRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillDepositSummary.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLREGDTL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutBillDtlReg.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLWITHGSTINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutBillwithGstInfo.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRABILLDETAILREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ExtraServiceBillDetail.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_MONTHLYINFORMATION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\MonthlyInformation.rpt";
                        break;
                    default:
                        break;
                }
                //}

                reportdocument.PrintOptions.PrinterName = frmRpt.Instance.cmbPrinter.Text;
                reportdocument.Load(ReportFilePath, OpenReportMethod.OpenReportByDefault);

                //Assign Parameter Value to Report
                this.AssignParameterToReport(reportdocument);

                //Set Margin To Reports
                //this.AssignMarginToReport(reportdocument);

                Database crDatabase;
                Tables crTables;
                TableLogOnInfo crTableLogOnInfo;
                ConnectionInfo crConnectionInfo;

                crConnectionInfo = new ConnectionInfo();
                crConnectionInfo.ServerName = clsPublicVariables.ServerName1;
                crConnectionInfo.DatabaseName = clsPublicVariables.DatabaseName1;
                crConnectionInfo.UserID = clsPublicVariables.UserName1;
                crConnectionInfo.Password = clsPublicVariables.Password1;

                crDatabase = reportdocument.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                }

                //crystalReportViewer1.ReportSource = reportdocument;

                //reportdocument.SetDatabaseLogon(UserName1, Password1, ServerName1, DatabaseName1);

                ////Assign Parameter Value to Report
                //this.AssignParameterToReport(reportdocument);

                ////Set Margin To Reports
                //this.AssignMarginToReport(reportdocument);


                // Assign Formula String 
                switch (RptEnum)
                {

                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILL:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False";
                        filenamestr_1 = "Bill No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKINOUTREGISTER:
                        filenamestr_1 = "CheckINOUTRegister ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKINREGISTER:
                        filenamestr_1 = "CheckINRegister ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICE:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICEDTL.DELFLG}=False";
                        filenamestr_1 = "Perfoma Invoice No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CASHRECEIPT:
                        formulastr = " {CASHRECEIPT.RID} = [" + ModuleId_1 + "]";
                        filenamestr_1 = "Cash Receipt No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRABILLREGISTER:
                        filenamestr_1 = "Extra Bill Register ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CASHRECEIPTREGISTER:
                        filenamestr_1 = "Cash Receipt Register ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRASERBILL:
                        formulastr = " {EXTRABILL.RID} = [" + ModuleId_1 + "] and {EXTRABILLDTL.DELFLG}=False";
                        filenamestr_1 = "Extra service bill " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTREGISTER:
                        filenamestr_1 = "CheckOUTRegister ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_OCCUPANCYREGISTER:
                        filenamestr_1 = "OccupiedRegister ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_SALESREGISTER:
                        filenamestr_1 = "SalesRegister ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_REGISTRATIONCARD:
                        formulastr = " {ROOMCHECKIN.RID} = [" + ModuleId_1 + "] and {ROOMCHECKIN.DELFLG}=False";
                        filenamestr_1 = "Registration Card " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRABILLREGCHARGEWISE:
                        filenamestr_1 = "ExtraBillRegisterChargeWise ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHARGEWISEEXTRASERVICE:
                        filenamestr_1 = "ServiceWiseBillRegister";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMWISEEXTRASERVICE:
                        filenamestr_1 = "RoomWiseBillRegister";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMTARIFFWISEREG:
                        filenamestr_1 = "Room Tariff Register ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_BREAKFASTCOUPONS:
                        formulastr = " {ROOMCHECKIN.RID} = [" + ModuleId_1 + "] and {ROOMCHECKIN.DELFLG}=False";
                        filenamestr_1 = "Breakfast Coupons " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_LUNCHCOUPONS:
                        formulastr = " {ROOMCHECKIN.RID} = [" + ModuleId_1 + "] and {ROOMCHECKIN.DELFLG}=False";
                        filenamestr_1 = "Lunch Coupons " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_DINNERCOUPONS:
                        formulastr = " {ROOMCHECKIN.RID} = [" + ModuleId_1 + "] and {ROOMCHECKIN.DELFLG}=False";
                        filenamestr_1 = "Dinner Coupons " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISC:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False";
                        filenamestr_1 = "Bill No Wod" + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEWODISC:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICEDTL.DELFLG}=False";
                        filenamestr_1 = "Perfoma Bill No Wod" + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLUSD:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False";
                        filenamestr_1 = "Bill No USD" + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISCUSD:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False";
                        filenamestr_1 = "Bill No WO DISC USD" + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEUSD:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICEDTL.DELFLG}=False";
                        filenamestr_1 = "Perfoma Bill No USD" + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEWODISCUSD:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICE.DELFLG}=False";
                        filenamestr_1 = "Per Bill No wo disc USD" + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_POLICEINQUIRYSAMPLE:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICE.DELFLG}=False";
                        filenamestr_1 = "Police Inquiry" + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_DATEWISELUXURYTAXREG:
                        filenamestr_1 = "DatewiseLuxurytaxReg";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_DATEWISEVATTAXREG:
                        filenamestr_1 = "DatewiseVAttaxReg";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_DATEWISEEXTTAX1REG:
                        filenamestr_1 = "Datewiseexttaxreg";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLREG:
                        filenamestr_1 = "Checkoutbillreg";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHSM_TARIFF_BILL:
                        filenamestr_1 = "TAriff Bill";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_WOTARIFF_BILL:
                        filenamestr_1 = "Wo TAriff Bill";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_WOTARIFF_DIFFBILL:
                        filenamestr_1 = "Wo TAriff diff Bill";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKET:
                        formulastr = " {ENTRYTICKET.RID} = [" + ModuleId_1 + "]";
                        filenamestr_1 = "Entry Ticket No" + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKETDETAILCOLLECTIONRPT:
                        filenamestr_1 = "Entry Ticket Details Collection Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKETSUMMARYCOLLECTIONRPT:
                        filenamestr_1 = "Entry Ticket Summary Collection Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLDETAILCOLLECTIONRPT:
                        filenamestr_1 = "Coupon Bill Details Collection Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLSUMMARYCOLLECTIONRPT:
                        filenamestr_1 = "Coupon Bill Summary Collection Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUNDDETAILCOLLECTIONRPT:
                        filenamestr_1 = "Coupon Refund Details Collection Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLITEMWISEDETAILCOLLECTIONRPT:
                        filenamestr_1 = "Item Wise Collection Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUNDSUMMARYCOLLECTIONRPT:
                        filenamestr_1 = "Coupon Refund Summary Collection Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILL:
                        formulastr = " {COUPONBILL.RID} = [" + ModuleId_1 + "]";
                        filenamestr_1 = "Coupon Bill No" + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUND:
                        formulastr = " {REFUNDBILL.RID} = [" + ModuleId_1 + "]";
                        filenamestr_1 = "Refund Bill No" + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLREGDATEWISE:
                        filenamestr_1 = "Coupon Bill Register Date Wise";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_POLICEINQUIRYREPORT:
                        filenamestr_1 = "Police Inquirt Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_SEGMENTWISESUMMARY:
                        filenamestr_1 = "Segment Wise Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMBOOKING:
                        filenamestr_1 = "Room Booking";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_RECIPAYDTL:
                        filenamestr_1 = "Recipt";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_LUXURYTAXDETAILSREG:
                        filenamestr_1 = "Luxury Tax Reg";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COLLECTIONREPORT:
                        filenamestr_1 = "Collection Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLSUMMARYRPT:
                        filenamestr_1 = "check Out Details Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CASHIERREPORT:
                        filenamestr_1 = "Cashier Report Details Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONDEPOSITBILL:
                        formulastr = " {COUPONBILL.RID} = [" + ModuleId_1 + "]";
                        filenamestr_1 = "Coupon Depost Bill No" + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLDEPOSITSUMMARYRPT:
                        filenamestr_1 = "Coupon Bill Deposit Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLREGDTL:
                        filenamestr_1 = "Check Out Bill Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLWITHGSTINFO:
                        filenamestr_1 = "Check Out Bill GST Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRABILLDETAILREGISTER:
                        filenamestr_1 = "Extra Service Bill Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_MONTHLYINFORMATION:
                        filenamestr_1 = "Monthly Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    default:
                        break;
                }

                // Get the report document

                reportdocument.RecordSelectionFormula = formulastr;
                reportdocument.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                reportdocument.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
                objDiskOpt.DiskFileName = filename;
                reportdocument.ExportOptions.DestinationOptions = objDiskOpt;
                reportdocument.Export();

                Cursor.Current = Cursors.Default;
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ReportEmail(bool IsDefaultRpt, enumRMSForms RptName_1, string ModuleId_1, string RptFilePath_1)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void AssignParameterToReport(ReportDocument RptDoc_1)
        {

            try
            {
                //string CurName_1 = "", CurSymbol_1 = "";
                long paramcount = 0;
                paramcount = RptDoc_1.ParameterFields.Count;

                if (paramcount > 0)
                {
                    try { RptDoc_1.SetParameterValue("Title1", RptTitle1); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Title2", RptTitle2); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Title3", RptTitle3); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Title4", RptTitle4); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Title5", RptTitle5); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Title6", RptTitle6); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Title7", RptTitle7); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Title8", RptTitle8); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("@p_fromdate", this.Fromdate.ToString("yyyy/MM/dd")); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("@p_todate", this.Todate.ToString("yyyy/MM/dd")); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Para1", this.Para1); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Footer1", RptFooter1); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Footer2", RptFooter2); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Footer3", RptFooter3); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Footer4", RptFooter4); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Footer5", RptFooter5); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Footer6", RptFooter6); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Footer7", RptFooter7); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Footer8", RptFooter8); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("@p_serrid", this.Para1); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("@p_roomrid", this.Para1); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("CbHeader1", CbHeader1); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("CbHeader2", CbHeader2); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("CbHeader3", CbHeader3); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("CbFooter1", CbFooter1); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("CbFooter2", CbFooter2); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("CbFooter3", CbFooter3); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("CrFooter1", CrFooter1); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("CrFooter2", CrFooter2); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("CrFooter3", CrFooter3); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("CrHeader1", CrHeader1); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("CrHeader2", CrHeader2); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("CrHeader3", CrHeader3); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("BillTitle", BillTitle); }
                    catch (Exception) { }


                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Error occured in AssignParameterToReport() " + ex.Message.ToString(), clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AssignMarginToReport(ReportDocument RptDoc_1)
        {
            int TopMarg;
            int BtmMarg;
            int LftMarg;

            PageMargins margins;
            frmReport frm1 = new frmReport();

            int.TryParse(frm1.Instance.txtTopMargin.Text + "", out TopMarg);
            int.TryParse(frm1.Instance.txtLeftMargin.Text + "", out LftMarg);
            int.TryParse(frm1.Instance.txtFooterMargin.Text + "", out BtmMarg);

            //TopMarg = Convert.ToInt32(Convert.ToDecimal(frm1.Instance.txtTopMargin.Text) * 1440);
            //LftMarg = Convert.ToInt32(Convert.ToDecimal(frm1.Instance.txtLeftMargin.Text) * 1440);
            //BtmMarg = Convert.ToInt32(Convert.ToDecimal(frm1.Instance.txtFooterMargin.Text) * 1440);

            TopMarg = Convert.ToInt32(TopMarg * 1440);
            LftMarg = Convert.ToInt32(LftMarg * 1440);
            BtmMarg = Convert.ToInt32(BtmMarg * 1440);

            margins = RptDoc_1.PrintOptions.PageMargins;
            margins.bottomMargin = 0;
            margins.leftMargin = LftMarg;
            margins.topMargin = TopMarg;
            RptDoc_1.PrintOptions.ApplyPageMargins(margins);
        }

        public void DirectPrintSHMSReport(string FormName_1, string ModuleId_1)
        {
            ReportDocument reportdocument = new ReportDocument();
            frmImage frmimg = new frmImage();
            frmReport frmRpt = new frmReport();
            string ReportFilePath;
            string formulastr = "";

            try
            {
                ReportFilePath = "";
                clsPublicVariables.enumSHMSReports FormEnum = (clsPublicVariables.enumSHMSReports)Enum.Parse(typeof(clsPublicVariables.enumSHMSReports), FormName_1, true);

                this.GetConnectionDetails();
                this.GetSettingDetails();

                switch (FormEnum)
                {
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Perfomainvoice.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_CASHRECEIPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Cashreceipt.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRASERBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Extrabill.rpt";
                        RptPrintername = RptExtraBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_REGISTRATIONCARD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RegistrationCard.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_BREAKFASTCOUPONS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BreakfastCoupons.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_LUNCHCOUPONS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\LunchCoupons.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_DINNERCOUPONS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DinnerCoupons.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISC:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_WoDisc.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEWODISC:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Perfomainvoice_WoDisc.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLUSD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbillusd.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISCUSD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_WoDisc_USD.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEUSD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PerfomainvoiceUSD.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEWODISCUSD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Perfomainvoice_WoDisc_USD.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_POLICEINQUIRYSAMPLE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PoliceInquirysample.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_FOODCOUPONSSAMPLE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\LunchDinnerCoupons.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_DATEWISELUXURYTAXREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateLuxuryTaxRegister.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_DATEWISEVATTAXREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseVATRegister.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_DATEWISEEXTTAX1REG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseExttax1Register.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutBillRegister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHSM_TARIFF_BILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_Tariff.rpt";
                        RptPrintername = RptBillPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_WOTARIFF_BILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_Tariff_Wo.rpt";
                        RptPrintername = RptBillPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_WOTARIFF_DIFFBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_Tariff_Wo_Diff.rpt";
                        RptPrintername = RptBillPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKET:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicket.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKETDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKETSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketSummaryCollection.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillSummary.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUNDDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefundBillDetailed.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLITEMWISEDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillItemWiseDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUNDSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefundBillSummary.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBill.rpt";
                        RptPrintername = RptCouponPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUND:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefundBill.rpt";
                        RptPrintername = RptCouponPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_BLANKGUESTREGISTRATIONCARD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BlankGuestRegistrationCard.rpt";
                        // RptPrintername = RptPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLREGDATEWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillRegisterDateWise.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_POLICEINQUIRYREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PoliceInquiryReport.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_SEGMENTWISESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SegmentWiseSummary.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMBOOKING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RoomBookingLetter.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_RECIPAYDTL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutreceipt.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_LUXURYTAXDETAILSREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\LuxuaryTaxDetailsReg.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COLLECTIONREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CollectionReport.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLSUMMARYRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutBillDetailsReg.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CASHIERREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CashierReport.rpt";
                        RptPrintername = RptBillPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONDEPOSITBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponDepositBill.rpt";
                        RptPrintername = RptCouponPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLDEPOSITSUMMARYRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillDepositSummary.rpt";
                        RptPrintername = RptCouponPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLREGDTL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutBillDtlReg.rpt";
                        RptPrintername = RptBillPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLWITHGSTINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutBillwithGstInfo.rpt";
                        RptPrintername = RptBillPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRABILLDETAILREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ExtraServiceBillDetail.rpt";
                        RptPrintername = RptBillPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_MONTHLYINFORMATION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\MonthlyInformation.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    default:
                        break;
                }


                if (RptPrintername != "")
                {
                    reportdocument.PrintOptions.PrinterName = RptPrintername;
                }

                reportdocument.Load(ReportFilePath, OpenReportMethod.OpenReportByDefault);

                reportdocument.SetDatabaseLogon(clsPublicVariables.UserName1, clsPublicVariables.Password1, clsPublicVariables.ServerName1, clsPublicVariables.DatabaseName1);

                this.AssignParameterToReport(reportdocument);
                //this.AssignMarginToReport(reportdocument);

                formulastr = "";
                // Assign Formula String 
                switch (FormEnum)
                {
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILL:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICE:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICEDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CASHRECEIPT:
                        formulastr = " {cashreceipt.RID} = [" + ModuleId_1 + "]";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRASERBILL:
                        formulastr = " {EXTRABILL.RID} = [" + ModuleId_1 + "] and {EXTRABILLDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_REGISTRATIONCARD:
                        formulastr = " {ROOMCHECKIN.RID} = [" + ModuleId_1 + "] and {ROOMCHECKIN.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_BREAKFASTCOUPONS:
                        formulastr = " {ROOMCHECKIN.RID} = [" + ModuleId_1 + "] and {ROOMCHECKIN.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_LUNCHCOUPONS:
                        formulastr = " {ROOMCHECKIN.RID} = [" + ModuleId_1 + "] and {ROOMCHECKIN.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_DINNERCOUPONS:
                        formulastr = " {ROOMCHECKIN.RID} = [" + ModuleId_1 + "] and {ROOMCHECKIN.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISC:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEWODISC:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICEDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLUSD:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISCUSD:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEUSD:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICEDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEWODISCUSD:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICEDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHSM_TARIFF_BILL:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False and {ROOMCHECKOUTDTL.SERRID}=0";
                        //{BILLDTL.DELFLG}=False                        
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_WOTARIFF_BILL:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False and {ROOMCHECKOUTDTL.SERRID}>0";
                        //{BILLDTL.DELFLG}=False                        
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_WOTARIFF_DIFFBILL:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False and {ROOMCHECKOUTDTL.SERRID}>0";
                        //{BILLDTL.DELFLG}=False                        
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKET:
                        formulastr = " {ENTRYTICKET.RID} = [" + ModuleId_1 + "] and {ENTRYTICKET.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUND:
                        formulastr = " {REFUNDBILL.RID} = [" + ModuleId_1 + "] and {REFUNDBILL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILL:
                        formulastr = " {COUPONBILL.RID} = [" + ModuleId_1 + "] and {COUPONBILL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMBOOKING:
                        formulastr = " {ROOMBOOKING.RID} = [" + ModuleId_1 + "] and {ROOMBOOKING.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_RECIPAYDTL:
                        formulastr = " {RECIPAYDTL.RID} = [" + ModuleId_1 + "] and {RECIPAYDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONDEPOSITBILL:
                        formulastr = " {COUPONBILL.RID} = [" + ModuleId_1 + "] and {COUPONBILL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    default:
                        break;
                }

                Database crDatabase;
                Tables crTables;
                TableLogOnInfo crTableLogOnInfo;
                ConnectionInfo crConnectionInfo;

                crConnectionInfo = new ConnectionInfo();
                crConnectionInfo.ServerName = clsPublicVariables.ServerName1;
                crConnectionInfo.DatabaseName = clsPublicVariables.DatabaseName1;
                crConnectionInfo.UserID = clsPublicVariables.UserName1;
                crConnectionInfo.Password = clsPublicVariables.Password1;

                crDatabase = reportdocument.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                }

                //crystalReportViewer1.ReportSource = reportdocument;

                reportdocument.RecordSelectionFormula = formulastr;
                reportdocument.PrintToPrinter(1, false, 0, 0);


                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured in DirectPrintSHMS() " + ex.Message.ToString(), clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void DirectPrintSHMSReport(string FormName_1, string ModuleId_1, String noofprint_1)
        {
            ReportDocument reportdocument = new ReportDocument();
            frmImage frmimg = new frmImage();
            frmReport frmRpt = new frmReport();
            string ReportFilePath;
            string formulastr = "";
            Int64 noofprint1 = 0;

            try
            {
                Int64.TryParse(noofprint_1 + "", out noofprint1);

                ReportFilePath = "";
                clsPublicVariables.enumSHMSReports FormEnum = (clsPublicVariables.enumSHMSReports)Enum.Parse(typeof(clsPublicVariables.enumSHMSReports), FormName_1, true);

                this.GetConnectionDetails();
                this.GetSettingDetails();

                switch (FormEnum)
                {
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Perfomainvoice.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_CASHRECEIPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Cashreceipt.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRASERBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Extrabill.rpt";
                        RptPrintername = RptExtraBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_REGISTRATIONCARD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RegistrationCard.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_BREAKFASTCOUPONS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BreakfastCoupons.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_LUNCHCOUPONS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\LunchCoupons.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_DINNERCOUPONS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DinnerCoupons.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISC:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_WoDisc.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEWODISC:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Perfomainvoice_WoDisc.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLUSD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbillusd.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISCUSD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_WoDisc_USD.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEUSD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PerfomainvoiceUSD.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEWODISCUSD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Perfomainvoice_WoDisc_USD.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_POLICEINQUIRYSAMPLE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PoliceInquirysample.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_FOODCOUPONSSAMPLE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\LunchDinnerCoupons.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_DATEWISELUXURYTAXREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateLuxuryTaxRegister.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_DATEWISEVATTAXREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseVATRegister.rpt";
                        break;

                    case clsPublicVariables.enumSHMSReports.SHMS_DATEWISEEXTTAX1REG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseExttax1Register.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutBillRegister.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHSM_TARIFF_BILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_Tariff.rpt";
                        RptPrintername = RptBillPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_WOTARIFF_BILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_Tariff_Wo.rpt";
                        RptPrintername = RptBillPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_WOTARIFF_DIFFBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutbill_Tariff_Wo_Diff.rpt";
                        RptPrintername = RptBillPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKET:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicket.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKETDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKETSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketSummaryCollection.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillSummary.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUNDDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefundBillDetailed.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLITEMWISEDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillItemWiseDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUNDSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefundBillSummary.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBill.rpt";
                        RptPrintername = RptCouponPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUND:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefundBill.rpt";
                        RptPrintername = RptCouponPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_BLANKGUESTREGISTRATIONCARD:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BlankGuestRegistrationCard.rpt";
                        // RptPrintername = RptPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLREGDATEWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillRegisterDateWise.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_POLICEINQUIRYREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PoliceInquiryReport.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_SEGMENTWISESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SegmentWiseSummary.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMBOOKING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RoomBookingLetter.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_RECIPAYDTL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checkoutreceipt.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_LUXURYTAXDETAILSREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\LuxuaryTaxDetailsReg.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COLLECTIONREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CollectionReport.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLSUMMARYRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutBillDetailsReg.rpt";
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CASHIERREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CashierReport.rpt";
                        RptPrintername = RptBillPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONDEPOSITBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponDepositBill.rpt";
                        RptPrintername = RptCouponPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILLDEPOSITSUMMARYRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillDepositSummary.rpt";
                        RptPrintername = RptCouponPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLREGDTL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutBillDtlReg.rpt";
                        RptPrintername = RptBillPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CHECKOUTBILLWITHGSTINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CheckoutBillwithGstInfo.rpt";
                        RptPrintername = RptBillPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRABILLDETAILREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ExtraServiceBillDetail.rpt";
                        RptPrintername = RptBillPrintername;
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_MONTHLYINFORMATION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\MonthlyInformation.rpt";
                        RptPrintername = RptBillPrintername;
                        break;

                    default:
                        break;
                }


                if (RptPrintername != "")
                {
                    reportdocument.PrintOptions.PrinterName = RptPrintername;
                }

                reportdocument.Load(ReportFilePath, OpenReportMethod.OpenReportByDefault);

                reportdocument.SetDatabaseLogon(clsPublicVariables.UserName1, clsPublicVariables.Password1, clsPublicVariables.ServerName1, clsPublicVariables.DatabaseName1);

                this.AssignParameterToReport(reportdocument);
                //this.AssignMarginToReport(reportdocument);

                formulastr = "";
                // Assign Formula String 
                switch (FormEnum)
                {
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILL:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICE:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICEDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_CASHRECEIPT:
                        formulastr = " {cashreceipt.RID} = [" + ModuleId_1 + "]";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_EXTRASERBILL:
                        formulastr = " {EXTRABILL.RID} = [" + ModuleId_1 + "] and {EXTRABILLDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_REGISTRATIONCARD:
                        formulastr = " {ROOMCHECKIN.RID} = [" + ModuleId_1 + "] and {ROOMCHECKIN.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_BREAKFASTCOUPONS:
                        formulastr = " {ROOMCHECKIN.RID} = [" + ModuleId_1 + "] and {ROOMCHECKIN.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_LUNCHCOUPONS:
                        formulastr = " {ROOMCHECKIN.RID} = [" + ModuleId_1 + "] and {ROOMCHECKIN.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_DINNERCOUPONS:
                        formulastr = " {ROOMCHECKIN.RID} = [" + ModuleId_1 + "] and {ROOMCHECKIN.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISC:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEWODISC:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICEDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLUSD:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMCHECKOUTBILLWODISCUSD:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEUSD:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICEDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_PERFOMAINVOICEWODISCUSD:
                        formulastr = " {PERFOMAINVOICE.RID} = [" + ModuleId_1 + "] and {PERFOMAINVOICEDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHSM_TARIFF_BILL:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False and {ROOMCHECKOUTDTL.SERRID}=0";
                        //{BILLDTL.DELFLG}=False                        
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_WOTARIFF_BILL:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False and {ROOMCHECKOUTDTL.SERRID}>0";
                        //{BILLDTL.DELFLG}=False                        
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_WOTARIFF_DIFFBILL:
                        formulastr = " {ROOMCHECKOUT.RID} = [" + ModuleId_1 + "] and {ROOMCHECKOUTDTL.DELFLG}=False and {ROOMCHECKOUTDTL.SERRID}>0";
                        //{BILLDTL.DELFLG}=False                        
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ENTRYTICKET:
                        formulastr = " {ENTRYTICKET.RID} = [" + ModuleId_1 + "] and {ENTRYTICKET.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONREFUND:
                        formulastr = " {REFUNDBILL.RID} = [" + ModuleId_1 + "] and {REFUNDBILL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONBILL:
                        formulastr = " {COUPONBILL.RID} = [" + ModuleId_1 + "] and {COUPONBILL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_ROOMBOOKING:
                        formulastr = " {ROOMBOOKING.RID} = [" + ModuleId_1 + "] and {ROOMBOOKING.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_RECIPAYDTL:
                        formulastr = " {RECIPAYDTL.RID} = [" + ModuleId_1 + "] and {RECIPAYDTL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    case clsPublicVariables.enumSHMSReports.SHMS_COUPONDEPOSITBILL:
                        formulastr = " {COUPONBILL.RID} = [" + ModuleId_1 + "] and {COUPONBILL.DELFLG}=False";
                        //{BILLDTL.DELFLG}=False
                        break;
                    default:
                        break;
                }

                Database crDatabase;
                Tables crTables;
                TableLogOnInfo crTableLogOnInfo;
                ConnectionInfo crConnectionInfo;

                crConnectionInfo = new ConnectionInfo();
                crConnectionInfo.ServerName = clsPublicVariables.ServerName1;
                crConnectionInfo.DatabaseName = clsPublicVariables.DatabaseName1;
                crConnectionInfo.UserID = clsPublicVariables.UserName1;
                crConnectionInfo.Password = clsPublicVariables.Password1;

                crDatabase = reportdocument.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                }

                //crystalReportViewer1.ReportSource = reportdocument;

                reportdocument.RecordSelectionFormula = formulastr;

                if (noofprint1 > 1)
                {
                    for (int cntprint = 0; cntprint < noofprint1; cntprint++)
                    {
                        reportdocument.PrintToPrinter(1, false, 0, 0);
                    }
                }
                else
                {
                    reportdocument.PrintToPrinter(1, false, 0, 0);
                }

                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured in DirectPrintSHMS() " + ex.Message.ToString(), clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private object GetField(Object obj, String fieldName)
        {
            System.Reflection.FieldInfo fi = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return fi.GetValue(obj);
        }
    }
}
