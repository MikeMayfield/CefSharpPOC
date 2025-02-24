// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.WinForms;
using System;
using System.IO;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public static class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
#if ANYCPU
            CefRuntime.SubscribeAnyCpuAssemblyResolver();
#endif

            var settings = new CefSettings();

            // Critical CSP-related configuration
            settings.CefCommandLineArgs.Add("disable-web-security");
            settings.CefCommandLineArgs.Add("allow-running-insecure-content", "1");
            settings.CefCommandLineArgs.Add("disable-site-isolation-trials");  // Helps with some security policy conflicts

            // Existing media configuration
            settings.CefCommandLineArgs.Add("enable-media-stream");
            settings.CefCommandLineArgs.Add("use-fake-ui-for-media-stream");
            settings.CefCommandLineArgs.Add("enable-usermedia-screen-capturing");
            settings.CefCommandLineArgs.Add("disable-plugins-discovery", "1");
            settings.CefCommandLineArgs.Add("disable-extensions", "1");

            // Enhanced security relaxation for modern web apps
            settings.CefCommandLineArgs.Add("disable-features", "CrossSiteDocumentBlockingAlways,IsolateOrigins,site-per-process");

            // Initialize Cef
            var initialized = Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

            if (!initialized)
            {
                MessageBox.Show("Cef.Initialized failed, check the log file for more details.");
                return 0;
            }

            Application.EnableVisualStyles();
            Application.Run(new BrowserForm());

            return 0;
        }
    }
}