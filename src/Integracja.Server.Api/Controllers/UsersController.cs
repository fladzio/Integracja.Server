﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Integracja.Server.Api.Attributes;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : DefaultController
    {
        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;
        private readonly IGamemodeService _gamemodeService;
        private readonly IGameUserService _gameUserService;
        private readonly IPictureService _pictureService;

        public UsersController(ICategoryService categoryService, IQuestionService questionService, IGamemodeService gamemodeService, IGameUserService gameUserService, IPictureService pictureService)
        {
            _categoryService = categoryService;
            _questionService = questionService;
            _gamemodeService = gamemodeService;
            _gameUserService = gameUserService;
            _pictureService = pictureService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoryDto>> CreatedCategories()
        {
            return await _categoryService.GetOwned<CategoryDto>(UserId.Value);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("[action]")]
        public async Task<IEnumerable<QuestionDto>> CreatedQuestions()
        {
            return await _questionService.GetOwned<QuestionDto>(UserId.Value);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("[action]")]
        public async Task<IEnumerable<GamemodeDto>> CreatedGamemodes()
        {
            return await _gamemodeService.GetOwned<GamemodeDto>(UserId.Value);
        }

        /// <summary>
        /// Returns games that user joined and are upcoming or ready to play
        /// </summary>
        /// <response code="200">Successful operation</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(IEnumerable<GameUserDto<GameDto>>), StatusCodes.Status200OK)]
        [Mobile]
        [HttpGet("[action]")]
        public async Task<IEnumerable<GameUserDto<GameDto>>> Games()
        {
            return await _gameUserService.GetActive<GameUserDto<GameDto>>(UserId.Value);
        }

        /// <summary>
        /// Returns games that user joined and can't play
        /// </summary>
        /// <response code="200">Successful operation</response>
        /// <response code="500">Internal server error</response>
        [Mobile]
        [ProducesResponseType(typeof(IEnumerable<GameUserDto<GameDto>>), StatusCodes.Status200OK)]
        [HttpGet("[action]")]
        public async Task<IEnumerable<GameUserDto<GameDto>>> GamesArchived()
        {
            return await _gameUserService.GetArchived<GameUserDto<GameDto>>(UserId.Value);
        }

        /// <summary>
        /// Returns game that user joined by id 
        /// </summary>
        /// <param name="id">Id of the game</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid id supplied</response>
        /// <response code="500">Internal server error</response>
        [Mobile]
        [ProducesResponseType(typeof(GameUserDto<DetailGameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpGet("[action]/{id}")]
        public async Task<GameUserDto<DetailGameDto>> Games(int id)
        {
            return await _gameUserService.Get<GameUserDto<DetailGameDto>>(id, UserId.Value);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [HttpPost("[action]")]
        public async Task<IActionResult> UploadPicture([Required] IFormFile profilePicture)
        {
            var uri = await _pictureService.Save(profilePicture, UserId.Value);
            return Created(uri, null);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("[action]")]
        public async Task<IActionResult> DeletePicture()
        {
            await _pictureService.Delete(UserId.Value);
            return NoContent();
        }
    }
}
