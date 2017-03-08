using System.Threading.Tasks;

namespace Server.Business.Email.Interfaces
{
    public interface IEmailOps
    {
        Task<string> GetTemplate(string templateName);

        Task SendEmail(Model.EmailModel gift);
    }
}
