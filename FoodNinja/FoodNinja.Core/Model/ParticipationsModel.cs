using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Model
{
    public class ParticipationsModel
    {
        public string ParticipationId { get; set; }
        public string OrderId { get; set; }
        public string NinjaUserId { get; set; }
        public string FavoriteChoice { get; set; }
        public string Selection { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}