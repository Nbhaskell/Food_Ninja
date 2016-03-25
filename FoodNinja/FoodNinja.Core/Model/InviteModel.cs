using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Model
{
    public class InviteModel
    {
        public int InviteId { get; set; }
        public int TeamId { get; set; }
        public string Token { get; set; }

        public bool Invalid
        {
            get
            {
                byte[] data = Convert.FromBase64String(Token);
                DateTime timestamp = DateTime.FromBinary(BitConverter.ToInt64(data, 0));

                return timestamp < DateTime.UtcNow.AddHours(-48);
            }
        }
    }
}