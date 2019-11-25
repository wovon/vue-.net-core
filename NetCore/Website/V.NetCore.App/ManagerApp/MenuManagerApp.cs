using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;
using V.NetCore.App.Request;
using V.NetCore.Infrastructure;
using V.NetCore.Repository.Domain;
using V.NetCore.Repository.Domain.Router;
using V.NetCore.Repository.Interface;

namespace V.NetCore.App.ManagerApp
{
    public class MenuManagerApp : BaseApp<Menu>
    {
        public MenuManagerApp(IUnitWork unitWork, IRepository<Menu> repository) : base(unitWork, repository)
        {

        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="menu"></param>
        public void Add(Menu menu)
        {
            menu.ParentId = string.IsNullOrEmpty(menu.ParentId) ? null : menu.ParentId;
            ChangeModuleCascade(menu);
            menu.CreateTime=DateTime.Now;
            menu.UpdateTime=DateTime.Now;
            UnitWork.Add(menu);
            UnitWork.Save();
        }

        /// <summary>
        /// 删除菜单 及 子集
        /// </summary>
        /// <param name="ids"></param>
        public void Delete(string ids)
        {
            var listMenuButtons = UnitWork.Find<MenuButton>(mb => ids.Contains(mb.MenuId)).ToList();
            //删除对应
            foreach (var menuButton in listMenuButtons)
            {
                UnitWork.Delete(menuButton);
            }

            //删除菜单 及 子集
            foreach (var menu in UnitWork.Find<Menu>(mb => ids.Contains(mb.Id)).ToList())
            {
                UnitWork.Delete<Menu>(m=>m.CascadeId.Contains(menu.CascadeId));
            }
            UnitWork.Save();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="menu"></param>
        public void Update(Menu menu)
        {
            menu.ParentId = string.IsNullOrEmpty(menu.ParentId) ? null : menu.ParentId;
            ChangeModuleCascade(menu);
            menu.UpdateTime = DateTime.Now;
            UnitWork.Update<Menu>(u=>u.Id==menu.Id, u=>new Menu()
            {
                Id = menu.Id,
                Name = menu.Name,
                ParentId=menu.ParentId,
                ParentName=menu.ParentName,
                CascadeId=menu.CascadeId,
                Code=menu.Code,
                LinkAddress=menu.LinkAddress,
                Icon=menu.Icon,
                Path = menu.Path,
                Title = menu.Title,
                Component = menu.Component,
                Redirect = menu.Redirect,
                hidden = menu.hidden,
                UpdateTime = menu.UpdateTime,
                Sort = menu.Sort
            });
            UnitWork.Save();
        }


        /// <summary>
        /// 设置菜单按钮
        /// </summary>
        /// <param name="menuButtons"></param>
        public void SetMenuButton(QueryMenuButton menuButtons)
        {
            UnitWork.Delete<MenuButton>(d => menuButtons.MenuButtons.Where(mb => mb.MenuId.Contains(d.MenuId)).Count() > 0);
            foreach (var mb in menuButtons.MenuButtons)
            {
                if (!string.IsNullOrEmpty(mb.ButtonId))
                {
                    UnitWork.Add<MenuButton>(new MenuButton()
                    {
                        MenuId = mb.MenuId,
                        ButtonId = mb.ButtonId
                    });
                }
            }
            UnitWork.Save();
        }

        /// <summary>
        /// 查询Menu 及 对应的按钮
        /// </summary>
        /// <returns></returns>
        public List<Menu> LoadMenuAndButton()
        {
            var listMenu = UnitWork.SqlQuery<Menu>(
                "select Id, Name,Name as Label,Name as Title,Path,Component,Redirect, ParentId, ParentName, CascadeId, Code,hidden, LinkAddress, Icon, Sort, CreateTime, UpdateTime, IsAble, IsDel from Menus " +
                "where isAble = 0 and isDel=0 order by Sort desc,CreateTime asc", new object[] { }).ToList();
            
            foreach (var m in listMenu)
            {
                var listDyButton = new List<dynamic>();
                var listButton = UnitWork.SqlQuery<Button>(
                    "select b.Id,b.Name,b.Name as Label,b.Name as Title,b.Code, b.Icon, b.Explain, b.CreateTime, b.UpdateTime, b.Sort from MenuButton bm left join Buttons b on bm.ButtonId = b.Id where MenuId='" + m.Id + "' order by b.Sort desc",
                    new object[]{}).ToList();
                
                listButton.ForEach((lb) =>
                {
                    dynamic dyButton = new Button() {Id=lb.Id,MenuId = m.Id, ParentId = m.Id,Name = lb.Name,
                        Code = lb.Code,Icon= lb.Icon,Label = lb.Label,Title= lb.Title,
                        Explain = lb.Explain,CreateTime = lb.CreateTime,UpdateTime = lb.UpdateTime,
                        Sort = lb.Sort,MenuButtons = lb.MenuButtons};
                    listDyButton.Add(dyButton);
                });
                m.children = listDyButton.Count > 0 ? listDyButton : null;
            }
            return BindNew(listMenu, null);
        }

        //public List<Menu> LoadMenuAuthority()
        //{
        //    //var listMenu = UnitWork.Find<Menu>(null).Where(w=>w.IsDel==false && w.IsAble == false).ToList();
        //    var listMenu = UnitWork.SqlQuery<Menu>(
        //        "select Id, Name,Name as Label,Name as Title,Path,Component,Redirect, ParentId, ParentName, CascadeId, Code,hidden, LinkAddress, Icon, Sort, CreateTime, UpdateTime, IsAble, IsDel from Menus " +
        //        "where isAble = 0 and isDel=0 order by Sort desc,CreateTime asc", new object[] { }).ToList();
        //    foreach (var m in listMenu)
        //    {
        //        var listButton = UnitWork.SqlQuery<Button>(
        //            "select b.Id,b.Name,b.Name as Label,b.Code, b.Icon, b.Explain, b.CreateTime, b.UpdateTime, b.Sort from MenuButton bm left join Buttons b on bm.ButtonId = b.Id where MenuId=@ButtonId",
        //            new object[]
        //            {
        //                new SqlParameter()
        //                {
        //                    ParameterName = "@ButtonId",
        //                    Value = m.Id,
        //                    DbType = DbType.String
        //                }
        //            }).ToList();
        //        //m.ButtonIds = string.Join(",", listButton.Select(s => s.Id).ToList());
        //        //m.ButtonNames = string.Join(",", listButton.Select(s => s.Name).ToList());
        //        m.children = listButton;
        //        m.meta = new Meta()
        //        {
        //            roles = new string[] { "admin", "editor" },
        //            title = m.Title,
        //            icon = m.Icon,
        //            noCache = false,
        //        };
        //    }
        //    return BindNew(listMenu, null);
        //}

        public List<Menu> Load(QueryMenuReq queryMenuReq)
        {
            //var listMenu = UnitWork.Find<Menu>(null).Where(w=>w.IsDel==false && w.IsAble == false).ToList();
            var listMenu = UnitWork.SqlQuery<Menu>(
                "select Id, Name,Name as Label,Name as Title,Path,Component,Redirect, ParentId, ParentName, CascadeId, Code,hidden, LinkAddress, Icon, Sort, CreateTime, UpdateTime, IsAble, IsDel from Menus " +
                "where isAble = 0 and isDel=0 and Component like @Component and Name like @Name order by Sort desc,CreateTime asc", 
                new object[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@Component",
                        Value = "%"+queryMenuReq.Component+"%",
                        DbType = DbType.String
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@Name",
                        Value = "%"+queryMenuReq.Name+"%",
                        DbType = DbType.String
                    }
                }).ToList();
            foreach (var m in listMenu)
            {
                var listButton = UnitWork.SqlQuery<Button>(
                    "select b.Id,b.Name,b.Name as Label,b.Name as Title,b.Code, b.Icon, b.Explain, b.CreateTime, b.UpdateTime, b.Sort from MenuButton bm left join Buttons b on bm.ButtonId = b.Id where MenuId=@ButtonId",
                    new object[]
                    {
                        new SqlParameter()
                        {
                            ParameterName = "@ButtonId",
                            Value = m.Id,
                            DbType = DbType.String
                        }
                    }).ToList();
                m.ButtonIds = string.Join(",",listButton.Select(s => s.Id).ToList());
                m.ButtonNames = string.Join(",", listButton.Select(s => s.Name).ToList());
                m.Buttons = listButton;
                m.meta = new Meta()
                {
                    roles = new [] { "admin", "editor","v" },
                    title = m.Title,
                    icon = m.Icon,
                    noCache = false,
                };
            }
            return BindNew(listMenu, null);
        }
        public List<Menu> LoadNoCascade(QueryMenuReq queryMenuReq)
        {
            //var listMenu = UnitWork.Find<Menu>(null).Where(w=>w.IsDel==false && w.IsAble == false).ToList();
            var listMenu = UnitWork.SqlQuery<Menu>(
                "select Id, Name,Name as Label,Name as Title,Path,Component,Redirect, ParentId, ParentName, CascadeId, Code,hidden, LinkAddress, Icon, Sort, CreateTime, UpdateTime, IsAble, IsDel from Menus " +
                "where isAble = 0 and isDel=0 and Component like @Component and Name like @Name order by Sort desc,CreateTime asc",
                new object[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@Component",
                        Value = "%"+queryMenuReq.Component+"%",
                        DbType = DbType.String
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@Name",
                        Value = "%"+queryMenuReq.Name+"%",
                        DbType = DbType.String
                    }
                }).ToList();
            return listMenu;
        }
    }
}
