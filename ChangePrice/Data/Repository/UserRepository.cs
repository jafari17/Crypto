//using ChangePrice.Data.DataBase;
//using ChangePrice.Data.Dto;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Xml.Linq;

//namespace ChangePrice.Data.Repository
//{
//    public class UserRepository : IUserRepository
//    {
//        private CryptoDbContext _db;

//        public UserRepository(CryptoDbContext context)
//        {
//            _db = context;
//        }

//        public List<UserDto> GetAllUser()
//        {
//            var userList = _db.UserService.ToList();

//            List< UserDto > userDtoList = new List< UserDto >();

//            foreach (var userItem in userList)
//            {
//                UserDto userDto = new UserDto()
//                {
//                    UserId = userItem.UserId,
//                    EmailAddress = userItem.EmailAddress,
//                    Name = userItem.Name,
//                    IsActive = userItem.IsActive
//                };
//                userDtoList.Add(userDto);
//            }

//            return userDtoList;
//        }

//        public UserDto GetUserById(int userId)
//        {
//            var user = _db.UserService.Find(userId);
//            UserDto userDto = new UserDto()
//            {
//                UserId = user.UserId,
//                EmailAddress = user.EmailAddress,
//                Name = user.Name,
//                IsActive = user.IsActive
//            };
//            return userDto;
//        }
//        public void InsertUser(UserService User)
//        {
//            _db.UserService.Add(User);
//        }

//        public void UpdateUser(UserService User)
//        {
//            _db.Entry(User).State = EntityState.Modified;
//        }
//        public void DeleteUser(UserService User)
//        {
//            _db.Entry(User).State = EntityState.Deleted;

//        }

//        public void DeleteUserById(int userId)
//        {
//            var user = _db.UserService.Find(userId);
//            if (user != null)
//            {
//                DeleteUser(user);
//            }
//        }
//        public void Save()
//        {
//            _db.SaveChanges();
//        }
//    }
//}
