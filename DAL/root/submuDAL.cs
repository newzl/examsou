/*
 * 创建人:   hllive
 * 创建时间: 2017/11/20 9:31:48
 * 描述:
 */
using Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL.root
{
    public class submuDAL : IDisposable
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
        ~submuDAL() { Dispose(false); }
        #endregion

        public int save(submu m)
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
                new SqlParameter("@F ", m.F),
                new SqlParameter("@explain ", m.explain),
                new SqlParameter("@result ", m.result),
                new SqlParameter("@instate ", m.instate),
                new SqlParameter("@creator ", m.creator),
                new SqlParameter("@retu",SqlDbType.Int)
            };
            pars[13].Direction = ParameterDirection.ReturnValue;
            SqlHelper.RunProcedure("[root_instMu]", pars);
            return Convert.ToInt16(pars[13].Value);
        }
        public int update(submu m)
        {
            string sql = "update MultinomialSubject set Title=@title,Subjects=@subs,A=@A,B=@B,C=@C,D=@D,E=@E,F=@F,result=@result,SubjectClassID=@sid,mender=@mender,mendTime=GETDATE(),explain=@explain,inputState=@instate where ID=@id";
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
                new SqlParameter("@F ",SqlDbType.NVarChar),
                new SqlParameter("@explain ", SqlDbType.NVarChar),
                new SqlParameter("@result ", m.result),
                new SqlParameter("@instate ", m.instate),
                new SqlParameter("@mender ", m.creator)
            };
            if (m.E != "" && m.E != null) pars[8].Value = m.E;
            else pars[8].Value = DBNull.Value;
            if (m.F != "" && m.F != null) pars[9].Value = m.F;
            else pars[9].Value = DBNull.Value;
            if (m.explain != "" && m.explain != null) pars[10].Value = m.explain;
            else pars[10].Value = DBNull.Value;
            return SqlHelper.ExcuteNonQuery(sql, pars);
        }
        public void inState(int id, bool state)
        {
            string sql = "update MultinomialSubject set inputState=@state where ID=@id";
            SqlParameter[] pars = {
                new SqlParameter("@id", id),
                new SqlParameter("@state ", state)
            };
            SqlHelper.ExcuteNonQuery(sql, pars);
        }
        public submu getEntity(int id)
        {
            string sql = "select k.id,s.ID[sid],snav,title,Subjects[subs],A,B,C,D,E,F,result,explain,inputState[instate] from MultinomialSubject k left join subjectClass s on k.SubjectClassID=s.ID where k.ID=@id";
            SqlParameter[] pars = { 
                new SqlParameter("@id", id)
            };
            return SqlHelper.ExecuteEntity<submu>(sql, pars);
        }
    }
}
