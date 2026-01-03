import { mapRequests, thresholds, mapEndpoints,projectId } from '../common.js';

const endpoints = [
  { path: `Addresses/${projectId}/Addresses/DropdownList`, tag: 'Addresses_DropdownList',  duration: 1000 },
  { path: `Addresses/${projectId}/Addresses`, tag: 'Addresses'  },

  { path: `Agreements/${projectId}/Agreement`, tag: 'Agreements'  },

  { path: `Dropdowns/WalkRefer`, tag: 'Dropdowns_WalkRefer'  },
  { path: `Dropdowns/TitledeedRequestFlow`, tag: 'Dropdowns_TitledeedRequestFlow'  },
  { path: `Dropdowns/ProjectByProductType`, tag: 'Dropdowns_ProjectByProductType'  },
  { path: `Dropdowns/AllStatus`, tag: 'Dropdowns_AllStatus'  },
  { path: `Dropdowns/AllIsActive`, tag: 'Dropdowns_AllIsActive'  },
  { path: `Dropdowns/Project`, tag: 'Dropdowns_Project'  },

  { path: `Floors/${projectId}/Towers/Floors/DropdownList`, tag: 'Floors_Towers_Floors_DropdownList'  },
  { path: `Floors/${projectId}/Towers/FloorsEventBooking/DropdownList`, tag: 'Floors_Towers_FloorsEventBooking_DropdownList'  },
  
  // { path: `Images/${projectId}/Logo`, tag: 'Images_Logo'  },
  // { path: `Images/${projectId}/FloorPlanImages`, tag: 'Images_FloorPlanImages'  },
  // { path: `Images/${projectId}/FloorPlanDetail`, tag: 'Images_FloorPlanDetail'  },
  // { path: `Images/${projectId}/RoomPlanDetail`, tag: 'Images_RoomPlanDetail'  },
  // { path: `Images/${projectId}/RoomPlanImages`, tag: 'Images_RoomPlanImages'  },

  // { path: `Models/${projectId}/Models/DropdownList`, tag: 'Models_DropdownList'  },
  // { path: `Models/${projectId}/Models/All`, tag: 'Models_All'  },
  // { path: `Models/${projectId}/Models`, tag: 'Models'  },

  { path: `Projects`, tag: 'Projects'  },
  { path: `Projects/Count`, tag: 'Projects_Count'  },
  { path: `Projects/DefaultProject`, tag: 'Projects_DefaultProject'  },
  { path: `Projects/${projectId}/DataStatus`, tag: 'Projects_DataStatus'  },

  { path: `RoundFees/${projectId}/RoundFees`, tag: 'RoundFees'  },

  { path: `Towers/${projectId}/Towers`, tag: 'Towers'  },
  { path: `Towers/${projectId}/Towers/DropdownList`, tag: 'Towers_DropdownList'  },
];
mapEndpoints(endpoints)

const service = "api/v1/apcrm-prj-project-service"

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