
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';

function ClientList() {
    const [clients, setClients] = useState([]);

    useEffect(() => {
        fetchClients();
    }, []);

    const fetchClients = async () => {
        try {
            const response = await axios.get('http://localhost:5142/api/clients');
            setClients(response.data);
        } catch (error) {
            console.error('Error fetching clients:', error);
        }
    };

    const deleteClient = async (id) => {
        try {
            await axios.delete(`http://localhost:5142/api/clients/${id}`);
            fetchClients();
        } catch (error) {
            console.error('Error deleting client:', error);
        }
    };

    return (
        <div>
            <h2>
                <i className="fas fa-list"></i> Clients
            </h2>
            <Link to="/create" className="btn btn-primary mb-3">
                <i className="fas fa-plus"></i> Create Client
            </Link>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {clients.map(client => (
                        <tr key={client.id}>
                            <td>{client.name}</td>
                            <td>{client.email}</td>
                            <td>
                                <Link to={`/update/${client.id}`} className="btn btn-warning btn-sm me-2">
                                    <i className="fas fa-edit"></i> Edit
                                </Link>
                                <button className="btn btn-danger btn-sm" onClick={() => deleteClient(client.id)}>
                                    <i className="fas fa-trash"></i> Delete
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default ClientList;
