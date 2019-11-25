import request from "@/utils/request"
import axios from "axios"

export function departmentRoleList(query){
  return request({
    url:"/api/Departments/GetDepartmentRole",
    method:"get",
    params:query
  })
}

export function roleDepartmentList(query){
  return request({
    url:"/api/Departments/GetRoleDepartment",
    method:"get",
    params:query
  })
}

export function userDepartmentRoleList(query){
  return request({
    url:"/api/Departments/GetUserDepartmentRole",
    method:"get",
    params:query
  })
}
