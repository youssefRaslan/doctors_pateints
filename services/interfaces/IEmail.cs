namespace doctors.services.interfaces
{
    public interface IEmail
    {
        Task SendEmailAsync(string toEmail, string subject);
     
    }
}
