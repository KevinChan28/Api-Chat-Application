using Application_Chat.DTO;
using Application_Chat.Models;
using Application_Chat.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application_Chat.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUser _user;

		public UsersController(IUser user)
		{
			_user = user;
		}

		/// <summary>
		/// Registrar un usuario
		/// </summary>
		/// <returns>Id del usuario nuevo</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la creación.</response>
		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> RegisterUser([FromBody] CreateUser userNew)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				string IdUser = await _user.Create(userNew);
				if (IdUser != null)
				{
					response.Success = true;
					response.Message = "user register";
					response.Data = new { IdUser = IdUser };
				}
				else
				{
					return BadRequest();
				}
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			return Ok(response);
		}

		/// <summary>
		/// Obtener todos los usuarios
		/// </summary>
		/// <returns>Lista de usuarios</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la consulta.</response>
		[HttpGet]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> GetUsers()
		{
			ResponseBase response = new ResponseBase();
			try
			{
				List<User> users = await _user.GetAllUsers();
				response.Data = users;
				response.Success = true;
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			return Ok(response);
		}

		/// <summary>
		/// Eliminar un usuario
		/// </summary>
		/// <returns></returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la acción.</response>
		[HttpDelete("{idUser}")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> DeleteUser([FromRoute] string idUser)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				bool success = await _user.Delete(idUser);
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
		/// Cambiar el UserName de un usuario
		/// </summary>
		/// <returns>id del usuario que se actualizó</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la acción.</response>
		[HttpPatch]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> ChangeNameUser([FromBody] ChangeUserName model)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				if (model.IdUser == null | model.UserName == null)
				{
					return BadRequest();
				}

				string idUserUpdated = await _user.UpdateNameOfUser(model.UserName, model.IdUser);
				if (idUserUpdated != null)
				{
					response.Success = true;
					response.Data = idUserUpdated;
					response.Message = "Name user changed";
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
		/// Validar credenciales del login
		/// </summary>
		/// <returns>Token</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la creación.</response>
		[HttpPost("Login")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> ValidateCredentials([FromBody] Login login)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				string token = await _user.ValidateCredentials(login);

				if (token != null)
				{
					response.Success = true;
					response.Message = "Credentials validated";
					response.Data = token;
				}
				else
				{
					response.Success = false;
					response.Message = "Credentials incorrect";
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
