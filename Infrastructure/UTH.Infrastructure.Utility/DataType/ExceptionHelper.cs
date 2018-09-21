using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 异常 辅助类
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// 获取异常消息
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="isStackTrace">是否显示堆栈信息</param>
        /// <returns></returns>
        public static string GetMessage(Exception ex, bool isStackTrace = false)
        {
            if (ex.IsNull())
            {
                return "";
            }

            string message = string.Empty;

            #region 指定类型的异常信息

            //if (ex is DbEntityValidationException)
            //{
            //    DbEntityValidationException valEx = ex as DbEntityValidationException;
            //    StringBuilder sb = new StringBuilder();
            //    foreach (var failure in valEx.EntityValidationErrors)
            //    {
            //        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
            //        foreach (var error in failure.ValidationErrors)
            //        {
            //            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
            //            sb.AppendLine();
            //        }
            //    }
            //    return sb.ToString();
            //}

            //if (ex is DbUpdateException)
            //{
            //    DbUpdateException updateEx = ex as DbUpdateException;
            //    if (updateEx.InnerException != null)
            //    {
            //        return updateEx.InnerException.InnerException != null ? updateEx.InnerException.InnerException.ToString() : updateEx.InnerException.Message;
            //    }
            //    return updateEx.Message;
            //    //StringBuilder sb = new StringBuilder();
            //    //foreach (var failure in updateEx.)
            //    //{
            //    //    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
            //    //    foreach (var error in failure.ValidationErrors)
            //    //    {
            //    //        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
            //    //        sb.AppendLine();
            //    //    }
            //    //}
            //    //return sb.ToString();
            //}

            //if (ex is DbUpdateConcurrencyException)
            //{
            //    return "框架在更新时引起了乐观并发，后修改的数据不会被保存";
            //}

            #endregion

            //Exception
            if (isStackTrace)
            {
                if (ex.InnerException != null)
                {
                    message = ex.ToString();
                }
                if (!string.IsNullOrWhiteSpace(ex.StackTrace))
                {
                    message += ex.StackTrace.ToString();
                }
            }
            else
            {
                if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                {
                    message = ex.InnerException.Message;
                }
                if (!string.IsNullOrWhiteSpace(ex.Message))
                {
                    message = ex.Message;
                }
                if (string.IsNullOrWhiteSpace(message))
                {
                    message = ex.ToString();
                }
            }
            return message;
        }
    }

    /// <summary>
    /// 网络/请求异常
    /// </summary>
    public class NetworkException : Exception
    {
        public NetworkException() : base() { }
        public NetworkException(string message) : base(message) { }
        public NetworkException(string message, Exception innerException) : base(message, innerException) { }
        public NetworkException(SerializationInfo info, StreamingContext context) : base(info,context) { }
    }

    /// <summary>
    /// 服务异常
    /// </summary>
    public class ServiceException : Exception
    {
        public ServiceException() : base() { }
        public ServiceException(string message) : base(message) { }
        public ServiceException(string message, Exception innerException) : base(message, innerException) { }
        public ServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// 认证异常
    /// </summary>
    public class AuthException : Exception
    {
        public AuthException() : base() { }
        public AuthException(string message) : base(message) { }
        public AuthException(string message, Exception innerException) : base(message, innerException) { }
        public AuthException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
