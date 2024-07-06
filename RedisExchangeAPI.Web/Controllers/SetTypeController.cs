using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers;
public class SetTypeController(RedisService redisService):BaseController(redisService,2)
{
    private readonly string listKey = "hashnames";

    public IActionResult Index()
    {
        HashSet<string> namesList = [];

        if (Db.KeyExists(listKey))
            Db.SetMembers(listKey).ToList().ForEach(x => namesList.Add(x.ToString()));

        return View(namesList);
    }

    [HttpPost]
    public IActionResult Add(string name)
    {
        Db.KeyExpire(listKey, DateTime.Now.AddMinutes(5));

        Db.SetAdd(listKey, name);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteItem(string name)
    {
        await Db.SetRemoveAsync(listKey, name);

        return RedirectToAction("Index");
    }
}