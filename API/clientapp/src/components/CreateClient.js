
import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function CreateClient() {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await axios.post('http://localhost:5142/api/clients', { name, email, addresses: [] });
            navigate('/');
        } catch (error) {
            console.error('Error creating client:', error);
        }
    };


    return (
        <div>
            <h2>
                <i className="fas fa-user-plus"></i> Create Client
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
                    <i className="fas fa-check"></i> Create
                </button>
            </form>
        </div>
    );
}

export default CreateClient;
