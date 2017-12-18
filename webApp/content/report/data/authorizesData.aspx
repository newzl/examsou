<%@ Page Language="C#" %>

<script runat="server"> 
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("valid=1 and companyID=" + Request.QueryString["companyid"]);
        if (Request.QueryString["ename"] != null)
        {
            webApp.Areas.admin.Controllers.employeeListFindwhere m = new webApp.Areas.admin.Controllers.employeeListFindwhere()
            {
                ename = Request.QueryString["ename"],
                sex = Convert.ToInt32(Request.QueryString["sex"]),
                idcard = Request.QueryString["idcard"],
                bm = Convert.ToInt32(Request.QueryString["bm"]),
                ks = Convert.ToInt32(Request.QueryString["ks"]),
                jb = Convert.ToInt32(Request.QueryString["jb"]),
                zc = Convert.ToInt32(Request.QueryString["zc"]),
                minreg = Request.QueryString["minreg"],
                maxreg = Request.QueryString["maxreg"],
                state = Convert.ToInt32(Request.QueryString["state"])
            };
            if (m.ename != "") sb.Append(" and name like '%" + m.ename.Trim() + "%'");
            if (m.sex != -1) sb.Append(" and sexID=" + m.sex);
            if (m.idcard != "") sb.Append(" and idcard like '%" + m.idcard.Trim() + "%'");
            if (m.bm != -1) sb.Append(" and departmentID=" + m.bm);
            if (m.ks != -1) sb.Append(" and officesID=" + m.ks);
            if (m.jb != -1) sb.Append(" and levelID=" + m.jb);
            if (m.zc != -1) sb.Append(" and jobInfoID=" + m.zc);
            if (m.minreg != "") sb.Append(" and registerTime>='" + m.minreg.Trim() + "'");
            if (m.maxreg != "") sb.Append(" and registerTime<='" + m.maxreg.Trim() + " 23:59:59'");
            if (m.state != -1) sb.Append(" and state=" + m.state);
        }
        string QuerySQL = "select name,sex,idcard,phone,registerTime,department,offices,[level],jobInfo,[state] from v_employeeList where " + sb.ToString() + " order by applyTime desc,registerTime desc";
        SqlJsonReportData.GenOneRecordset(this, QuerySQL);
    }
</script>
