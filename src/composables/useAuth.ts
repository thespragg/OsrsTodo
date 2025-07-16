import { ref, computed } from 'vue'
import { supabase, type User, type AuthUser } from '@/lib/supabase'

const currentAuthUser = ref<AuthUser | null>(null)
const currentUser = ref<User | null>(null)
const loading = ref(false)
const error = ref('')

export const useAuth = () => {
  const isAuthenticated = computed(() => !!currentAuthUser.value)
  const hasUsername = computed(() => !!currentUser.value)

  const signUp = async (email: string, password: string) => {
    loading.value = true
    error.value = ''

    try {
      const { data, error: signUpError } = await supabase.auth.signUp({
        email,
        password,
        options: {
          emailRedirectTo: `${window.location.origin}/OsrsTodo/`,
        },
      })

      if (signUpError) throw signUpError

      return data
    } catch (err: unknown) {
      console.error('Sign up error:', err)
      if (err instanceof Error) {
        error.value = err.message || 'Failed to sign up'
      } else {
        error.value = 'Failed to sign up'
      }
    } finally {
      loading.value = false
    }
  }

  const signIn = async (email: string, password: string) => {
    loading.value = true
    error.value = ''

    try {
      const { data, error: signInError } = await supabase.auth.signInWithPassword({
        email,
        password,
      })

      if (signInError) throw signInError

      return data
    } catch (err: unknown) {
      console.error('Sign in error:', err)
      if (err instanceof Error) {
        error.value = err.message || 'Failed to sign in'
      } else {
        error.value = 'Failed to sign in'
      }
    } finally {
      loading.value = false
    }
  }

  const signOut = async () => {
    loading.value = true
    error.value = ''

    try {
      const { error: signOutError } = await supabase.auth.signOut()
      if (signOutError) throw signOutError

      currentAuthUser.value = null
      currentUser.value = null
    } catch (err: unknown) {
      console.error('Sign out error:', err)
      if (err instanceof Error) {
        error.value = err.message || 'Failed to sign out'
      } else {
        error.value = 'Failed to sign out'
      }
    } finally {
      loading.value = false
    }
  }

  const registerUsername = async (username: string) => {
    if (!currentAuthUser.value) {
      error.value = 'Must be signed in to register username'
      return
    }

    loading.value = true
    error.value = ''

    try {
      const { data: newUser, error: insertError } = await supabase
        .from('users')
        .insert([
          {
            username: username.trim(),
            email: currentAuthUser.value.email,
          },
        ])
        .select()
        .single()

      if (insertError) throw insertError

      currentUser.value = newUser
      return newUser
    } catch (err: unknown) {
      console.error('Username registration error:', err)
      if (typeof err === 'object' && err !== null && 'code' in err) {
        if ((err as { code: string }).code === '23505') {
          error.value = 'Username already taken'
        } else if ('message' in err) {
          error.value = (err as { message: string }).message || 'Failed to register username'
        } else {
          error.value = 'Failed to register username'
        }
      } else {
        error.value = 'Failed to register username'
      }
    } finally {
      loading.value = false
    }
  }

  const loadUserByEmail = async (email: string) => {
    loading.value = true
    error.value = ''

    try {
      const { data: user, error: fetchError } = await supabase
        .from('users')
        .select('*')
        .eq('email', email)
        .single()

      if (fetchError && fetchError.code !== 'PGRST116') {
        throw fetchError
      }

      currentUser.value = user || null
      return user
    } catch (err: unknown) {
      console.error('Load user error:', err)
      if (err instanceof Error) {
        error.value = err.message || 'Failed to load user'
      } else {
        error.value = 'Failed to load user'
      }
    } finally {
      loading.value = false
    }
  }

  const initializeAuth = async () => {
    loading.value = true

    try {
      const {
        data: { session },
      } = await supabase.auth.getSession()

      if (session?.user) {
        currentAuthUser.value = session.user as AuthUser
        await loadUserByEmail(session.user.email!)
      }

      supabase.auth.onAuthStateChange(async (event, session) => {
        if (event === 'SIGNED_IN' && session?.user) {
          currentAuthUser.value = session.user as AuthUser
          await loadUserByEmail(session.user.email!)
        } else if (event === 'SIGNED_OUT') {
          currentAuthUser.value = null
          currentUser.value = null
        }
      })
    } catch (err: unknown) {
      console.error('Auth initialization error:', err)
      if (err instanceof Error) {
        error.value = err.message || 'Failed to initialize authentication'
      } else {
        error.value = 'Failed to initialize authentication'
      }
    } finally {
      loading.value = false
    }
  }

  return {
    currentAuthUser,
    currentUser,
    loading,
    error,
    isAuthenticated,
    hasUsername,
    signUp,
    signIn,
    signOut,
    registerUsername,
    loadUserByEmail,
    initializeAuth,
  }
}
