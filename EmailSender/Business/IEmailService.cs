using EmailSender.Models;

namespace EmailSender.Business;

public interface IEmailService
{
    EmailResponseModel SendEmail(EmailRequestModel emailRequestModel);
}