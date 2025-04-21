using System;
using System.IO;

namespace Exams.CSharp
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }

        public Product(string name, decimal price, int inventory)
        {
            Name = name;
            Price = price;
            Inventory = inventory;
        }
    }

    public class ShoppingCart
    {
        private readonly System.Collections.Generic.Dictionary<Product, int> _items;

        public ShoppingCart()
        {
            _items = new System.Collections.Generic.Dictionary<Product, int>();
        }

        public void AddProduct(Product product, int quantity)
        {
            if (product.Inventory < quantity)
            {
                throw new InvalidOperationException("Not enough inventory for product: " + product.Name);
            }

            if (_items.ContainsKey(product))
            {
                _items[product] += quantity;
            }
            else
            {
                _items[product] = quantity;
            }

            product.Inventory -= quantity;
        }

        public void RemoveProduct(Product product, int quantity)
        {
            if (!_items.ContainsKey(product))
            {
                throw new InvalidOperationException("Product not in cart: " + product.Name);
            }

            if (_items[product] < quantity)
            {
                throw new InvalidOperationException("Cannot remove more than added quantity for product: " + product.Name);
            }

            _items[product] -= quantity;
            product.Inventory += quantity;

            if (_items[product] == 0)
            {
                _items.Remove(product);
            }
        }

        public decimal Checkout()
        {
            decimal total = 0;
            foreach (var item in _items)
            {
                total += item.Key.Price * item.Value;
            }
            _items.Clear();
            return total;
        }

        public System.Collections.Generic.IReadOnlyDictionary<Product, int> GetItems()
        {
            return _items;
        }
    }

    public class SimpleFileSystem
    {
        private readonly System.Collections.Generic.Dictionary<string, string> _files;

        public SimpleFileSystem()
        {
            _files = new System.Collections.Generic.Dictionary<string, string>();
        }

        public void CreateFile(string filename, string content)
        {
            if (_files.ContainsKey(filename))
            {
                throw new InvalidOperationException("File already exists: " + filename);
            }
            _files[filename] = content;
        }

        public string ReadFile(string filename)
        {
            if (!_files.ContainsKey(filename))
            {
                throw new FileNotFoundException("File not found: " + filename);
            }
            return _files[filename];
        }

        public void WriteFile(string filename, string content)
        {
            if (!_files.ContainsKey(filename))
            {
                throw new FileNotFoundException("File not found: " + filename);
            }
            _files[filename] = content;
        }

        public void DeleteFile(string filename)
        {
            if (!_files.ContainsKey(filename))
            {
                throw new FileNotFoundException("File not found: " + filename);
            }
            _files.Remove(filename);
        }

        public System.Collections.Generic.IEnumerable<string> ListFiles()
        {
            return _files.Keys;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Test ProductShoppingCart
            Console.WriteLine("Testing ProductShoppingCart...");

            Product apple = new Product("Apple", 0.5m, 100);
            Product banana = new Product("Banana", 0.3m, 50);

            ShoppingCart cart = new ShoppingCart();

            try
            {
                cart.AddProduct(apple, 10);
                cart.AddProduct(banana, 5);
                Console.WriteLine("Added products to cart.");

                Console.WriteLine("Cart items:");
                foreach (var item in cart.GetItems())
                {
                    Console.WriteLine($"{item.Key.Name} - Quantity: {item.Value}");
                }

                decimal total = cart.Checkout();
                Console.WriteLine($"Total price: {total}");

                Console.WriteLine($"Apple inventory after checkout: {apple.Inventory}");
                Console.WriteLine($"Banana inventory after checkout: {banana.Inventory}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            // Test SimpleFileSystem
            Console.WriteLine("\nTesting SimpleFileSystem...");

            SimpleFileSystem fs = new SimpleFileSystem();

            try
            {
                fs.CreateFile("test.txt", "Hello, world!");
                Console.WriteLine("Created file 'test.txt'.");

                string content = fs.ReadFile("test.txt");
                Console.WriteLine("Read file content: " + content);

                fs.WriteFile("test.txt", "Updated content.");
                Console.WriteLine("Updated file 'test.txt'.");

                content = fs.ReadFile("test.txt");
                Console.WriteLine("Read updated content: " + content);

                fs.DeleteFile("test.txt");
                Console.WriteLine("Deleted file 'test.txt'.");

                Console.WriteLine("Files in system:");
                foreach (var file in fs.ListFiles())
                {
                    Console.WriteLine(file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
