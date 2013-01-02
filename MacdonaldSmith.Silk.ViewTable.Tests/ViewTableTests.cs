using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace MacdonaldSmith.Silk.ViewTable.Tests
{
    [TestFixture]
    public class ViewTableTests
    {
        [Test]
        public void can_add_column()
        {
            string columnName = "string column";
            IViewTable viewTable = new ViewTable(1);
            viewTable.AddColumn<string>("column name", "default");

            Assert.AreEqual(1, viewTable.ColumnCount);
            Assert.AreEqual(1, viewTable.RowCount);
            Assert.AreEqual("default", viewTable.GetValue<string>(0, columnName));

            viewTable.Update(0, columnName, "new string value");

            Assert.AreEqual("new string value", viewTable.GetValue<string>(0, columnName));
        }
    }
}
