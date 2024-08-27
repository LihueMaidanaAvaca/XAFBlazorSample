using DevExpress.Blazor;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Templates;
using DevExpress.ExpressApp.Blazor.Templates.Toolbar.ActionControls;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Templates.ActionControls;
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace XAFBlazorSample.Blazor.Server.Templates
{
    public class PopupWindowTemplate1 : WindowTemplateBase, ISupportActionsToolbarVisibility, IPopupWindowTemplateSize
    {
        public PopupWindowTemplate1()
        {
            IsActionsToolbarVisible = true;
            Toolbar = new DxToolbarAdapter(new DxToolbarModel());
            Toolbar.AddActionContainer(nameof(PredefinedCategory.ObjectsCreation));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Search), ToolbarItemAlignment.Right);
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Filters), ToolbarItemAlignment.Right);
            Toolbar.AddActionContainer(nameof(PredefinedCategory.FullTextSearch), ToolbarItemAlignment.Right);

            BottomToolbar = new DxToolbarAdapter(new DxToolbarModel());
            BottomToolbar.AddActionContainer(nameof(PredefinedCategory.Diagnostic), ToolbarItemAlignment.Right);
            BottomToolbar.AddActionContainer(DialogController.DialogActionContainerName, ToolbarItemAlignment.Right);
            BottomToolbar.ToolbarModel.SizeMode = SizeMode.Large;
        }
        protected override IEnumerable<IActionControlContainer> GetActionControlContainers() =>
            Toolbar.ActionContainers.Concat(BottomToolbar.ActionContainers);
        protected override RenderFragment CreateComponent() => PopupWindowTemplate1Component.Create(this);

        public bool IsActionsToolbarVisible { get; private set; }
        public DxToolbarAdapter Toolbar { get; }
        public DxToolbarAdapter BottomToolbar { get; }
        public string MinWidth { get; set; }
        public string MinHeight { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string MaxWidth { get; set; }
        public string MaxHeight { get; set; }

        void ISupportActionsToolbarVisibility.SetVisible(bool isVisible) => IsActionsToolbarVisible = isVisible;
    }
}
