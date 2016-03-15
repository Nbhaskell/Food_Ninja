using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Model
{
    public class RestaurantOptionModel
    {
        public int RestaurantOptionId { get; set; }
        public int OrderId { get; set; }
        public DateTime CreatedDate { get; set; }

        public IEnumerable<ParticipationModel> Participations { get; set; }
    }
}