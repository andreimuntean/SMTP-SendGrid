using System;
using System.Threading.Tasks;

class Program
{
    static string ReadLine(string displayedMessage)
    {
        Console.Write(displayedMessage + ":\n> ");

        return Console.ReadLine();
    }

    /// <summary>
    /// Reads a body of text. Stops once the specified line has been read.
    /// </summary>
    /// <param name="displayedMessage">The message displayed to the user.</param>
    /// <param name="lastLine">Stops reading once this line has been read.</param>
    /// <returns></returns>
    static string ReadBody(string displayedMessage, string lastLine)
    {
        var body = "";
        var nextLine = "";

        Console.Write(string.Format("{0} (last line must be \"{1}\"):\n> ",
            displayedMessage, lastLine));

        while ((nextLine = Console.ReadLine()) != lastLine)
        {
            body += nextLine + "\n";
        }

        // Removes the last newline.
        return body.TrimEnd('\n');
    }

    static void Main(string[] args)
    {
        var senderName = ReadLine("Sender Name");
        var senderEmail = ReadLine("Sender Email");
        var recipientEmails = ReadBody("Recipient Emails", string.Empty).Split('\n');
        var subject = ReadLine("Subject");
        var message = ReadBody("Message", "-");

        Task.Run(async () =>
        {
            try
            {
                await new EmailService().Send(subject, message, senderName, senderEmail, recipientEmails);
                Console.WriteLine("Email sent.");
            }
            catch
            {
                Console.WriteLine("Could not send email.");
            }
        });

        Console.Read();
    }
}
