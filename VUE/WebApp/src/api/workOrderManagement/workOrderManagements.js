import request from "@/utils/request";
import axios from "axios";

export function WorkOrderManagementsList(query) {
  return request({
    url: "/api/WorkOrderManagement/WorkOrderManagements/Load",
    method: "get",
    params: query
  });
}
export function WorkOrderManagementsAdd(d) {
  return request({
    url: "/api/WorkOrderManagement/WorkOrderManagements/Add",
    method: "post",
    data: d
  });
}

export function WorkOrderManagementsDelete(params) {
  return request({
    url: "/api/WorkOrderManagement/WorkOrderManagements/Delete",
    method: "get",
    params: params
  });
}

export function WorkOrderManagementsEdit(d) {
  return request({
    url: "/api/WorkOrderManagement/WorkOrderManagements/Update",
    method: "post",
    data: d
  });
}
