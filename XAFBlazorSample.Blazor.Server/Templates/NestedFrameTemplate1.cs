using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Templates;
using DevExpress.ExpressApp.Blazor.Templates.Toolbar.ActionControls;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Templates.ActionControls;
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace XAFBlazorSample.Blazor.Server.Templates
{
    public class NestedFrameTemplate1 : FrameTemplate, ISupportActionsToolbarVisibility, ISelectionDependencyToolbar
    {
        public NestedFrameTemplate1()
        {
            IsActionsToolbarVisible = true;
            Toolbar = new DxToolbarAdapter(new DxToolbarModel());
            Toolbar.AddActionContainer(nameof(PredefinedCategory.ObjectsCreation));
            Toolbar.AddActionContainer("Link");
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Edit));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.RecordEdit));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.View));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Reports));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Diagnostic));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Filters));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.FullTextSearch));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Export));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Unspecified));
        }
        protected override IEnumerable<IActionControlContainer> GetActionControlContainers() => Toolbar.ActionContainers;
        protected override RenderFragment CreateComponent() => NestedFrameTemplate1Component.Create(this);
        protected override void BeginUpdate()
        {
            base.BeginUpdate();
            ((ISupportUpdate)Toolbar).BeginUpdate();
        }
        protected override void EndUpdate()
        {
            ((ISupportUpdate)Toolbar).EndUpdate();
            base.EndUpdate();
        }
        public bool IsActionsToolbarVisible { get; private set; }
        public DxToolbarAdapter Toolbar { get; }
        void ISupportActionsToolbarVisibility.SetVisible(bool isVisible) => IsActionsToolbarVisible = isVisible;
    }
}
