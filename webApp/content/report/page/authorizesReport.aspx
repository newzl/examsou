<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="authorizesReport.aspx.cs" Inherits="webApp.content.report.page.authorizesReport" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8;IE=EmulateIE9" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>打印预览</title>
    <style type="text/css">html, body {margin: 0;height: 100%;}</style>
    <script src="../CreateControl.js"></script>
    <script>
        function window_onload() {
            ReportViewer.Report.OnExportBegin = function (Object) {
                var Report = ReportViewer.Report;
                if (Object.ExportType == 1) {
                    var E2XLSOption = Object.AsE2XLSOption;
                    E2XLSOption.OnlyExportDetailGrid = false;
                    E2XLSOption.SupressEmptyLines = false;
                    E2XLSOption.SameAsPrint = false;
                    E2XLSOption.ExportPageHeaderFooter = false;
                    E2XLSOption.ExportPageBreak = false;
                }
            };
            ReportViewer.Start();
        }
    </script>
</head>
<body onload="window_onload()">
    <script type="text/javascript">
        var Report = '/content/report/grf/authorizes.grf';
        var Data = '/content/report/data/authorizesData.aspx' + window.location.search;
        CreatePrintViewerEx("100%", "100%", Report, Data, true, "");
    </script>
</body>
</html>
