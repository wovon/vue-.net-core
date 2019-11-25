using System.Collections.Generic;
using V.NetCore.Repository.Domain;

namespace V.NetCore.App.Interface
{
    public interface IAuthStrategy
    {
        List<Department> Departments { get; }

        User User
        {
            get; set;
        }

    }
}
