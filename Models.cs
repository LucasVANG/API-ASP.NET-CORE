using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public class Sale
    {
        public int id_Product { get; set; }
        public int Quantity { get; set; }
        public int Sale_id { get; set; }
        public DateTime Sale_date { get; set; }
        public int id_shop { get; set; }



    }

    public class Product
    {
        public string Name_Product { get; set; }
        public int Product_id { get; set; }
        public float Price { get; set; }
        public string Desc_Product { get; set; }

    }

    public class Shop
    {
        public string Name_Shop { get; set; }
        public int Shop_id { get; set; }
        public int id_Region { get; set; }


    }


    public class Region
    {
        public string Name_Region { get; set; }
        public int Region_id { get; set; }
        public string Country { get; set; }


    }
}


