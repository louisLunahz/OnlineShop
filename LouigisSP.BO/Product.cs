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
            this.id = id;
            this.name = name;
            this.brand = brand;
            this.model = model;
            this.color = color;
            this.price = price;
            this.stock = stock;
        }

        public Product()
        {
        }

        public Product(string id, string name, string brand, string model, string color, string price, string stock) {
            this.id = id;
            this.name = name;
            this.brand = brand;
            this.model = model;
            this.color = color;
            this.price = float.Parse(price);
            this.stock = int.Parse(stock);

        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Brand { get => brand; set => brand = value; }
        public string Model { get => model; set => model = value; }
        public string Color { get => color; set => color = value; }
        public float Price { get => price; set => price = value; }
        public int Stock { get => stock; set => stock = value; }

      

        public override string ToString()
        {
            return "Id: "+id+"    Name: " + name + "    Brand:" + brand + "    Model:" + model + "    Color:" + color + "    Price:" + price + "    Stock:" + stock; 
        }

        public void showDetails() {
            Console.Write("Id: "+id+" ");
            Console.Write("Name: "+name + " ");
            Console.Write("Brand: "+brand + " ");
            Console.Write("Model: "+model + " ");
            Console.Write("Color: "+color + " ");
            Console.Write("Price: "+price + " ");

        }

       
    }
}
