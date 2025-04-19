export interface TodoItem {
  id: number
  name: string
  icon: string
  completed: boolean
  children: TodoItem[]
}

export interface TrackedItem {
  id: number
  name: string
  icon: string
  count: number
  target: number
}

export interface ChoreItem {
  id: number
  name: string
  icon: string
  streak: number
  history: Record<string, boolean>
}

export interface IconOption {
  name: string
  value: string
}

export interface HeatmapDay {
  date: string
  completed: boolean
}

export type AccessToken = {
  token: string
  expires: string
}

export type Account = {
  id: string
  username: string
  userId: string
}
