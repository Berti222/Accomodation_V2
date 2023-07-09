using AccomodationWebAPI.CustomExceptions;
using AccomodationWebAPI.DTOs;
using AccomodationWebAPI.Logic.ControllerLogic;
using Microsoft.AspNetCore.Mvc;

namespace AccomodationWebAPI.Controllers
{
    public abstract class CustomControllerBase<TLogic, TGetDTO, TPostDTO, TPutDTO> : ControllerBase 
                                                                            where TLogic : ControllerLogicBase<TGetDTO, TPostDTO, TPutDTO>
                                                                            where TGetDTO : IDTOWithId
                                                                            where TPostDTO : class
                                                                            where TPutDTO : class
    {
        private readonly TLogic logic;

        public CustomControllerBase(TLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 2)
        {
            try
            {
                var result = await logic.GetAllAsync(pageNumber, pageSize);
                return Ok(result);
            }
            catch (HTTPStatusException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetById(int id)
        {
            try
            {
                var createdInstance = await logic.GetAsync(id);
                return Ok(createdInstance);
            }
            catch (HTTPStatusException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> Create([FromBody] TPostDTO postDTO)
        {
            try
            {
                var createdInstance = await logic.CreateAsync(postDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdInstance.Id }, createdInstance);
            }
            catch (HTTPStatusException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> Delete(int id)
        {
            try
            {
                await logic.DeleteAsync(id);
                return NoContent();
            }
            catch (HTTPStatusException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> Update([FromBody] TPutDTO putDTO)
        {
            try
            {
                var updatedInstance = await logic.UpdateAsync(putDTO);
                return Ok(updatedInstance);
            }
            catch (HTTPStatusException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
