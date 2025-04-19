import axios, {
  type AxiosInstance,
  type AxiosRequestConfig,
  type AxiosRequestHeaders,
  type AxiosResponse,
  type InternalAxiosRequestConfig,
} from 'axios'

export interface IHttp {
  request<T = any, R = T>(config: AxiosRequestConfig): Promise<R>
  get<T = any, R = T>(url: string, config?: AxiosRequestConfig): Promise<R>
  post<T = any, R = T>(url: string, data?: T, config?: AxiosRequestConfig): Promise<R>
  put<T = any, R = T>(url: string, data?: T, config?: AxiosRequestConfig): Promise<R>
  patch<T = any, R = T>(url: string, data?: T, config?: AxiosRequestConfig): Promise<R>
  delete<T = any, R = T>(url: string, config?: AxiosRequestConfig): Promise<R>
}

export class Http implements IHttp {
  private instance: AxiosInstance | null = null
  private token: string | undefined

  public get bearer(): string | undefined {
    return this.token
  }

  private get http(): AxiosInstance {
    return this.instance != null ? this.instance : this.initHttp('http://localhost:5000')
  }

  protected addResponseTranslator(instance: AxiosInstance): void {
    instance.interceptors.response.use(
      (resp: AxiosResponse) => resp.data,
      (error: any) => this.handleError(error),
    )
  }

  protected initHttp(baseUrl: string): AxiosInstance {
    const inst = axios.create({
      baseURL: baseUrl,
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json; charset=utf-8',
        'X-Requested-With': 'XMLHttpRequest',
      },
    })

    inst.interceptors.request.use(
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      async (config: InternalAxiosRequestConfig<any>) => {
        this.token = localStorage.getItem('accessToken') ?? undefined
        if (!config.headers) config.headers = {} as AxiosRequestHeaders
        config.headers.Authorization = `Bearer ${this.token}`
        return config
      },
      async (error) => {
        return await Promise.reject(error)
      },
    )

    this.addResponseTranslator(inst)

    this.instance = inst
    return inst
  }

  public request<T = any, R = T>(config: AxiosRequestConfig): Promise<R> {
    return this.http.request(config)
  }

  public get<T = any, R = T>(url: string, config?: AxiosRequestConfig): Promise<R> {
    return this.http.get<T, R>(url, config)
  }

  public post<T = any, R = T>(url: string, data?: T, config?: AxiosRequestConfig): Promise<R> {
    return this.http.post<T, R>(url, data, config)
  }

  public async put<T = any, R = T>(url: string, data?: T, config?: AxiosRequestConfig): Promise<R> {
    return this.http.put<T, R>(url, data, config)
  }

  public patch<T = any, R = T>(url: string, data?: T, config?: AxiosRequestConfig): Promise<R> {
    return this.http.patch<T, R>(url, data, config)
  }

  public delete<T = any, R = T>(url: string, config?: AxiosRequestConfig): Promise<R> {
    return this.http.delete<T, R>(url, config)
  }

  private handleError(error: any) {
    if (!error?.response && error?.request) {
      return
    }

    if (error.response.data.detail) {
      throw {
        detail: error.response.data.detail,
      }
    }

    throw error
  }
}
