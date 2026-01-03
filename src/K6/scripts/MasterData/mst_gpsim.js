import { mapRequests, thresholds, mapEndpoints, projectId } from '../common.js';

const endpoints = [
  { path: 'GPSimulate/GPSimulateList', tag: 'GPSimulate_GPSimulateList' },
  { path: `GPSimulate/GetNewGPFromProjectID/${projectId}`, tag: 'GPSimulate_GetNewGPFromProjectID' },

  { path: 'ReallocateMinPrice/GetReallocateMinPriceList', tag: 'ReallocateMinPrice_GetReallocateMinPriceList' },
  { path: `ReallocateMinPrice/GetNewReallocateMinPriceFromProjectID/${projectId}`, tag: 'ReallocateMinPrice_GetNewReallocateMinPriceFromProjectID' },
];
mapEndpoints(endpoints)

const service = "api/v1/apcrm-mst-gpsim-service"

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