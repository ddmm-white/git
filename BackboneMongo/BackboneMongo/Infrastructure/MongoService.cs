using BackboneMongo.Models;
using MongoDB.Driver;

namespace BackboneMongo.Infrastructure
{
	public class MongoService
	{
		private const string ConnectionString = "mongodb://localhost";
		private const string DbName = "[dbname]";

		public MongoCollection GetCollection<T>( string collectionName ) 
			where T : MongoDbCollection
		{
			var client = new MongoClient( ConnectionString );
			var server = client.GetServer();
			var database = server.GetDatabase( DbName );
			var collection = database.GetCollection<T>( collectionName );
			return collection;
		}
	}
}