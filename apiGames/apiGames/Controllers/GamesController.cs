using apiGames.Middlewares.Exceptions;
using apiGames.Models;
using apiGames.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Search and show all registered games
        /// </summary>
        /// <remarks>It is not possible to return games without pagination</remarks>
        /// <param name="page">Shows the number of the page under consult. Min: 1</param>
        /// <param name="quantity">Determines the number of games per page. Min: 1 - Max: 50</param>
        /// <response code="200">Returns the game list</response>
        /// <response code="204">In case of there is no game to show</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> GetList([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var games = await _gameService.GetPageList(page, quantity);
            if (games.Count() == 0)
                return NoContent();
            return Ok(games);
        }

        /// <summary>
        /// Search a game by its Id
        /// </summary>
        /// <param name="id">Searcher game's Id</param>
        /// <response code="200">Returns the searched game</response>
        /// <response code="204">In case of there is no game with this Id</response>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<List<GameViewModel>>> Get([FromRoute] Guid id)
        {
            var game = await _gameService.GetById(id);
            if (game == null)
                return NoContent();

            return Ok();
        }
        /// <summary>
        /// Insert a new game
        /// </summary>
        /// <param name="gameInputModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.Insert(gameInputModel);
                return Ok(game);
            }
            catch(AlreadyRegisteredException ex)
            {
                return UnprocessableEntity(ex);
            }
        }
        /// <summary>
        /// Update a registered game
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gameInputModel"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid id, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Update(id, gameInputModel);
                return Ok();
            }
            catch(NonRegisteredException ex)
            {
                return NotFound(ex);
            }
        }
        /// <summary>
        /// Updates only a game's price
        /// </summary>
        /// <param name="id"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpPatch("{id:guid}/price/{price:double}")]
        public async Task<ActionResult> PriceUpdate([FromRoute] Guid id, [FromRoute] double price)
        {
            try
            {
                await _gameService.PriceUpdate(id, price);
                return Ok();
            }
            catch(NonRegisteredException ex)
            {
                return NotFound(ex);
            }
        }
        /// <summary>
        /// Delete a registered game
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid id)
        {
            try
            {
                await _gameService.Remove(id);
                return Ok();
            }
            catch(NonRegisteredException ex)
            {
                return NotFound(ex);
            }
        }
    }
}
