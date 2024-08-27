using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAFDashboardListviewTest.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class TestClass2: XPBaseObject
    {

        public TestClass2()
        {

        }

        public TestClass2(Session session) : base(session)
        {
        }
        Guid fOid;
        [Key(true)]
        [ColumnDbDefaultValue("(newid())")]
        public Guid Oid
        {
            get { return fOid; }
            set { SetPropertyValue<Guid>(nameof(Oid), ref fOid, value); }
        }

        string fFirstName;
        [Size(40)]
        public string FirstName
        {
            get { return fFirstName; }
            set { SetPropertyValue<string>(nameof(FirstName), ref fFirstName, value); }
        }
        string fLastName;
        [Size(80)]
        public string LastName
        {
            get { return fLastName; }
            set { SetPropertyValue<string>(nameof(LastName), ref fLastName, value); }
        }
    }
}
