using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;


namespace NZWalksAPI.Data
{
    public class NZWalksDbContext : DbContext
    {
       
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }




        //tao du lieu cho bang difficulties tren sql
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //seeding data for dificulties
            //easy, meidum, hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("46f2f526-fba7-4981-95ca-01d36d9c61b8"),  //cach generous id vao view > other window > c# interface
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("4dbf8308-c583-45f7-b9e0-1329241eca4e") ,
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("2c12e36f-f729-4388-8c9f-04634f9a9c28"),
                    Name = "Hard"
                }
            };
            //seed difficulties for data base (tao bang difficulties cho co so du lieu tren sql)
            //dua du lieu vao co so du lieu tren sql bang entity
            modelBuilder.Entity<Difficulty>().HasData(difficulties);  //trong ngoac tron la cung cap cho no mot mang hoac danh sach


            //seeding data for regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);

            //sau khi tao du lieu xong thi phai chay lenh update database de du lieu duoc cap nhat trong sql
            //tool > nuget packet manager > packet manager console chay lenh add-migration "abc..."  sau do chay lenh update-database
        }
    }
}
