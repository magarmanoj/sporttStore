using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Common.Interfaces
{
    public interface IFileUploadService
    {
        void UploadFile(Stream input, string filepath);
    }
}
