import React, { useState } from 'react';
import api from '../axiosConfig'; // Importe o Axios configurado
import QrReader from 'react-qr-reader';

function BuscaCliente() {
    const [clienteId, setClienteId] = useState('');
    const [cliente, setCliente] = useState(null);
    const [erro, setErro] = useState('');
    const [scannerAtivo, setScannerAtivo] = useState(false);

    const handleScan = (data) => {
        if (data) {
            setClienteId(data);
            setScannerAtivo(false);
        }
    };

    const handleError = (err) => {
        console.error(err);
        setErro('Erro ao escanear o código QR');
    };

    const buscarCliente = async () => {
        try {
            const response = await api.get(`/cliente/buscarClientePorId/${clienteId}`);
            setCliente(response.data);
            setErro('');
        } catch (error) {
            console.error('Erro ao buscar cliente:', error);
            setCliente(null);
            setErro('Cliente não encontrado');
        }
    };

    const toggleScanner = () => {
        setScannerAtivo((prevScannerAtivo) => !prevScannerAtivo);
        setErro('');
    };

    return (
        <div>
            <h2>Buscar Cliente</h2>
            <input type="text" value={clienteId} onChange={(e) => setClienteId(e.target.value)} />
            <button onClick={buscarCliente}>Buscar</button>
            <button onClick={toggleScanner}>
                {scannerAtivo ? 'Desativar Scanner' : 'Ativar Scanner'}
            </button>
            {erro && <p>{erro}</p>}
            {scannerAtivo && (
                <QrReader
                    delay={300}
                    onError={handleError}
                    onScan={handleScan}
                    style={{ width: '100%' }}
                />
            )}
        </div>
    );
}

export default BuscaCliente;
