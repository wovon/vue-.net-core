import request from '@/utils/request'

export function getRouter(name) {
  return request({
    // url: '/api/Router/List',
    url: '/api/Menus/Load',
    method: 'get',
    params: { name }
  })
}
