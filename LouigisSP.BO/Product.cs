using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.BO
{
    public class Product: Fileable
    {
        string id;
        string name;
        string brand;
        string model;
        string color;
        float price;
        int stock;

        public Product(string id, string name, string brand, string model, string color, float price, int stock)
        {
            this.Id = id;
            this.Name = name;
            this.Brand = brand;
            this.Model = model;
            this.Color = color;
            this.Price = price;
            this.Stock = stock;
        }

        public Product()
        {
        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Brand { get => brand; set => brand = value; }
        public string Model { get => model; set => model = value; }
        public string Color { get => color; set => color = value; }
        public float Price { get => price; set => price = value; }
        public int Stock { get => stock; set => stock = value; }

        public virtual void PrintAllDetails()
        {
            Console.Clear();
            Console.WriteLine("Name: "+ name);
            Console.WriteLine("Brand: " + brand);
            Console.WriteLine("Model: " + model);
            Console.WriteLine("Color: " + color);
            Console.WriteLine("Price: " + price);
            Console.WriteLine("Stock: "+stock);
        }

        public override string ToString()
        {
            return "Id: "+id+"    Name: " + name + "    Brand:" + brand + "    Model:" + model + "    Color:" + color + "    Price:" + price + "    Stock:" + stock; 
        }
    }
}
