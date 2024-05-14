using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsCourseWork
{
    public partial class Form1 : Form
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(Enum.GetNames(typeof(GraphicsType)));
            comboBox2.Items.AddRange(Enum.GetNames(typeof(LicenseType)));
            try
            {
                _dbContext.Database.EnsureCreated();
                ViewAllGraphicsPackages();
                ViewAllGames();

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating database: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                var name = textBox1.Text;
                var manufacturer = textBox2.Text;
                var graphicsTypeInput = comboBox1.SelectedItem.ToString();
                var graphicsType = graphicsTypeInput == "Векторна" ? GraphicsType.Векторна : (graphicsTypeInput == "Растрова" ? GraphicsType.Растрова : GraphicsType.Обидва);
                var pricePerYear = decimal.Parse(textBox3.Text);

                db.GraphicsPackages.Add(new GraphicsPackage { Name = name, Manufacturer = manufacturer, GraphicsType = graphicsType, PricePerYear = pricePerYear });
                db.SaveChanges();

                ViewAllGraphicsPackages();
            }
        }

        private void ViewAllGraphicsPackages()
        {
            using (var db = new ApplicationDbContext())
            {
                listBox1.Items.Clear();

                foreach (var graphicsPackage in db.GraphicsPackages.OrderBy(e => e.Name))
                {
                    listBox1.Items.Add($"Id: {graphicsPackage.Id}, Назва: {graphicsPackage.Name}, Виробник: {graphicsPackage.Manufacturer}, Тип графіки: {graphicsPackage.GraphicsType}, Ціна на рік: {graphicsPackage.PricePerYear}");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                var selectedItem = listBox1.SelectedItem.ToString();
                var id = int.Parse(selectedItem.Split(',')[0].Split(':')[1].Trim());
                var graphicsPackage = _dbContext.GraphicsPackages.Find(id);

                if (graphicsPackage != null)
                {
                    _dbContext.GraphicsPackages.Remove(graphicsPackage);
                    _dbContext.SaveChanges();
                    listBox1.Items.Remove(selectedItem);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                var selectedItem = listBox1.SelectedItem.ToString();
                var id = int.Parse(selectedItem.Split(',')[0].Split(':')[1].Trim());
                var graphicsPackage = _dbContext.GraphicsPackages.Find(id);

                if (graphicsPackage != null)
                {
                    graphicsPackage.Name = textBox1.Text;
                    graphicsPackage.Manufacturer = textBox2.Text;
                    graphicsPackage.GraphicsType = (GraphicsType)Enum.Parse(typeof(GraphicsType), comboBox1.SelectedItem.ToString());
                    graphicsPackage.PricePerYear = decimal.Parse(textBox3.Text);

                    _dbContext.GraphicsPackages.Update(graphicsPackage);
                    _dbContext.SaveChanges();

                    ViewAllGraphicsPackages();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                var name = textBox4.Text;
                var developer = textBox5.Text;
                var platform = textBox6.Text;
                var licenseTypeInput = comboBox2.SelectedItem.ToString();
                var licenseType = licenseTypeInput == "Дворічна" ? LicenseType.Дворічна : LicenseType.Трирічна;
                var pricePerYear = decimal.Parse(textBox7.Text);

                db.Games.Add(new Game { Name = name, Developer = developer, Platform = platform, LicenseType = licenseType, PricePerYear = pricePerYear });
                db.SaveChanges();

                ViewAllGames();
            }
        }

        private void ViewAllGames()
        {
            using (var db = new ApplicationDbContext())
            {
                listBox2.Items.Clear();

                foreach (var game in db.Games.OrderBy(e => e.Name))
                {
                    listBox2.Items.Add($"Id: {game.Id}, Назва: {game.Name}, Розробник: {game.Developer}, Платформа: {game.Platform}, Тип ліцензії: {game.LicenseType}, Ціна на рік: {game.PricePerYear}");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                var selectedItem = listBox2.SelectedItem.ToString();
                var id = int.Parse(selectedItem.Split(',')[0].Split(':')[1].Trim());
                var game = _dbContext.Games.Find(id);

                if (game != null)
                {
                    game.Name = textBox4.Text;
                    game.Developer = textBox5.Text;
                    game.Platform = textBox6.Text;
                    game.LicenseType = (LicenseType)Enum.Parse(typeof(LicenseType), comboBox2.SelectedItem.ToString());
                    game.PricePerYear = decimal.Parse(textBox7.Text);

                    _dbContext.Games.Update(game);
                    _dbContext.SaveChanges();

                    ViewAllGames();
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                var selectedItem = listBox2.SelectedItem.ToString();
                var id = int.Parse(selectedItem.Split(',')[0].Split(':')[1].Trim());
                var game = _dbContext.Games.Find(id);

                if (game != null)
                {
                    _dbContext.Games.Remove(game);
                    _dbContext.SaveChanges();
                    listBox2.Items.Remove(selectedItem);
                }
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                listBox1.Items.Clear();

                if (radioButton1.Checked)
                {
                    foreach (var graphicsPackage in db.GraphicsPackages.OrderBy(e => e.PricePerYear))
                    {
                        listBox1.Items.Add($"Id: {graphicsPackage.Id}, Назва: {graphicsPackage.Name}, Виробник: {graphicsPackage.Manufacturer}, Тип графіки: {graphicsPackage.GraphicsType}, Ціна на рік: {graphicsPackage.PricePerYear}");
                    }
                }
                else if (radioButton2.Checked)
                {
                    foreach (var graphicsPackage in db.GraphicsPackages.OrderByDescending(e => e.PricePerYear))
                    {
                        listBox1.Items.Add($"Id: {graphicsPackage.Id}, Назва: {graphicsPackage.Name}, Виробник: {graphicsPackage.Manufacturer}, Тип графіки: {graphicsPackage.GraphicsType}, Ціна на рік: {graphicsPackage.PricePerYear}");
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                listBox2.Items.Clear();

                if (radioButton4.Checked)
                {
                    foreach (var game in db.Games.OrderBy(e => e.PricePerYear))
                    {
                        listBox2.Items.Add($"Id: {game.Id}, Назва: {game.Name}, Розробник: {game.Developer}, Платформа: {game.Platform}, Тип ліцензії: {game.LicenseType}, Ціна на рік: {game.PricePerYear}");
                    }
                }
                else if (radioButton3.Checked)
                {
                    foreach (var game in db.Games.OrderByDescending(e => e.PricePerYear))
                    {
                        listBox2.Items.Add($"Id: {game.Id}, Назва: {game.Name}, Розробник: {game.Developer}, Платформа: {game.Platform}, Тип ліцензії: {game.LicenseType}, Ціна на рік: {game.PricePerYear}");
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var selectedItem = listBox1.SelectedItem.ToString();
            var id = int.Parse(selectedItem.Split(',')[0].Split(':')[1].Trim());
            var graphicsPackage = _dbContext.GraphicsPackages.Find(id);

            var duration = (int)numericUpDown1.Value;
            var isStudent = checkBox1.Checked;

            decimal licenseCost = graphicsPackage.PricePerYear * duration;

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

            textBox8.Text = $"Вартість ліцензії: {licenseCost}";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var selectedItem = listBox2.SelectedItem.ToString();
            var id = int.Parse(selectedItem.Split(',')[0].Split(':')[1].Trim());
            var game = _dbContext.Games.Find(id);

            var duration = (int)numericUpDown2.Value;

            var licenseCost = game.PricePerYear * duration;

            if (game.PricePerYear > 500 && checkBox2.Checked)
            {
                licenseCost *= 0.9m;
            }

            textBox9.Text = $"Вартість ліцензіїи: {licenseCost}";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string manufacturer = textBox10.Text.Trim();
            listBox1.DataSource = null;
            listBox1.Items.Clear();

            var graphicsPackages = _dbContext.GraphicsPackages
                .Where(gp => EF.Functions.Like(gp.Manufacturer.Trim(), $"%{manufacturer}%"))
                .Select(gp => new {
                    Id = gp.Id,
                    Name = gp.Name,
                    Manufacturer = gp.Manufacturer,
                    GraphicsType = gp.GraphicsType,
                    PricePerYear = gp.PricePerYear
                })
                .ToList()
                .Select(gp => $"Id: {gp.Id}, Назва: {gp.Name}, Виробник: {gp.Manufacturer}, Тип графіки: {gp.GraphicsType}, Ціна на рік: {gp.PricePerYear}")
                .ToList();

            listBox1.Items.AddRange(graphicsPackages.ToArray());
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string developer = textBox11.Text.Trim();
            listBox2.DataSource = null;
            listBox2.Items.Clear();

            var games = _dbContext.Games
                .Where(g => EF.Functions.Like(g.Developer.Trim(), $"%{developer}%"))
                .Select(g => new {
                    Id = g.Id,
                    Name = g.Name,
                    Developer = g.Developer,
                    Platform = g.Platform,
                    LicenseType = g.LicenseType,
                    PricePerYear = g.PricePerYear
                })
                .ToList()
                .Select(g => $"Id: {g.Id}, Назва: {g.Name}, Розробник: {g.Developer}, Платформа: {g.Platform}, Тип ліцензії: {g.LicenseType}, Ціна на рік: {g.PricePerYear}")
                .ToList();

            listBox2.Items.AddRange(games.ToArray());
        }
    }
}