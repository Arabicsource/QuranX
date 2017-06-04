using QuranX.DomainClasses.Model;
using System.Data.Entity.Migrations;
using QuranX.DomainClasses.ServicesImpl;
using System;
using System.Xml.Linq;
using System.IO;
using System.Reflection;

namespace QuranX.DomainClasses.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<QuranX.DomainClasses.ServicesImpl.ObjectSpace>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QuranX.DomainClasses.ServicesImpl.ObjectSpace context)
        {
        }
    }
}
