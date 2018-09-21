using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace UTH.Plug.Speech
{
    /// <summary>
    /// 语音数据
    /// </summary>
   [Serializable]
    public class SpeechData
    {
        public Guid Key { get; set; }

        public byte[] Data { get; set; }

        public int Offset { get; set; }

        public int Length { get; set; }

        public string Text { get; set; }

        public bool Complete { get; set; }

        public DateTime LastDt { get; set; }
    }
}
