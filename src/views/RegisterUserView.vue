<template>
  <div class="min-h-screen bg-gray-50 flex items-center justify-center">
    <div class="max-w-md w-full mx-4">
      <Card class="shadow-lg">
        <template #header>
          <div class="text-center pt-6">
            <h1 class="text-2xl font-bold text-gray-800">Choose Username</h1>
            <p class="text-gray-600 mt-2">Create your unique username</p>
          </div>
        </template>

        <template #content>
          <form @submit.prevent="handleSubmit" class="space-y-6">
            <div class="space-y-2">
              <label for="username" class="block text-sm font-medium text-gray-700">
                Username
              </label>
              <InputText
                id="username"
                v-model="username"
                placeholder="Enter your username"
                :disabled="loading"
                class="w-full"
                :class="{ 'p-invalid': error }"
              />
            </div>

            <Button
              type="submit"
              :disabled="loading || !username.trim()"
              :loading="loading"
              class="w-full"
              severity="primary"
            >
              Create Username
            </Button>
          </form>

          <Message v-if="error" severity="error" class="mt-4" :closable="false">
            {{ error }}
          </Message>

          <div class="mt-4 text-center">
            <Button @click="handleSignOut" severity="secondary" text size="small">
              Sign out
            </Button>
          </div>
        </template>
      </Card>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useAuth } from '@/composables/useAuth'
import { useRouter } from 'vue-router'

const { registerUsername, signOut, loading, error } = useAuth()
const router = useRouter()

const username = ref('')

const handleSubmit = async () => {
  const user = await registerUsername(username.value)
  if (user) {
    router.push('/')
  }
}

const handleSignOut = async () => {
  await signOut()
  router.push('/login')
}
</script>
