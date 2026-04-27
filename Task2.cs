using System;
using System.IO;
using System.Text.RegularExpressions;

namespace RemoveUkrainianVowelWords
{
    class Program
    {
        static void Main(string[] args)
        {
            // Шляхи до файлів
            string inputFile = "input2.txt";
            string outputFile = "output_result.txt";

            // Перевірка наявності вхідного файлу
            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"Помилка: Файл {inputFile} не знайдено!");
                return;
            }

            // 1. Читання тексту з файлу
            string text = File.ReadAllText(inputFile);
            Console.WriteLine("--- Оригінальний текст ---");
            Console.WriteLine(text);
            Console.WriteLine("--------------------------\n");

            // 2. Регулярний вираз для пошуку українських слів на голосну літеру
            // [аеєиіїоуюяАЕЄИІЇОУЮЯ] - перша літера (тільки українські голосні)
            // [а-яА-ЯґҐєЄіІїЇ'’]* - решта літер слова (включно з апострофом)
            // \b - межа слова
            string pattern = @"\b[аеєиіїоуюяАЕЄИІЇОУЮЯ][а-яА-ЯґҐєЄіІїЇ'’]*\b";

            // 3. Видалення знайдених слів (заміна на порожній рядок)
            string modifiedText = Regex.Replace(text, pattern, "");

            // (Опціонально) Очищення зайвих пробілів, які могли залишитися після видалення слів
            modifiedText = Regex.Replace(modifiedText, @"[ ]{2,}", " ");
            
            // 4. Запис зміненого тексту у новий файл
            File.WriteAllText(outputFile, modifiedText);

            Console.WriteLine("--- Результат після видалення ---");
            Console.WriteLine(modifiedText);
            Console.WriteLine("---------------------------------\n");
            
            Console.WriteLine($"Результат успішно збережено у файл: {outputFile}");
        }
    }
}
