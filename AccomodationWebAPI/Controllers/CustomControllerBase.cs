using AccomodationWebAPI.CustomExceptions;
using AccomodationWebAPI.DTOs;
using AccomodationWebAPI.Logic.ControllerLogic;
using AccomodationWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Specialized;

namespace AccomodationWebAPI.Controllers
{
    public abstract class CustomControllerBase<TLogic, TGetDTO, TPostDTO, TPutDTO> : ControllerBase 
                                                                            where TLogic : ControllerLogicBase<TGetDTO, TPostDTO, TPutDTO>
                                                                            where TGetDTO : IDTOWithId
                                                                            where TPostDTO : class
                                                                            where TPutDTO : class
    {
        protected readonly TLogic logic;
        protected readonly ILoggingHelper loggingHelper;

        public CustomControllerBase(TLogic logic, ILoggingHelper loggingHelper)
        {
            this.logic = logic;
            this.loggingHelper = loggingHelper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 2)
        {
            try
            {
                loggingHelper.LogInformation("{ControlleName}/GetAll method calld with parameters: pageNumber: {pageNumber} and pageSize: {pageSize}"
                                             , GetType().Name, pageNumber, pageSize);
                var result = await logic.GetAllAsync(pageNumber, pageSize);
                return Ok(result);
            }
            catch (HTTPStatusException ex)
            {
                loggingHelper.LogHttpException(ex, GetType().Name, "GetAll");
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                loggingHelper.LogGeneralException(ex, GetType().Name, "GetAll");
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
                Log.Information("{controllerName}/GetById/{id} called", GetType().Name, id);
                var createdInstance = await logic.GetAsync(id);
                return Ok(createdInstance);
            }
            catch (HTTPStatusException ex)
            {
                loggingHelper.LogHttpException(ex, GetType().Name, "GetById");
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                loggingHelper.LogGeneralException(ex, GetType().Name, "GetById");
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
                loggingHelper.LogObjectPassedAsParameter(GetType().Name, nameof(Create), postDTO);
                var createdInstance = await logic.CreateAsync(postDTO);
                loggingHelper.LogObjectReturnedAtCreateOrUpdate(GetType().Name, nameof(Create), createdInstance);
                return CreatedAtAction(nameof(GetById), new { id = createdInstance.Id }, createdInstance);
            }
            catch (HTTPStatusException ex)
            {
                loggingHelper.LogHttpException(ex, GetType().Name, "Create");
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                loggingHelper.LogGeneralException(ex, GetType().Name, "Create");
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
                loggingHelper.LogInformation("{controller}/Delete method called with id {id}", GetType().Name, id);
                await logic.DeleteAsync(id);
                return NoContent();
            }
            catch (HTTPStatusException ex)
            {
                loggingHelper.LogHttpException(ex, GetType().Name, "Delete");
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                loggingHelper.LogGeneralException(ex, GetType().Name, "Delete");
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
                loggingHelper.LogObjectPassedAsParameter(GetType().Name, nameof(Update), putDTO);
                var updatedInstance = await logic.UpdateAsync(putDTO);
                loggingHelper.LogObjectReturnedAtCreateOrUpdate(GetType().Name, nameof(Update), updatedInstance);
                return Ok(updatedInstance);
            }
            catch (HTTPStatusException ex)
            {
                loggingHelper.LogHttpException(ex, GetType().Name, "Update");
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                loggingHelper.LogGeneralException(ex, GetType().Name, "Update");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
