using Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL.root
{
    public class subchDAL : IDisposable
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
        ~subchDAL() { Dispose(false); }
        #endregion

        public int save(subch m)
        {
            SqlParameter[] pars = { 
                new SqlParameter("@sid", m.sid),
                new SqlParameter("@title ", m.title),
                new SqlParameter("@subs ", m.subs),
                new SqlParameter("@A ", m.A),
                new SqlParameter("@B ", m.B),
                new SqlParameter("@C ", m.C),
                new SqlParameter("@D ", m.D),
                new SqlParameter("@E ", m.E),
                new SqlParameter("@explain ", m.explain),
                new SqlParameter("@result ", m.result),
                new SqlParameter("@instate ", m.instate),
                new SqlParameter("@creator ", m.creator),
                new SqlParameter("@retu",SqlDbType.Int)
            };
            pars[12].Direction = ParameterDirection.ReturnValue;
            SqlHelper.RunProcedure("[root_instCh]", pars);
            return Convert.ToInt16(pars[12].Value);
        }
        public int update(subch m)
        {
            string sql = "update ChooseSubject set Title=@title,Subjects=@subs,A=@A,B=@B,C=@C,D=@D,E=@E,result=@result,SubjectClassID=@sid,mender=@mender,mendTime=GETDATE(),explain=@explain,inputState=@instate where ID=@id";
            SqlParameter[] pars = {
                new SqlParameter("@id", m.id),
                new SqlParameter("@sid", m.sid),
                new SqlParameter("@title ", m.title),
                new SqlParameter("@subs ", m.subs),
                new SqlParameter("@A ", m.A),
                new SqlParameter("@B ", m.B),
                new SqlParameter("@C ", m.C),
                new SqlParameter("@D ", m.D),
                new SqlParameter("@E ",SqlDbType.NVarChar),
                new SqlParameter("@explain ", SqlDbType.NVarChar),
                new SqlParameter("@result ", m.result),
                new SqlParameter("@instate ", m.instate),
                new SqlParameter("@mender ", m.creator)
            };
            if (m.E != "" && m.E != null) pars[8].Value = m.E;
            else pars[8].Value = DBNull.Value;
            if (m.explain != "" && m.explain != null) pars[9].Value = m.explain;
            else pars[9].Value = DBNull.Value;
            return SqlHelper.ExcuteNonQuery(sql, pars);
        }
        public void inState(int id, bool state)
        {
            string sql = "update ChooseSubject set inputState=@state where ID=@id";
            SqlParameter[] pars = {
                new SqlParameter("@id", id),
                new SqlParameter("@state ", state)
            };
            SqlHelper.ExcuteNonQuery(sql, pars);
        }
        public subch getEntity(int id)
        {
            string sql = "select k.id,s.ID[sid],snav,title,Subjects[subs],A,B,C,D,E,result,explain,inputState[instate] from ChooseSubject k left join subjectClass s on k.SubjectClassID=s.ID where k.ID=@id";
            SqlParameter[] pars = { 
                new SqlParameter("@id", id)
            };
            return SqlHelper.ExecuteEntity<subch>(sql, pars);
        }
    }
}
