using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers;
    public class SortedSetTypeController(RedisService redisService):BaseController(redisService,3)
    {
        private readonly string listKey = "sortedsetnames";

        public IActionResult Index()
        {
            HashSet<string> list = [];

            if (Db.KeyExists(listKey))
            {
                Db.SortedSetScan(listKey).ToList().ForEach(x =>  list.Add(x.ToString()) );
                //veya
                Db.SortedSetRangeByRank(listKey, 0, 5, order: Order.Descending).ToList().ForEach(x => list.Add(x.ToString()));
            }

            return View(list);
        }

        [HttpPost]
        public IActionResult Add(string name, int score)
        {
            Db.SortedSetAdd(listKey, name, score);
            Db.KeyExpire(listKey, DateTime.Now.AddMinutes(1));
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            Db.SortedSetRemove(listKey, name);

            return RedirectToAction("Index");
        }
    }