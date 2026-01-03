import { mapRequests, thresholds, mapEndpoints } from '../common.js';

const endpoints = [
  { path: 'AuditLog/ContactAuditList', tag: 'AuditLog_ContactAuditList'  },
  { path: 'AuditLog/ContactChangeLog', tag: 'AuditLog_ContactChangeLog'  },
];
mapEndpoints(endpoints)

const service = "api/v1/apcrm-mst-auditlog-service"

export const options = {
  thresholds: thresholds,
  // vus: 1
  stages: [
    // { duration: '10s', target: 10 },
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