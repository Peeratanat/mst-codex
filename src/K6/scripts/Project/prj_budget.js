import { mapRequests, thresholds, mapEndpoints } from '../common.js';

var projectId = "464DC028-4B61-4C1E-BF25-00399A859D9F"
const endpoints = [
  { path: 'BudgetMinPrices/GetBudgetMinPriceList', tag: 'BudgetMinPrices_GetBudgetMinPriceList',  duration: 1000 },
  { path: `BudgetPromotions/${projectId}/BudgetPromotions`, tag: 'BudgetPromotions_BudgetPromotions',  duration: 1000 },
];
mapEndpoints(endpoints)

const service = "api/v1/apcrm-prj-budget-service"

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