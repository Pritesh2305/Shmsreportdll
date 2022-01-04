using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Shmsreportdll
{
    public class clsPublicVariables
    {
        public enum enumRMSForms
        {
            RMS_MSTUNIT,
            RMS_MSTDEPT,
            RMS_MSTITEMGROUP,
            RMS_MSTITEM,
            RMS_MSTTABLE,
            RMS_MSTEMPCAT,
            RMS_MSTEMP,
            RMS_MSTITEMPRICELIST,
            RMS_KOT,
            RMS_BILL,
            RMS_SETTLEMENT,
            RMS_KOTREGISTER,
            RMS_BILLREGISTER,
            RMS_KOTREG,
            RMS_BILLREG,
            RMS_KOTB,
            RMS_BILLB,
            RMS_SETTLEMENTB,
            RMS_KOTBREGISTER,
            RMS_BILLBREGISTER,
            RMS_KOTBREG,
            RMS_BILLBREG,
            RMS_PURCHAESREGISTER,
            RMS_PAYMENTREGISTER,
            RMS_CUSTOUTSTANDING,
            RMS_KOTEDITREG,
            RMS_KOTDELETEREG,
            RMS_BILLEDITREG,
            RMS_BILLDELETEREG,
            RMS_ITEMWISESALES,
            POS_BILLREGISTER,
            POS_BILLBREGISTER,
            POS_BILLREG,
            POS_BILLBREG,
            POS_SETTLEMENT,
            POS_SETTLEMENTB,
            POS_SUPPLIERREGISTER,
            POS_PURCHAESREGISTER,
            POS_PAYMENTREGISTER,
            POS_CUSTOUTSTANDING,
            POS_BILLEDITREG,
            POS_BILLDELETEREG,
            POS_ITEMWISESALES,
            RMS_DATEWISEBILLING,
            RMS_REVISEDBILLREG,
            RMS_CAPCOMMIREGISTER,
            RMS_BILLWISESALESSUMMARY,
            RMS_BANQBOOKINGREG,
            RMS_ITEMGROUPWISESALES,
            POS_BILL,
            RMS_VATREGISTER,
            RMS_REPORTDEPARTWISESALES,
            RMS_CHECKLISTITEMSTOCK,
            RMS_TABLERUNNINGSUMMARY
        };

        public enum enumSHMSReports
        {
            SHMS_CHECKINREGISTER,
            SHMS_ROOMCHECKOUTBILL,
            SHMS_CHECKINOUTREGISTER,
            SHMS_PERFOMAINVOICE,
            SHMS_CASHRECEIPT,
            SHMS_EXTRABILLREGISTER,
            SHMS_CASHRECEIPTREGISTER,
            SHMS_EXTRASERBILL,
            SHMS_CHECKOUTREGISTER,
            SHMS_OCCUPANCYREGISTER,
            SHMS_SALESREGISTER,
            SHMS_REGISTRATIONCARD,
            SHMS_EXTRABILLREGCHARGEWISE,
            SHMS_CHARGEWISEEXTRASERVICE,
            SHMS_ROOMWISEEXTRASERVICE,
            SHMS_ROOMTARIFFWISEREG,
            SHMS_LUNCHCOUPONS,
            SHMS_BREAKFASTCOUPONS,
            SHMS_DINNERCOUPONS,
            SHMS_ROOMCHECKOUTBILLWODISC,
            SHMS_PERFOMAINVOICEWODISC,
            SHMS_ROOMCHECKOUTBILLUSD,
            SHMS_ROOMCHECKOUTBILLWODISCUSD,
            SHMS_PERFOMAINVOICEUSD,
            SHMS_PERFOMAINVOICEWODISCUSD,
            SHMS_POLICEINQUIRYSAMPLE,
            SHMS_FOODCOUPONSSAMPLE,
            SHMS_DATEWISELUXURYTAXREG,
            SHMS_DATEWISEVATTAXREG,
            SHMS_DATEWISEEXTTAX1REG,
            SHMS_CHECKOUTBILLREG,
            SHSM_TARIFF_BILL,
            SHMS_WOTARIFF_BILL,
            SHMS_ENTRYTICKET,
            SHMS_COUPONREFUND,
            SHMS_COUPONBILL,
            SHMS_ENTRYTICKETDETAILCOLLECTIONRPT,
            SHMS_ENTRYTICKETSUMMARYCOLLECTIONRPT,
            SHMS_COUPONBILLDETAILCOLLECTIONRPT,
            SHMS_COUPONBILLSUMMARYCOLLECTIONRPT,
            SHMS_COUPONREFUNDDETAILCOLLECTIONRPT,
            SHMS_COUPONBILLITEMWISEDETAILCOLLECTIONRPT,
            SHMS_COUPONREFUNDSUMMARYCOLLECTIONRPT,
            SHMS_BLANKGUESTREGISTRATIONCARD,
            SHMS_COUPONBILLREGDATEWISE,
            SHMS_POLICEINQUIRYREPORT,
            SHMS_SEGMENTWISESUMMARY,
            SHMS_ROOMBOOKING,
            SHMS_RECIPAYDTL,
            SHMS_LUXURYTAXDETAILSREG,
            SHMS_COLLECTIONREPORT,
            SHMS_CHECKOUTBILLSUMMARYRPT,
            SHMS_CASHIERREPORT,
            SHMS_COUPONDEPOSITBILL,
            SHMS_COUPONBILLDEPOSITSUMMARYRPT,
            SHMS_CHECKOUTBILLREGDTL,
            SHMS_CHECKOUTBILLWITHGSTINFO,
            SHMS_WOTARIFF_DIFFBILL,
            SHMS_EXTRABILLDETAILREGISTER,
            SHMS_MONTHLYINFORMATION
        };
       
        public enum enumReportFilterCondition
        {
            Random=1
        };

        public static string AppPath = Path.GetDirectoryName(Application.ExecutablePath).ToString();
        public static string ReportPath = AppPath + "\\Report" ;
        public static string Project_Title = "KRUPA INFOTECH";
        
        public static string err;
        public static string errForm;
        public static string frmname;
        public static string LineNumber;

        public static string DatabaseType1;
        public static string ServerName1;
        public static string DatabaseName1;
        public static string UserName1;
        public static string Password1;
        public static string DataPort1;

        public static string RptTitle1;
        public static string RptTitle2;
        public static string RptTitle3;
        public static string RptTitle4;
        public static string RptTitle5;
        public static string RptTitle6;
        public static string RptTitle7;
        public static string RptTitle8;
        
        public static string RptPrintername;
        public static string RptBillPrintername;
        public static string RptExtraBillPrintername;
        public static string RptCouponPrintername;
        public static string RptRefundPrintername;
        
        public static string RptFooter1;
        public static string RptFooter2;
        public static string RptFooter3;
        public static string RptFooter4;
        public static string RptFooter5;
        public static string RptFooter6;
        public static string RptFooter7;
        public static string RptFooter8;
        
        public static string CbHeader1;
        public static string CbHeader2;
        public static string CbHeader3;
        public static string CbFooter1;
        public static string CbFooter2;
        public static string CbFooter3;

        public static string CrHeader1;
        public static string CrHeader2;
        public static string CrHeader3;
        public static string CrFooter1;
        public static string CrFooter2;
        public static string CrFooter3;
        public static string BillTitle;


    }
}
