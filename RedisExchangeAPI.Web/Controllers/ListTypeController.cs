using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers;
    public class ListTypeController(RedisService redisService) : BaseController(redisService,1)
    {
        private readonly string listKey = "names";
    
        public IActionResult Index()
        {
            List<string> namesList = [];
            if (Db.KeyExists(listKey))
                Db.ListRange(listKey).ToList().ForEach(x => namesList.Add(x.ToString()));

            return View(namesList);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            Db.ListLeftPush(listKey, name);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            Db.ListRemoveAsync(listKey, name).Wait();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteFirstItem()
        {
            Db.ListLeftPop(listKey);
            return RedirectToAction("Index");
        }
    }