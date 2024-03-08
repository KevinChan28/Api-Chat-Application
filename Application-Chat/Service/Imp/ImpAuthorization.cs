using System.Security.Claims;

namespace Application_Chat.Service.Imp
{
	public class ImpAuthorization : IAuthorization
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ImpAuthorization(IHttpContextAccessor contextAccessor)
		{
			_httpContextAccessor = contextAccessor;
		}


		public string GetRolUserCurrent()
		{
			ClaimsIdentity Identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

			if (Identity.IsAuthenticated != false)
			{
				string rol = (Identity.FindFirst(ClaimTypes.Role).Value);

				return rol;
			}

			return null;
		}

		public string UserCurrent()
		{
			ClaimsIdentity Identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

			if (Identity.IsAuthenticated != false)
			{
				string idUsuarioActual = Identity.FindFirst(ClaimTypes.NameIdentifier).Value;

				return idUsuarioActual;
			}

			return null;
		}
	}
}
