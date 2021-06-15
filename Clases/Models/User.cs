using Clases.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Clases
{
    public class User : BaseEntity
    {

        [Display(Name = "Nombre de usuario")]
        [Required(ErrorMessage = "Requerido")]
        public string Name { get; set; }
        [Display(Name = "Correo electronico")]
        [Required(ErrorMessage = "Requerido")]
        [EmailAddress(ErrorMessage = "Correo no es válido")] 
        public string Mail { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public string Password { get; set; }
        public virtual ICollection<Land> Land { get; set; }
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Requerido")]
        [Remote("IsUserNameAvailable", "Usuarios", ErrorMessage="El usuario ya existe", HttpMethod="GET", AdditionalFields="Id")]
        public string Username { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        [Display(Name = "Activo?")]
        public bool IsActive { get; set; }
        //public Guid ActivationCode { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

        public User()
        {
            Land = new List<Land>();
            Roles = new List<Role>();
        }
    }
}
