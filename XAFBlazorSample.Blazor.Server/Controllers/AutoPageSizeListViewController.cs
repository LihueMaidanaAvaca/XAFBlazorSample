using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XAFBlazorSample.Blazor.Server.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class AutoPageSizeListViewController : ViewController<ListView>
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public AutoPageSizeListViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            if (View.Editor is DxGridListEditor)
            {
                DotNetObjectReference<AutoPageSizeListViewController> controllerReference = DotNetObjectReference.Create(this);
                var JSRuntime = ((BlazorApplication)Application).ServiceProvider.GetRequiredService<IJSRuntime>();
                JSRuntime.InvokeVoidAsync("WaitForElement", System.Threading.CancellationToken.None, View.Id.ToLower(), controllerReference);
            }
        }
        [JSInvokable]
        public void WaitForElementCallback(WaitForElementResult r)
        {
            if (r == null) return;  // Shouldn't ever happen

            var correctHeight = Math.Min(r.windowHeight, r.viewContainerHeight);
            var correctPageSize = (int)Math.Floor(correctHeight / r.averageRowHeight) - 3; //  3 is hacked adjustment

            // Only change if page size difference is greater than 1 more or less than it was
            if (Math.Abs(correctPageSize - gridAdapter.GridModel.PageSize) > 1)
            {
                gridAdapter.GridModel.PageSize = correctPageSize - r.listviewAutoRowCount;
            }
        }
        public IDxGridAdapter gridAdapter { get; set; }
        protected override async void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            DxGridListEditor listEditor = ((ListView)View).Editor as DxGridListEditor;
            if (listEditor != null) gridAdapter = ((DxGridAdapter)listEditor.GetGridAdapter());
        }
        public class WaitForElementResult
        {
            public decimal viewContainerHeight { get; set; }
            public decimal averageRowHeight { get; set; }
            public decimal gridHeight { get; set; }
            public decimal windowHeight { get; set; }
            public int listviewAutoRowCount { get; set; }
        }
        protected override void OnDeactivated()
        {
            if (View.Editor is DxGridListEditor)
            {
                var JSRuntime = ((BlazorApplication)Application).ServiceProvider.GetRequiredService<IJSRuntime>();
                JSRuntime.InvokeVoidAsync("RemoveWaitForElement", System.Threading.CancellationToken.None, View.Id.ToLower());
            }
            base.OnDeactivated();
        }
    }
}
