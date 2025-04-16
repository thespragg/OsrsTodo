<template>
  <main>
    <p>test</p>
  </main>
</template>

<script lang="ts" setup>
import { useToast } from 'primevue/usetoast'
// import { useAuthStore } from '@/stores/authStore'

// const auth = useAuthStore()
// auth.clearToken()

const toast = useToast()
toast.add({ severity: 'info', summary: 'Info', detail: 'Message Content', life: 3000 })
</script>
