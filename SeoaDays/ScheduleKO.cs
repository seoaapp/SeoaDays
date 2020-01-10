using System;
using System.Xml;
using System.Xml.Linq;

namespace SeoaDays
{
    public class ScheduleKO
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

            Console.WriteLine(-1);
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
                    return $"일정 {name}(일시: {date}) 제작이 완료되었습니다";
                }
                else
                {
                    return "이미 중복된 이름의 일정이 있습니다. 이름을 바꿔 다시 시도해 주세요";
                }
            }
            catch
            {
                return "일정의 이름이 잘못되었습니다. 이름을 고친 후 다시 시도해주세요.";
            }
        }
        public void ScheduleSee()
        {
        }
        public void ScheduleDelete()
        {
        }
    }
}
