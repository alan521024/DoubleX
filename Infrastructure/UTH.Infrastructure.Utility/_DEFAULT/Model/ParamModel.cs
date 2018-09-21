using System;
using System.Collections.Generic;
using System.Text;

namespace UTH.Infrastructure.Utility
{
    public class TParam<T1>
    {
        public T1 Param1 { get; set; }
    }
    public class TParam<T1, T2> : TParam<T1>
    {
        public T2 Param2 { get; set; }
    }
    public class TParam<T1, T2, T3> : TParam<T1, T2>
    {
        public T3 Param3 { get; set; }
    }
    public class TParam<T1, T2, T3, T4> : TParam<T1, T2, T3>
    {
        public T4 Param4 { get; set; }
    }
    public class TParam<T1, T2, T3, T4, T5> : TParam<T1, T2, T3, T4>
    {
        public T5 Param5 { get; set; }
    }

}
