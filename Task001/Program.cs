using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task001
{
    class Program
    {
        public class Logic
        {
            Random rnd = new Random();
            public string CreateIp()
            {
                return $"{rnd.Next(50)}.{rnd.Next(0)}.{rnd.Next(0)}.{rnd.Next(0)}";
            }

            public string CreateTime()
            {
                return $"{rnd.Next(23)}:{rnd.Next(59)}:{rnd.Next(59)}";
            }

            public int CreateDayOfWeek()
            {
                return rnd.Next(7);
            }

        }

        public class Line
        {
            public string Ip { get; set; }
            public string Time { get; set; }
            public int DayOfWeek { get; set; }         
        }

        static void Main(string[] args)
        {
            string inputFile = @"C:\z_input.txt";
            string outputFile = @"C:\z_output.txt";
            ///////////////////////
            Console.Write("Введите число строк в файле:");
            var countLine = Console.ReadLine();
            List<Line> dataLine = new List<Line>();
            Logic logic = new Logic();
            
            for (int i = 0; i < Convert.ToInt32(countLine); i++)
            {
                Line data = new Line
                {
                    Ip = logic.CreateIp(),
                    Time = logic.CreateTime(),
                    DayOfWeek = logic.CreateDayOfWeek()
                };
                dataLine.Add(data);
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(inputFile, false, System.Text.Encoding.Default))
                {
                    var sort = dataLine.OrderBy(s => s.DayOfWeek).OrderBy(s => s.Ip);
                    foreach (var item in sort)
                    {
                        sw.WriteLine($"{item.Ip} {item.Time} {item.DayOfWeek}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            /////////////////////////
            List<Line> outLine = new List<Line>();

            try
            {
                using (StreamReader sr = new StreamReader(inputFile, System.Text.Encoding.Default))
                {
                    
                    string lineFile = "";
                    string[] element = new string[3];
                    while ((lineFile = sr.ReadLine()) != null)
                    {
                        Line line = new Line();
                        element = lineFile.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        line.Ip = element[0];
                        line.Time = element[1];
                        line.DayOfWeek = Convert.ToInt32(element[2]);
                        outLine.Add(line);
                        //Console.WriteLine(lineFile);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var ipPopuls = new List<(string ip, int count, int[] popWeek)>();
            int wcount = 0;
            
            int daypop = -1;
            foreach (var item in outLine)
            {
                int[] wpop = new int[7];
                var same = ipPopuls.FindIndex(g => g.ip == item.Ip);
                var (ip, count, popWeek) = ipPopuls.Find(g => g.ip == item.Ip);
                if (same != -1)
                {                    
                    wcount = count + 1;
                    popWeek[item.DayOfWeek] += 1;                  
                    wpop = popWeek;
                    ipPopuls.RemoveAt(same);                                        
                }
                else
                {
                    wcount = 1;
                    wpop[item.DayOfWeek] = 1;
                }                

                var addItem = (item.Ip, wcount, wpop);
                ipPopuls.Add(addItem);
              
            }

            //Console.ReadLine();

            try
            {
                using (StreamWriter sw = new StreamWriter(outputFile, false, System.Text.Encoding.Default))
                {
                    foreach (var item in ipPopuls)
                    {
                        var next = -1;
                        for (int i =0; i < item.popWeek.Length; i++)
                        {
                            var current = item.popWeek[i];
                            if (current > next && current > 1)
                            {
                                next = current;
                                daypop = i;
                            }
                            
                        }
                        sw.WriteLine($"{ item.ip} число посещений: {item.count} || популярный день недели {daypop}");
                        daypop = -1;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
