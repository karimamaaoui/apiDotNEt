using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        public static IWebHostEnvironment _webHostEnivronment;

        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnivronment=webHostEnvironment;
        }   

        [HttpPost]
        public async Task<string> Post([FromForm] FileUpload fileUpload)
        {
         try{
                if(fileUpload.files.Length>0)
                {
                    string path=_webHostEnivronment.WebRootPath+"\\images\\";
                    if(!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream FileStream =System.IO.File.Create(path +fileUpload.files.FileName))
                    {
                        fileUpload.files.CopyTo(FileStream);
                        FileStream.Flush();
                        return "Upload Done.";
                    }
                    }
                        else {
                            return "failed";
                        
                    
                }
         }  catch(Exception e )
         {
            return e.Message;
         } 
        }
    
        [HttpGet("{fileName}")]
        public async Task<IActionResult> Get([FromRoute] string fileName){
                string path=_webHostEnivronment.WebRootPath+"\\images\\";
                var filePath=path+fileName +".png";
                if(System.IO.File.Exists(filePath))
                {
                    byte [] b=System.IO.File.ReadAllBytes(filePath);
                    return File(b,"image/png");
                }
                return null;
        }
    
    
    }
}