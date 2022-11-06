import axios, { AxiosResponse } from "axios";

class axiosFacade {
  static get = <T>(url: string): Promise<AxiosResponse<any, any>> => {
    return axios.get<T>(url);
  };
}

export default axiosFacade;
