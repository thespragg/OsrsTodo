import { createRouter, createWebHistory } from 'vue-router'
import { supabase } from '@/lib/supabase'

const router = createRouter({
  history: createWebHistory('/OsrsTodo/'),
  routes: [
    {
      path: '/',
      name: 'dashboard',
      component: () => import('@/views/DashboardView.vue'),
      meta: { requiresAuth: true },
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('@/views/LoginView.vue'),
      meta: { requiresAuth: false },
    },
    {
      path: '/register',
      name: 'register',
      component: () => import('@/views/RegisterView.vue'),
      meta: { requiresAuth: false },
    },
    {
      path: '/forgot-password',
      name: 'forgot-password',
      component: () => import('@/views/ForgottenPasswordView.vue'),
      meta: { requiresAuth: false },
    },
  ],
})

router.beforeEach(async (to, _from, next) => {
  const user = await supabase.auth.getUser()
  if (!to.meta['requiresAuth'] || user.data.user !== null) {
    return next()
  } else {
    return next({ name: 'login' })
  }
})

export default router
