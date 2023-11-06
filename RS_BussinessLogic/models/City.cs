using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RS_DataAccess.models.common;

namespace RS_DataAccess.models
{
    public class City : BaseEntity
    {
        [Required]
        [Display(Name = "Ciudad")]
        [StringLength(80, ErrorMessage = "La Ciudad debería menos de 80 caracteres.")]
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
