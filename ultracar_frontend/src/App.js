import React from 'react';
import logo from './logo.svg';
import './App.css';
import BuscaCliente from './telas/BuscaCliente';
import DetalhesCliente from './telas/DetalhesCliente';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';



function App() {
  return (
    <div className="App">

      <Router>
        <Routes>
          <Route path="/" element={<BuscaCliente />} />
          <Route path="DetalhesCliente/:id" element={<DetalhesCliente />} />
        </Routes>
      </Router>


    </div>
  );
}

export default App;
