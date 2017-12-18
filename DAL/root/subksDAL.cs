using Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL.root
{
    public class subksDAL: IDisposable
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
        ~subksDAL() { Dispose(false); }
        #endregion

        public int save(subks m, string stype)
        {
            SqlParameter[] pars = { 
                new SqlParameter("@sid", m.sid),
                new SqlParameter("@title ", m.title),
                new SqlParameter("@subs ", m.subs),
                new SqlParameter("@explain ", m.explain),
                new SqlParameter("@result ", m.result),
                new SqlParameter("@instate ", m.instate),
                new SqlParameter("@creator ", m.creator),
                new SqlParameter("@stype ", stype),
                new SqlParameter("@retu",SqlDbType.Int)
            };
            pars[8].Direction = ParameterDirection.ReturnValue;
            SqlHelper.RunProcedure("[root_instKs]", pars);
            return Convert.ToInt16(pars[8].Value);
        }
        public int update(subks m,string table)
        {
            string sql = "update " + table + " set Title=@title,Subjects=@subs,result=@result,SubjectClassID=@sid,mender=@mender,mendTime=GETDATE(),explain=@explain,inputState=@instate where ID=@id";
            SqlParameter[] pars = {
                new SqlParameter("@id", m.id),
                new SqlParameter("@sid", m.sid),
                new SqlParameter("@title ", m.title),
                new SqlParameter("@subs ", m.subs),
                new SqlParameter("@explain ", SqlDbType.NVarChar),
                new SqlParameter("@result ", m.result),
                new SqlParameter("@instate ", m.instate),
                new SqlParameter("@mender ", m.creator)
            };
            if (m.explain != "" && m.explain != null) pars[4].Value = m.explain;
            else pars[4].Value = DBNull.Value;
            return SqlHelper.ExcuteNonQuery(sql, pars);
        }
        public void inState(int id, bool state, string table)
        {
            string sql = "update " + table + " set inputState=@state where ID=@id";
            SqlParameter[] pars = {
                new SqlParameter("@id", id),
                new SqlParameter("@state ", state)
            };
            SqlHelper.ExcuteNonQuery(sql, pars);
        }
        public subks getEntity(int id, string table)
        {
            string sql = "select k.id,s.ID[sid],snav,title,Subjects[subs],result,explain,inputState[instate] from " + table + " k left join subjectClass s on k.SubjectClassID=s.ID where k.ID=@id";
            SqlParameter[] pars = { 
                new SqlParameter("@id", id)
            };
            return SqlHelper.ExecuteEntity<subks>(sql, pars);
        }
    }
}
