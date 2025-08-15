using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalks.API.CustomActionFilters;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
    //https://localhost:/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,IMapper mapper)
        {
            _dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();
            //var regionsDto = new List<RegionDto>();
            //foreach (var region in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetbyId([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetbyIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound(mapper.Map<RegionDto>(regionDomain));
            }


            return Ok(regionDomain);
        }

       //Create endpoint
        [HttpPost]
        [ValidateModel]
        
        public async Task<IActionResult> Create([FromBody] CreateRegionDto createRegionDto)
        {
           
                var regionDomainModel = mapper.Map<Region>(createRegionDto);
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
                // Domianmodel to DTO
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);



                return CreatedAtAction(nameof(GetbyId), new { id = regionDto.Id }, regionDto);
           
            
            
        }


        //Update Region
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionDto)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionDto);
            regionDomainModel = await regionRepository.Update(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // Map DTO to Domain Model

            //Convert Domain Model to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);           
            return  Ok(regionDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (ModelState.IsValid)
            {
                var regionDomainModel = await regionRepository.DeleteAsync(id);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return Ok(regionDto);
            }
            else { return BadRequest(ModelState); }
            
        }
    }
}

