import { useAccountsStore } from '@/stores/accountStore'
import { ref } from 'vue'

const useHydration = () => {
  const hydrated = ref(false)

  const hydrate = async () => {
    const accountStore = useAccountsStore()

    await accountStore.fetchAccounts()
  }

  const isHydrated = () => hydrated.value

  return {
    isHydrated,
    hydrate,
  }
}

export const hydrationService = useHydration()
