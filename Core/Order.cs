﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Order
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime CreationDate { get; set; }

        public Order(int id, User user)
        {
            Id = id;
            User = user;
            CreationDate = DateTime.Now;
        }

        public override string ToString()        
            => $"Order Id: {Id}, User: {User.Name}, Creation Date {CreationDate:dd/MM/yyyy}";
        
    }
}
