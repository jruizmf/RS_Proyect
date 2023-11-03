using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RS_DataAccess.models.common;

namespace RS_DataAccess.models
{
    public class User : BaseEntity
    {
        [Required]

        [Display(Name = "Usuario")]
        [StringLength(80, ErrorMessage = "El Email debería menos de 50 caracteres.")]
        public string UserName { get; set; }
        [Required]
        public int Status { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }

        [StringLength(50)]
        public string FacebookAuth { get; set; }
        [StringLength(50)]
        public string GoogleAuth { get; set; }

        public UserProfile Profile { get; protected set; }



    }
}
