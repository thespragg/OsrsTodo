import { Route } from '../route'
import { type IHttp } from '../http'
import type { AccessToken } from '@/types'

export class UserRoute extends Route {
  constructor(http: IHttp) {
    super(http)
  }

  login = async (loginRequest: { username: string; password: string }): Promise<AccessToken> =>
    await this._http.post<any, AccessToken>(`/user/login`, loginRequest)

  register = async (registerRequest: {
    username: string
    email: string
    password: string
  }): Promise<AccessToken> =>
    await this._http.post<any, AccessToken>(`/user/register`, registerRequest)
}
