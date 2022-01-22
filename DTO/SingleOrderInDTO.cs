using System;
using System.ComponentModel.DataAnnotations;

namespace A2.DTO
{
    public class SingleOrderInDTO
    {
        [Required] public int ProductID { get; set; }
    }
}
