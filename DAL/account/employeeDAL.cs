using Models;
using System.Data.SqlClient;
using System.Data;
using System;

namespace DAL.account
{
    public class employeeDAL : IDisposable
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
            if (disposing)
                dis = true;
        }
        ~employeeDAL() { Dispose(false); }
        #endregion

        #region 登录
        public employeeLoginInfo login(string account, string pwd)
        {
            SqlParameter[] pars = {
                                 new SqlParameter("@account",SqlDbType.NVarChar,100),
                                 new SqlParameter("@pwd",SqlDbType.NVarChar,200)
                                 };
            pars[0].Value = account;
            pars[1].Value = pwd;
            return SqlHelper.RunProcedureEntity<employeeLoginInfo>("[acc_login]", pars);
        }
        //通过eid获得前台登录数据
        public employeeCookie getCookieEntity(string eid)
        {
            string sql = "select name,photoURL[photo] from employee where uniqueID=@eid";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@eid",eid)
            };
            return SqlHelper.ExecuteEntity<employeeCookie>(sql, param);
        }
        #endregion

        #region 注册
        //手机号码是否存在
        public bool phoneExists(string phone)
        {
            string sql = "select id from employee where mobilePhone=@phone";
            SqlParameter[] pars = 
            {
                new SqlParameter("@phone",phone)
            };
            return SqlHelper.Exists(sql, pars);
        }
        //邮箱是否存在
        public bool emailExists(string email)
        {
            string sql = "select id from employee where email=@email";
            SqlParameter[] pars = 
            {
                new SqlParameter("@email",email)
            };
            return SqlHelper.Exists(sql, pars);
        }
        //手机号注册
        public int regPhone(string phone, string pwd, string token)
        {
            SqlParameter[] pars = {
                                 new SqlParameter("@phone",phone),
                                 new SqlParameter("@pwd",pwd),
                                 new SqlParameter("@token",token),
                                 new SqlParameter("@retu",SqlDbType.Int)
                                 };
            pars[3].Direction = ParameterDirection.ReturnValue;
            SqlHelper.RunProcedure("[acc_regPhone]", pars);
            return Convert.ToInt32(pars[3].Value);
        }
        //邮箱注册
        public int regEmail(string account, string email, string pwd, string token)
        {
            SqlParameter[] pars = {
                                 new SqlParameter("@account",account),
                                 new SqlParameter("@email",email),
                                 new SqlParameter("@pwd",pwd),
                                 new SqlParameter("@token",token),
                                 new SqlParameter("@retu",SqlDbType.Int)
                                 };
            pars[4].Direction = ParameterDirection.ReturnValue;
            SqlHelper.RunProcedure("[acc_regEmail]", pars);
            return Convert.ToInt32(pars[4].Value);
        }

        #endregion

        #region 找回密码
        //手机或邮箱是否存在
        public bool phoneOrEmailExists(string account)
        {
            string sql = "select id from employee where email=@account or mobilePhone=@account and valid=1";
            SqlParameter[] pars = 
            {
                new SqlParameter("@account",account)
            };
            return SqlHelper.Exists(sql, pars);
        }
        //找回密码
        public int backPwd(string account, string pwd, string token)
        {
            SqlParameter[] pars = {
                                 new SqlParameter("@account",account),
                                 new SqlParameter("@pwd",pwd),
                                 new SqlParameter("@token",token),
                                 new SqlParameter("@retu",SqlDbType.Int)
                                 };
            pars[3].Direction = ParameterDirection.ReturnValue;
            SqlHelper.RunProcedure("[acc_backPwd]", pars);
            return Convert.ToInt32(pars[3].Value);
        }

        #endregion

        #region 后台操作
        public employeeDetail getDetail(int id)
        {
            string sql = "select name,sex,photo,nation,degree,state,company,phone,idcard,account,email,department,offices,level,jobInfo,logins,learns,lastLoginTime,registerTime from v_employeeList where valid=1 and id=@id";
            SqlParameter[] pars = 
            {
                new SqlParameter("@id",id)
            };
            return SqlHelper.ExecuteEntity<employeeDetail>(sql, pars);
        }
        /// <summary>
        /// 修改认证状态
        /// </summary>
        /// <param name="state"></param>
        /// <param name="id"></param>
        public void updateState(int state, int id)
        {
            string sql = "update employee set [state]=@state where id=@id";
            SqlParameter[] pars = 
            {
                new SqlParameter("@state",state),
                new SqlParameter("@id",id)
            };
            SqlHelper.ExcuteNonQuery(sql, pars);
        }
        #endregion

        #region 基本资料
        /// <summary>
        /// 个人中心数据
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public DataSet profileCore(string eid)
        {
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@eid",eid)
            };
            return SqlHelper.RunProcedure("[me_profileCore]", param, "profileCore");
        }
        //判断是否可以申请审核，可以则更新审核日期
        public int isApply(string eid)
        {
            string sql = "if exists(select * from employee where name is not null and sex is not null and companyID is not null and officesID is not null and JobInfoID is not null and uniqueID=@eid) ";
            sql += " begin update employee set applyTime=GETDATE() where uniqueID=@eid end ";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@eid",eid)
            };
            return SqlHelper.ExcuteNonQuery(sql, param);
        }
        //修改密码
        public int changePwd(string uniqueID, string oldPwd, string newPwd)
        {
            string sql = "if exists (select top 1 1 from employee where uniqueID=@uniqueID and userPwd=@userPwd and valid=1) ";
            sql += " begin update employee set userPwd=@newPwd where uniqueID=@uniqueID end ";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@uniqueID",uniqueID),
                new SqlParameter("@userPwd",oldPwd),
                new SqlParameter("@newPwd",newPwd)
            };
            return SqlHelper.ExcuteNonQuery(sql, param);
        }
        //获得基本资料实体  //profile/personal
        public employeePersonal getPersonalEntity(string eid)
        {
            //string sql = "select photoURL[photo],account,e.name,sex,idcard,mobilePhone[phone],email,x.pxzbm[city],x.xzbm[county],e.companyID,zzmm,nation,degreeid,bz from employee e left join vp_xzbm x on e.xzbm=x.xzbm where e.valid=1 and uniqueID=@eid";
            string sql = "select photoURL[photo],account,name,sex,mobilePhone[phone],email,zzmm,nation,degreeid from employee where valid=1 and uniqueID=@eid";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@eid",eid)
            };
            return SqlHelper.ExecuteEntity<employeePersonal>(sql, param);
        }
        //获得工作信息实体  //profile/profession
        public employeeProfession getProfessionEntity(string eid)
        {
            string sql = "select x.pxzbm[city],x.xzbm[county],company,levelID[jibie],jobInfoID[zhicheng],idcard,bz from employee e left join vp_xzbm x on x.xzbm=e.xzbm where uniqueID=@eid";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@eid",eid)
            };
            return SqlHelper.ExecuteEntity<employeeProfession>(sql, param);
        }
        //获得基本资料 部门科室，级别职称  实体
        public employee_bkjz getBkjzEntity(string eid)
        {
            string sql = "select departmentID[bumen],officesID[keshi],levelID[jibie],jobInfoID[zhicheng] from employee where uniqueID=@eid";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@eid",eid)
            };
            return SqlHelper.ExecuteEntity<employee_bkjz>(sql, param);
        }
        //保存基本资料
        public int saveEmployee(Models.employeePersonal m)
        {
            SqlParameter[] pars = new SqlParameter[] 
            {
                new SqlParameter("@eid",m.eid),
                new SqlParameter("@name",m.name),
                new SqlParameter("@sex",m.sex),
                new SqlParameter("@phone",m.phone),
                new SqlParameter("@email",m.email),
                new SqlParameter("@zzmm",m.zzmm),
                new SqlParameter("@nation",m.nation),
                new SqlParameter("@degreeid",m.degreeid),
                new SqlParameter("@photo",m.photo),
                new SqlParameter("@retu",SqlDbType.Int)
            };
            pars[9].Direction = ParameterDirection.ReturnValue;
            SqlHelper.RunProcedures("[me_saveEmployee]", pars);
            return Convert.ToInt32(pars[9].Value);
        }
        //保存工作信息
        public int saveProfession(Models.employeeProfession m)
        {
            SqlParameter[] pars = new SqlParameter[] 
            {
                new SqlParameter("@eid",m.eid),
                new SqlParameter("@xzbm",m.county),
                new SqlParameter("@company",m.company),
                new SqlParameter("@jibie",m.jibie),
                new SqlParameter("@zhicheng",m.zhicheng),
                new SqlParameter("@idcard",m.idcard),
                new SqlParameter("@bz",m.bz),
                new SqlParameter("@retu",SqlDbType.Int)
            };
            pars[7].Direction = ParameterDirection.ReturnValue;
            SqlHelper.RunProcedures("[me_saveProfession]", pars);
            return Convert.ToInt32(pars[7].Value);
        }
        //保存基本信息 部门科室，级别职称
        public int saveBkjz(Models.employee_bkjz m)
        {
            SqlParameter[] pars = new SqlParameter[] 
            {
                new SqlParameter("@eid",m.eid),
                new SqlParameter("@bumen",m.bumen),
                new SqlParameter("@keshi",m.keshi),
                new SqlParameter("@jibie",m.jibie),
                new SqlParameter("@zhicheng",m.zhicheng)
            };
            return SqlHelper.RunProcedures("[me_saveEmployeeBkjz]", pars);
        }

        //获得证件照的路径
        public string getPhoto(string eid)
        {
            string sql = "select photo from employee where uniqueID=@eid";
            SqlParameter[] pars ={
                                    new SqlParameter("@eid",eid)
                                };
            DataTable dt = SqlHelper.ExcuteDataTable(sql, pars);
            return dt.Rows[0]["photo"] == DBNull.Value ? null : dt.Rows[0]["photo"].ToString();
        }
        //上传证件照
        public void updPhoto(string photo, string eid)
        {
            string SQL = "update employee set photo=@photo where uniqueID=@eid";
            SqlParameter[] pars ={
                                    new SqlParameter("@photo",SqlDbType.VarChar,60),
                                    new SqlParameter("@eid",SqlDbType.NVarChar,36)
                                };
            pars[0].Value = photo;
            pars[1].Value = eid;
            SqlHelper.ExcuteNonQuery(SQL, pars);
        }
        #endregion
    }
}
