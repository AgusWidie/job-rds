using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WEB_API_DASHBOARD.Services
{
    public class CreateFile
    {
        public bool Write(string ImgStr, string MapPath, string ImgName)
        {
            String path = HttpContext.Current.Server.MapPath(MapPath); //Path

            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            string imageName = ImgName;

            //set the image path
            string imgPath = Path.Combine(path, imageName);

            byte[] imageBytes = Convert.FromBase64String(ImgStr);

            File.WriteAllBytes(imgPath, imageBytes);

            return true;
        }

    }
}