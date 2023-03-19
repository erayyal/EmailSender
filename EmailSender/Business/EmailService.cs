using System.Net;
using System.Net.Mail;
using EmailSender.Models;

namespace EmailSender.Business;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public EmailResponseModel SendEmail(EmailRequestModel emailRequestModel)
    {
        try
        {
            var fromAddress = new MailAddress(_configuration.GetSection("FromAddress").Value, "İsim");
            var toAddress = new MailAddress("Formun gönderileceği email adresi", "İsim");
            var toSender = new MailAddress(emailRequestModel.EmailAddress, emailRequestModel.Name);
            var fromPassword = _configuration.GetSection("Password").Value;


            var smtp = new SmtpClient
            {
                Host = _configuration.GetSection("Host").Value,
                Port = Convert.ToInt32(_configuration.GetSection("Port").Value),
                EnableSsl = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using var messageToUs = new MailMessage(fromAddress, toAddress)
            {
                IsBodyHtml = true,
                Subject = "Yeni Mesajınız Var!",
                Body = "<h4>Gönderenin Maili : " + emailRequestModel.EmailAddress + "</h4>"
                       + "<h4>Gönderenin Adı : " + emailRequestModel.Name + "</h4>"
                       + "<h4>Göndrilen Mailin Konusu : " + emailRequestModel.Subject + "</h4>"
                       + "<h4>Göndrilen Formun İçeriği ; </h4>"
                       + emailRequestModel.Message
            };
            using var messageToSender = new MailMessage(fromAddress, toSender)
            {
                IsBodyHtml = true,
                Subject = "Otomatik Cevap!",
                Body = "<h3>Teşekkürler " + emailRequestModel.Name + "!</h3>"
            };
            smtp.Send(messageToSender);
            smtp.Send(messageToUs);
            return new EmailResponseModel { Status = true, StatusText = "Mail başarıyla gönderildi" };
        }
        catch (Exception ex)
        {
            return new EmailResponseModel { Status = false, StatusText = ex.Message };
        }
    }
}