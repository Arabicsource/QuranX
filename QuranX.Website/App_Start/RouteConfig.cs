using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuranX.Website
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				 name: "",
				 url: "",
				 defaults: new { Controller = "Chapter", Action = "Index" });

			//routes.MapRoute(
			//        name: "",
			//        url: "{Chapter}.{FirstVerse}-{LastVerse}",
			//        defaults: new 
			//        {
			//            Controller = "VerseBrowse",
			//            Action = "Index"
			//        },
			//        constraints: new
			//        {
			//            Chapter = @"\d+",
			//            FirstVerse = @"\d+",
			//            LastVerse = @"\d+"
			//        }
			//    );

			routes.MapRoute(
					  name: "",
					  url: "{Chapter}.{Verse}",
					  defaults: new
					  {
						  Controller = "VerseBrowse",
						  Action = "Index",
					  },
					  constraints: new
					  {
						  Chapter = @"\d+",

						  Verse = @"\d+"
					  }
				 );

			routes.MapRoute(null, "{controller}/{action}");

		}
	}
}
