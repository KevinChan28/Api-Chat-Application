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
	public class MessagesController : ControllerBase
	{
		private readonly IMessage _message;

		public MessagesController(IMessage message)
		{
			_message = message;
		}


		/// <summary>
		/// Registrar un mensaje
		/// </summary>
		/// <returns>Id del message</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la creación.</response>
		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> RegisterUser([FromBody] SendMessage message)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				string idMessage = await _message.CreateMessage(message);
				if (idMessage != null)
				{
					response.Success = true;
					response.Message = "message register";
					response.Data = new { IdUser = idMessage };
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
		/// Obtener todos los mensajes
		/// </summary>
		/// <returns>Lista de mensajes</returns>
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
				List<Message> messages = await _message.GetAllMessages();
				response.Message = "Search success";
				response.Data = messages;
				response.Success = true;
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			return Ok(response);
		}

		/// <summary>
		/// Cambiar el contenido de un mensaje
		/// </summary>
		/// <returns>Id del mensaje actualizado</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la consulta.</response>
		[HttpPatch("content")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> ChangeContentMessage([FromBody] string text, string idMessage)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				string message = await _message.ChangeContentMessage(text, idMessage);

				if (message != null)
				{
					response.Message = "Updated successfully";
					response.Data = message;
					response.Success = true;
				}
				else
				{
					return BadRequest("Message not found");
				}

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			return Ok(response);
		}

		/// <summary>
		/// Eliminar un mensaje
		/// </summary>
		/// <returns></returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la consulta.</response>
		[HttpDelete("{idMessage}")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> DeleteMessage([FromRoute] string idMessage)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				bool success = await _message.DeleteMessage(idMessage);

				if (success != false)
				{
					response.Message = "Delete successfully";
					response.Data = success;
					response.Success = true;
				}
				else
				{
					return BadRequest("Message not found");
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
