import request from "@/utils/request"
import axios from "axios"

//获取列表
export function deptList(query){
  return request({
    url:"/api/Departments/Load",
    method:"get",
    params:query
  })
}

//添加部门
export function deptAdd(d) {
  return request({
    url:"/api/Departments/Add",
    method:"post",
    data:d
  })
}

//修改部门
export function deptEdit(d){
  return request({
    url:"/api/Departments/Update",
    method:"post",
    data:d
  })
}

//删除部门
export function deptDelete(params)
{
  return request({
    url:"/api/Departments/Delete",
    method:"get",
    params:params
  })
}

//通过部门设置角色
export function setDepartmentRole(d)
{
  return request({
    url:"/api/Departments/setDepartmentRole",
    method:"post",
    data:d
  })
}


