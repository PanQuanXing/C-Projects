using LanguageLocalization;
using ProjectManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace HerbalAnalysis
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class PanQuanXingApp : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {

        }

        private void Application_Activated(object sender, EventArgs e)
        {
            string extension = ".pqxProj";
            string title = "HerbalAnalysis";
            string extensionDescription = I18N.GetString("A Project File For Herbal Analysis");
            FileRegistrationHelper.SetFileAssociation(extension,title+".".ToString()+extensionDescription);
        }
    }
}
