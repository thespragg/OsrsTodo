import { Http } from './http'
import { AccountsRoute } from './routes/accounts'
import { type IHttp } from './http'

export class Api {
  private _accounts: AccountsRoute | null = null
  private _http: IHttp = new Http()

  public accounts = (): AccountsRoute => {
    if (!this._accounts) {
      this._accounts = new AccountsRoute(this._http)
    }
    return this._accounts
  }
}

export const api = new Api()
