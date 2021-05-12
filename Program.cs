using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace CSinDepth
{
    class Program
    {
        public readonly static string exePath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @".\";

        static void Main(string[] args)
        {
            //Simple01();
            SimpleSavetoXLS();




        }

        static void SimpleSavetoXLS() {

            var app = new Application { Visible = false };
            try
            {
                Workbook workbook = app.Workbooks.Add();
                Worksheet worksheet = app.ActiveSheet;
                int row = 1;
                List<Product> Lp = Product.GetSampleProducts();
                List<Supplier> Lsp = Supplier.GetSampleSupplier();
                Lp.Add(new Product("Noname"));

                //Left outer JOIN 
                var result = from p in Lp.Where(x => x.Price <= 100 || x?.Price == null)
                             join s in Lsp on p.SupplierID equals s.SupplierID
                             into PS
                             from res in PS.DefaultIfEmpty()
                             select new
                             {
                                 Sname = res?.Name ?? "",
                                 Pname = p.Name,
                                 Price = p?.Price ?? 999999.99m,
                             };

                foreach (var res in result)
                {
                    worksheet.Cells[row, 1].Value = res.Sname;
                    worksheet.Cells[row, 2].Value = res.Pname;
                    worksheet.Cells[row, 3].Value = res.Price;
                    row++;
                }
                workbook.SaveAs(Filename: exePath + "demo.xls",
                                FileFormat: XlFileFormat.xlWorkbookNormal);
            }
            catch (Exception ex) {
                Console.WriteLine (ex.StackTrace );
            }
            finally
            {
                app.Application.Quit();
            }


        }

        static void Simple02() {

            XDocument doc = XDocument.Load("..\\..\\MyXMLFile01.xml");
            var filtered = from p in doc.Descendants("Product")
                           join s in doc.Descendants("Supplier")
                              on (int)p.Attribute("SupplierID")
                              equals (int)s.Attribute("SupplierID")
                           where (decimal)p.Attribute("Price") > 10
                           orderby (string)s.Attribute("Name"),
                                   (string)p.Attribute("Name")
                           select new
                           {
                               SupplierName = (string)s.Attribute("Name"),
                               ProductName = (string)p.Attribute("Name"),
                               Price = (string)p.Attribute("Price")
                           };
            foreach (var v in filtered)
            {
                Console.WriteLine("{0} / {1}  / {2}",
                                  v.SupplierName, v.ProductName, v.Price);
            }
            Console.ReadLine();


        }

        static void Simple01() {

            List<Product> Lp = Product.GetSampleProducts();
            List<Supplier> Lsp = Supplier.GetSampleSupplier();
            Lp.Add(new Product("Noname"));

            //Left outer JOIN 
            var result = from p in Lp.Where(x => x.Price <= 100 || x?.Price == null)
                         join s in Lsp on p.SupplierID equals s.SupplierID
                         into PS
                         from res in PS.DefaultIfEmpty()
                         select new
                         {
                             Sname = res?.Name ?? null,
                             Pname = p.Name,
                             Price = p?.Price ?? null,
                         };


            foreach (var v in result.OrderBy(x => x?.Price).ThenBy(x => x?.Sname).ThenBy(x => x.Pname))
                Console.WriteLine("{0} - {1:N2} - {2}", v.Pname, v.Price, v.Sname);

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
                new Product{Name="Smartph  ",Price=999.99m,SupplierID=1001},
                new Product{Name="Assaian 2",Price=10.45m,SupplierID=1001 },
                new Product{Name="Assaian  ",Price=1.45m,SupplierID=1001 },
                new Product{Name="Zooming  ",Price=0.4m },
                new Product{Name="Food     ",Price=9.99m },
                new Product{Name="Qest     ",Price=9.99m,SupplierID=1001},
                new Product{Name="Soop     ",Price=1.75m ,SupplierID=2001},
                new Product{Name="Foot     ",Price=1000.01m, SupplierID=2001 },
                new Product{Name="Botting  ",Price=56.41m,SupplierID=1001 },
                new Product{Name="Botting 2" }

            };
        
        }


    }

    class Supplier
    {
        readonly string name;
        public string Name => name;
        
        readonly int? supplierID;
        public int? SupplierID => supplierID;

        Supplier(string name =null, int? supplierID =null)
        {
            this.name = name;
            this.supplierID = supplierID;
        }


        public static List<Supplier> GetSampleSupplier()
        {
            return new List<Supplier>
            {
                new Supplier(name:"Apple    ",supplierID:1001),
                new Supplier(name:"Microsoft",supplierID:2001)

            };

        }


    }
}
