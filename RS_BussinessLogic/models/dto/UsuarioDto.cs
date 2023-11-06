using System;
using System.ComponentModel.DataAnnotations;
using MLGBussinesLogic.models.common;

namespace MLGBussinesLogic.models.dto
{
    public class UsuarioDto : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string UsuarioNombre { get; set; }

        public int Status { get; set; }

        public string Password { get; set; }

        public Guid? ClienteId { get; set; }
     
        public Guid? UsuarioId { get; set; }

        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }

        public DateTime? Fecha { get; set; }
    }
}
