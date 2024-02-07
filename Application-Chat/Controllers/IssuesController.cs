using Application_Chat.DTO;
using Application_Chat.Models;
using Application_Chat.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application_Chat.Controllers
{
	[EnableCors("Cors")]
	[Route("api/[controller]")]
	[ApiController]
	public class IssuesController : ControllerBase
	{
		private readonly IIsue _issue;

		public IssuesController(IIsue issue)
		{
			_issue = issue;
		}

		/// <summary>
		/// Agregar un usuario a un grupo
		/// </summary>
		/// <returns>Id del asunto</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la creación.</response>
		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> AddUser([FromBody] AddUserToGroup model)
		{
			ResponseBase response = new ResponseBase();

			try
			{
				string idIssue = await _issue.AddUserToGroup(model);

				if (idIssue != null)
				{
					response.Success = true;
					response.Message = "User added";
					response.Data = new { IdIssue = idIssue };
				}
				else
				{
					response.Success = false;
					return BadRequest("User not found");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			return Ok(response);
		}


		/// <summary>
		/// Obtener los grupos a los que pertenece un usuario
		/// </summary>
		/// <returns>Lista de grupos</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la consulta.</response>
		[HttpGet]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> GetGroupsOfUser()
		{
			ResponseBase response = new ResponseBase();
			try
			{
				List<ListGroups> groups = await _issue.GetGroupsBelongUserCurrent();
				response.Data = groups;
				response.Success = true;
				response.Message = "Search success";
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			return Ok(response);
		}

        /// <summary>
        /// Agregar un usuario a un grupo por su id
        /// </summary>
        /// <returns>Id del asunto</returns>
        /// <response code="200"> Exito </response>
        /// <response code="500">Ha ocurrido un error en la creación.</response>
        [HttpPost("groupId")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddUserToGroupId([FromBody] AddUserToGroupId model)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                string idIssue = await _issue.AddUserToGroupId(model);

                if (idIssue != null)
                {
                    response.Success = true;
                    response.Message = "User added";
                    response.Data = new { IdIssue = idIssue };
                }
                else
                {
                    response.Success = false;
                    return BadRequest("User not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(response);
        }
    }
}
