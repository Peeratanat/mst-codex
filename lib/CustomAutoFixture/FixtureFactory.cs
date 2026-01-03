using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoFixture;using CustomAutoFixture;
using AutoFixture.Kernel;
using Database.Models;
using Database.Models.MST;
using Database.Models.USR;

namespace CustomAutoFixture
{
    public class FixtureFactory
    {
        private readonly static Fixture fixture = new Fixture();

        public static Fixture Get()
        {
            //fixture.Customizations.Add(
            //    new PropertyTypeOmitter(
            //        typeof(User)));
            fixture.Customizations.Add(new PropertyNameOmitter("CreatedBy"));
            fixture.Customizations.Add(new PropertyNameOmitter("UpdatedBy"));
            fixture.Customizations.Add(new PropertyNameOmitter("CreatedByUserID"));
            fixture.Customizations.Add(new PropertyNameOmitter("UpdatedByUserID"));
            fixture.Customizations.Add(new PropertyNameOmitter("UserAuthorizeProjects"));
            fixture.Customizations.Add(new PropertyNameOmitter("UserRoles"));
            fixture.Customizations.Add(new PropertyNameOmitter("LastOpportunityID"));
            fixture.Customizations.Add(new PropertyNameOmitter("LastOpportunity"));
            fixture.Customizations.Add(new PropertyNameOmitter("LastOpportunityActivityID"));
            fixture.Customizations.Add(new PropertyNameOmitter("LastOpportunityActivity"));
            fixture.Customizations.Add(new PropertyNameOmitter("OwnerID"));
            fixture.Customizations.Add(new PropertyNameOmitter("Owner"));
            fixture.Customizations.Add(new PropertyNameOmitter("TitledeedDetails"));
            fixture.Customizations.Add(new PropertyNameOmitter("Items"));
            fixture.Customizations.Add(new PropertyNameOmitter("SubDistricts"));

            //Ignore all List in Database.Models
            Assembly assembly = Assembly.LoadFrom("Database.Models.dll");
            if (assembly != null)
            {
                var models = assembly.GetTypes()
                                      .Where(t => t.Namespace != null && t.Namespace.Contains("Database.Models"))
                                      .ToList();

                foreach (var model in models)
                {
                    var listProps = model.GetProperties().Where(o => o.PropertyType.Name.Contains("List")).ToList();
                    foreach (var prop in listProps)
                    {
                        fixture.Customizations.Add(new PropertyNameOmitter(prop.Name));
                    }
                }
            }


            return fixture;
        }
    }

    internal class PropertyNameOmitter : ISpecimenBuilder
    {
        private readonly IEnumerable<string> names;

        internal PropertyNameOmitter(params string[] names)
        {
            this.names = names;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var propInfo = request as PropertyInfo;
            if (propInfo != null && names.Contains(propInfo.Name))
                return new OmitSpecimen();

            return new NoSpecimen();
        }
    }

    internal class PropertyTypeOmitter : ISpecimenBuilder
    {
        private readonly Type type;

        internal PropertyTypeOmitter(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            this.type = type;
        }

        internal Type Type
        {
            get { return this.type; }
        }

        public object Create(object request, ISpecimenContext context)
        {
            var propInfo = request as PropertyInfo;
            if (propInfo != null && propInfo.PropertyType == type)
                return new OmitSpecimen();

            return new NoSpecimen();
        }
    }
}
