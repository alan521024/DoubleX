using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 表达式辅助类
    /// </summary>
    public static class ExpressHelper
    {
        #region 获取内容

        public static Expressible<T> Get<T>() where T : class, new()
        {
            return new Expressible<T>();
        }
        public static Expressible<T, T2> Get<T, T2>() where T : class, new() where T2 : class, new()
        {
            return new Expressible<T, T2>();
        }
        public static Expressible<T, T2, T3> Get<T, T2, T3>() where T : class, new() where T2 : class, new() where T3 : class, new()
        {
            return new Expressible<T, T2, T3>();
        }
        public static Expressible<T, T2, T3, T4> Get<T, T2, T3, T4>() where T : class, new() where T2 : class, new() where T3 : class, new() where T4 : class, new()
        {
            return new Expressible<T, T2, T3, T4>();
        }

        #endregion

        #region 扩展方法

        #endregion

        #region 验证判断

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        #endregion
    }

    public class Expressible<T> where T : class, new()
    {
        Expression<Func<T, bool>> _exp = null;

        public Expressible<T> And(Expression<Func<T, bool>> exp)
        {
            if (_exp == null)
                _exp = exp;
            else
                _exp = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(_exp.Body, exp.Body), _exp.Parameters);
            return this;
        }

        public Expressible<T> AndIF(bool isAnd, Expression<Func<T, bool>> exp)
        {
            if (isAnd)
                And(exp);
            return this;
        }

        public Expressible<T> Or(Expression<Func<T, bool>> exp)
        {
            if (_exp == null)
                _exp = exp;
            else
                _exp = Expression.Lambda<Func<T, bool>>(Expression.OrElse(_exp.Body, exp.Body), _exp.Parameters);
            return this;
        }

        public Expressible<T> OrIF(bool isOr, Expression<Func<T, bool>> exp)
        {
            if (isOr)
                Or(exp);
            return this;
        }


        public Expression<Func<T, bool>> ToExpression()
        {
            if (_exp == null)
                _exp = it => true;
            return _exp;
        }
    }
    public class Expressible<T, T2> where T : class, new() where T2 : class, new()
    {
        Expression<Func<T, T2, bool>> _exp = null;

        public Expressible<T, T2> And(Expression<Func<T, T2, bool>> exp)
        {
            if (_exp == null)
                _exp = exp;
            else
                _exp = Expression.Lambda<Func<T, T2, bool>>(Expression.AndAlso(_exp.Body, exp.Body), _exp.Parameters);
            return this;
        }

        public Expressible<T, T2> AndIF(bool isAnd, Expression<Func<T, T2, bool>> exp)
        {
            if (isAnd)
                And(exp);
            return this;
        }

        public Expressible<T, T2> Or(Expression<Func<T, T2, bool>> exp)
        {
            if (_exp == null)
                _exp = exp;
            else
                _exp = Expression.Lambda<Func<T, T2, bool>>(Expression.OrElse(_exp.Body, exp.Body), _exp.Parameters);
            return this;
        }

        public Expressible<T, T2> OrIF(bool isOr, Expression<Func<T, T2, bool>> exp)
        {
            if (isOr)
                Or(exp);
            return this;
        }


        public Expression<Func<T, T2, bool>> ToExpression()
        {
            if (_exp == null)
                _exp = (it, t2) => true;
            return _exp;
        }
    }
    public class Expressible<T, T2, T3> where T : class, new() where T2 : class, new() where T3 : class, new()
    {
        Expression<Func<T, T2, T3, bool>> _exp = null;

        public Expressible<T, T2, T3> And(Expression<Func<T, T2, T3, bool>> exp)
        {
            if (_exp == null)
                _exp = exp;
            else
                _exp = Expression.Lambda<Func<T, T2, T3, bool>>(Expression.AndAlso(_exp.Body, exp.Body), _exp.Parameters);
            return this;
        }

        public Expressible<T, T2, T3> AndIF(bool isAnd, Expression<Func<T, T2, T3, bool>> exp)
        {
            if (isAnd)
                And(exp);
            return this;
        }

        public Expressible<T, T2, T3> Or(Expression<Func<T, T2, T3, bool>> exp)
        {
            if (_exp == null)
                _exp = exp;
            else
                _exp = Expression.Lambda<Func<T, T2, T3, bool>>(Expression.OrElse(_exp.Body, exp.Body), _exp.Parameters);
            return this;
        }

        public Expressible<T, T2, T3> OrIF(bool isOr, Expression<Func<T, T2, T3, bool>> exp)
        {
            if (isOr)
                Or(exp);
            return this;
        }


        public Expression<Func<T, T2, T3, bool>> ToExpression()
        {
            if (_exp == null)
                _exp = (it, t2, t3) => true;
            return _exp;
        }
    }
    public class Expressible<T, T2, T3, T4> where T : class, new() where T2 : class, new() where T3 : class, new() where T4 : class, new()
    {
        Expression<Func<T, T2, T3, T4, bool>> _exp = null;

        public Expressible<T, T2, T3, T4> And(Expression<Func<T, T2, T3, T4, bool>> exp)
        {
            if (_exp == null)
                _exp = exp;
            else
                _exp = Expression.Lambda<Func<T, T2, T3, T4, bool>>(Expression.AndAlso(_exp.Body, exp.Body), _exp.Parameters);
            return this;
        }

        public Expressible<T, T2, T3, T4> AndIF(bool isAnd, Expression<Func<T, T2, T3, T4, bool>> exp)
        {
            if (isAnd)
                And(exp);
            return this;
        }

        public Expressible<T, T2, T3, T4> Or(Expression<Func<T, T2, T3, T4, bool>> exp)
        {
            if (_exp == null)
                _exp = exp;
            else
                _exp = Expression.Lambda<Func<T, T2, T3, T4, bool>>(Expression.OrElse(_exp.Body, exp.Body), _exp.Parameters);
            return this;
        }

        public Expressible<T, T2, T3, T4> OrIF(bool isOr, Expression<Func<T, T2, T3, T4, bool>> exp)
        {
            if (isOr)
                Or(exp);
            return this;
        }


        public Expression<Func<T, T2, T3, T4, bool>> ToExpression()
        {
            if (_exp == null)
                _exp = (it, t2, t3, t4) => true;
            return _exp;
        }
    }

    public class Expressible
    {
    }
}