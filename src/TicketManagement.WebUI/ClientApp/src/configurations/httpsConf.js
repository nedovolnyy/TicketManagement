export const configHTTPS = {
    basePath: 'https://localhost:5003',
    headers: { 'Content-Type': 'application/json' },
    fetchMethod: () => window.fetch,
    getHeaders: () => { return {} },
    responseHandler: null,
    errorHandler: null
};
