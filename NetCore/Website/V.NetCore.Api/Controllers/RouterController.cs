using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V.NetCore.App.Response;
using V.NetCore.Repository.Domain.Router;

namespace V.NetCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RouterController : ControllerBase
    {
        public TableData List()
        {
            var result = new TableData();

            var Routers = new List<Router>();
            //首页
            //Routers.Add(new Router()
            //{
            //    path = "",
            //    component = "Layout",
            //    redirect = "dashboard",
            //    noCache = true,
            //    children = new List<Router>()
            //    {
            //        new Router()
            //        {
            //            path = "dashboard",
            //            component = "_import('dashboard/index')",
            //            name = "dashboard",
            //            meta = new Meta()
            //            {
            //                roles = new string[] {"admin", "editor"},
            //                title = "dashboard",
            //                icon = "dashboard",
            //                noCache = true,
            //            }
            //        }
            //    }
            //});
            //
            //Routers.Add(new Router()
            //{
            //    path = "/documentation",
            //    component = "Layout",
            //    redirect = "/documentation/index",
            //    noCache = true,
            //    children = new List<Router>()
            //    {
            //        new Router()
            //        {
            //            path = "index",
            //            component = "_import('documentation/index')",
            //            name = "文档",
            //            meta = new Meta()
            //            {
            //                roles = new string[] {"admin", "editor"},
            //                title = "文档",
            //                icon = "documentation",
            //                noCache = true,
            //            }
            //        }
            //    }
            //});
            //
            Routers.Add(new Router()
            {
                path = "/permission",
                component = "Layout",
                redirect = "/permission/index",
                noCache = true,
                meta = new Meta()
                {
                    roles = new string[] { "admin", "editor","v" }
                },
                children = new List<Router>()
                {
                    new Router()
                    {
                        path = "index",
                        component = "_import('permission/index')",
                        name = "权限测试页",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "权限测试页",
                            icon = "lock",
                            noCache = true,
                        }
                    }
                }
            });
            //
            Routers.Add(new Router()
            {
                path = "/icon",
                component = "Layout",
                noCache = true,
                children = new List<Router>()
                {
                    new Router()
                    {
                        path = "index",
                        component = "_import('svg-icons/index')",
                        name = "图标",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "图标",
                            icon = "icon",
                            noCache = true,
                        }
                    }
                }
            });
            //
            Routers.Add(new Router()
            {
                path = "/components",
                component = "Layout",
                redirect = "noredirect",
                name= "component-demo",
                noCache = true,
                meta = new Meta()
                {
                    title= "组件",
                    icon="component",
                    roles = new string[] { "admin", "editor", "v" }
                },
                children = new List<Router>()
                {
                    new Router()
                    {
                        path = "tinymce",
                        component = "_import('components-demo/tinymce')",
                        name = "编辑器",
                        meta = new Meta()
                        {roles = new string[] {"admin", "editor","v"},title = "编辑器",icon = "lock",noCache = true}
                    },
                    new Router()
                    {
                        path = "markdown",
                        component = "_import('components-demo/markdown')",
                        name = "markdown",
                        meta = new Meta()
                            {roles = new string[] {"admin", "editor","v"},title = "markdown",icon = "lock",noCache = true}
                    },
                    new Router()
                    {
                        path = "json-editor",
                        component = "_import('components-demo/jsonEditor')",
                        name = "json-editor",
                        meta = new Meta()
                            {roles = new string[] {"admin", "editor","v"},title = "JSON编辑器",icon = "lock",noCache = true}
                    },
                    new Router()
                    {
                        path = "dnd-list",
                        component = "_import('components-demo/dndList')",
                        name = "dnd-list",
                        meta = new Meta()
                            {roles = new string[] {"admin", "editor","v"},title = "列表拖拽",icon = "lock",noCache = true}
                    },
                    new Router()
                    {
                        path = "splitpane",
                        component = "_import('components-demo/splitpane')",
                        name = "splitpane",
                        meta = new Meta()
                            {roles = new string[] {"admin", "editor","v"},title = "splitpane",icon = "lock",noCache = true}
                    },
                    new Router()
                    {
                        path = "avatar-upload",
                        component = "_import('components-demo/avatarUpload')",
                        name = "avatar-upload",
                        meta = new Meta()
                            {roles = new string[] {"admin", "editor","v"},title = "头像上传",icon = "lock",noCache = true}
                    },
                    new Router()
                    {
                        path = "dropzone",
                        component = "_import('components-demo/dropzone')",
                        name = "dropzone",
                        meta = new Meta()
                            {roles = new string[] {"admin", "editor","v"},title = "dropzone",icon = "lock",noCache = true}
                    },
                    new Router()
                    {
                        path = "sticky",
                        component = "_import('components-demo/sticky')",
                        name = "sticky",
                        meta = new Meta()
                            {roles = new string[] {"admin", "editor","v"},title = "sticky",icon = "lock",noCache = true}
                    },
                    new Router()
                    {
                        path = "count-to",
                        component = "_import('components-demo/countTo')",
                        name = "count-to",
                        meta = new Meta()
                            {roles = new string[] {"admin", "editor","v"},title = "countTo",icon = "lock",noCache = true}
                    },
                    new Router()
                    {
                        path = "mixin",
                        component = "_import('components-demo/mixin')",
                        name = "mixin",
                        meta = new Meta()
                            {roles = new string[] {"admin", "editor","v"},title = "mixin",icon = "lock",noCache = true}
                    },
                    new Router()
                    {
                        path = "back-to-top",
                        component = "_import('components-demo/backToTop')",
                        name = "back-to-top",
                        meta = new Meta()
                            {roles = new string[] {"admin", "editor","v"},title = "返回顶部",icon = "lock",noCache = true}
                    },
                }
            });
            //
            Routers.Add(new Router()
            {
                path = "/charts",
                component = "Layout",
                //redirect = "noredirect",
                name = "图表",
                noCache = true,
                meta = new Meta()
                {
                    title = "图表",
                    icon = "chart",
                    roles = new string[] { "admin", "editor", "v" }
                },
                children = new List<Router>()
                {
                    new Router()
                    {
                        path = "keyboard",
                        component = "_import('charts/keyboard')",
                        name = "keyboardChart",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "键盘图表",
                            icon = "lock",
                            noCache = true
                        }
                    },
                    new Router()
                    {
                        path = "line",
                        component = "_import('charts/line')",
                        name = "line",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "line",
                            icon = "lock",
                            noCache = true
                        }
                    }
                }
            });
            //
            Routers.Add(new Router()
            {
                path = "/example",
                component = "Layout",
                redirect = "/example/table/complex-table",
                name = "综合实例",
                meta = new Meta()
                {
                    title = "综合实例",
                    icon = "example",
                    roles = new string[] { "admin", "editor", "v" }
                },
                children = new List<Router>()
                {
                    new Router()
                    {
                        path = "/example/table",
                        component = "_import('example/table/index')",
                        redirect="/example/table/complex-table",
                        name = "Table",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "Table",
                            icon = "table",
                            //noCache = true
                        },
                        children = new List<Router>()
                        {
                            new Router()
                            {
                                path = "dynamic-table",
                                component = "_import('example/table/dynamicTable/index')",
                                //redirect="/example/table/complex-table",
                                name = "动态Table",
                                meta = new Meta()
                                {
                                    roles = new string[] {"admin", "editor","v"},
                                    title = "动态Table",
                                    icon = "table",
                                    //noCache = true
                                }
                            },
                            new Router()
                            {
                                path = "drag-table",
                                component = "_import('example/table/dragTable')",
                                //redirect="/example/table/complex-table",
                                name = "拖拽Table",
                                meta = new Meta()
                                {
                                    roles = new string[] {"admin", "editor","v"},
                                    title = "拖拽Table",
                                    icon = "table",
                                    //noCache = true
                                }
                            },
                            new Router()
                            {
                                path = "inline-edit-table",
                                component = "_import('example/table/inlineEditTable')",
                                //redirect="/example/table/complex-table",
                                name = "Table内编辑",
                                meta = new Meta()
                                {
                                    roles = new string[] {"admin", "editor","v"},
                                    title = "Table内编辑",
                                    icon = "table",
                                    //noCache = true
                                }
                            },
                            new Router()
                            {
                                path = "tree-table",
                                component = "_import('example/table/treeTable/treeTable')",
                                //redirect="/example/table/complex-table",
                                name = "树形表格",
                                meta = new Meta()
                                {
                                    roles = new string[] {"admin", "editor","v"},
                                    title = "树形表格",
                                    icon = "table",
                                    //noCache = true
                                }
                            },
                            new Router()
                            {
                                path = "custom-tree-table",
                                component = "_import('example/table/treeTable/customTreeTable')",
                                //redirect="/example/table/complex-table",
                                name = "自定义树表",
                                meta = new Meta()
                                {
                                    roles = new string[] {"admin", "editor","v"},
                                    title = "自定义树表",
                                    icon = "table",
                                    //noCache = true
                                }
                            },
                            new Router()
                            {
                                path = "complex-table",
                                component = "_import('example/table/complexTable')",
                                //redirect="/example/table/complex-table",
                                name = "综合Table",
                                meta = new Meta()
                                {
                                    roles = new string[] {"admin", "editor","v"},
                                    title = "综合Table",
                                    icon = "table",
                                    //noCache = true
                                }
                            }
                        }
                    },
                    new Router()
                    {
                        path = "tab/index",
                        component = "_import('example/tab/index')",
                        //redirect="",
                        name = "tab",
                        icon = "tab",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "tab",
                            icon = "tab",
                            //noCache = true
                        }
                    }
                }
            });
            //
            Routers.Add(new Router()
            {
                path = "/form",
                component = "Layout",
                redirect = "noredirect",
                name = "表单",
                meta = new Meta()
                {
                    title = "表单",
                    icon = "form",
                    roles = new string[] { "admin", "editor", "v" }
                },
                children = new List<Router>()
                {
                    new Router()
                    {
                        path = "create-form",
                        component = "_import('form/create')",
                        name = "创建表单",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "创建表单",
                            icon = "table",
                            //noCache = true
                        }
                    },
                    new Router()
                    {
                        path = "edit-form",
                        component = "_import('form/edit')",
                        name = "编辑表单",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "编辑表单",
                            icon = "table",
                            //noCache = true
                        }
                    }
                }
            });
            //error
            Routers.Add(new Router()
            {
                path = "/error",
                component = "Layout",
                //redirect = "/permission/index",
                name= "错误页面",
                noCache = true,
                meta = new Meta()
                {
                    title= "错误页面",
                    icon = "404",
                    roles = new string[] { "admin", "editor", "v" }
                },
                children = new List<Router>()
                {
                    new Router()
                    {
                        path = "401",
                        component = "_import('errorPage/401')",
                        name = "401",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "401",
                            noCache = true,
                        }
                    },
                    new Router()
                    {
                        path = "404",
                        component = "_import('errorPage/404')",
                        name = "404",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "404",
                            noCache = true,
                        }
                    }
                }
            });
            //error log
            Routers.Add(new Router()
            {
                path = "/error-log",
                component = "Layout",
                //redirect = "/permission/index",
                noCache = true,
                meta = new Meta()
                {
                    title = "错误日志",
                    roles = new string[] { "admin", "editor", "v" }
                },
                children = new List<Router>()
                {
                    new Router()
                    {
                        path = "log",
                        component = "_import('errorLog/index')",
                        name = "错误日志",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "errorLog",
                            noCache = true,
                        }
                    }
                }
            });
            //excel
            Routers.Add(new Router()
            {
                path = "/excel",
                component = "Layout",
                //redirect = "/permission/index",
                noCache = true,
                meta = new Meta()
                {
                    title = "excel",
                    roles = new string[] { "admin", "editor", "v" }
                },
                children = new List<Router>()
                {
                    new Router()
                    {
                        path = "export-excel",
                        component = "_import('excel/exportExcel')",
                        name = "export-excel",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "export-excel",
                            noCache = true,
                        }
                    },
                    new Router()
                    {
                        path = "export-selected-excel",
                        component = "_import('excel/selectExcel')",
                        name = "export-selected-excel",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "export-selected-excel",
                            noCache = true,
                        }
                    },
                    new Router()
                    {
                        path = "upload-excel",
                        component = "_import('excel/uploadExcel')",
                        name = "upload-excel",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "upload-excel",
                            noCache = true,
                        }
                    }
                }
            });
            
            //clipboard
            Routers.Add(new Router()
            {
                path = "/clipboard",
                component = "Layout",
                //redirect = "/permission/index",
                noCache = true,
                meta = new Meta()
                {
                    title = "clipboard",
                    roles = new string[] { "admin", "editor", "v" }
                },
                children = new List<Router>()
                {
                    new Router()
                    {
                        path = "index",
                        component = "_import('clipboard/index')",
                        name = "clipboard",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "clipboard",
                            noCache = true,
                        }
                    }
                }
            });
            //系统设置
            Routers.Add(new Router()
            {
                path = "/system",
                component = "Layout",
                noCache = true,
                //name="系统设置",
                meta = new Meta()
                {
                    roles = new string[] { "admin", "editor", "v" },
                    title = "系统设置",
                    icon = "icon",
                    noCache = true,
                },
                children = new List<Router>()
                {
                    new Router()
                    {
                        path = "users",
                        component = "_import('system/users')",
                        name = "用户设置",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "用户设置",
                            icon = "icon",
                            noCache = true,
                        }
                    },
                    new Router()
                    {
                        path = "departments",
                        component = "_import('system/departments')",
                        name = "部门设置",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "部门设置",
                            icon = "icon",
                            noCache = true,
                        }
                    },
                    new Router()
                    {
                        path = "roles",
                        component = "_import('system/roles')",
                        name = "角色设置",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "角色设置",
                            icon = "icon",
                            noCache = true,
                        }
                    },
                    new Router()
                    {
                        path = "menus",
                        component = "_import('system/menus')",
                        name = "菜单设置",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "菜单设置",
                            icon = "icon",
                            noCache = true,
                        }
                    },
                    new Router()
                    {
                        path = "vtest",
                        component = "_import('system/vtest')",
                        name = "测试",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "测试",
                            icon = "icon",
                            noCache = true,
                        }
                    }
                }
            });
            //切换语言环境
            Routers.Add(new Router()
            {
                path = "/i18n",
                component = "Layout",
                noCache = true,
                children = new List<Router>()
                {
                    new Router()
                    {
                        path = "index",
                        component = "_import('i18n-demo/index')",
                        name = "i18n",
                        meta = new Meta()
                        {
                            roles = new string[] {"admin", "editor","v"},
                            title = "i18n",
                            icon = "international",
                            noCache = true
                        }
                    }
                }
            });
            //错误页面
            Routers.Add(new Router()
            {
                path = "*",
                redirect = "/404",
                hidden = true
            });
            result.data = Routers;
            return result;
        }
    }
}