using System;
namespace MusicApi.Backend.SourceApi.Database
{
    public interface IFileWriter
    {
        string DefaultDirectory { get; set; } 

        bool Write(byte[] bytes, string fileName);

        bool Write(byte[] bytes, string fileName, string directory);
    }
}
