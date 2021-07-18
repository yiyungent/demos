using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace MonitClipbrdDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ClipboardHooker m_clipboardHooker;
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            m_clipboardHooker = new ClipboardHooker(this);
            m_clipboardHooker.ClipboardUpdated += OnClipboardUpdated;
        }

        private void OnClipboardUpdated(object sender, EventArgs e)
        {
            tb.Text = "老板，有人修改了剪贴板。";
            IDataObject data = Clipboard.GetDataObject();
            string[] fs = data.GetFormats();
            tb.Text += $"\n数据格式：{string.Join("、", fs)}";
        }

        protected override void OnClosed(EventArgs e)
        {
            if (m_clipboardHooker == null) return;
            m_clipboardHooker.ClipboardUpdated -= OnClipboardUpdated;
            m_clipboardHooker.Dispose();
        }
    }

    class ClipboardHooker:IDisposable
    {
        const int WM_CLIPBOARDUPDATE = 0x031D;

        [DllImport("User32.dll")]
        static extern bool AddClipboardFormatListener(IntPtr hwnd);

        HwndSource _hwndSource = null;

        public void Dispose()
        {
            _hwndSource?.Dispose();
        }

        public ClipboardHooker(Window window)
        {
            WindowInteropHelper helper = new WindowInteropHelper(window);
            _hwndSource = HwndSource.FromHwnd(helper.Handle);
            bool r = AddClipboardFormatListener(_hwndSource.Handle);
            if (r)
            {
                _hwndSource.AddHook(new HwndSourceHook(OnHooked));
            }
        }

        private IntPtr OnHooked(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_CLIPBOARDUPDATE)
            {
                ClipboardUpdated?.Invoke(this, EventArgs.Empty);
                return IntPtr.Zero;
            }
            return IntPtr.Zero;
        }

        public event EventHandler ClipboardUpdated;
    }
}
