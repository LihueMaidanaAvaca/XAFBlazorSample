using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Xpo;
using XAFBlazorSample.Blazor.Server.Templates;
using XAFBlazorSample.Blazor.Server.Services;

namespace XAFBlazorSample.Blazor.Server;

public class XAFBlazorSampleBlazorApplication : BlazorApplication {
    public XAFBlazorSampleBlazorApplication() {
        ApplicationName = "XAFBlazorSample";
        CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
        DatabaseVersionMismatch += XAFBlazorSampleBlazorApplication_DatabaseVersionMismatch;
    }
    protected override void OnSetupStarted() {
        base.OnSetupStarted();
#if DEBUG
        if(System.Diagnostics.Debugger.IsAttached && CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
            DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
        }
#endif
    }
    private void XAFBlazorSampleBlazorApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
        e.Updater.Update();
        e.Handled = true;
#else
        if(System.Diagnostics.Debugger.IsAttached) {
            e.Updater.Update();
            e.Handled = true;
        }
        else {
            string message = "The application cannot connect to the specified database, " +
                "because the database doesn't exist, its version is older " +
                "than that of the application or its schema does not match " +
                "the ORM data model structure. To avoid this error, use one " +
                "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

            if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
                message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
            }
            throw new InvalidOperationException(message);
        }
#endif
    }

    protected override IFrameTemplate CreateDefaultTemplate(TemplateContext context)
    {
        if (context == TemplateContext.ApplicationWindow)
        {
            return new ApplicationWindowTemplate1();
        }
        else if (context == TemplateContext.PopupWindow || context == TemplateContext.LookupWindow)
        {
            return new PopupWindowTemplate1();
        }
        else if (context == TemplateContext.NestedFrame)
        {
            return new NestedFrameTemplate1();
        }
        return base.CreateDefaultTemplate(context);
    }
}
