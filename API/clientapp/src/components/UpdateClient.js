import React, { useState, useEffect, useCallback } from 'react';
import axios from 'axios';
import { useNavigate, useParams } from 'react-router-dom';

function UpdateClient() {
    const { id } = useParams();
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const navigate = useNavigate();

    const fetchClient = useCallback(async () => {
        try {
            const response = await axios.get(`http://localhost:5142/api/clients/${id}`);
            setName(response.data.name);
            setEmail(response.data.email);
        } catch (error) {
            console.error('Error fetching client:', error);
        }
    }, [id]);

    useEffect(() => {
        fetchClient();
    }, [fetchClient]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await axios.put(`http://localhost:5142/api/clients/${id}`, { id, name, email, addresses: [] });
            navigate('/');
        } catch (error) {
            console.error('Error updating client:', error);
        }
    };

    return (
        <div>
            <h2>
                <i className="fas fa-edit"></i> Update Client
            </h2>
            <form onSubmit={handleSubmit}>
                <div className="mb-3">
                    <label className="form-label">Name</label>
                    <input type="text" className="form-control" value={name} onChange={(e) => setName(e.target.value)} required />
                </div>
                <div className="mb-3">
                    <label className="form-label">Email</label>
                    <input type="email" className="form-control" value={email} onChange={(e) => setEmail(e.target.value)} required />
                </div>
                <button type="submit" className="btn btn-primary">
                    <i className="fas fa-check"></i> Update
                </button>
            </form>
        </div>
    );
}

export default UpdateClient;