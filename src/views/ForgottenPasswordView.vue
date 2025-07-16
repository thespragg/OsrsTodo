<template>
  <div
    class="min-h-screen w-full bg-gradient-to-br from-blue-50 to-indigo-100 flex items-center justify-center px-4 sm:px-6 lg:px-8"
  >
    <div class="max-w-md w-full space-y-8">
      <div class="text-center">
        <h2 class="mt-6 text-3xl font-extrabold text-gray-900">Reset your password</h2>
        <p class="mt-2 text-sm text-gray-600">Enter your email to receive reset instructions</p>
      </div>

      <div class="bg-white rounded-xl shadow-lg p-8">
        <form @submit.prevent="handleForgotPassword" class="space-y-4 w-full">
          <div>
            <label for="email" class="block text-sm font-medium text-gray-700 mb-2"
              >Email Address</label
            >
            <InputText
              id="email"
              v-model="email"
              type="email"
              placeholder="Enter your email"
              class="w-full"
              @blur="v$.email.$touch"
            />
            <div v-if="v$.email.$error" class="mt-1">
              <small class="text-red-500">{{ v$.email.$errors[0]?.$message }}</small>
            </div>
          </div>

          <div class="space-y-3 w-full">
            <Button
              type="submit"
              :loading="loading"
              :disabled="loading"
              class="!w-full bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 px-4 rounded-md transition-colors"
              icon="pi pi-envelope"
            >
              {{ loading ? 'Sending...' : 'Send reset link' }}
            </Button>
          </div>
        </form>

        <div class="mt-6 text-center">
          <router-link to="/login" class="text-blue-600 hover:text-blue-500 font-medium"
            >Back to login</router-link
          >
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useVuelidate } from '@vuelidate/core'
import { required, email as emailRule, helpers } from '@vuelidate/validators'
import InputText from 'primevue/inputtext'
import Button from 'primevue/button'
import { supabase } from '@/lib/supabase'

const router = useRouter()

const email = ref('')
const loading = ref(false)

const rules = {
  email: {
    required: helpers.withMessage('Email is required', required),
    email: helpers.withMessage('Please enter a valid email', emailRule),
  },
}

const v$ = useVuelidate(rules, { email })

const handleForgotPassword = async () => {
  const isValid = await v$.value.$validate()
  if (!isValid) return

  loading.value = true

  try {
    const { error } = await supabase.auth.resetPasswordForEmail(email.value)
    if (error) throw error

    await router.push('/login')
  } catch (error) {
    console.error('Password reset failed:', error)
  } finally {
    loading.value = false
  }
}
</script>
