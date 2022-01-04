using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace SmartCRM
{
    public static class clsSendEmail
    {

        public static bool clsSendEmailMsg(string recipient,
                                            string sender,
                                            string SMTP_Server,
                                            string senderName,
                                            string recipientName,
                                            string Subject,
                                            string attachment,
                                            string msgText,
                                            string Cc,
                                            string Bcc)
        {

            try
            {

                MailMessage mailMsg = new MailMessage();
                MailAddress ESender, ERecipient;
                SmtpClient client = new SmtpClient(SMTP_Server);

                if (senderName != "")
                {
                    ESender = new MailAddress(sender, senderName);
                }
                else
                {
                    ESender = new MailAddress(sender);
                }

                if (recipientName != "")
                {
                    ERecipient = new MailAddress(recipient, recipientName);
                }
                else
                {
                    ERecipient = new MailAddress(recipient);
                }

                //Main Settings

                mailMsg.From = ESender;
                mailMsg.To.Add(recipient);

                if (Cc != "")
                {
                    mailMsg.CC.Add(Cc);
                }

                if (Bcc != "")
                {
                    mailMsg.Bcc.Add(Bcc);
                }

                if (attachment != "")
                {
                    Attachment attach = new Attachment(attachment);
                    mailMsg.Attachments.Add(attach);
                }

                if (msgText != "")
                {
                    mailMsg.Body = msgText;
                }

                mailMsg.Priority = MailPriority.High;
                client.Send(mailMsg);

                return true;
            }
            catch (Exception ex)
            {
                //clsErrorHendler clserr = new clsErrorHendler();
                //clserr.err = ex.StackTrace;
                //clserr.errForm = clserr.err.Substring(clserr.err.LastIndexOf("\\") + 1);
                //clserr.frmname = clserr.errForm.Substring(0, clserr.errForm.IndexOf(":"));
                //clserr.LineNumber = clserr.errForm.Substring(clserr.errForm.IndexOf(":") + 1);
                //clserr.ProcErrorHandler(ex.Message.ToString(), "clsSendEmail", "clsSendEmail", clserr.LineNumber);
                return false;
            }

        }

    }
}
