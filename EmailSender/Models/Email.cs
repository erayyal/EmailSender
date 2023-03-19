namespace EmailSender.Models;

public class Email
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public string From { get; set; }
    public string To { get; set; }
}