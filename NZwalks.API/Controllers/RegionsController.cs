using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
    //https://localhost:/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            _dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();
            var regionsDto = new List<RegionDto>();
            foreach (var region in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetbyId([FromRoute] Guid id)
        {
            var region = regionRepository.GetbyId(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }

        [HttpPost]
        [Route("{idc:Guid}")]
        public async Task<IActionResult> Create([FromBody] CreateRegionDto createRegionDto)
        {
            var regionDomainModel = new Region
            {
                Code = createRegionDto.Code,
                Name = createRegionDto.Name,
                RegionImageUrl = createRegionDto.RegionImageUrl
            };
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
            // Domianmodel to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };


            return CreatedAtAction(nameof(GetbyId), new { id = regionDto.Id }, regionDto);
        }


        //Update Region
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionDto)
        {
            var regionDomainModel = new Region
            {
                Code = updateRegionDto.Code,
                Name = updateRegionDto.Name,
                RegionImageUrl = updateRegionDto.RegionImageUrl
            };
            regionDomainModel = await regionRepository.Update(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // Map DTO to Domain Model

            //Convert Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);

        }
    }
}

