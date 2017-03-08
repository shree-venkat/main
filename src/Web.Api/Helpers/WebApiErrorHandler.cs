using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Server.Api.Helpers
{
    using System;

    public class WebApiErrorHandler
    {
        public static IHttpActionResult Throw(Exception ex)
        {
            throw new HttpResponseException(
                new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(GetExceptionDetails(ex))
                });
        }

        private static string GetExceptionDetails(Exception ex)
        {
            var level = 0;
            var error = new StringBuilder(ex != null ? ex.Message : string.Empty);
            while (ex != null && ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
            {
                error.AppendLine(string.Format("\r\nInner Exception (level - {0}): {1}", ++level, ex.InnerException.Message));
                ex = ex.InnerException;
            }

            return error.ToString();
        }
    }
}