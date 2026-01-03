import { mapRequests, thresholds, mapEndpoints } from '../common.js';

const endpoints = [
  { path: 'BankAccounts/DropdownList', tag: 'BankAccounts_DropDownList',  duration: 1000 },
  { path: 'BankAccounts?page=1&pageSize=10', tag: 'BankAccounts' },
  { path: 'BankBranchBOTs?page=1&pageSize=10', tag: 'BankBranchBOTs' },
  { path: 'BankBranchs/DropdownList', tag: 'BankBranchs_DropDownList',  duration: 1000 },
  { path: 'BankBranchs?page=1&pageSize=10', tag: 'BankBranchs',  duration: 1000  },
  { path: 'Banks/DropdownList', tag: 'Banks_DropDownList',  duration: 1000 },
  { path: 'Banks?page=1&pageSize=10', tag: 'Banks' },
  { path: 'Banks/BankOnlyDropdownList', tag: 'Banks_BankOnlyDropdownList',  duration: 1000 },
  { path: 'EDCs/DropdownList', tag: 'EDCs_DropDownList',  duration: 1000 },
  { path: 'EDCs/EDCBankDropdownList', tag: 'EDCs_EDCBankDropdownList',  duration: 1000 },
  { path: 'EDCs?page=1&pageSize=10', tag: 'EDCs' },
  { path: 'EDCs/Banks?page=1&pageSize=10', tag: 'EDCs_Banks' },
  { path: 'EDCs/Fees', tag: 'EDCs_Fees' },
  { path: 'EDCs/EDCCreditCardPaymentTypeDropdownList', tag: 'EDCs_EDCCreditCardPaymentTypeDropdownList' },
];
mapEndpoints(endpoints)

const service = "api/v1/apcrm-mst-finacc-service"

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