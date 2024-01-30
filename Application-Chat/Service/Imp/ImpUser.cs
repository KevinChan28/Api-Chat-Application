using Application_Chat.DTO;
using Application_Chat.Models;
using Application_Chat.Repository;
using Application_Chat.Security;
using Application_Chat.Tools;

namespace Application_Chat.Service.Imp
{
	public class ImpUser : IUser
	{
		private IUserRepository _userRepository;
		private JwtSettings _jwtSettings;

		public ImpUser(IUserRepository userRepository, JwtSettings jwtSettings)
		{
			_userRepository = userRepository;
			_jwtSettings = jwtSettings;
		}


		public async Task<string> Create(CreateUser model)
		{
			User user = new User
			{
				Email = model.Email,
				Password = Encrypt.GetSHA256(model.Password),
				UserName = model.UserName,
				Image = model.Image
			};

			string idUser = await _userRepository.CreateUser(user);

			return idUser;
		}

		public async Task<bool> Delete(string idUser)
		{
			User userFind = await _userRepository.GetUserById(idUser);

			if (userFind != null)
			{
				await _userRepository.DeleteUser(idUser);

				return true;
			}

			return false;
		}


		public async Task<List<User>> GetAllUsers()
		{
			return await _userRepository.GetAllUsers();
		}

		public async Task<User> GetUserById(string idUser)
		{
			User user = await _userRepository.GetUserById(idUser);

			if (user != null)
			{
				return user;
			}

			return null;
		}

		public async Task<string> UpdateNameOfUser(string nameUserNew, string idUser)
		{
			User user = await _userRepository.GetUserById(idUser);

			if (user != null)
			{
				user.UserName = nameUserNew;

				string idUserUpdated = await _userRepository.UpdateUser(user);

				return idUserUpdated;
			}

			return null;
		}

		public async Task<string> ValidateCredentials(Login login)
		{
			User userByEmail = await _userRepository.GetUserByEmail(login.User, Encrypt.GetSHA256(login.Password));
			User userByUserName = await _userRepository.GetUserByUserName(login.User, Encrypt.GetSHA256(login.Password));

			if (userByEmail != null | userByUserName != null)
			{
				string token;

				User userFind = userByEmail == null ? userByUserName : userByEmail;

				UserTokens userTokens = new UserTokens
				{
					Email = userFind.Email,
					Id = userFind.Id,
					UserName = userFind.UserName,
					GuidId = Guid.NewGuid(),
					Rol = string.Empty,
				};
				token = JwtHelppers.GenerateToken(userTokens, _jwtSettings);

				return token;
			}

			return null;
		}
	}
}
