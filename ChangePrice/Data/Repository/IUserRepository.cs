using ChangePrice.Data.DataBase;
using ChangePrice.Data.Dto;

namespace ChangePrice.Data.Repository
{
    public interface IUserRepository
    {
        
        List<UserDto> GetAllUser();
        UserDto GetUserById(int userId);
        void InsertUser(UserName user);
        void UpdateUser(UserName user);
        void DeleteUserById(int userId);
        void DeleteUser(UserName user);



        void Save();
    }
}
