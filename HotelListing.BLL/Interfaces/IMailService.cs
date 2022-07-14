using HotelListing.BLL.DTO.Mail;

namespace HotelListing.BLL.Interfaces;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);

    Task SendEmailDefaultSmtpAsync(MailRequest mailRequest);
}