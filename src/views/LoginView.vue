<template>
  <div
    class="min-h-screen w-full bg-gradient-to-br from-blue-50 to-indigo-100 flex items-center justify-center px-4 sm:px-6 lg:px-8"
  >
    <div class="max-w-md w-full space-y-8">
      <div class="text-center">
        <h2 class="mt-6 text-3xl font-extrabold text-gray-900">Sign in to your account</h2>
        <p class="mt-2 text-sm text-gray-600">Enter your credentials to access your Osrs Todo</p>
      </div>

      <div class="bg-white rounded-xl shadow-lg p-8">
        <form @submit.prevent="handleLogin" class="space-y-4 w-full">
          <div>
            <label for="email" class="block text-sm font-medium text-gray-700 mb-2">
              Email Address
            </label>
            <InputText
              id="email"
              v-model="formData.email"
              type="email"
              placeholder="Enter your email"
              class="w-full"
              @blur="v$.email.$touch"
            />
            <div v-if="v$.email.$error" class="mt-1">
              <small class="text-red-500">
                {{ v$.email.$errors[0]?.$message }}
              </small>
            </div>
          </div>

          <div>
            <label for="password" class="block text-sm font-medium text-gray-700 mb-2">
              Password
            </label>
            <Password
              id="password"
              inputClass="w-full"
              class="block w-full"
              v-model="formData.password"
              placeholder="Enter your password"
              :feedback="false"
              toggleMask
              @blur="v$.password.$touch"
            />
            <div v-if="v$.password.$error" class="mt-1">
              <small class="text-red-500">
                {{ v$.password.$errors[0]?.$message }}
              </small>
            </div>
          </div>

          <div class="flex items-center justify-between">
            <router-link
              :to="{ name: 'forgot-password' }"
              class="text-sm text-blue-600 hover:text-blue-500"
            >
              Forgot password?
            </router-link>
          </div>

          <div class="space-y-3 w-full">
            <Button
              type="submit"
              :loading="loading"
              :disabled="loading"
              class="!w-full bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 px-4 rounded-md transition-colors"
              icon="pi pi-sign-in"
            >
              {{ loading ? 'Signing in...' : 'Sign in' }}
            </Button>
          </div>
        </form>

        <div class="mt-6 text-center">
          <p class="text-gray-600">
            Don't have an account?
            <router-link
              :to="{ name: 'register' }"
              class="text-blue-600 hover:text-blue-500 font-medium"
            >
              Sign up here
            </router-link>
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useVuelidate } from '@vuelidate/core'
import { required, email, minLength, helpers } from '@vuelidate/validators'
import InputText from 'primevue/inputtext'
import Password from 'primevue/password'
import Button from 'primevue/button'
import { supabase } from '@/lib/supabase'
import type { SignInWithPasswordCredentials } from '@supabase/supabase-js'

interface LoginFormData {
  email: string
  password: string
}

const router = useRouter()

const formData = reactive<LoginFormData>({
  email: '',
  password: '',
})

const loading = ref<boolean>(false)

const rules = {
  email: {
    required: helpers.withMessage('Email is required', required),
    email: helpers.withMessage('Please enter a valid email', email),
  },
  password: {
    required: helpers.withMessage('Password is required', required),
    minLength: helpers.withMessage('Password must be at least 6 characters', minLength(6)),
  },
}

const v$ = useVuelidate(rules, formData)

const handleLogin = async (): Promise<void> => {
  const isValid = await v$.value.$validate()

  if (!isValid) {
    return
  }

  loading.value = true

  try {
    const credentials: SignInWithPasswordCredentials = {
      email: formData.email,
      password: formData.password,
    }

    await supabase.auth.signInWithPassword(credentials)

    await router.push('/')
  } catch (error) {
    console.error('Login failed:', error)
  } finally {
    loading.value = false
  }
}
</script>
