import './App.css'
import Task from './pages/Task'
import { RelayEnvironmentProvider } from 'react-relay'
import relayEnvironment from './relay/Enviornment'
import { ToastContainer } from '@adobe/react-spectrum'

function App() {

  return (
    <div>
      <RelayEnvironmentProvider environment={relayEnvironment}>
          <Task />
          <ToastContainer/>
      </RelayEnvironmentProvider>
    </div>
  )
}

export default App
