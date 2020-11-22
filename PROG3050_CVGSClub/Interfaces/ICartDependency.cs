using PROG3050_CVGSClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROG3050_CVGSClub.Interfaces
{
    public interface ICartDependency
    {
        double TotalSum(List<Item> cart);

        double TaxAmount(double totalBeforeTax, double tax);

        double FinalCost(double totalBeforeTax, double taxAmount);
    }
}
