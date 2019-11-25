import {
  constantRouterMap
} from '@/router'
import {
  getRouter
} from '@/api/router'
const _import = require('@/router/_import_' + process.env.NODE_ENV)
import Layout from '@/views/layout/Layout'

/**
 * 通过meta.role判断是否与当前用户权限匹配
 * @param roles
 * @param route
 */
function hasPermission(roles, route) {
  //console.log("meta",route.meta);
  if (route.meta && route.meta.roles) {
    // console.log("roles",roles)
    // console.log('route',route)
    return roles.some(role => {
      //console.log("path"+route.path);
      return route.meta.roles.indexOf(role) >= 0;
    })
  } else {
    return true
  }
}

/**
 * 递归过滤异步路由表，返回符合用户角色权限的路由表
 * @param asyncRouterMap
 * @param roles
 */
function filterAsyncRouter(asyncRouterMap, roles) {
  const accessedRouters = asyncRouterMap.filter(route => {
    if (hasPermission(roles, route)) {
      if (route.children && route.children.length) {
        route.children = filterAsyncRouter(route.children, roles)
      }
      return true
    }
    return false
  })
  return accessedRouters
}

//异步路由
//返回路由中的component
function GetComponent(name) {
  if (name === "Layout") {
    return Layout
  } else if (!name || name === "") {
    return null
  } else {
    return Layout
  }
}
//异步获取路由处理
async function getAsyncRouterArr() {
  let asyncRouterMap = [];
  let resp = await getRouter();
  //路由中component (此处有BUG 不是无限子类。待改为递归)
  resp.data.data.forEach(_data => {
    let _router = '[' + _data + ']'
    if (_data.children) {
      if (_data.children.length === 0) {
        delete _data.children
      } else {
        _data.children.forEach(c => {
          c.component = eval(c.component)
          if (c.children) {
            c.children.forEach(cc => {
              cc.component = eval(cc.component)
            })
          }
        })
      }
    }
    _data.component = eval("GetComponent('" + _router + "')");
    asyncRouterMap.push(_data)
    //console.log('tag', _data)
  });

  return asyncRouterMap
}

const permission = { //permission这块 我记得这个项目里写过了 是这个文件里面？》
  state: {
    routers: constantRouterMap,
    addRouters: []
  },
  mutations: {
    SET_ROUTERS: (state, routers) => {
      state.addRouters = routers
      state.routers = constantRouterMap.concat(routers)
    }
  },
  actions: {
    GenerateRoutes({
      commit
    }, data) {
      return new Promise(async resolve => {
        const {
          roles
        } = data
        //console.log(JSON.stringify(roles));
        let accessedRouters
        // if (roles.indexOf('admin') >= 0) {
        //   accessedRouters = asyncRouterMap
        // } else {
        let asyncRouterMap = await getAsyncRouterArr();
        accessedRouters = filterAsyncRouter(asyncRouterMap, roles)
        //console.log(accessedRouters);
        //}
        commit('SET_ROUTERS', accessedRouters)
        resolve()
      })
    }
  }
}

export default permission
