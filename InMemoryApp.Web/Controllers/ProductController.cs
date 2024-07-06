using InMemoryApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers;

public class ProductController(IMemoryCache memoryCache) : Controller
{
    private IMemoryCache _memoryCache = memoryCache;

    public IActionResult Index()
    {

        MemoryCacheEntryOptions options = new()
        {
            AbsoluteExpiration = DateTime.Now.AddSeconds(10),
            //  options.SlidingExpiration = TimeSpan.FromSeconds(10);
            Priority = CacheItemPriority.High
        };

        options.RegisterPostEvictionCallback((key, value, reason, state) =>_memoryCache.Set("callback", $"{key}->{value} => sebep:{reason}"));

        _memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);

        Product p = new() { Id = 1, Name = "Kalem", Price = 200 };

        _memoryCache.Set<Product>("product:1", p);
        _memoryCache.Set<double>("money", 100.99);

        return View();
    }

    public IActionResult Show()
    {
        _memoryCache.TryGetValue("zaman", out string zamancache);
        _memoryCache.TryGetValue("callback", out string callback);
        ViewBag.zaman = zamancache;
        ViewBag.callback = callback;

        ViewBag.product = _memoryCache.Get<Product>("product:1");

        return View();
    }
}