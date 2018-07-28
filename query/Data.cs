using Microsoft.Win32;
using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace query
{
    partial class  Data
    {
        public static IHTMLElement inputSearch;

        public static List<Word> RawList=new List<Word>();

        public static string[] VacArray;

        public class Word
        {
            public string ID { get; set; }
            public string Sentense { get; set; }
            public string Comment { get; set; }

            /// <summary>
            /// 是否查得到
            /// </summary>
            public bool IsExist;

            /// <summary>
            /// 有没有例句
            /// </summary>
            public bool HasSamples { get; set; }


        }

        static public string FormateToString()
        {
            StringBuilder @string = new StringBuilder();

            foreach (var word in RawList)
            {
                @string.AppendLine();
                @string.AppendLine(word.ID);
                if (word.IsExist == false)
                {
                    @string.AppendLine("无查询结果");
                    continue;
                }
                if (word.Comment==null)
                {
                    @string.AppendLine("无查询结果");
                    continue;
                }
                @string.AppendLine(word.Comment.Replace("\r\n",""));

                if (word.HasSamples==false)
                {
                    @string.AppendLine("没有例句\n");
                    continue;
                }
                var s = word.Sentense.Replace("  ","*").Split('*');

                foreach (var st in s)
                {
                    //去除空字符串、数字
                    if (st.Length==0 || st.Length==1 ||st.Length ==2)
                    {                        
                        continue;
                    }
                    @string.AppendLine(st);
                }
                @string.AppendLine();
            }
            return @string.ToString();
        }

        public static MainWindow window;

    }
}
