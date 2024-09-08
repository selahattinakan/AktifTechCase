using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.WorkerService.Services
{
    public class WriteTextService
    {
        public void WriteText(string text)
        {
            string projectDirectory = Directory.GetCurrentDirectory();
            string folderName = "SmsMail";
            string fileName = "OrderSmsMailText.txt";
            string folderPath = Path.Combine(projectDirectory, folderName);
            string filePath = Path.Combine(folderPath, fileName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            text = $"{text} \n";
            File.AppendAllText(filePath, text);
        }
    }
}
