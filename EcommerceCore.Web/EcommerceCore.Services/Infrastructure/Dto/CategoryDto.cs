using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceCore.Domain.Enums;

namespace EcommerceCore.Services.Infrastructure.Dto
{
    public class CategoryDto
    {
        public string Name { get; set; }        
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
