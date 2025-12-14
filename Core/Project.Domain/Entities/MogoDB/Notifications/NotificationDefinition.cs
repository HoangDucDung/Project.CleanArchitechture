using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domain.Entities.MogoDB.Notifications
{
    [Table("NotificationDefinition")]
    public class NotificationDefinition
    {
        [BsonId]
        public Guid ConfigId { get; set; }

        public string ConfigCode { get; set; }

        public string ConfigName { get; set; }

        public object ConfigData { get; set; }
    }
}
