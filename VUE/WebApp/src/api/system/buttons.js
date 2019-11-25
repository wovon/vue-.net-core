import request from "@/utils/request"
import axios from "axios"

export function buttonList(query){
  return request({
    url: '/api/Buttons/load',
    method: 'get',
    params: query
  })
}
export function buttonAdd(data) {
  return request({
    url:"/api/Buttons/Add",
    method:"post",
    data:data
  })
}

export function buttonEdit(data) {
  return request({
    url:"/api/Buttons/Update",
    method:"post",
    data:data
  })
}
export function buttonDelete(params){
  return request({
    url:'/api/Buttons/Delete',
    method:'get',
    params:params
  })
}
// export function buttonDelete(data) {
//   var param={ids:data}
//   console.log("datatatatat" + data);
//   return axios.delete("http://localhost:5000/api/Buttons/Delete",{
//     params:param
//   })
// }
export function buttonListByMenuIds(query){
  return request({
    url: '/api/Buttons/LoadByMenuIds',
    method: 'get',
    params: query
  })
}

export function buttonListByMenuName(query){
  return request({
    url: '/api/Buttons/LoadByMenuName',
    method: 'get',
    params: query
  })
}
