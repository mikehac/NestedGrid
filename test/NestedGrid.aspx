<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NestedGrid.aspx.cs" Inherits="NessTest.NestedGrid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.11.2.min.js"></script>
    <script src="js/kendo/js/kendo.all.min.js" type="text/javascript"></script>

    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="Content/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" href="js/kendo/styles/kendo.common.min.css" type="text/css" />
    <link rel="Stylesheet" href="js/kendo/styles/kendo.bootstrap.min.css" type="text/css" />
    <link rel="Stylesheet" href="js/kendo/styles/kendo.dataviz.min.css" type="text/css" />
    <link rel="Stylesheet" href="js/kendo/styles/kendo.dataviz.bootstrap.min.css" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            var element = $("#grid").kendoGrid({
                dataSource: {
                    type: 'get',
                    dataType: "json",
                    transport: {
                        read: "NestedGrid.aspx",
                        data: {
                            'GetParrents': true
                        }
                    },
                    pageSize: 6,
                    serverPaging: true,
                    serverSorting: true
                },
                height: 600,
                sortable: true,
                pageable: true,
                //detailInit: detailInit,
                dataBound: function() {
                    this.expandRow(this.tbody.find("tr.k-master-row").first());
                },
                columns: [
                    {
                        field: "Name",
                        title: "סוג חשבון",
                        width: "110px"
                    },
                    {
                        field: "",
                        title: "שדה 1",
                        width: "110px"
                    },
                    {
                        field: "",
                        title: "",
                        width: "110px"
                    },
                    {
                        field: "Total",
                        title: "סכום",
                        width: "110px"
                    },
                    {
                        field: "Title"
                    }
                ]
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" dir="rtl">
        <div id="grid"></div>
    </form>
</body>
</html>
