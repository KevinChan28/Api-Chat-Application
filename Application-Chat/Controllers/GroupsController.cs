using Application_Chat.DTO;
using Application_Chat.Models;
using Application_Chat.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application_Chat.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GroupsController : ControllerBase
	{
		private readonly IGroup _group;

		public GroupsController(IGroup group)
		{
			_group = group;
		}

		/// <summary>
		/// Crear un grupo
		/// </summary>
		/// <returns>Id del grupo nuevo</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la creación.</response>
		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> Create([FromBody] CreateGroup groupNew)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				string idGroup = await _group.Create(groupNew);

				if (idGroup != null)
				{
					response.Success = true;
					response.Message = "group created";
					response.Data = new { idGroup = idGroup };
				}
				else
				{
					return BadRequest();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			return Ok(response);
		}

		/// <summary>
		/// Obtener todos los grupos
		/// </summary>
		/// <returns>Lista de grupos</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la consulta.</response>
		[HttpGet]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> GetGroups()
		{
			ResponseBase response = new ResponseBase();
			try
			{
				List<Group> groups = await _group.GetAllGroups();
				response.Data = groups;
				response.Success = true;
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			return Ok(response);
		}

		/// <summary>
		/// Eliminar un grupo
		/// </summary>
		/// <returns></returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la acción.</response>
		[HttpDelete("{idGroup}")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> DeleteGroup([FromRoute] string idGroup)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				bool success = await _group.DeleteGroup(idGroup);
				if (!success)
				{
					return NotFound();
				}
				response.Success = true;
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			return Ok(response);
		}


		/// <summary>
		/// actualizar un grupo
		/// </summary>
		/// <returns>id del grupo que se actualizó</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la acción.</response>
		[HttpPatch]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> UpdateGroup([FromBody] ChangeGroup model)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				if (model.id == null | model.Name == null)
				{
					return BadRequest();
				}

				string idGroupUpdated = await _group.UpdateGroup(model);

				if (idGroupUpdated != null)
				{
					response.Success = true;
					response.Data = idGroupUpdated;
					response.Message = "Grupo actualizado";
				}
				else
				{
					return NotFound();
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
