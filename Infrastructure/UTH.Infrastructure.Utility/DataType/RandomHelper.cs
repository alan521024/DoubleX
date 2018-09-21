using System;
using System.Collections.Generic;
using System.Text;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 随机数辅助类
    /// </summary>
    public class RandomHelper
    {
        #region 属性变量

        private static char[] randomContent ={'0','1','2','3','4','5','6','7','8','9',
            //'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
        };

        #endregion

        #region 获取内容

        #endregion

        #region 扩展方法

        #endregion

        #region 验证判断

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)


        /// <summary>
        /// 获取随机数字符串
        /// </summary>
        /// <param name="length"></param>
        /// <param name="contentChars"></param>
        /// <returns></returns>
        public static string GetToRandomString(int length, char[] contentChars = null)
        {
            if (contentChars == null || (contentChars != null && contentChars.Length == 0))
            {
                contentChars = randomContent;
            }

            int min = 0, max = contentChars.Length;
            var numArr = GetToRandomNumberArray(length, min, max);
            StringBuilder builder = new StringBuilder();
            for (var i = 0; i < numArr.Length; i++)
            {
                builder.Append(contentChars[numArr[i]]);
            }
            return builder.ToString();
        }

        /// <summary>
        /// 生成一个随机数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="numArr">仅：生成不重复随机数 ， 己有随机数组（用于判断num是否存在己有随机数组中）</param>
        /// <param name="num">仅：生成不重复随机数 ，随机数</param>
        /// <param name="random">仅：生成不重复随机数 ，随机对象</param>
        /// <returns></returns>
        public static int GetToRandomNumber(int min, int max, int[] numArr = null, int? num = null, Random random = null)
        {
            if (random == null)
            {
                random = new Random(unchecked((int)DateTime.Now.Ticks));
            }

            if (num == null)
            {
                num = random.Next(min, max); //随机取数
            }
            if (numArr != null && numArr.Length > 0)
            {
                int n = 0;
                while (n < numArr.Length)
                {
                    if (numArr[n] == num.Value) //利用循环判断是否有重复
                    {
                        num = random.Next(min, max); //重新随机获取。
                        GetToRandomNumber(min, max, numArr, num, random);//递归:如果取出来的数字和已取得的数字有重复就重新随机获取。
                    }
                    n++;
                }
            }
            return num.Value;
        }

        /// <summary>
        /// 生成一组随机数
        /// </summary>
        /// <param name="length"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int[] GetToRandomNumberArray(int length, int min, int max)
        {
            Random random = new Random(unchecked((int)DateTime.Now.Ticks));
            int[] numArr = new int[length];
            int num = 0;
            for (int i = 0; i < length; i++)
            {
                num = random.Next(min, max); //随机取数
                numArr[i] = GetToRandomNumber(min, max, numArr, num, random); //取出值赋到数组中
            }
            return numArr;
        }

        #endregion
    }
}
