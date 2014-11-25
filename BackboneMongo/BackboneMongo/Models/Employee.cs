namespace BackboneMongo.Models
{
	public class Employee : MongoDbCollection
	{
		public int age { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }

		public string description { get; set; }
	}
}