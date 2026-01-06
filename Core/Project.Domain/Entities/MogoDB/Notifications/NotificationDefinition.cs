using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domain.Entities.MogoDB.Notifications
{
    [Table("NotificationDefinition")]
    public class NotificationDefinition
    {
        [BsonId]
        public Guid ConfigId { get; set; }

        public string ConfigCode { get; set; } = string.Empty;

        public string ConfigName { get; set; } = string.Empty;

        public object ConfigData { get; set; } = new();
    }
}
