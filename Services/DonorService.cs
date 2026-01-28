using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using ChineseAuctionAPI.Models.QueryParams;
using System.Drawing;

namespace ChineseAuctionAPI.Services
{
    public class DonorService: IDonorService
    {
        private const string Location = "DonorService";
        private readonly IDonorRepo _repo;
        private readonly IMapper _mapper;
        public DonorService(IDonorRepo repo,IMapper mapper)
        {
            _repo=repo;
            _mapper = mapper;
            
        }

        public async Task<IEnumerable<DonorReadDTO>> GetDonors(DonorQParams donorParams)
        {

            var donors = await _repo.GetDonors(donorParams);
            if (donors == null) return Enumerable.Empty<DonorReadDTO>();
            return _mapper.Map<IEnumerable<DonorReadDTO>>(donors);
        }

        public async Task<DonorReadDTO> FindDonorById(int id)
        {
            Donor donorEntity = await _repo.FindDonorById(id);

            if (donorEntity == null)
            {
                throw new ErrorResponse(404, "FindDonorById",
                    "Donor not found.",
                    $"No donor found with ID {id}.", "GET", Location);
            }

            return _mapper.Map<DonorReadDTO>(donorEntity);
        }

        public async Task AddDonor(DonorCreateDTO donor)
        {
            var donors = await _repo.GetDonors();
            bool existingDonor = donors.Any(d => d.Email == donor.Email);
            if (existingDonor)
            {
                throw new ErrorResponse(409, "AddDonor",
                    "A donor with this email already exists.",
                    $"Conflict: Donor email {donor.Email} is already registered.", "POST", Location);
            }

            Donor donorEntity = _mapper.Map<Donor>(donor);
            if (donorEntity == null)
            {
                throw new ErrorResponse(500, "AddDonor", "Error processing donor data.", "AutoMapper failed to map DonorCreateDTO.", "POST", "srv");
            }

            await _repo.AddDonor(donorEntity);
        }

        public async Task UpdateDonor(DonorUpdateDTO donor)
        {
            var donors = await _repo.GetDonors();
            bool existingDonor = donors.Any(d => d.Id == donor.Id);
            if (!existingDonor)
            {
                throw new ErrorResponse(409, "UpdateDonor",
                    "Donor to update not found.",
                    $"ID {donor.Id} not found.", "PUT", Location);
            }

            existingDonor = donors.Any(d => d.Email == donor.Email);
            if (existingDonor && donors.FirstOrDefault(d => d.Email == donor.Email)?.Id != donor.Id)
            {
                throw new ErrorResponse(409, "UpdateDonor",
                    "This email is already taken.",
                   $"Email conflict for update: {donor.Email}", "PUT", Location);
            }

            Donor donorEntity = _mapper.Map<Donor>(donor);
            await _repo.UpdateDonor(donorEntity);
        }

        public async Task DeleteDonor(int id)
        {
            var donors=await _repo.GetDonors();
            bool existingDonor = donors.Any(d => d.Id == id);
            if (!existingDonor)
            {
                throw new ErrorResponse(404, "DeleteDonor",
                    "Donor to delete not found.",
                    $"Delete failed: ID {id} does not exist.", "DELETE", Location);
            }
            await _repo.DeleteDonor(id);
        }






    }
}
