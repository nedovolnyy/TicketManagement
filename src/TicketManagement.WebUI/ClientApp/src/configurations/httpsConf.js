export const configHTTPS = {
    basePath: 'https://localhost:5003',
    headers: { 'Content-Type': 'application/json' },
    getHeaders: () => { return {} },
    responseHandler: null,
    errorHandler: null
};
