import request from "@/utils/request"
import axios from "axios"
export function menuList(query){
  return request({
    url: '/api/Menus/load',
    method: 'get',
    params: query
  })
}

export function menuNoCascadeList(query){
  return request({
    url: '/api/Menus/LoadNoCascade',
    method: 'get',
    params: query
  })
}


export function menuAuthorityList(query){
  return request({
    url: '/api/Menus/LoadMenuAuthority',
    method: 'get',
    params: query
  })
}

export function menuAdd(d) {
  return request({
    url: '/api/Menus/Add',
    method: 'post',
    data:d
  })
}

export function menuDelete(params){
  return request({
    url:'/api/Menus/Delete',
    method:'get',
    params:params
  })
}

export function menuEdit(d) {
  return request({
    url: '/api/Menus/Update',
    method: 'post',
    data:d
  })
}

export function setMenuButton(d) {
  return request({
    url: '/api/Menus/SetMenuButton',
    method: 'post',
    data:d
  })
}

