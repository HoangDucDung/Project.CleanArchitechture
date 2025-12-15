using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Contract.MessageBroker
{
    public class OptionKafka
    {
        public string ConnectionKafka { get; set; } = string.Empty;

        public string GroupId { get; set; } = string.Empty;

        public string Topic { get; set; } = string.Empty;
    }
}
