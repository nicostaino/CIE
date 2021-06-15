using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
 
using Clases;
using ControlTemperaturaWebApi.Models;
using Servicios.Services.User;

namespace ControlTemperaturaWebApi.Controllers
{
    public class UsersController : ApiController
    {
       // private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        // GET: api/Users


        public List<User> GetUsers()
        {

            List<User> listaRetorno = new List<User>();
            foreach (var usuario in _userService.GetAll())
            {
                var aInsertar = new User();
                aInsertar.Id = usuario.Id;
                aInsertar.Name = usuario.Name;
                aInsertar.Password = usuario.Password;
                aInsertar.Mail = usuario.Mail;
                listaRetorno.Add(aInsertar);
            }
            return listaRetorno;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(long id)
        {
            User user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
      
        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userService.Add(user); 

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

       
        
    }
}
