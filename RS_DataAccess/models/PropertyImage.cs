using RS_DataAccess.models;
using RS_DataAccess.models.common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS_DataAccess.models
{
    public class PaymentMethods : BaseEntity
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre de empleado es requerido.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debería tener entre 3 y 50 caracteres.")]
        public string Name { get; set; }
        [Display(Name = "Apellido Paterno")]
        [StringLength(50, ErrorMessage = "El apellido paterno debería menos de 50 caracteres.")]
        public string Lastname { get; set; }
        [Display(Name = "Apellido Materno")]
        [StringLength(50, ErrorMessage = "El apellido materno debería menos de 50 caracteres.")]
        public string Secondlastname { get; set; }


        [Display(Name = "Email")]
        [RegularExpression("^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)", ErrorMessage = "Email Invalido")]
        [StringLength(80, ErrorMessage = "El Email debería menos de 50 caracteres.")]
        public string Email { get; set; }

        [Display(Name = "RFC")]
        [RegularExpression("^([A-z]{4}[0-9]{6}[A-z0-9]{3})", ErrorMessage = "RFC Invalido")]
        [StringLength(14, MinimumLength = 1)]
        public string RFC { get; set; }

        [Display(Name = "Estado")]
        [ForeignKey("State")]
        public Guid StateId { get; set; }
        [Display(Name = "País")]
        [ForeignKey("Country")]
        public Guid CountryId { get; set; }
        [Display(Name = "Localidad")]
        [ForeignKey("City")]
        public Guid CityId { get; set; }

        [Display(Name = "Colonia")]
        [StringLength(40)]
        public string AddressNeighborhood { get; set; }
        [Display(Name = "Calle")]
        [StringLength(40, MinimumLength = 1)]
        public string AddressStreet { get; set; }
        [Display(Name = "Numero Externo")]
        [StringLength(8, MinimumLength = 1)]
        public string AddressNumber { get; set; }

        [Display(Name = "Código Postal")]
        [StringLength(8, MinimumLength = 1)]
        public string ZIP { get; set; }


        public User User { get; set; }
    }
}
