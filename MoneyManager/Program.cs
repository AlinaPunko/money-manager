using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyManager.Models;

namespace MoneyManager
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ApplicationContext())
            {
                Console.WriteLine("Hello");
            }
        }
    }
}
