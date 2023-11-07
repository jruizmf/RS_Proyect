using System;
using System.ComponentModel.DataAnnotations;
using RS_DataAccess.models;
using RS_DataAccess.models.common;

namespace RS_BussinessLogic.models.dto
{
    public class UserDto : BaseEntity
    {
        [Required]

        [Display(Name = "Usuario")]
        [StringLength(80, ErrorMessage = "El Email debería menos de 50 caracteres.")]
        public string UserName { get; set; }
        [Required]
        public int Status { get; set; }
        public string Password { get; set; }

        public byte[] PasswordSalt { get; set; }

        [StringLength(50)]
        public string FacebookAuth { get; set; }
        [StringLength(50)]
        public string GoogleAuth { get; set; }

        public UserProfile UserProfile { get; protected set; }

    }
}
