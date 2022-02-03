using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileMatchSDK;

namespace ConsoleFileMatch
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
             * Local file
             * Remote location
             * 
             */
        }


        static void MatchFile()
        {
            FileMatchSDK.File localfile = new FileMatchSDK.File(
                @"E:\Laptop Backup\My Documents\iTunes Media\Music\311\Greatest Hits '93-'03\04 Amber.m4a");
        }
    }
}
