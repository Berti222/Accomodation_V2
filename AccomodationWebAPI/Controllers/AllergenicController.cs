using AccomodationWebAPI.CustomExceptions;
using AccomodationWebAPI.DTOs.GetDTOs;
using AccomodationWebAPI.DTOs.PostDTOs;
using AccomodationWebAPI.DTOs.PutDTOs;
using AccomodationWebAPI.Logic.ControllerLogic;
using AccomodationWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccomodationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergenicController : CustomControllerBase<AllergenicLogic, AllergenicDTO, AllergenicPostDTO, AllergenicPutDTO>
    {
        public AllergenicController(AllergenicLogic logic, ILoggingHelper loggingHelper) 
            : base(logic, loggingHelper)
        {
        }
    }
}
