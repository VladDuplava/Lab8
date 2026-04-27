using System;
using System.IO;

namespace BinaryFilesTask
{
    class Program
    {
        static void Main(string[] args)
        {
            // Шлях до двійкового файлу (розширення може бути будь-яким, часто використовують .bin або .dat)
            string filePath = "positive_numbers.bin";

            // Дана послідовність із n цілих чисел 
            // (для прикладу задамо масив одразу в коді, але можна зробити і введення з клавіатури)
            int[] sequence = { -10, 15, 0, 42, -7, 8, -1, 99, -33, 4 };

            Console.WriteLine("--- Початкова послідовність ---");
            Console.WriteLine(string.Join(", ", sequence));
            Console.WriteLine("-------------------------------\n");

            // 1. Створення файлу і запис у нього всіх додатних чисел
            // FileMode.Create створює новий файл або перезаписує існуючий
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                foreach (int number in sequence)
                {
                    // Перевіряємо, чи число додатне (більше нуля)
                    if (number > 0)
                    {
                        writer.Write(number); // Записуємо число у двійковому форматі
                    }
                }
            }
            
            Console.WriteLine($"Додатні числа успішно записано у двійковий файл: {filePath}\n");

            // 2. Читання вмісту двійкового файлу та виведення на екран
            Console.WriteLine("--- Вміст двійкового файлу ---");
            
            if (File.Exists(filePath))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
                {
                    // Читаємо дані, поки позиція курсора не досягне кінця файлу
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        // Зчитуємо 4 байти і перетворюємо їх назад у тип int
                        int readNumber = reader.ReadInt32();
                        Console.Write(readNumber + " ");
                    }
                    Console.WriteLine(); // Перехід на новий рядок після виведення всіх чисел
                }
            }
            else
            {
                Console.WriteLine("Файл не знайдено!");
            }
            
            Console.WriteLine("-------------------------------\n");
        }
    }
}
