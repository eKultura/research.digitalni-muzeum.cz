import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import './index.css';
import App from './App.jsx';
import { BrowserRouter, Routes, Route } from "react-router";
import DocumentFolders from './Components/DocumentFolders.jsx';
import Layout from './Components/Layout.jsx';
import SearchTool from './Components/SearchTool.jsx';

createRoot(document.getElementById('root')).render(
  <StrictMode>
     <BrowserRouter>
      <Routes>
      <Route path="/" element={<Layout />}>
        <Route index element={<App />} />
        <Route path="documents" element={<DocumentFolders />}/>
        <Route path="searching" element={<SearchTool />}/>
      </Route>
      </Routes>
    </BrowserRouter>
  </StrictMode>,
)
