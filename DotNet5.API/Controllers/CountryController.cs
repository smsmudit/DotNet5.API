using AutoMapper;
using DotNet5.API.IRepository;
using DotNet5.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet5.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<CountryController> _logger;
        private readonly IMapper _mapper;
        public CountryController(IUnitOfWork unitOfWork,ILogger<CountryController> logger,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _unitOfWork.Countries.GetAll();
                var result = _mapper.Map<IList<CountryDTO>>(countries);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountries)}");
                return StatusCode(500, "Internal server error.Please try again Later");
            }
        }
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetCountry(int Id)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(a => a.Id == Id, new List<string> { "Hotels" });
                var result = _mapper.Map<CountryDTO>(country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountry)}");
                return StatusCode(500, "Internal server error.Please try again Later");
            }
        }
    }
}
