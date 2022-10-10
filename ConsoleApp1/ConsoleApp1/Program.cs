
using System.ComponentModel.DataAnnotations;
using MessagePack;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

var product = new Product();
product.Name = "Apple";
product.ExpiryDate = new DateTime(2023,01,01);
product.Sizes = new List<string> { "small", "large" };
product.Price = 3.99M;

var product1 = new Product();
product1.Name = "Apple";
product1.ExpiryDate = new DateTime(2023, 01, 01);
product1.Sizes = new List<string> { "small", "large" };
product1.Price = 3.99M;

var product2 = new Product();
product2.Name = "Apple";
product2.ExpiryDate = new DateTime(2023, 01, 01);
product2.Sizes = new List<string> { "small", "large" };
product2.Price = 3.99M;

var list = new List<Product>() { product, product1, product2 };

string output = JsonConvert.SerializeObject(new List<Product>() { product,product1,product2}, Formatting.Indented);

Console.WriteLine(output);

byte[] bytes = MessagePackSerializer.Serialize(list);

var result = MessagePackSerializer.Deserialize<List<Product>>(bytes);

foreach (var item in result)
{
    Console.WriteLine(item);
}

var json = MessagePackSerializer.SerializeToJson(result);

Console.WriteLine(json);

[MessagePackObject]
public record Product
{
    [MessagePack.Key(0)]
    public string Name { get; set; }
    [MessagePack.Key(1)]
    public DateTime ExpiryDate { get; set; }
    [MessagePack.Key(2)]
    public decimal Price { get; set; }
    [MessagePack.Key(3)]
    public List<string> Sizes { get; set; }
}