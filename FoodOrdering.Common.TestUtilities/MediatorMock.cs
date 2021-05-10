using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;

namespace FoodOrdering.Common.TestUtilities
{
	public class MediatorMock : Mock<IMediator>
	{
		private readonly List<INotification> publishedNotifications = new();

		public IEnumerable<INotification> PublishedNotifications => publishedNotifications;

		public MediatorMock()
		{
			Setup(x => x.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()))
				.Callback((INotification n, CancellationToken _) => publishedNotifications.Add(n));
		}

		public void ClearPublishedNotifications()
		{
			publishedNotifications.Clear();
		}
	}
}
