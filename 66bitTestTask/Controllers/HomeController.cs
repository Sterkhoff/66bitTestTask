using _66bitTestTask.Data.Repository;
using _66bitTestTask.Hubs;
using _66bitTestTask.Models;
using _66bitTestTask.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Globalization;

namespace _66bitTestTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMyAppRepository _repo;
        private readonly IHubContext<MyHub> _hubContext;

        public HomeController(IMyAppRepository repo, IMapper mapper, IHubContext<MyHub> hubContext)
        {
            _mapper = mapper;
            _repo = repo;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> ShowSoccerPlayers(string? formMessage = null, bool playerWasUpdated = false)
        {
            var players = await _repo.GetAllSoccerPlayers();
            var playersViewModels = _mapper.Map<SoccerPlayerViewModel[]>(players);
            ViewBag.PlayersViewModels = playersViewModels;
            ViewBag.Teams = await _repo.GetAllTeams();
            ViewBag.FormMessage = formMessage;
            ViewBag.PlayerWasUpdated = playerWasUpdated;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSoccerPlayer(SoccerPlayerViewModel playerViewModel)
        {
            if (ModelState.IsValid)
            {
                playerViewModel.Team = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(playerViewModel.Team);
                var team = await _repo.GetTeamByName(playerViewModel.Team);
                if (playerViewModel.TeamIsNew)
                {
                    if (team != null)
                        return RedirectToAction("ShowSoccerPlayers",
                            new { formMessage = "Изменение не совершено, введённая команда уже существует, выберите её в списке команд" });

                    team = new Team(playerViewModel.Team);
                    await _repo.AddTeam(team);
                }
                var newPlayer = _mapper.Map<SoccerPlayer>(playerViewModel);
                newPlayer.Team = team;
                await _repo.UpdatePlayerById(playerViewModel.Id, newPlayer);
                await _hubContext.Clients.All.SendAsync("Receive");
                return RedirectToAction("ShowSoccerPlayers",
                    new { formMessage = "Изменение успешно совершено", playerWasUpdated = true });
            }
            else
                return RedirectToAction("ShowSoccerPlayers",
                    new { formMessage = "Произошла ошибка сервера, попробуйте еще раз" });
        }

        [HttpGet]
        public async Task<IActionResult> CreateSoccerPlayer(string? formMessage = null, bool playerWasAdded = false)
        {
            ViewBag.FormMessage = formMessage;
            ViewBag.PlayerWasAdded = playerWasAdded;
            ViewBag.Teams = await _repo.GetAllTeams();
            await _hubContext.Clients.All.SendAsync("Receive");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSoccerPlayer(SoccerPlayerViewModel playerViewModel)
        {
            if (ModelState.IsValid)
            {
                playerViewModel.Team = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(playerViewModel.Team);
                var team = await _repo.GetTeamByName(playerViewModel.Team);
                if (playerViewModel.TeamIsNew)
                {
                    if (team != null)
                        return RedirectToAction("CreateSoccerPlayer",
                            new { formMessage = "Такая команда уже добавлена, выберите её в списке команд" });
                    team = new Team(playerViewModel.Team);
                    await _repo.AddTeam(team);
                }
                var player = _mapper.Map<SoccerPlayer>(playerViewModel);
                player.Team = team;
                await _repo.AddSoccerPlayer(player);
                await _hubContext.Clients.All.SendAsync("Receive");
                return RedirectToAction("CreateSoccerPlayer",
                    new { formMessage = "Футболист успешно добавлен", playerWasAdded = true });
            }
            else
                return RedirectToAction("CreateSoccerPlayer",
                    new { formMessage = "Произошла ошибка сервера, попробуйте еще раз" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult DateValidation(DateOnly birthDate)
        {
            if (birthDate.AddYears(1) > DateOnly.FromDateTime(DateTime.Now))
                return Json("Футболист должен быть старше одного года");
            return Json(true);
        }
    }
}
