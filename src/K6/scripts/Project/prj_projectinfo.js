import { mapRequests, thresholds, mapEndpoints } from '../common.js';

const endpoints = [
  { path: `Project/GetProjectInformationList`, tag: 'Project_GetProjectInformationList'  },
  { path: `Project/GetProjectInformationList_HealthCheck`, tag: 'Project_GetProjectInformationList_HealthCheck'  },
  { path: `Project/GetProjectInformationDetail`, tag: 'Project_GetProjectInformationDetail'  },
  { path: `Project/DropdownList/ProjectBrand`, tag: 'Project_DropdownList_ProjectBrand'  },
  { path: `Project/DropdownList/ProjectType`, tag: 'Project_DropdownList_ProjectType'  },
  { path: `Project/DropdownList/ProjectStatus`, tag: 'Project_DropdownList_ProjectStatus'  },
  { path: `Project/DropdownList/ProjectZone`, tag: 'Project_DropdownList_ProjectZone'  },

];
mapEndpoints(endpoints)

const service = "api/v1/apcrm-prj-projectinfo-service"

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