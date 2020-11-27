using Microsoft.AspNetCore.Http;
using PROG3050_CVGSClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROG3050_CVGSClub.Interfaces
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

        public double TaxAmount (double totalBeforeTax)
        {
            double taxRate = 0.13;
            return Math.Round(totalBeforeTax * taxRate, 2);
        }

        public double FinalCost(double totalBeforeTax, double taxAmount)
        {
            return totalBeforeTax + taxAmount;
        }

        public List<Item> AddToCart(Games games, List<Item> existingCart)
        {
            List<Item> cart = new List<Item>();
            if (existingCart != null)
                cart = existingCart;

            cart.Add(new Item
            {
                Game = games,
                Quantity = 1
            });

            return cart;
        }

        public int FindGameIndex (int id, List<Item> cart)
        {
            try
            {
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].Game.GameId.Equals(id))
                        return i;
                }

                return -1;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                throw e;
            }
        }

        public List<Item> BuyGame(int id, List<Item> cart, Games games)
        {
            var index = FindGameIndex(id, cart);
            List<Item> newCart;

            if (index != -1)
            {
                cart[index].Quantity++;
                newCart = cart;
            }
            else
                newCart = AddToCart(games, cart);

            return newCart;
        }

        public List<Item> RemoveGame(int id, List<Item> cart)
        {
            var index = FindGameIndex(id, cart);
            cart.RemoveAt(index);
            return cart;
        }

        public GamesLibrary GameLibrary(List<Item> cart, string memberId, int i)
        {
            GamesLibrary gamesLibrary = new GamesLibrary();
            gamesLibrary.GameId = cart[i].Game.GameId;
            gamesLibrary.MemberId = memberId;
            return gamesLibrary;
        }
    }
}
