using AdminSite.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AdminSite.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        List<Users> userList = new List<Users>();
        private IHubContext<ConnectionHub> _hubContext;

        public WeatherForecastController(IHubContext<ConnectionHub> hubContext,ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        [HttpGet]
        public IActionResult Get()
        {

            userList.Add(new Users()
            {
                Id =1,
                Name="Demo1",
                UserName="Demo User",
                Email="Demo1@gmail.com"

            });
            userList.Add(new Users()
            {
                Id = 2,
                Name = "Demo2",
                UserName = "Demo User2",
                Email = "Demo2@gmail.com"
            });

            userList.Add(new Users()
            {
                Id = 3,
                Name = "Demo3",
                UserName = "Demo User3",
                Email = "Demo3@gmail.com"
            });


            return Ok(userList);
        }
        [HttpPost]
        public IActionResult PostData(List<Users> users)
        {
            var couunt = users.Count + 1;
          
            Users user = new Users();
            user.Id = couunt ;
            user.Name = "Demo" + couunt ;
            user.UserName = "Demo User" + couunt;
            user.Email = "Demo" + couunt  + "@gmail.com";
            _hubContext.Clients.All.SendAsync("PostWhetherData", user);

            return Ok();
        }
    }
}
