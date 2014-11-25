using System.Linq;
using System.Web.Mvc;
using BackboneMongo.Infrastructure;
using BackboneMongo.Models;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace BackboneMongo.Controllers
{
	public class HomeController : Controller
	{
		public MongoService MongoService = new MongoService();

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult GetData( string name )
		{
			var collection = MongoService.GetCollection<Employee>( "Employee" );
			var employees = collection.AsQueryable<Employee>();

			if ( !string.IsNullOrEmpty( name ) )
			{
				employees = from e in employees
							where e.firstName.ToLower().Contains( name ) || e.lastName.ToLower().Contains( name )
							select e;
			}

			return Json( employees, JsonRequestBehavior.AllowGet );
		}

		[HttpPost]
		public ActionResult UpdateData( Employee employee )
		{
			var collection = MongoService.GetCollection<Employee>( "Employee" );

			var update = Update<Employee>.Set( x => x.firstName, employee.firstName )
				.Set( x => x.lastName, employee.lastName )
				.Set( x => x.age, employee.age )
				.Set( x => x.description, employee.description );

			collection.Update( Query.EQ( "_id", employee.Id ), update );

			return Json( "{ Ok: true }" );
		}

		[HttpPost]
		public ActionResult InsertData( Employee employee )
		{
			var collection = MongoService.GetCollection<Employee>( "Employee" );
			collection.Insert( employee );
			return Json( employee );
		}

		[HttpPost]
		public ActionResult DeleteData( string mongoId )
		{
			var collection = MongoService.GetCollection<Employee>( "Employee" );
			collection.Remove( Query.EQ( "_id", ObjectId.Parse( mongoId ) ) );
			return Json( "{ Ok: true }" );
		}
	}
}