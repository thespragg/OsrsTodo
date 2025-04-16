<template>
  <div class="min-h-screen flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div class="max-w-md w-full space-y-8">
      <Card class="p-6">
        <template #header>
          <h2 class="mt-6 text-3xl font-extrabold text-gray-50">Sign in to your account</h2>
        </template>
        <template #content>
          <form class="mt-8 space-y-6" @submit.prevent="handleLogin">
            <div class="rounded-md shadow-sm space-y-4">
              <div>
                <label for="username" class="block text-sm font-medium text-gray-50"
                  >Username</label
                >
                <InputText
                  id="username"
                  v-model="form.username"
                  type="text"
                  required
                  class="mt-1 w-full"
                  :class="{ 'p-invalid': errors.username }"
                />
                <small class="p-error" v-if="errors.username">{{ errors.username }}</small>
              </div>

              <div>
                <label for="password" class="block text-sm font-medium text-gray-50"
                  >Password</label
                >
                <Password
                  id="password"
                  v-model="form.password"
                  required
                  class="mt-1 w-full"
                  :class="{ 'p-invalid': errors.password }"
                  toggleMask
                  :feedback="false"
                />
                <small class="p-error" v-if="errors.password">{{ errors.password }}</small>
              </div>
            </div>

            <!-- <div class="flex items-center justify-between">
              <div class="flex items-center">
                <Checkbox id="remember" v-model="rememberMe" binary />
                <label for="remember" class="ml-2 block text-sm text-gray-50">Remember me</label>
              </div>

              <div class="text-sm">
                <router-link
                  to="/forgot-password"
                  class="font-medium text-primary-600 hover:text-primary-500"
                >
                  Forgot your password?
                </router-link>
              </div>
            </div> -->

            <div>
              <Button type="submit" label="Sign in" class="w-full" :loading="loading" />
            </div>
          </form>

          <div class="mt-6">
            <div class="relative">
              <div class="absolute inset-0 flex items-center">
                <div class="w-full border-t border-gray-300"></div>
              </div>
              <div class="relative flex justify-center text-sm">
                <span class="px-2 bg-white text-gray-500 rounded-full">Or</span>
              </div>
            </div>

            <div class="mt-6">
              <router-link to="/register">
                <Button label="Create a new account" class="w-full" severity="secondary" />
              </router-link>
            </div>
          </div>
        </template>
      </Card>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/authStore'
import { useToast } from 'primevue/usetoast'

const authStore = useAuthStore()
const router = useRouter()
const toast = useToast()

const form = ref({
  username: '',
  password: '',
})

const errors = ref({
  username: '',
  password: '',
})

const loading = ref(false)
const rememberMe = ref(false)

const handleLogin = async () => {
  loading.value = true
  errors.value = { username: '', password: '' }

  try {
    await authStore.login(form.value)
    toast.add({
      severity: 'success',
      summary: 'Success',
      detail: 'Logged in successfully',
      life: 3000,
    })

    const redirect = router.currentRoute.value.query.redirect
    router.push(redirect ? redirect.toString() : '/')
  } catch (error: any) {
    if (error.detail) {
      errors.value.username = error.detail
    } else {
      toast.add({
        severity: 'error',
        summary: 'Login Failed',
        detail: error.response?.data?.message || 'An error occurred during login',
        life: 5000,
      })
    }
  } finally {
    loading.value = false
  }
}
</script>
