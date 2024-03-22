using NewBalance.Application.Requests.Mail;
using System.Threading.Tasks;

namespace NewBalance.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}