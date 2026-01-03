import { mapRequests, thresholds, mapEndpoints } from '../common.js';

const endpoints = [
  { path: 'Menu/GetRoleDropdownList', tag: 'Menu_GetRoleDropdownList',  duration: 1000 },
  { path: 'Menu/GetModuleDropdownList', tag: 'Menu_GetModuleDropdownList',  duration: 1000 },
  { path: 'Menu/GetMenuDropdownList', tag: 'Menu_GetMenuDropdownList',  duration: 1000 },
  { path: 'Menu/GetMenuActionDropdownList', tag: 'Menu_GetMenuActionDropdownList',  duration: 1000 },
  
];
mapEndpoints(endpoints)

const service = "api/v1/apcrm-auth-rolepermission-service"

export const options = {
  thresholds: thresholds,
  // vus: 1
  stages: [
    // { duration: '2s', target: 1 },
    { duration: '30s', target: 50 }, // 30 วินาทีแรก ใช้ 100 VUs
    { duration: '30s', target: 100 }, // 30 วินาทีถัดไป เพิ่มขึ้นเป็น 200 VUs
    { duration: '30s', target: 150 }, // 30 วินาทีถัดไป เพิ่มขึ้นเป็น 300 VUs
    { duration: '30s', target: 200 }, // 30 วินาทีถัดไป เพิ่มขึ้นเป็น 400 VUs
    { duration: '30s', target: 50 }, // 30 วินาทีสุดท้าย ลดลงเหลือ 100 VUs
  ],
};
export default function () {
  mapRequests(service, endpoints);
}