<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NessTest.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.11.2.min.js"></script>
    <script src="js/kendo/js/kendo.all.min.js" type="text/javascript"></script>

<%--    <script src="Scripts/moment.js" type="text/javascript"></script>
    <script src="Scripts/moment-with-locales.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>--%>
    <script src="Scripts/moment-with-locales.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <script src="js/modernizr.custom.57657.js" type="text/javascript"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="Content/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" href="js/kendo/styles/kendo.common.min.css" type="text/css" />
    <link rel="Stylesheet" href="js/kendo/styles/kendo.bootstrap.min.css" type="text/css" />
    <link rel="Stylesheet" href="js/kendo/styles/kendo.dataviz.min.css" type="text/css" />
    <link rel="Stylesheet" href="js/kendo/styles/kendo.dataviz.bootstrap.min.css" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#datetimepicker1').datetimepicker();
            //var data = [];
            
            //for (var i = 0; i < 5; i++) {
            //    var item = {
            //        name: 'My item' + i.toString(),
            //        price: Math.floor(Math.random() * 100) + 1,
            //        asin: Math.floor(Math.random() * 1000) + i
            //    };

            //    data.push(item);
            //}

            //$('#grid1').kendoGrid({
            //    dataSource: data,
            //    columns: [
            //        { field: "name", title: "Name", width: "140px" },
            //        { field: "price", title: "price", width: "140px" },
            //        { field: "asin", title: "ASIN", width: "140px" }
            //    ]
            //});
            //////////////////////////////////////
            var times = [];
            var series = [];
            $('input[type="button"]').click(function () {
                show();
            });
            var show = function () {
                var gridDs = new kendo.data.DataSource({
                    transport: {
                        read: {
                            url: 'GetData.ashx?rt=grid',
                            dataType: 'json'
                        }
                    },
                    schema: {
                        model: {
                            fields: {
                                Index: { type: 'string' },
                                LastExchange: { type: 'number' },
                                DailyDiff: { type: 'number' },
                                PeriodDiff: { type: 'number' }
                            }
                        },
                        parse: function (resp) {
                            var ret = [];
                            $.each(resp.DataContainer, function (index, item) {
                                ret.push(item);
                            });
                            return ret;
                        }
                    }
                });

                $('#grid').kendoGrid({
                    dataSource: gridDs,
                    columns: [
                        { field: "PeriodDiff", title: "שינוי לתקופה נבחרת%", width: "140px" },
                        { field: "DailyDiff", title: "שינוי יומי%", width: "140px" },
                        { field: "LastExchange", title: "שער אחרון", width: "140px" },
                        { field: "Index", title: "מדד", width: "140px" }
                    ]
                });

                var chartDS = new kendo.data.DataSource({
                    transport: {
                        read: {
                            url: 'GetData.ashx?rt=chart',
                            dataType: 'json'
                        }
                    },
                    schema: {
                        //model: {
                        //    fields: {
                        //        Index: { type: 'string' },
                        //        LastExchange: { type: 'number' },
                        //        DailyDiff: { type: 'number' },
                        //        PeriodDiff: { type: 'number' }
                        //    }
                        //},
                        parse: function (resp) {
                            times = [];
                            for (var x in resp.DataContainer[0].Values) {
                                times.push(x);
                            }

                            var ret = [];
                            $.each(resp.DataContainer, function (index, item) {
                                var newItem = {
                                    Name: item.Name,
                                    data: []
                                };

                                for (var x in item.Values) {
                                    newItem.data.push(item.Values[x]);
                                }

                                ret.push(newItem);
                            });
                            console.log(times);
                            series = ret;
                            console.log(ret);
                            return ret;
                        }
                    }
                });
                $("#chart").kendoChart({
                    dataSource: chartDS,
                    legend: {
                        visible: true,
                        position: "bottom"
                    },
                    seriesDefaults: {
                        type: "line",
                        style: "smooth"
                    },
                    series: series,
                    valueAxis: {
                        labels: {
                            format: "{0}%"
                        },
                        line: {
                            visible: true
                        },
                        axisCrossingValue: -10
                    },
                    categoryAxis: {
                        categories: times
                    },
                    tooltip: {
                        visible: true,
                        format: "{0}%",
                        template: "#= series.Name #: #= value #"
                    }
                });
            }

            var start = $("#from").kendoDatePicker({
                change: startChange
            }).data("kendoDatePicker");

            var end = $("#to").kendoDatePicker({
                change: endChange
            }).data("kendoDatePicker");


            function startChange() {
                var startDate = start.value(),
                endDate = end.value();

                if (startDate) {
                    startDate = new Date(startDate);
                    startDate.setDate(startDate.getDate());
                    end.min(startDate);
                } else if (endDate) {
                    start.max(new Date(endDate));
                } else {
                    endDate = new Date();
                    start.max(endDate);
                    end.min(endDate);
                }
            }

            function endChange() {
                var endDate = end.value(),
                startDate = start.value();

                if (endDate) {
                    endDate = new Date(endDate);
                    endDate.setDate(endDate.getDate());
                    start.max(endDate);
                } else if (startDate) {
                    end.min(new Date(startDate));
                } else {
                    endDate = new Date();
                    start.max(endDate);
                    end.min(endDate);
                }
            }
            $('#periodSelector').kendoDropDownList({
                dataSource:[
                        { text: "יומי", value: "1" },
                        { text: "שבועי", value: "2" },
                        { text: "חודשי", value: "3" }
                ],
                dataTextField: "text",
                dataValueField: "value"
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-group">
            <div class="input-group date" style="width:200px;" id="datetimepicker1">
                <input type="text" class="form-control" />	<span class="input-group-addon"><span class="glyphicon-calendar glyphicon"></span></span>
            </div>
        </div>
        <div style="margin: auto; border: 1px solid; width: 1024px;">
            <input type="button" value="Show" />
            <div class="demo-section k-header" style="background-color:#E1E1E1;direction:rtl;">
                <span>הצג תקופה</span>
                <div id="periodSelector"></div>
                <span>תאריך מ:</span>
                <div class="k-rtl">
                    <input id="from" style="width:200px;" />
                </div>
                <span>עד</span>
                <div class="k-rtl">
                    <input id="to" style="width: 200px;" />
                </div>
            </div>
            <div id="chart" style="width: 800px;"></div>
            <div id="grid" class="k-rtl" style="width: 800px;"></div>
        </div>
    </form>
</body>
</html>
