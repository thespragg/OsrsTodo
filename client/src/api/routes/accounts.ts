import { type AccessToken } from '@/types/accessToken'
import { Route } from '../route'
import { type IHttp } from '../http'

export class AccountsRoute extends Route {
  constructor(http: IHttp) {
    super(http)
  }

  login = async (loginRequest: { username: string; password: string }): Promise<AccessToken> =>
    await this._http.post<any, AccessToken>(`/accounts/login`, loginRequest)

  register = async (registerRequest: {
    username: string
    email: string
    password: string
  }): Promise<AccessToken> =>
    await this._http.post<any, AccessToken>(`/accounts/register`, registerRequest)
}
