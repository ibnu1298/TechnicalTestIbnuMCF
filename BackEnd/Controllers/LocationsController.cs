using AutoMapper;
using BackEnd.DTOs.BPKB;
using BackEnd.DTOs;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BackEnd.DTOs.Location;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILocation _location;
        private readonly IMapper _mapper;
        private static readonly BaseResponse baseResponse = new();
        public LocationsController(DataContext context, ILocation location, IMapper mapper)
        {
            _location = location;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GET()
        {
            try
            {
                ListGetLocation response = new();
                var results = await _location.GetAll();
                if (results == null || !results.Any())
                {
                    baseResponse.IsSucceeded = false;
                    baseResponse.Message = "Data tidak di temukan";
                    return NotFound(baseResponse);
                }
                response.ListStorageLocation = _mapper.Map<IEnumerable<GetLocation>>(results);
                response.IsSucceeded = true;
                response.Message = "Succeeded";
                return Ok(response);
            }
            catch (Exception ex)
            {
                baseResponse.IsSucceeded = false;
                baseResponse.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.InternalServerError, baseResponse);
            }
        }
    }
}
