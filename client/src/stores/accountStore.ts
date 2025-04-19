import { api } from '@/api'
import type { Account } from '@/types'
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useAccountsStore = defineStore('account', () => {
  const accounts = ref<Account[]>([])

  const fetchAccounts = async () => {
    try {
      accounts.value = await api.accounts().get()
    } catch (error) {
      throw error
    }
  }

  return {
    fetchAccounts,
    accounts,
  }
})
