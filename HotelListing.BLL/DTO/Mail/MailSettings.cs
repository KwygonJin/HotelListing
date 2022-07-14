namespace HotelListing.BLL.DTO.Mail
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public bool EnableSsl { get; set; }
        public int Port { get; set; }
    }
}
