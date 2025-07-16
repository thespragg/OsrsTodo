<template>
  <div class="min-h-screen bg-gray-50">
    <div class="bg-white shadow">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div class="flex justify-between items-center py-6">
          <div>
            <h1 class="text-3xl font-bold text-gray-900">OSRS Todo</h1>
            <p class="text-gray-600">Welcome back, {{ currentUser?.username }}!</p>
          </div>
          <div class="flex items-center space-x-4">
            <Avatar
              :label="currentUser?.username?.charAt(0).toUpperCase()"
              class="bg-blue-500 text-white"
            />
            <Button @click="handleSignOut" severity="secondary" outlined>
              <i class="pi pi-sign-out mr-2"></i>
              Sign Out
            </Button>
          </div>
        </div>
      </div>
    </div>

    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
      <Card>
        <template #header>
          <div class="p-6 border-b">
            <h2 class="text-xl font-semibold">Dashboard</h2>
          </div>
        </template>

        <template #content>
          <div class="text-center py-12">
            <i class="pi pi-check-circle text-6xl text-green-500 mb-4"></i>
            <h3 class="text-xl font-semibold mb-2">You're all set!</h3>
            <p class="text-gray-600">Your todo app is ready to go.</p>
          </div>
        </template>
      </Card>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useAuth } from '@/composables/useAuth'
import { useRouter } from 'vue-router'

const { currentUser, signOut } = useAuth()
const router = useRouter()

const handleSignOut = async () => {
  await signOut()
  router.push('/login')
}
</script>
