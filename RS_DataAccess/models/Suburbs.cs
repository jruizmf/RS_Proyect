using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RS_DataAccess.models.common;

namespace RS_DataAccess.models
{
    public class Suburbs : BaseEntity
    {
        [ForeignKey("Municipality")]
        public Guid MunicipalityId { get; set; }
        [ForeignKey("City")]
        public Guid CityId { get; set; }

        [Required]
        [Display(Name = "Municipio")]
        [StringLength(80, ErrorMessage = "El Municipio debería menos de 80 caracteres.")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "C.P.")]
        [StringLength(10, ErrorMessage = "El Código Postal debería menos de 10 caracteres.")]
        public string ZIPCode { get; set; }
        public int Status { get; set; }

        public virtual City City { get; protected set; }
        public virtual Municipality Municipality { get; protected set; }
    }
}
