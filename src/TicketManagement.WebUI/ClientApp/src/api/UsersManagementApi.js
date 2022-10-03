/* eslint-disable */
/*
    My Title generated
    
    version: 1.0.0
*/

export function Configuration(config) {
    this.basePath = '';
    this.fetchMethod = window.fetch;
    this.headers = {};
    this.getHeaders = () => { return {} };
    this.responseHandler = null;
    this.errorHandler = null;

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

    this.apiUsersManagementUserPost = (password, body, options = {}) => {
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

    this.apiUsersManagementUserIdDelete = (id, options = {}) => {
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

    this.apiUsersManagementUserUserIdGet = (userId, options = {}) => {
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

    this.apiUsersManagementHasPasswordUserIdGet = (userId, options = {}) => {
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

    this.apiUsersManagementIsEmailConfirmedUserIdGet = (userId, options = {}) => {
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

    this.apiUsersManagementEmailTokenUserIdPost = (userId, newEmail, options = {}) => {
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

    this.apiUsersManagementEmailUserIdPost = (userId, newEmail, token, options = {}) => {
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

    this.apiUsersManagementUserNameUserIdPost = (userId, userName, options = {}) => {
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

    this.apiUsersManagementPasswordUserIdPut = (userId, currentPassword, newPassword, options = {}) => {
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

    this.apiUsersManagementPasswordUserIdPost = (userId, password, options = {}) => {
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

    this.apiUsersManagementRefreshSignInUserIdPost = (userId, options = {}) => {
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

    this.apiUsersManagementPhoneNumberUserIdPut = (userId, phoneNumber, options = {}) => {
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

    this.apiUsersManagementRoleUserIdGet = (userId, options = {}) => {
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

    this.apiUsersManagementRoleUserIdPut = (userId, body, options = {}) => {
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

    this.apiUsersManagementCartPut = (money, userId, options = {}) => {
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

    this.apiUsersManagementPurchasePost = (eventSeatId, returnUrl, price, userId, options = {}) => {
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

    this.apiUsersManagementLanguageUserIdPost = (culture, userId, options = {}) => {
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

    this.apiUsersManagementRoleEmailPost = (email, role, options = {}) => {
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

    this.apiUsersManagementSignInEmailPost = (email, isPersistent, options = {}) => {
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

    this.apiUsersValidateGet = (token, options = {}) => {
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
