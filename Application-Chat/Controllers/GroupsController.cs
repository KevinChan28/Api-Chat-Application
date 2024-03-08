using Application_Chat.DTO;
using Application_Chat.Models;
using Application_Chat.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application_Chat.Controllers
{
	[Authorize]
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
		[Authorize]
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
		/// actualizar nombre de un grupo
		/// </summary>
		/// <returns>id del grupo que se actualizó</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la acción.</response>
		[HttpPatch("ChangeName")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> ChangeNameGroup([FromBody] ChangeGroup model)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				if (model.id == null | model.Name == null)
				{
					return BadRequest();
				}

				string idGroupUpdated = await _group.UpdateNameOfGroup(model.Name, model.id);

				if (idGroupUpdated != null)
				{
					response.Success = true;
					response.Data = idGroupUpdated;
					response.Message = "Nombre de grupo actualizado";
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

        /// <summary>
        /// actualizar descripción de un grupo
        /// </summary>
        /// <returns>id del grupo que se actualizó</returns>
        /// <response code="200"> Exito </response>
        /// <response code="500">Ha ocurrido un error en la acción.</response>
        [HttpPatch("ChangeDescription")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ChangeDescriptionGroup([FromBody] ChangeGroup model)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                if (model.id == null | model.Description ==null)
                {
                    return BadRequest();
                }

                string idGroupUpdated = await _group.UpdateDescriptionOfGroup(model.Description, model.id);

                if (idGroupUpdated != null)
                {
                    response.Success = true;
                    response.Data = idGroupUpdated;
                    response.Message = "Descripción de grupo actualizado";
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

        /// <summary>
        /// actualizar imagen de un grupo
        /// </summary>
        /// <returns>id del grupo que se actualizó</returns>
        /// <response code="200"> Exito </response>
        /// <response code="500">Ha ocurrido un error en la acción.</response>
        [HttpPatch("ChangeImage")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ChangeImageGroup([FromBody] ChangeGroup model)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                if (model.id == null | model.Image == null)
                {
                    return BadRequest();
                }

                string idGroupUpdated = await _group.UpdateImagenOfGroup(model.Image, model.id);

                if (idGroupUpdated != null)
                {
                    response.Success = true;
                    response.Data = idGroupUpdated;
                    response.Message = "Imagen de grupo actualizado";
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

        /// <summary>
        /// actualizar visibilidad de un grupo
        /// </summary>
        /// <returns>id del grupo que se actualizó</returns>
        /// <response code="200"> Exito </response>
        /// <response code="500">Ha ocurrido un error en la acción.</response>
        [HttpPatch("ChangeVisibility")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ChangeVisibilityGroup([FromBody] ChangeGroup model)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                if (model.id == null | model.Visibility == null)
                {
                    return BadRequest();
                }

                string idGroupUpdated = await _group.UpdateVisibilityOfGroup(model.Visibility, model.id);

                if (idGroupUpdated != null)
                {
                    response.Success = true;
                    response.Data = idGroupUpdated;
                    response.Message = "Visibilidad de grupo actualizado";
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

        /// <summary>
        /// Obtener todos los grupos publicos
        /// </summary>
        /// <returns>Lista de grupos publicos</returns>
        /// <response code="200"> Exito </response>
        /// <response code="500">Ha ocurrido un error en la consulta.</response>
        [HttpGet("PublicGroups")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPublicGroups()
        {
            ResponseBase response = new ResponseBase();
            try
            {
                List<Group> publicGroups = await _group.GetAllPublicGroups();
                response.Data = publicGroups;
                response.Success = true;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(response);
        }
    }
}
