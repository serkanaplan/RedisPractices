using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;

namespace RedisExchangeAPI.Web.Controllers;
public class HashTypeController(RedisService redisService) : BaseController(redisService,4)
{
    public string HashKey { get; set; } = "sozluk";

    public IActionResult Index()
    {
        Dictionary<string, string> list = [];

        if (Db.KeyExists(HashKey))
            Db.HashGetAll(HashKey).ToList().ForEach(x =>list.Add(x.Name, x.Value));

        return View(list);
    }

    [HttpPost]
    public IActionResult Add(string name, string val)
    {
        Db.HashSet(HashKey, name, val);

        return RedirectToAction("Index");
    }

    public IActionResult DeleteItem(string name)
    {
        Db.HashDelete(HashKey, name);
        return RedirectToAction("Index");
    }
}