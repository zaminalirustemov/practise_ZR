namespace Boutique_ZR.Services
{

    public interface IEmailSender
    {
        void Send(string to, string subject, string html);
        void Send(string[] allTo, string subject, string html);
    }
}
