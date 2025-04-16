import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

import ToastService from 'primevue/toastservice'
import PrimeVue from 'primevue/config'
import Nora from '@primeuix/themes/nora'

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use(ToastService)
app.use(PrimeVue, {
  theme: {
    preset: Nora,
  },
})

app.mount('#app')
