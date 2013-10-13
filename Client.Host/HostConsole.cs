using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Client.Host.Framework;
using System.Net;
using System.IO;

namespace Client.Host
{
    class HostConsole
    {
        static void Main(string[] args)
        {
            RunHosts();
        }

        static void RunHosts()
        {
            HostManager.Run();
            Console.WriteLine("服务正在运行，如需暂停服务请输入stop，如需重置服务请输入reset，如需完全关闭服务请输入off");
            bool off = false;
            while (!off)
            {
                string t = Console.ReadLine();
                switch (t.ToLower())
                {
                    case "stop":
                        if (HostManager.Running)
                        {
                            HostManager.Stop();
                            Console.WriteLine("服务已经暂停，如需开启服务请输入open，如需完全关闭服务请输入off");
                        }
                        break;
                    case "open":
                        if (!HostManager.Running)
                        {
                            HostManager.Run();
                            Console.WriteLine("服务正在运行，如需暂停服务请输入stop，如需完全关闭服务请输入off");
                        }
                        break;
                    case "off":
                        off = true;
                        break;
                    default:
                        Console.WriteLine("系统看不懂你的鸟语，请重新输入");
                        if (HostManager.Running)
                        {
                            Console.WriteLine("服务正在运行，如需暂停服务请输入stop，如需完全关闭服务请输入off");
                        }
                        else
                        {
                            Console.WriteLine("服务已经暂停，如需开启服务请输入open，如需完全关闭服务请输入off");
                        }
                        break;
                }
            }
            Console.WriteLine("服务已经完全关闭，请默哀三分钟！");
        }
    }
}
