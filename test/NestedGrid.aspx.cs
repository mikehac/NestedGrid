using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NessTest
{
    public partial class NestedGrid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["GetParrents"]))
            {
                Response.Write(new JavaScriptSerializer().Serialize(GetParrents()));
                Response.End();
            }

            if (!string.IsNullOrEmpty(Request["ParrentId"]))
            {
                Response.Write(new JavaScriptSerializer().Serialize(GetChildrenByID(int.Parse(Request["ParrentId"]))));
                Response.End();
            }
        }

        ParrentNode[] GetParrents()
        {
            List<ParrentNode> pd = new List<ParrentNode>();
            pd.Add(new ParrentNode()
            {
                PropertyId = 1,
                PropertyName = "עו''ש",
                Total = 22.15f
            });
            
            pd.Add(new ParrentNode()
            {
                PropertyId = 2,
                PropertyName = "חסכונות",
                Total = 41.25f
            }); pd.Add(new ParrentNode()
            
            {
                PropertyId = 3,
                PropertyName = "פקדונות",
                Total = 14.29f
            }); 
            
            return pd.ToArray();
        }

        ChildNode1[] GetChildrenByID(int parentID)
        {
            List<ChildNode1> cn = new List<ChildNode1>();
            for (int i = 0; i < 5; i++)
            {
                cn.Add(new ChildNode1()
                {
                    Id = i,
                    ParentPropertyId = 1,
                    Name = "חשבון 123",
                    Total = i * 3.14f
                });
            }

            for (int i = 5; i < 10; i++)
            {
                cn.Add(new ChildNode1()
                {
                    Id = i,
                    ParentPropertyId = 2,
                    Name = "חשבון 957",
                    Total = i * 3.14f
                });
            }

            for (int i = 10; i < 15; i++)
            {
                cn.Add(new ChildNode1()
                {
                    Id = i,
                    ParentPropertyId = 3,
                    Name = "חשבון 348",
                    Total = i * 3.14f
                });
            }


            return cn.Where(r => r.ParentPropertyId == parentID).ToArray();
        }
    }

    public class ParrentNode
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public float Total { get; set; }
    }

    public class ChildNode1
    {
        public int ParentPropertyId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public float Total { get; set; }
    }
}