using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
            await walkRepository.CreateAsync(walkDomainModel);

          
            return Ok(mapper.Map<WalksDto>(walkDomainModel));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();
            //map domain model to dto
            return Ok(mapper.Map<List<WalksDto>>(walksDomainModel));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if(walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalksDto>(walkDomainModel));
        }

        //update by id
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            //map dto to domain model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            //map domain model to dto
            return Ok(mapper.Map<WalksDto>(walkDomainModel));

        }
        //delete by id
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //su dung phuong thuc trong repository
            var walkDomainModel = await walkRepository.DeleteAsync(id);

            /* var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);//truy cap db bang dbcontext*/
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            //delete
            /* dbContext.Regions.Remove(regionDomainModel);
             await dbContext.SaveChangesAsync();//su dung phuong thuc trong repository thi khong can xoa o day*/

            //anh xa domain model sang dto
            /*var regionDto = new RegionsDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };*/
             
            return Ok(mapper.Map<WalksDto>(walkDomainModel));
        }
    }
}
