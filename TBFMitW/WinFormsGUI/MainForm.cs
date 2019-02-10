using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FileManager;
using System.Threading.Tasks;

namespace WinFormsGUI
{
    enum TagElems { Manager, ComboBox, AnotherListView, LastClickedColumn }

    public partial class MainForm : Form
    {
        WinFormsHFC hfc = new WinFormsHFC();
        OpenFileDialog fd = new OpenFileDialog() { DefaultExt = ".exe", Multiselect = false };
        ListView lastFocusedList;
        Size prevSize;
        public MainForm()
        {
            InitializeComponent();
            hfc.CreateREADME();
            hfc.CreateFileOpeningConfig();
            var leftManager = new Manager(new DialogWindows(), new XmlLogger(), new FileManager.Menu(), new FileManager.Menu());
            var rightManager = new Manager(new DialogWindows(), new XmlLogger(), new FileManager.Menu(), new FileManager.Menu());
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripProgressBar1.Visible = false;
            #region Tags
            listView1.Tag = new Dictionary<TagElems, Object>()
            {
                { TagElems.Manager, leftManager},
                { TagElems.ComboBox, comboBox1 },
                { TagElems.AnotherListView, listView2 },
                { TagElems.LastClickedColumn, new int?(3) }
            };
            listView2.Tag = new Dictionary<TagElems, Object>()
            {
                { TagElems.Manager, rightManager },
                { TagElems.ComboBox, comboBox2 },
                { TagElems.AnotherListView, listView1 },
                { TagElems.LastClickedColumn, new int?(3) }
            };
            comboBox1.Tag = listView1;
            comboBox2.Tag = listView2;
            LeftOut.Tag = listView1;
            RightOut.Tag = listView2;
            #endregion
            #region Combo Boxes
            foreach (var i in leftManager.AddMenu.Content)
                comboBox1.Items.Add(i.Name);
            foreach (var i in rightManager.AddMenu.Content)
                comboBox2.Items.Add(i.Name);
            comboBox1.SelectedIndex = comboBox2.SelectedIndex = 0;
            #endregion
            #region List Views
            listView1.Columns.Add("Name", 200);
            listView2.Columns.Add("Name", 200);
            listView1.Columns.Add("Last Write Date", 120);
            listView1.Columns.Add("Size", 70);
            listView2.Columns.Add("Last Write Date", 120);
            listView2.Columns.Add("Size", 70);
            listView1.Columns.Add("Type", 70);
            listView2.Columns.Add("Type", 70);
            listView1.View = listView2.View = View.Details;
            listView1.MultiSelect = listView2.MultiSelect = false;
            listView1.SmallImageList = imageList1;
            listView2.SmallImageList = imageList1;
            listView1.AllowDrop = true;
            listView2.AllowDrop = true;
            listView1.ItemDrag += ListView_Drag;
            listView2.ItemDrag += ListView_Drag;
            listView1.DragEnter += ListView_DragEnter;
            listView2.DragEnter += ListView_DragEnter;
            listView1.DragDrop += ListView_Drop;
            listView2.DragDrop += ListView_Drop;
            UpdateListView(listView1);
            UpdateListView(listView2);
            #endregion
            #region Events
            listView1.DoubleClick += ListView_DoubleClick;
            listView2.DoubleClick += ListView_DoubleClick;
            LeftOut.Click += OutBtn_Click;
            RightOut.Click += OutBtn_Click;
            comboBox1.SelectedValueChanged += ComboBox_Change;
            comboBox2.SelectedValueChanged += ComboBox_Change;
            comboBox1.KeyDown += ComboBoxPath_Enter;
            comboBox2.KeyDown += ComboBoxPath_Enter;
            searchToolStripMenuItem.Click += SearchBtn_Click;
            lastSearchResultToolStripMenuItem.Click += LastSearchBtn_Click;
            showToolStripMenuItem.Click += LogShowBtn_Click;
            clearToolStripMenuItem.Click += LogClearBtn_Click;
            renameToolStripMenuItem.Click += FileRenameBtn_Click;
            createDirectoryToolStripMenuItem.Click += CreateDirectory_Click;
            createFileToolStripMenuItem.Click += CreateFileBtn_Click;
            deleteToolStripMenuItem.Click += DeleteBtn_Click;
            MoveBtn.Click += MoveBtn_Click;
            CopyBtn.Click += CopyBtn_Click;
            listView1.GotFocus += List_GetFocus;
            listView2.GotFocus += List_GetFocus;
            comboBox1.GotFocus += ComboBox_GetFocus;
            comboBox2.GotFocus += ComboBox_GetFocus;
            helpToolStripMenuItem.Click += HelpBtn_Click;
            ResizeBegin += ResizeBegin_Act;
            ResizeEnd += Resize_Act;
            listView1.ColumnClick += Column_Click;
            listView2.ColumnClick += Column_Click;
            listView1.KeyDown += ListViewKeys_Down;
            listView2.KeyDown += ListViewKeys_Down;
            getSelectedInfoToolStripMenuItem.Click += GetSelectedInfoBtn_Click;
            getCurrentInfoToolStripMenuItem.Click += GetCurrentInfoBtn_Click;
            listView1.ItemSelectionChanged += ListViewSelection_Changed;
            listView2.ItemSelectionChanged += ListViewSelection_Changed;
            addStandartFileOpeningProgramToolStripMenuItem.Click += AddStandartProgram;
            #endregion
            #region Focus
            listView1.TabIndex = 0;
            comboBox1.TabIndex = 1;
            LeftOut.TabIndex = 2;
            listView2.TabIndex = 3;
            comboBox2.TabIndex = 4;
            RightOut.TabIndex = 5;
            MoveBtn.TabIndex = 6;
            CopyBtn.TabIndex = 7;
            #endregion
        }

        void ListViewSelection_Changed(object o, EventArgs e)
        {
            var list = o as ListView;
            if (list.SelectedIndices.Count > 0)
            {
                var dict = list.Tag as Dictionary<TagElems, Object>;
                (dict[TagElems.Manager] as Manager).MainMenu.CurPosition = list.SelectedIndices[0];
            }
        }

        void ListView_Drag(object o, ItemDragEventArgs e) => DoDragDrop((e.Item as ListViewItem).Index.ToString(), DragDropEffects.Copy);

        void ListView_DragEnter(object o, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) && (o as ListView) != lastFocusedList)
                e.Effect = DragDropEffects.Copy;
        }

        void ListView_Drop(object o, DragEventArgs e)
        {
            var dict = (o as ListView).Tag as Dictionary<TagElems, Object>;
            var anotherDict = (dict[TagElems.AnotherListView] as ListView).Tag as Dictionary<TagElems, Object>;
            (dict[TagElems.Manager] as Manager).MoveFrom((anotherDict[TagElems.Manager] as Manager).MainMenu.Content[int.Parse(e.Data.GetData(DataFormats.StringFormat).ToString())].FullName);
            (dict[TagElems.Manager] as Manager).Paste();
            UpdateListView(o as ListView);
            (anotherDict[TagElems.Manager] as Manager).UpdateContent();
            UpdateListView(dict[TagElems.AnotherListView] as ListView);
        }

        void List_GetFocus(object o, EventArgs e) => lastFocusedList = o as ListView;

        void ComboBox_GetFocus(object o, EventArgs e) => lastFocusedList = (o as ComboBox).Tag as ListView;

        void ListView_DoubleClick(object o, EventArgs e)
        {
            var list = o as ListView;
            var dict = list.Tag as Dictionary<TagElems, Object>;
            (dict[TagElems.Manager] as Manager).MainMenu.CurPosition = list.SelectedIndices[0];
            (dict[TagElems.Manager] as Manager).MoveIn((dict[TagElems.Manager] as Manager).MainMenu);
            UpdateListView(list);
        }

        void OutBtn_Click(object o, EventArgs e)
        {
            ListView list = (o as Button).Tag as ListView;
            ((list.Tag as Dictionary<TagElems, Object>)[TagElems.Manager] as Manager).MoveOut();
            UpdateListView(list);
        }

        void ComboBox_Change(object o, EventArgs e)
        {
            var box = o as ComboBox;
            if (box.Focused)
            {
                var list = box.Tag as ListView;
                var dict = list.Tag as Dictionary<TagElems, Object>;
                (dict[TagElems.Manager] as Manager).AddMenu.CurPosition = box.SelectedIndex;
                (dict[TagElems.Manager] as Manager).MoveIn((dict[TagElems.Manager] as Manager).AddMenu);
                UpdateListView(list);
            }
        }

        void SearchBtn_Click(object o, EventArgs e)
        {
            var dict = lastFocusedList.Tag as Dictionary<TagElems, Object>;
            Task task = new Task((dict[TagElems.Manager] as Manager).Search);
            task.ContinueWith(new Action<Task>(obj => UpdateListView(lastFocusedList)));
            task.ContinueWith(new Action<Task>(obj => toolStripProgressBar1.Visible = false));
            toolStripProgressBar1.Visible = true;
            task.Start();
        }

        void LastSearchBtn_Click(object o, EventArgs e)
        {
            var dict = lastFocusedList.Tag as Dictionary<TagElems, Object>;
            (dict[TagElems.Manager] as Manager).LastSearchRes();
            UpdateListView(lastFocusedList);
        }

        void LogShowBtn_Click(object o, EventArgs e)
        {
            var dict = lastFocusedList.Tag as Dictionary<TagElems, Object>;
            (dict[TagElems.Manager] as Manager).History();
            UpdateListView(lastFocusedList);
        }

        void LogClearBtn_Click(object o, EventArgs e)
        {
            var dict = lastFocusedList.Tag as Dictionary<TagElems, Object>;
            (dict[TagElems.Manager] as Manager).ClearLog();
        }

        void FileRenameBtn_Click(object o, EventArgs e)
        {
            var dict = lastFocusedList.Tag as Dictionary<TagElems, Object>;
            (dict[TagElems.Manager] as Manager).Rename();
            UpdateListView(lastFocusedList);
        }

        void CreateDirectory_Click(object o, EventArgs e)
        {
            var dict = lastFocusedList.Tag as Dictionary<TagElems, Object>;
            (dict[TagElems.Manager] as Manager).CreateDirectory();
            UpdateListView(lastFocusedList);
        }

        void CreateFileBtn_Click(object o, EventArgs e)
        {
            var dict = lastFocusedList.Tag as Dictionary<TagElems, Object>;
            (dict[TagElems.Manager] as Manager).CreateFile();
            UpdateListView(lastFocusedList);
        }

        void DeleteBtn_Click(object o, EventArgs e)
        {
            var dict = lastFocusedList.Tag as Dictionary<TagElems, Object>;
            (dict[TagElems.Manager] as Manager).Delete();
            UpdateListView(lastFocusedList);
        }

        void ComboBoxPath_Enter(object o, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                var dict = lastFocusedList.Tag as Dictionary<TagElems, Object>;
                (dict[TagElems.Manager] as Manager).SetPath((dict[TagElems.ComboBox] as ComboBox).Text);
                lastFocusedList.Focus();
                UpdateListView(lastFocusedList);
            }
        }

        void CopyBtn_Click(object o, EventArgs e)
        {
            var dict = lastFocusedList.Tag as Dictionary<TagElems, Object>;
            var list2 = dict[TagElems.AnotherListView] as ListView;
            var dict2 = list2.Tag as Dictionary<TagElems, Object>;
            (dict2[TagElems.Manager] as Manager).CopyFrom((dict[TagElems.Manager] as Manager).MainMenu.GetCurrentElement().FullName);
            (dict2[TagElems.Manager] as Manager).Paste();
            UpdateListView(list2);
        }

        void MoveBtn_Click(object o, EventArgs e)
        {
            var dict = lastFocusedList.Tag as Dictionary<TagElems, Object>;
            try
            {
                var list2 = dict[TagElems.AnotherListView] as ListView;
                var dict2 = list2.Tag as Dictionary<TagElems, Object>;
                (dict2[TagElems.Manager] as Manager).MoveFrom((dict[TagElems.Manager] as Manager).MainMenu.GetCurrentElement().FullName);
                (dict2[TagElems.Manager] as Manager).Paste();
                (dict[TagElems.Manager] as Manager).UpdateContent();
                UpdateListView(list2);
                UpdateListView(lastFocusedList);
            }
            catch (NullReferenceException)
            {
                (dict[TagElems.Manager] as Manager).ManagerDialogWindows.ErrorMessage("Select item");
            }
        }

        void HelpBtn_Click(object o, EventArgs e)
        {
            var dict = lastFocusedList.Tag as Dictionary<TagElems, Object>;
            (dict[TagElems.Manager] as Manager).Help("README.txt");
        }

        void GetSelectedInfoBtn_Click(object o, EventArgs e)
        {
            var dict = lastFocusedList.Tag as Dictionary<TagElems, Object>;
            (dict[TagElems.Manager] as Manager).SelectedInfo();
        }

        void GetCurrentInfoBtn_Click(object o, EventArgs e)
        {
            var dict = lastFocusedList.Tag as Dictionary<TagElems, Object>;
            (dict[TagElems.Manager] as Manager).CurrentDirInfo();
        }

        void Column_Click(object o, ColumnClickEventArgs e)
        {
            var dict = lastFocusedList.Tag as Dictionary<TagElems, Object>;
            if (e.Column == dict[TagElems.LastClickedColumn] as int?)
                (dict[TagElems.Manager] as Manager).MakeDescending();
            else
                switch (e.Column)
                {
                    case 0:
                        (dict[TagElems.Manager] as Manager).Sort(new NameComparer());
                        break;
                    case 1:
                        (dict[TagElems.Manager] as Manager).Sort(new LastWriteDateComparer());
                        break;
                    case 2:
                        (dict[TagElems.Manager] as Manager).Sort(new FileSizeComparer());
                        break;
                    case 3:
                        (dict[TagElems.Manager] as Manager).Sort(new DirFilesComparer());
                        break;
                }
            dict[TagElems.LastClickedColumn] = e.Column;
            UpdateListView(lastFocusedList);
        }

        void ListViewKeys_Down(object o, KeyEventArgs e)
        {
            var dict = lastFocusedList.Tag as Dictionary<TagElems, Object>;
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    ListView_DoubleClick(o, e);
                    break;
                case Keys.Back:
                    (dict[TagElems.Manager] as Manager).MoveOut();
                    UpdateListView(o as ListView);
                    break;
                case Keys.C:
                    if (e.Control)
                        CopyBtn_Click(o, e);
                    break;
                case Keys.X:
                    if (e.Control)
                        MoveBtn_Click(o, e);
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        void UpdateListView(ListView list)
        {
            var dict = list.Tag as Dictionary<TagElems, Object>;
            List<ListViewItem> items = new List<ListViewItem>((dict[TagElems.Manager] as Manager).MainMenu.Content.Count);
            foreach (var i in (dict[TagElems.Manager] as Manager).MainMenu.Content)
            {
                items.Add(new ListViewItem(i.Name, i.Is() == FileSystemEntry.Directory ? 0 : 1));
                items[items.Count - 1].SubItems.Add(i.LastWriteTime.ToString());
                items[items.Count - 1].SubItems.Add(i.ToFileInfo().GetSize());
                items[items.Count - 1].SubItems.Add(i.Is() == FileSystemEntry.Directory ? i.Is().ToString() : $"{i.Is().ToString()} ({i.Extension})");
            }
            list.Items.Clear();
            list.Items.AddRange(items.ToArray());
            if ((dict[TagElems.Manager] as Manager).ContentState == State.Default)
                (dict[TagElems.ComboBox] as ComboBox).Text = (dict[TagElems.Manager] as Manager).CurrentDirectory.FullName;
            else
                (dict[TagElems.ComboBox] as ComboBox).Text = (dict[TagElems.Manager] as Manager).ContentState.ToString();
        }

        void ResizeBegin_Act(object o, EventArgs e) => prevSize = Size;

        void Resize_Act(object o, EventArgs e)
        {
            var diff = Size - prevSize;
            listView1.Width += diff.Width / 2;
            listView2.Width += diff.Width / 2;
            comboBox1.Width += diff.Width / 2;
            comboBox2.Width += diff.Width / 2;
            listView1.Height += diff.Height;
            listView2.Height += diff.Height;
            listView2.Left += diff.Width / 2;
            comboBox2.Left += diff.Width / 2;
            LeftOut.Left += diff.Width / 2;
            RightOut.Left += diff.Width / 2;
            MoveBtn.Left += diff.Width / 2;
            CopyBtn.Left += diff.Width / 2;
            MoveBtn.Top += diff.Height / 2;
            CopyBtn.Top += diff.Height / 2;
            comboBox1.Focus();
            comboBox2.Focus();
            listView1.Focus();
        }

        void AddStandartProgram(object o, EventArgs e)
        {
            var dialog = ((lastFocusedList.Tag as Dictionary<TagElems, Object>)[TagElems.Manager] as Manager).ManagerDialogWindows;
            string extention = dialog.Input("Input extention");
            if (fd.ShowDialog() == DialogResult.OK)
                hfc.AddFileOpeningConfigKey(extention, fd.FileName);
        }
    }
}
