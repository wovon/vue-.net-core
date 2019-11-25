import request from "@/utils/request"
import axios from "axios"

//获取选项列表
export function selectOptionList(query){
  return request({
    url:"/api/SelectOptions/Load",
    method:"get",
    params:query
  })
}

//添加选项
export function selectOptionAdd(d) {
  return request({
    url:"/api/SelectOptions/Add",
    method:"post",
    data:d
  })
}

//修改选项
export function selectOptionEdit(d){
  return request({
    url:"/api/SelectOptions/Update",
    method:"post",
    data:d
  })
}

//删除选项
export function selectOptionDelete(params)
{
  return request({
    url:"/api/SelectOptions/Delete",
    method:"get",
    params:params
  })
}



