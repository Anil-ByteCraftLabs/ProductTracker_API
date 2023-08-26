using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Application.Interfaces.FileStorage
{
    public interface IFileStorageProvider
    {
        Task SaveFileAsync(string fileName, Stream sourceStream, bool overwriteFile = false);

        Task RemoveFileAsync(string fileName);

        Task<bool> DoesFileExists(string fileName);

    }
}
