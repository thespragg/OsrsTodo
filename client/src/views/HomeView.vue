<template>
  <div class="flex flex-col h-screen w-screen overflow-hidden bg-neutral-950">
    <div class="bg-neutral-900 shadow-sm p-4 flex items-center justify-end">
      <Dropdown
        v-model="selectedAccount"
        :options="accountStore.accounts.map((x) => x.username)"
        optionLabel=""
        placeholder="Select an account"
        class="w-full md:w-64"
      />
    </div>

    <div class="flex flex-col md:flex-row flex-1 overflow-hidden">
      <div class="flex-1 overflow-y-auto p-4 bg-neutral-900 m-2 rounded-lg shadow">
        <h2 class="text-xl font-semibold mb-4">Main Content</h2>
        <div class="space-y-4">
          <p>This is the large left section that takes most space on desktop.</p>
        </div>
      </div>

      <div class="flex flex-col md:w-96 overflow-hidden">
        <div class="flex-1 overflow-y-auto p-4 bg-neutral-900 m-2 rounded-lg shadow">
          <h2 class="text-xl font-semibold mb-4">Top Right</h2>
          <div class="space-y-2">
            <p>This is the top part of the vertically split right section.</p>
          </div>
        </div>

        <div class="flex-1 overflow-y-auto p-4 bg-neutral-900 m-2 rounded-lg shadow">
          <h2 class="text-xl font-semibold mb-4">Bottom Right</h2>
          <div class="space-y-2">
            <p>This is the bottom part of the vertically split right section.</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useAccountsStore } from '@/stores/accountStore'

const accountStore = useAccountsStore()
const selectedAccount = ref(null)
</script>
