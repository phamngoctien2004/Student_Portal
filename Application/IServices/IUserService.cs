using Application.DTOs.User;
using Core.Entities;

namespace Application.IServices
{
    public interface IUserService : IBaseService<User, UserRequest,UserResponse, UserParams>
    {
        Task<User?> GetEntityByEmail(string email);
        Task ChangePassword(ChangePasswordRequest req);

        Task ResetPassword(ResetPasswordRequest req);
    }
}
