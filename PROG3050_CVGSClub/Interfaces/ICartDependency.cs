using Microsoft.AspNetCore.Mvc;
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

        double TaxAmount(double totalBeforeTax);

        double FinalCost(double totalBeforeTax, double taxAmount);

        List<Item> AddToCart(Games games, List<Item> existingCart);

        int FindGameIndex(int id, List<Item> cart);

        List<Item> BuyGame(int id, List<Item> cart, Games games);

        List<Item> RemoveGame(int id, List<Item> cart);

        GamesLibrary GameLibrary(List<Item> cart, string memberId, int i);

    }
}
