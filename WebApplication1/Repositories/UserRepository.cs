using DoctorsApp.Context;
using DoctorsApp.Exceptions;
using DoctorsApp.Interfaces;
using DoctorsApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DoctorsApp.Repositories
{
    public class UserRepository : IRepositories<string, User>
    {
        private readonly RequestTrackerContext _context;
        public UserRepository(RequestTrackerContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User item)
        {
            _context.Add(item);
            _context.SaveChanges();
            return item;
        }

        public async Task<User> Delete(string key)
        {
            var user = await GetAsync(key);
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
                return user;
            }
            throw new NoSuchUserException();
        }

        public async Task<User> GetAsync(string key)
        {
            var users = await GetAsync();
            var user = users.FirstOrDefault(e => e.Username == key);
            if (user != null)
            {
                return user;
            }
            throw new NoSuchUserException();
        }

        public async Task<List<User>> GetAsync()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public async Task<User> Update(User item)
        {
            var user = await GetAsync(item.Username);
            if (user != null)
            {
                _context.Entry<User>(item).State=EntityState.Modified;
                _context.SaveChanges();
                return user;
            }
            throw new NoSuchUserException();
        }
    }
}
