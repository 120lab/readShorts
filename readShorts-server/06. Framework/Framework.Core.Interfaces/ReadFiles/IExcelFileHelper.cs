using System.Collections.Generic;

namespace Framework.Core.Interfaces.ReadFiles
{
    public interface IExcelFileHelper
    {
        IFileData CreateFileAndGetFileData<T>(List<T> records);
        byte[] GenerateExcel<T>(string path, string sheetName, List<T> records);
    }
}