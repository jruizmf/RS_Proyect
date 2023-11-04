using RS_DataAccess.models;
using RS_DataAccess.models.common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS_DataAccess.models
{
    public class PropertyImage : BaseEntity
    {
        [ForeignKey("Property")]
        public Guid PropertyId { get; set; }

        [Display(Name = "URL")]
        [Required(ErrorMessage = "La URL es requerida.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debería tener entre 3 y 50 caracteres.")]
        public string URL { get; set; }
        [Display(Name = "Descripción")]
        [StringLength(200, ErrorMessage = "La descripción debería menos de 200 caracteres.")]
        public string Description { get; set; }

    }
}
