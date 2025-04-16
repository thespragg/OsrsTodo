<template>
  <div class="min-h-screen flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div class="max-w-md w-full space-y-8">
      <Card class="p-6">
        <template #header>
          <h2 class="text-3xl font-extrabold text-gray-50">Create a new account</h2>
        </template>
        <template #content>
          <form class="mt-8 space-y-6" @submit.prevent="handleRegister">
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
                <label for="email" class="block text-sm font-medium text-gray-50">Email</label>
                <InputText
                  id="email"
                  v-model="form.email"
                  type="email"
                  required
                  class="mt-1 w-full"
                  :class="{ 'p-invalid': errors.email }"
                />
                <small class="p-error" v-if="errors.email">{{ errors.email }}</small>
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
                  :feedback="true"
                />
                <small class="p-error" v-if="errors.password">{{ errors.password }}</small>
              </div>

              <div>
                <label for="confirmPassword" class="block text-sm font-medium text-gray-50"
                  >Confirm Password</label
                >
                <Password
                  id="confirmPassword"
                  v-model="confirmPassword"
                  required
                  class="mt-1 w-full"
                  :class="{ 'p-invalid': errors.confirmPassword }"
                  toggleMask
                  :feedback="false"
                />
                <small class="p-error" v-if="errors.confirmPassword">{{
                  errors.confirmPassword
                }}</small>
              </div>
            </div>

            <div>
              <Button type="submit" label="Register" class="w-full" :loading="loading" />
            </div>
          </form>

          <div class="mt-6">
            <div class="relative">
              <div class="absolute inset-0 flex items-center">
                <div class="w-full border-t border-gray-300"></div>
              </div>
              <div class="relative flex justify-center text-sm">
                <span class="px-2 bg-gray-300 text-gray-700 rounded-full"
                  >Already have an account?</span
                >
              </div>
            </div>

            <div class="mt-6">
              <router-link to="/login">
                <Button label="Sign in instead" class="w-full" severity="secondary" />
              </router-link>
            </div>
          </div>
        </template>
      </Card>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/authStore'
import { useToast } from 'primevue/usetoast'

const authStore = useAuthStore()
const router = useRouter()
const toast = useToast()

const form = ref({
  username: '',
  email: '',
  password: '',
})

const confirmPassword = ref('')
const loading = ref(false)

const errors = ref({
  username: '',
  email: '',
  password: '',
  confirmPassword: '',
})

const passwordsMatch = computed(() => {
  return form.value.password === confirmPassword.value
})

const handleRegister = async () => {
  loading.value = true
  errors.value = { username: '', email: '', password: '', confirmPassword: '' }

  if (!passwordsMatch.value) {
    errors.value.confirmPassword = 'Passwords do not match'
    loading.value = false
    return
  }

  try {
    await authStore.register(form.value)
    toast.add({
      severity: 'success',
      summary: 'Success',
      detail: 'Account created successfully. You can now log in.',
      life: 5000,
    })
    router.push('/login')
  } catch (error: any) {
    if (error.response?.data?.errors) {
      errors.value = { ...errors.value, ...error.response.data.errors }
    } else {
      toast.add({
        severity: 'error',
        summary: 'Registration Failed',
        detail: error.response?.data?.message || 'An error occurred during registration',
        life: 5000,
      })
    }
  } finally {
    loading.value = false
  }
}
</script>
