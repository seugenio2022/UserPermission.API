using Microsoft.AspNetCore.Mvc;
using UserPermission.API.Application.Commands.ModifyPermission;
using UserPermission.API.Application.Commands.RequestPermission;
using UserPermission.API.WebUI.Controllers;
using UserPermission.API.Application.Queries.GetPermissions;

namespace WebUI.Controllers
{
    public class PermissionController : ApiControllerBase
    {
        [HttpPost]
        [Route("Request")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Request(RequestPermissionCommand requestPermission) => Created(string.Empty,await Mediator.Send(requestPermission));

        [HttpPut]
        [Route("Modify")]
        public async Task<IActionResult> Modify(ModifyPermissionCommand modifyPermission) 
        { 
            await Mediator.Send(modifyPermission);
            return Ok();
        }
       
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get() => Ok(await Mediator.Send(new GetPermissionsQuery()));
    }
}
