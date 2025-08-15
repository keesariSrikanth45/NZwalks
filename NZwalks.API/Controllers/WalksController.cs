using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;
using NZwalks.API.CustomActionFilters;
namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        

        //Create Task  
        //Post: API/Walks

        [HttpPost]
        [ValidateModelAttribute]
      
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
           
                // Mapping AddWalkRequestDto to Domain model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                await walkRepository.CreateAsync(walkDomainModel);
                //map dDpmaon model to DTO
                var walkDto = mapper.Map<WalkDto>(walkDomainModel);
                return Ok(walkDto);
            




        }

        //GetAll
        //GET: /api/walks?filterOn=Name&filterQuery=Track
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool isAscending =true , [FromQuery] int pagenumber=1, [FromQuery] int pageSize =1000)
        {

            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pagenumber , pageSize);
            // Mapping Domain model to DTO
            
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        //GetById
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetWalkByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            // Mapping Domain model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }


        //Update by action
        [HttpPut]

        [Route("{id:guid}")]
        [ValidateModel]
        

        public async Task<IActionResult> Update([FromRoute] Guid id,UpdateWalkRequestDto updateWalkRequestDto )
        {
            
                //Map Dto to Domain 
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
                if (walkDomainModel == null)
                {
                    return NotFound();

                }

                //mapt DOmain model to Dto

                return Ok(mapper.Map<WalkDto>(walkDomainModel));
           
           



        }

        // Delete
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }



    }
}
