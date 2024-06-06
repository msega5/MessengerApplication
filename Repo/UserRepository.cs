using MessengerApplication.Abstraction;
using MessengerApplication.Context;
using MessengerApplication;
using MessengerApplication.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MessengerApplication.Repo
{
    public class UserRepository : IUserRepository
    {
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

                if (context.Users.ToString()=="admin")
                {
                    throw new Exception("Email is already registered");
                }
                else
                {
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

        public void UserDelete(string email, string password, RoleId roleId)
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

                context.Remove(user);
                context.SaveChanges();
            }
        }

    }
}
