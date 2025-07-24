import React, { useState } from 'react';
import MainAnalytics from './components/MainAnalytics';
import ClientSearch from './components/ClientSearch';
import { Box } from '@mui/material';
import ChatComponent from './components/ChatComponent';

function App() {
  const [selectedClient, setSelectedClient] = useState(null);

  return (
    <Box sx={{ display: 'flex', bgcolor: '#f5f5f5', minHeight: '100vh', width: '100%', overflowX: 'hidden' }}>
      <Box
        sx={{
          flex: 1,
          maxWidth: 1600,
          mx: 'auto',
          px: { xs: 1, md: 4 },
          py: { xs: 2, md: 4 },
          display: 'flex',
          flexDirection: 'column',
          gap: 0,
        }}
      >
        <ClientSearch onSelectClient={setSelectedClient} />
        <ChatComponent></ChatComponent>
        {selectedClient && <MainAnalytics client={selectedClient} />}
      </Box>
    </Box>
  );
}

export default App;


