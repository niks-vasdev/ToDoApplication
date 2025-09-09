import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { defaultTheme, Provider } from '@adobe/react-spectrum'
import './style/task.css'
createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Provider theme={defaultTheme}>
      <App />
    </Provider>
  </StrictMode>,
)
