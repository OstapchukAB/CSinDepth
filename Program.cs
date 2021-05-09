using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSinDepth
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Product> Lp = Product.GetSampleProducts();
            //Lp.Sort(delegate (Product x, Product y)
            //{
            //    //return y.Price.CompareTo(x.Price);
            //    return x.Name.CompareTo(y.Name);
            //});

            // C#2.0
            //Predicate<Product> test = delegate (Product p) { return p.Price > 10m; };
            //List<Product> matches = Lp.FindAll(test); 
            //Action<Product> print = Console.WriteLine;
            //matches.ForEach(print); 


            Lp.Add(new Product("Noname"));

            //C#3.0
            foreach (Product p in Lp.OrderBy(x => x.Price).ThenBy(x => x.Name)
                .Where(x => x.Price < 100 || x.Price == null))
            {
                //var price = p.Price.ToString();
                Console.WriteLine(p.ToString());
            }

            //Console.WriteLine();

            Console.ReadLine();
            
        }
    }
    class Product 
    { 
        public string Name { get; private set; }
        public decimal? Price { get; private set; }
        public Product(string name, decimal? price=null)
        {
            Name = name;
            Price = price;
        }
        Product() { }
        public static List<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product{Name="West side",Price=999.99m},
                new Product{Name="Assaian 2",Price=10.45m },
                new Product{Name="Assaian  ",Price=1.45m },
                new Product{Name="Zooming  ",Price=0.4m },
                new Product{Name="Food     ",Price=9.99m },
                new Product{Name="Qest     ",Price=9.99m},
                new Product{Name="Sian     ",Price=1.75m },
                new Product{Name="Foom     ",Price=1000.01m },
                new Product{Name="Botting  ",Price=56.41m },
                new Product{Name="Botting 2",Price=null }

            };
        
        }
        public override string ToString()
        {
            
            return string.Format("{0}     {1:N2}",
                Name,
                Price /*==null?"NULL":Price.ToString()*/);
        }


    }
}
