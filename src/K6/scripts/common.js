import http from 'k6/http';
import { sleep, check } from 'k6';

const ENDPOINT = "https://dev-api-gateway.apthai.com"
const token = __ENV.token || "F0AD1602FB6A4694DC91E1103C6E030726F866F67B754912BED3FF97EEDE6138"
export const projectId = "464DC028-4B61-4C1E-BF25-00399A859D9F"

export const thresholds = {};


export function mapEndpoints(endpoints) {
  endpoints.forEach(({ tag }) => {
    thresholds[`http_req_failed{type:${tag}}`] = ["rate<0.05"]; // Response failure rate must be less than 5%
    thresholds[`http_req_duration{type:${tag}}`] = ['p(95)<3000']; // 95% of response times must be less than 3 seconds
  });
}

export function mapRequests(service, endpoints) {
  for (const { path, tag, headers = null, duration = 2000 } of endpoints) { //loop  ยิง ตาม endpoints ที่ส่งเข้ามา
    get(`${ENDPOINT}/${service}/${path}`, tag, headers, duration);
  }
}
function get(endpoint, tags, headers = null, duration) {
  headers = headers || {
    'Accept': '*/*',
    'Authorization': `Bearer ${token}`,
    'Content-Type': 'application/json',
  };
  var res = http.get(endpoint, { headers: headers, tags: { type: tags } }); 

  const checks = {
    [`${tags} => is status 200`]: (r) => r.status === 200,
    [`${tags} => time is less than ${duration / 1000}s`]: (r) => r.timings.duration < duration, // Check if transaction time is less than 2 seconds
  };

  if (res.status === 429) {
    checks[`${tags} => is status 429`] = (r) => r.status === 429;
  }

  if (res.status === 500) {
    checks[`${tags} => is status 500`] = (r) => r.status === 500;
  }

  check(res, checks);
  if (res.status == 200) {
    console.log(`${tags} => status: ${res.status}`);
  } else {
    console.error(`${tags} => status: ${res.status}`);
  }
  sleep(Math.random() * duration / 1000);// รอเป็นช่วงเวลาแบบสุ่มระหว่าง 0 ถึง duration / 1000 วินาที
} 