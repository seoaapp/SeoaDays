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
            XElement newSchedule = new XElement(name,
                new XElement("date",date),
                new XElement("content",content));
            user.Add(newSchedule);
            xml.Save("data.xml");
            return $"Schedule \"{name}\" was made";
        }
        public void ScheduleSee(SocketMessage msg)
        {
        }
        public void ScheduleDelete(SocketMessage msg)
        {
        }
    }
}
