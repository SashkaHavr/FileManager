using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager;

namespace ConsoleGUI
{
    class PageMenu : Menu
    {
        public int Page { get; private set; } = 0;
        public bool Scrolled { get; set; } = false;

        public override void NullPos()
        {
            base.NullPos();
            Page = 0;
        }
        public override void ControllPos()
        {
            base.ControllPos();
            if(Page < 0)
                Page = 0;
            if (Page > Content.Count / ConsoleHelpers.GetMaxStrings())
                Page = Content.Count / ConsoleHelpers.GetMaxStrings();
        }
        public override void MoveUp()
        {
            CurPosition--;
            if (Page > 0 && CurPosition < 0)
            {
                Page--;
                Scrolled = true;
                CurPosition = ConsoleHelpers.GetMaxStrings() - 1;
            }
            else if (CurPosition < 0)
            {
                if (Content.Count / ConsoleHelpers.GetMaxStrings() > 0 && !(Content.Count % ConsoleHelpers.GetMaxStrings() == 0 && Page == 0))
                {
                    Page = Content.Count / ConsoleHelpers.GetMaxStrings();
                    Scrolled = true;
                }
                CurPosition = Content.Count - 1 - ConsoleHelpers.GetMaxStrings() * Page;
            }
        }
        public override void MoveDown()
        {
            CurPosition++;
            if (CurPosition == ConsoleHelpers.GetMaxStrings())
            {
                Page++;
                Scrolled = true;
                CurPosition = 0;
            }
            if (CurPosition > Content.Count - 1 - ConsoleHelpers.GetMaxStrings() * Page)
            {
                CurPosition = 0;
                if(Page == Content.Count / ConsoleHelpers.GetMaxStrings() && Page != 0)
                {
                    Page = 0;
                    Scrolled = true;
                }
            }
        }
    }
}
