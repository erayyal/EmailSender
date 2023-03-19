using EmailSender.Business;
using EmailSender.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EmailSender.Controllers;

public class EmailController : Controller
{
    private readonly IEmailService _emailService;


    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("SendEmail")]
    [SwaggerOperation(
        Summary = "Mail gönderir",
        Description = "Mail gönderir")]
   
    public EmailResponseModel SendEmail([FromBody] EmailRequestModel emailRequestModel)
    {
        var response = _emailService.SendEmail(emailRequestModel);
        return (response);
    }
}