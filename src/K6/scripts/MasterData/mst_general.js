import { mapRequests, thresholds, mapEndpoints } from '../common.js';

const endpoints = [
  { path: 'AgentEmployees/DropdownList', tag: 'AgentEmployees_DropDownList',  duration: 1000 },
  { path: 'AgentEmployees?page=1&pageSize=10', tag: 'AgentEmployees' },
  { path: 'Agents/DropdownList', tag: 'Agents_DropDownList',  duration: 1000 },
  { path: 'Agents?page=1&pageSize=10', tag: 'Agents' },
  
  { path: 'AttorneyTransfers/DropdownList', tag: 'AttorneyTransfers_DropDownList',  duration: 1000 },

  { path: 'BGs/DropdownList', tag: 'BGs_DropDownList',  duration: 1000 },
  { path: 'BGs?page=1&pageSize=10', tag: 'BGs',  duration: 1000  },
  { path: 'BOConfigurations?page=1&pageSize=10', tag: 'BOConfigurations',  duration: 1000  },
  
  { path: 'Brands/DropdownList', tag: 'Brands_DropDownList',  duration: 1000 },
  { path: 'Brands?page=1&pageSize=10', tag: 'Brands' }, 
  
  { path: 'CancelReasons/DropdownList', tag: 'CancelReasons_DropdownList',  duration: 1000 },
  { path: 'CancelReasons?page=1&pageSize=10', tag: 'CancelReasons' }, 
 
  { path: 'CancelReturnSettings?page=1&pageSize=10', tag: 'CancelReturnSettings' }, 
 
  { path: 'Countries/DropdownList', tag: 'Countries_DropdownList',  duration: 1000 },
  { path: 'Countries?page=1&pageSize=10', tag: 'Countries' }, 
 
  { path: 'Districts/DropdownList', tag: 'Districts_DropdownList',  duration: 1000 },
  { path: 'Districts?page=1&pageSize=10', tag: 'Districts' }, 

  { path: 'LandOffices/DropdownList', tag: 'LandOffices_DropdownList',  duration: 1000 },
  { path: 'LandOffices?page=1&pageSize=10', tag: 'LandOffices' }, 
  { path: 'MasterCenterGroups?page=1&pageSize=10', tag: 'MasterCenterGroups' }, 
  { path: 'MasterPriceItems/DropdownList', tag: 'MasterPriceItems_DropdownList',  duration: 1000 },
  { path: 'Servitude/GetServitudeList', tag: 'Servitude' }, 
  
  { path: 'SpecMaterial/GetSpecMaterialList?page=1&pageSize=10', tag: 'SpecMaterial_GetSpecMaterialList' },
  { path: 'SpecMaterial/GetAllSpecMaterialItems?page=1&pageSize=10', tag: 'SpecMaterial_GetAllSpecMaterialItems' },
  
  { path: 'SubBGs/DropdownList', tag: 'SubBGs_DropdownList',  duration: 1000 },
  { path: 'SubBGs?page=1&pageSize=10', tag: 'SubBGs' }, 

  { path: 'SubDistricts/DropdownList', tag: 'SubDistricts_DropdownList',  duration: 1000 },
  { path: 'SubDistricts?page=1&pageSize=10', tag: 'SubDistricts' }, 

  { path: 'TypeOfRealEstates/DropdownList', tag: 'TypeOfRealEstates_DropdownList',  duration: 1000 },
  { path: 'TypeOfRealEstates?page=1&pageSize=10', tag: 'TypeOfRealEstates' }, 
];
mapEndpoints(endpoints)

const service = "api/v1/apcrm-mst-general-service"

export const options = {
  thresholds: thresholds,
  // vus: 1
  stages: [
    // // { duration: '2s', target: 1 },
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