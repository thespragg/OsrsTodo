import { Http } from './http'
import { UserRoute } from './routes/user'
import { type IHttp } from './http'
import { AccountRoute } from './routes/accounts'

export class Api {
  private _user: UserRoute | null = null
  private _accounts: AccountRoute | null = null
  private _http: IHttp = new Http()

  public user = (): UserRoute => {
    if (!this._user) {
      this._user = new UserRoute(this._http)
    }
    return this._user
  }

  public accounts = (): AccountRoute => {
    if (!this._accounts) {
      this._accounts = new AccountRoute(this._http)
    }
    return this._accounts
  }
}

export const api = new Api()
