using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Projections
{
    public class OperationTypeInfo
    {
        public string Name { get; set; }
        public int Amount { get; set; }

        public OperationTypeInfo()
        { }

        public OperationTypeInfo(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }
    }
}
