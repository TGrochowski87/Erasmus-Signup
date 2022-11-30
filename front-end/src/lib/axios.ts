import axios, { AxiosRequestConfig, AxiosResponse } from "axios";

class axiosFacade {
  static get = <T>(
    url: string,
    config?: AxiosRequestConfig<any> | undefined
  ): Promise<AxiosResponse<any, any>> => {
    return axios.get<T>(url, config);
  };
}

export default axiosFacade;
