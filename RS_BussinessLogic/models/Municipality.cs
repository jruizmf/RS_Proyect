using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RS_DataAccess.models.common;

namespace RS_BussinessLogic.models
{
    public class Municipality : BaseEntity
    {
        [ForeignKey("State")]
        public Guid StateId { get; set; }

        [Required]
        [Display(Name = "Municipio")]
        [StringLength(80, ErrorMessage = "El Municipio debería menos de 80 caracteres.")]
        public string Description { get; set; }
        public int Status { get; set; }
        public virtual State State { get; protected set; }
    }
}
