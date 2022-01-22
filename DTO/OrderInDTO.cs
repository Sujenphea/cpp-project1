using System;
using System.ComponentModel.DataAnnotations;

namespace A2.DTO
{
    public class OrderInDTO
    {
        [Required] public int ProductID { get; set; }
        [Required] public int Quantity { get; set; }
    }
}
