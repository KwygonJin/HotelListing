using HotelListing.DTO.Mail;
using System.Threading.Tasks;

namespace HotelListing.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

        Task SendEmailDefaultSmtpAsync(MailRequest mailRequest);
    }
}
