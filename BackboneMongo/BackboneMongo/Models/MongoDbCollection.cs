using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackboneMongo.Models
{
	public class MongoDbCollection
	{
		[BsonId]
		public ObjectId Id { get; set; }

		[DataMember]
		public string MongoId
		{
			get
			{
				return Id.ToString();
			}
			set
			{
				Id = ObjectId.Parse( value );
			}
		}
	}
}