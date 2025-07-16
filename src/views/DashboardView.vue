<template>
  <p @click="signOut">Dashboard</p>
</template>

<script setup lang="ts">
import { supabase } from '@/lib/supabase'
import router from '@/router'

const signOut = async () => {
  await supabase.auth.signOut()
  router.push({ name: 'login' })
}
</script>
