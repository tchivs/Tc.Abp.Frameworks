using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Tc.Abp.AspNetCore.Store;
/*
 * App.razor
 <Fluxor.Blazor.Web.StoreInitializer/>
 */
public interface IAbpState<TState> : IState<TState>, IScopedDependency
{

}
public interface IAbpStateSelection<TState, TValue> : IStateSelection<TState, TValue>, ITransientDependency
{

}
