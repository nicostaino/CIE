using AccessoADatos;
using AccessoADatos.Core.Interfaces;
using Autofac;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Web;


namespace ControlTemperaturaWebApi.Models
{
    public class EntityFrameworkModule : Autofac.Module
    {


        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(AppIngresoTemperatrasContext)).As(typeof(IContext)).InstancePerLifetimeScope();
        }

    }
}