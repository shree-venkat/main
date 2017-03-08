using Server.Api.Helpers;
using Server.Api.ViewModels;
using Server.Business.User.Interfaces;
using Server.Model;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Web.Api.Controllers
{
    public class DropboxController : ApiController
    {
        private readonly IGdriveOps _drive;

        public DropboxController(IGdriveOps drive)
        {
            _drive = drive;
        }

        // GET api/GDrive
        public IHttpActionResult Get(int page = 1, int pageSize = 10, string searchTerm = "")
        {
            try
            {
                var files = _drive.SearchFileNames(searchTerm ?? string.Empty);

                var pagedFiles = new PagedCollection().GetPagedCollection(files.OrderBy(x => x.FileName).ThenByDescending(x => x.UploadedOn), page, pageSize);

                var viewModel = new PagedCollectionViewModel<FileContent>
                {
                    Items = pagedFiles.Items,
                    Page = pagedFiles.Page,
                    TotalCount = pagedFiles.TotalCount,
                    TotalPages = pagedFiles.TotalPages
                };

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return WebApiErrorHandler.Throw(ex);
            }
        }

        // GET api/GDrive/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var file = _drive.GetFile(id);
                if (file == null)
                {
                    return NotFound();
                }

                var result = new HttpResponseMessage(HttpStatusCode.OK)
                                            {
                                                Content = new ByteArrayContent(file.Content)
                                            };
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                                            {
                                                FileName = file.FileName
                                            };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                return ResponseMessage(result);
            }
            catch (Exception ex)
            {
                return WebApiErrorHandler.Throw(ex);
            }
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
