import axios, { AxiosRequestConfig, AxiosResponse } from "axios";

class axiosFacade {
  static get = <T>(url: string, config?: AxiosRequestConfig<any> | undefined): Promise<AxiosResponse<T, any>> => {
    return axios.get<T>(url, config);
  };

  static post = <T>(
    url: string,
    data?: any,
    config?: AxiosRequestConfig<any> | undefined
  ): Promise<AxiosResponse<T, any>> => {
    return axios.post<T>(url, data, config);
  };

  static put = <T>(
    url: string,
    data?: any,
    config?: AxiosRequestConfig<any> | undefined
  ): Promise<AxiosResponse<T, any>> => {
    return axios.put<T>(url, data, config);
  };

  static delete = <T>(url: string, config?: AxiosRequestConfig<any> | undefined): Promise<AxiosResponse<T, any>> => {
    return axios.delete<T>(url, config);
  };
}

export default axiosFacade;
