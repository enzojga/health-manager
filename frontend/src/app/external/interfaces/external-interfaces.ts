export interface Patient {
  id: number
  name: string
  cpf: string
  roomId: number | null
  doctorId: number | null
  nurseId: number | null
  createdAt: string
  updatedAt: string
  doctor?: Workers
  nurse?: Workers
}

export interface Workers {
  id: number
  name: string
  type: "Nurse" | "Doctor"
  createdAt: string
  updatedAt: string,
  patientAsNurse: Patient[],
  patientAsDoctor: Patient[]
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
  isPatient: boolean,
  isRoom: boolean
}

export interface SelectModalData {
  title: string,
  options: string[],
  placeholder: string
}

export interface Room {
  id: number
  capacity: number
  createdAt: string
  updatedAt: string
  patients: Patient[]
}

export interface RoomDTO {
  capacity: number
}

export interface ButtonsCard {
  icon: string,
  tooltip: string
}