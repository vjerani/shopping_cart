using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingCartDemo.DataAccessLayer.Entities
{
    public class PromotionCode : BaseEntity
    {
        /// <summary>
        /// Gets or sets the label for discount code.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        
        public float Amount { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is discount or otherwise is off cost price reduction, <c>false</c>.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is discount; otherwise, <c>false</c>.
        /// </value>
        public bool IsDiscount { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [use in conjuction].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use in conjuction]; otherwise, <c>false</c>.
        /// </value>
        public bool UseInConjuction { get; set; }
    }
}
