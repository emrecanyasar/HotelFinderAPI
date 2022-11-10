using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelFinderAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelsController : ControllerBase
    {
        private IHotelService _hotelService;
        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        /// <summary>
        /// Get All Hotels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hotels=await _hotelService.GetAllHotels();
            return Ok(hotels);//200+data
        }

        /// <summary>
        /// Get Hotels By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")] // api/hotels/GetHotelById/1
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel=await _hotelService.GetHotelById(id);
            if (hotel!=null)
            {
                return Ok(hotel);//200+data
            }
            return NotFound();//404
        }

        [HttpGet]
        [Route("[action]/{name}")] // api/hotels/GetHotelByName/1
        public async Task<IActionResult> GetHotelByName(string name)
        {
            var hotel =await _hotelService.GetHotelByName(name);
            if (hotel != null)
            {
                return Ok(hotel);//200+data
            }
            return NotFound();//404
        }

        /// <summary>
        /// Create an Hotels
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateHotel([FromBody]Hotel hotel)
        {
           var createdHotel=await _hotelService.CreateHotel(hotel);
            return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel);
        }

        /// <summary>
        /// Update the Hotels
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel hotel)
        {
            if (await _hotelService.GetHotelById(hotel.Id) != null)
            {
                return Ok(await _hotelService.UpdateHotel(hotel));
            }
            return NotFound();
        }

        /// <summary>
        /// Delete the Hotels
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (await _hotelService.GetHotelById(id) != null)
            {
                await _hotelService.DeleteHotel(id);
                return Ok();
            }
            return NotFound();
             
        }
    }
}
