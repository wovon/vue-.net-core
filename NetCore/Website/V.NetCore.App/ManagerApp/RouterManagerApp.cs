using System;
using System.Collections.Generic;
using System.Text;
using V.NetCore.Repository.Domain.Router;
using V.NetCore.Repository.Interface;

namespace V.NetCore.App.ManagerApp
{
    public class RouterManagerApp : BaseApp<Router>
    {
        public RouterManagerApp(IUnitWork unitWork, IRepository<Router> repository) : base(unitWork, repository)
        {

        }

        

    }
}
