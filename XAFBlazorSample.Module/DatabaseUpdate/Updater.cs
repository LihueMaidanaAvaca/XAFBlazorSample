using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using XAFDashboardListviewTest.Module.BusinessObjects;

namespace XAFBlazorSample.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema()
    {
        base.UpdateDatabaseAfterUpdateSchema();

        // Create test data for paging simulation
        string firstname = "John";
        string lastname = "Smith";
        for (int i = 1; i <= 200; i++)
        {
            string lastnameX = string.Format("{0} {1}", lastname, i.ToString().PadLeft(3, '0'));
            TestClass1 tc = ObjectSpace.FirstOrDefault<TestClass1>(t => lastnameX == t.LastName);
            if (tc is null)
            {
                tc = ObjectSpace.CreateObject<TestClass1>();
                tc.FirstName = firstname;
                tc.LastName = lastnameX;
            }
        }

        ObjectSpace.CommitChanges();
    }
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
        //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
        //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
        //}
    }
}
