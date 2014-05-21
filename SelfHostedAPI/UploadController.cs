using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SelfHostedAPI
{
   public class UploadController : ApiController
   {
      public IHttpActionResult Get()
      {
         return Ok("test");
      }

      static readonly string ServerUploadFolder = @"c:\temp\uploads";

      [HttpPut]
      public async Task<FileResult> Put()
      {
         // Verify that this is an HTML Form file upload request
         if (!Request.Content.IsMimeMultipartContent("form-data"))
         {
            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.UnsupportedMediaType));
         }
         
         // Create a stream provider for setting up output streams
         var streamProvider = new CustomFormDataStreamProvider(ServerUploadFolder);
      
         // Read the MIME multipart asynchronously content using the stream provider we just created.
         await Request.Content.ReadAsMultipartAsync(streamProvider);

         // Create response
         return new FileResult
         {
            FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName),
            Submitter = streamProvider.FormData["submitter"]
         };
      }
   }

   public class CustomFormDataStreamProvider :MultipartFormDataStreamProvider
   {
      public CustomFormDataStreamProvider(string rootPath) : base(rootPath)
      {
      }

      public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
      {
         if (headers == null)
         {
            throw new NullReferenceException("headers cant be null");
         }
         return headers.ContentDisposition.FileName.Replace("\"","");
      }
   }
}