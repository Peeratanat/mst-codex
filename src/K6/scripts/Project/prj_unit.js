import { mapRequests, thresholds, mapEndpoints, projectId } from '../common.js';

const endpoints = [
  { path: `HighRiseFees/${projectId}/HighRiseFees`, tag: 'HighRiseFees' },
  { path: `LockFloors/${projectId}/LockFloor`, tag: 'LockFloors' },
  { path: `LockUnits/${projectId}/LockUnit`, tag: 'LockUnits' },
  { path: `LowRiseFees/${projectId}/LowRiseFees`, tag: 'LowRiseFees' },
  { path: `MinPrices/${projectId}/MinPrices`, tag: 'MinPrices' },
  { path: `Models/DropdownList`, tag: 'DropdownList' },
  { path: `Prices/${projectId}/PriceLists`, tag: 'Prices_PriceLists' },
  { path: `TitleDeeds/${projectId}/TitleDeeds`, tag: 'TitleDeeds' },
  { path: `UnitControlInterests/${projectId}/UnitControlInterest`, tag: 'UnitControlInterests' },

  { path: `Units/GetUnitPosition`, tag: 'Units_GetUnitPosition' },
  { path: `Units/GetUnitMasterPlanDetail`, tag: 'Units_GetUnitMasterPlanDetail' },
  { path: `Units/${projectId}/Units`, tag: 'Units' },
  { path: `Units/${projectId}/Units/DropdownList`, tag: 'Units_DropdownList' },
  { path: `Units/${projectId}/Units/QuotationDropdownList`, tag: 'Units_QuotationDropdownList' },
  { path: `Units/${projectId}/Units/DropdownListSellPrice`, tag: 'Units_DropdownListSellPrice' },

  { path: `WaiveCustomerSigns/${projectId}/WaiveCustomerSigns`, tag: 'WaiveCustomerSigns_WaiveCustomerSigns' },
  { path: `WaiveQCs/${projectId}/WaiveQCs`, tag: 'WaiveQCs_WaiveCustomerSigns' },

];
mapEndpoints(endpoints)

const service = "api/v1/apcrm-prj-unit-service"

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