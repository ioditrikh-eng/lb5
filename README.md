# lb5
# Magical Creatures Management System

C# console application for managing a collection of magical creatures with various abilities and characteristics.

## Features

- ✅ Add magical creatures with validation
- ✅ View all creatures in table format
- ✅ Search by multiple criteria
- ✅ Demonstrate magical abilities
- ✅ Delete creatures by various methods
- ✅ Input validation with retry mechanism

## Class Structure

### MagicalCreature Class
- **Properties**: Name, Species, Age, MagicPower, MagicType, CanFly, DiscoveryDate, HealthPoints
- **Methods**: CalculateBattlePower(), GetCreatureInfo(), IsAncient(), Train(), Heal(), Evolve()

### MagicType Enum
10 magical types: Fire, Water, Air, Earth, Light, Dark, Nature, Ice, Electric, Arcane

## Validation Rules
- Name: 2-25 chars, letters/spaces/hyphens only
- Species: 3-30 chars
- Age: 0-5000 years
- MagicPower: 1-1000
- HealthPoints: 1-500
- DiscoveryDate: 1000-01-01 to current date

## How to Run

1. Clone repository
2. Open in Visual Studio 2022+
3. Build solution (F6)
4. Run without debugging (Ctrl + F5)

## Technologies
- .NET 8.0
- C# 10.0
- Console Application

## Author
Illi Dietrich

## License
MIT License
