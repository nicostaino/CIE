using Clases;
using Clases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ControlTemperaturaSitioWeb.CustomAuthentication
{
    public class CustomMembershipUser : MembershipUser
    {
        #region User Properties

        public int Id { get; set; }
        public string UserName { get; set; } 
        public long LandId { get; set; }
        public string Name { get; set; }

        public ICollection<Role> Roles { get; set; }

        #endregion

        public CustomMembershipUser(User user):base("CustomMembership", user.Username, user.Id, user.Mail, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            Id = int.Parse(user.Id.ToString());
            UserName = user.Username;
            Name = user.Name;
            Roles = user.Roles;
            
        }
    }
}