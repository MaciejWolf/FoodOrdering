using System.Linq;
using System.Threading.Tasks;

namespace FoodOrdering.Web.Api.Contexts
{
	public interface IContext
	{
		string RequestId { get; }
		string TraceId { get; }
		IIdentityContext Identity { get; }
	}
}
