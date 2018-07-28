using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace query
{
    class Manager
    {
        static public void Read()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "text (*.txt)|*.*";
            if (openFileDialog.ShowDialog()==true)
            {
                FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open);
                StreamReader reader = new StreamReader(fs);
                var res=reader.ReadToEnd();
                
                Data.VacArray = res.Split('\r','\n');
            }
        }
        static public async void Query()
        {
            Data.LogOutput("开始查询");
            for (int i = 0; i < Data.VacArray.Length; i++)
            {
                if (Data.VacArray[i]=="")
                {
                    continue;
                }
                Data.window.webbrowser.Navigate("http://fanyi.baidu.com/#en/zh/" + Data.VacArray[i].ToLower());

                //等js运行完
                await Task.Run(() => { Thread.Sleep(1800);});

                Data.window.a();

                var s = Data.inputSearch.outerHTML;
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(s);

                StringBuilder @comment=new StringBuilder();


                var pronounce = document.DocumentNode.SelectNodes("//span[@class='phonetic-transcription']");
                if (pronounce !=null && pronounce.Count!=1)
                {
                    @comment.Append(pronounce[1].SelectSingleNode("//b").InnerText);
                }

                var rt = document.DocumentNode.SelectSingleNode("//div[@class='dictionary-comment']");              
                if (rt==null) //查不到当前单词
                {
                    Data.RawList.Add(new Data.Word
                    {
                        ID = Data.VacArray[i],
                        IsExist = false,
                    });
                    continue;
                }
                @comment.Append(rt.InnerText);

                #region Process Example Sentenses
                var sentense = document.DocumentNode.SelectSingleNode("//div[@class='double-sample']");

                if (sentense==null) //没有例句
                {
                    Data.RawList.Add(new Data.Word
                    {
                        ID=Data.VacArray[i],
                        IsExist = true,
                        HasSamples= false
                    });
                    continue;
                }

                //去掉tips
                var tips= document.DocumentNode.SelectSingleNode("//div[@class='sample-origin-tips']");
                if (tips!=null)
                {
                    tips.Remove();
                }


                var resources = sentense.SelectNodes("//p[@class='sample-resource']");
                if (resources!=null)
                {
                    foreach (var item in resources)
                    {
                        item.Remove();
                    }
                }

                var lables = sentense.SelectNodes("//label");
                if (lables != null)
                {
                    foreach (var item in lables)
                    {
                        item.Remove();
                    }
                }

                #endregion

                var s1 = sentense.InnerText;
                Data.RawList.Add(new Data.Word
                {
                    IsExist = true,
                    HasSamples=true,
                    ID = Data.VacArray[i],
                    Comment=@comment.ToString(),
                    Sentense=s1
                });             
            }

        }
        static public void Save()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, Data.FormateToString());
        }
    }
}
