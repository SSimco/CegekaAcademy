import './App.css';
import { Pets } from './Pages/Pets';
import { Route, Routes } from 'react-router-dom';
import { Home } from './Pages/Home';
import { Fundraisers } from './Pages/Fundraiser';
import { NoMatch } from './Pages/NoMatch';
import { Layout } from './HOC/Layout';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'

function App() {
  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Layout>
        <Routes>
          <Route path="/Pets" element={<Pets></Pets>} />
          <Route path="/Fundraisers" element={<Fundraisers></Fundraisers>} />
          <Route path="/" element={<Home></Home>} />
          <Route path="*" element={<NoMatch></NoMatch>} />
        </Routes>
      </Layout>
    </LocalizationProvider>
  );
}

export default App;
