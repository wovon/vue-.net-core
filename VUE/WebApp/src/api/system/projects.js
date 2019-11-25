import request from "@/utils/request"

export function projectList(query){
return request({
    url:'/api/Projects/load',
    method: 'get',
    params: query
})
}
//project
//Projects
export function projectAdd(data) {
return request({
    url:"/api/Projects/Add",
    method:"post",
    data:data
})
}

export function projectEdit(data) {
return request({
    url:"/api/Projects/Update",
    method:"post",
    data:data
})
}

export function projectDelete(params){
return request({
    url:'/api/Projects/Delete',
    method:'get',
    params:params
})
}

export function setProjectWorkflow(d) {
    return request({
      url: '/api/Projects/SetProjectWorkflow',
      method: 'post',
      data:d
    })
  }