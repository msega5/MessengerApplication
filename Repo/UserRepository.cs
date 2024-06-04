using AutoMapper;
using MessengerApplication.Abstraction;
using MessengerApplication.Models;
using MessengerApplication.Models.DTO;
using System;
using System.Security.Cryptography;
using System.Text;

namespace MessengerApplication.Repo
{
    public class UserRepository : IUserRepository
    {

        /*{
            private IMapper _mapper;
            private MessengerContext _context;

            public UserRepository(IMapper mapper, MessengerContext context)
            {
                _mapper = mapper;
                _context = context;
            }

            public void AddUser(UserDTO user)
            {
                _context.Users.Add(_mapper.Map<User>(user));
                _context.SaveChanges();
            }

            public bool Exists(string email) => _context.Users.Count(x => x.Active && x.Email == email) > 0;

            public bool Exists(Guid id) => _context.Users.Count(x => x.Active && x.Id == id) > 0;

            public IEnumerable<UserDTO> ListUsers()
            {
                return _context.Users.Select(_mapper.Map<UserDTO>).ToList();
            }
        }
    */
        public void UserAdd(string email, string password, RoleId roleId)
        {
            using (var context = new MessengerContext())
            {
                if (roleId == RoleId.Admin)
                {
                    var c = context.Users.Count(x => x.RoleId == RoleId.Admin);
                    if (c > 0)
                    {
                        throw new Exception("Only one admin may be registered");
                    }
                }

                var user = new User();
                user.Email = email;
                user.RoleId = roleId;

                user.Salt = new byte[16];
                new Random().NextBytes(user.Salt);

                var data = Encoding.ASCII.GetBytes(password).Concat(user.Salt).ToArray();
                SHA512 shaM = new SHA512Managed();
                user.Password = shaM.ComputeHash(data);
                context.Add(user);
                context.SaveChanges();
            }
        }

        public RoleId UserCheck(string email, string password)
        {
        using (var context = new MessengerContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Email == email);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var data = Encoding.ASCII.GetBytes(password).Concat(user.Salt).ToArray();
                SHA512 shaM = new SHA512Managed();
                var bpassword = shaM.ComputeHash(data);

                if (user.Password.SequenceEqual(bpassword))
                {
                    return user.RoleId;
                }
                else
                {
                    throw new Exception("Wrong password");
                }
            }
        }

    }
}
