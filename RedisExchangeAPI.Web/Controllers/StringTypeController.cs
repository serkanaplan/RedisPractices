using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers;
    public class StringTypeController(RedisService redisService):BaseController(redisService,0)
    {
        public IActionResult Index()
        {
            Db.StringSet("name", "Serkan Kaplan");
            Db.StringSet("ziyaretci", 100);

            return View();
        }

        public IActionResult Show()
        {
            // var value = Db.StringGet("name");
            // var value = Db.StringGetRange("name", 0, 2);
            var value = Db.StringLength("name");

            // Db.StringIncrement("ziyaretci", 10);

            // var count = Db.StringDecrementAsync("ziyaretci", 1).Result;

            Db.StringDecrementAsync("ziyaretci", 10).Wait();
            
            // if(value.HasValue)
                ViewBag.value = value.ToString();

            return View();
        }
    }