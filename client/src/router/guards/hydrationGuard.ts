import { hydrationService } from '@/composables/hydration'
import { useAuthStore } from '@/stores/authStore'
import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router'

export const hydrationGuard = async (
  to: RouteLocationNormalized,
  from: RouteLocationNormalized,
  next: NavigationGuardNext,
) => {
  const authStore = useAuthStore()
  if (authStore.isAuthenticated() && !hydrationService.isHydrated()) {
    await hydrationService.hydrate()
  }
  next()
}
