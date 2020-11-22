using PROG3050_CVGSClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROG3050_CVGSClub.Helpers
{
    public class CartDependency : ICartDependency
    {
        public double TotalSum(List<Item> cart)
        {
            var totalSum = cart.Sum(item => item.Game.ListPrice * item.Quantity);
            try
            {
                return (double)totalSum;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                throw e;
            }
        }

        public double TaxAmount (double totalBeforeTax, double tax)
        {
            return Math.Round(totalBeforeTax * tax, 2);
        }

        public double FinalCost(double totalBeforeTax, double taxAmount)
        {
            return totalBeforeTax + taxAmount;
        }
    }
}
