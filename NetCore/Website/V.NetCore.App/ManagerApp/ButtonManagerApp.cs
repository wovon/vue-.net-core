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
    public class ButtonManagerApp:BaseApp<Button>
    {
        public ButtonManagerApp(IUnitWork unitWork, IRepository<Button> repository) : base(unitWork, repository)
        {
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="button"></param>
        public void Add(Button button) {
            button.CreateTime = DateTime.Now;
            button.UpdateTime = DateTime.Now;
            UnitWork.Add(button);
            UnitWork.Save();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            UnitWork.Delete<MenuButton>(mb => id.Contains(mb.ButtonId));
            UnitWork.Delete<Button>(b => id.Contains(b.Id));
            UnitWork.Save();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="button"></param>
        public void Update(Button button)
        {
            UnitWork.Update<Button>(b => b.Id == button.Id, b => new Button
            {
                Name = button.Name,
                Code = button.Code,
                Icon = button.Icon,
                Sort = button.Sort,
                Explain = button.Explain
            });
            UnitWork.Save();
        }


        /// <summary>
        /// 加载列表
        /// </summary>
        /// <returns></returns>
        public List<ButtonView> Load()
        {
            //var result = from d in UnitWork.Find<Button>(null) select d;
            var listButton = UnitWork.SqlQuery<Button>(
                "select Id, Name,Label,Title, Code, Icon, Explain, CreateTime, UpdateTime, Sort From Buttons", new object[] { }).ToList();
            
            var listButtonView = new List<ButtonView>();
            foreach (var b in listButton)
            {
                ButtonView bv = b;
                bv.Label = b.Name;
                listButtonView.Add(bv);
            }
            return listButtonView;
        }

        /// <summary>
        /// 根据菜单ID 加载列表
        /// </summary>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public List<ButtonView> LoadByMenuIds(string menuIds)
        {
            //var result = from d in UnitWork.Find<Button>(null) select d;
            var listButton = UnitWork.SqlQuery<Button>(
                "select b.* From Menus m left join MenuButton mb on m.id = mb.MenuId left join Buttons b on mb.ButtonId  = b.Id where b.id is not null and m.id in ('"
                + menuIds.Trim(',').Replace(",", "','") + "') order by b.Sort desc,id desc", new object[] { }).ToList();

            var listButtonView = new List<ButtonView>();
            foreach (var b in listButton)
            {
                ButtonView bv = b;
                bv.Label = b.Name;
                listButtonView.Add(bv);
            }
            return listButtonView;
        }

        /// <summary>
        /// 通过菜单的 名称 和 组件 获取按钮
        /// </summary>
        /// <param name="name">菜单名称</param>
        /// <param name="component">组件</param>
        /// <returns></returns>
        public List<ButtonView> LoadByMenuName(string name, string component)
        {
            //var result = from d in UnitWork.Find<Button>(null) select d;
            var listButton = UnitWork.SqlQuery<Button>(
                "select b.* From Menus m left join MenuButton mb on m.id = mb.MenuId left join Buttons b on mb.ButtonId  = b.Id where b.id is not null " +
                "and m.id in (select Id from Menus where isAble = 0 and isDel=0 and Component like @Component and Name like @Name) " +
                "order by b.Sort desc,id desc"
                , new object[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@Name",
                        Value = "%" + name + "%",
                        DbType = DbType.String
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@Component",
                        Value = "%" + component + "%",
                        DbType = DbType.String
                    }
                }).ToList();

            var listButtonView = new List<ButtonView>();
            foreach (var b in listButton)
            {
                ButtonView bv = b;
                bv.Label = b.Name;
                listButtonView.Add(bv);
            }

            return listButtonView;
        }

    }
}
