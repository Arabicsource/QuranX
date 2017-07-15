using QuranX.DomainClasses.Entities;
using System.Data.Entity.Migrations;
using QuranX.DomainClasses.Services;
using System;
using System.Xml.Linq;
using System.IO;
using System.Reflection;

namespace QuranX.DomainClasses.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<QuranX.DomainClasses.Services.ObjectSpace>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QuranX.DomainClasses.Services.ObjectSpace context)
        {
        }
    }
}
