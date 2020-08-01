using CefSharp;
using CefSharp.ModelBinding;
using CefSharp.WinForms;
using DataSpider.Handler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSpider
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 实例化浏览器对象
        /// </summary>
        public ChromiumWebBrowser CWBrowser;

        public MainForm()
        {
            InitializeComponent();
            InitBrowser();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.F12)
            {
                CWBrowser.ShowDevTools();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region 初始化浏览器
        /// <summary>
        /// 初始化浏览器
        /// </summary>
        public void InitBrowser()
        {
            var settings = new CefSettings();
            settings.CachePath = AppDomain.CurrentDomain.BaseDirectory + "cache";
            settings.PersistSessionCookies = true;
            settings.WindowlessRenderingEnabled = true;
            settings.Locale = "zh.CN";
            settings.CefCommandLineArgs.Add("disable-web-security", "1");
            settings.CefCommandLineArgs.Add("disable-gpu", "1");

            settings.CefCommandLineArgs.Add("disable-gpu-compositing", "1");

            //加载一个指定版本的ppapi（flash插件）
            settings.CefCommandLineArgs.Add("ppapi-flash-path", AppDomain.CurrentDomain.BaseDirectory.ToString() + @"PepperFlash\pepflashplayer.dll");
            settings.CefCommandLineArgs.Add("ppapi-flash-version", "20.0.0.286");

            Cef.Initialize(settings);
            if (CefSharpSettings.ShutdownOnExit)
            {
                Application.ApplicationExit += OnApplicationExit;
            }
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            //控件对象创建
            CWBrowser = new ChromiumWebBrowser("http://www.baidu.com");

            CWBrowser.LifeSpanHandler = new LifeSpanHandler();//包含连接打开方式
            CWBrowser.JsDialogHandler = new JsDialogHandler();//设置可以弹出js弹框

            //控件添加到窗体
            this.Controls.Add(CWBrowser);
            CWBrowser.Dock = DockStyle.Fill;
            //控件创建事件
            CWBrowser.LoadingStateChanged += CWBrowser_LoadingStateChanged;
            CWBrowser.LoadError += CWBrowser_LoadError;

            BindingOptions bindingOption = new BindingOptions { CamelCaseJavascriptNames = false, Binder = new DefaultBinder() };
            //绑定JS对象
            
        }

        private void CWBrowser_LoadError(object sender, LoadErrorEventArgs e)
        {
            
        }

        private void CWBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            Cef.Shutdown();
        }

        #endregion
    }
}
