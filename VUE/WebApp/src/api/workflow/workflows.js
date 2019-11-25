import request from "@/utils/request"
import axios from "axios"

export function workflowList(query){
  return request({
    url: '/api/Workflow/load',
    method: 'get',
    params: query
  })
}

export function workflowAdd(data) {
  return request({
    url:"/api/Workflow/Add",
    method:"post",
    data:data
  })
}

export function workflowDelete(query) {
  return request({
    url:"/api/Workflow/Delete",
    method:"get",
    params:query
  })
}

export function workflowEdit(data) {
  return request({
    url:"/api/Workflow/Update",
    method:"post",
    data:data
  })
}

export function LoadByProjectIds(query){
  return request({
    url: '/api/Workflow/LoadByProjectIds',
    method: 'get',
    params: query
  })
}
