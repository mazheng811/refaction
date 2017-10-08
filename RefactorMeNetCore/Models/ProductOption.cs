using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RefactorMeNetCore.Models
{
    public partial class ProductOption
    {
        public Guid Id { get; set; }        
        [JsonIgnore]
        public Guid ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }        
    }
}
