using System.Net.Mail;

namespace SaveEmailOnLocalDisk.Services;

public class SaveMessageService : ISaveMessageService
{
    public Task SaveEmailAsync(string emailFrom, string emailTo, string subject, string body)
    {
        try
        {
            Execute(emailFrom, emailTo, subject, body).Wait();
            return Task.FromResult(0);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task Execute(string emailFrom, string emailTo, string subject, string body)
    {
        try
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(emailFrom, "API Save Email")
            };

            mail.To.Add(new MailAddress(emailTo));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            using (var client = new SmtpClient("somehost"))
            {
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = @"C:\";
                client.Send(mail);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
