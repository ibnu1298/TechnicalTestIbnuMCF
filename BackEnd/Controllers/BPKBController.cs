using AutoMapper;
using BackEnd.DTOs;
using BackEnd.DTOs.BPKB;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BPKBController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IBpkb _bpkb;
        private readonly IMapper _mapper;
        private static readonly BaseResponse baseResponse = new();
        public BPKBController(DataContext context, IBpkb bpkb, IMapper mapper)
        {
            _bpkb = bpkb;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GET()
        {
            try
            {
                ListGetBPKBDTO response = new();
                var results = await _bpkb.GetAll();
                if (results == null || !results.Any())
                {
                    baseResponse.IsSucceeded = false;
                    baseResponse.Message = "Data tidak di temukan";
                    return NotFound(baseResponse);
                }
                response.ListBPKB = _mapper.Map<IEnumerable<GetBPKBDTO>>(results);
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
        [HttpPut("Edit")]
        public async Task<ActionResult> UPDATE(InsertUpdateBPKBDTO request)
        {
            try
            {
                var newData = _mapper.Map<TrBpkb>(request);
                var results = await _bpkb.Update(newData);
                baseResponse.Message = results.Message;
                baseResponse.IsSucceeded = results.IsSucceeded;
                return results.IsSucceeded ? Ok(baseResponse) : BadRequest(baseResponse);
            }
            catch (Exception ex)
            {
                baseResponse.IsSucceeded = false;
                baseResponse.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.InternalServerError, baseResponse);
            }
        }
        [HttpPost("Add")]
        public async Task<ActionResult> INSERT(InsertUpdateBPKBDTO request)
        {
            try
            {
                var newData = _mapper.Map<TrBpkb>(request);
                var results = await _bpkb.Insert(newData);
                baseResponse.Message = results.Message;
                baseResponse.IsSucceeded = results.IsSucceeded;
                return results.IsSucceeded ? Ok(baseResponse) : BadRequest(baseResponse);
            }
            catch (Exception ex)
            {
                baseResponse.IsSucceeded = false;
                baseResponse.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.InternalServerError, baseResponse);
            }
        }
        [HttpGet("{agreemenNum}")]
        public async Task<ActionResult> GETBYAggreemenNum(string agreemenNum)
        {
            try
            {
                GetBPKBDTO response = new();
                var results = await _bpkb.GetById(agreemenNum);
                if (results == null)
                {
                    baseResponse.IsSucceeded = false;
                    baseResponse.Message = "Data tidak di temukan";
                    return NotFound(baseResponse);
                }
                response = _mapper.Map<GetBPKBDTO>(results);
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
