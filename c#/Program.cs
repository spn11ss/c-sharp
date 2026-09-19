using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int OperationsCount;
            do
            {
                Console.Write("Введите количество оппераций от 2 до 40: ");
            } while (!int.TryParse(Console.ReadLine(), out OperationsCount) || OperationsCount < 2 || OperationsCount > 40);

            string[] names = new string[OperationsCount];
            decimal[] amounts = new decimal[OperationsCount];

            Console.WriteLine("\n Введите расходы в формате: Название; Сумма");

            for (int i = 0; i < OperationsCount; i++)
            {
                bool isValid = false;
                while (!isValid)
                {
                    Console.WriteLine($"Операция {i + 1}: ");
                    string input = Console.ReadLine();
                    string[] parts = input.Split(';');
                    if (parts.Length == 2)
                    {
                        string name = parts[0].Trim();
                        if (!string.IsNullOrWhiteSpace(name) && decimal.TryParse(parts[1].Trim(), out decimal amount) && amount > 0)
                        {
                            names[i] = name;
                            amounts[i] = amount;
                            isValid = true;
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: Неверный формат суммы! Сумма должна быть положительным числом.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Неверный формат! Используйте: Название; Сумма");
                    }
                }
            }
            if (names.Length != 0 || amounts.Length != 0)
            {
                bool isValue = false;
                while (!isValue)
                {
                    int num;
                    Console.WriteLine("Меню:\n1. Вывод данных\n2. Статистика\n3. Сортировка по цене\n4. Конвертация валюты\n5. Поиск по названию\n0. Выход");
                    Console.Write("Введите пункт меню: ");
                    if (!int.TryParse(Console.ReadLine(), out num)) return;
                    switch (num)
                    {
                        case 0:
                            isValue = true;
                            break;
                        case 1:
                            DisplayData(names, amounts);
                            break;
                        case 2:
                            Statistic(amounts);
                            break;
                        case 3:
                            SortByPrice(names, amounts);
                            break;
                        case 4:
                            Convert(amounts);
                            break;
                        case 5:
                            Search(names, amounts);
                            break;
                        default:
                            Console.WriteLine("Неверный выбор!");
                            break;
                    }
                }
            }
        }
        static void DisplayData(string[] names, decimal[] amounts)
        {
            decimal sum = 0;
            Console.WriteLine("Все затраты:");
            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {names[i]} - {amounts[i]} руб.");
                sum += amounts[i];
            }
            Console.WriteLine($"Количество затрат: {names.Length}\n Сумма всех затрат: {sum} руб.\n");
        }
        static void Statistic(decimal[] amounts)
        {
            decimal Max = amounts[0];
            decimal Min = amounts[0];
            decimal Sum = 0;
            for (int i = 0; i < amounts.Length; i++)
            {
                Sum += amounts[i];
                if (amounts[i] > Max) Max = amounts[i];
                if (amounts[i] < Min) Min = amounts[i];
            }
            decimal Average = Sum / amounts.Length;

            Console.WriteLine($"Статистика:\nСреднее: {Average} руб.\nМаксимальное: {Max} руб.\nМинимальное:{Min} руб.\nСумма: {Sum} руб.\n");

        }
        static void SortByPrice(string[] names, decimal[] amounts)
        {
            bool isSorted = true;
            int count = amounts.Length;
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (amounts[j] > amounts[j + 1])
                    {
                        decimal tempAmount = amounts[j];
                        amounts[j] = amounts[j + 1];
                        amounts[j + 1] = tempAmount;

                        string tempName = names[j];
                        names[j] = names[j + 1];
                        names[j + 1] = tempName;
                    }
                }
            }
            for (int k = 0; k < count - 1; k++)
            {
                if (amounts[k] > amounts[k + 1])
                {
                    isSorted = false;
                    break;
                }
            }
            if (isSorted) Console.WriteLine("Успешно рассортировано по цене!");
            else Console.WriteLine("Что-то пошло не так: массив не отсортирован!");
        }

        static void Convert(decimal[] amounts)
        {
            int num;
            decimal Kours;
            Console.WriteLine("Конвертация валюты:\n1. Доллар США\n2. Евро\n3. Другая валюта (ввести курс вручную)");
            Console.Write("Выберите валюту: ");
            if (!int.TryParse(Console.ReadLine(), out num)) return;
            switch (num)
            {
                case 1:
                    Kours = 84.5093m;
                    break;
                case 2:
                    Kours = 97.4984m;
                    break;
                case 3:
                    Console.Write("Введите курс конвертации (1 валюта = X рублям): ");
                    if (!decimal.TryParse(Console.ReadLine(), out Kours) || Kours <= 0) return;
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    return;
            }
            Console.WriteLine("Расходы в выбранной валюте:");
            for (int i = 0; i < amounts.Length; i++)
            {
                decimal price = amounts[i] / Kours;
                Console.WriteLine($"{i + 1}. {amounts[i]} руб. = {price} (по курсу {Kours})");
            }

        }
        static void Search(string[] names, decimal[] amounts)
        {
            bool found = false;
            Console.Write("Введите название для поиска: ");
            string searchtxt = Console.ReadLine().ToLower();

            Console.WriteLine("Результаты поиска: ");
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i].ToLower().Contains(searchtxt))
                {
                    Console.WriteLine($"{names[i]}-{amounts[i]} руб.");
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("Расходы с таким названием не найдены.");
            }
        }
    }
}
