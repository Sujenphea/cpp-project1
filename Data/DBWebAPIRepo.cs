using System;
using System.Collections.Generic;
using System.Linq;
using A2.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace A2.Data
{
    public class DBWebAPIRepo: IWebAPIRepo
    {
        private readonly MyDbContext _dbContext;

        public DBWebAPIRepo(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _dbContext.Users.ToList<User>();
            return users;
        }

        public User Register(User user)
        {
            EntityEntry<User> e = _dbContext.Users.Add(user);
            User u = e.Entity;
            _dbContext.SaveChanges();

            return u;
        }

        public bool ValidLogin(string userName, string password)
        {
            User u = _dbContext.Users.FirstOrDefault(u => u.UserName == userName && u.Password == password);

            if (u == null)
            {
                return false;
            }

            return true;
        }


        public Order PurchaseItem(Order order)
        {
            EntityEntry<Order> e = _dbContext.Orders.Add(order);
            Order o = e.Entity;
            _dbContext.SaveChanges();

            return o;
        }
    }
}
