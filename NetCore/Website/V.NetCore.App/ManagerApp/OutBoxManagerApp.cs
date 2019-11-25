using System;
using System.Collections.Generic;
using System.Linq;
using V.NetCore.App.Interface;
using V.NetCore.App.Request;
using V.NetCore.App.Response;
using V.NetCore.Repository.Domain;
using V.NetCore.Repository.Interface;
using V.NetCore.Tools;


namespace V.NetCore.App.ManagerApp
{
    public class OutBoxManagerApp : BaseApp<OutBox>
    {
        private readonly IAuth _auth;
        public OutBoxManagerApp(IUnitWork unitWork, IRepository<OutBox> repository, IAuth auth) : base(unitWork, repository)
        {
            _auth = auth;
        }
        public void Add(OutBoxView view)
        {
            OutBox outBox = view;
            outBox.username = view.username;
            outBox.Mbno = view.Mbno;
            outBox.Msg = view.Msg;
            outBox.SendTime = DateTime.Now;
            UnitWork.Add(outBox);
            UnitWork.Save();
        }
        public void Delete(string ids)
        {
            UnitWork.Delete<OutBox>(u => ids.Contains(u.Id));
            UnitWork.Save();
        }
        public void Update(OutBoxView view)
        {
            UnitWork.Update<OutBox>(u => u.Id == view.id, u => new OutBox
            {
                username = view.username,
                Mbno = view.Mbno,
                Msg = view.Msg,
                SendTime = DateTime.Now,
            });
            UnitWork.Save();
        }
    }
}
