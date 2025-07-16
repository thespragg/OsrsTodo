<template>
  <div class="min-h-screen bg-gray-50 flex items-center justify-center">
    <div class="max-w-md w-full mx-4">
      <Card class="shadow-lg">
        <template #header>
          <div class="text-center pt-6">
            <h1 class="text-3xl font-bold text-gray-800">OSRS Todo</h1>
            <p class="text-gray-600 mt-2">
              {{ isSignUp ? 'Create your account' : 'Sign in to your account' }}
            </p>
          </div>
        </template>

        <template #content>
          <form @submit.prevent="handleSubmit" class="space-y-6">
            <div class="space-y-2">
              <label for="email" class="block text-sm font-medium text-gray-700"> Email </label>
              <InputText
                id="email"
                v-model="email"
                type="email"
                placeholder="Enter your email"
                :disabled="loading"
                class="w-full"
                :class="{ 'p-invalid': error }"
              />
            </div>

            <div class="space-y-2">
              <label for="password" class="block text-sm font-medium text-gray-700">
                Password
              </label>
              <Password
                id="password"
                v-model="password"
                placeholder="Enter your password"
                :disabled="loading"
                class="w-full"
                :class="{ 'p-invalid': error }"
                :feedback="false"
                toggle-mask
              />
            </div>

            <Button
              type="submit"
              :disabled="loading || !email.trim() || !password.trim()"
              :loading="loading"
              class="w-full"
              severity="primary"
            >
              {{ isSignUp ? 'Create Account' : 'Sign In' }}
            </Button>
          </form>

          <div class="mt-4 text-center">
            <Button @click="isSignUp = !isSignUp" severity="secondary" text size="small">
              {{ isSignUp ? 'Already have an account? Sign in' : 'Need an account? Sign up' }}
            </Button>
          </div>

          <Message v-if="error" severity="error" class="mt-4" :closable="false">
            {{ error }}
          </Message>

          <Message v-if="showEmailConfirmation" severity="info" class="mt-4" :closable="false">
            Please check your email and click the confirmation link to complete your registration.
          </Message>
        </template>
      </Card>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useAuth } from '@/composables/useAuth'

const { signIn, signUp, loading, error } = useAuth()

const email = ref('')
const password = ref('')
const isSignUp = ref(false)
const showEmailConfirmation = ref(false)

const handleSubmit = async () => {
  showEmailConfirmation.value = false

  if (isSignUp.value) {
    const result = await signUp(email.value, password.value)
    if (result && !result.user?.email_confirmed_at) {
      showEmailConfirmation.value = true
    }
  } else {
    const result = await signIn(email.value, password.value)
    if (result) {
      // Router guard will handle redirect
    }
  }
}
</script>
