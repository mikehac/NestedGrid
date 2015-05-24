using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace test
{
    /// <summary>
    /// Summary description for GetNestedGridData
    /// </summary>
    public class GetNestedGridData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string rt = context.Request["rt"];
            switch (rt)
            {
                case "GetParrents":
                    context.Response.Write(new JavaScriptSerializer().Serialize(GetParrents()));
                    return;

                case "GetChildren":
                    context.Response.Write(new JavaScriptSerializer().Serialize(GetChildrenByID(int.Parse(context.Request["ParrentId"]))));
                    return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
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