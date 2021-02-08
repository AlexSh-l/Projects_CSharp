using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace FileStorage.Controllers
{
    [ApiController]
    [Route("[controller]/path/to")]
    public class FStorageController : ControllerBase
    {
        public const string mainRoot = "D:\\Alex\\КСИС\\Labs\\FileStorage\\Storage";

        private readonly ILogger<FStorageController> _logger;

        public FStorageController(ILogger<FStorageController> logger)
        {
            _logger = logger;
        }

        public List<string> GetAll(string path)
        {
            try
            {
                var directories = Directory.GetDirectories(path);
                var files = Directory.GetFiles(path);
                var list = new List<string>();
                list.AddRange(directories);
                list.AddRange(files);
                list.ForEach(str => str.Replace("D:\\Alex\\КСИС\\Labs\\FileStorage\\Storage\\", ""));
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    list[i] = list[i].Replace(mainRoot + "\\", "");
                }
                return list;
            }
            catch
            {
                throw new Exception("Unable to get contents.");
            }
        }

        public bool InsertFile(string filename, IFormFile file)
        {
            try
            {
                var fileStream = new FileStream(mainRoot + "\\" + filename + "\\" + file.FileName, FileMode.Create);
                file.CopyTo(fileStream);
                fileStream.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }        

        [HttpGet]
        [HttpGet("{*filename}")]
        public ActionResult GETFile(string filename)
        {
            if (filename == null || filename.Trim() == "")
            {
                try
                {
                    if (Directory.GetFiles(mainRoot).Length != 0 && Directory.GetDirectories(mainRoot).Length != 0)
                    {                        
                        return new JsonResult(GetAll(mainRoot));
                    }
                    else
                    {
                        return Ok("Directory is empty.");                        
                    }
                }
                catch
                {
                    return BadRequest("Unable to read the directory, it may be deleted.");
                }
            }
            else
            {
                string path = mainRoot + "\\" + filename;
                if (Directory.Exists(path))
                {
                    if (Directory.GetFiles(path).Length != 0 && Directory.GetDirectories(path).Length != 0)
                    {                        
                        try
                        {
                            return new JsonResult(GetAll(path));
                        }
                        catch
                        {
                            return BadRequest("Unable to read the directory.");
                        }
                    }
                    else
                    {
                        return Ok("Directory is empty.");
                    }

                }
                else if(System.IO.File.Exists(path))
                {
                    try
                    {
                        FileStream fileStream = new FileStream(path, FileMode.Open);
                        return File(fileStream, "application/unknown", filename);
                    }
                    catch
                    {
                        return BadRequest("Can't open the file.");
                    }
                }
                else
                {
                    return NotFound("It doesn't exist.");
                }
            }
        }

        [HttpPut("{*filename}")]
        public ActionResult PUTFile(IFormFile file, string filename)
        {
            if (filename != null)
            {                   
                if (InsertFile(filename, file))
                {
                    return Ok(file.FileName + " was added.");
                }
                else
                {
                    return BadRequest("Unable to add.");
                }
            }
            else
            {
                return BadRequest("File has no name.");
            }
        }

        [HttpHead("{*filename}")]
        public ActionResult HEADFile(string filename)
        {
            try
            {
                var path = mainRoot + "\\" + filename;
                if (System.IO.File.Exists(path))
                {
                    return GETFile(filename);
                }
                else
                {
                    return NotFound("Unable to find the file.");
                }
            }
            catch
            {
                return BadRequest("Incorrect request.");
            }
        }

        [HttpDelete("{*filename}")]
        public ActionResult DELETEFile(string filename)
        {
            try
            {
                var path = mainRoot + "\\" + filename;
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return Ok(filename + " was deleted.");
                }
                else if (Directory.Exists(path))
                {
                    if (filename != "" && filename != null)
                    {
                        Directory.Delete(path, true);
                        return Ok(filename + " was deleted.");
                    }
                    else
                    {
                        return BadRequest("Unable to delete root.");
                    }
                }
                else
                {
                    return BadRequest("Incorrect request.");
                }
            }
            catch
            {
                return BadRequest("Incorrect request.");
            }
        }      

    }
}
