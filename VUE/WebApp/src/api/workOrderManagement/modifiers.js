import request from "@/utils/request";
import axios from "axios";

export function modifiersList(query) {
  return request({
    url: "/api/WorkOrderManagement/Modifiers/load",
    method: "get",
    params: query
  });
}
export function modifiersAdd(d) {
  return request({
    url: "/api/WorkOrderManagement/Modifiers/Add",
    method: "post",
    data: d
  });
}

export function modifiersDelete(params) {
  return request({
    url: "/api/WorkOrderManagement/Modifiers/Delete",
    method: "get",
    params: params
  });
}

export function modifiersEdit(d) {
  return request({
    url: "/api/WorkOrderManagement/Modifiers/Update",
    method: "post",
    data: d
  });
}
