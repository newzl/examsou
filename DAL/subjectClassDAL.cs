using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class subjectClassDAL : IDisposable
    {
        #region 释放资源
        bool dis;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (dis) return;
            if (disposing) dis = true;
        }
        ~subjectClassDAL() { Dispose(false); }
        #endregion

        public void save(Models.subject m)
        {
            string sql = "declare @leved int,@id int";
            sql += " select @leved=leved+1 from subjectClass where ID=@pid";
            sql += " select top(1) @id=ID+1 from subjectClass order by ID desc";
            sql += " insert into subjectClass(ID,name,pId,leved,valid,levelID,introduce) values(@id,@sname,@pid,@leved,@valid,@levelID,@introduce)";
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@sname",m.sname),
                new SqlParameter("@pid",m.pid),
                new SqlParameter("@valid",m.valid),
                new SqlParameter("@levelID",m.levelID),
                new SqlParameter("@introduce",SqlDbType.NVarChar,2000)
            };
            if (m.introduce != "" && m.introduce != null) param[4].Value = m.introduce;
            else param[4].Value = DBNull.Value;
            SqlHelper.ExcuteNonQuery(sql, param);
        }

        public Models.subject getEntity(int id)
        {
            string sql = "select id,pid,name[sname],levelID,introduce,valid from subjectClass where id=@id";
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@id",id)
            };
            return SqlHelper.ExecuteEntity<Models.subject>(sql, param);
        }
        public void update(Models.subject m)
        {
            string sql = "update subjectClass set name=@sname,introduce=@introduce,valid=@valid where id=@id";
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@id",m.id),
                new SqlParameter("@sname",m.sname),
                new SqlParameter("@valid",m.valid),
                new SqlParameter("@introduce",SqlDbType.NVarChar,2000)
            };
            if (m.introduce != "" && m.introduce != null) param[3].Value = m.introduce;
            else param[3].Value = DBNull.Value;
            SqlHelper.ExcuteNonQuery(sql, param);
        }
        public void setValid(bool valid, int id)
        {
            string sql = "update subjectClass set valid=@valid where id in (select id from f_getChildrenUn(@id))";
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@valid",valid),
                new SqlParameter("@id",id)
            };
            SqlHelper.ExcuteNonQuery(sql, param);
        }
        public void delete(int id)
        {
            string sql = "delete subjectClass where id in (select id from f_getChildrenUn(@id))";
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@id",id)
            };
            SqlHelper.ExcuteNonQuery(sql, param);
        }
        public string getName(int sid)
        {
            string sql = "select name from subjectClass where ID=@sid";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@sid",sid)
            };
            DataTable dt = SqlHelper.ExcuteDataTable(sql, param);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]["name"].ToString();
            }
            else
            {
                return null;
            }
        }
        //选择题库
        public DataTable seleSubject(string eid)
        {
            string sql = "select id,name,pid,learns from subjectClass where valid=1 and leved in(1,2) and total>0 and (levelID=0 or levelID=(select ISNULL(levelID,0) from employee where uniqueID=@eid))";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@eid",eid)
            };
            return SqlHelper.ExcuteDataTable(sql, param);
        }
        //题库详细
        public Models.subjectDetail detailEntity(int sid, string eid)
        {
            string sql = "select a.id,name,introduce,[level],a.total,learns,comments,evaluate,cast(case when b.id is null then 0 else 1 end as bit)[isLearn] from v_subjectDetail a left join learnSubject b on a.id=b.[sid] and a.id=@sid and b.eid=@eid where a.id=@sid";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@sid",sid),
                new SqlParameter("@eid",eid)
            };
            return SqlHelper.ExecuteEntity<Models.subjectDetail>(sql, param);
        }
        //查看科目系列
        public DataTable subjectTree(int sid)
        {
            string sql = "DECLARE @BOSS hierarchyid SELECT @BOSS=orgNode FROM subjectClass WHERE ID=@sid and valid=1 SELECT id,name+'('+cast(total as varchar)+'题)'[name],pid FROM subjectClass WHERE orgNode.IsDescendantOf(@BOSS)=1 and valid=1 and leved<>2 and total>0";
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@sid",sid)
            };
            return SqlHelper.ExcuteDataTable(sql, param);
        }
        //获得树数据
        public DataTable getTree(int pid)
        {
            string sql = "select id,name,cast(case when exists(select * from subjectClass where valid=1 and a.id=pId and id<>a.pId) then 1 else 0 end as bit)[isParent] from subjectClass a where valid=1 and leved<>0 and pId=@pid";
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@pid",pid)
            };
            return SqlHelper.ExcuteDataTable(sql, param);
        }
        //获得树数据-用于题库录入
        public DataTable getSelTree(int pid)
        {
            string sql = "select id,cast(a.leved as varchar)+'-'+a.name[name],case when leved=4 then snav else null end[nav],leved,cast(case when exists(select * from subjectClass where valid=1 and a.id=pId and id<>a.pId) then 1 else 0 end as bit)[isParent] from subjectClass a where valid=1 and leved<>0 and pId=@pid";
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@pid",pid)
            };
            return SqlHelper.ExcuteDataTable(sql, param);
        }
        //获得树数据
        public DataTable getManTree(int pid)
        {
            string sql = "select a.id,cast(a.leved as varchar)+'-'+a.name+'('+b.name+')-'+cast(a.total as varchar)+'题' as name,a.name[mc],leved,levelID,a.total,a.valid,cast(case when exists(select * from subjectClass where a.id=pId and id<>a.pId) then 1 else 0 end as bit)[isParent] from subjectClass a left join subjectLevel b on a.levelID=b.ID where pid=@pid";
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@pid",pid)
            };
            return SqlHelper.ExcuteDataTable(sql, param);
        }
        //通过level值获得select--用于随机组卷
        public DataTable sel_level(int level) {
            string sql = "select ID[val],name[text] from subjectClass where valid=1 and pId=2 and levelID=@level";
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@level",level)
            };
            return SqlHelper.ExcuteDataTable(sql, param);
        }//通过pid值获得select--用于随机组卷
        public DataTable sel_pid(int pid)
        {
            string sql = "select ID[val],name[text] from subjectClass where valid=1 and pId=@pid";
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@pid",pid)
            };
            return SqlHelper.ExcuteDataTable(sql, param);
        }
    }
}
