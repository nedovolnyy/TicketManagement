export const EventsManagementApiHTTPSconfig = {
    basePath: 'https://localhost:5003',
    headers: { 'Content-Type': 'application/json' },
    getHeaders: () => { return {} },
    responseHandler: null,
    errorHandler: null
};

export const UsersManagementApiHTTPSconfig = {
  basePath: 'https://localhost:5004',
  headers: { 'Content-Type': 'application/json' },
  getHeaders: () => { return {} },
  responseHandler: null,
  errorHandler: null
};
