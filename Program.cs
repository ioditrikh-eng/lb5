using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    private static List<MagicalCreature> creatures = new List<MagicalCreature>();
    private static int maxCreatures = 0;

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        InitializeProgram();
        ShowMainMenu();
    }

    static void InitializeProgram()
    {
        Console.WriteLine("=== Система Керування Магічними Істотами ===");
        maxCreatures = ReadPositiveInteger("Введіть максимальну кількість істот для керування (N > 0): ");
        Console.WriteLine($"Максимальна кількість істот встановлена: {maxCreatures}");
    }

    static void ShowMainMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== ГОЛОВНЕ МЕНЮ ===");
            Console.WriteLine("1 - Додати магічну істоту");
            Console.WriteLine("2 - Переглянути всі істоти");
            Console.WriteLine("3 - Знайти істоту");
            Console.WriteLine("4 - Продемонструвати магічні здібності");
            Console.WriteLine("5 - Видалити істоту");
            Console.WriteLine("6 - Продемонструвати static-методи");
            Console.WriteLine("0 - Вийти з програми");

            string choice = ReadString("Оберіть опцію: ");

            switch (choice)
            {
                case "1": AddCreature(); break;
                case "2": ViewAllCreatures(); break;
                case "3": FindCreature(); break;
                case "4": DemonstrateAbilities(); break;
                case "5": DeleteCreature(); break;
                case "6": DemonstrateStaticMethods(); break;
                case "0": return;
                default: Console.WriteLine("Некоректна опція!"); break;
            }
        }
    }

    
    static string ReadString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine()!.Trim();
    }

    static int ReadInteger(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int result))
                return result;
            Console.WriteLine("Некоректне введення! Будь ласка, введіть ціле число.");
        }
    }

    static int ReadPositiveInteger(string prompt)
    {
        while (true)
        {
            int result = ReadInteger(prompt);
            if (result > 0) return result;
            Console.WriteLine("Будь ласка, введіть ціле додатне число.");
        }
    }

    static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out double result))
                return result;
            Console.WriteLine("Некоректне введення! Будь ласка, введіть число.");
        }
    }

    static int ReadIntegerInRange(string prompt, int min, int max)
    {
        while (true)
        {
            int result = ReadInteger(prompt);
            if (result >= min && result <= max) return result;
            Console.WriteLine($"Значення має бути між {min} і {max}.");
        }
    }

    static double ReadDoubleInRange(string prompt, double min, double max)
    {
        while (true)
        {
            double result = ReadDouble(prompt);
            if (result >= min && result <= max) return result;
            Console.WriteLine($"Значення має бути між {min} і {max}.");
        }
    }

    static DateTime ReadDateTime(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (DateTime.TryParse(Console.ReadLine(), out DateTime result))
                return result;
            Console.WriteLine("Некоректний формат дати! Використовуйте dd.MM.yyyy");
        }
    }

    static DateTime ReadDateTimeInRange(string prompt, DateTime min, DateTime max)
    {
        while (true)
        {
            DateTime result = ReadDateTime(prompt);
            if (result >= min && result <= max) return result;
            Console.WriteLine($"Дата має бути між {min:dd.MM.yyyy} і {max:dd.MM.yyyy}.");
        }
    }

    static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine()!.ToLower().Trim();
            if (input == "y" || input == "так") return true;
            if (input == "n" || input == "ні") return false;
            Console.WriteLine("Будь ласка, введіть 'y' для так або 'n' для ні.");
        }
    }

    static MagicType ReadMagicType(string prompt)
    {
        while (true)
        {
            Console.WriteLine("Доступні типи магії: ");
            for (int i = 0; i < Enum.GetValues(typeof(MagicType)).Length; i++)
                Console.WriteLine($"- {(MagicType)i} ({i})");

            Console.Write(prompt);
            if (Enum.TryParse(Console.ReadLine(), out MagicType result) &&
                Enum.IsDefined(typeof(MagicType), result))
                return result;

            Console.WriteLine("Некоректний тип магії! Будь ласка, введіть вірний номер.");
        }
    }

  
    static void AddCreature()
    {
        if (creatures.Count >= maxCreatures)
        {
            Console.WriteLine($"Неможливо додати більше істот. Досягнуто максимальний ліміт ({maxCreatures}).");
            return;
        }

        Console.WriteLine("\n=== ДОДАТИ НОВУ МАГІЧНУ ІСТОТУ ===");
        Console.WriteLine("Способи додавання:");
        Console.WriteLine("1 - Звичайне створення (конструктори)");
        Console.WriteLine("2 - З рядка (TryParse)");

        string choice = ReadString("Оберіть спосіб: ");

        try
        {
            if (choice == "2")
            {
                AddCreatureFromString();
            }
            else
            {
                AddCreatureWithConstructor();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Магія не вдалася: {ex.Message}");
        }
    }

    static void AddCreatureWithConstructor()
    {
        int constructorType = new Random().Next(1, 4);
        MagicalCreature newCreature;

        switch (constructorType)
        {
            case 1:
                Console.WriteLine("Створення істоти конструктором без параметрів...");
                newCreature = new MagicalCreature();
                break;

            case 2:
                Console.WriteLine("Створення істоти конструктором з двома параметрами...");
                string name = ReadString("Введіть ім'я істоти: ");
                string species = ReadString("Введіть вид: ");
                newCreature = new MagicalCreature(name, species);
                break;

            case 3:
                Console.WriteLine("Створення істоти основним конструктором...");
                string fullName = ReadString("Введіть ім'я істоти: ");
                string fullSpecies = ReadString("Введіть вид: ");
                int age = ReadIntegerInRange("Введіть вік (роки): ", 0, 5000);
                double magicPower = ReadDoubleInRange("Введіть магічну силу (1-1000): ", 1, 1000);
                MagicType magicType = ReadMagicType("Введіть номер типу магії: ");
                bool canFly = ReadYesNo("Вміє літати? (y/n): ");
                DateTime discoveryDate = ReadDateTimeInRange(
                    "Введіть дату відкриття (dd.MM.yyyy): ",
                    new DateTime(1000, 1, 1),
                    DateTime.Now
                );
                int healthPoints = ReadIntegerInRange("Введіть очки здоров'я (1-500): ", 1, 500);

                newCreature = new MagicalCreature(fullName, fullSpecies, age, magicPower,
                                                magicType, canFly, discoveryDate, healthPoints);
                break;

            default:
                throw new InvalidOperationException("Невірний тип конструктора");
        }

        creatures.Add(newCreature);
        Console.WriteLine($"✨ {newCreature.Name} було додано до вашої колекції! ✨");
    }

    static void AddCreatureFromString()
    {
        Console.WriteLine("\nФормат рядка: Ім'я;Вид;Вік;Магічна_сила;Тип_магії;Може_літати;Дата_відкриття;Здоров'я");
        Console.WriteLine("Приклад: Дракон;Драконід;150;450;Fire;True;15.03.2020;300");

        string input = ReadString("Введіть рядок: ");

        if (MagicalCreature.TryParse(input, out MagicalCreature newCreature))
        {
            creatures.Add(newCreature);
            Console.WriteLine($"✨ {newCreature.Name} було додано з рядка! ✨");
        }
        else
        {
            Console.WriteLine("❌ Не вдалося створити істоту з введеного рядка. Перевірте формат.");
        }
    }

    static void ViewAllCreatures()
    {
        if (creatures.Count == 0)
        {
            Console.WriteLine("У вашій колекції немає магічних істот.");
        }
        else
        {
            Console.WriteLine("\n=== ВАША МАГІЧНА КОЛЕКЦІЯ ===");
            for (int i = 0; i < creatures.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {creatures[i].GetCreatureInfo()}");
            }
        }

       
        Console.WriteLine($"\n📊 Статистика:");
        Console.WriteLine($"Створено істот: {MagicalCreature.CreaturesCreated}");
        Console.WriteLine($"Рівень магії світу: {MagicalCreature.WorldMagicLevel:F1}");
        Console.WriteLine($"Глобальна магічна сила: {MagicalCreature.CalculateGlobalMagicPower():F1}");
    }

    static void DemonstrateStaticMethods()
    {
        Console.WriteLine("\n=== ДЕМОНСТРАЦІЯ STATIC-МЕТОДІВ ===");

      
        Console.WriteLine($"\n📊 Поточний стан магічного світу:");
        Console.WriteLine($"Створено істот: {MagicalCreature.CreaturesCreated}");
        Console.WriteLine($"Рівень магії світу: {MagicalCreature.WorldMagicLevel:F1}");
        Console.WriteLine($"Глобальна магічна сила: {MagicalCreature.CalculateGlobalMagicPower():F1}");

    
        Console.WriteLine($"\n🔮 Демонстрація методів Parse та TryParse:");

      
        string correctString = "Фенікс;Птах;500;800;Fire;True;01.01.2020;400";
        Console.WriteLine($"Коректний рядок: {correctString}");

        try
        {
            MagicalCreature parsedCreature = MagicalCreature.Parse(correctString);
            Console.WriteLine($"✅ Parse успішний: {parsedCreature.Name}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Parse помилка: {ex.Message}");
        }

       
        string incorrectString = "Невірний;Формат;abc;def";
        Console.WriteLine($"\nНекоректний рядок: {incorrectString}");

        if (MagicalCreature.TryParse(incorrectString, out MagicalCreature tryParseCreature))
        {
            Console.WriteLine($"✅ TryParse успішний: {tryParseCreature.Name}");
        }
        else
        {
            Console.WriteLine($"❌ TryParse не вдався - некоректний формат");
        }

      
        if (creatures.Count > 0)
        {
            Console.WriteLine($"\n📝 Демонстрація ToString():");
            Console.WriteLine($"Перша істота у рядковому форматі: {creatures[0]}");
        }

      
        Console.WriteLine($"\n⚡ Зміна рівня магії світу:");
        double newLevel = ReadDoubleInRange("Введіть новий рівень магії світу (0-1000): ", 0, 1000);
        MagicalCreature.WorldMagicLevel = newLevel;
        Console.WriteLine($"Новий рівень магії світу: {MagicalCreature.WorldMagicLevel:F1}");
        Console.WriteLine($"Нова глобальна магічна сила: {MagicalCreature.CalculateGlobalMagicPower():F1}");
    }

    static void FindCreature()
    {
        if (creatures.Count == 0)
        {
            Console.WriteLine("Немає істот для пошуку.");
            return;
        }

        Console.WriteLine("\n=== ПОШУК МАГІЧНОЇ ІСТОТИ ===");
        string name = ReadString("Введіть ім'я для пошуку: ");
        var results = creatures.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

        if (results.Count == 0)
        {
            Console.WriteLine("Магічних істот не знайдено.");
            return;
        }

        Console.WriteLine($"🔮 Знайдено {results.Count} магічну істоту(и):");
        foreach (var creature in results)
        {
            Console.WriteLine($"• {creature.GetCreatureInfo()}");
        }
    }

    static void DemonstrateAbilities()
    {
        if (creatures.Count == 0)
        {
            Console.WriteLine("Немає істот для демонстрації.");
            return;
        }

        Console.WriteLine("\n=== ДЕМОНСТРАЦІЯ МАГІЧНИХ ЗДІБНОСТЕЙ ===");
        ViewAllCreatures();

        int creatureNumber = ReadIntegerInRange("Оберіть номер істоти для демонстрації: ", 1, creatures.Count);
        MagicalCreature selectedCreature = creatures[creatureNumber - 1];

        Console.WriteLine($"\n✨ Магічні здібності {selectedCreature.Name} ✨");
        Console.WriteLine("1 - Звичайне тренування");
        Console.WriteLine("2 - Тренування з інтенсивністю");
        Console.WriteLine("3 - Тренування з типом вправи");
        Console.WriteLine("4 - Звичайне лікування");
        Console.WriteLine("5 - Лікування зіллям");
        Console.WriteLine("6 - Еволюціонувати істоту");
        Console.WriteLine("7 - Інформація про істоту");

        string choice = ReadString("Оберіть дію: ");

        try
        {
            switch (choice)
            {
                case "1":
                    double hours = ReadDoubleInRange("Введіть години тренування (0.5-24): ", 0.5, 24);
                    selectedCreature.Train(hours);
                    break;

                case "2":
                    double hoursIntense = ReadDoubleInRange("Введіть години тренування (0.5-24): ", 0.5, 24);
                    int intensity = ReadIntegerInRange("Введіть інтенсивність (1-10): ", 1, 10);
                    selectedCreature.Train(hoursIntense, intensity);
                    break;

                case "3":
                    double hoursExercise = ReadDoubleInRange("Введіть години тренування (0.5-24): ", 0.5, 24);
                    string exerciseType = ReadString("Введіть тип вправи (силове/швидкісне/медитація/концентрація): ");
                    selectedCreature.Train(hoursExercise, exerciseType);
                    break;

                case "4":
                    int healingPoints = ReadIntegerInRange("Введіть очки лікування (1-100): ", 1, 100);
                    selectedCreature.Heal(healingPoints);
                    break;

                case "5":
                    int potionHealing = ReadIntegerInRange("Введіть очки лікування (1-100): ", 1, 100);
                    string potionType = ReadString("Введіть тип зілля (велике/середнє/мале): ");
                    selectedCreature.Heal(potionHealing, potionType);
                    break;

                case "6":
                    selectedCreature.Evolve();
                    break;

                case "7":
                    Console.WriteLine($"📜 {selectedCreature.GetCreatureInfo()}");
                    break;

                default:
                    Console.WriteLine("Некоректна дія!");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Магія не вдалася: {ex.Message}");
        }
    }

    static void DeleteCreature()
    {
        if (creatures.Count == 0)
        {
            Console.WriteLine("Немає істот для видалення.");
            return;
        }

        Console.WriteLine("\n=== ВИДАЛЕННЯ МАГІЧНОЇ ІСТОТИ ===");
        ViewAllCreatures();
        int number = ReadIntegerInRange("Введіть номер істоти для видалення: ", 1, creatures.Count);
        string name = creatures[number - 1].Name;
        creatures.RemoveAt(number - 1);
        Console.WriteLine($"✨ {name} повернувся до магічного світу! ✨");
    }
}