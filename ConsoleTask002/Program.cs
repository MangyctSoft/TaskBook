using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace ConsoleTask002
{
    /// <summary>
    /// #Random-Task 002.
    /// Требуется написать программу, определяющую в каких системах счисления
    /// с основанием от 2 до 36 это число не содержит одинаковых цифр.
    /// </summary>

    class Program
    {
        /// <summary>
        /// Получить остаток от деления
        /// </summary>
        /// <param name="number">Число для поиска</param>
        /// <param name="numberSystem">Система счисления</param>
        /// <returns></returns>
        static int NumbersInNumberSystem(ref int number, int numberSystem)
        {
            var result = number % numberSystem;
            number = number / numberSystem;
            return result; 
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите целое число:");
            var numConsole = Console.ReadLine();
            var cons = int.Parse(numConsole);

            var globalList = new Dictionary<int, List<int>>(); // - Словарь для хранения всех результатов

            // - Перебираем системы счисления с 2 до 36 
            for (int numSystem = 2; numSystem < 37; numSystem++)
            {
                var number = cons;
                List<int> varibals = new List<int>();

                while (true)
                {
                    var rest = NumbersInNumberSystem(ref number, numSystem);
                    varibals.Add(rest);
                    if (number <= 1)
                    {
                        if (rest == 0)
                        {
                            varibals.Add(1);
                        }
                        break;
                    }
                }
                varibals.Reverse();         // - Чтобы число было верным, необходимо перевернуть список
                globalList.Add(numSystem, varibals);   // - Добавляем результат в словарь, где хранятся все результаты
                if (numSystem == 2)
                {
                    Console.WriteLine("Вывод всех результатов.");
                }

                Console.Write("Результат числа {0} в {1}-ой системе:", numConsole, numSystem);
                foreach (var item in varibals)
                {
                    Console.Write("[{0}]", item);
                }
                Console.WriteLine("");
            }

            Console.WriteLine("\nОдинаковых цифр нет в:");
            foreach (var list in globalList)
            {
                var findList = list.Value.Distinct();
                if (findList.Count() == list.Value.Count())
                {
                    Console.Write("В {0}-ой системе: ", list.Key);
                    foreach (var item in list.Value)
                    {
                        Console.Write("[{0}]", item);
                    }
                    Console.WriteLine("");
                }
            }
            Console.ReadLine();
        }
    }
}
