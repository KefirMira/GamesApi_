using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Clients;
using Models.Games.GameDomain;
using Models.Games.GameView;
using Models.Tokens;
using Services.Clients;

namespace GamesApi_.Controllers.Clients
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IClientsService _clientService;
        public ClientsController(ILogger<ClientsController> logger, IClientsService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }
        
        [HttpGet("all"),Authorize]
        public IEnumerable<ClientView> GetAll()
        {
            IEnumerable<ClientDomain> clientDomains = _clientService.GetAllClients();
            return ClientView.Convert(clientDomains);
        }
        
        [HttpPost("authorization")]
        public async Task<IActionResult> Authorization([FromBody]Auth auth)
        {
            if(_clientService.GetClient(auth.Login,auth.Password))
            {
                TokensView tokensView = _clientService.CreateToken(_clientService.GetClientTok(auth.Login,auth.Password),HttpContext);
                if (tokensView != null)
                    return Ok(tokensView);
                else
                    return NotFound();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("registration")]
        public async Task<IActionResult> Create([FromBody]ClientBlank clientBlank)
        {
            if (_clientService.CreateClient(clientBlank))
            {
                return Ok(new { message = "Пользователь создан" });
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody]string token)
        {
            TokensView _token = _clientService.RefreshToken(token);
            if (_token!=null)
            {
                return Ok(_token);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
