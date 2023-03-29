using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RPG_Api.Data;
using RPG_Api.DTOs.Characters;

namespace RPG_Api.Service.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        } 
        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            var character = _mapper.Map<Character>(newCharacter);

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters.Select(x => _mapper.Map<GetCharacterDTO>(x)).ToListAsync();
            return serviceResponse;
        }


        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            var dbCharacter = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacter.Select(x => _mapper.Map<GetCharacterDTO>(x)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDTO>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();

            try 
            {
                var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == updateCharacter.Id);

                if (character is null)
                {
                    throw new Exception($"Character with ID: {updateCharacter.Id} not found :(");
                }
                _mapper.Map(updateCharacter, character);

                character.Name = updateCharacter.Name;
                character.Hitpoint = updateCharacter.Hitpoint;
                character.Strength = updateCharacter.Strength;
                character.Defence = updateCharacter.Defence;
                character.Intelligence = updateCharacter.Intelligence;
                character.Charaters = updateCharacter.Charaters;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        } 

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);

                if (character is null)
                {
                    throw new Exception($"Character with ID: {id} not found :(");
                }
                _context.Remove(character);
                await _context.SaveChangesAsync(); 

                serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}