import { createClient } from '@supabase/supabase-js'

const supabaseUrl = import.meta.env['VITE_SUPABASE_URL']
const supabaseKey = import.meta.env['VITE_SUPABASE_ANON_KEY']

export const supabase = createClient(supabaseUrl, supabaseKey)

export interface User {
  id: string
  username: string
  email: string
  created_at: string
}

export interface AuthUser {
  id: string
  email: string
  user_metadata: {
    [key: string]: unknown
  }
}
