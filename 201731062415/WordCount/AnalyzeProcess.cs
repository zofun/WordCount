﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace WordCount
{
    /// <summary>
    /// 统计文本中的单词信息
    /// </summary>
    class AnalyzeProcess : Handler
    {
        /// <summary>
        /// 统计字符数数量
        /// </summary>
        /// <param name="context"></param>
        public void countChar(Context context)
        {
            int count = 0;
           
            foreach (char countchar in context.FileInfo)
            {
                if(countchar<128 && countchar >= 0)
                {
                    count++;
                }
            }
            context.BaseInfo.Add("characters", count);

        }

        /// <summary>
        /// 统计每个单词出现的次数
        /// </summary>
        /// <param name="context"></param>
        private void countWords(Context context)
        {
            string[] words = Regex.Split(context.FileInfo, @"\W+");
            foreach (string word in words)
            {
                if (context.WordCount.ContainsKey(word))
                {
                    context.WordCount[word]++;
                }
                else
                {
                    context.WordCount[word] = 1;
                }
            }

         
        }

        /// <summary>
        /// 统计文本的行数
        /// </summary>
        /// <param name="context"></param>
        private void countLines(Context context)
        {

            MatchCollection mch = Regex.Matches(context.FileInfo, "\n");
            context.BaseInfo.Add("lines", mch.Count);
        }

       
    
        

        /// <summary>
        /// 文本内容统计的处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool process(Context context)
        {

            //统计文本的各种信息
            countChar(context);
            countWords(context);
            countLines(context);

            context.BaseInfo.Add("words", context.WordCount.Count);
            //交由下一步处理
            getNextHandler().process(context);
            return true;
        }

       
    }
}
