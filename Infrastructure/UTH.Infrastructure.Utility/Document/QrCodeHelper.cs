using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using QRCoder;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 二维码辅助类
    /// </summary>
    public static class QrCodeHelper
    {
        #region 属性变量

        #endregion

        #region 获取内容

        #endregion

        #region 扩展方法

        #endregion

        #region 验证判断

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content"></param>
        public static Bitmap GetCode(string content, string iconPath = null, int iconSize = 0)
        {
            content.CheckEmpty();
            if (!iconPath.IsEmpty())
            {
                return GetCode(content, 20, Color.Black, Color.White, new Bitmap(iconPath), iconSize);
            }
            return GetCode(content, 20, Color.Black, Color.White);
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content"></param>
        public static Bitmap GetCode(string content, int pixelsPerModule, Color darkColor, Color lightColor, Bitmap icon = null, int iconSizePercent = 15, int iconBorderWidth = 6, bool drawQuietZones = true)
        {
            content.CheckEmpty();
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        return qrCode.GetGraphic(pixelsPerModule, darkColor, lightColor, icon, iconSizePercent, iconBorderWidth, drawQuietZones);
                    }
                }
            }
        }

        #endregion
    }
}
