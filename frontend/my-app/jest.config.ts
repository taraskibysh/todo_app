import type { Config } from 'jest'

const config: Config = {
  preset: 'ts-jest',
  testEnvironment: 'jsdom',
  setupFilesAfterEnv: ['<rootDir>/src/setupTests.ts'],
  moduleNameMapper: {
    '\\.(css|less|sass|scss)$': 'identity-obj-proxy',  // Мокаємо стилі
  },
  transform: {
    '^.+\\.(ts|tsx)$': 'ts-jest',   // Трансформація TypeScript
    '^.+\\.(js|jsx)$': 'babel-jest' // Якщо є JS + Babel
  }
}

export default config
