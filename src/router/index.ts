// src/router/index.ts
import { createRouter, createWebHistory } from 'vue-router'
import { useAuth } from '@/composables/useAuth'

const router = createRouter({
  history: createWebHistory('/OsrsTodo/'),
  routes: [
    {
      path: '/',
      name: 'dashboard',
      component: () => import('@/views/DashboardView.vue'),
      meta: { requiresAuth: true, requiresUsername: true },
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('@/views/LoginView.vue'),
    },
    {
      path: '/register-user',
      name: 'register-user',
      component: () => import('@/views/RegisterUserView.vue'),
      meta: { requiresAuth: true },
    },
  ],
})

router.beforeEach(async (to, _from, next) => {
  const { isAuthenticated, hasUsername, initializeAuth } = useAuth()

  if (!isAuthenticated.value) {
    await initializeAuth()
  }

  if (to.meta['requiresAuth'] && !isAuthenticated.value) {
    next('/login')
  } else if (to.meta['requiresUsername'] && !hasUsername.value) {
    next('/register-user')
  } else if (to.name === 'login' && isAuthenticated.value) {
    if (hasUsername.value) {
      next('/')
    } else {
      next('/register-user')
    }
  } else {
    next()
  }
})

export default router
