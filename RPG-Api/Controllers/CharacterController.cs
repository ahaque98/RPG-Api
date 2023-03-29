global using RPG_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPG_Api.DTOs.Characters;
using RPG_Api.Service.CharacterService;

namespace RPG_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        private static List<Character> characters = new List<Character>()
        {
            new Character { Id = 1, Name="Frodo"}
        };

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }


        [HttpGet]
        public async Task<ActionResult<List<GetCharacterDTO>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCharacterDTO>> Get(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> UpdateCharacter(UpdateCharacterDTO updateCharacter)
        {
            var response = await _characterService.UpdateCharacter(updateCharacter);
            if (response.Data is null)
            {
                return NotFound(response); 
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpDelete("{id}")] 
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }
    } 
}
