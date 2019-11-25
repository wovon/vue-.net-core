import request from "@/utils/request"
import axios from "axios"

export function historyList(query)
{
    return request({
        url:'/api/History/Load',
        method:'get',
        params:query
    })

}
export function historyAdd(data)
{

    return request({
        url:"/api/History/Add",
        method:"post",
        data:data
    })
}

export function histroyEdit(data)
{

    return request({
        url:"/api/History/Update",
        method:"post",
        data:data 
    })
}

export function historyDelete(params){
    return request({
        url:'/api/History/Delete',
        method:'get',
        params:params
    })
}