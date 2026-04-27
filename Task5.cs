using System;
using System.IO;

namespace FileSystemTaskMac
{
    class Program
    {
        static void Main(string[] args)
        {
            // Адаптація для Mac: замість D:\temp використовуємо Робочий стіл
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string tempDir = Path.Combine(desktopPath, "temp");

            // Замініть "MacUser" на ваше реальне прізвище (англійською)
            string lastName = "MacUser"; 
            
            // Формуємо шляхи до папок
            string dir1 = Path.Combine(tempDir, lastName + "1");
            string dir2 = Path.Combine(tempDir, lastName + "2");

            try
            {
                // 1. Створюємо папку temp та підпапки (якщо існують - нічого не зламається)
                Directory.CreateDirectory(tempDir);
                Directory.CreateDirectory(dir1);
                Directory.CreateDirectory(dir2);
                Console.WriteLine("1. Папки успішно створено.");

                // 2. У папці 1 створюємо t1.txt та t2.txt
                string t1Path = Path.Combine(dir1, "t1.txt");
                string t2Path = Path.Combine(dir1, "t2.txt");

                // Згідно із завданням: "Текст у кутових дужках замінити відповідним чином" 
                // (тобто записуємо реальний текст без дужок)
                string text1 = "Шевченко Степан Іванович, 2001 року народження, місце проживання м. Суми\n";
                string text2 = "Комар Сергій Федорович, 2000 року народження, місце проживання м. Київ\n";

                File.WriteAllText(t1Path, text1);
                File.WriteAllText(t2Path, text2);
                Console.WriteLine("2. Файли t1.txt та t2.txt створено та заповнено даними.");

                // 3. У папці 2 створюємо t3.txt (записуємо вміст t1.txt, а потім t2.txt)
                string t3Path = Path.Combine(dir2, "t3.txt");
                string combinedText = File.ReadAllText(t1Path) + File.ReadAllText(t2Path);
                File.WriteAllText(t3Path, combinedText);
                Console.WriteLine("3. Файл t3.txt створено у папці 2 (текст успішно об'єднано).");

                // 4. Виведення розгорнутої інформації про створені файли
                Console.WriteLine("\n--- 4. Інформація про початково створені файли ---");
                PrintFileInfo(t1Path);
                PrintFileInfo(t2Path);
                PrintFileInfo(t3Path);
                Console.WriteLine("----------------------------------------------------\n");

                // 5. Перенести t2.txt у папку 2
                string t2NewPath = Path.Combine(dir2, "t2.txt");
                // Видаляємо файл у цільовій папці, якщо він там уже є (щоб уникнути помилки при повторному запуску)
                if (File.Exists(t2NewPath)) File.Delete(t2NewPath); 
                File.Move(t2Path, t2NewPath);
                Console.WriteLine("5. Файл t2.txt успішно перенесено у папку 2.");

                // 6. Скопіювати t1.txt у папку 2
                string t1CopyPath = Path.Combine(dir2, "t1.txt");
                File.Copy(t1Path, t1CopyPath, true); // true означає, що файл можна перезаписати
                Console.WriteLine("6. Файл t1.txt успішно скопійовано у папку 2.");

                // 7. Папку 2 (у завданні "К2") перейменувати в ALL, а папку 1 вилучити
                string allDir = Path.Combine(tempDir, "ALL");
                
                // Якщо папка ALL вже існує з попереднього запуску - видаляємо її
                if (Directory.Exists(allDir)) Directory.Delete(allDir, true); 
                
                // Перейменування папки реалізується через її переміщення
                Directory.Move(dir2, allDir);
                
                // Видалення папки 1 (параметр true дозволяє видалити папку навіть якщо в ній є файли)
                Directory.Delete(dir1, true); 
                Console.WriteLine("7. Папку 2 перейменовано в ALL. Папку 1 видалено.");

                // 8. Вивести повну інформацію про файли папки ALL
                Console.WriteLine("\n--- 8. Повна інформація про файли у папці ALL ---");
                string[] allFiles = Directory.GetFiles(allDir);
                foreach (string file in allFiles)
                {
                    PrintFileInfo(file);
                }
                Console.WriteLine("---------------------------------------------------");
                
                Console.WriteLine($"\n✅ Програму завершено! Перевірте результат у папці: {allDir}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Виникла помилка: {ex.Message}");
            }
        }

        // Допоміжний метод для зручного виведення інформації про файл
        static void PrintFileInfo(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            if (fi.Exists)
            {
                Console.WriteLine($"📄 Ім'я файлу: {fi.Name}");
                Console.WriteLine($"   Шлях: {fi.FullName}");
                Console.WriteLine($"   Розмір: {fi.Length} байт");
                Console.WriteLine($"   Створено: {fi.CreationTime}");
                Console.WriteLine();
            }
        }
    }
}
