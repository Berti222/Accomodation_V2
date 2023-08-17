﻿using AccomodationWebAPI.DTOs.GetDTOs;
using AccomodationWebAPI.DTOs.PostDTOs;
using AccomodationWebAPI.DTOs.PutDTOs;
using AccomodationWebAPI.Logic.ControllerLogic;
using Microsoft.AspNetCore.Mvc;

namespace AccomodationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : CustomControllerBase<RoomLogic, RoomDTO, RoomPostDTO, RoomPutDTO>
    {
        public RoomController(RoomLogic logic) : base(logic)
        {
        }
    }
}
