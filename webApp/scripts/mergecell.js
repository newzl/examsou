//函数说明：合并指定表格（表格id为tableId）指定列（列数为tableColnum）的相同文本的相邻单元格
//参数说明：tableId 为需要进行合并单元格的表格的id。如在HTMl中指定表格 id="data" ，此参数应为 #data 
//参数说明：tableColnum 为需要合并单元格的所在列。为数字，从最左边第一列为1开始算起。
function rowspan(tableId, tableColnum) {
    firsttd = "";
    currenttd = "";
    SpanNum = 0;
    tableObj = $(tableId + " tr td:nth-child(" + tableColnum + ")");
    tableObj.each(function (i) {
        if (i == 0) {
            firsttd = $(this);
            SpanNum = 1;
        } else {
            currenttd = $(this);
            if (firsttd.text() == currenttd.text()) {
                SpanNum++;
                currenttd.hide(); //remove();
                firsttd.attr("rowSpan", SpanNum);
            } else {
                firsttd = $(this);
                SpanNum = 1;
            }
        }
    });
}
//函数说明：合并指定表格（表格id为tableId）指定行（行数为_w_table_rownum）的相同文本的相邻单元格
//参数说明：tableId 为需要进行合并单元格的表格id。如在HTMl中指定表格 id="data" ，此参数应为 #data 
//参数说明：_w_table_rownum 为需要合并单元格的所在行。其参数形式请参考jQuery中nth-child的参数。
//          如果为数字，则从最左边第一行为1开始算起。
//          "even" 表示偶数行
//          "odd" 表示奇数行
//          "3n+1" 表示的行数为1、4、7、10.......
//参数说明：maxcolnum 为指定行中单元格对应的最大列数，列数大于这个数值的单元格将不进行比较合并。
//          此参数可以为空，为空则指定行的所有单元格要进行比较合并。
function _w_table_colspan(tableId, _w_table_rownum, maxcolnum) {
    if (maxcolnum == void 0) { maxcolnum = 0; }
    firsttd = "";
    currenttd = "";
    SpanNum = 0;
    $(tableId + " tr:nth-child(" + _w_table_rownum + ")").each(function (i) {
        tableObj = $(this).children();
        tableObj.each(function (i) {
            if (i == 0) {
                firsttd = $(this);
                SpanNum = 1;
            } else if ((maxcolnum > 0) && (i > maxcolnum)) {
                return "";
            } else {
                currenttd = $(this);
                if (firsttd.text() == currenttd.text()) {
                    SpanNum++;
                    currenttd.hide(); //remove();
                    firsttd.attr("colSpan", SpanNum);
                } else {
                    firsttd = $(this);
                    SpanNum = 1;
                }
            }
        });
    });
}
