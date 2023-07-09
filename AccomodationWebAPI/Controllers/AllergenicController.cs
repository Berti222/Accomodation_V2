using AccomodationWebAPI.CustomExceptions;
using AccomodationWebAPI.DTOs.GetDTOs;
using AccomodationWebAPI.DTOs.PostDTOs;
using AccomodationWebAPI.DTOs.PutDTOs;
using AccomodationWebAPI.Logic.ControllerLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccomodationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //public class AllergenicController : ControllerBase
    public class AllergenicController : CustomControllerBase<AllergenicLogic, AllergenicDTO, AllergenicPostDTO, AllergenicPutDTO>
    {
        public AllergenicController(AllergenicLogic logic) : base(logic)
        {
        }
        //private readonly AllergenicLogic allergenicLogic;

        //public AllergenicController(AllergenicLogic allergenicLogic)
        //{
        //    this.allergenicLogic = allergenicLogic;
        //}

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 2)
        //{
        //    try
        //    {
        //        var result = await allergenicLogic.GetAllAsync(pageNumber, pageSize);
        //        return Ok(result);
        //    }
        //    catch (HTTPStatusException ex)
        //    {
        //        return StatusCode((int) ex.StatusCode, ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpGet("{id}", Name = "GetById")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    try
        //    {
        //        var createdInstance = await allergenicLogic.GetAsync(id);
        //        return Ok(createdInstance);
        //    }
        //    catch (HTTPStatusException ex)
        //    {
        //        return StatusCode((int)ex.StatusCode, ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Create([FromBody] AllergenicPostDTO allergenic)
        //{
        //    try
        //    {
        //        var createdInstance = await allergenicLogic.CreateAsync(allergenic);
        //        return CreatedAtRoute("GetById", new { id = createdInstance.Id }, createdInstance);
        //    }
        //    catch (HTTPStatusException ex)
        //    {
        //        return StatusCode((int)ex.StatusCode, ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        await allergenicLogic.DeleteAsync(id);
        //        return NoContent();
        //    }
        //    catch (HTTPStatusException ex)
        //    {
        //        return StatusCode((int)ex.StatusCode, ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpPut]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Update([FromBody] AllergenicPutDTO allergenic)
        //{
        //    try
        //    {
        //        var updatedInstance = await allergenicLogic.UpdateAsync(allergenic);
        //        return Ok(updatedInstance);
        //    }
        //    catch (HTTPStatusException ex)
        //    {
        //        return StatusCode((int)ex.StatusCode, ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}
