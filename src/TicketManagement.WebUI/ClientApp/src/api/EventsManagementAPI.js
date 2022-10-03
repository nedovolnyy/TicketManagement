/* eslint-disable */
/*
    My Title generated
    
    version: 1.0.0
*/

export function Configuration(config) {
    this.basePath = '';
    this.headers = {};
    this.getHeaders = () => { return {} };
    this.responseHandler = null;
    this.errorHandler = null;

    if (config) {
        if (config.basePath) {
            this.basePath = config.basePath;
        }
        if (config.headers) {
            this.headers = config.headers;
        }
        if (config.getHeaders) {
            this.getHeaders = config.getHeaders;
        }
        if (config.responseHandler) {
            this.responseHandler = config.responseHandler;
        }
        if (config.errorHandler) {
            this.errorHandler = config.errorHandler;
        }
    }
}

const setAdditionalParams = (params = [], additionalParams = {}) => {
    Object.keys(additionalParams).forEach(key => {
        if(additionalParams[key]) {
            params.append(key, additionalParams[key]);
        }
    });
};

export function AreaManagementApi(config) {
    this.config = config || new Configuration();

    this.apiAreaManagementAreasGet = (options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/AreaManagement/areas';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiAreaManagementAreaPost = (body, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/AreaManagement/area';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'post',
                headers: { 'Content-Type': 'application/json', ...headers, ...getHeaders(), ...options.headers},
                body: 'object' === typeof body ? JSON.stringify(body) : body
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiAreaManagementAreaPut = (body, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/AreaManagement/area';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'put',
                headers: { 'Content-Type': 'application/json', ...headers, ...getHeaders(), ...options.headers},
                body: 'object' === typeof body ? JSON.stringify(body) : body
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiAreaManagementAreaAreaIdDelete = (areaId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/AreaManagement/area/{areaId}';
        url = url.split(['{', '}'].join('areaId')).join(encodeURIComponent(String(areaId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'delete',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiAreaManagementAreaAreaIdGet = (areaId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/AreaManagement/area/{areaId}';
        url = url.split(['{', '}'].join('areaId')).join(encodeURIComponent(String(areaId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiAreaManagementAreasByLayoutIdLayoutIdGet = (layoutId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/AreaManagement/AreasByLayoutId/{layoutId}';
        url = url.split(['{', '}'].join('layoutId')).join(encodeURIComponent(String(layoutId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };
}

export function EventAreaManagementApi(config) {
    this.config = config || new Configuration();

    this.apiEventAreaManagementEventAreasGet = (options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventAreaManagement/eventAreas';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventAreaManagementEventAreaPost = (body, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventAreaManagement/eventArea';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'post',
                headers: { 'Content-Type': 'application/json', ...headers, ...getHeaders(), ...options.headers},
                body: 'object' === typeof body ? JSON.stringify(body) : body
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventAreaManagementEventAreaPut = (body, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventAreaManagement/eventArea';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'put',
                headers: { 'Content-Type': 'application/json', ...headers, ...getHeaders(), ...options.headers},
                body: 'object' === typeof body ? JSON.stringify(body) : body
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventAreaManagementEventAreaEventAreaIdDelete = (eventAreaId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventAreaManagement/eventArea/{eventAreaId}';
        url = url.split(['{', '}'].join('eventAreaId')).join(encodeURIComponent(String(eventAreaId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'delete',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventAreaManagementEventAreaEventAreaIdGet = (eventAreaId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventAreaManagement/eventArea/{eventAreaId}';
        url = url.split(['{', '}'].join('eventAreaId')).join(encodeURIComponent(String(eventAreaId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventAreaManagementEventAreasByEventIdEventIdGet = (eventId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventAreaManagement/EventAreasByEventId/{eventId}';
        url = url.split(['{', '}'].join('eventId')).join(encodeURIComponent(String(eventId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };
}

export function EventManagementApi(config) {
    this.config = config || new Configuration();

    this.apiEventManagementEventsGet = (options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventManagement/events';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventManagementEventPost = (price, body, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventManagement/event';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (price !== undefined) {
            params.append('price', price);
        }
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'post',
                headers: { 'Content-Type': 'application/json', ...headers, ...getHeaders(), ...options.headers},
                body: 'object' === typeof body ? JSON.stringify(body) : body
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventManagementEventPut = (price, body, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventManagement/event';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (price !== undefined) {
            params.append('price', price);
        }
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'put',
                headers: { 'Content-Type': 'application/json', ...headers, ...getHeaders(), ...options.headers},
                body: 'object' === typeof body ? JSON.stringify(body) : body
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventManagementEventEventIdDelete = (eventId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventManagement/event/{eventId}';
        url = url.split(['{', '}'].join('eventId')).join(encodeURIComponent(String(eventId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'delete',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventManagementEventEventIdGet = (eventId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventManagement/event/{eventId}';
        url = url.split(['{', '}'].join('eventId')).join(encodeURIComponent(String(eventId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventManagementIsAllAvailableSeatsEventIdGet = (eventId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventManagement/IsAllAvailableSeats/{eventId}';
        url = url.split(['{', '}'].join('eventId')).join(encodeURIComponent(String(eventId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventManagementEventsByLayoutIdLayoutIdGet = (layoutId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventManagement/EventsByLayoutId/{layoutId}';
        url = url.split(['{', '}'].join('layoutId')).join(encodeURIComponent(String(layoutId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventManagementPriceByEventIdEventIdGet = (eventId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventManagement/PriceByEventId/{eventId}';
        url = url.split(['{', '}'].join('eventId')).join(encodeURIComponent(String(eventId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventManagementSeatsAvailableCountEventIdGet = (eventId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventManagement/SeatsAvailableCount/{eventId}';
        url = url.split(['{', '}'].join('eventId')).join(encodeURIComponent(String(eventId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventManagementSeatsCountLayoutIdGet = (layoutId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventManagement/SeatsCount/{layoutId}';
        url = url.split(['{', '}'].join('layoutId')).join(encodeURIComponent(String(layoutId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };
}

export function EventSeatManagementApi(config) {
    this.config = config || new Configuration();

    this.apiEventSeatManagementEventSeatsGet = (options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventSeatManagement/eventSeats';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventSeatManagementEventSeatPost = (body, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventSeatManagement/eventSeat';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'post',
                headers: { 'Content-Type': 'application/json', ...headers, ...getHeaders(), ...options.headers},
                body: 'object' === typeof body ? JSON.stringify(body) : body
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventSeatManagementEventSeatPut = (body, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventSeatManagement/eventSeat';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'put',
                headers: { 'Content-Type': 'application/json', ...headers, ...getHeaders(), ...options.headers},
                body: 'object' === typeof body ? JSON.stringify(body) : body
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventSeatManagementEventSeatEventSeatIdDelete = (eventSeatId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventSeatManagement/eventSeat/{eventSeatId}';
        url = url.split(['{', '}'].join('eventSeatId')).join(encodeURIComponent(String(eventSeatId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'delete',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventSeatManagementEventSeatEventSeatIdGet = (eventSeatId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventSeatManagement/eventSeat/{eventSeatId}';
        url = url.split(['{', '}'].join('eventSeatId')).join(encodeURIComponent(String(eventSeatId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventSeatManagementEventSeatStatusEventSeatIdPost = (eventSeatId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventSeatManagement/EventSeatStatus/{eventSeatId}';
        url = url.split(['{', '}'].join('eventSeatId')).join(encodeURIComponent(String(eventSeatId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'post',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiEventSeatManagementEventSeatsByEventAreaIdEventAreaIdGet = (eventAreaId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/EventSeatManagement/EventSeatsByEventAreaId/{eventAreaId}';
        url = url.split(['{', '}'].join('eventAreaId')).join(encodeURIComponent(String(eventAreaId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };
}

export function LayoutManagementApi(config) {
    this.config = config || new Configuration();

    this.apiLayoutManagementLayoutsGet = (options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/LayoutManagement/layouts';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiLayoutManagementLayoutPost = (body, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/LayoutManagement/layout';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'post',
                headers: { 'Content-Type': 'application/json', ...headers, ...getHeaders(), ...options.headers},
                body: 'object' === typeof body ? JSON.stringify(body) : body
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiLayoutManagementLayoutPut = (body, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/LayoutManagement/layout';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'put',
                headers: { 'Content-Type': 'application/json', ...headers, ...getHeaders(), ...options.headers},
                body: 'object' === typeof body ? JSON.stringify(body) : body
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiLayoutManagementLayoutLayoutIdDelete = (layoutId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/LayoutManagement/layout/{layoutId}';
        url = url.split(['{', '}'].join('layoutId')).join(encodeURIComponent(String(layoutId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'delete',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiLayoutManagementLayoutLayoutIdGet = (layoutId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/LayoutManagement/layout/{layoutId}';
        url = url.split(['{', '}'].join('layoutId')).join(encodeURIComponent(String(layoutId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiLayoutManagementLayoutsByVenueIdVenueIdGet = (venueId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/LayoutManagement/LayoutsByVenueId/{venueId}';
        url = url.split(['{', '}'].join('venueId')).join(encodeURIComponent(String(venueId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };
}

export function SeatManagementApi(config) {
    this.config = config || new Configuration();

    this.apiSeatManagementSeatsGet = (options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/SeatManagement/seats';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiSeatManagementSeatPost = (body, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/SeatManagement/seat';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'post',
                headers: { 'Content-Type': 'application/json', ...headers, ...getHeaders(), ...options.headers},
                body: 'object' === typeof body ? JSON.stringify(body) : body
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiSeatManagementSeatPut = (body, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/SeatManagement/seat';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'put',
                headers: { 'Content-Type': 'application/json', ...headers, ...getHeaders(), ...options.headers},
                body: 'object' === typeof body ? JSON.stringify(body) : body
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiSeatManagementSeatSeatIdDelete = (seatId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/SeatManagement/seat/{seatId}';
        url = url.split(['{', '}'].join('seatId')).join(encodeURIComponent(String(seatId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'delete',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiSeatManagementSeatSeatIdGet = (seatId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/SeatManagement/seat/{seatId}';
        url = url.split(['{', '}'].join('seatId')).join(encodeURIComponent(String(seatId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiSeatManagementSeatsByAreaIdAreaIdGet = (areaId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/SeatManagement/SeatsByAreaId/{areaId}';
        url = url.split(['{', '}'].join('areaId')).join(encodeURIComponent(String(areaId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };
}

export function ThirdPartyEventApi(config) {
    this.config = config || new Configuration();

    this.apiThirdPartyEventThirdPartyEventPost = (body, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/ThirdPartyEvent/ThirdPartyEvent';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'post',
                headers: { 'Content-Type': 'application/json', ...headers, ...getHeaders(), ...options.headers},
                body: 'object' === typeof body ? JSON.stringify(body) : body
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };
}

export function VenueManagementApi(config) {
    this.config = config || new Configuration();

    this.apiVenueManagementVenuesGet = (options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/VenueManagement/venues';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiVenueManagementVenuePost = (body, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/VenueManagement/venue';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'post',
                headers: { 'Content-Type': 'application/json', ...headers, ...getHeaders(), ...options.headers},
                body: 'object' === typeof body ? JSON.stringify(body) : body
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiVenueManagementVenuePut = (body, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/VenueManagement/venue';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'put',
                headers: { 'Content-Type': 'application/json', ...headers, ...getHeaders(), ...options.headers},
                body: 'object' === typeof body ? JSON.stringify(body) : body
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiVenueManagementVenueVenueIdDelete = (venueId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/VenueManagement/venue/{venueId}';
        url = url.split(['{', '}'].join('venueId')).join(encodeURIComponent(String(venueId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'delete',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            promise.then(response => {
                if (response.status === 200 || response.status === 204) {
                    resolve(response);
                } else {
                    reject(response);
                }
            });
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiVenueManagementVenueVenueIdGet = (venueId, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/VenueManagement/venue/{venueId}';
        url = url.split(['{', '}'].join('venueId')).join(encodeURIComponent(String(venueId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };

    this.apiVenueManagementVenueIdByNameNameGet = (name, options = {}) => {
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/VenueManagement/VenueIdByName/{name}';
        url = url.split(['{', '}'].join('name')).join(encodeURIComponent(String(name)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
            });
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response));
            } else {
                promise.then(response => {
                    if (response.status === 200 || response.status === 204) {
                        return response.json();
                    } else {
                        reject(response);
                    }
                }).then(data => resolve(data));
            }
            promise.catch(error => reject(error));
        });
        promise.abort = controller.abort;
        return promise;
    };
}
