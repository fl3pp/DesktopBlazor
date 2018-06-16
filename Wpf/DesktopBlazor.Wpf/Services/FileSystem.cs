using DesktopBlazor.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBlazor.Wpf
{
    public interface IFileSystem
    {
        IEnumerable<File> GetFiles(string folder);
    }

    internal sealed class FileSystem : IFileSystem
    {
        public IEnumerable<File> GetFiles(string folder)
        {
            foreach (var directory in System.IO.Directory.EnumerateDirectories(folder))
            {
                yield return new File { Kind = FileKind.Folder, Path = directory };
            }

            foreach (var file in System.IO.Directory.EnumerateFiles(folder))
            {
                yield return new File { Kind = FileKind.File, Path = file };
            }
        }
    }
}
