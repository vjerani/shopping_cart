using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartDemo.ApplicationLayer.Dto
{
    public class PromotionCodeOutput
    {
        public bool Valid { get; set; }
        public PromotionCodeDto Code { get; set; }
    }

    public class PromotionCodeDto
    {
        public string Label { get; set; }
        public float Discount { get; set; }
        public float Offprice { get; set; }
        public bool UseInConjuction { get; set; }
    }
}
