using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Reflection;

namespace SmartCRM
{
    public class clsDbFunction : clsPublicVariables
    {
        public OleDbConnection msDatabaseCon = new OleDbConnection();
        public OleDbDataAdapter msdtp = new OleDbDataAdapter();
        public OleDbCommand mscmd = new OleDbCommand();
        public OleDbDataReader msdr;
        public OleDbTransaction transaction;
        private ClsGeneral objclsGen = new ClsGeneral();

        public bool OpenDatabaseDataConnection()
        {
            try
            {
                if (File.Exists(Database_Path))
                {
                    msDatabaseCon = new OleDbConnection();
                    if (msDatabaseCon.State == ConnectionState.Open)
                    {
                        msDatabaseCon.Close();
                    }

                    msDatabaseCon.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=False; " +
                                                        " Data Source=" + Database_Path + ";Jet OLEDB:Database Password=" + AccessPassword + ";";
                    msDatabaseCon.Open();
                    return true;
                }
                else
                {
                    MessageBox.Show("Smart CRM Database File Not Found.", PROJECT_TITLE);
                    return false;
                }             


            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occured in Connection() " + ex.Message.ToString(), PROJECT_TITLE);
                Application.Exit();
                return false;
            }
        }

        public bool OpenDatabaseConnection()
        {
            try
            {
                if (string.IsNullOrEmpty(msDatabaseCon.ConnectionString))
                {
                    OpenDatabaseDataConnection();
                }

                if (msDatabaseCon.State == ConnectionState.Closed)
                {
                    msDatabaseCon.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                //clsErrorHendler clserr = new clsErrorHendler();
                //clserr.err = ex.StackTrace;
                //clserr.errForm = clserr.err.Substring(clserr.err.LastIndexOf("\\") + 1);
                //clserr.frmname = clserr.errForm.Substring(0, clserr.errForm.IndexOf(":"));
                //clserr.LineNumber = clserr.errForm.Substring(clserr.errForm.IndexOf(":") + 1);
                //clserr.ProcErrorHandler(ex.Message.ToString(), "ClsDatabaseDbFunction", "OpenDatabaseConnection", clserr.LineNumber);
                return false;
            }
        }

        public bool CloseDatabaseConnection()
        {
            try
            {
                if (msDatabaseCon.State == ConnectionState.Open)
                {
                    msDatabaseCon.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                //clsErrorHendler clserr = new clsErrorHendler();
                //clserr.err = ex.StackTrace;
                //clserr.errForm = clserr.err.Substring(clserr.err.LastIndexOf("\\") + 1);
                //clserr.frmname = clserr.errForm.Substring(0, clserr.errForm.IndexOf(":"));
                //clserr.LineNumber = clserr.errForm.Substring(clserr.errForm.IndexOf(":") + 1);
                //clserr.ProcErrorHandler(ex.Message.ToString(), "ClsDatabaseDbFunction", "CloseDatabaseConnection", clserr.LineNumber);
                return false;
            }

        }

        public bool ExecuteDatabaseCommand(string cmdStr)
        {
            try
            {
                OpenDatabaseConnection();
                mscmd.Connection = msDatabaseCon;
                mscmd.CommandText = cmdStr;
                mscmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                //clsErrorHendler clserr = new clsErrorHendler();
                //clserr.err = ex.StackTrace;
                //clserr.errForm = clserr.err.Substring(clserr.err.LastIndexOf("\\") + 1);
                //clserr.frmname = clserr.errForm.Substring(0, clserr.errForm.IndexOf(":"));
                //clserr.LineNumber = clserr.errForm.Substring(clserr.errForm.IndexOf(":") + 1);
                //clserr.ProcErrorHandler(ex.Message.ToString(), "ClsDatabaseDbFunction", "ExecuteDatabaseCommand", clserr.LineNumber);
                return false;
            }
            finally
            {
                CloseDatabaseConnection();
            }
        }

        public object ExecuteDbCommandScalar(string cmdStr)
        {
            object obj = null;

            try
            {
                OpenDatabaseDataConnection();
                mscmd.Connection = msDatabaseCon;
                mscmd.CommandText = cmdStr;
                obj = mscmd.ExecuteScalar();

                if (DBNull.Value.Equals(obj) == true)
                {
                    obj = "";
                }

                return obj;
            }
            catch (Exception ex)
            {
                //clsErrorHendler clserr = new clsErrorHendler();
                //clserr.err = ex.StackTrace;
                //clserr.errForm = clserr.err.Substring(clserr.err.LastIndexOf("\\") + 1);
                //clserr.frmname = clserr.errForm.Substring(0, clserr.errForm.IndexOf(":"));
                //clserr.LineNumber = clserr.errForm.Substring(clserr.errForm.IndexOf(":") + 1);
                //clserr.ProcErrorHandler(ex.Message.ToString(), "ClsDbFunction", "ExecuteDbCommandScalar", clserr.LineNumber);
                return obj;
            }

        }

        public OleDbDataReader ExecuteMsOleDbDataReader(string cmdStr)
        {
            OleDbDataReader functionReturnValue = default(OleDbDataReader);
            functionReturnValue = msdr;

            try
            {
                OpenDatabaseConnection();
                mscmd.Connection = msDatabaseCon;
                mscmd.CommandText = cmdStr;

                if ((msdr != null))
                {
                    msdr.Close();
                }
                msdr = mscmd.ExecuteReader();
                functionReturnValue = msdr;

            }
            catch (Exception ex)
            {
                functionReturnValue = msdr;
                CloseDatabaseConnection();
                //clsErrorHendler clserr = new clsErrorHendler();
                //clserr.err = ex.StackTrace;
                //clserr.errForm = clserr.err.Substring(clserr.err.LastIndexOf("\\") + 1);
                //clserr.frmname = clserr.errForm.Substring(0, clserr.errForm.IndexOf(":"));
                //clserr.LineNumber = clserr.errForm.Substring(clserr.errForm.IndexOf(":") + 1);
                //clserr.ProcErrorHandler(ex.Message.ToString(), "ClsDatabaseDbFunction", "ExecuteMsOleDbDataReader", clserr.LineNumber);
            }

            return functionReturnValue;
        }

        public DataTable FillDataTable(string cmdstr, string tblnm1)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                OpenDatabaseDataConnection();
                mscmd.Connection = msDatabaseCon;
                msdtp = new OleDbDataAdapter(cmdstr, msDatabaseCon);
                msdtp.Fill(ds, tblnm1);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                //clsErrorHendler clserr = new clsErrorHendler();
                //clserr.err = ex.StackTrace;
                //clserr.errForm = clserr.err.Substring(clserr.err.LastIndexOf("\\") + 1);
                //clserr.frmname = clserr.errForm.Substring(0, clserr.errForm.IndexOf(":"));
                //clserr.LineNumber = clserr.errForm.Substring(clserr.errForm.IndexOf(":") + 1);
                //clserr.ProcErrorHandler(ex.Message.ToString(), "ClsDbFunction", "FillDataTable", clserr.LineNumber);
                return dt;
            }
            finally
            {
                CloseDatabaseConnection();
            }
        }

        public bool FillMstCombo(System.Windows.Forms.ComboBox obj, string FieldName, bool AddAllTextYn)
        {
            string item = "";
            string cmdStr = null;

            try
            {
                cmdStr = ExecuteDbCommandScalar("Select FieldValue from MstCombo where FieldName='" + FieldName + "'").ToString();

                obj.Items.Clear();
                string[] arrContent = cmdStr.Split(new Char[] { '!' });

                obj.Items.Clear();
                if (AddAllTextYn == true)
                {
                    obj.Items.Add("All");
                }

                for (Int32 i = 0; i <= arrContent.Length - 1; i++)
                {
                    item = item + arrContent[i];
                    obj.Items.Add(item);
                    item = "";

                }

                return true;
            }

            catch (Exception ex)
            {
                //clsErrorHendler clserr = new clsErrorHendler();
                //clserr.err = ex.StackTrace;
                //clserr.errForm = clserr.err.Substring(clserr.err.LastIndexOf("\\") + 1);
                //clserr.frmname = clserr.errForm.Substring(0, clserr.errForm.IndexOf(":"));
                //clserr.LineNumber = clserr.errForm.Substring(clserr.errForm.IndexOf(":") + 1);
                //clserr.ProcErrorHandler(ex.Message.ToString(), "ClsDbFunction", "FillMstCombo", clserr.LineNumber);
                return false;
            }
            finally
            {
                CloseDatabaseConnection();
            }
        }

        public void FillDataCombo(object Obj, string DisplayMember1, string ValueMember1, bool AddBlankValue1)
        {
            string strSQL = null;
            OleDbDataAdapter DA = new OleDbDataAdapter();
            DataSet DS = new DataSet();
            object[] vArrSp = null;

            try
            {
                vArrSp = DisplayMember1.Split('|');

                strSQL = "Select " + vArrSp[0].ToString() + ", " + vArrSp[1].ToString() + " from " + ValueMember1 + "";

                if (AddBlankValue1 == true)
                {
                    strSQL = strSQL + " UNION Select 0 as " + vArrSp[0].ToString() + ", '' as " + vArrSp[1].ToString() + " from " + ValueMember1 + "";
                }


                strSQL = strSQL + " Order by " + vArrSp[1].ToString();

                DA = new OleDbDataAdapter(strSQL, msDatabaseCon);
                DA.Fill(DS, ValueMember1);
                ((System.Windows.Forms.ComboBox)Obj).DataSource = DS.Tables[0];
                ((System.Windows.Forms.ComboBox)Obj).DisplayMember = vArrSp[1].ToString();
                ((System.Windows.Forms.ComboBox)Obj).ValueMember = vArrSp[0].ToString();

            }
            catch (Exception ex)
            {
                //    clsErrorHendler clserr = new clsErrorHendler();
                //    clserr.err = ex.StackTrace;
                //    clserr.errForm = clserr.err.Substring(clserr.err.LastIndexOf("\\") + 1);
                //    clserr.frmname = clserr.errForm.Substring(0, clserr.errForm.IndexOf(":"));
                //    clserr.LineNumber = clserr.errForm.Substring(clserr.errForm.IndexOf(":") + 1);
                //    clserr.ProcErrorHandler(ex.Message.ToString(), "ClsDbFunction", "FillDataCombo", clserr.LineNumber);
            }
        }

        public object[] FillCombo(string cmdStr)
        {

            int Ctr1 = 0;

            try
            {
                OpenDatabaseConnection();
                DataTable dt = FillDataTable(cmdStr, "");
                //DataRow row = default(DataRow);

                object[] cmbItems = new object[dt.Rows.Count];

                Ctr1 = 0;

                foreach (DataRow row in dt.Rows)
                {
                    cmbItems[Ctr1] = row[0].ToString();
                    Ctr1 = Ctr1 + 1;
                }

                return cmbItems;

            }
            catch (Exception ex)
            {
                //clsErrorHendler clserr = new clsErrorHendler();
                //clserr.err = ex.StackTrace;
                //clserr.errForm = clserr.err.Substring(clserr.err.LastIndexOf("\\") + 1);
                //clserr.frmname = clserr.errForm.Substring(0, clserr.errForm.IndexOf(":"));
                //clserr.LineNumber = clserr.errForm.Substring(clserr.errForm.IndexOf(":") + 1);
                //clserr.ProcErrorHandler(ex.Message.ToString(), "ClsDbFunction", "Fillcombo", clserr.LineNumber);
                return null;
            }
            finally
            {
                CloseDatabaseConnection();
            }
        }

        public long GetTotalNumberofRecord(string qry1)
        {
            object no1;
            Int64 cnt1;
            try
            {
                ClsGeneral objcls = new ClsGeneral();

                OpenDatabaseConnection();
                mscmd.Connection = msDatabaseCon;
                mscmd.CommandText = qry1;
                no1 = mscmd.ExecuteScalar();
                cnt1 = objcls.Null2lng(no1);
                //cnt1 = Convert.ToInt64(no1);
                return cnt1;

            }
            catch (Exception ex)
            {
                //clsErrorHendler clserr = new clsErrorHendler();
                //clserr.err = ex.StackTrace;
                //clserr.errForm = clserr.err.Substring(clserr.err.LastIndexOf("\\") + 1);
                //clserr.frmname = clserr.errForm.Substring(0, clserr.errForm.IndexOf(":"));
                //clserr.LineNumber = clserr.errForm.Substring(clserr.errForm.IndexOf(":") + 1);
                //clserr.ProcErrorHandler(ex.Message.ToString(), "ClsDbFunction", "GetTotalNumberofRecord", clserr.LineNumber);
                return 0;
            }
        }

        public string GetFieldValue(string qry1)
        {
            object str;
            string str1;
            try
            {
                OpenDatabaseConnection();
                mscmd.Connection = msDatabaseCon;
                mscmd.CommandText = qry1;
                str = mscmd.ExecuteScalar();
                str1 = Convert.ToString(str);
                return str1;
            }
            catch (Exception ex)
            {
                //clsErrorHendler clserr = new clsErrorHendler();
                //clserr.err = ex.StackTrace;
                //clserr.errForm = clserr.err.Substring(clserr.err.LastIndexOf("\\") + 1);
                //clserr.frmname = clserr.errForm.Substring(0, clserr.errForm.IndexOf(":"));
                //clserr.LineNumber = clserr.errForm.Substring(clserr.errForm.IndexOf(":") + 1);
                //clserr.ProcErrorHandler(ex.Message.ToString(), "ClsDbFunction", "GetFieldValue", clserr.LineNumber);
                return "";
            }
        }

    }
}
