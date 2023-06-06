using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Games.GameBlank;
using Models.Games.GameDomain;
using Models.Games.GameView;
using Models.PublishingHouse.PublishingHouseDomain;
using Services.Games;

namespace GamesApi_.Controllers.Games
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> _logger;
        private readonly IGamesService _gameService;

        public GamesController(ILogger<GamesController> logger, IGamesService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }
       
        [HttpGet("all"),Authorize]
        public IEnumerable<GameView> GetAll()
        {
            IEnumerable<GameDomain> gameDomains = _gameService.GetAllGame();
            return GameView.Convert(gameDomains);
        }

        [HttpGet("allPublishers")]
        public IEnumerable<PublishingHouseDomain> GetAllPublishers()
        {
            IEnumerable<PublishingHouseDomain> publishingHouseDomains = _gameService.GetAllPublishers();
            return publishingHouseDomains;
        }
        
        [HttpGet("{gameId}"),Authorize]
        public GameView Get(int gameId)
        {
            GameDomain selectedGame = _gameService.GetGame(gameId);
            return GameView.Convert(selectedGame);
        }

        [HttpPost("create"),Authorize]
        public IActionResult Create([FromBody] GameDomain newGame)
        {
            if (_gameService.CreateGame(newGame))
                return Ok();
            else
                return NotFound();
        }

        [HttpPost("remove"),Authorize]
        public IActionResult Delete(int gameId)
        {
            if (_gameService.DeleteGame(gameId))
                return Ok();
            else
                return NotFound();
            
        }

        [HttpPost("update"),Authorize]
        public IActionResult Update([FromBody]GameDomain gameDomain)
        {
            if (_gameService.UpdateGame(gameDomain))
                return Ok();
            else
                return NotFound();
            
        }
        [HttpPost("createdevelopergame"),Authorize]
        public IActionResult CreateDeveloperGame([FromBody] DeveloperToGame developer)
        {
            if (_gameService.CreateDeveloperToGame(developer))
                return Ok();
            else
                return NotFound();
            
        }
        [HttpPost("createganregame"),Authorize]
        public IActionResult CreateGenreGame([FromBody] GenresToGame genre)
        {
            if (_gameService.CreateGenresToGame(genre))
                return Ok();
            else
                return NotFound();
            
        }
    }
}
