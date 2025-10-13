using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using Models.AppSettings;
using Models.Entities.File;
using Models.Entities.Issue;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Exceptions;

namespace Services.File
{
    public interface IFileService
    {
        Task<List<int>> SaveFile(List<IFormFile> fileList);
    }
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FileService(IUnitOfWork unitOfWork)
        {
        }
        public async Task<List<int>> SaveFile(List<IFormFile> fileList)
        {
            List<TempFileModel> files = new List<TempFileModel>();
            foreach (var file in fileList)
            {
                TempFileModel tempFile = new TempFileModel
                {
                    FileName = file.FileName,
                    Extension = Path.GetExtension(file.FileName),
                    ExpiryDate = DateAndTime.Now.AddDays(1)
                };
                tempFile.FilePath = SaveFileToLocal(file, Guid.NewGuid().ToString() + tempFile.Extension, AppSettings.TemporaryFilePath);
                files.Add(tempFile);

            }
            await _unitOfWork.Repository<TempFileModel, int>().BulkInsertAsync(files);
            return await _unitOfWork.CommitAsync() > 0 
                ? files.Select(s => s.Id).ToList() 
                : throw new BadRequestException("Files not save!");
        }
        public string SaveFileToLocal(IFormFile file, string fileName, string folderName = null)
        {
            try
            {
                var path = string.IsNullOrEmpty(folderName) ? AppSettings.TemporaryFilePath : folderName;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string location = System.IO.Path.Combine(path, fileName);
                using var fileStream = new FileStream(location, FileMode.Create, FileAccess.Write);
                file.CopyTo(fileStream);
                return location;
            }
            catch (Exception ex)
            {
                throw new BadRequestException("File Not Found!");
            }
        }
    }
}
