
import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import ClientList from './components/ClientList';
import CreateClient from './components/CreateClient';
import UpdateClient from './components/UpdateClient';

function App() {
    return (
        <Router>
            <div className="container mt-4">
                <nav className="navbar navbar-expand-lg navbar-light bg-light mb-4">
                    <Link className="navbar-brand" to="/">
                        <i className="fas fa-users"></i> Client Management
                    </Link>
                </nav>
                <Routes>
                    <Route path="/" element={<ClientList />} />
                    <Route path="/create" element={<CreateClient />} />
                    <Route path="/update/:id" element={<UpdateClient />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
