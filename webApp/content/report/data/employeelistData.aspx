<%@ Page Language="C#" %>

<script runat="server"> 
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("valid=1 and companyID=" + Request.QueryString["companyid"]);
        if (Request.QueryString["ename"] != null)
        {
            webApp.Areas.admin.Controllers.employeeListDataFindwhere m = new webApp.Areas.admin.Controllers.employeeListDataFindwhere()
            {
                ename = Request.QueryString["ename"],
                sex = Convert.ToInt32(Request.QueryString["sex"]),
                idcard = Request.QueryString["idcard"],
                phone = Request.QueryString["phone"],
                bm = Convert.ToInt32(Request.QueryString["bm"]),
                ks = Convert.ToInt32(Request.QueryString["ks"]),
                jb = Convert.ToInt32(Request.QueryString["jb"]),
                zc = Convert.ToInt32(Request.QueryString["zc"]),
                minlogin = Request.QueryString["minlogin"],
                maxlogin = Request.QueryString["maxlogin"],
                minreg = Request.QueryString["minreg"],
                maxreg = Request.QueryString["maxreg"],
                minlearns = Request.QueryString["minlearns"],
                maxlearns = Request.QueryString["maxlearns"],
                minexam = Request.QueryString["minexam"],
                maxexam = Request.QueryString["maxexam"],
                state = Convert.ToInt32(Request.QueryString["state"])
            };
            if (m.ename != "") sb.Append(" and name like '%" + m.ename.Trim() + "%'");
            if (m.sex != -1) sb.Append(" and sexID=" + m.sex);
            if (m.idcard != "") sb.Append(" and idcard like '%" + m.idcard.Trim() + "%'");
            if (m.phone != "") sb.Append(" and phone like '%" + m.phone.Trim() + "%'");
            if (m.bm != -1) sb.Append(" and departmentID=" + m.bm);
            if (m.ks != -1) sb.Append(" and officesID=" + m.ks);
            if (m.jb != -1) sb.Append(" and levelID=" + m.jb);
            if (m.zc != -1) sb.Append(" and jobInfoID=" + m.zc);
            if (m.minlogin != "") sb.Append(" and lastLoginTime>='" + m.minlogin.Trim() + "'");
            if (m.maxlogin != "") sb.Append(" and lastLoginTime<='" + m.maxlogin.Trim() + " 23:59:59'");
            if (m.minreg != "") sb.Append(" and registerTime>='" + m.minreg.Trim() + "'");
            if (m.maxreg != "") sb.Append(" and registerTime<='" + m.maxreg.Trim() + " 23:59:59'");
            if (m.minlearns != "") sb.Append(" and learns>=" + m.minlearns);
            if (m.maxlearns != "") sb.Append(" and learns<=" + m.maxlearns);
            if (m.minexam != "") sb.Append(" and exams>=" + m.minexam);
            if (m.maxexam != "") sb.Append(" and exams<=" + m.maxexam);
            if (m.state != -1) sb.Append(" and state=" + m.state);
        }
        string QuerySQL = "select name,sex,idcard,nation,degree,zzmm,department,offices,[level],jobInfo,[state],logins,learns,exams,account,phone,email,lastLoginTime,registerTime from v_employeeList where " + sb.ToString() + " order by registerTime desc";
        SqlJsonReportData.GenOneRecordset(this, QuerySQL);
    }
</script>
