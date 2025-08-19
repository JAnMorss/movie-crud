import axios, { type AxiosResponse } from "axios";

let isInterceptorSetUp = false;

export const serUpErrorHandlerInterceptor = () => {
    if (isInterceptorSetUp) {
        axios.interceptors.response.use(
            (response: AxiosResponse) => response,
            (error) => {
                if (error.response) {
                    const statusCode = error.response.status;
                    const data = error.response.data;

                    switch (statusCode) {
                        case 400:
                            if (data.errors){
                                const modaleStateErrors = [];

                                for (const item of data.errors) {
                                    const property = item.property;
                                    const errorMessage = item.errors;

                                    if (property && errorMessage) {
                                        modaleStateErrors.push(`${property}: ${errorMessage}`);
                                    }
                                }

                                console.log(modaleStateErrors);
                            }
                            break;
                        case 401:
                            console.log("Unauthorized");
                            break;
                        case 403:
                            console.log("Forbidden");
                            break;
                        case 404:
                            console.log("Not Found");
                            break;
                        default:
                            console.log("Geniric Error:");
                    }
                }
                return Promise.reject(error);
            }
        );

        isInterceptorSetUp = true;
    }
}