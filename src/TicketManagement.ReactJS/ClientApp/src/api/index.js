// @ts-nocheck
/*
    My Title generated
    
    version: 1.0.0
*/

export class Configuration {
	basePath? = '';
	fetchMethod = window.fetch;
	headers?: any = {};
    getHeaders: any = () => { return {} };
    responseHandler: any = null;
    errorHandler: any = null;

	constructor(config: Configuration | any) {
	    if (config) {
	        if (config.basePath) {
                this.basePath = config.basePath;
            }
            if (config.fetchMethod) {
                this.fetchMethod = config.fetchMethod;
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
}

const setAdditionalParams = (params = [], additionalParams = {}) => {
    Object.keys(additionalParams).forEach(key => {
        if(additionalParams[key]) {
            params.append(key, additionalParams[key]);
        }
    });
};

export class Area {

	constructor(obj: Area) {
	}
}

export class BaseEntity {
	id?: number;

	constructor(obj: BaseEntity) {
        this.id = obj.id;
	}
}

export class ProblemDetails {
	type: string;
	title: string;
	status: number;
	detail: string;
	instance: string;

	constructor(obj: ProblemDetails) {
        this.type = obj.type;
        this.title = obj.title;
        this.status = obj.status;
        this.detail = obj.detail;
        this.instance = obj.instance;
	}
}

export class EventArea {

	constructor(obj: EventArea) {
	}
}

export class Event {

	constructor(obj: Event) {
	}
}

export class EventSeat {

	constructor(obj: EventSeat) {
	}
}

export class Layout {

	constructor(obj: Layout) {
	}
}

export class Seat {

	constructor(obj: Seat) {
	}
}

export class EventFromJson {
	fullImagePath: string;
	event: any;
	price?: number;
	eventLogoImage: string;

	constructor(obj: EventFromJson) {
        this.fullImagePath = obj.fullImagePath;
        this.event = obj.event;
        this.price = obj.price;
        this.eventLogoImage = obj.eventLogoImage;
	}
}

export class Venue {

	constructor(obj: Venue) {
	}
}

export class MethodOptions {
	headers?: any = {};
	returnResponse?: boolean = false;

	constructor(options: MethodOptions) {
		if (options.headers) {
			this.headers = options.headers;
		}
		if (options.returnResponse) {
			this.returnResponse = options.returnResponse;
		}
	}
}

export class AreaManagementApi {
    private readonly config: Configuration;

    constructor(config: Configuration | any) {
        this.config = new Configuration(config);
    }

	apiAreaManagementAreasGet(options: MethodOptions = {}): Promise<Area[]> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/AreaManagement/areas';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiAreaManagementAreaPost(body?: Area, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/AreaManagement/area';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiAreaManagementAreaPut(body?: Area, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/AreaManagement/area';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiAreaManagementAreaAreaIdDelete(areaId: number, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/AreaManagement/area/{areaId}';
		url = url.split(['{', '}'].join('areaId')).join(encodeURIComponent(String(areaId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiAreaManagementAreaAreaIdGet(areaId: number, options: MethodOptions = {}): Promise<Area> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/AreaManagement/area/{areaId}';
		url = url.split(['{', '}'].join('areaId')).join(encodeURIComponent(String(areaId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiAreaManagementAreasByLayoutIdLayoutIdGet(layoutId: number, options: MethodOptions = {}): Promise<Area[]> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/AreaManagement/AreasByLayoutId/{layoutId}';
		url = url.split(['{', '}'].join('layoutId')).join(encodeURIComponent(String(layoutId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}
}

export class EventAreaManagementApi {
    private readonly config: Configuration;

    constructor(config: Configuration | any) {
        this.config = new Configuration(config);
    }

	apiEventAreaManagementEventAreasGet(options: MethodOptions = {}): Promise<EventArea[]> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventAreaManagement/eventAreas';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiEventAreaManagementEventAreaPost(body?: EventArea, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventAreaManagement/eventArea';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiEventAreaManagementEventAreaPut(body?: EventArea, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventAreaManagement/eventArea';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiEventAreaManagementEventAreaEventAreaIdDelete(eventAreaId: number, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventAreaManagement/eventArea/{eventAreaId}';
		url = url.split(['{', '}'].join('eventAreaId')).join(encodeURIComponent(String(eventAreaId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiEventAreaManagementEventAreaEventAreaIdGet(eventAreaId: number, options: MethodOptions = {}): Promise<EventArea> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventAreaManagement/eventArea/{eventAreaId}';
		url = url.split(['{', '}'].join('eventAreaId')).join(encodeURIComponent(String(eventAreaId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiEventAreaManagementEventAreasByEventIdEventIdGet(eventId: number, options: MethodOptions = {}): Promise<EventArea[]> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventAreaManagement/EventAreasByEventId/{eventId}';
		url = url.split(['{', '}'].join('eventId')).join(encodeURIComponent(String(eventId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}
}

export class EventManagementApi {
    private readonly config: Configuration;

    constructor(config: Configuration | any) {
        this.config = new Configuration(config);
    }

	apiEventManagementEventsGet(options: MethodOptions = {}): Promise<Event[]> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventManagement/events';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiEventManagementEventPost(price: number, body?: Event, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventManagement/event';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (price !== undefined) {
            params.append('price', price as any);
        }
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiEventManagementEventPut(price: number, body?: Event, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventManagement/event';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (price !== undefined) {
            params.append('price', price as any);
        }
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiEventManagementEventEventIdDelete(eventId: number, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventManagement/event/{eventId}';
		url = url.split(['{', '}'].join('eventId')).join(encodeURIComponent(String(eventId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiEventManagementEventEventIdGet(eventId: number, options: MethodOptions = {}): Promise<Event> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventManagement/event/{eventId}';
		url = url.split(['{', '}'].join('eventId')).join(encodeURIComponent(String(eventId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiEventManagementIsAllAvailableSeatsEventIdGet(eventId: number, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventManagement/IsAllAvailableSeats/{eventId}';
		url = url.split(['{', '}'].join('eventId')).join(encodeURIComponent(String(eventId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiEventManagementEventsByLayoutIdLayoutIdGet(layoutId: number, options: MethodOptions = {}): Promise<Event[]> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventManagement/EventsByLayoutId/{layoutId}';
		url = url.split(['{', '}'].join('layoutId')).join(encodeURIComponent(String(layoutId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiEventManagementPriceByEventIdEventIdGet(eventId: number, options: MethodOptions = {}): Promise<number> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventManagement/PriceByEventId/{eventId}';
		url = url.split(['{', '}'].join('eventId')).join(encodeURIComponent(String(eventId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiEventManagementSeatsAvailableCountEventIdGet(eventId: number, options: MethodOptions = {}): Promise<number> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventManagement/SeatsAvailableCount/{eventId}';
		url = url.split(['{', '}'].join('eventId')).join(encodeURIComponent(String(eventId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiEventManagementSeatsCountLayoutIdGet(layoutId: number, options: MethodOptions = {}): Promise<number> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventManagement/SeatsCount/{layoutId}';
		url = url.split(['{', '}'].join('layoutId')).join(encodeURIComponent(String(layoutId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}
}

export class EventSeatManagementApi {
    private readonly config: Configuration;

    constructor(config: Configuration | any) {
        this.config = new Configuration(config);
    }

	apiEventSeatManagementEventSeatsGet(options: MethodOptions = {}): Promise<EventSeat[]> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventSeatManagement/eventSeats';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiEventSeatManagementEventSeatPost(body?: EventSeat, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventSeatManagement/eventSeat';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiEventSeatManagementEventSeatPut(body?: EventSeat, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventSeatManagement/eventSeat';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiEventSeatManagementEventSeatEventSeatIdDelete(eventSeatId: number, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventSeatManagement/eventSeat/{eventSeatId}';
		url = url.split(['{', '}'].join('eventSeatId')).join(encodeURIComponent(String(eventSeatId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiEventSeatManagementEventSeatEventSeatIdGet(eventSeatId: number, options: MethodOptions = {}): Promise<EventSeat> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventSeatManagement/eventSeat/{eventSeatId}';
		url = url.split(['{', '}'].join('eventSeatId')).join(encodeURIComponent(String(eventSeatId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiEventSeatManagementEventSeatStatusEventSeatIdPost(eventSeatId: number, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventSeatManagement/EventSeatStatus/{eventSeatId}';
		url = url.split(['{', '}'].join('eventSeatId')).join(encodeURIComponent(String(eventSeatId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiEventSeatManagementEventSeatsByEventAreaIdEventAreaIdGet(eventAreaId: number, options: MethodOptions = {}): Promise<EventSeat[]> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/EventSeatManagement/EventSeatsByEventAreaId/{eventAreaId}';
		url = url.split(['{', '}'].join('eventAreaId')).join(encodeURIComponent(String(eventAreaId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}
}

export class LayoutManagementApi {
    private readonly config: Configuration;

    constructor(config: Configuration | any) {
        this.config = new Configuration(config);
    }

	apiLayoutManagementLayoutsGet(options: MethodOptions = {}): Promise<Layout[]> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/LayoutManagement/layouts';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiLayoutManagementLayoutPost(body?: Layout, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/LayoutManagement/layout';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiLayoutManagementLayoutPut(body?: Layout, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/LayoutManagement/layout';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiLayoutManagementLayoutLayoutIdDelete(layoutId: number, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/LayoutManagement/layout/{layoutId}';
		url = url.split(['{', '}'].join('layoutId')).join(encodeURIComponent(String(layoutId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiLayoutManagementLayoutLayoutIdGet(layoutId: number, options: MethodOptions = {}): Promise<Layout> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/LayoutManagement/layout/{layoutId}';
		url = url.split(['{', '}'].join('layoutId')).join(encodeURIComponent(String(layoutId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiLayoutManagementLayoutsByVenueIdVenueIdGet(venueId: number, options: MethodOptions = {}): Promise<Layout[]> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/LayoutManagement/LayoutsByVenueId/{venueId}';
		url = url.split(['{', '}'].join('venueId')).join(encodeURIComponent(String(venueId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}
}

export class SeatManagementApi {
    private readonly config: Configuration;

    constructor(config: Configuration | any) {
        this.config = new Configuration(config);
    }

	apiSeatManagementSeatsGet(options: MethodOptions = {}): Promise<Seat[]> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/SeatManagement/seats';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiSeatManagementSeatPost(body?: Seat, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/SeatManagement/seat';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiSeatManagementSeatPut(body?: Seat, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/SeatManagement/seat';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiSeatManagementSeatSeatIdDelete(seatId: number, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/SeatManagement/seat/{seatId}';
		url = url.split(['{', '}'].join('seatId')).join(encodeURIComponent(String(seatId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiSeatManagementSeatSeatIdGet(seatId: number, options: MethodOptions = {}): Promise<Seat> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/SeatManagement/seat/{seatId}';
		url = url.split(['{', '}'].join('seatId')).join(encodeURIComponent(String(seatId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiSeatManagementSeatsByAreaIdAreaIdGet(areaId: number, options: MethodOptions = {}): Promise<Seat[]> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/SeatManagement/SeatsByAreaId/{areaId}';
		url = url.split(['{', '}'].join('areaId')).join(encodeURIComponent(String(areaId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}
}

export class ThirdPartyEventApi {
    private readonly config: Configuration;

    constructor(config: Configuration | any) {
        this.config = new Configuration(config);
    }

	apiThirdPartyEventThirdPartyEventPost(body?: EventFromJson, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/ThirdPartyEvent/ThirdPartyEvent';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}
}

export class VenueManagementApi {
    private readonly config: Configuration;

    constructor(config: Configuration | any) {
        this.config = new Configuration(config);
    }

	apiVenueManagementVenuesGet(options: MethodOptions = {}): Promise<Venue[]> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/VenueManagement/venues';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiVenueManagementVenuePost(body?: Venue, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/VenueManagement/venue';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiVenueManagementVenuePut(body?: Venue, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/VenueManagement/venue';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiVenueManagementVenueVenueIdDelete(venueId: number, options: MethodOptions = {}): Promise<any> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/VenueManagement/venue/{venueId}';
		url = url.split(['{', '}'].join('venueId')).join(encodeURIComponent(String(venueId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
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
	}

	apiVenueManagementVenueVenueIdGet(venueId: number, options: MethodOptions = {}): Promise<Venue> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/VenueManagement/venue/{venueId}';
		url = url.split(['{', '}'].join('venueId')).join(encodeURIComponent(String(venueId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}

	apiVenueManagementVenueIdByNameNameGet(name?: string, options: MethodOptions = {}): Promise<number> {
        const {fetchMethod, basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
		let url = '/api/VenueManagement/VenueIdByName/{name}';
		url = url.split(['{', '}'].join('name')).join(encodeURIComponent(String(name)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
		const query = params.toString();
		return new Promise((resolve, reject) => {
			const promise = fetchMethod(basePath + url + (query ? '?' + query : ''), {
				method: 'get',
                headers: { ...headers, ...getHeaders(), ...options.headers}
			});
            !!responseHandler && promise.then(responseHandler);
            !!errorHandler && promise.catch(errorHandler);
            if (options.returnResponse) {
                promise.then(response => resolve(response as any));
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
	}
}
