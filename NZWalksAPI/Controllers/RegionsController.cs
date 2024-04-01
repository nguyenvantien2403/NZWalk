using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace NZWalksAPI.Controllers
{
    ///https://localhost:7061/api/regions             khi url gọi đến tên sau gạch chéo regions sẽ tương ứng với bộ điều khiển RegionsController và phương thức bên dưới sẽ hoạt động
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet] //đang tạo phương thức lấy tất cả nên sẽ dùng httpget

        /*    public IActionResult GetAll()
            {
               *//* var region = dbContext.Regions.ToList();//dang su dung lop dbContext truy cap truc tiep vao database
                return Ok (region);*//*
                //cach hoat dong cua DT0
                //lay du lieu tu database do vao domain model
                var regionsDomain = dbContext.Regions.ToList();
                var regionDto = new List<RegionsDto>();
                foreach (var regionDomain in regionsDomain)
                {
                    regionDto.Add(new RegionsDto()
                    {
                        Id = regionDomain.Id,
                        Code = regionDomain.Code,
                        Name = regionDomain.Name,
                        RegionImageUrl = regionDomain.RegionImageUrl,

                    });    
                }
                return Ok(regionDto);
                //sau do anh xa domain model voi dto de dua ket qua sang dto tra cho client
            }*/
        public async Task<IActionResult> GetAll()// async Task la lap trinh bat dong bo, giup cho hieu nang tot hon
        {
            // var regionsDomain = await dbContext.Regions.ToListAsync();//su dung dbtext de lay du lieu
            var regionsDomain = await regionRepository.GetAllAsync();//khi co repository thi khong dung cau lenh ben tren nua, goi den interface luon
            /*var regionDto = new List<RegionsDto>();
             foreach (var regionDomain in regionsDomain) //anh xa thu cong
             {
                 regionDto.Add(new RegionsDto()
                 {
                     Id = regionDomain.Id,
                     Code = regionDomain.Code,
                     Name = regionDomain.Name,
                     RegionImageUrl = regionDomain.RegionImageUrl,

                 });
             }*/

            //anh xa tu domainmodel sang dto su dung auto mapper nen khong can su dung doan tren
            var regionDto = mapper.Map<List<RegionsDto>>(regionsDomain);
            return Ok(regionDto);
            //sau do anh xa domain model voi dto de dua ket qua sang dto tra cho client
        }

        //get region by id
        //https://localhost:7061/api/regions/{id}  
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById ([FromRoute] Guid id) {

            /* var region = dbContext.Regions.Find(id); chi dung khi truyen vao tham so la id(khoa chinh), ko duoc su dung voi tham so khac*/
            /* var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);*/
            var regionDomain = await regionRepository.GetByIdAsync(id); //lay du lieu thong qua repository
            if (regionDomain == null)
            {
                return NotFound();
            }

            /* var regionDto = new RegionsDto()
             {
                 Id = regionDomain.Id,
                 Code = regionDomain.Code,
                 Name = regionDomain.Name,
                 RegionImageUrl = regionDomain.RegionImageUrl,

             };*/
            var regionDto = mapper.Map<List<RegionsDto>>(regionDomain);//anh xa tu domain model sang dto ngoac nhon la dich, ngoac tron la nguon
            return Ok(regionDto);

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //anh xa hoac chuyen doi dto thanh domain model
            /*var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };*/
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);//



            //sau do dung domain model de tao moi region su dung dbcontext

            /*  await dbContext.Regions.AddAsync(regionDomainModel);
              await dbContext.SaveChangesAsync();//luu lai thay doi*/

            //tao regionDomainModel moi bang phuong thuc ben repository
             regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //anh xa domain model tro ve dto
            /* var regionDto = new RegionsDto
             {
                 Id = regionDomainModel.Id,
                 Code = regionDomainModel.Code,
                 Name = regionDomainModel.Name,
                 RegionImageUrl = regionDomainModel.RegionImageUrl,
             };*/
            var regionDto = mapper.Map<RegionsDto>(regionDomainModel);//ngoac nhon la dich, ngoac tron la nguon
            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDomainModel);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)//truyen id vao de kiem tra xem ton tai hay khong, neu co thi su dung id do de cap nhat thong tin
        {
            //su dung phuong thuc ben repository
            /*var regionDomainModel = new Region
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl,

            };*/
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            //kiem tra xem id co ton tai khong
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);



            //kiem tra xem id co ton tai khong
            /*  var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);//truy cap vao db bang dbcontext*/
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //anh xa dto toi domain model
            /* regionDomainModel.Code = updateRegionRequestDto.Code;
             regionDomainModel.Name = updateRegionRequestDto.Name;
             regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
             await dbContext.SaveChangesAsync();*/


            //anh xa domain model ve dto
            /*  var regionDto = new RegionsDto
              {
                  Id = regionDomainModel.Id,
                  Code = regionDomainModel.Code,
                  Name = regionDomainModel.Name,
                  RegionImageUrl = regionDomainModel.RegionImageUrl
              };*/
            var regionDto = mapper.Map<RegionsDto>(regionDomainModel);
            return Ok(regionDto);
        }

        //delete
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            //su dung phuong thuc trong repository
            var regionDomainModel = await regionRepository.DeleteAsync(id);

           /* var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);//truy cap db bang dbcontext*/
            if(regionDomainModel == null)
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
            var regionDto = mapper.Map<RegionsDto>(regionDomainModel);
            return Ok(regionDto);

        }
       
    }
}
