using System;
using System.Collections.Generic;
using A2.Models;

namespace A2.Data
{
    public interface IWebAPIRepo
    {
        public IEnumerable<User> GetUsers();
        public User Register(User user);
        public bool ValidLogin(string userName, string password);
        public Order PurchaseItem(Order order);
    }
}
