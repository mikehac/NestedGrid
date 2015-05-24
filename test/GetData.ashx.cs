using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace NessTest
{
    /// <summary>
    /// Summary description for GetData
    /// </summary>
    public class GetData : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            string rt = context.Request["rt"];
            switch (rt)
            {
                case "grid":
                    context.Response.Write(FillGridData());
                    return;
                case "chart":
                    context.Response.Write(FillChartData());
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

        private string FillGridData()
        {
            List<GridItem> gi = new List<GridItem>();
            gi.Add(new GridItem() { Index = "ת\"א25", LastExchange = 1458.25f, DailyDiff = 0.02f, PeriodDiff = 0.02f });
            gi.Add(new GridItem() { Index = "ת\"א100", LastExchange = 1740.07f, DailyDiff = -0.02f, PeriodDiff = -0.02f });
            gi.Add(new GridItem() { Index = "ת\"א בנקים", LastExchange = 850.2f, DailyDiff = 0.72f, PeriodDiff = 0.72f });
            gi.Add(new GridItem() { Index = "יתר מאגר", LastExchange = 630.5f, DailyDiff = 0.75f, PeriodDiff = 0.75f });
            gi.Add(new GridItem() { Index = "מדד תל בונד 20", LastExchange = 1740.07f, DailyDiff = -0.02f, PeriodDiff = -0.02f });

            GridData data = new GridData();
            for (int i = 0; i < gi.Count; i++)
            {
                data.DataContainer.Add(gi[i]);
            }

            //System.Web.Script.Serialization.JavaScriptSerializer()
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(data);
        }

        private string FillChartData()
        {
            List<ChartSingleItem> chi = new List<ChartSingleItem>();
            //chi.Add(new ChartSingleItem() { Name = "ת\"א25", Values = new float[] { 0.23f, 0.23f, -0.15f, -0.12f, 0.75f } });
            //chi.Add(new ChartSingleItem() { Name = "ת\"א100", Values = new float[] { -0.2f, -0.17f, 0.2f, 0.3f, 0.24f } });
            //chi.Add(new ChartSingleItem() { Name = "ת\"א בנקים", Values = new float[] { -0.3f, -0.28f, -0.35f, -0.42f, -0.45f } });
            //chi.Add(new ChartSingleItem() { Name = "יתר מאגר", Values = new float[] { -0.38f, -0.3f, -0.35f, -0.4f, -0.45f } });
            //chi.Add(new ChartSingleItem() { Name = "מדד תל בונד 20", Values = new float[] { -0.5f, -0.43f, -0.35f, -0.2f, -0.45f } });
            ChartData data = new ChartData();

            ChartSingleItem singleItem = new ChartSingleItem();
            singleItem.Values.Add("11:50", 0.23f);
            singleItem.Values.Add("13:00", 0.23f);
            singleItem.Values.Add("14:15", -0.15f);
            singleItem.Values.Add("16:10", -0.12f);
            singleItem.Values.Add("17:40", 0.75f);
            singleItem.Name = "ת\"א25";
            chi.Add(singleItem);

            singleItem = new ChartSingleItem();
            singleItem.Values.Add("11:50", -0.2f);
            singleItem.Values.Add("13:00", -0.17f);
            singleItem.Values.Add("14:15", -0.2f);
            singleItem.Values.Add("16:10", 0.3f);
            singleItem.Values.Add("17:40", 0.24f);
            singleItem.Name = "ת\"א100";
            chi.Add(singleItem);

            singleItem = new ChartSingleItem();
            singleItem.Values.Add("11:50", -0.3f);
            singleItem.Values.Add("13:00", -0.28f);
            singleItem.Values.Add("14:15", -0.35f);
            singleItem.Values.Add("16:10", -0.42f);
            singleItem.Values.Add("17:40", -0.45f);
            singleItem.Name = "ת\"א בנקים";
            chi.Add(singleItem);

            singleItem = new ChartSingleItem();
            singleItem.Values.Add("11:50", -0.38f);
            singleItem.Values.Add("13:00", -0.3f);
            singleItem.Values.Add("14:15", -0.35f);
            singleItem.Values.Add("16:10", -0.4f);
            singleItem.Values.Add("17:40", -0.45f);
            singleItem.Name = "יתר מאגר";
            chi.Add(singleItem);

            singleItem = new ChartSingleItem();
            singleItem.Values.Add("11:50", -0.5f);
            singleItem.Values.Add("13:00", -0.43f);
            singleItem.Values.Add("14:15", -0.35f);
            singleItem.Values.Add("16:10", -0.2f);
            singleItem.Values.Add("17:40", -0.45f);
            singleItem.Name = "מדד תל בונד 20";
            chi.Add(singleItem);


            foreach (ChartSingleItem item in chi)
            {
                data.DataContainer.Add(item);
            }

            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(data);
        }
    }

    #region Grid Data
    public class GridData
    {
        public List<GridItem> DataContainer { get; set; }
        public GridData()
        {
            DataContainer = new List<GridItem>();
        }
    }

    public class GridItem
    {
        public string Index { get; set; }
        public float LastExchange { get; set; }
        public float DailyDiff { get; set; }
        public float PeriodDiff { get; set; }
    }
    #endregion
    #region Chart Data
    public class ChartData
    {
        public List<ChartSingleItem> DataContainer { get; set; }
        public ChartData()
        {
            DataContainer = new List<ChartSingleItem>();
        }
    }
    public class ChartSingleItem
    {
        public ChartSingleItem()
        {
            Values = new Dictionary<string, float>();
        }

        public string Name { get; set; }
        public Dictionary<string, float> Values { get; set; }
    }
    #endregion
}