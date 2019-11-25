import request from '@/utils/request'
import axios from 'axios'
export function fetchList(query) {
  // var aaa = request({
  //   url: 'http://localhost:5000/api/article/list',
  //   method: 'get',
  //   params: query
  // })
  // return aaa
  return axios.get('http://localhost:5000/api/VueService/list', {
    method: 'get',
    params: query
  })
}

export function fetchArticle() {
  return request({
    url: '/article/detail',
    method: 'get'
  })
}

export function fetchPv(pv) {
  return request({
    url: '/article/pv',
    method: 'get',
    params: { pv }
  })
}

export function createArticle(data) {
  return request({
    url: '/article/create',
    method: 'post',
    data
  })
}

export function updateArticle(data) {
  return request({
    url: '/article/update',
    method: 'post',
    data
  })
}
