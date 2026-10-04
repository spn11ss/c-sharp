using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_
{
    internal class Program
    {
        static int nextid = 1;

        class Product
        {
            public int ProductID;
            public string Name;
            public decimal Price;
            public int Quantity;

            public Product (int productID, string name, decimal price, int quantity)
            {
                ProductID = productID;
                Name = name;
                Price = price;
                Quantity = quantity;
            }
            public void PrintInfo()
            {
                Console.WriteLine($"ID: {ProductID}\nНазвание: {Name}\nЦена: {Price}\nКоличество:{Quantity}\n");
            }
        }
        static void Main()
        {
            List<Product> products = new List<Product>();
            
            bool IsSuccess = false;

            while (!IsSuccess)
            {
                Console.Write("Меню:\n1. Добавить товар в список\n2. Удалить товар из списка\n3. Вывести информацию о товарах\n4. Выход\n\nВыберите действие: ");
                if (!int.TryParse(Console.ReadLine(), out int choise)) return;
                switch (choise)
                {
                    case 1:
                        AddProd(products);
                        break;
                    case 2:
                        RemoveProd(products);
                        break;
                    case 3:
                        ShowProd(products);
                        break;
                    case 4:
                        IsSuccess = true;
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Пожалуйста, введите число от 1 до 4.");
                        break;
                }
            }
        }
        static void AddProd(List<Product>products)
        {
            Console.WriteLine("Добавление товара:");

            string name;
            while (true)
            {
                Console.Write("Введите наименование товара: ");
                name = Console.ReadLine();

                bool isDuplicate = false;

                foreach (Product prod in products)
                {
                    if (prod.Name.ToLower() == name.ToLower())
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (isDuplicate) Console.WriteLine("Продукт с таким наименованием уже имеется, введите другое название\n");
                else break;
            }
            Console.Write("Введите цену товара: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price)) return;

            Console.Write("Введите количество товара: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity)) return;

            Product product = new Product(nextid, name, price, quantity);
            nextid++;

            products.Add(product);
            Console.WriteLine($"Товар '{name}' успешно добавлен с ID: {product.ProductID}");
        } 
        static void RemoveProd(List<Product> products)
        {
            Console.WriteLine("Удаление товара из списка:");

            if (products.Count == 0)
            {
                Console.WriteLine("Список товаров пуст. Нечего удалять.");
                return;
            }

            Console.Write("Введите ID товара для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int delID)) return;

            Product removeProd = null;
            foreach (Product prod in products)
            {
                if (prod.ProductID == delID)
                {
                    removeProd = prod;
                    break;
                }
            }

            if (removeProd != null)
            {
                products.Remove(removeProd);
                Console.WriteLine($"Товар '{removeProd.Name}' (ID: {delID}) успешно удалён.");
            }
            else Console.WriteLine($"Товар с ID {delID} не найден.");
        }
        static void ShowProd(List<Product> products)
        {
            Console.WriteLine("Список всех товаров:");

            if (products.Count == 0)
            {
                Console.WriteLine("Список товаров пуст.");
                return;
            }

            Console.WriteLine($"Всего товаров: {products.Count}\n");
            foreach (Product producti in products)
            {
                producti.PrintInfo();
            }
        }
    }
}
