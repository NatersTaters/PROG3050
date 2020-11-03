using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROG3050_CVGSClub.Models
{
    public class ProductModel
    {
        private List<Games> products;

        public ProductModel()
        {
            this.products = new List<Games>() {
                new Games {
                    GameId = 1,
                    GameName = "Halo",
                    ListPrice = 60,
                    ContentRating = "M",
                    Genre = "FPS",
                    AvailablePlatforms = "XBOX",
                    MaxPlayers = "16"
                }
            };
        }

        public List<Games> findAll()
        {
            return this.products;
        }

        public Games find(int id)
        {
            return this.products.Single(p => p.GameId.Equals(id));
        }

    }
}
