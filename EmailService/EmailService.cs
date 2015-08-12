using SendGrid;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

class EmailService
{
    public async Task Send(string subject, string message, string senderName, string senderEmail, params string[] recipientEmails)
    {
        // Partially creates the email.
        var email = new SendGridMessage()
        {
            Subject = subject,
            Text = message,
            From = new MailAddress(senderEmail, senderName)
        };

        // Adds the recipients.
        email.AddTo(recipientEmails);

        // Registers the credentials required to send the email via SendGrid.
        var credentials = new NetworkCredential()
        {
            UserName = "your-sendgrid-username",
            Password = "your-sendgrid-password"
        };

        // Sends the email.
        await new Web(credentials).DeliverAsync(email);
    }
}