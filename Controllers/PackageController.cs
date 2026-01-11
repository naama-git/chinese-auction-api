using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ChineseAuctionAPI.DTO.PackageDTO;

namespace ChineseAuctionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;
        private readonly IValidator<CreatePackageDTO> _createValidator;
        private readonly IValidator<UpdatePackageDTO> _updateValidator;



        public PackageController(IPackageService packageService,IValidator<CreatePackageDTO> createValidator, IValidator<UpdatePackageDTO> updateValidator)
        {
            _packageService = packageService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePackage([FromBody] CreatePackageDTO createPackageDTO)
        {
            var validationResult=await _createValidator.ValidateAsync(createPackageDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }
            await _packageService.AddPackage(createPackageDTO);
            return Ok(201);
        }

        [HttpPut("{packageId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePackage(int packageId, [FromBody] UpdatePackageDTO updatePackageDTO)
        {
            var validationResult = await _updateValidator.ValidateAsync(updatePackageDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }
            await _packageService.UpdatePackage(packageId, updatePackageDTO);
            return Ok();
        }

        [HttpDelete("{packageId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePackage(int packageId)
        {
            await _packageService.DeletePackage(packageId);
            return Ok();
        }

    }
}
