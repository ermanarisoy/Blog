using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
	public class IntegrationBaseEvent
	{
		public IntegrationBaseEvent()
		{
			Id = Guid.NewGuid().ToString();
			CreationDate = DateTime.UtcNow;
		}

		public IntegrationBaseEvent(string id, DateTime createDate)
		{
			Id = id;
			CreationDate = createDate;
		}

		public string Id { get; private set; }

		public DateTime CreationDate { get; private set; }
	}
}
