using System;
using System.Collections.Generic;

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
        private readonly Dictionary<Product, int> _items;

        public ShoppingCart()
        {
            _items = new Dictionary<Product, int>();
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

        public IReadOnlyDictionary<Product, int> GetItems()
        {
            return _items;
        }
    }
}
