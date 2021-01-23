using System;
using System.Threading;

namespace std
{
	class Program
	{
		static int MAX = 20;
		static int Main(string[] args)
		{
			Shop ourshop = new Shop();
			ourshop = Shop.input();
			ourshop.output(ourshop);
			int sum = ourshop + ourshop;
			Console.WriteLine("Sum price = " + sum);
			Console.WriteLine();
			Console.ReadKey();
			Console.Clear();
			//Console.WriteLine("Price of 1st item = " + ourshop.returnPrice(0));
			//Console.WriteLine("Price of 1st item = " + (ourshop++).returnPrice(0));
			//Console.WriteLine("Price of 1st item = " + (ourshop).returnPrice(0));
			//Console.WriteLine("Price of 1st item = " + (++ourshop).returnPrice(0));

			int amount = 2;
			Shop[] arrShop = new Shop[amount];

			for (int i = 0; i < 2; i++)
				arrShop[i] = new Shop(amount + i);

			for (int i = 0; i < 2; i++)
				arrShop[i].output(arrShop[i]);


			//string value, value1;
			//value1 = "";
			//ourshop.func1(out value, ref value1);
			Console.WriteLine("Program is over. Press any key to exit.");
			Console.ReadKey();

			return 0;
		}

// Create struct
	class Item
	{
		private string country;
		private string name;
		private double price;

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

			public string getCountry => country;

			public string getName => name;
			
			public double getPrice => price;

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
				} while (String.IsNullOrEmpty(newCountry) || newCountry[0] == ' ');

				do
				{
					Console.WriteLine("Input name of " + (i + 1) + " product: ");
					newName = Console.ReadLine();
				} while (String.IsNullOrEmpty(newName) || newName[0] == ' ');


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
					item.setPrice(item.getPrice * 0.5);
			}

			// Add markup
			public static void markup(Item item, int num)
			{
				for (int i = 0; i < num; i++)
					item.setPrice(item.getPrice * 2);
			}
	};

	class Shop
	{
			private Item[] prod = new Item[MAX];
			private int quantityProd;
			private static string nameShop = "Welcome to our shop network!";

			// Init
			public Shop()
            {
				this.quantityProd = 0;
			}


			// Correct(?) init
			public Shop(int newQuantityProd)
			{
				this.quantityProd = newQuantityProd;
				for (int i = 0; i < quantityProd; i++)
					prod[i] = new Item();
			}

			public int getQuantityProd => quantityProd;

			public static Shop input()
			{
				int quantityProd = 0;
				Item[] prod = new Item[MAX];
				//do
				//{
				//	Console.WriteLine("How many products do you have in your shop?");
				//	do
				//	{
				//		while (!int.TryParse(Console.ReadLine(), out quantityProd))
				//			Console.WriteLine("Error! Input a positive number!");
				//	} while (quantityProd <= 0);
				//} while (quantityProd > MAX);

				try
				{
					Console.WriteLine("How many products do you have in your shop?");
					//Console.Read(quantityProd);
					quantityProd = int.Parse(Console.ReadLine());
					if (quantityProd <= 0 || quantityProd > MAX)
					{
						throw new Exception();
					}
				}
				catch (FormatException)
				{
					Console.WriteLine("Sorry, some problems with input. You was enter not number. Press any key to exit.");
					Console.ReadKey();
					System.Environment.Exit(1);
					
				}
				catch (Exception)
                {
					Console.WriteLine("Sorry, some problems with input. You was enter negative/zero or too big number. Press any key to exit.");
					Console.ReadKey();
					System.Environment.Exit(1);
				}

				Shop ourshop = new Shop(quantityProd);
				

				for (int i = 0; i < quantityProd; i++)
				{
					Item smth = Item.input(i);
					ourshop.prod[i] = smth;
					Console.WriteLine("If you want to have the same products - enter their quantity without the one just entered.");
					Console.WriteLine("If you don't want to - enter anything and click enter.");
					int num;
					if (!int.TryParse(Console.ReadLine(), out num))
						num = 0;

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
				Console.WriteLine(nameShop);
				for (int i = 0; i < quantityProd; i++)
				{
					ourshop.prod[i].output(ourshop.prod[i], i);
				}
			}

			public static int operator +(Shop ourshop, Shop shop)
			{
				return (Item.add(ourshop.prod[0], shop.prod[0]));
			}

			public static Shop operator ++(Shop ourshop)
			{
                Shop shop = new Shop(ourshop.getQuantityProd);
                ourshop.prod[0].setPrice(ourshop.prod[0].getPrice + 1);
                shop = ourshop;
                return shop;
            }

			public double returnPrice(int i)
			{
				return this.prod[i].getPrice;
			}

			public static int add(Item item, Item secondItem)
			{
				int sum;
				sum = Convert.ToInt32(item.getPrice) + Convert.ToInt32(secondItem.getPrice);
				return sum;
			}

			public void func1(out string value, ref string value1)
			{
				value = "Hello World!";
			}
		}; 
    }
}
