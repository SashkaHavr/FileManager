using System.Collections.Generic;
using System.IO;
using System;

namespace FileManager
{
    public class Menu
    {
        public List<FileSystemInfo> Content { get; internal set; } = new List<FileSystemInfo>();
        public int CurPosition { get; set; }

        public virtual void NullPos() => CurPosition = 0;

        public virtual void ControllPos()
        {
            if (CurPosition < 0)
                CurPosition = 0;
            else if (CurPosition >= Content.Count)
                CurPosition = Content.Count - 1;
        }

        public virtual void MoveUp()
        {
            CurPosition--;
            if (CurPosition < 0)
                CurPosition = Content.Count - 1;
        }
        public virtual void MoveDown()
        {
            CurPosition++;
            if (CurPosition > Content.Count - 1)
                CurPosition = 0;
        }
        public FileSystemInfo GetCurrentElement() => Content.Count > 0 ? Content[CurPosition] : null;
    }
}