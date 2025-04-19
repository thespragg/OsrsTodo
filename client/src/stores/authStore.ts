import { api } from '@/api'
import type { AccessToken } from '@/types'
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useAuthStore = defineStore('auth', () => {
  const token = ref(localStorage.getItem('accessToken') || null)
  const tokenExpires = ref(
    !localStorage.getItem('accessTokenExpires')
      ? null
      : localStorage.getItem('accessTokenExpires')!,
  )

  const login = async (loginRequest: { username: string; password: string }) => {
    try {
      const response = await api.user().login(loginRequest)
      setToken(response)
      return response
    } catch (error) {
      clearToken()
      throw error
    }
  }

  const register = async (register: { username: string; email: string; password: string }) => {
    try {
      const response = await api.user().register(register)
      return response
    } catch (error) {
      throw error
    }
  }

  const setToken = (accessToken: AccessToken) => {
    token.value = accessToken.token
    tokenExpires.value = accessToken.expires

    localStorage.setItem('accessToken', accessToken.token)
    localStorage.setItem('accessTokenExpires', accessToken.expires)
  }

  const clearToken = () => {
    token.value = null
    tokenExpires.value = null

    localStorage.removeItem('accessToken')
    localStorage.removeItem('accessTokenExpires')
  }

  const isAuthenticated = () => {
    if (!token.value || !tokenExpires.value) return false
    return new Date(tokenExpires.value) > new Date()
  }

  return {
    token,
    tokenExpires,
    login,
    register,
    setToken,
    clearToken,
    isAuthenticated,
  }
})
