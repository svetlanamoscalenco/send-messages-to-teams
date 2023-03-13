using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using System.Diagnostics;
using TeamsMessages.Models;

namespace TeamsMessages.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GraphApiClientUI _graphApiClientUI;

        public HomeController(ILogger<HomeController> logger, GraphApiClientUI graphApiClientUI)
        {
            _logger = logger;
            _graphApiClientUI = graphApiClientUI;
        }

        [AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
        public IActionResult Index()
        {
            var user = _graphApiClientUI.GetGraphApiUser().Result;            
            var channelName = _graphApiClientUI.GetChannelDisplayName();
            var model = new MessageViewModel() { ChannelName = channelName, UserDisplayName = user.DisplayName};

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(MessageViewModel messageViewModel)
        {
            var chatMessage = _graphApiClientUI.SendMessageToTeamsChannel(messageViewModel.Message).Result;            
            TempData["PostAction"] = "Message was sent successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}