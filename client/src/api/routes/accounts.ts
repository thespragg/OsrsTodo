import type { Account } from '@/types/account'
import type { IHttp } from '../http'
import { Route } from '../route'

export class AccountRoute extends Route {
  constructor(http: IHttp) {
    super(http)
  }

  get = async (): Promise<Account[]> => await this._http.get(`/accounts`)
}
