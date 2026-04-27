using System;
using System.IO;
using System.Text.RegularExpressions;

namespace RemoveDuplicateFirstLetters
{
    class Program
    {
        static void Main(string[] args)
        {
            // Шляхи до файлів
            string inputFile = "input3.txt";
            string outputFile = "output_result3.txt";

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

            // 2. Регулярний вираз для пошуку слів. 
            // \p{L} - це будь-яка літера (українська, англійська тощо), ' - апостроф.
            string pattern = @"[\p{L}']+";

            // 3. Обробка тексту: замінюємо кожне знайдене слово за допомогою власної логіки
            string modifiedText = Regex.Replace(text, pattern, match =>
            {
                string word = match.Value;

                // Якщо слово складається з 1 літери, змінювати нічого не потрібно
                if (word.Length <= 1) return word;

                // Беремо першу літеру
                char firstChar = word[0];
                string firstCharStr = firstChar.ToString();

                // Відокремлюємо решту слова
                string restOfWord = word.Substring(1);

                // Видаляємо всі входження першої літери з решти слова (ігноруючи регістр)
                string replacedRest = Regex.Replace(
                    restOfWord, 
                    Regex.Escape(firstCharStr), 
                    "", 
                    RegexOptions.IgnoreCase
                );

                // Збираємо слово назад: перша літера + очищена решта слова
                return firstCharStr + replacedRest;
            });

            // 4. Запис зміненого тексту у новий файл
            File.WriteAllText(outputFile, modifiedText);

            Console.WriteLine("--- Результат після обробки ---");
            Console.WriteLine(modifiedText);
            Console.WriteLine("---------------------------------\n");
            
            Console.WriteLine($"Результат успішно збережено у файл: {outputFile}");
        }
    }
}
