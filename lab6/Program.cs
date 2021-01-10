using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace std
{
	class Program
	{
		static int MAX = 20;
		static int Main(string[] args)
		{
			Shop ourshop = new Shop();
			ourshop = Shop.input(ourshop);
			ourshop.output(ourshop);
			Console.WriteLine("Sum price = " + Shop.add(ourshop));
			Console.WriteLine();
			Console.WriteLine("Program is over.");
			Thread.Sleep(10000);

			return 0;
		}

// Create class
	class Item
	{
		private	string country;
		private string name;
		private double price;

			// Init
			public Item()
			{
				country = "";
				name = "";
				price = 0;
			}
			// Correct(?) init
			public Item(string newCountry, string newName, double newPrice)
			{
				country = newCountry;
				name = newName;
				price = newPrice;
			}

			public string getCountry()
			{
				return (country);
			}
			public string getName()
			{
				return (name);
			}
			public double getPrice()
			{
				return (price);
			}
			public void setCountry(string newCountry)
			{
				country = newCountry;
			}
			public void setName(string newName)
			{
				name = newName;
			}
			public void setPrice(double newPrice)
			{
				price = newPrice;
			}

			// Read 
			public static Item input(int i)
			{
				string newCountry;
				string newName;
				double newPrice;
				do
				{
					Console.WriteLine("Input country of " + (i + 1) + " product: ");
					newCountry = Console.ReadLine();
				} while (newCountry[0] == '\0' || newCountry[0] == ' ');

				do
				{
					Console.WriteLine("Input name of " + (i + 1) + " product: ");
					newName = Console.ReadLine();
				} while (newName[0] == '\0' || newName[0] == ' ');


				Console.WriteLine("Input price of " + (i + 1) + " product: ");
				do
				{
					while (!double.TryParse(Console.ReadLine(), out newPrice))
						Console.WriteLine("Error! Input a positive number!");
				} while (newPrice <= 0);
				Console.WriteLine();
				Item newItem = new Item(newCountry, newName, newPrice);
				return (newItem);
			}

			// Display
			public void output(Item prod, int i)
			{
				Console.WriteLine((i + 1) + " product:");
				Console.WriteLine("Country: " + prod.country);
				Console.WriteLine("Product: " + prod.name);
				Console.WriteLine("Price = " + prod.price);
				Console.WriteLine();
			}

			// Add 
			public static int add(Item item, Item secondItem)
			{
				int sum;
				sum = Convert.ToInt32(item.price) + Convert.ToInt32(secondItem.price);
				return sum;
			}

			// Add sale
			public static void sale(Item item, int num)
			{
				for (int i = 0; i < num; i++)
					item.setPrice(item.getPrice() * 0.5);
			}

			// Add markup
			public void markup(Item item, int num)
			{
				for (int i = 0; i < num; i++)
					item.setPrice(item.getPrice() * 2);
			}
	};

	class Shop
	{
			private Item[] prod = new Item[MAX];
			private int quantityProd;
			// Init
			public Shop()
			{
				quantityProd = 0;
			}

			// Correct(?) init
			public Shop(int newQuantityProd)
			{
				quantityProd = newQuantityProd;
			}

			public int getQuantityProd()
			{
				return (quantityProd);
			}

			public static Shop input(Shop ourshop)
			{
				int quantityProd;
				Item[] prod = new Item[MAX];
				do
				{
					Console.WriteLine("How many products do you have in your shop?");
					do
					{
						while (!int.TryParse(Console.ReadLine(), out quantityProd))
							Console.WriteLine("Error! Input a positive number!");
					} while (quantityProd <= 0);
				} while (quantityProd <= 0 || quantityProd > MAX);
				ourshop.quantityProd = quantityProd;

				for (int i = 0; i < quantityProd; i++)
				{
					ourshop.prod[i] = Item.input(i);
					Console.WriteLine("If you want to have the same products - enter their quantity without the one just entered.");
					Console.WriteLine("If you don't want to - enter anything and click enter.");
					int num;
					do
					{
						while (!int.TryParse(Console.ReadLine(), out num))
							Console.WriteLine("Error! Input a positive number!");
					} while (num <= 0);

					if (num > 0 && num < (quantityProd - i))
					{
						for (int j = i + 1; j <= (i + num); j++)
						{
							ourshop.prod[j] = ourshop.prod[i];
						}
						i = i + num;
					}
				}
				return (ourshop);
			}

			public void output(Shop ourshop)
			{
				for (int i = 0; i < quantityProd; i++)
				{
					ourshop.prod[i].output(ourshop.prod[i], i);
				}
			}

			public static int add(Shop ourshop)
			{
				return (Item.add(ourshop.prod[0], ourshop.prod[0]));
			}

			public static int add(Item item, Item secondItem)
			{
				int sum;
				sum = Convert.ToInt32(item.getPrice()) + Convert.ToInt32(secondItem.getPrice());
				return sum;
			}

		~Shop()
		{
		}
	}; 
    }
}
