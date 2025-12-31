using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using static ChineseAuctionAPI.DTO.PackageDTO;

namespace ChineseAuctionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;


        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadPackageDTO>>> GetAllPackages()
        {
            var packages = await _packageService.GetPackages();
            return Ok(packages);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadPackageDTO>> GetPackageById(int id)
        {
            var package = await _packageService.GetPackageById(id);
            if (package == null) return NotFound();
            return Ok(package);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePackage(CreatePackageDTO createPackageDTO)
        {
            await _packageService.AddPackage(createPackageDTO);
            return Ok(201);
        }

    }
}
