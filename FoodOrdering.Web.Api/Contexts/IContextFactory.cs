using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrdering.Web.Api.Contexts
{
	public interface IContextFactory
    {
        IContext Create();
    }
}
