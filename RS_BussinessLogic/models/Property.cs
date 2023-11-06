using RS_DataAccess.models;
using RS_DataAccess.models.common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS_BussinessLogic.models
{
    public class Property : BaseEntity
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Display(Name = "nombre")]
        [Required(ErrorMessage = "El nombre es requerida.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debería tener entre 3 y 50 caracteres.")]
        public string Title { get; set; }
        [Display(Name = "Descripción")]
        [StringLength(200, ErrorMessage = "La descripción debería menos de 200 caracteres.")]
        public string Description { get; set; }

        [Display(Name = "Tags")]
        [StringLength(14, MinimumLength = 1)]
        public string[] Tags { get; set; }

        [Display(Name = "Precio")]
        public float Price { get; set; }
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
