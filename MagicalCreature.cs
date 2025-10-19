using System;
using System.Collections.Generic;
using System.Globalization;

public class MagicalCreature
{
    private static int _creaturesCreated = 0;
    private static double _worldMagicLevel = 100.0;

    public static int CreaturesCreated => _creaturesCreated;
    public static double WorldMagicLevel
    {
        get => _worldMagicLevel;
        set
        {
            if (value < 0) throw new ArgumentException("Рівень магії світу не може бути від'ємним");
            _worldMagicLevel = value;
        }
    }

    private string _name;
    private string _species;
    private int _age;
    private double _magicPower;
    private MagicType _magicType;
    private bool _canFly;
    private DateTime _discoveryDate;
    private int _healthPoints;
    private int _evolutionLevel;

    public string Description { get; set; } = "Таємнича магічна істота";

    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Ім'я не може бути порожнім");
            if (value.Length < 2)
                throw new ArgumentException("Ім'я має містити мінімум 2 символи");
            _name = value;
        }
    }

    public string Species
    {
        get { return _species; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Вид не може бути порожнім");
            _species = value;
        }
    }

    public int Age
    {
        get { return _age; }
        set
        {
            if (value < 0) throw new ArgumentException("Вік не може бути від'ємним");
            _age = value;
        }
    }

    public double MagicPower
    {
        get { return _magicPower; }
        set
        {
            if (value < 0) throw new ArgumentException("Магічна сила не може бути від'ємною");
            _magicPower = value;
        }
    }

    public MagicType MagicType { get; set; }
    public bool CanFly { get; set; }
    public DateTime DiscoveryDate { get; private set; }
    public int HealthPoints
    {
        get { return _healthPoints; }
        set
        {
            if (value < 0) throw new ArgumentException("Здоров'я не може бути від'ємним");
            _healthPoints = value;
        }
    }
    public int EvolutionLevel
    {
        get { return _evolutionLevel; }
        private set { _evolutionLevel = value; }
    }
    public string MagicalStatus => CalculateMagicalStatus();

    public MagicalCreature()
    {
        _name = "Безіменний";
        _species = "Невідомий вид";
        _age = 1;
        _magicPower = 10;
        _magicType = MagicType.Arcane;
        _canFly = false;
        _discoveryDate = DateTime.Now;
        _healthPoints = 100;
        _evolutionLevel = 1;
        _creaturesCreated++;
        Console.WriteLine("⚡ Викликано конструктор без параметрів!");
    }

    public MagicalCreature(string name, string species) : this()
    {
        Name = name;
        Species = species;
        Console.WriteLine("🔮 Викликано конструктор з двома параметрами (ім'я та вид)!");
    }

    public MagicalCreature(string name, string species, int age, double magicPower,
                         MagicType magicType, bool canFly, DateTime discoveryDate, int healthPoints)
    {
        Name = name;
        Species = species;
        Age = age;
        MagicPower = magicPower;
        MagicType = magicType;
        CanFly = canFly;
        DiscoveryDate = discoveryDate;
        HealthPoints = healthPoints;
        _evolutionLevel = 1;
        _creaturesCreated++;
        Console.WriteLine("✨ Викликано основний конструктор з усіма параметрами!");
    }

    public static double CalculateGlobalMagicPower()
    {
        return WorldMagicLevel * (1 + _creaturesCreated * 0.01);
    }

    public static MagicalCreature Parse(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
            throw new ArgumentException("Рядок не може бути порожнім");

        string[] parts = s.Split(';');
        if (parts.Length != 8)
            throw new FormatException("Невірний формат рядка. Очікується 8 параметрів розділених ';'");

        try
        {
            string name = parts[0].Trim();
            string species = parts[1].Trim();
            int age = int.Parse(parts[2].Trim());

            
            double magicPower = double.Parse(parts[3].Trim(), CultureInfo.InvariantCulture);

            MagicType magicType = (MagicType)Enum.Parse(typeof(MagicType), parts[4].Trim());
            bool canFly = bool.Parse(parts[5].Trim());

           
            DateTime discoveryDate = DateTime.ParseExact(parts[6].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

            int healthPoints = int.Parse(parts[7].Trim());

            return new MagicalCreature(name, species, age, magicPower, magicType, canFly, discoveryDate, healthPoints);
        }
        catch (Exception ex)
        {
            throw new FormatException($"Помилка парсингу: {ex.Message}", ex);
        }
    }

    public static bool TryParse(string s, out MagicalCreature obj)
    {
        obj = null;
        try
        {
            obj = Parse(s);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public override string ToString()
    {
        
        return $"{Name};{Species};{Age};{MagicPower.ToString(CultureInfo.InvariantCulture)};{MagicType};{CanFly};{DiscoveryDate:dd.MM.yyyy};{HealthPoints}";
    }

    public void Train(double hours) => TrainCreature(hours);

    public void Train(double hours, int intensity)
    {
        if (intensity <= 0 || intensity > 10)
            throw new ArgumentException("Інтенсивність має бути від 1 до 10");

        double powerIncrease = hours * 0.5 * (intensity * 0.5);
        _magicPower += powerIncrease;

        if (_magicPower > 10000)
            _magicPower = 10000;

        Console.WriteLine($"{_name} потренувався {hours} годин з інтенсивністю {intensity}/10. " +
                         $"Магічна сила збільшена на {powerIncrease:F1}!");
    }

    public void Train(double hours, string exerciseType)
    {
        double multiplier = exerciseType.ToLower() switch
        {
            "силове" => 1.3,
            "швидкісне" => 1.2,
            "медитація" => 0.8,
            "концентрація" => 1.1,
            _ => 1.0
        };

        double powerIncrease = hours * 0.5 * multiplier;
        _magicPower += powerIncrease;

        if (_magicPower > 10000)
            _magicPower = 10000;

        Console.WriteLine($"{_name} потренувався {hours} годин ({exerciseType}). " +
                         $"Магічна сила збільшена на {powerIncrease:F1}!");
    }

    public void Heal(int healingPoints) => HealCreature(healingPoints);

    public void Heal(int healingPoints, string potionType)
    {
        double multiplier = potionType.ToLower() switch
        {
            "велике" => 1.5,
            "середнє" => 1.2,
            "мале" => 1.0,
            _ => 1.0
        };

        int actualHealing = (int)(healingPoints * multiplier);
        _healthPoints += actualHealing;

        if (_healthPoints > 1000)
            _healthPoints = 1000;

        Console.WriteLine($"{_name} випив {potionType} зілля. Отримано {actualHealing} очків лікування. " +
                         $"Здоров'я: {_healthPoints}");
    }

    private void TrainCreature(double hours)
    {
        if (hours <= 0) throw new ArgumentException("Час тренування має бути більше 0");
        double powerIncrease = hours * 0.5;
        _magicPower += powerIncrease;
        if (_magicPower > 10000) _magicPower = 10000;
        Console.WriteLine($"{_name} потренувався {hours} годин. Магічна сила збільшена на {powerIncrease:F1}!");
    }

    private void HealCreature(int healingPoints)
    {
        if (healingPoints <= 0) throw new ArgumentException("Кількість лікування має бути більше 0");
        _healthPoints += healingPoints;
        if (_healthPoints > 1000) _healthPoints = 1000;
        Console.WriteLine($"{_name} отримав {healingPoints} очків лікування. Здоров'я: {_healthPoints}");
    }

    private string CalculateMagicalStatus()
    {
        if (_magicPower < 100) return "Початківець";
        else if (_magicPower < 500) return "Досвідчений";
        else if (_magicPower < 2000) return "Майстер";
        else return "Архімаг";
    }

    public string GetCreatureInfo() => FormatCreatureInfo();

    private string FormatCreatureInfo()
    {
        string flyStatus = _canFly ? "Так" : "Ні";
        return $"{_name} ({_species}), {_age} років, {_magicType} магія, " +
               $"Сила: {_magicPower:F1}, Літає: {flyStatus}, " +
               $"Здоров'я: {_healthPoints}, Еволюція: {_evolutionLevel}";
    }

    public void Evolve() => EvolveCreature();

    private void EvolveCreature()
    {
        if (_evolutionLevel >= 5)
            throw new InvalidOperationException($"{_name} вже досяг максимального рівня еволюції!");

        _evolutionLevel++;  
        _magicPower *= 1.2;
        _healthPoints += 50;
        Console.WriteLine($"✨ {_name} еволюціонував до рівня {_evolutionLevel}! ✨");
    }

    public bool IsAncient() => _age >= 1000;
    public int GetDaysSinceDiscovery() => (DateTime.Now - DiscoveryDate).Days;
}