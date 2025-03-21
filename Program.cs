using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Skapar en klass för Produkt
    public class Product
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(string category, string name, decimal price)
        {
            Category = category;
            Name = name;
            Price = price;
        }

        // Visar produkten i Kategori - Produktnamn - Pris
        public override string ToString()
        {
            return $"{Category} - {Name} - {Price:C}";
        }
    }

    // SKapar en klass för att hantera Produktlistan
    public class ProductList
    {
        private List<Product> products;

        public ProductList()
        {
            products = new List<Product>();
        }

        // Lägg till en produkt i listan
        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        // Sortera produkter på pris (lägsta till högsta) 
        public List<Product> GetSortedProducts()
        {
            return products.OrderBy(p => p.Price).ToList();
        }

        // Beräkna totalpriset för alla produkter
        public decimal GetTotalPrice()
        {
            return products.Sum(p => p.Price);
        }

        // Visa alla produkter  (Kategori, Produkt, Pris)
        public void DisplayProducts()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nLista över tillagda produkter (sorterade efter pris):");
            Console.ResetColor();

            if (products.Any()) // Kontrollera om listan är tom
            {
                // Skriv rubriker för kolumner
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("{0,-20} {1,-30} {2,10}", "Kategori", "Produkt", "Pris");
                Console.ResetColor();

                // Skriver ut varje produkt 
                foreach (var product in GetSortedProducts())
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("{0,-20} {1,-30} {2,10:C}", product.Category, product.Name, product.Price);
                    Console.ResetColor();
                }

                // Visa totalpris
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nTotalpris: {GetTotalPrice():C}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Inga produkter har lagts till.");
                Console.ResetColor();
            }
        }
    }

    static void Main(string[] args)
    {
        ProductList productList = new ProductList();  // Skapa en lista 
        bool running = true;

        while (running)
        {
            // Fråga om kategori
            Console.ForegroundColor = ConsoleColor.DarkYellow;  // mörkgul färg
            Console.Write("Ange produktkategori - följ stegen(eller 'q' för att avsluta): ");
            Console.ResetColor();  // Återställ färgen 
            string category = Console.ReadLine();

            //  "q", avsluta programmet
            if (category.ToLower() == "q")
            {
                running = false;
                break;
            }

            // Fråga om produktens namn
            string name = "";
            bool validName = false;
            while (!validName)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;  // Gul färg
                Console.Write("Ange produktnamn: ");
                Console.ResetColor();  // Återställ färgen 
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name)) // tomt eller mellanslag
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Produktens namn kan inte vara tomt. Försök igen.");
                    Console.ResetColor();
                }
                else
                {
                    validName = true;
                }
            }

            // Fråga om produktens pris
            decimal price = 0;
            bool validPrice = false;
            while (!validPrice)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;  // mörkröd färg
                Console.Write("Ange produktens pris: ");
                Console.ResetColor();  // Återställ färgen 
                string priceInput = Console.ReadLine();

                try
                {
                    price = decimal.Parse(priceInput);
                    if (price < 0)
                    {
                        throw new ArgumentException("Priset kan inte vara negativt.");
                    }
                    validPrice = true;
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" Vänligen ange ett giltigt pris igen.");
                    Console.ResetColor();
                }
                catch (ArgumentException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }

            // Skapa och lägg till produkten i listan
            Product product = new Product(category, name, price);
            productList.AddProduct(product);

            // Presentera listan och fråga om användare vill lägga till fler produkter
            productList.DisplayProducts();

            // Avsluta eller lägg till produkt
            Console.ForegroundColor = ConsoleColor.Green;  // grön färg
            Console.WriteLine("\nProdukten finns nu i listan!\nVill du lägga till en till produkt? (Skriv 'y' för ja, 'q' för att avsluta)");
            Console.ResetColor();  // Återställ färgen 
            string quitOption = Console.ReadLine();
            if (quitOption.ToLower() == "q")
            {
                running = false;
            }
        }

        // Visa den slutgiltiga listan 
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\nAvslutar programmet. Här är den slutgiltiga listan:");
        Console.ResetColor();
        productList.DisplayProducts();
    }
}
