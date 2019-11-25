import request from "@/utils/request"
import axios from "axios"
export function userList(query){
  return request({
    url: '/api/Users/load',
    method: 'get',
    params: query
  })
}
export function userAdd(d) {
  return request({
    url: '/api/Users/Add',
    method: 'post',
    data:d
  })
}

export function userDelete(params){
  return request({
    url:'/api/Users/Delete',
    method:'get',
    params:params
  })
}

export function userEdit(d) {
  return request({
    url: '/api/Users/Edit',
    method: 'post',
    data:d
  })
}

