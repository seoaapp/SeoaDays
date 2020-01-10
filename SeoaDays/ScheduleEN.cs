using System;
using Discord;
using Discord.WebSocket;
using System.Xml;
using System.Xml.Linq;

namespace SeoaDays
{
    public class ScheduleEN
    {
        public string ScheduleAdd(ulong id, string name, string date, string content = null)
        {
            XDocument xml = XDocument.Load("data.xml");
            XElement user = xml.Root.Element("_" + id);
            /*
             <userName>
                <name>
                    <date>0000-00-00</date>
                    <content>asdfasdf</content>                   
                </name>
            </userName>
            */

            try
            {
                XElement a = user.Element("_" + name);
                if (a == null)
                {
                    Console.WriteLine(1);
                    if (date.Split('-').Length < 3)
                    {
                        date += "-23-59";
                    }
                    XElement newSchedule = new XElement("_" + name,
                    new XElement("date", date),
                    new XElement("content", content));
                    user.Add(newSchedule);
                    xml.Save("data.xml");
                    return $"Schedule {name}(Date: {date}) was made";
                }
                else
                {
                    return "You already have schedule(same name)";
                }
            }
            catch
            {
                return "The name of schedule is wrong! Please edit name and retry";
            }
        }
        public void ScheduleSee(SocketMessage msg)
        {
        }
        public void ScheduleDelete(SocketMessage msg)
        {
        }
    }
}
