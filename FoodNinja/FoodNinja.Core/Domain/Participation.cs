using FoodNinja.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class Participation
    {
        public Participation()
        {

        }
        public Participation(ParticipationModel participation) : this()
        {
            Update(participation);
            CreatedDate = DateTime.Now;
            NinjaUserId = participation.NinjaUserId;
            Selection = participation.Selection;
        }

        public int ParticipationId { get; set; }
        public int OrderId { get; set; }
        public int RestaurantLocationId { get; set; }
        public int NinjaUserId { get; set; }
        public bool FavoriteChoice { get; set; }
        public string Selection { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual NinjaUser NinjaUser { get; set; }
     //   public virtual Order Order { get; set; }
        public virtual RestaurantOption Option { get; set; }

        public void Update(ParticipationModel participation)
        {

        }
    }
}