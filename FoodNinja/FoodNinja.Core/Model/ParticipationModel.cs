using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Model
{
    public class ParticipationModel
    {
        public int ParticipationId { get; set; }
        public int OrderId { get; set; }
        public int NinjaUserId { get; set; }
        public string FavoriteChoice { get; set; }
        public string Selection { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}