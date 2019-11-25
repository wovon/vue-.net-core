import router from "./router"; //permission在这  这里不都做好了
import store from "./store";
import { Message } from "element-ui";
import NProgress from "nprogress"; // progress bar
import "nprogress/nprogress.css"; // progress bar style
import { getToken } from "@/utils/auth"; // getToken from cookie

NProgress.configure({ showSpinner: false }); // NProgress Configuration

// permissiom judge function
function hasPermission(roles, permissionRoles) {
  //if (roles.indexOf('admin') >= 0) return true // admin permission passed directly
  if (!permissionRoles) return true;
  return roles.some(role => permissionRoles.indexOf(role) >= 0);
}

const whiteList = ["/login", "/authredirect"]; // 直接跳转  白名单

router.beforeEach((to, from, next) => {
  NProgress.start(); // 开始进度条
  if (getToken()) {
    // 确定是否有令牌
    /* has token*/
    if (to.path === "/login") {
      next({ path: "/" });
      NProgress.done(); // 如果当前页面是仪表盘，每个钩子都不会触发，所以手动处理它
    } else {
      //console.log(store.getters);
      if (store.getters.roles.length === 0) {
        // 判断当前用户是否已拉取完user_info信息
        //console.log("store",store);
        store
          .dispatch("GetUserInfo")
          .then(res => {
            // 拉取user_info
            //console.log('res.data', res.data)
            const roles = ["editor", "v", "admin"]; //res.data.result.roles // note: roles must be a array! such as: ['editor','develop']
            //console.log("roles",roles)
            store.dispatch("GenerateRoutes", { roles }).then(response => {
              // 根据roles权限生成可访问的路由表
              //console.log(store.getters.addRouters);
              //console.log("GenerateRoutes",store.getters.addRouters)
              router.addRoutes(store.getters.addRouters); // 动态添加可访问路由表
              //console.log("addRouters",store.getters.addRouters)
              //console.log('tag111', store.getters.addRouters)
              next({ ...to, replace: true }); // hack方法 确保addRoutes已完成 ,set the replace: true so the navigation will not leave a history record
              //next({ path: '/' })
            });
          })
          .catch(() => {
            store.dispatch("FedLogOut").then(() => {
              Message.error("验证失败，请重新登录！");
              next({ path: "/login" });
            });
          });
      } else {
        // 没有动态改变权限的需求可直接next() 删除下方权限判断 ↓
        //if (hasPermission(store.getters.roles, to.meta.roles)) {
        next(); //
        // } else {
        //   next({ path: '/401', replace: true, query: { noGoBack: true }})
        // }
        // 可删 ↑
      }
    }
  } else {
    /* has no token*/
    if (whiteList.indexOf(to.path) !== -1) {
      // 在免登录白名单，直接进入
      next();
    } else {
      next("/login"); // 否则全部重定向到登录页
      NProgress.done(); //如果当前页面是登录后不会触发每个钩子，所以手动处理它
    }
  }
});

router.afterEach(() => {
  NProgress.done(); // 完成进度条
});
