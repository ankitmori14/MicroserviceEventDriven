using System.Net.Mail;
using System.Net;
using NotificationService.Model;
using System.Reflection;

namespace NotificationService
{
    public class SendEmailNotification
    {
        /// <summary>
        /// Send email Notification
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityDetails"></param>
        public void SendEmail<T>(T entityDetails)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.Credentials = new NetworkCredential("", "");
                client.EnableSsl = true;
                string result = ToCommaSeparatedString(entityDetails);
                var aaa = entityDetails;
                // Create the mail message
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("no-reply@gmail.com", "Your Name"),
                    Subject = "New Order has been Placed",
                    Body = result,
                    IsBodyHtml = false
                };

                // Add recipients
                mailMessage.To.Add("youemail@gmail.com");

                // Send the email
                try
                {
                    client.Send(mailMessage);
                    Console.WriteLine("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// ToCommaSeparatedString
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public string ToCommaSeparatedString<T>(T model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            // Get all public properties of the model
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Create a comma-separated string of property names and values
            var result = properties.Select(p =>
            {
                // Get the property name and value
                var propertyName = p.Name;
                var propertyValue = p.GetValue(model)?.ToString() ?? string.Empty;

                // Format as PropertyName:Value
                return $"{propertyName}:{propertyValue}";
            });

            // Join the formatted strings with commas
            return string.Join(",", result);
        }
    }

}
