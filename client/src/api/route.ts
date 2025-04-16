import { type IHttp } from './http'

export abstract class Route {
  protected readonly _http: IHttp
  constructor(http: IHttp) {
    this._http = http
  }
}
