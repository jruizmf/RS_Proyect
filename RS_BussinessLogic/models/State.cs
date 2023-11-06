using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RS_DataAccess.models.common;

namespace RS_BussinessLogic.models
{
    public class State : BaseEntity
    {
        [ForeignKey("User")]
        public Guid CountryId { get; set; }

        [Required]
        [Display(Name = "Estado")]
        [StringLength(80, ErrorMessage = "El Estado debería menos de 80 caracteres.")]
        public string Description { get; set; }
        [StringLength(10, ErrorMessage = "El código de estadoi debería menos de 10 caracteres.")]
        public string Code { get; set; }
        public int Status { get; set; }

        public virtual Country Country { get; protected set; }



    }
}
