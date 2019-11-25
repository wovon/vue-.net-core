
import request from "@/utils/request"


export function roleList (query){
  return request({
    url:"/api/Roles/Load",
    method:"get",
    params:query
  })
}

export function roleAdd (d){
  return request({
    url:"/api/Roles/Add",
    method:"post",
    data:d
  })
}

export function roleDelete (ids){
  return request({
    url:"/api/Roles/Delete",
    method:"get",
    params:ids
  })
}


export function roleEdit (d){
  return request({
    url:"/api/Roles/Update",
    method:"post",
    data:d
  })
}

//通过角色设置部门
export function setRoleDepartment(d)
{
  return request({
    url:"/api/Departments/setRoleDepartment",
    method:"post",
    data:d
  })
}

