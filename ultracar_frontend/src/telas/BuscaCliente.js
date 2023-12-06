import React, { useState, useEffect } from 'react';
import QrReader from 'react-qr-reader';
import { useNavigate } from 'react-router-dom';
import api from '../axiosConfig';

function BuscaCliente() {
    const [clienteId, setClienteId] = useState('');
    const [erro, setErro] = useState('');
    const [scannerAtivo, setScannerAtivo] = useState(false);
    const [buscarAoDigitar, setBuscarAoDigitar] = useState(false); // Novo estado
    const navigate = useNavigate();
    const [scannerKey, setScannerKey] = useState(Date.now());

    const handleScan = (data) => {
        if (data) {
            setClienteId(data);
            setBuscarAoDigitar(true); // Ativar busca ao escanear
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
            if (response.data) {
                navigate(`./DetalhesCliente/${clienteId}`);
            } else {
                setErro('Cliente não encontrado');
            }
        } catch (error) {
            console.error('Erro ao buscar cliente:', error);
            setErro('Cliente não encontrado');
        }
    };

    useEffect(() => {
        if (buscarAoDigitar) {
            buscarCliente();
            setBuscarAoDigitar(false); // Reseta o estado para evitar nova busca ao digitar
        }
    }, [clienteId, buscarAoDigitar]);

    const toggleScanner = () => {
        setScannerAtivo((prevScannerAtivo) => !prevScannerAtivo);
        setErro('');
        if (!scannerAtivo) {
            setScannerKey(Date.now());
        }
    };

    useEffect(() => {
        if (scannerAtivo) {
            setScannerKey(Date.now());
        }
    }, [scannerAtivo]);

    return (
        <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
            <h2>Buscar Cliente</h2>
            <input type="text" value={clienteId} onChange={(e) => setClienteId(e.target.value)} />
            <button onClick={buscarCliente}>Buscar</button> {/* Botão específico para buscar */}
            <button onClick={toggleScanner}>
                {scannerAtivo ? 'Desativar Scanner' : 'Ativar Scanner'}
            </button>
            {erro && <p>{erro}</p>}
            <div style={{ width: '300px', height: '300px', display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                {scannerAtivo && (
                    <QrReader
                        key={scannerKey}
                        delay={300}
                        onError={handleError}
                        onScan={handleScan}
                        style={{ width: '100%', height: '100%' }}
                        videoId="qr-video"
                    />
                )}
            </div>
        </div>
    );
}

export default BuscaCliente;
