namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AccessoADatos.AppIngresoTemperatrasContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AccessoADatos.GestorInventarioEmpresasContext";
        }

        protected override void Seed(AccessoADatos.AppIngresoTemperatrasContext context)
        {
            //context.Lands.AddOrUpdate(new Clases.Land { Name = "Chimpay", Id = 1 });
            //context.Lands.AddOrUpdate(new Clases.Land { Name = "Concordia", Id = 2 });
            //context.Lands.AddOrUpdate(new Clases.Land { Name = "Sarmiento", Id = 3 });
            //context.Lands.AddOrUpdate(new Clases.Land { Name = "Metan", Id = 4 });
            //context.Lands.AddOrUpdate(new Clases.Land { Name = "Gamorel", Id = 5 });
            //context.TaskTypes.AddOrUpdate(new Domain.Entities.TaskType() { Name = "Feriados", Code = "Fer", Id =2 });
            //context.TaskTypes.AddOrUpdate(new Domain.Entities.TaskType() { Name = "Dia Estudio", Code = "DES", Id =3 });
            //context.TaskTypes.AddOrUpdate(new Domain.Entities.TaskType() { Name = "Dia por Enfermedad", Code = "DEN", Id = 4});
            //context.TaskTypes.AddOrUpdate(new Domain.Entities.TaskType() { Name = "Licencia especial", Code = "LES", Id = 5 });
        }
    }
}
