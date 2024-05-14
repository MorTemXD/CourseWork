using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        using (var db = new ApplicationDbContext())
        {
            db.Database.EnsureCreated();

            while (true)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine("1. Додати гру");
                Console.WriteLine("2. Додати графічний пакет");
                Console.WriteLine("3. Видалити гру");
                Console.WriteLine("4. Видалити графічний пакет");
                Console.WriteLine("5. Редагувати гру");
                Console.WriteLine("6. Редагувати графічний пакет");
                Console.WriteLine("7. Ігри відсортовані за ціною в порядку зростання:");
                Console.WriteLine("8. Ігри за розробником");
                Console.WriteLine("9. Графічні пакети відсортовані за ціною в порядку зростання");
                Console.WriteLine("10. Пакети графіки за виробником");
                Console.WriteLine("11. Обчислити вартість ліцензії гри");
                Console.WriteLine("12. Обчислити вартість ліцензії графічного пакету");
                Console.WriteLine("13. Переглянути усі гри");
                Console.WriteLine("14. Переглянути усі графічні пакети");
                Console.WriteLine("15. Вийти");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddGame(db);
                        break;
                    case "2":
                        AddGraphicsPackage(db);
                        break;
                    case "3":
                        DeleteGame(db);
                        break;
                    case "4":
                        DeleteGraphicsPackage(db);
                        break;
                    case "5":
                        EditGame(db);
                        break;
                    case "6":
                        EditGraphicsPackage(db);
                        break;
                    case "7":
                        ViewGamesSortedByPrice(db);
                        break;
                    case "8":
                        ViewGamesByDeveloper(db);
                        break;
                    case "9":
                        ViewGraphicsPackagesSortedByPrice(db);
                        break;
                    case "10":
                        ViewGraphicsPackagesByManufacturer(db);
                        break;
                    case "11":
                        CalculateGameLicenseCost(db);
                        break;
                    case "12":
                        CalculateGraphicsPackageLicenseCost(db);
                        break;
                    case "13":
                        ViewAllGames(db);
                        break;
                    case "14":
                        ViewAllGraphicsPackages(db);
                        break;
                    case "15":
                        return;
                }
            }
        }
    }

    static void AddGame(ApplicationDbContext db)
    {
        Console.Write("Введіть назву гри: ");
        var name = Console.ReadLine();

        Console.Write("Введіть розробника гри: ");
        var developer = Console.ReadLine();

        Console.Write("Введіть платформу гри: ");
        var platform = Console.ReadLine();

        Console.Write("Введіть тип ліцензії (Дворічна/Трирічна): ");
        var licenseType = Console.ReadLine() == "Дворічна" ? LicenseType.Дворічна : LicenseType.Трирічна;

        Console.Write("Введіть ціну гри на рік: ");
        var pricePerYear = decimal.Parse(Console.ReadLine());

        db.Games.Add(new Game { Name = name, Developer = developer, Platform = platform, LicenseType = licenseType, PricePerYear = pricePerYear });
        db.SaveChanges();
    }

    static void AddGraphicsPackage(ApplicationDbContext db)
    {
        Console.Write("Введіть назву графічного пакету: ");
        var name = Console.ReadLine();

        Console.Write("Введіть виробника графічного пакету: ");
        var manufacturer = Console.ReadLine();

        Console.Write("Введіть тип графіки (Векторна/Растрова/Обидва): ");
        var graphicsTypeInput = Console.ReadLine();
        var graphicsType = graphicsTypeInput == "Векторна" ? GraphicsType.Векторна : (graphicsTypeInput == "Растрова" ? GraphicsType.Растрова : GraphicsType.Обидва);

        Console.Write("Введіть ціну графічного пакету на рік: ");
        var pricePerYear = decimal.Parse(Console.ReadLine());

        db.GraphicsPackages.Add(new GraphicsPackage { Name = name, Manufacturer = manufacturer, GraphicsType = graphicsType, PricePerYear = pricePerYear });
        db.SaveChanges();
    }

    static void DeleteGame(ApplicationDbContext db)
    {
        Console.Write("Введіть id гри для видалення: ");
        var id = int.Parse(Console.ReadLine());

        var game = db.Games.FirstOrDefault(e => e.Id == id);

        if (game != null)
        {
            db.Games.Remove(game);
            db.SaveChanges();
        }
        else
        {
            Console.WriteLine("Гру з таким id не знайдено.");
        }
    }

    static void DeleteGraphicsPackage(ApplicationDbContext db)
    {
        Console.Write("Введіть id графічного пакету для видалення: ");
        var id = int.Parse(Console.ReadLine());

        var graphicsPackage = db.GraphicsPackages.FirstOrDefault(e => e.Id == id);

        if (graphicsPackage != null)
        {
            db.GraphicsPackages.Remove(graphicsPackage);
            db.SaveChanges();
        }
        else
        {
            Console.WriteLine("Графічного пакету з таким id не знайдено.");
        }
    }

    static void EditGame(ApplicationDbContext db)
    {
        Console.Write("Введіть id гри для редагування: ");
        var id = int.Parse(Console.ReadLine());

        var game = db.Games.FirstOrDefault(e => e.Id == id);

        if (game != null)
        {
            Console.Write("Введіть нову назву гри: ");
            game.Name = Console.ReadLine();

            Console.Write("Введіть нового розробника гри: ");
            game.Developer = Console.ReadLine();

            Console.Write("Введіть нову платформу гри: ");
            game.Platform = Console.ReadLine();

            Console.Write("Введіть новий тип ліцензії (Дворічна/Трирічна): ");
            game.LicenseType = Console.ReadLine() == "Дворічна" ? LicenseType.Дворічна : LicenseType.Трирічна;

            Console.Write("Введіть нову ціну гри на рік: ");
            game.PricePerYear = decimal.Parse(Console.ReadLine());

            db.SaveChanges();
        }
        else
        {
            Console.WriteLine("Гру з таким id не знайдено.");
        }
    }

    static void EditGraphicsPackage(ApplicationDbContext db)
    {
        Console.Write("Введіть id графічного пакету для редагування: ");
        var id = int.Parse(Console.ReadLine());

        var graphicsPackage = db.GraphicsPackages.FirstOrDefault(e => e.Id == id);

        if (graphicsPackage != null)
        {
            Console.Write("Введіть нову назву графічного пакету: ");
            graphicsPackage.Name = Console.ReadLine();

            Console.Write("Введіть нового виробника графічного пакету: ");
            graphicsPackage.Manufacturer = Console.ReadLine();

            Console.Write("Введіть новий тип графіки (Векторна/Растрова/Обидва): ");
            graphicsPackage.GraphicsType = Console.ReadLine() == "Векторна" ? GraphicsType.Векторна : (Console.ReadLine() == "Растрова" ? GraphicsType.Растрова : GraphicsType.Обидва);

            Console.Write("Введіть нову ціну графічного пакету на рік: ");
            graphicsPackage.PricePerYear = decimal.Parse(Console.ReadLine());

            db.SaveChanges();
        }
        else
        {
            Console.WriteLine("Пакет графіки з таким id не знайдено.");
        }
    }

    static void ViewGamesSortedByPrice(ApplicationDbContext db)
    {
        Console.WriteLine("Ігри відсортовані за ціною в порядку зростання:");
        foreach (var game in db.Games.OrderBy(e => e.PricePerYear))
        {
            Console.WriteLine($"Id: {game.Id}, Назва: {game.Name}, Розробник: {game.Developer}, Платформа: {game.Platform}, Тип ліцензії: {game.LicenseType}, Ціна на рік: {game.PricePerYear}");
        }
    }

    static void ViewGamesByDeveloper(ApplicationDbContext db)
    {
        Console.Write("Введіть розробника гри: ");
        var developer = Console.ReadLine();

        var games = db.Games.Where(e => e.Developer == developer).ToList();

        if (games.Count == 0)
        {
            Console.WriteLine("Гри з таким розробником не знайдено.");
        }
        else
        {
            foreach (var game in games)
            {
                Console.WriteLine($"Id: {game.Id}, Назва: {game.Name}, Платформа: {game.Platform}, Тип ліцензії: {game.LicenseType}, Ціна на рік: {game.PricePerYear}");
            }
        }
    }

    static void ViewGraphicsPackagesSortedByPrice(ApplicationDbContext db)
    {
        Console.WriteLine("Графічні пакети відсортовані за ціною в порядку зростання:");
        foreach (var graphicsPackage in db.GraphicsPackages.OrderBy(e => e.PricePerYear))
        {
            Console.WriteLine($"Id: {graphicsPackage.Id}, Назва: {graphicsPackage.Name}, Виробник: {graphicsPackage.Manufacturer}, Тип графіки: {graphicsPackage.GraphicsType}, Ціна на рік: {graphicsPackage.PricePerYear}");
        }
    }

    static void ViewGraphicsPackagesByManufacturer(ApplicationDbContext db)
    {
        Console.Write("Введіть виробника графічного пакету: ");
        var manufacturer = Console.ReadLine();

        var graphicsPackages = db.GraphicsPackages.Where(e => e.Manufacturer == manufacturer).ToList();

        if (graphicsPackages.Count == 0)
        {
            Console.WriteLine("Графічні пакети з таким виробником не знайдено.");
        }
        else
        {
            foreach (var graphicsPackage in graphicsPackages)
            {
                Console.WriteLine($"Id: {graphicsPackage.Id}, Назва: {graphicsPackage.Name}, Тип графіки: {graphicsPackage.GraphicsType}, Ціна на рік: {graphicsPackage.PricePerYear}");
            }
        }
    }

    static void CalculateGameLicenseCost(ApplicationDbContext db)
    {
        Console.Write("Введіть id гри для обчислення вартості ліцензії: ");
        var id = int.Parse(Console.ReadLine());

        var game = db.Games.FirstOrDefault(e => e.Id == id);

        if (game != null)
        {
            Console.Write("Чи ви студент? (так/ні): ");
            var isStudent = Console.ReadLine().ToLower() == "так";

            Console.Write("Введіть тривалість ліцензії в роках: ");
            var duration = int.Parse(Console.ReadLine());

            var licenseCost = game.PricePerYear * duration;

            if (isStudent && game.PricePerYear > 500)
            {
                licenseCost *= 0.9m;
            }

            Console.WriteLine($"Вартість ліцензії для гри {game.Name}: {licenseCost}");
        }
        else
        {
            Console.WriteLine("Гру з таким id не знайдено.");
        }
    }

    static void CalculateGraphicsPackageLicenseCost(ApplicationDbContext db)
    {
        Console.Write("Введіть id графічного пакету для обчислення вартості ліцензії: ");
        var id = int.Parse(Console.ReadLine());

        var graphicsPackage = db.GraphicsPackages.FirstOrDefault(e => e.Id == id);

        if (graphicsPackage != null)
        {
            Console.Write("Чи ви студент? (так/ні): ");
            var isStudent = Console.ReadLine().ToLower() == "так";

            Console.Write("Введіть тривалість ліцензії в роках: ");
            var duration = int.Parse(Console.ReadLine());

            var licenseCost = graphicsPackage.PricePerYear * duration;

            if (isStudent)
            {
                if (duration >= 2 && duration < 3)
                {
                    licenseCost *= 0.95m;
                }
                else if (duration >= 3)
                {
                    licenseCost *= 0.9m;
                }
            }

            Console.WriteLine($"Вартість ліцензії для графічного пакету {graphicsPackage.Name}: {licenseCost}");
        }
        else
        {
            Console.WriteLine("Графічні пакети з таким id не знайдено.");
        }
    }

    static void ViewAllGames(ApplicationDbContext db)
    {
        Console.WriteLine("Всі ігри:");
        foreach (var game in db.Games.OrderBy(e => e.Name))
        {
            Console.WriteLine($"Id: {game.Id}, Назва: {game.Name}, Розробник: {game.Developer}, Платформа: {game.Platform}, Тип ліцензії: {game.LicenseType}, Ціна на рік: {game.PricePerYear}");
        }
    }

    static void ViewAllGraphicsPackages(ApplicationDbContext db)
    {
        Console.WriteLine("Усі графічні пакети:");
        foreach (var graphicsPackage in db.GraphicsPackages.OrderBy(e => e.Name))
        {
            Console.WriteLine($"Id: {graphicsPackage.Id}, Назва: {graphicsPackage.Name}, Виробник: {graphicsPackage.Manufacturer}, Тип графіки: {graphicsPackage.GraphicsType}, Ціна на рік: {graphicsPackage.PricePerYear}");
        }
    }
}