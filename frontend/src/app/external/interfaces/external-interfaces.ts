export interface Patient {
  id: number
  name: string
  cpf: string
  roomId: number | null
  doctorId: number | null
  nurseId: number | null
  createdAt: string
  updatedAt: string
  doctor?: Worker
  nurse?: Worker
}

export interface Worker {
  id: number
  name: string
  type: "Nurse" | "Doctor"
  createdAt: string
  updatedAt: string
}

export interface Appointment {
  id: number
  finished: boolean
  userId: number
  createdAt: string
  updatedAt: string
  patient: Patient
}

export interface PatientDTO {
  name: string
  cpf: string
  roomId?: number
  doctorId?: number
  nurseId?: number
}

export interface CreateModalData {
  title: string,
  isPatient: boolean
}

export interface SelectModalData {
  title: string,
  options: string[],
  placeholder: string
}