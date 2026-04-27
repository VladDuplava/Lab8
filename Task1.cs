
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebDomainProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Шляхи до файлів
            string inputFile = "input.txt";
            string foundSitesFile = "found_sites.txt";
            string modifiedFile = "modified_text.txt";

            // Перевірка наявності вхідного файлу
            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"Помилка: Файл {inputFile} не знайдено!");
                Console.WriteLine("Створіть цей файл у папці з програмою та додайте туди текст.");
                return;
            }

            // 1. Читання тексту з файлу
            string text = File.ReadAllText(inputFile);
            Console.WriteLine("--- Оригінальний текст ---");
            Console.WriteLine(text);
            Console.WriteLine("--------------------------\n");

            // 2. Пошук вебсайтів домену .com за допомогою регулярного виразу
            // Шукає формати: site.com, www.site.com, http://site.com, https://www.site.com
            string pattern = @"\b(?:https?://)?(?:www\.)?[\w-]+\.com\b";
            MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

            // 3. Підрахунок кількості
            Console.WriteLine($"Знайдено адрес домену .com: {matches.Count}");

            if (matches.Count == 0)
            {
                Console.WriteLine("В тексті немає адрес домену .com.");
                return;
            }

            // 4. Запис знайдених адрес у новий файл
            var foundSites = matches.Cast<Match>().Select(m => m.Value).ToList();
            File.WriteAllLines(foundSitesFile, foundSites);
            Console.WriteLine($"Знайдені адреси збережено у файл: {foundSitesFile}\n");

            // Виведення знайдених адрес на екран
            for (int i = 0; i < foundSites.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {foundSites[i]}");
            }

            // 5. Вилучення або заміна за вказаними параметрами
            Console.WriteLine("\nВведіть адресу, яку хочете змінити або видалити (з тих, що вище):");
            string targetSite = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(targetSite) || !text.Contains(targetSite))
            {
                Console.WriteLine("Такої адреси не знайдено або введено порожній рядок. Завершення роботи.");
                return;
            }

            Console.WriteLine("Введіть текст для ЗАМІНИ (або натисніть Enter, щоб просто ВИДАЛИТИ цю адресу):");
            string replacementText = Console.ReadLine();

            // Виконуємо заміну або видалення
            string modifiedText = text.Replace(targetSite, replacementText);

            // 6. Запис зміненого тексту у новий файл
            File.WriteAllText(modifiedFile, modifiedText);

            Console.WriteLine("\n--- Текст успішно змінено! ---");
            if (string.IsNullOrEmpty(replacementText))
            {
                Console.WriteLine($"Адресу '{targetSite}' було вилучено.");
            }
            else
            {
                Console.WriteLine($"Адресу '{targetSite}' було замінено на '{replacementText}'.");
            }
            
            Console.WriteLine($"Результат збережено у файл: {modifiedFile}");
        }
    }
}
