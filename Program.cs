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
            List<Supplier> Lsp = Supplier.GetSampleSupplier();  
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
            //foreach (Product p in Lp.OrderBy(x => x.Price).ThenBy(x => x.Name)
            //    .Where(x => x.Price < 100 || x.Price == null))
            //{
            //    //var price = p.Price.ToString();
            //    Console.WriteLine(p.ToString());
            //}
            var filtered = from p in Lp
                           join s in Lsp
              on p.SupplierID equals s.SupplierID
                           where p.Price < 100
                           orderby p.Price,p.Name,s.Name    
                           select new { SupName = s.Name, ProdName = p.Name, p.Price };
                           


            //Console.WriteLine();
            foreach (var v in filtered)
                Console.WriteLine("{0}   {1}     {2:N2} $",
                    v.SupName,v.ProdName,v.Price);  

            Console.ReadLine();
            
        }
    }
    class Product 
    { 
        public string Name { get; private set; }
        public decimal? Price { get; private set; }
        public int? SupplierID { get; private set; }
        public Product(string name, decimal? price=null, int? supplierID=null)
        {
            Name = name;
            Price = price;
            SupplierID = supplierID;
        }
        Product() { }
        public static List<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product{Name="Wooding  ",Price=999.99m,SupplierID=1001},
                new Product{Name="Assaian 2",Price=10.45m,SupplierID=1001 },
                new Product{Name="Assaian  ",Price=1.45m,SupplierID=1001 },
                new Product{Name="Zooming  ",Price=0.4m,SupplierID=null },
                new Product{Name="Food     ",Price=9.99m,SupplierID=null },
                new Product{Name="Qest     ",Price=9.99m,SupplierID=null},
                new Product{Name="Soop     ",Price=1.75m ,SupplierID=2001},
                new Product{Name="Foot     ",Price=1000.01m, SupplierID=2001 },
                new Product{Name="Botting  ",Price=56.41m,SupplierID=1001 },
                new Product{Name="Botting 2",Price=null,SupplierID=null }

            };
        
        }
        public override string ToString()
        {
            
            return string.Format("{2:D4}    {0}     {1:N2}",
                Name,Price , SupplierID==null ? 0000:SupplierID);
        }


    }

    class Supplier
    {
        public string Name { get; private set; }
        public int SupplierID { get; private set; }
        public Supplier(string name, int supplierID)
        {
            Name = name;
            SupplierID = supplierID;
        }
        Supplier() { }
        public static List<Supplier> GetSampleSupplier()
        {
            return new List<Supplier>
            {
                new Supplier{Name="Apple    ",SupplierID=1001},
                new Supplier{Name="Microsoft",SupplierID=2001 }

            };

        }
        public override string ToString()
        {

            return string.Format("{0}     {1:N2}",Name, SupplierID );
        }


    }
}
