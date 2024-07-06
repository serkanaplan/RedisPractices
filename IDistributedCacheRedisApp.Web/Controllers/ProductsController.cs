using IDistributedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace IDistributedCacheRedisApp.Web.Controllers;

public class ProductsController(IDistributedCache distributedCache) : Controller
{
    private IDistributedCache _distributedCache = distributedCache;

    // public  IActionResult Index()
    public async Task<IActionResult> Index()
    {
        DistributedCacheEntryOptions cacheEntryOptions = new()
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(2),
        };
        // basit string cash islemi
        // _distributedCache.SetString("name", "serkan",cacheEntryOptions);
        // await _distributedCache.SetStringAsync("name", "serkan", cacheEntryOptions);

        // object to jsonString cash islemi
        Product product = new() { Id = 1, Name = "Samsung", Price = 100 };
        string jsonProduct = JsonSerializer.Serialize(product);
        await _distributedCache.SetStringAsync("product:1", jsonProduct, cacheEntryOptions);

        // object to Byte cash islemi
        Product product2 = new() { Id = 1, Name = "Apple", Price = 200 };
        byte[] bytesProduct = JsonSerializer.SerializeToUtf8Bytes(product2);
        await _distributedCache.SetAsync("product:2", bytesProduct, cacheEntryOptions);

        return View();
    }

    public IActionResult Show()
    {
        ViewBag.Product = JsonSerializer.Deserialize<Product>(_distributedCache.GetString("product:1"));
        ViewBag.Product2 = JsonSerializer.Deserialize<Product>(_distributedCache.GetString("product:2"));
        return View();
    }

    public IActionResult Delete()
    {
        _distributedCache.Remove("product:1");
        return View();
    }

    public IActionResult ImageUrl()
    {
        byte[] resimbyte = _distributedCache.Get("resim");

        return File(resimbyte, "image/jpg");
    }

    public IActionResult ImageCache()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/download.jpg");

        byte[] imageByte = System.IO.File.ReadAllBytes(path);

        _distributedCache.Set("resim", imageByte);

        return View();
    }


}
