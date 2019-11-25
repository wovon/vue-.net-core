import request from "@/utils/request"

export function userRoleAdd(d){
  return request({
    url:"/api/UserRole/Add",
    method:"post",
    data:d
  })
}
