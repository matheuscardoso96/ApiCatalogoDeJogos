using ApiCatalogoDeJogos.Exceptions;
using ApiCatalogoDeJogos.Models.InputModels;
using ApiCatalogoDeJogos.Models.ViewModels;
using ApiCatalogoDeJogos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoDeJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int count = 1) 
        {
            var result = await _gameService.Get(page, count);
            if (result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("{gameId:guid}")]
        public async Task<ActionResult<GameViewModel>> Get([FromRoute]Guid gameId)
        {
            var game = await _gameService.Get(gameId);
            if (game == null)
            {
                return NoContent();
            }

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameInputModel>> Insert([FromBody]GameInputModel gameInputModel) 
        {            
            try
            {
                await _gameService.Insert(gameInputModel);
                return Ok(gameInputModel);
            }
            catch (GameAlreadyInDataBaseException ex)
            {
                return UnprocessableEntity("This game already exist.");

            }
            
        }

        [HttpPut("{gameId:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute]Guid gameId, [FromBody]GameInputModel game)
        {          
            try
            {
                await _gameService.Update(gameId, game);
                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return UnprocessableEntity("This game is not registered in database.");

            }
        }

        [HttpPatch("{gameId:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGamePrice([FromRoute] Guid gameId, [FromRoute] double price)
        {
            try
            {
                await _gameService.Update(gameId, price);
                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return UnprocessableEntity("This game is not registered in database.");

            }

        }

        [HttpDelete("{gameId:guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid gameId)
        {           
            try
            {
                await _gameService.Delete(gameId);
                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return UnprocessableEntity("This game is not registered in database.");

            }
        }
    }
}
