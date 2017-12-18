using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;

namespace Common
{
    public class DataParse : IDisposable
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
        ~DataParse() { Dispose(false); }
        #endregion
        /// <summary>
        /// DataTable转换List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<T> DataTableToList<T>(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0) {
                List<T> list = new List<T>();
                DataTableEntityBuilder<T> eblist = DataTableEntityBuilder<T>.CreateBuilder(dt.Rows[0]);
                foreach (DataRow info in dt.Rows)
                    list.Add(eblist.Build(info));
                dt.Dispose();
                dt = null;
                return list;
            }
            else return null;
        }
        /// <summary>
        /// DataRow转实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public T DataRowToEntity<T>(DataRow dr)
        {
            DataTableEntityBuilder<T> eblist = DataTableEntityBuilder<T>.CreateBuilder(dr);
            return eblist.Build(dr);
        }

        #region 内部
        //返回DataRow 中对应的列的值。  
        public class DataTableEntityBuilder<T>
        {
            private static readonly MethodInfo getValueMethod = typeof(DataRow).GetMethod("get_Item", new Type[] { typeof(int) });
            private static readonly MethodInfo isDBNullMethod = typeof(DataRow).GetMethod("IsNull", new Type[] { typeof(int) });
            private delegate T Load(DataRow dataRecord);
            private Load handler;
            private DataTableEntityBuilder() { }
            public T Build(DataRow dataRecord)
            {
                return handler(dataRecord);
            }
            public static DataTableEntityBuilder<T> CreateBuilder(DataRow dataRow)
            {
                DataTableEntityBuilder<T> dynamicBuilder = new DataTableEntityBuilder<T>();
                DynamicMethod method = new DynamicMethod("DynamicCreateEntity", typeof(T), new Type[] { typeof(DataRow) }, typeof(T), true);
                ILGenerator generator = method.GetILGenerator();
                LocalBuilder result = generator.DeclareLocal(typeof(T));
                generator.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));
                generator.Emit(OpCodes.Stloc, result);
                for (int index = 0; index < dataRow.ItemArray.Length; index++)
                {
                    PropertyInfo propertyInfo = typeof(T).GetProperty(dataRow.Table.Columns[index].ColumnName);
                    Label endIfLabel = generator.DefineLabel();
                    if (propertyInfo != null && propertyInfo.GetSetMethod() != null)
                    {
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, index);
                        generator.Emit(OpCodes.Callvirt, isDBNullMethod);
                        generator.Emit(OpCodes.Brtrue, endIfLabel);
                        generator.Emit(OpCodes.Ldloc, result);
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, index);
                        generator.Emit(OpCodes.Callvirt, getValueMethod);
                        generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);
                        generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());
                        generator.MarkLabel(endIfLabel);
                    }
                }
                generator.Emit(OpCodes.Ldloc, result);
                generator.Emit(OpCodes.Ret);
                dynamicBuilder.handler = (Load)method.CreateDelegate(typeof(Load));
                return dynamicBuilder;
            }
        }
        #endregion
    }
}
