using AutoMapper;
using MessengerApplication.Abstraction;
using MessengerApplication.Models;
using MessengerApplication.Models.DTO;
using System;

namespace MessengerApplication.Repo
{
    public class UserRepository : IUserRepository
    {
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
}
