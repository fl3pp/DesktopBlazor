using System;

namespace DesktopBlazor.Contracts
{
    public enum FileKind
    {
        File,
        Folder,
    }

    public class File
    {
        public FileKind Kind { get; set; }
        public string Path { get; set; }
    }
}
