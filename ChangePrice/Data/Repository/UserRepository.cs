using ChangePrice.Data.Dto;
using ChangePrice.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Linq;

namespace ChangePrice.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private TestCryptoCreatQueryContext _db;

        public UserRepository(TestCryptoCreatQueryContext context)
        {
            _db = context;
        }

        public List<UserDto> GetAllUser()
        {
            var userList = _db.UserName.ToList();

            List< UserDto > userDtoList = new List< UserDto >();

            foreach (var userItem in userList)
            {
                UserDto userDto = new UserDto()
                {
                    UserId = userItem.UserId,
                    EmailAddress = userItem.EmailAddress,
                    Name = userItem.Name,
                    IsActive = userItem.IsActive
                };
                userDtoList.Add(userDto);
            }

            return userDtoList;
        }

        public UserDto GetUserById(int userId)
        {
            var user = _db.UserName.Find(userId);
            UserDto userDto = new UserDto()
            {
                UserId = user.UserId,
                EmailAddress = user.EmailAddress,
                Name = user.Name,
                IsActive = user.IsActive
            };
            return userDto;
        }
        public void InsertUser(UserName User)
        {
            _db.UserName.Add(User);
        }

        public void UpdateUser(UserName User)
        {
            _db.Entry(User).State = EntityState.Modified;
        }
        public void DeleteUser(UserName User)
        {
            _db.Entry(User).State = EntityState.Deleted;

        }

        public void DeleteUserById(int userId)
        {
            var user = _db.UserName.Find(userId);
            if (user != null)
            {
                DeleteUser(user);
            }
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
