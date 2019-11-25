import request from "@/utils/request"

export function userDepartmentAdd(d){
  return request({
    url:"/api/UserDepartment/Add",
    method:"post",
    data:d
  })
}
