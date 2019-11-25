using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using V.NetCore.App.ModelViews;
using V.NetCore.App.Request;
using V.NetCore.App.Response;
using V.NetCore.Repository.Domain;
using V.NetCore.Repository.Interface;
namespace V.NetCore.App.ManagerApp
{
    public class HistoryManagerApp : BaseApp<History>
    {
        public HistoryManagerApp(IUnitWork unitWork, IRepository<History> repository) : base(unitWork, repository)
        {
        }
        //添加
        public void Add(History history)
        {
            UnitWork.Add(history);
            UnitWork.Save();

        }
        //修改
        public void Edit(History history)
        {
            UnitWork.Update(history);
            UnitWork.Save();
        }
        public void Update(History history)
        {
            UnitWork.Update<History>(b => b.GU_ID == history.GU_ID, b => new History
            {
                UserName = history.UserName,
                Remake=history.Remake
               
            });
            UnitWork.Save();
        }



        public void Delete(string id)
        {
           
            UnitWork.Delete<History>(b => id.Contains(b.Id));
            UnitWork.Save();
        }

        //列表查看
        public List<HistoryView> Load()
        {
            var listHistory = UnitWork.SqlQuery<History>(
               "select Id, UserName,Remake,GU_ID,Updatetime,UserID,Wom_ID From History", new object[] { }).ToList();

            var listHistoryView = new List<HistoryView>();
            foreach (var v in listHistory)
            {
                HistoryView hv = v;
                listHistoryView.Add(hv);
            }
            return listHistoryView;

        }
        //public List<ButtonView> Load()
        //{
        //    //var result = from d in UnitWork.Find<Button>(null) select d;
        //    var listButton = UnitWork.SqlQuery<Button>(
        //        "select Id, Name,Label,Title, Code, Icon, Explain, CreateTime, UpdateTime, Sort From Buttons", new object[] { }).ToList();

        //    var listButtonView = new List<ButtonView>();
        //    foreach (var b in listButton)
        //    {
        //        ButtonView bv = b;
        //        bv.Label = b.Name;
        //        listButtonView.Add(bv);
        //    }
        //    return listButtonView;
        //}





    }
}
