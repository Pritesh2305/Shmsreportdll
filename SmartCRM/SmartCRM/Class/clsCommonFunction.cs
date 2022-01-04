using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCRM
{
    class clsCommonFunction
    {

        public bool CheckMobileNumber(string Mobileno) 
        {
            const string validphno="0123456789";
            Int64 cnt=0;
            string tphno="";
            string ch;
            bool retval=true;
            
            try
            {
                tphno = Mobileno.Trim();
                retval=true;

                if (tphno.Length > 13)
                {
                 retval=false;
                 return retval;
                }

               

                   


            }
            catch(Exception ex)
            {

            }
        }
        
        
            If Left(tphno, 1) = "+" And Len(tphno) = 13 Then

                tphno = Right(tphno, Len(tphno) - 1)

            End If

            If Left(tphno, 2) = "91" And Len(tphno) = 12 Then

                tphno = Right(tphno, Len(tphno) - 2)

            End If

            If Left(tphno, 1) = "0" And Len(tphno) = 11 Then

                tphno = Right(tphno, Len(tphno) - 1)

            End If

            If Len(tphno) <> 10 Then

                CheckMobileNumber = False
                Exit Function

            End If

            For cnt = 1 To Len(tphno)

                ch = Mid(tphno, cnt, 1)

                If InStr(1, validphno, ch, vbTextCompare) = 0 Then

                    CheckMobileNumber = False
                    Exit For

                End If

            Next

            Exit Function


        Catch ex As Exception
            Return False
            MsgBox("Error Occured in CheckMobileNo")
        End Try

    End Function
    }
}
