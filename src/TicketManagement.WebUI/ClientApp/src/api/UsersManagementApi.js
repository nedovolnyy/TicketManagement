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

export function UsersManagementApi(config) {
    this.config = config || new Configuration();

    this.apiUsersManagementUsersGet = (options = {}) => { 
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/users';
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

    this.apiUsersManagementUserPost = (args, body, options = {}) => { 
        const { password } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/user';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (password !== undefined) {
            params.append('password', password);
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

    this.apiUsersManagementUserPut = (body, options = {}) => { 
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/user';
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

    this.apiUsersManagementUserIdDelete = (args, options = {}) => { 
        const { id } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/user/{id}';
        url = url.split(['{', '}'].join('id')).join(encodeURIComponent(String(id)));
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

    this.apiUsersManagementUserUserIdGet = (args, options = {}) => { 
        const { userId } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/user/{userId}';
        url = url.split(['{', '}'].join('userId')).join(encodeURIComponent(String(userId)));
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

    this.apiUsersManagementHasPasswordUserIdGet = (args, options = {}) => { 
        const { userId } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/HasPassword/{userId}';
        url = url.split(['{', '}'].join('userId')).join(encodeURIComponent(String(userId)));
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

    this.apiUsersManagementIsEmailConfirmedUserIdGet = (args, options = {}) => { 
        const { userId } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/IsEmailConfirmed/{userId}';
        url = url.split(['{', '}'].join('userId')).join(encodeURIComponent(String(userId)));
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

    this.apiUsersManagementEmailTokenUserIdPost = (args, options = {}) => { 
        const { userId, newEmail } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/EmailToken/{userId}';
        url = url.split(['{', '}'].join('userId')).join(encodeURIComponent(String(userId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (newEmail !== undefined) {
            params.append('newEmail', newEmail);
        }
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

    this.apiUsersManagementEmailUserIdPost = (args, options = {}) => { 
        const { userId, newEmail, token } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/Email/{userId}';
        url = url.split(['{', '}'].join('userId')).join(encodeURIComponent(String(userId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (newEmail !== undefined) {
            params.append('newEmail', newEmail);
        }
        if (token !== undefined) {
            params.append('token', token);
        }
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

    this.apiUsersManagementUserNameUserIdPost = (args, options = {}) => { 
        const { userId, userName } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/UserName/{userId}';
        url = url.split(['{', '}'].join('userId')).join(encodeURIComponent(String(userId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (userName !== undefined) {
            params.append('userName', userName);
        }
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

    this.apiUsersManagementPasswordUserIdPut = (args, options = {}) => { 
        const { userId, currentPassword, newPassword } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/password/{userId}';
        url = url.split(['{', '}'].join('userId')).join(encodeURIComponent(String(userId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (currentPassword !== undefined) {
            params.append('currentPassword', currentPassword);
        }
        if (newPassword !== undefined) {
            params.append('newPassword', newPassword);
        }
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'put',
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

    this.apiUsersManagementPasswordUserIdPost = (args, options = {}) => { 
        const { userId, password } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/password/{userId}';
        url = url.split(['{', '}'].join('userId')).join(encodeURIComponent(String(userId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (password !== undefined) {
            params.append('password', password);
        }
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

    this.apiUsersManagementRefreshSignInUserIdPost = (args, options = {}) => { 
        const { userId } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/RefreshSignIn/{userId}';
        url = url.split(['{', '}'].join('userId')).join(encodeURIComponent(String(userId)));
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

    this.apiUsersManagementPhoneNumberUserIdPut = (args, options = {}) => { 
        const { userId, phoneNumber } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/PhoneNumber/{userId}';
        url = url.split(['{', '}'].join('userId')).join(encodeURIComponent(String(userId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (phoneNumber !== undefined) {
            params.append('phoneNumber', phoneNumber);
        }
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'put',
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

    this.apiUsersManagementRoleUserIdGet = (args, options = {}) => { 
        const { userId } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/role/{userId}';
        url = url.split(['{', '}'].join('userId')).join(encodeURIComponent(String(userId)));
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

    this.apiUsersManagementRoleUserIdPut = (args, body, options = {}) => { 
        const { userId } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/role/{userId}';
        url = url.split(['{', '}'].join('userId')).join(encodeURIComponent(String(userId)));
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

    this.apiUsersManagementCartPut = (args, options = {}) => { 
        const { money, userId } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/cart';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (money !== undefined) {
            params.append('money', money);
        }
        if (userId !== undefined) {
            params.append('userId', userId);
        }
        const query = params.toString();
        const controller = new AbortController();
        const promise = new Promise((resolve, reject) => {
            const promise = fetch(basePath + url + (query ? '?' + query : ''), {
                signal: controller.signal,
                method: 'put',
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

    this.apiUsersManagementPurchasePost = (args, options = {}) => { 
        const { eventSeatId, returnUrl, price, userId } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/purchase';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (eventSeatId !== undefined) {
            params.append('eventSeatId', eventSeatId);
        }
        if (returnUrl !== undefined) {
            params.append('returnUrl', returnUrl);
        }
        if (price !== undefined) {
            params.append('price', price);
        }
        if (userId !== undefined) {
            params.append('userId', userId);
        }
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

    this.apiUsersManagementLanguageUserIdPost = (args, options = {}) => { 
        const { culture, userId } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/language/{userId}';
        url = url.split(['{', '}'].join('userId')).join(encodeURIComponent(String(userId)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (culture !== undefined) {
            params.append('culture', culture);
        }
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

    this.apiUsersManagementRoleEmailPost = (args, options = {}) => { 
        const { email, role } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/role/{email}';
        url = url.split(['{', '}'].join('email')).join(encodeURIComponent(String(email)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (role !== undefined) {
            params.append('role', role);
        }
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

    this.apiUsersManagementUserUPost = (body, options = {}) => { 
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/user/u';
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

    this.apiUsersManagementSignInEmailPost = (args, options = {}) => { 
        const { email, isPersistent } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/UsersManagement/SignIn/{email}';
        url = url.split(['{', '}'].join('email')).join(encodeURIComponent(String(email)));
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (isPersistent !== undefined) {
            params.append('isPersistent', isPersistent);
        }
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
}

export function UsersApi(config) {
    this.config = config || new Configuration();

    this.apiUsersRegisterPost = (body, options = {}) => { 
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/Users/Register';
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

    this.apiUsersLoginPost = (body, options = {}) => { 
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/Users/Login';
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

    this.apiUsersValidateGet = (args, options = {}) => { 
        const { token } = args;
        const { basePath, headers, getHeaders, responseHandler, errorHandler} = this.config;
        let url = '/api/Users/validate';
        const params = new URLSearchParams();
        setAdditionalParams(params, options.params);
        if (token !== undefined) {
            params.append('token', token);
        }
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
