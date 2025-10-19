using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

[TestClass]
public class MagicalCreatureTests
{
    private MagicalCreature _creature;
    private static int _initialCreaturesCount;

    [TestInitialize]
    public void TestInitialize()
    {
        _initialCreaturesCount = MagicalCreature.CreaturesCreated;
        _creature = new MagicalCreature("Тестовий Дракон", "Драконід", 100, 200.0,
                                      MagicType.Fire, true, new DateTime(2020, 1, 1), 300);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _creature = null;
    }

    [TestMethod]
    public void Constructor_Default_CreatesCreatureWithDefaultValues()
    {
        var creature = new MagicalCreature();

        Assert.AreEqual("Безіменний", creature.Name);
        Assert.AreEqual("Невідомий вид", creature.Species);
        Assert.AreEqual(1, creature.Age);
        Assert.AreEqual(10.0, creature.MagicPower, 0.001); 
        Assert.AreEqual(100, creature.HealthPoints);
    }

    [TestMethod]
    public void Constructor_TwoParameters_CreatesCreatureWithNameAndSpecies()
    {
        var creature = new MagicalCreature("Фенікс", "Птах");

        Assert.AreEqual("Фенікс", creature.Name);
        Assert.AreEqual("Птах", creature.Species);
        Assert.AreEqual(1, creature.Age);
    }

    [TestMethod]
    public void Constructor_AllParameters_CreatesCreatureWithAllProperties()
    {
        string name = "Елементаль";
        string species = "Стихія";
        int age = 500;
        double magicPower = 750.5;
        MagicType magicType = MagicType.Water;
        bool canFly = false;
        DateTime discoveryDate = new DateTime(2010, 5, 15);
        int healthPoints = 450;

        var creature = new MagicalCreature(name, species, age, magicPower, magicType, canFly, discoveryDate, healthPoints);

        Assert.AreEqual(name, creature.Name);
        Assert.AreEqual(species, creature.Species);
        Assert.AreEqual(age, creature.Age);
        Assert.AreEqual(magicPower, creature.MagicPower, 0.001); 
        Assert.AreEqual(magicType, creature.MagicType);
        Assert.AreEqual(canFly, creature.CanFly);
        Assert.AreEqual(discoveryDate, creature.DiscoveryDate);
        Assert.AreEqual(healthPoints, creature.HealthPoints);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Name_SetEmptyValue_ThrowsArgumentException()
    {
        var creature = new MagicalCreature();
        creature.Name = "";
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Name_SetTooShortValue_ThrowsArgumentException()
    {
        var creature = new MagicalCreature();
        creature.Name = "A";
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(-100)]
    [ExpectedException(typeof(ArgumentException))]
    public void Age_SetNegativeValue_ThrowsArgumentException(int invalidAge)
    {
        var creature = new MagicalCreature();
        creature.Age = invalidAge;
    }

    [TestMethod]
    [DataRow(-50.5)]
    [DataRow(-0.1)]
    [ExpectedException(typeof(ArgumentException))]
    public void MagicPower_SetNegativeValue_ThrowsArgumentException(double invalidPower)
    {
        var creature = new MagicalCreature();
        creature.MagicPower = invalidPower;
    }

    [TestMethod]
    [DataRow(-10)]
    [DataRow(-1)]
    [ExpectedException(typeof(ArgumentException))]
    public void HealthPoints_SetNegativeValue_ThrowsArgumentException(int invalidHealth)
    {
        var creature = new MagicalCreature();
        creature.HealthPoints = invalidHealth;
    }

    [TestMethod]
    public void Train_WithValidHours_IncreasesMagicPower()
    {
        double initialPower = _creature.MagicPower;
        double trainingHours = 2.0;

        _creature.Train(trainingHours);

        Assert.AreEqual(initialPower + (trainingHours * 0.5), _creature.MagicPower, 0.001);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    [DataRow(-5.5)]
    [ExpectedException(typeof(ArgumentException))]
    public void Train_WithInvalidHours_ThrowsArgumentException(double invalidHours)
    {
        _creature.Train(invalidHours);
    }

    [TestMethod]
    public void Train_WithIntensity_IncreasesMagicPowerWithMultiplier()
    {
        double initialPower = _creature.MagicPower;
        double trainingHours = 2.0;
        int intensity = 5;

        _creature.Train(trainingHours, intensity);

        double expectedIncrease = trainingHours * 0.5 * (intensity * 0.5);
        Assert.AreEqual(initialPower + expectedIncrease, _creature.MagicPower, 0.001);
    }

    [TestMethod]
    [DataRow(2.0, "силове", 1.3)]
    [DataRow(1.0, "швидкісне", 1.2)]
    [DataRow(3.0, "медитація", 0.8)]
    [DataRow(2.5, "концентрація", 1.1)]
    [DataRow(1.5, "невідомий", 1.0)]
    public void Train_WithExerciseType_UsesCorrectMultiplier(double hours, string exerciseType, double expectedMultiplier)
    {
        double initialPower = _creature.MagicPower;

        _creature.Train(hours, exerciseType);

        double expectedIncrease = hours * 0.5 * expectedMultiplier;
        Assert.AreEqual(initialPower + expectedIncrease, _creature.MagicPower, 0.001);
    }

    [TestMethod]
    public void Heal_WithValidPoints_IncreasesHealthPoints()
    {
        int initialHealth = _creature.HealthPoints;
        int healingPoints = 50;

        _creature.Heal(healingPoints);

        Assert.AreEqual(initialHealth + healingPoints, _creature.HealthPoints);
    }

    [TestMethod]
    [DataRow(50, "велике", 75)]
    [DataRow(100, "середнє", 120)]
    [DataRow(30, "мале", 30)]
    [DataRow(40, "невідомий", 40)]
    public void Heal_WithPotionType_UsesCorrectMultiplier(int baseHealing, string potionType, int expectedHealing)
    {
        int initialHealth = _creature.HealthPoints;

        _creature.Heal(baseHealing, potionType);

        Assert.AreEqual(initialHealth + expectedHealing, _creature.HealthPoints);
    }

    [TestMethod]
    public void Heal_ExceedsMaxHealth_CapsAtMaximum()
    {
        _creature.HealthPoints = 950;
        int healingPoints = 100;

        _creature.Heal(healingPoints);

        Assert.AreEqual(1000, _creature.HealthPoints);
    }

    [TestMethod]
    public void Evolve_ValidEvolution_IncreasesLevelAndStats()
    {
        int initialLevel = _creature.EvolutionLevel;
        double initialPower = _creature.MagicPower;
        int initialHealth = _creature.HealthPoints;

        _creature.Evolve();

        Assert.AreEqual(initialLevel + 1, _creature.EvolutionLevel);
        Assert.AreEqual(initialPower * 1.2, _creature.MagicPower, 0.001);
        Assert.AreEqual(initialHealth + 50, _creature.HealthPoints);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Evolve_MaxLevelReached_ThrowsException()
    {
        var maxLevelCreature = new MagicalCreature("Максимальний", "Вид", 1000, 5000,
                                                 MagicType.Arcane, true, DateTime.Now, 1000);

        for (int i = 0; i < 4; i++)
        {
            maxLevelCreature.Evolve();
        }

        maxLevelCreature.Evolve();
    }

    [TestMethod]
    public void CreaturesCreated_AfterCreatingObjects_ReturnsCorrectCount()
    {
        int initialCount = MagicalCreature.CreaturesCreated;

        var creature1 = new MagicalCreature();
        var creature2 = new MagicalCreature("Тест", "Вид");

        Assert.AreEqual(initialCount + 2, MagicalCreature.CreaturesCreated);
    }

    [TestMethod]
    public void WorldMagicLevel_SetValidValue_UpdatesCorrectly()
    {
        double newLevel = 250.5;

        MagicalCreature.WorldMagicLevel = newLevel;

        Assert.AreEqual(newLevel, MagicalCreature.WorldMagicLevel, 0.001); 
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void WorldMagicLevel_SetNegativeValue_ThrowsException()
    {
        MagicalCreature.WorldMagicLevel = -50.0;
    }

    [TestMethod]
    public void CalculateGlobalMagicPower_WithCreatures_ReturnsCorrectValue()
    {
        MagicalCreature.WorldMagicLevel = 100.0;
        int creaturesCount = MagicalCreature.CreaturesCreated;

        double globalPower = MagicalCreature.CalculateGlobalMagicPower();

        double expectedPower = 100.0 * (1 + creaturesCount * 0.01);
        Assert.AreEqual(expectedPower, globalPower, 0.001);
    }

    [TestMethod]
    public void Parse_ValidString_ReturnsMagicalCreature()
    {
        
        string validString = "Гном;Гномід;250;350.5;Earth;False;20.05.2015;280";

        var creature = MagicalCreature.Parse(validString);

        Assert.IsNotNull(creature);
        Assert.AreEqual("Гном", creature.Name);
        Assert.AreEqual("Гномід", creature.Species);
        Assert.AreEqual(250, creature.Age);
        Assert.AreEqual(350.5, creature.MagicPower, 0.001); 
        Assert.AreEqual(MagicType.Earth, creature.MagicType);
        Assert.IsFalse(creature.CanFly);
        Assert.AreEqual(280, creature.HealthPoints);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Parse_EmptyString_ThrowsException()
    {
        MagicalCreature.Parse("");
    }

    [TestMethod]
    [ExpectedException(typeof(FormatException))]
    public void Parse_InvalidFormatString_ThrowsException()
    {
        MagicalCreature.Parse("Некоректний;рядок;без;достатньо;частин");
    }

    [TestMethod]
    [ExpectedException(typeof(FormatException))]
    public void Parse_InvalidDataTypes_ThrowsException()
    {
        MagicalCreature.Parse("Ім'я;Вид;не_число;не_число;Fire;True;01.01.2020;100");
    }

    [TestMethod]
    public void TryParse_ValidString_ReturnsTrueAndCreature()
    {
        
        string validString = "Ельф;Ельфійський;150;420.7;Nature;True;10.10.2018;320";

        bool result = MagicalCreature.TryParse(validString, out MagicalCreature creature);

        Assert.IsTrue(result);
        Assert.IsNotNull(creature);
        Assert.AreEqual("Ельф", creature.Name);
    }

    [TestMethod]
    public void TryParse_InvalidString_ReturnsFalseAndNull()
    {
        string invalidString = "Невірний;формат;abc;def";

        bool result = MagicalCreature.TryParse(invalidString, out MagicalCreature creature);

        Assert.IsFalse(result);
        Assert.IsNull(creature);
    }

    [TestMethod]
    public void ToString_ValidCreature_ReturnsCorrectFormat()
    {
        var creature = new MagicalCreature("Унікорн", "Міфічний", 300, 550.0,
                                         MagicType.Light, true, new DateTime(2012, 6, 15), 400);

        string result = creature.ToString();

        
        string expected = "Унікорн;Міфічний;300;550;Light;True;15.06.2012;400";
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DataRow(50, false)]
    [DataRow(999, false)]
    [DataRow(1000, true)]
    [DataRow(1500, true)]
    public void IsAncient_WithDifferentAges_ReturnsCorrectResult(int age, bool expected)
    {
        var creature = new MagicalCreature("Тест", "Вид", age, 100, MagicType.Fire, false, DateTime.Now, 100);

        bool result = creature.IsAncient();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetDaysSinceDiscovery_ValidDate_ReturnsPositiveNumber()
    {
        var discoveryDate = DateTime.Now.AddDays(-30);
        var creature = new MagicalCreature("Тест", "Вид", 100, 100, MagicType.Fire, false, discoveryDate, 100);

        int days = creature.GetDaysSinceDiscovery();

        Assert.IsTrue(days >= 29 && days <= 31);
    }

    [TestMethod]
    [DataRow(50.0, "Початківець")]
    [DataRow(99.9, "Початківець")]
    [DataRow(100.0, "Досвідчений")]
    [DataRow(250.0, "Досвідчений")]
    [DataRow(499.9, "Досвідчений")]
    [DataRow(500.0, "Майстер")]
    [DataRow(1500.0, "Майстер")]
    [DataRow(1999.9, "Майстер")]
    [DataRow(2000.0, "Архімаг")]
    [DataRow(5000.0, "Архімаг")]
    public void MagicalStatus_WithDifferentPowerLevels_ReturnsCorrectStatus(double power, string expectedStatus)
    {
        var creature = new MagicalCreature("Тест", "Вид", 100, power, MagicType.Fire, false, DateTime.Now, 100);

        string status = creature.MagicalStatus;

        Assert.AreEqual(expectedStatus, status);
    }

    [TestMethod]
    public void GetCreatureInfo_ValidCreature_ReturnsFormattedString()
    {
        var creature = new MagicalCreature("Дракон", "Драконід", 500, 750.5, MagicType.Fire, true, DateTime.Now, 450);

        string info = creature.GetCreatureInfo();

        Console.WriteLine($"DEBUG: {info}");

        
        Assert.IsTrue(info.Contains("Дракон"));
        Assert.IsTrue(info.Contains("Драконід"));
        Assert.IsTrue(info.Contains("500"));
        Assert.IsTrue(info.Contains("Fire"));
        Assert.IsTrue(info.Contains("750")); 

        
        bool hasFlyInfo = info.Contains("Літає: Так") || info.Contains("Літає: Ні");
        Assert.IsTrue(hasFlyInfo, "Должно содержать информацию о полете");

        Assert.IsTrue(info.Contains("450")); 
    }

}