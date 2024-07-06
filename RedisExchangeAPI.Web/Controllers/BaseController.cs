using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers;
public class BaseController : Controller
{
    private readonly RedisService _redisService;

    protected readonly IDatabase Db;

    public BaseController(RedisService redisService,int db)
    {
        _redisService = redisService;
        Db = _redisService.GetDb(db);
    }
}